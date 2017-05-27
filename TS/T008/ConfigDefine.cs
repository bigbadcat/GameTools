using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using XuXiang.ClassLibrary;
using System.IO;
using System.Reflection;

namespace T008
{
    #region 数据定义=====================================================================================

    /// <summary>
    /// 枚举数据。
    /// </summary>
    public class EnumInfo
    {
        /// <summary>
        /// 枚举名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 枚举描述。
        /// </summary>
        public string Note;

        /// <summary>
        /// 枚举项列表。
        /// </summary>
        public List<EnumItemInfo> Items = new List<EnumItemInfo>();

        /// <summary>
        /// 名称到信息索引的映射。
        /// </summary>
        private Dictionary<string, int> m_dicNoteToIndex = new Dictionary<string, int>();

        /// <summary>
        /// 通过描述获取枚举项。
        /// </summary>
        /// <param name="note">枚举项描述。</param>
        /// <returns>枚举项。</returns>
        public EnumItemInfo GetEnumItemInfo(string note)
        {
            int index;
            if (m_dicNoteToIndex.TryGetValue(note, out index))
            {
                return Items[index];
            }
            return null;
        }

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public void AssignFromXmlNode(XmlNode node)
        {
            Name = XmlUtil.GetAttribute(node, "Name");
            Note = XmlUtil.GetAttribute(node, "Note");
            Items.Clear();
            m_dicNoteToIndex.Clear();
            foreach (XmlNode tmp in node.ChildNodes)
            {
                EnumItemInfo info = new EnumItemInfo();
                info.AssignFromXmlNode(tmp);
                if (!m_dicNoteToIndex.ContainsKey(info.Note))
                {
                    m_dicNoteToIndex.Add(info.Note, Items.Count);
                    Items.Add(info);
                }
                else
                {
                    MainForm.CurForm.Log("枚举项描述重复 {0}.{1}", Name, info.Note);
                }
            }
        }

        /// <summary>
        /// 生成定义。
        /// </summary>
        /// <param name="sb">定义内容保存对象。</param>
        public void BuildDefine(StringBuilder sb)
        {
            sb.AppendLine("    /// <summary>");
            sb.AppendLine(string.Format("    /// {0}。", Note));
            sb.AppendLine("    /// </summary>");
            sb.AppendLine(string.Format("    public enum {0}", Name));
            sb.AppendLine("    {");
            for (int i = 0; i < Items.Count; ++i)
            {
                EnumItemInfo info = Items[i];
                sb.AppendLine("        /// <summary>");
                sb.AppendLine(string.Format("        /// {0}。", info.Note));
                sb.AppendLine("        /// </summary>");
                sb.AppendLine(string.Format("        {0} = {1},", info.Name, info.Value));
                if (i < Items.Count - 1)
                {
                    sb.AppendLine();        //不是最后一项留空行
                }
            }
            sb.AppendLine("    }");
        }
    }

    /// <summary>
    /// 枚举项数据。
    /// </summary>
    public class EnumItemInfo
    {
        /// <summary>
        /// 枚举值。
        /// </summary>
        public int Value;

        /// <summary>
        /// 枚举项名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 枚举项描述。
        /// </summary>
        public string Note;

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public void AssignFromXmlNode(XmlNode node)
        {
            Value = XmlUtil.GetAttributeInt(node, "Value");
            Name = XmlUtil.GetAttribute(node, "Name");
            Note = XmlUtil.GetAttribute(node, "Note");
        }
    }

    /// <summary>
    /// 字段信息。
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// 中文名称，用于定位Excel中的列。
        /// </summary>
        public string CName;

        /// <summary>
        /// 变量名称，用于生成代码加载用。
        /// </summary>
        public string VName;

        /// <summary>
        /// 数据类型。
        /// </summary>
        public string Type;

        /// <summary>
        /// 是否为数组。
        /// </summary>
        public bool IsArray;

        /// <summary>
        /// 数据导出者。
        /// </summary>
        private DataExporter m_deExporter;

        /// <summary>
        /// 初始化数据导出者。
        /// </summary>
        public void InitExporter()
        {
            m_deExporter = DataExporter.GetDataExporter(Type);
        }

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public void AssignFromXmlNode(XmlNode node)
        {
            CName = XmlUtil.GetAttribute(node, "CName");
            VName = XmlUtil.GetAttribute(node, "VName");
            Type = XmlUtil.GetAttribute(node, "Type");
            IsArray = XmlUtil.GetAttributeBool(node, "IsArray");            
        }

        /// <summary>
        /// 导出数据。
        /// </summary>
        /// <param name="data">数据字符串。</param>
        /// <param name="stream">要写入到的数据流。</param>
        public void Export(string data, Stream stream)
        {
            if (IsArray)
            {
                //数组的要先写入数量，在拆分逐个导出
                List<string> fdata = DataUtil.Split(data, "|", true);
                DataUtil.WriteInt32(stream, fdata.Count);
                foreach (string fd in fdata)
                {
                    m_deExporter.Exprot(fd, stream);
                }
            }
            else
            {
                m_deExporter.Exprot(data, stream);
            }
        }
    }

    /// <summary>
    /// 类定义信息。
    /// </summary>
    public class ClassInfo
    {
        /// <summary>
        /// 类名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 类描述。
        /// </summary>
        public string Note;

        /// <summary>
        /// 是否简单类型。
        /// </summary>
        public bool IsSimple;

        /// <summary>
        /// 字段列表。
        /// </summary>
        public List<FieldInfo> FieldInfos = new List<FieldInfo>();

        /// <summary>
        /// 获取类型是否含有数组字段。
        /// </summary>
        public bool IsHaveArrayField
        {
            get
            {
                foreach (var finfo in FieldInfos)
                {
                    if (finfo.IsArray)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public void AssignFromXmlNode(XmlNode node)
        {
            Name = XmlUtil.GetAttribute(node, "Name");
            Note = XmlUtil.GetAttribute(node, "Note");
            IsSimple = XmlUtil.GetAttributeBool(node, "IsSimple");
            FieldInfos.Clear();
            foreach (XmlNode tmp in node.ChildNodes)
            {
                FieldInfo info = new FieldInfo();
                info.AssignFromXmlNode(tmp);
                FieldInfos.Add(info);
            }
        }

        /// <summary>
        /// 生成定义。
        /// </summary>
        /// <param name="sb">定义内容保存对象。</param>
        public void BuildDefine(StringBuilder sb)
        {
            //类名
            sb.AppendLine("    /// <summary>");
            sb.AppendLine(string.Format("    /// {0}。", Note));
            sb.AppendLine("    /// </summary>");
            sb.AppendLine(string.Format("    public class {0} : IDataInfo", Name));
            sb.AppendLine("    {");

            //字段
            for (int i = 0; i < FieldInfos.Count; ++i)
            {
                FieldInfo info = FieldInfos[i];
                string type = info.IsArray ? string.Format("List<{0}>", info.Type) : info.Type;
                sb.AppendLine("        /// <summary>");
                sb.AppendLine(string.Format("        /// {0}。", info.CName));
                sb.AppendLine("        /// </summary>");
                sb.AppendLine(string.Format("        public {0} {1} = {2};", type, info.VName, GetDefaultVale(type)));
                sb.AppendLine();
            }

            //加载代码
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据信息接口，统一读取信息。");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        public void ReadData(DataReader dr)");
            sb.AppendLine("        {");
            for (int i=0; i< FieldInfos.Count; ++i)
            {
                FieldInfo info = FieldInfos[i];
                if (info.IsArray)
                {
                    sb.AppendLine(string.Format("            {0}.Clear();", info.VName));
                    sb.AppendLine(string.Format("            int {0}Num = dr.ReadInt32();", info.VName));
                    sb.AppendLine(string.Format("            for (int i=0; i<{0}Num; ++i)", info.VName));
                    sb.AppendLine("            {");
                    BuildArrayReadCode(sb, "                ", info.VName, info.Type);
                    sb.AppendLine("            }");
                }
                else
                {
                    sb.AppendLine("            " + GetReadCode(info.VName, info.Type));
                }
            }
            sb.AppendLine("        }");
            sb.AppendLine("    }");
        }

        /// <summary>
        /// 获取默认值。
        /// </summary>
        /// <param name="type">数据类型。</param>
        /// <returns>默认值。</returns>
        public static string GetDefaultVale(string type)
        {
            string tl = type.ToLower();
            if (tl.CompareTo("bool") == 0)
            {
                return "false";
            }
            else if (tl.CompareTo("int") == 0)
            {
                return "0";
            }
            else if (tl.CompareTo("float") == 0)
            {
                return "0";
            }
            else if (tl.CompareTo("string") == 0)
            {
                return "string.Empty";
            }

            EnumInfo einfo = ConfigArchive.Instance.GetEnumInfo(type);
            if (einfo != null)
            {
                return string.Format("({0})0", type);
            }
            return string.Format("new {0}()", type);
        }

        /// <summary>
        /// 获取数据读取代码。
        /// </summary>
        /// <param name="vname">变量名称。</param>
        /// <param name="type">数据类型。</param>
        /// <returns>读取代码。</returns>
        public static string GetReadCode(string vname, string type)
        {
            string tl = type.ToLower();
            if (tl.CompareTo("bool") == 0)
            {
                return string.Format("{0} = dr.ReadBoolean();", vname);
            }
            else if (tl.CompareTo("int") == 0)
            {
                return string.Format("{0} = dr.ReadInt32();", vname);
            }
            else if (tl.CompareTo("float") == 0)
            {
                return string.Format("{0} = dr.ReadFloat();", vname);
            }
            else if (tl.CompareTo("string") == 0)
            {
                return string.Format("{0} = dr.ReadString();", vname);
            }

            EnumInfo einfo = ConfigArchive.Instance.GetEnumInfo(type);
            if (einfo != null)
            {
                return string.Format("{0} = ({1})dr.ReadInt32();", vname, type);
            }

            ClassInfo cinfo = ConfigArchive.Instance.GetClassInfo(type);
            if (cinfo != null)
            {
                return string.Format("{0}.ReadData(dr);", vname);
            }

            return string.Empty;
        }

        /// <summary>
        /// 生成数组读取代码。
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="retract"></param>
        /// <param name="vname"></param>
        /// <param name="type"></param>
        public static void BuildArrayReadCode(StringBuilder sb, string retract, string vname, string type)
        {
            string tl = type.ToLower();
            if (tl.CompareTo("bool") == 0)
            {
                sb.AppendLine(string.Format("{0}{1}.Add(dr.ReadBoolean());", retract, vname));
            }
            else if (tl.CompareTo("int") == 0)
            {
                sb.AppendLine(string.Format("{0}{1}.Add(dr.ReadInt32());", retract, vname));
            }
            else if (tl.CompareTo("float") == 0)
            {
                sb.AppendLine(string.Format("{0}{1}.Add(dr.ReadFloat());", retract, vname));
            }
            else if (tl.CompareTo("string") == 0)
            {
                sb.AppendLine(string.Format("{0}{1}.Add(dr.ReadString());", retract, vname));
            }
            else
            {
                EnumInfo einfo = ConfigArchive.Instance.GetEnumInfo(type);
                if (einfo != null)
                {
                    sb.AppendLine(string.Format("{0}{1}.Add(({2})dr.ReadInt32());", retract, vname, type));
                    return;
                }

                ClassInfo cinfo = ConfigArchive.Instance.GetClassInfo(type);
                if (cinfo != null)
                {
                    sb.AppendLine(string.Format("{0}{1} temp = new {1}();", retract, type));
                    sb.AppendLine(string.Format("{0}temp.ReadData(dr);", retract));
                    sb.AppendLine(string.Format("{0}{1}.Add(temp);", retract, vname));
                    return;
                }
            }
            
        }
    }

    /// <summary>
    /// 导出信息。
    /// </summary>
    public class ExportInfo
    {
        /// <summary>
        /// 显示名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 分组名称。
        /// </summary>
        public string GroupName;

        /// <summary>
        /// 数据源，Excel文件名称，带后缀名。
        /// </summary>
        public string SourceName;

        /// <summary>
        /// 导出文件名称，带后缀名。
        /// </summary>
        public string ExportName;

        /// <summary>
        /// 类型定义。
        /// </summary>
        public string ClassDefine;

        /// <summary>
        /// 数据表。
        /// </summary>
        public List<string> Sheets = new List<string>();

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public void AssignFromXmlNode(XmlNode node)
        {
            Name = XmlUtil.GetAttribute(node, "Name");
            SourceName = XmlUtil.GetAttribute(node, "SourceName");
            ExportName = XmlUtil.GetAttribute(node, "ExportName");
            ClassDefine = XmlUtil.GetAttribute(node, "ClassDefine");
            Sheets = DataUtil.Split(XmlUtil.GetAttribute(node, "Sheets"));
        }

        /// <summary>
        /// 开始导出。
        /// </summary>
        /// <returns>是否导出成功。</returns>
        public bool StartExport()
        {
            MainForm form = MainForm.CurForm;
            form.Log("开始导出 {0}\\{1}", GroupName, Name);

            Excel.Application eapp = ConfigArchive.Instance.ExcelApp;
            bool ret = true;
            string file = ConfigArchive.Instance.SourceFolder + "\\" + SourceName;
            string exportfile = ConfigArchive.Instance.ExportFolder + "\\" + ExportName;
            FileStream fs = new FileStream(exportfile, FileMode.Create);
            try
            {
                object mv = Missing.Value;
                eapp.Visible = false;
                eapp.UserControl = true;
                Excel.Workbook wb = eapp.Workbooks.Open(file, mv, true, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv);
                int count = wb.Worksheets.Count;
                DataUtil.WriteInt32(fs, Sheets.Count);      //写入数据表数量
                foreach (string sname in Sheets)
                {
                    //找到要导出的表
                    Excel.Worksheet ws = null;
                    for (int i = 1; i <= count; ++i)
                    {
                        //工作表内容
                        Excel.Worksheet temp = wb.Worksheets[i] as Excel.Worksheet;
                        if (temp.Name.CompareTo(sname) == 0)
                        {
                            ws = temp;
                            break;
                        }
                    }

                    if (ws != null)
                    {
                        //要导出的数据二维数组
                        object[,] data = (object[,])ws.UsedRange.Cells.Value2;
                        form.Log("数据表({0})...", sname);
                        if (!Export(data, fs))
                        {
                            form.Log("导出数据表({0})失败", sname);
                            break;
                        }
                    }
                    else
                    {
                        form.Log("未找到数据表({0})", sname);
                        ret = false;
                        break;
                    }
                }
                wb.Close();
                wb = null;
                form.Log(string.Empty);
            }
            catch (Exception e)
            {
                form.Log("{0}\n{1}", e.Message, e.StackTrace);
                ret = false;
            }

            //关闭Excel
            //eapp.quit();
            //eapp = null;                
            fs.Flush();
            fs.Dispose();
            fs = null;
            GC.Collect();
            return ret;
        }

        /// <summary>
        /// 导出数据表。
        /// </summary>
        /// <param name="data">数据表内容。</param>
        /// <param name="stream">要写入到的数据流。</param>
        /// <returns>是否导出成功。</returns>
        private bool Export(object[,] data, Stream stream)
        {
            //初步检查
            int c = data.GetLength(1);
            if (c <= 0)
            {
                MainForm.CurForm.Log("要导出的数据表至少要有1列");
                return false;
            }

            const int DATA_START_ROW = 3;           //数据起始行
            int r = data.GetLength(0);
            if (r < DATA_START_ROW - 1)
            {
                MainForm.CurForm.Log("要导出的数据表至少要有{0}行", DATA_START_ROW - 1);
                return false;
            }

            //读入要导出的列头信息
            string[] fieldname = new string[c];                             //把字段名称数组，用于确定字段索引
            for (int i = 0; i < c; ++i)
            {
                fieldname[i] = data[2, i + 1].ToString().Trim();
            }

            ClassInfo cinfo = ConfigArchive.Instance.GetClassInfo(ClassDefine);
            int[] fieldindex = new int[cinfo.FieldInfos.Count];                       //要导出的字段索引,从1开始             
            for (int i = 0; i < fieldindex.Length; ++i)
            {
                string efname = cinfo.FieldInfos[i].CName;      //要导出的字段名称
                int j = 0;
                for (; j < c; ++j)
                {
                    if (efname.CompareTo(fieldname[j]) == 0)
                    {
                        fieldindex[i] = j + 1;
                        break;
                    }
                }
                if (j >= c)
                {
                    MainForm.CurForm.Log("未找到字段({0})", efname);
                    return false;
                }
            }

            //重新确定有多少行，第一列为空时则结束            
            for (int i = DATA_START_ROW; i <= r; ++i)        
            {
                object obj = data[i, 1];
                if (obj == null || string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    r = i - 1;
                    break;
                }
            }

            //逐行导出数据
            DataUtil.WriteInt32(stream, r - (DATA_START_ROW - 1));     //写入记录条数(排除非数据行数)
            for (int i = DATA_START_ROW; i <= r; ++i)
            {
                for (int j = 0; j < fieldindex.Length; ++j)
                {
                    object obj = data[i, fieldindex[j]];
                    string d = obj == null ? string.Empty : obj.ToString().Trim();
                    cinfo.FieldInfos[j].Export(d, stream);
                }
            }

            return true;
        }
    }

    #endregion
}
