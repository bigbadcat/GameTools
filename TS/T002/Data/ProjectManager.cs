using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using T002.Common;
using XuXiang.ClassLibrary;

namespace T002.Data
{
    /// <summary>
    /// 工程管理者类。
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// 全局数据，通过此来访问工程。
        /// </summary>
        public static ProjectManager Project = null;

        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。创建一个工程管理者类。
        /// </summary>
        public ProjectManager()
        {
        }

        /// <summary>
        /// 创建工程。
        /// </summary>
        /// <param name="strName">工程名称。</param>
        /// <param name="strFolder">要保存到的文件夹</param>
        /// <returns>0、创建成功 1、要保存的目录不存在 2、保存目录中存在同名目录 3、其它错误</returns>
        public Int32 CreateNewProject(String strName, String strFolder)
        {
            //判定保存的目录
            if (!Directory.Exists(strFolder))
            {
                return 1;
            }

            String strProjectFolder = strFolder + "\\" + strName;
            String strProjectFile = String.Format("{0}\\{1}{2}", strProjectFolder, strName, NAME_EXT_PROJECT);

            //判定对应的工程目录
            if (Directory.Exists(strProjectFolder))
            {
                return 2;
            }

            try
            {
                //创建工程目和界面资源管理目录
                Directory.CreateDirectory(strProjectFolder);
                Directory.CreateDirectory(strProjectFolder + BRANCH_NAME_INTERFACEFOLDER);

                //初始化工程文件
                FileStream fsProject = new FileStream(strProjectFile, FileMode.CreateNew);
                StreamWriter swWriter = new StreamWriter(fsProject);
                swWriter.WriteLine("[Project]");
                swWriter.WriteLine("Mark=UserInterfaceProject");
                swWriter.Flush();
                fsProject.Flush();
                swWriter.Close();
                swWriter = null;
                fsProject.Dispose();
                fsProject = null;
            }
            catch
            {
                return 3;
            }

            return 0;
        }

        /// <summary>
        /// 打开工程。
        /// </summary>
        /// <param name="strProjectFile">工程文件的绝对路径，包含后缀名。</param>
        /// <returns>0、打开成功。 1、文件不存在。 2、不是工程文件</returns>
        public Int32 OpenProject(String strProjectFile)
        {
            //文件必须存在
            Int32 iRet = 0;
            if (File.Exists(strProjectFile))
            {
                //简单验证是否为SceneWorkshop工程文件
                StringBuilder sbBuffer = new StringBuilder(256);
                ReadWriteIni.GetPrivateProfileString("Project", "Mark", String.Empty, sbBuffer, 64, strProjectFile);
                if (sbBuffer.ToString().Equals("UserInterfaceProject"))
                {
                    //确定为工程文件
                    this.m_strProjectFile = strProjectFile;
                    ReadWriteIni.GetPrivateProfileString("Project", "Font", String.Empty, sbBuffer, 256, strProjectFile);
                    this.m_strFontFile = sbBuffer.ToString();
                    T002.Platform.Image.FontFile = m_strFontFile.Equals(String.Empty) ? String.Empty : this.ProjectFolder + "\\" + m_strFontFile;

                    //读入配置
                    String cfgf = Path.GetDirectoryName(this.m_strProjectFile) + NAME_PROJECT_CONFIG;
                    CheckConfigFile();
                    ReadWriteIni.GetPrivateProfileString("Config", "Assets", String.Empty, sbBuffer, 256, cfgf);
                    this.m_strAssetsFolder = sbBuffer.ToString();
                    ReadWriteIni.GetPrivateProfileString("Config", "InterfaceBuild", String.Empty, sbBuffer, 256, cfgf);
                    this.m_strInterfaceBuildFolder = sbBuffer.ToString();
                }
                else
                {
                    iRet = 2;
                }
            }
            else
            {
                iRet = 1;
            }

            return iRet;
        }

        /// <summary>
        /// 关闭工程。
        /// </summary>
        /// <returns>0、成功关闭。</returns>
        public Int32 CloseProject()
        {
            Int32 iRet = 0;
            this.m_strProjectFile = String.Empty;
            this.m_strAssetsFolder = String.Empty;
            this.m_strInterfaceBuildFolder = String.Empty;
            return iRet;
        }

        /// <summary>
        /// 重命名工程。
        /// </summary>
        /// <param name="strNewName">工程的新名称。</param>
        /// <returns>0、命名成功。 1、工程已存在</returns>
        public Int32 RenameProject(String strNewName)
        {
            //保存原来的文件名和文件夹
            String strFile = this.m_strProjectFile;
            String strFolder = this.ProjectFolder;

            //判断工程文件夹是否已经存在
            String strNewProjectFoler = Path.GetDirectoryName(strFolder) + "\\" + strNewName;
            if (Directory.Exists(strNewProjectFoler))
            {
                return 1;
            }

            //修改工程文件名和工程文件夹名
            DiskWork.ReNameFile(strFile, strNewName);
            DiskWork.ReNameFolder(strFolder, strNewName);

            //更新工程文件路径
            StringBuilder sbNewProjectFile = new StringBuilder(strNewProjectFoler);
            sbNewProjectFile.Append("\\");
            sbNewProjectFile.Append(strNewName);
            sbNewProjectFile.Append(NAME_EXT_PROJECT);
            this.m_strProjectFile = sbNewProjectFile.ToString();

            return 0;
        }

        /// <summary>
        /// 重命名工程资源文件。
        /// </summary>
        /// <param name="strFileName">资源文件的绝对路径，无后缀。</param>
        /// <param name="strNewName">要修改的名称（无路径，无后缀）。</param>
        /// <param name="iType">资源类型</param>
        public Int32 RenameProjectResource(String strFileName, String strNewName, Int32 iType)
        {
            String strExt = ResourceFileNameExt[iType];
            String strResourceFile = strFileName + strExt;
            Int32 iRet = 0;

            //判断是什么类型的资源，不同类型不同处理
            if (iType == TYPE_RESOURCE_INTERFACE)
            {
                iRet = DiskWork.ReNameFile(strResourceFile, strNewName);
            }

            return iRet;
        }

        /// <summary>
        /// 重命名资源文件夹。
        /// </summary>
        /// <param name="strFolderName">资源文件夹路径。</param>
        /// <param name="strNewName">文件夹新名称。</param>
        /// <param name="iType">资源类型。</param>
        /// <returns>0、成功重命名。</returns>
        public Int32 RenameResourceFolder(String strFolderName, String strNewName, Int32 iType)
        {
            Int32 iRet = DiskWork.ReNameFolder(strFolderName, strNewName);

            return iRet;
        }

        /// <summary>
        /// 移动工程资源文件。
        /// </summary>
        /// <param name="strDestFolder">要移动到的文件夹。</param>
        /// <param name="strCutFile">资源文件的绝对路径，无后缀。</param>
        /// <param name="iType">资源类型。</param>
        /// <returns>0、成功剪切 1、目标文件夹不存在 2、要剪切的文件不存在 3、目标文件夹以存在同名文件</returns>
        public Int32 MoveProjectResource(String strDestFolder, String strCutFile, Int32 iType)
        {
            Int32 iRet = 0;
            String strResourceFile = strCutFile + ResourceFileNameExt[iType];

            //判断是什么类型的资源，不同类型不同处理
            if (iType == TYPE_RESOURCE_INTERFACE)
            {
                iRet = DiskWork.CutFileToFolder(strDestFolder, strResourceFile);
            }

            return iRet;
        }

        /// <summary>
        /// 移动资源文件夹。
        /// </summary>
        /// <param name="strDestFolder">目标文件夹。</param>
        /// <param name="strCutFolder">要剪切的文件夹。</param>
        /// <returns>0、成功剪切 1、目标文件夹不存在 2、要剪切的文件夹不存在 3、目标文件夹中已存在同名文件夹</returns>
        public Int32 MoveResourceFolder(String strDestFolder, String strCutFolder)
        {
            return DiskWork.CutFolderToFolder(strDestFolder, strCutFolder);
        }

        /// <summary>
        /// 删除工程资源。
        /// </summary>
        /// <param name="strFile">资源文件的绝对路径（不包含后缀名）。</param>
        /// <param name="iType">资源类型。</param>
        /// <returns></returns>
        public Int32 DeleteProjectResource(String strFile, Int32 iType)
        {
            Int32 iRet = 0;
            String strResourceFile = strFile + ResourceFileNameExt[iType];

            //判断是什么类型的资源，不同类型不同处理
            if (iType == TYPE_RESOURCE_INTERFACE)
            {
                iRet = DiskWork.DeleteFile(strResourceFile);
            }
            return iRet;
        }

        /// <summary>
        /// 删除工程资源文件夹。
        /// </summary>
        /// <param name="strFolder">文件夹的决定路径。</param>
        /// <returns>0、成功删除 1、文件夹不存在 2、其它错误</returns>
        public Int32 DeleteResourceFoler(String strFolder)
        {
            return DiskWork.DeleteFolder(strFolder);
        }

        /// <summary>
        /// 复制工程资源。
        /// </summary>
        /// <param name="strFile">资源文件路径（无后缀名）。</param>
        /// <param name="iType">资源类型。</param>
        /// <returns>复制成功后的名称。</returns>
        public String CopyProjectResource(String strFile, Int32 iType)
        {
            String strCopyFile = strFile + ResourceFileNameExt[iType];
            String strNewFile = this.GetCopyFileName(strCopyFile);

            //可复制的资源只有界面
            if (iType == TYPE_RESOURCE_INTERFACE)
            {
                if (DiskWork.CopyFile(strNewFile, strCopyFile, true) != 0)
                {
                    strNewFile = String.Empty;
                }
            }

            return strNewFile;
        }

        /// <summary>
        /// 复制资源文件夹。
        /// </summary>
        /// <param name="strFolder">文件夹路径。</param>
        /// <returns>复制成功后的名称。</returns>
        public String CopyResourceFolder(String strFolder)
        {
            String strNewFolder = this.GetCopyFolderName(strFolder);
            if (DiskWork.CopyFolder(strNewFolder, strFolder) != 0)
            {
                strNewFolder = String.Empty;
            }

            return strNewFolder;
        }

        /// <summary>
        /// 创建工程资源文件夹。
        /// </summary>
        /// <param name="strPath">创建文件夹的位置。</param>
        /// <param name="iType">资源类型。</param>
        /// <returns>返回创建的文件夹名称。</returns>
        public String CreateResourceFolder(String strPath, Int32 iType)
        {
            //获取合法名字
            Int32 iNum = 1;
            String strFullPath = strPath;   //哨兵
            for (; Directory.Exists(strFullPath); ++iNum)
            {
                strFullPath = String.Format(@"{0}\新建文件夹({1})", strPath, iNum);
            }

            //创建文件夹
            Directory.CreateDirectory(strFullPath);
            return Path.GetFileName(strFullPath);
        }

        /// <summary>
        /// 打开资源文件，创建文件对象。
        /// </summary>
        /// <param name="strFileName">资源文件的绝对路径。</param>
        /// <param name="iType">资源类型</param>
        /// <returns>资源文件对象。</returns>
        public ResourceFile OpenResourceFile(String strFileName, Int32 iType)
        {
            ResourceFile rfEditFile = null;

            if (iType == TYPE_RESOURCE_INTERFACE)
            {
                rfEditFile = InterfaceFile.LoadFromFile(strFileName);
            }

            return rfEditFile;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取工程的名称。
        /// </summary>
        public String ProjectName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.m_strProjectFile);
            }
        }

        /// <summary>
        /// 获取工程的文件路径。
        /// </summary>
        public String ProjectFile
        {
            get
            {
                return m_strProjectFile;
            }
        }

        /// <summary>
        /// 获取工程文件夹。
        /// </summary>
        public String ProjectFolder
        {
            get
            {
                return Path.GetDirectoryName(this.m_strProjectFile);
            }
        }

        /// <summary>
        /// 获取界面的根文件夹路径。
        /// </summary>
        public String InterfaceRootFolder
        {
            get
            {
                return this.ProjectFolder + BRANCH_NAME_INTERFACEFOLDER;
            }
        }

        /// <summary>
        /// 获取工程是否打开状态。
        /// </summary>
        public Boolean IsOpen
        {
            get
            {
                return this.m_strProjectFile != String.Empty;
            }
        }

        /// <summary>
        /// 获取或设置工程使用的资源目录。
        /// </summary>
        public String AssetsFolder
        {
            get
            {
                return this.m_strAssetsFolder;
            }
            set
            {
                String folder = value.Equals(String.Empty) || value.EndsWith("\\") ? value : value + "\\";
                if (!this.m_strAssetsFolder.Equals(folder))
                {
                    String cfgf = Path.GetDirectoryName(this.m_strProjectFile) + NAME_PROJECT_CONFIG;
                    ReadWriteIni.WritePrivateProfileString("Config", "Assets", folder, cfgf);
                    this.m_strAssetsFolder = folder;
                }
            }
        }

        /// <summary>
        /// 获取或设置界面生成的目录。
        /// </summary>
        public String InterfaceBuildFolder
        {
            get
            {
                return this.m_strInterfaceBuildFolder;
            }
            set
            {
                String folder = value.Equals(String.Empty) || value.EndsWith("\\") ? value : value + "\\";
                if (!this.m_strInterfaceBuildFolder.Equals(folder))
                {
                    String cfgf = Path.GetDirectoryName(this.m_strProjectFile) + NAME_PROJECT_CONFIG;
                    ReadWriteIni.WritePrivateProfileString("Config", "InterfaceBuild", folder, cfgf);
                    this.m_strInterfaceBuildFolder = folder;
                }
            }
        }

        /// <summary>
        /// 获取或设置界面文本字体文件。
        /// </summary>
        public String FontFile
        {
            get
            {
                return m_strFontFile;
            }
            set
            {
                //相等判断
                if (m_strFontFile.Equals(value))
                {
                    return;
                }

                //保存                
                ReadWriteIni.WritePrivateProfileString("Project", "Font", value, m_strProjectFile);
                this.m_strFontFile = value;

                //设置新的
                T002.Platform.Image.FontFile = m_strFontFile.Equals(String.Empty) ? String.Empty : this.ProjectFolder + "\\" + m_strFontFile;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 获取复制文件的新名字。
        /// </summary>
        /// <param name="strFileName">要复制的文件路径，包含后缀。</param>
        /// <returns>文件的新名字。</returns>
        protected String GetCopyFileName(String strFileName)
        {
            //所在文件夹路径 文件名 后缀名都拆分出来
            String strFolder = Path.GetDirectoryName(strFileName);
            String strFile = Path.GetFileNameWithoutExtension(strFileName);
            String strExt = Path.GetExtension(strFileName);
            String strNewName = strFileName;        //算个哨兵吧，for的第一次判断保证成功

            //判断改文件是否为附件
            Int32 iNum = 1;
            Int32 iMark = strFile.LastIndexOf("_附件(");
            Int32 iLast = strFile.LastIndexOf(")");
            if (iMark != -1 && iLast != -1)
            {
                Int32 iN;
                String strNew = strFile.Substring(0, iMark);    //文件名称
                String strNum = strFile.Substring(iMark + 4, iLast - iMark - 4);        //附件序号

                //尝试转换附件序号
                if (Int32.TryParse(strNum, out iN))
                {
                    //成功则更新信息
                    iNum = iN;
                    strFile = strNew;
                }
            }

            //循环测试，直到生成不重复的名字
            for (; File.Exists(strNewName); ++iNum)
            {
                strNewName = String.Format("{0}\\{1}_附件({2}){3}", strFolder, strFile, iNum, strExt);
            }

            return strNewName;
        }

        /// <summary>
        /// 获取复制文件夹的新名字。
        /// </summary>
        /// <param name="strFolderName">要复制的文件夹名称。</param>
        /// <returns></returns>
        protected String GetCopyFolderName(String strFolderName)
        {
            //拆分当前文件夹和其所在的目录
            String strParentFolder = Path.GetDirectoryName(strFolderName);
            String strFolder = Path.GetFileName(strFolderName);
            String strNewName = strFolderName;        //算个哨兵吧，for的第一次判断保证成功

            //判断改文件是否为附件
            Int32 iNum = 1;
            Int32 iMark = strFolder.LastIndexOf("_附件(");
            Int32 iLast = strFolder.LastIndexOf(")");
            if (iMark != -1 && iLast != -1)
            {
                Int32 iN;
                String strNew = strFolder.Substring(0, iMark);    //文件名称
                String strNum = strFolder.Substring(iMark + 4, iLast - iMark - 4);        //附件序号

                //尝试转换附件序号
                if (Int32.TryParse(strNum, out iN))
                {
                    //成功则更新信息
                    iNum = iN;
                    strFolder = strNew;
                }
            }

            //循环测试，直到生成不重复的名字
            for (; Directory.Exists(strNewName); ++iNum)
            {
                strNewName = String.Format("{0}\\{1}_附件({2})", strParentFolder, strFolder, iNum);
            }

            return strNewName;
        }

        /// <summary>
        /// 检查配置文件，若不存在则生成默认配置文件。
        /// </summary>
        protected void CheckConfigFile()
        {
            String cfgf = Path.GetDirectoryName(this.m_strProjectFile) + NAME_PROJECT_CONFIG;
            if (!File.Exists(cfgf))
            {
                //初始配置文件
                FileStream fsConfig = new FileStream(cfgf, FileMode.CreateNew);
                StreamWriter swWriter = new StreamWriter(fsConfig);
                swWriter.WriteLine("[Config]");
                swWriter.WriteLine("Assets=");
                swWriter.WriteLine("InterfaceBuild=");
                swWriter.Flush();
                fsConfig.Flush();
                swWriter.Close();
                swWriter = null;
                fsConfig.Dispose();
                fsConfig = null;
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 工程文件路径。
        /// </summary>
        private String m_strProjectFile = String.Empty;

        /// <summary>
        /// 工程使用的资源目录。
        /// </summary>
        private String m_strAssetsFolder = String.Empty;

        /// <summary>
        /// 界面生成的目录。
        /// </summary>
        private String m_strInterfaceBuildFolder = String.Empty;

        /// <summary>
        /// 界面文本字体文件。
        /// </summary>
        private String m_strFontFile = String.Empty;

        #endregion

        #region 静态数据=====================================================================================

        /// <summary>
        /// 界面分支的名称。
        /// </summary>
        public const String BRANCH_NAME_INTERFACE = "界面";

        /// <summary>
        /// 界面支对应的根文件夹。
        /// </summary>
        public const String BRANCH_NAME_INTERFACEFOLDER = @"\Interface";


        /// <summary>
        /// 通过资源类型快速取得分支文件夹。
        /// </summary>
        public static String[] RootFolderType = new String[]
        {
            BRANCH_NAME_INTERFACEFOLDER, 
        };

        /// <summary>
        /// UI编辑文件后缀名。
        /// </summary>
        public const String NAME_EXT_INTERFACE_EDIT = ".xml";

        /// <summary>
        /// UI生成文件后缀名。
        /// </summary>
        public const String NAME_EXT_INTERFACE_BUILD = ".ui";

        /// <summary>
        /// 工程文件后缀名。
        /// </summary>
        public const String NAME_EXT_PROJECT = ".uip";

        /// <summary>
        /// 工程配置文件后缀名。
        /// </summary>
        public const String NAME_PROJECT_CONFIG = @"\Config.ini";

        /// <summary>
        /// 通过资源类型快速获取其文件后缀名。
        /// </summary>
        public static String[] ResourceFileNameExt = new String[] { NAME_EXT_INTERFACE_EDIT };

        /// <summary>
        /// 未知资源类型。
        /// </summary>
        public const Int32 TYPE_RESOURCE_UNKNOW = -1;

        /// <summary>
        /// 贴图资源类型。
        /// </summary>
        public const Int32 TYPE_RESOURCE_INTERFACE = 0;

        #endregion
    }
}