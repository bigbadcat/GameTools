using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using XuXiang.ClassLibrary;
using System.Text;
using System.IO;

namespace T008
{
    /// <summary>
    /// 配置文档。
    /// </summary>
    public class ConfigArchive
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ConfigArchive()
        {
        }

        /// <summary>
        /// 删除。
        /// </summary>
        public static void Destroy()
        {
            if (_instance != null)
            {
                if (_instance.m_eaCurExcelApp != null)
                {
                    _instance.m_eaCurExcelApp.Quit();
                    _instance.m_eaCurExcelApp = null;
                }
            }
        }

        /// <summary>
        /// 加载文件。
        /// </summary>
        /// <param name="file">文件路径。</param>
        /// <returns>是否加载成功。</returns>
        public bool Load(string file)
        {
            //清除空数据
            m_strConfigFilePath = file;
            m_strSourceFolder = string.Empty;
            m_strExportFolder = string.Empty;
            m_dicEnumInfos.Clear();
            m_dicExportInfos.Clear();

            bool ok = true;
            try
            {
                //打开XML文件
                XmlDocument doc = new XmlDocument();
                doc.Load(m_strConfigFilePath);

                //配置
                XmlNode root = doc.SelectSingleNode("ExportConfig");
                XmlNode config = root.SelectSingleNode("Config");
                m_strSourceFolder = XmlUtil.GetAttribute(config.SelectSingleNode("SourceFolder"), "Value").Trim();
                m_strExportFolder = XmlUtil.GetAttribute(config.SelectSingleNode("ExportFolder"), "Value").Trim();
                m_strBuildNameSpace = XmlUtil.GetAttribute(config.SelectSingleNode("BuildNameSpace"), "Value").Trim();
                m_strBuildPath = XmlUtil.GetAttribute(config.SelectSingleNode("BuildPath"), "Value").Trim();

                //获取枚举定义 数据定义 导出配置      
                //ParseEnums(root.SelectSingleNode("Enums"));
                ParseDefineFiles(root.SelectSingleNode("DefineFiles"));                
                ParseExportGroups(root.SelectSingleNode("ExportGroups"));
                CheckClasses();
                InitExproter();
            }
            catch (Exception e)
            {
                ok = false;
                file = string.Empty;
                MainForm.CurForm.Log(e.Message);
            }

            return ok;
        }

        /// <summary>
        /// 获取枚举信息。
        /// </summary>
        /// <param name="name">枚举名称。</param>
        /// <returns>枚举信息。</returns>
        public EnumInfo GetEnumInfo(string name)
        {
            EnumInfo info;
            if (m_dicEnumInfos.TryGetValue(name, out info))
            {
                return info;
            }
            return null;
        }

        /// <summary>
        /// 获取类名。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ClassInfo GetClassInfo(String name)
        {
            ClassInfo info;
            if (m_dicClassInfos.TryGetValue(name, out info))
            {
                return info;
            }
            return null;
        }

        /// <summary>
        /// 获取导出配置。
        /// </summary>
        /// <param name="group">分组名称。</param>
        /// <param name="name">导出名称。</param>
        /// <returns>导出配置。</returns>
        public ExportInfo GetExportInfo(string group, string name)
        {
            ExportInfo info = null;
            List<ExportInfo> infos = null;
            if (m_dicExportInfos.TryGetValue(group, out infos))
            {
                foreach (ExportInfo tmp in infos)
                {
                    if (tmp.Name.CompareTo(name) == 0)
                    {
                        info = tmp;
                        break;
                    }
                }
            }
            return info;
        }

        /// <summary>
        /// 开始导出数据。
        /// </summary>
        /// <param name="paths">导出配置信息列表。</param>
        public void StartExport(List<string> paths, Action end = null)
        {
            Thread t = new Thread(delegate ()
            {
                MainForm form = MainForm.CurForm;
                if (m_eaCurExcelApp == null)
                {
                    form.Log("启动Excel...");
                    m_eaCurExcelApp = new Excel.ApplicationClass();
                }

                if (m_eaCurExcelApp != null)
                {
                    float step = 100.0f / Math.Max(1, paths.Count);
                    form.Log("开始导出...");
                    for (int i = 0; i < paths.Count; ++i)
                    {
                        form.Progress((int)(step * (i + 1)));
                        string path = paths[i];
                        string[] gn = path.Split('\\');
                        ExportInfo info = GetExportInfo(gn[0], gn[1]);
                        if (info != null)
                        {
                            if (!info.StartExport())
                            {
                                break;
                            }
                        }
                        else
                        {
                            form.Log("没有发现配置 \"{0}\"", path);
                        }
                    }

                    form.Progress(100);
                    Thread.Sleep(500);
                    form.Log("导出完毕。\n");
                }
                else
                {
                    form.Log("无法打开Excel");
                }

                end?.Invoke();
            });
            t.Start();
        }

        /// <summary>
        /// 生成定义文件。
        /// </summary>
        public void BuildDefineScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using XuXiang;");
            sb.AppendLine();
            sb.AppendLine(string.Format("namespace {0}", m_strBuildNameSpace));
            sb.AppendLine("{");

            //枚举
            foreach (var kvp in m_dicEnumInfos)
            {
                kvp.Value.BuildDefine(sb);
                sb.AppendLine();
            }

            //通用接口
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 数据信息接口，统一读取信息。");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    public interface IDataInfo");
            sb.AppendLine("    {");
            sb.AppendLine("        void ReadData(DataReader dr);");
            sb.AppendLine("    }");

            //数据类型
            int n = m_dicClassInfos.Count;
            foreach (var kvp in m_dicClassInfos)
            {
                kvp.Value.BuildDefine(sb);                
                if (--n > 0)
                {
                    //还有类药导出则继续换行
                    sb.AppendLine();
                }
            }
            sb.AppendLine("}");

            //写入文件
            FileStream fstream = new FileStream(BuildPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fstream);
            sw.WriteLine(sb.ToString());
            sw.Flush();
            fstream.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
            fstream.Dispose();
            fstream = null;
            GC.Collect();
            MainForm.CurForm.Log("生成定义文件成功:\n{0}", BuildPath);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取文档实例。
        /// </summary>
        public static ConfigArchive Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigArchive();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取配置文件路径。
        /// </summary>
        public string ConfigFilePath
        {
            get { return m_strConfigFilePath; }
        }

        /// <summary>
        /// 获取枚举定义集合。
        /// </summary>
        public Dictionary<string, EnumInfo> EnumInfos
        {
            get { return m_dicEnumInfos; }
        }

        /// <summary>
        /// 获取导出信息集合。
        /// </summary>
        public Dictionary<string, List<ExportInfo>> ExportInfos
        {
            get { return m_dicExportInfos; }
        }

        /// <summary>
        /// 获取数据源文件夹。
        /// </summary>
        public string SourceFolder
        {
            get
            {
                string p = m_strConfigFilePath.Substring(0, m_strConfigFilePath.LastIndexOf('\\'));  //配置文件所在文件夹
                if (!string.IsNullOrEmpty(m_strSourceFolder))
                {
                    p = p + "\\" + m_strSourceFolder;
                }
                return p;
            }
        }

        /// <summary>
        /// 获取导出文件夹。
        /// </summary>
        public string ExportFolder
        {
            get
            {
                string p = m_strConfigFilePath.Substring(0, m_strConfigFilePath.LastIndexOf('\\'));  //配置文件所在文件夹
                if (!string.IsNullOrEmpty(m_strExportFolder))
                {
                    p = p + "\\" + m_strExportFolder;
                }
                return p;
            }
        }

        /// <summary>
        /// 获取代码的生成路径。
        /// </summary>
        public string BuildPath
        {
            get
            {
                string p = m_strConfigFilePath.Substring(0, m_strConfigFilePath.LastIndexOf('\\'));  //配置文件所在文件夹
                p = p + "\\" + m_strBuildPath;
                return p;
            }
        }

        /// <summary>
        /// 获取Excel应用。
        /// </summary>
        public Excel.Application ExcelApp
        {
            get { return m_eaCurExcelApp; }
        }
        
        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 解析枚举定义。
        /// </summary>
        /// <param name="node">枚举定义列表节点。</param>
        private void ParseEnums(XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            ///加载定义。
            foreach (XmlNode tmp in node.ChildNodes)
            {
                EnumInfo info = new EnumInfo();
                info.AssignFromXmlNode(tmp);
                if (!m_dicEnumInfos.ContainsKey(info.Name))
                {
                    m_dicEnumInfos.Add(info.Name, info);
                }
                else
                {
                    MainForm.CurForm.Log("枚举({0})重复定义", info.Name);
                }
            }
        }

        /// <summary>
        /// 解析类型定义。
        /// </summary>
        /// <param name="node">类型定义节点。</param>
        private void ParseClasses(XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            ///加载定义。
            foreach (XmlNode tmp in node.ChildNodes)
            {
                ClassInfo info = new ClassInfo();
                info.AssignFromXmlNode(tmp);
                if (!m_dicClassInfos.ContainsKey(info.Name))
                {
                    m_dicClassInfos.Add(info.Name, info);
                }
                else
                {
                    MainForm.CurForm.Log("类型({0})重复定义", info.Name);
                }
            }
        }

        /// <summary>
        /// 解析定义文件列表。
        /// </summary>
        /// <param name="node">类型定义文件列表。</param>
        private void ParseDefineFiles(XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            //逐个文件加载
            string path = m_strConfigFilePath.Substring(0, m_strConfigFilePath.LastIndexOf('\\') + 1);  //配置文件所在文件夹
            foreach (XmlNode tmp in node.ChildNodes)
            {
                if (tmp.Name.CompareTo("File") == 0)
                {
                    string file = path + XmlUtil.GetAttribute(tmp, "Name");
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);
                    ParseTypeDefine(doc.SelectSingleNode("TypeDefine"));
                }
            }
        }

        /// <summary>
        /// 解析类型定义。
        /// </summary>
        /// <param name="node">类型定义节点。</param>
        private void ParseTypeDefine(XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            //加载定义
            foreach (XmlNode tmp in node.ChildNodes)
            {
                if (tmp.Name.CompareTo("Enum") == 0)
                {
                    EnumInfo info = new EnumInfo();
                    info.AssignFromXmlNode(tmp);
                    if (!m_dicEnumInfos.ContainsKey(info.Name))
                    {
                        m_dicEnumInfos.Add(info.Name, info);
                    }
                    else
                    {
                        MainForm.CurForm.Log("枚举({0})重复定义", info.Name);
                    }
                }
                else if (tmp.Name.CompareTo("Class") == 0)
                {
                    ClassInfo info = new ClassInfo();
                    info.AssignFromXmlNode(tmp);
                    if (!m_dicClassInfos.ContainsKey(info.Name))
                    {
                        m_dicClassInfos.Add(info.Name, info);
                    }
                    else
                    {
                        MainForm.CurForm.Log("类型({0})重复定义", info.Name);
                    }
                }
                else
                {
                    MainForm.CurForm.Log("未知类型定义:{0}", tmp.Name);
                }
            }
        }

        /// <summary>
        /// 检查类型包含关系是否合法。
        /// </summary>
        private void CheckClasses()
        {
            foreach (var kvp in m_dicClassInfos)
            {
                foreach (var finfo in kvp.Value.FieldInfos)
                {
                    ClassInfo cinfo = GetClassInfo(finfo.Type);
                    if (cinfo != null)
                    {
                        if (!cinfo.IsSimple)
                        {
                            MainForm.CurForm.Log("类型({0})的成员类型({1})不是简单类型", kvp.Value.Name, cinfo.Name);
                            break;
                        }

                        if (finfo.IsArray && cinfo.IsHaveArrayField)
                        {
                            MainForm.CurForm.Log("类型({0})的成员类型({1})含有数组，不能再作为数组成员", kvp.Value.Name, cinfo.Name);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化字段导出对象，必须要在枚举和数据定义加载完成后再进行。
        /// </summary>
        private void InitExproter()
        {
            foreach (var kvp in m_dicClassInfos)
            {
                List<FieldInfo> fields = kvp.Value.FieldInfos;
                foreach (FieldInfo info in fields)
                {
                    info.InitExporter();
                }
            }
        }

        /// <summary>
        /// 解析导出分组。
        /// </summary>
        /// <param name="node">导出分组列表节点。</param>
        private void ParseExportGroups(XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            //加载分组
            foreach (XmlNode group in node.ChildNodes)
            {
                //导出分组
                string groupname = XmlUtil.GetAttribute(group, "Name");
                List<ExportInfo> infos = new List<ExportInfo>();
                if (!m_dicExportInfos.TryGetValue(groupname, out infos))
                {
                    infos = new List<ExportInfo>();
                    m_dicExportInfos.Add(groupname, infos);
                }

                //导出列表
                foreach (XmlNode tmp in group.ChildNodes)
                {
                    ExportInfo info = new ExportInfo();
                    info.GroupName = groupname;
                    info.AssignFromXmlNode(tmp);
                    infos.Add(info);
                }
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 实例对象。
        /// </summary>
        private static ConfigArchive _instance = null;

        /// <summary>
        /// 配置文件路径。
        /// </summary>
        private string m_strConfigFilePath = string.Empty;

        /// <summary>
        /// 数据源文件夹路径，相对于配置文件所在的文件夹。
        /// </summary>
        private string m_strSourceFolder = string.Empty;

        /// <summary>
        /// 导出文件夹路径，相对于配置文件所在的文件夹。
        /// </summary>
        private string m_strExportFolder = string.Empty;

        /// <summary>
        /// 生成的命名空间。
        /// </summary>
        private string m_strBuildNameSpace = string.Empty;

        /// <summary>
        /// 代码生成文件路径。
        /// </summary>
        private string m_strBuildPath = string.Empty;

        /// <summary>
        /// 当前的Exe应用。
        /// </summary>
        private Excel.Application m_eaCurExcelApp = null;
        
        /// <summary>
        /// 类定义信息。
        /// </summary>
        private Dictionary<string, EnumInfo> m_dicEnumInfos = new Dictionary<string, EnumInfo>();

        /// <summary>
        /// 类定义信息。
        /// </summary>
        private Dictionary<string, ClassInfo> m_dicClassInfos = new Dictionary<string, ClassInfo>();

        /// <summary>
        /// 导出信息集合。
        /// </summary>
        private Dictionary<string, List<ExportInfo>> m_dicExportInfos = new Dictionary<string, List<ExportInfo>>();

        #endregion
    }
}
