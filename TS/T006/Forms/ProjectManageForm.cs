using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using T006.Data;
using System.IO;

namespace T006.Forms
{
    /// <summary>
    /// 工程资源管理窗口类。
    /// </summary>
    public partial class ProjectManageForm : DockContent
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ProjectManageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 载入工程资源结构。
        /// </summary>
        /// <param name="strProjectFile">工程文件完整路径。</param>
        public void LoadProject(String strProjectFile)
        {
            //获取工程名称和工程文件夹，并清空TreeView控件
            String strProjectName = ProjectManager.Project.ProjectName;
            String strProjectFolder = ProjectManager.Project.ProjectFolder;
            this.tvProjectResource.Nodes.Clear();

            //创建工程根节点
            TreeNode tnProject = this.tvProjectResource.Nodes.Add(strProjectName);
            tnProject.SelectedImageIndex = tnProject.ImageIndex = IMG_INDEX_PRM_PROJECT;
            tnProject.Tag = MANAGE_TAG_PRM_PROJECTROOT;
            tnProject.ContextMenuStrip = this.cmsProject;

            //创建界面分支
            TreeNode tnBranch = tnProject.Nodes.Add(ProjectManager.BRANCH_NAME_PARTICLE);
            tnBranch.SelectedImageIndex = tnBranch.ImageIndex = IMG_INDEX_PRM_PARTICLE;
            tnBranch.Tag = MANAGE_TAG_PRM_PARTICLEROOT;
            tnBranch.ContextMenuStrip = this.cmsBranch;

            //加载四种资源
            this.LoadAppointedResource(strProjectFolder, ProjectManager.TYPE_RESOURCE_PARTICLE);

            //将工程的根节点展开
            tnProject.Expand();
        }

        /// <summary>
        /// 进行重命名操作。
        /// </summary>
        public void DoRename()
        {
            if (this.tvProjectResource.SelectedNode != null)
            {
                this.tvProjectResource.SelectedNode.BeginEdit();
            }
        }

        /// <summary>
        /// 进行删除操作。
        /// </summary>
        public void DoDelete()
        {
            //获取选选中的节点
            TreeNode tnSeletNode = this.GetCanEditNode();
            if (tnSeletNode == null)
            {
                return;
            }

            //确认是否删除
            String strMessage = String.Format("\"{0}\"将被永久删除。", tnSeletNode.Text);
            if (MessageBox.Show(strMessage, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            //获取路径，并删除节点
            String strPath;
            Int32 iType = this.GetFullPathFromTreeNode(tnSeletNode, out strPath);
            String strTag = tnSeletNode.Tag as String;
            tnSeletNode.Remove();

            //删除资源和删除文件夹分开
            if (strTag.Contains(MANAGE_TAG_PRM_FILE))
            {
                MainForm.AppMainForm.CloseFileWindow(strPath + ProjectManager.ResourceFileNameExt[iType], true);
                if (ProjectManager.Project.DeleteProjectResource(strPath, iType) != 0)
                {
                    this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
                }
            }
            else if (strTag.Contains(MANAGE_TAG_PRM_FOLDER))
            {
                MainForm.AppMainForm.CloseFileWindow(strPath, false);
                if (ProjectManager.Project.DeleteResourceFoler(strPath) != 0)
                {
                    this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
                }
            }
        }

        /// <summary>
        /// 进行复制操作。
        /// </summary>
        public void DoCopy()
        {
            //获取选选中的节点
            TreeNode tnSeletNode = this.GetCanEditNode();
            if (tnSeletNode == null)
            {
                return;
            }

            //复制资源
            String strPath;
            String strTag = tnSeletNode.Tag as String;
            Int32 iType = this.GetFullPathFromTreeNode(tnSeletNode, out strPath);
            TreeNode tnParent = tnSeletNode.Parent;     //用于节点的插入
            if (strTag.Contains(MANAGE_TAG_PRM_FILE))
            {
                String strNewName = ProjectManager.Project.CopyProjectResource(strPath, iType);
                if (strNewName != String.Empty)
                {
                    //添加新节点
                    TreeNode tnNewNode = new TreeNode(Path.GetFileNameWithoutExtension(strNewName));
                    tnNewNode.ImageIndex = tnNewNode.SelectedImageIndex = ResourceImageIndex[iType];
                    tnNewNode.Tag = FileTagType[iType];
                    tnNewNode.ContextMenuStrip = this.cmsResource;
                    tnParent.Nodes.Insert(0, tnNewNode);
                }
                else
                {
                    this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
                }
            }
            else if (strTag.Contains(MANAGE_TAG_PRM_FOLDER))
            {
                String strNewName = ProjectManager.Project.CopyResourceFolder(strPath);
                if (strNewName != String.Empty)
                {
                    //添加新节点
                    TreeNode tnNewNode = new TreeNode(Path.GetFileNameWithoutExtension(strNewName));
                    tnNewNode.ImageIndex = tnNewNode.SelectedImageIndex = IMG_INDEX_PRM_FOLDERCLOSE;
                    tnNewNode.Tag = FolderTagType[iType];
                    tnNewNode.ContextMenuStrip = this.cmsResource;

                    //添加节点，加载节点的结构
                    tnParent.Nodes.Insert(tnParent.Nodes.Count, tnNewNode);
                    this.LoadResourceStruct(tnNewNode, strNewName, iType);
                }
                else
                {
                    this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
                }
            }
        }

        /// <summary>
        /// 进行新建文件操作。
        /// </summary>
        public void DoNewFile()
        {
            //获取选选中的节点
            TreeNode tnSeletNode = this.tvProjectResource.SelectedNode;
            String strTag = tnSeletNode.Tag as String;

            //创建文件
            String strPath;
            Int32 iType = this.GetFullPathFromTreeNode(tnSeletNode, out strPath);
            String strName = MainForm.AppMainForm.NewResourceFile(strPath, iType);

            //若创建成功则添加节点到树视图里
            if (strName != String.Empty)
            {
                TreeNode tnNewFile = new TreeNode(strName);
                tnNewFile.ImageIndex = tnNewFile.SelectedImageIndex = ResourceImageIndex[iType];
                tnNewFile.Tag = FileTagType[iType];
                tnNewFile.ContextMenuStrip = this.cmsResource;
                tnSeletNode.Nodes.Insert(0, tnNewFile);
                tnSeletNode.Expand();
            }
        }

        /// <summary>
        /// 进行创建新文件夹操作。
        /// </summary>
        public void DoNewFolder()
        {
            //获取选选中的节点
            TreeNode tnSeletNode = this.m_tnContextMenuNode;
            String strTag = String.Empty;
            if (tnSeletNode != null)
            {
                //保证选中的工程根节点
                strTag = tnSeletNode.Tag as String;
                if (strTag == null || strTag.Equals(MANAGE_TAG_PRM_PROJECTROOT))
                {
                    return;
                }
            }

            //创建文件夹
            String strPath;
            Int32 iType = this.GetFullPathFromTreeNode(tnSeletNode, out strPath);
            String strName = ProjectManager.Project.CreateResourceFolder(strPath, iType);

            //更新TreeView
            TreeNode tnNewNode = new TreeNode(strName);
            tnNewNode.ImageIndex = tnNewNode.SelectedImageIndex = IMG_INDEX_PRM_FOLDERCLOSE;
            tnNewNode.Tag = FolderTagType[iType];
            tnNewNode.ContextMenuStrip = this.cmsResource;
            tnSeletNode.Nodes.Insert(tnSeletNode.Nodes.Count, tnNewNode);
            this.tvProjectResource.SelectedNode = tnNewNode;
            tnNewNode.BeginEdit();     //可编辑文件夹名称
        }

        /// <summary>
        /// 进行打开文件操作。
        /// </summary>
        public void DoOpen()
        {
            TreeNode tnSelect = this.tvProjectResource.SelectedNode;
            if (tnSelect == null)
            {
                return;
            }

            //获取文件的完整路径
            String strPath;
            Int32 iType = this.GetFullPathFromTreeNode(tnSelect, out strPath);

            //通过主窗体打开文件
            MainForm.AppMainForm.OpenResourceFile(strPath + ProjectManager.ResourceFileNameExt[iType], iType);
        }

        /// <summary>
        /// 进行项目属性操作。
        /// </summary>
        public void DoProjectProperty()
        {
            MainForm.AppMainForm.DoProjectProperty();
        }

        /// <summary>
        /// 添加文件节点到分支根部。
        /// </summary>
        /// <param name="strName">文件名称。</param>
        /// <param name="iType">资源类型。</param>
        public void AddFileNodeToRoot(String strName, Int32 iType)
        {
            if (strName == String.Empty)
            {
                return;
            }

            TreeNode tnFile = new TreeNode(strName);
            TreeNode tnRoot = this.GetBranchRootNode(iType);

            tnFile.ImageIndex = tnFile.SelectedImageIndex = ResourceImageIndex[iType];
            tnFile.Tag = FileTagType[iType];
            tnFile.ContextMenuStrip = this.cmsResource;
            tnRoot.Nodes.Insert(0, tnFile);
            tnRoot.Expand();
        }

        /// <summary>
        /// 关闭工程。
        /// </summary>
        public void CloseProject()
        {
            this.tvProjectResource.Nodes.Clear();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 界面分支根节点
        /// </summary>
        public TreeNode ParticleRootNode
        {
            get
            {
                return this.tvProjectResource.Nodes[0].Nodes[0];
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 重新加载指定类型的资源。
        /// </summary>
        /// <param name="strProjectFolder">工程目录。</param>
        /// <param name="iResourceType">指定类型的资源</param>
        /// <returns>返回是否加载成功。</returns>
        protected void LoadAppointedResource(String strProjectFolder, Int32 iResourceType)
        {
            //保证指定正确类型的资源
            if (iResourceType >= 0 && iResourceType <= 0)
            {
                //找到资源的根节点，清空。然后递归加载资源结构
                TreeNode tnResourceRoot = this.GetBranchRootNode(iResourceType);
                String strTileFolder = strProjectFolder + ProjectManager.RootFolderType[iResourceType];

                tnResourceRoot.Nodes.Clear();
                this.LoadResourceStruct(tnResourceRoot, strTileFolder, iResourceType);
                tnResourceRoot.Expand();
            }
        }

        /// <summary>
        /// 递归加载资源结构。
        /// </summary>
        /// <param name="tnNode">要加载的节点。</param>
        /// <param name="strFolder">节点对应的目录。</param>
        /// <param name="iResourceType">资源类型。</param>
        protected void LoadResourceStruct(TreeNode tnNode, String strFolder, Int32 iResourceType)
        {
            //获取该目录下的文件和文件夹信息
            DirectoryInfo diFileFolder = new DirectoryInfo(strFolder);

            //先加载文件信息
            FileInfo[] fiaFiles = diFileFolder.GetFiles();
            foreach (FileInfo fiTemp in fiaFiles)
            {
                //如果是相应合法资源则载入
                if (this.IsAppointedResource(fiTemp, iResourceType))
                {
                    String strFile = Path.GetFileNameWithoutExtension(fiTemp.Name);
                    TreeNode tnTemp = tnNode.Nodes.Add(strFile);
                    tnTemp.ImageIndex = tnTemp.SelectedImageIndex = ResourceImageIndex[iResourceType];
                    tnTemp.Tag = FileTagType[iResourceType];
                    tnTemp.ContextMenuStrip = this.cmsResource;
                }
            }

            //加载文件夹信息
            DirectoryInfo[] diaFolders = diFileFolder.GetDirectories();
            foreach (DirectoryInfo diTemp in diaFolders)
            {
                String strFolderName = diTemp.Name;
                TreeNode tnTemp = tnNode.Nodes.Add(strFolderName);
                tnTemp.ImageIndex = tnTemp.SelectedImageIndex = IMG_INDEX_PRM_FOLDERCLOSE;
                tnTemp.Tag = FolderTagType[iResourceType];
                tnTemp.ContextMenuStrip = this.cmsResource;

                //递归加载该文件夹
                this.LoadResourceStruct(tnTemp, diTemp.FullName, iResourceType);
            }
        }

        /// <summary>
        /// 判断该文件是否为指定类型的资源。
        /// </summary>
        /// <param name="fiFile">要判断的文件。</param>
        /// <param name="iType">资源类型。</param>
        protected Boolean IsAppointedResource(FileInfo fiFile, Int32 iType)
        {
            Boolean bIsAppointed = false;
            String strExtName = fiFile.Extension.ToLower();

            switch (iType)
            {
                case ProjectManager.TYPE_RESOURCE_PARTICLE:
                    {
                        bIsAppointed = strExtName.Equals(ProjectManager.NAME_EXT_PARTICLE_EDIT);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return bIsAppointed;
        }

        /// <summary>
        /// 获取分支的跟节点。
        /// </summary>
        /// <param name="iType">分支类型。</param>
        /// <returns>返回根节点。失败则返回null</returns>
        protected TreeNode GetBranchRootNode(Int32 iType)
        {
            //确定指定资源的Tag
            String strResourceTag = RootTagType[iType];
            TreeNode tnRoot = null;

            //找到资源的根节点
            foreach (TreeNode tnTemp in this.tvProjectResource.Nodes[0].Nodes)
            {
                if ((tnTemp.Tag as String) == strResourceTag)
                {
                    tnRoot = tnTemp;
                    break;
                }
            }

            return tnRoot;
        }

        /// <summary>
        /// 通过TreeNode获取绝对路径，无文件后缀名。
        /// </summary>
        /// <param name="tnNode">TreeNode节点。</param>
        /// <param name="strFilePath">输出参数。返回完整路径。</param>
        /// <returns>返回节点类型。</returns>
        protected Int32 GetFullPathFromTreeNode(TreeNode tnNode, out String strFilePath)
        {
            //先获取工程的根目录
            StringBuilder sbFilePath = new StringBuilder(ProjectManager.Project.ProjectFolder);
            String strTag = tnNode.Tag as String;
            Int32 iNodeType = this.GetBranchTypeFromTag(strTag);
            sbFilePath.Append(ProjectManager.RootFolderType[iNodeType]);

            //如果是文件或文件夹并且不是分支的根节点
            if (iNodeType != ProjectManager.TYPE_RESOURCE_UNKNOW && !strTag.Contains(MANAGE_TAG_PRM_ROOT))
            {
                //把结尾剩下的添加进去
                String strNodePath = tnNode.FullPath;
                Int32 iFirstSP = strNodePath.IndexOf("\\");
                sbFilePath.Append(strNodePath.Substring(strNodePath.IndexOf("\\", iFirstSP + 1)));
            }

            strFilePath = sbFilePath.ToString();
            return iNodeType;
        }

        /// <summary>
        /// 通过节点Tag来确定属于哪个分支。
        /// </summary>
        /// <param name="strTag">节点的Tag。</param>
        /// <returns>返回分支类型。</returns>
        protected Int32 GetBranchTypeFromTag(String strTag)
        {
            Int32 iNodeType = ProjectManager.TYPE_RESOURCE_UNKNOW;
            switch (strTag)
            {
                case MANAGE_TAG_PRM_PARTICLEFILE:
                case MANAGE_TAG_PRM_PARTICLEFOLDER:
                case MANAGE_TAG_PRM_PARTICLEROOT:
                    {
                        iNodeType = ProjectManager.TYPE_RESOURCE_PARTICLE;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return iNodeType;
        }

        /// <summary>
        /// 获取可编辑节点。
        /// </summary>
        /// <returns>返回节点，NULL则获取失败</returns>
        protected TreeNode GetCanEditNode()
        {
            //获取选选中的节点
            TreeNode tnSeletNode = this.tvProjectResource.SelectedNode;
            if (tnSeletNode != null)
            {
                //保证选中的不是根节点
                String strTag = tnSeletNode.Tag as String;
                if (strTag == null || strTag.Contains(MANAGE_TAG_PRM_ROOT))
                {
                    tnSeletNode = null;
                }
            }

            return tnSeletNode;
        }

        /// <summary>
        /// 节点改名称后。
        /// </summary>
        /// <param name="tnNode">改名称的节点。</param>
        /// <param name="strNewName">修改的新名称。</param>
        /// <returns>标志是否可以改。</returns>
        protected Boolean AfterNodeRename(TreeNode tnNode, String strNewName)
        {
            Boolean bCancel = false;
            String strTag = tnNode.Tag as String;
            if (strTag.Equals(MANAGE_TAG_PRM_PROJECTROOT))      //工程名被修改
            {
                bCancel = this.RenameProject(tnNode, strNewName);
            }
            else if (strTag.Contains(MANAGE_TAG_PRM_FILE))      //文件名被修改
            {
                bCancel = this.RenameResourceFile(tnNode, strNewName);
            }
            else if (strTag.Contains(MANAGE_TAG_PRM_FOLDER))    //文件夹名称被修改
            {
                bCancel = this.RenameResourceFolder(tnNode, strNewName);
            }

            return bCancel;
        }

        /// <summary>
        /// 重命名工程。
        /// </summary>
        /// <param name="tnNode">工程更节点。</param>
        /// <param name="strNewName">修改的新名称。</param>
        /// <returns>标志是否可以改。</returns>
        protected Boolean RenameProject(TreeNode tnNode, String strNewName)
        {
            Boolean bCancel = false;
            Int32 iRet = ProjectManager.Project.RenameProject(strNewName);
            if (iRet == 0)
            {
                MainForm.AppMainForm.Text = strNewName + @"-" + Application.ProductName;        //更新主窗口标题
            }
            else if (iRet == 1)     //工程重名
            {
                bCancel = true;
                String strMessage = String.Format("工程\"{0}\"已存在。", strNewName);
                if (MessageBox.Show(strMessage, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    tnNode.BeginEdit();     //继续重新编辑
                }
            }

            return bCancel;
        }

        /// <summary>
        /// 重命名资源文件。
        /// </summary>
        /// <param name="tnNode">工程文件节点。</param>
        /// <param name="strNewName">修改的新名称。</param>
        /// <returns>标志是否可以改。</returns>
        protected Boolean RenameResourceFile(TreeNode tnNode, String strNewName)
        {
            Boolean bCancel = false;
            String strFileName;
            Int32 iType = this.GetFullPathFromTreeNode(tnNode, out strFileName);

            if (ProjectManager.Project.RenameProjectResource(strFileName, strNewName, iType) != 0)
            {
                bCancel = true;
                this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
            }
            else
            {
                //通知主窗体文件名发生修改
                StringBuilder sbNewFileName = new StringBuilder(strFileName.Substring(0, strFileName.LastIndexOf('\\') + 1));
                sbNewFileName.Append(strNewName);
                sbNewFileName.Append(ProjectManager.ResourceFileNameExt[iType]);
                MainForm.AppMainForm.RenameResourceFile(strFileName + ProjectManager.ResourceFileNameExt[iType], sbNewFileName.ToString());
            }

            return bCancel;
        }

        /// <summary>
        /// 重命名资源文件夹。
        /// </summary>
        /// <param name="tnNode">工程文件夹节点。</param>
        /// <param name="strNewName">修改的新名称。</param>
        /// <returns>标志是否可以改。</returns>
        protected Boolean RenameResourceFolder(TreeNode tnNode, String strNewName)
        {
            Boolean bCancel = false;
            String strFolderName;
            Int32 iType = this.GetFullPathFromTreeNode(tnNode, out strFolderName);

            if (ProjectManager.Project.RenameResourceFolder(strFolderName, strNewName, iType) != 0)
            {
                bCancel = true;
                this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iType);
            }
            else
            {
                //通知主窗体文件夹名发生修改
                StringBuilder sbNewFolderName = new StringBuilder(strFolderName.Substring(0, strFolderName.LastIndexOf('\\') + 1));
                sbNewFolderName.Append(strNewName);
                MainForm.AppMainForm.RenameResourceFolder(strFolderName, sbNewFolderName.ToString());
            }

            return bCancel;
        }

        #endregion

        #region 数据变量=====================================================================================

        /// 右键菜单选择的项。
        /// </summary>
        protected TreeNode m_tnContextMenuNode = null;

        #endregion

        #region 静态数据=====================================================================================

        #region 节点Tag======================================================================================

        /// <summary>
        /// 工程根节点的Tag。
        /// </summary>
        public const String MANAGE_TAG_PRM_PROJECTROOT = "ProjectRoot";

        /// <summary>
        /// 界面分支根节点的Tag。
        /// </summary>
        public const String MANAGE_TAG_PRM_PARTICLEROOT = "ParticleRoot";

        /// <summary>
        /// 界面文件Tag。
        /// </summary>
        public const String MANAGE_TAG_PRM_PARTICLEFILE = "ParticleFile";

        /// <summary>
        /// 界面文件夹Tag。
        /// </summary>
        public const String MANAGE_TAG_PRM_PARTICLEFOLDER = "ParticleFolder";

        /// <summary>
        /// 根节点都Tag包含的字符串。
        /// </summary>
        public const String MANAGE_TAG_PRM_ROOT = "Root";

        /// <summary>
        /// 文件节点Tag都包含的字符串。
        /// </summary>
        public const String MANAGE_TAG_PRM_FILE = "File";

        /// <summary>
        /// 文件夹节点Tag都包含的字符串。
        /// </summary>
        public const String MANAGE_TAG_PRM_FOLDER = "Folder";

        /// <summary>
        /// 资源类型对应的分支根节点Tag。
        /// </summary>
        public static String[] RootTagType = new String[]
        {
            MANAGE_TAG_PRM_PARTICLEROOT
        };

        /// <summary>
        /// 资源类型对应的文件节点Tag。
        /// </summary>
        public static String[] FileTagType = new String[]
        {
            MANAGE_TAG_PRM_PARTICLEFILE
        };

        /// <summary>
        /// 资源类型对应的文件夹节点Tag。
        /// </summary>
        public static String[] FolderTagType = new String[]
        {
            MANAGE_TAG_PRM_PARTICLEFOLDER
        };

        #endregion

        #region 图像索引=====================================================================================

        /// <summary>
        /// 工程根节点图像索引。
        /// </summary>
        public const Int32 IMG_INDEX_PRM_PROJECT = 0;

        /// <summary>
        /// 界面分支根节点图像索引。
        /// </summary>
        public const Int32 IMG_INDEX_PRM_PARTICLE = 1;

        /// <summary>
        /// 界面文件节点图像索引。
        /// </summary>
        public const Int32 IMG_INDEX_PRM_PARTICLEFILE = 2;

        /// <summary>
        /// 文件夹节点关闭时图像索引。
        /// </summary>
        public const Int32 IMG_INDEX_PRM_FOLDERCLOSE = 3;

        /// <summary>
        /// 文件夹节点展开时图像索引。
        /// </summary>
        public const Int32 IMG_INDEX_PRM_FOLDEROPEN = 4;

        /// <summary>
        /// 资源类型对应的图像索引
        /// </summary>
        public static Int32[] ResourceImageIndex = new Int32[] 
        { 
            IMG_INDEX_PRM_PARTICLEFILE
        };

        #endregion

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 工程资源树节点折叠之后。
        /// </summary>
        private void tvProjectResource_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode tnCollapse = e.Node;
            String strTag = tnCollapse.Tag as String;
            if (strTag != null && strTag.Contains(MANAGE_TAG_PRM_FOLDER))
            {
                tnCollapse.ImageIndex = tnCollapse.SelectedImageIndex = IMG_INDEX_PRM_FOLDERCLOSE;
            }
        }

        /// <summary>
        /// 工程资源树节点展开之后。
        /// </summary>
        private void tvProjectResource_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode tnExpand = e.Node;
            String strTag = tnExpand.Tag as String;
            if (strTag != null && strTag.Contains(MANAGE_TAG_PRM_FOLDER))
            {
                tnExpand.ImageIndex = tnExpand.SelectedImageIndex = IMG_INDEX_PRM_FOLDEROPEN;
            }
        }

        /// <summary>
        /// 工程资源树结构视图鼠标按下。
        /// </summary>
        private void tvProjectResource_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tvEvent = sender as TreeView;
            TreeNode tnEvent = tvEvent.GetNodeAt(e.Location);

            if (e.Button == MouseButtons.Left)
            {
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (tvEvent.Nodes.Count != 0)
                {
                    //配合右键快捷菜单使用
                    this.m_tnContextMenuNode = tnEvent;
                    tvEvent.SelectedNode = tnEvent;
                }
            }
        }

        /// <summary>
        /// 准备拖动工程资源树结构视图节点。
        /// </summary>
        private void tvProjectResource_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode tnDrag = e.Item as TreeNode;
                if (tnDrag != null)
                {
                    //文件或文件夹才可以拖动
                    String strTag = tnDrag.Tag as String;
                    if (strTag != null && strTag.Contains(MANAGE_TAG_PRM_FILE) || strTag.Contains(MANAGE_TAG_PRM_FOLDER))
                    {
                        this.DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
        }

        /// <summary>
        /// 拖动操作进入工程资源树结构视图区域。
        /// </summary>
        private void tvProjectResource_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent("System.Windows.Forms.TreeNode") ? DragDropEffects.Move : DragDropEffects.None;
        }

        /// <summary>
        /// 拖动操作经过工程资源树结构视图区域。
        /// </summary>
        private void tvProjectResource_DragOver(object sender, DragEventArgs e)
        {
            //将经过的节点设为选中，视觉效果好
            TreeView tvEvent = sender as TreeView;
            TreeNode tnOver = tvEvent.GetNodeAt(tvEvent.PointToClient(new Point(e.X, e.Y)));
            if (tnOver != null)
            {
                TreeNode tnDrag = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
                Int32 iOverType = this.GetBranchTypeFromTag(tnOver.Tag as String);
                Int32 iDragType = this.GetBranchTypeFromTag(tnDrag.Tag as String);

                //如果节点属于同一分支才能接受
                if (iOverType == iDragType)
                {
                    e.Effect = DragDropEffects.Move;
                    tvEvent.SelectedNode = tnOver;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        /// <summary>
        /// 在工程资源树结构视图上结束拖动。
        /// </summary>
        private void tvProjectResource_DragDrop(object sender, DragEventArgs e)
        {
            //获得拖放中的节点和拖放目标TreeView，且不是自己移动到自己身上
            TreeView tvDragDrop = sender as TreeView;
            TreeNode tnDragNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
            TreeNode tnTargetNode = tvDragDrop.GetNodeAt(tvDragDrop.PointToClient(new Point(e.X, e.Y)));
            if (tnDragNode == null || tnDragNode == null || tnTargetNode == null || tnDragNode == tnTargetNode)
            {
                return;
            }

            //根据Tag进一步判断合法性，目标是文件节点或者目标位工程根目录
            String strDragTag = tnDragNode.Tag as String;
            String strTargetTag = tnTargetNode.Tag as String;
            if (strTargetTag == null || strDragTag == null || strTargetTag.Contains(MANAGE_TAG_PRM_FILE) || strTargetTag == MANAGE_TAG_PRM_PROJECTROOT)
            {
                return;
            }

            //获取节点类型和对应的文件或文件夹路径
            String strCut, strPast;
            Int32 iCut = this.GetFullPathFromTreeNode(tnDragNode, out strCut);
            Int32 iPast = this.GetFullPathFromTreeNode(tnTargetNode, out strPast);
            if (iCut != iPast)
            {
                //不同类型的资源不能移动
                MessageBox.Show("不能移动到其他类型资源的文件夹中", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (strDragTag.Contains(MANAGE_TAG_PRM_FOLDER) && strPast.Contains(strCut))
            {
                //目标文件夹不能是源文件夹的子文件夹
                MessageBox.Show("不能移动文件夹到自己子文件夹中", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MainForm.AppMainForm.IsOpenFile(strCut))
            {
                //已打开的不能移动
                MessageBox.Show("文件或文件夹内有文件已打开，不能移动", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //判断目标节点的子节点中是否有重名的
            foreach (TreeNode tnTemp in tnTargetNode.Nodes)
            {
                if (tnDragNode.Text.Equals(tnTemp.Text))
                {
                    String strType = strDragTag.Contains(MANAGE_TAG_PRM_FOLDER) ? "文件夹" : "文件";
                    String strMessage = String.Format("{0}\"{1}\"已存在", strType, tnDragNode.Text);
                    MessageBox.Show(strMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //从TreeView中删除拖动节点，文件和文件夹对应不同的处理方式
            Int32 iRet = 0;
            tnDragNode.Remove();
            if (strDragTag.Contains(MANAGE_TAG_PRM_FILE))
            {
                //移动资源，文件节点放在开头
                iRet = ProjectManager.Project.MoveProjectResource(strPast, strCut, iCut);
                if (iRet == 0)
                {
                    tnTargetNode.Nodes.Insert(0, tnDragNode);
                }
            }
            else
            {
                //移动文件夹,文件夹节点则放在末尾
                iRet = ProjectManager.Project.MoveResourceFolder(strPast, strCut);
                if (iRet == 0)
                {
                    tnTargetNode.Nodes.Insert(tnTargetNode.Nodes.Count, tnDragNode);
                }
            }

            //结果判断处理
            if (iRet != 0)
            {
                this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iCut);
            }
            else
            {
                //拖动节点为选择节点
                tvDragDrop.SelectedNode = tnDragNode;
            }
        }

        /// <summary>
        /// 准备编辑工程资源树结构视图节点。
        /// </summary>
        private void tvProjectResource_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeNode tnEdit = e.Node;
            String strTag = tnEdit.Tag as String;
            if (strTag != null)
            {
                //只有四大分支节点不能改名
                e.CancelEdit = strTag.Contains(MANAGE_TAG_PRM_ROOT) && strTag != MANAGE_TAG_PRM_PROJECTROOT;
            }
        }

        /// <summary>
        /// 工程资源树结构视图节点编辑结束。
        /// </summary>
        private void tvProjectResource_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //判断是否可以接受修改
            TreeNode tnNode = e.Node;
            String strTag = tnNode.Tag as String;
            if (e.Label == null || e.Label == String.Empty || strTag == null || e.Label == tnNode.Text)
            {
                e.CancelEdit = true;
                return;
            }

            //判断是否与兄弟节点重名（文件夹与文件可以重名）
            if (tnNode.Parent != null)
            {
                foreach (TreeNode tnTemp in tnNode.Parent.Nodes)
                {
                    if (tnTemp != tnNode && tnTemp.Tag.Equals(tnNode.Tag) && tnTemp.Text == e.Label)
                    {
                        e.CancelEdit = true;
                        String strMessage = String.Format("\"{0}\"已存在。", e.Label);
                        if (MessageBox.Show(strMessage, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            tnNode.BeginEdit();
                        }
                        return;
                    }
                }
            }

            e.CancelEdit = this.AfterNodeRename(tnNode, e.Label);
        }

        /// <summary>
        /// 鼠标双击工程资源节点。
        /// </summary>
        private void tvProjectResource_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tnClick = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                String strTag = tnClick.Tag as String;
                if (strTag != null && strTag.Contains(MANAGE_TAG_PRM_FILE))
                {
                    this.tvProjectResource.SelectedNode = tnClick;
                    this.DoOpen();
                }
            }
        }

        /// <summary>
        /// 工程菜单准备打开。
        /// </summary>
        private void cmsProject_Opening(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 分支菜单准备打开。
        /// </summary>
        private void cmsBranch_Opening(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 节点菜单准备打开。
        /// </summary>
        private void cmsResource_Opening(object sender, CancelEventArgs e)
        {
            //根据节点类型确定显示不同的菜单项
            String strTag = this.m_tnContextMenuNode.Tag as String;
            Boolean bFolder = strTag.Contains(MANAGE_TAG_PRM_FOLDER);
            Int32 iNodeType = this.GetBranchTypeFromTag(strTag);

            //设置菜单的显示项，不能依赖Visible属性，因为菜单还没显示，读取的值永远是false
            if (iNodeType == ProjectManager.TYPE_RESOURCE_PARTICLE)
            {
                this.tmsiResourceOpen.Visible = !bFolder;                       //文件才能打开
                this.tmsiResourceNew.Visible = bFolder;                         //文件夹才可以创建
                this.tmsiResourceBuild.Visible = !bFolder;                      //文件才能生成
            }
        }

        /// <summary>
        /// 单击工程快捷菜单 刷新 选项。
        /// </summary>
        private void tsmiProjectRefresh_Click(object sender, EventArgs e)
        {
            this.LoadProject(ProjectManager.Project.ProjectFile);
        }

        /// <summary>
        /// 单击工程快捷菜单 重命名 选项。
        /// </summary>
        private void tsmiProjectRename_Click(object sender, EventArgs e)
        {
            this.DoRename();
        }

        /// <summary>
        /// 单击工程快捷菜单 属性 选项。
        /// </summary>
        private void tsmiProjectProperty_Click(object sender, EventArgs e)
        {
            this.DoProjectProperty();
        }

        /// <summary>
        /// 单击分支快捷菜单 新建文件 选项。
        /// </summary>
        private void tmsiBranchNewFile_Click(object sender, EventArgs e)
        {
            this.DoNewFile();
        }

        /// <summary>
        /// 单击分支快捷菜单 新建文件夹 选项。
        /// </summary>
        private void tmsiBranchNewFolder_Click(object sender, EventArgs e)
        {
            this.DoNewFolder();
        }

        /// <summary>
        /// 单击分支快捷菜单 刷新 选项。
        /// </summary>
        private void tmsiBranchRefresh_Click(object sender, EventArgs e)
        {
            TreeNode tnBranch = this.m_tnContextMenuNode;
            Int32 iNodeType = this.GetBranchTypeFromTag(tnBranch.Tag as String);
            this.LoadAppointedResource(ProjectManager.Project.ProjectFolder, iNodeType);
        }

        /// <summary>
        /// 单击分支快捷菜单 生成 选项。
        /// </summary>
        private void tmsiBranchBuild_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单击资源快捷菜单 打开 选项。
        /// </summary>
        private void tmsiResourceOpen_Click(object sender, EventArgs e)
        {
            this.DoOpen();
        }

        /// <summary>
        /// 单击资源快捷菜单 新建文件 选项。
        /// </summary>
        private void tmsiResourceNewFile_Click(object sender, EventArgs e)
        {
            this.DoNewFile();
        }

        /// <summary>
        /// 单击资源快捷菜单 新建文件夹 选项。
        /// </summary>
        private void tmsiResourceNewFolder_Click(object sender, EventArgs e)
        {
            this.DoNewFolder();
        }

        /// <summary>
        /// 单击资源快捷菜单 生成 选项。
        /// </summary>
        private void tmsiResourceBuild_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单击资源快捷菜单 复制 选项。
        /// </summary>
        private void tmsiResourceCopy_Click(object sender, EventArgs e)
        {
            this.DoCopy();
        }

        /// <summary>
        /// 单击资源快捷菜单 删除 选项。
        /// </summary>
        private void tmsiResourceDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        /// <summary>
        /// 单击资源快捷菜单 重命名 选项。
        /// </summary>
        private void tmsiResourceRename_Click(object sender, EventArgs e)
        {
            this.DoRename();
        }

        #endregion
    }
}
