using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T006.Forms;
using WeifenLuo.WinFormsUI.Docking;
using T006.Data;
using System.IO;
using T006.Data.Particle;

namespace T006
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 全局变量，任何时候可以访问主窗体。
        /// </summary>
        public static MainForm AppMainForm = null;

        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.m_lstResourceFile = new List<ResourceFileForm>();
        }

        /// <summary>
        /// 进行新建工程操作。
        /// </summary>
        public void DoNewProject()
        {
            NewProjectForm npfNewProject = new NewProjectForm();
            if (npfNewProject.ShowDialog() == DialogResult.OK)
            {
                //把原来的工程关闭掉
                if (!this.DoCloseProject())
                {
                    return;
                }

                String strSaveFoler = npfNewProject.ProjectPath;
                String strPojectName = npfNewProject.ProjectName;
                Int32 iWidth = npfNewProject.SceneWidth;
                Int32 iHeight = npfNewProject.SceneHeight;
                Int32 iFPS = npfNewProject.ShowFPS;

                //创建新的工程
                if (ProjectManager.Project.CreateNewProject(strPojectName, strSaveFoler, iWidth, iHeight, iFPS) == 0)
                {
                    String strFileName = String.Format("{0}\\{1}\\{1}{2}", strSaveFoler, strPojectName, ProjectManager.NAME_EXT_PROJECT);

                    //打开新建的工程
                    this.StatusBarText1 = String.Format("正在打开工程: \"{0}\" ......", strFileName);
                    if (ProjectManager.Project.OpenProject(strFileName) == 0)
                    {
                        this.m_pmfProjectResource.LoadProject(strFileName);
                        this.Text = ProjectManager.Project.ProjectName + @"-" + Application.ProductName;
                        this.m_pmfProjectResource.Show();
                        this.m_epfEffectProperty.AssetsFolder = String.Empty;
                        this.SetProjectOpenOrClose(true);
                    }
                    this.StatusBarText1 = String.Empty;
                }
            }
        }

        /// <summary>
        /// 进行打开工程操作。
        /// </summary>
        public void DoOpenProject()
        {
            //选择打开工程文件
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "粒子工程文件 *.pp|*.pp";
            ofDialog.RestoreDirectory = true;                   //有可能修改目录名称，所以必须还原目录
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                //把原来的工程关闭掉
                if (!this.DoCloseProject())
                {
                    return;
                }

                this.StatusBarText1 = String.Format("正在打开工程: \"{0}\" ......", ofDialog.FileName);
                if (ProjectManager.Project.OpenProject(ofDialog.FileName) == 0)
                {
                    this.StatusBarText1 = "正在加载工程资源 ......";
                    this.m_pmfProjectResource.LoadProject(ofDialog.FileName);
                    this.Text = ProjectManager.Project.ProjectName + @"-" + Application.ProductName;
                    this.tmUpdate.Interval = 1000 / ProjectManager.Project.ShowFPS;
                    this.m_pmfProjectResource.Show();
                    this.m_epfEffectProperty.AssetsFolder = ProjectManager.Project.AssetsFolder;
                    this.SetProjectOpenOrClose(true);
                }
                else
                {
                    MessageBox.Show("打开工程失败：\r\n" + ofDialog.FileName, "错误");
                }
                this.StatusBarText1 = String.Empty;
            }
        }

        /// <summary>
        /// 新建资源文件。
        /// </summary>
        /// <param name="strFolderName">所在的文件夹</param>
        /// <param name="iType">资源类型。</param>
        public String NewResourceFile(String strFolderName, Int32 iType)
        {
            String strFileName = String.Empty;

            if (iType == ProjectManager.TYPE_RESOURCE_PARTICLE)
            {
                strFileName = this.NewParticleFile(strFolderName);
            }
            return strFileName;
        }

        /// <summary>
        /// 进行关闭文件操作。
        /// </summary>
        public void DoCloseFile()
        {
            ResourceFileForm rffFileForm = this.dkpMainForm.ActiveDocument as ResourceFileForm;

            if (rffFileForm != null)
            {
                rffFileForm.Close();
            }
        }

        /// <summary>
        /// 进行关闭工程操作。
        /// </summary>
        /// <returns>返回是否关闭了工程。</returns>
        public Boolean DoCloseProject()
        {
            //先关闭所有打开的文档窗体
            while (this.m_lstResourceFile.Count > 0)
            {
                Int32 iOldCount = this.m_lstResourceFile.Count;
                Int32 iNewCount = 0;
                ResourceFileForm rffTemp = this.m_lstResourceFile[0];

                rffTemp.Close();
                iNewCount = this.m_lstResourceFile.Count;

                //如果窗体数量没减少说明没有关闭被取消
                if (iOldCount == iNewCount)
                {
                    return false;
                }
            }

            this.m_pmfProjectResource.CloseProject();
            this.m_pmfProjectResource.Hide();
            this.SetProjectOpenOrClose(false);
            ProjectManager.Project.CloseProject();
            return true;
        }

        /// <summary>
        /// 进行重命名操作。
        /// </summary>
        public void DoRename()
        {
            if (this.m_pmfProjectResource.IsActivated)
            {
                this.m_pmfProjectResource.DoRename();
            }
        }

        /// <summary>
        /// 进行删除操作。
        /// </summary>
        public void DoDelete()
        {
            ResourceFileForm rffEdit = this.dkpMainForm.ActiveContent as ResourceFileForm;

            //根据焦点来确定是哪个窗体进行删除操作
            if (rffEdit != null)
            {
                rffEdit.DoDelete();
            }
            else if (this.m_pmfProjectResource.IsActivated)
            {
                this.m_pmfProjectResource.DoDelete();
            }
        }

        /// <summary>
        /// 进行项目属性操作。
        /// </summary>
        public void DoProjectProperty()
        {
            ProjectPropertyForm ppf = new ProjectPropertyForm();
            ppf.Project = ProjectManager.Project;
            ppf.ReadOnly = this.m_lstResourceFile.Count > 0;
            if (ppf.ShowDialog() == System.Windows.Forms.DialogResult.OK && !ppf.ReadOnly)
            {
                this.tmUpdate.Interval = 1000 / ProjectManager.Project.ShowFPS;
            }
        }

        /// <summary>
        /// 打开资源文件。
        /// </summary>
        /// <param name="strFileName">资源文件的绝对路径。</param>
        /// <param name="iType">资源类型。</param>
        public void OpenResourceFile(String strFileName, Int32 iType)
        {
            //检查是否满足打开文件的条件
            if (iType == ProjectManager.TYPE_RESOURCE_PARTICLE)
            {
                if (ProjectManager.Project.AssetsFolder == String.Empty)
                {
                    MessageBox.Show("打开界面文件需要先设置资源目录。 ^_^", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DoProjectProperty();
                    if (ProjectManager.Project.AssetsFolder == String.Empty)
                    {
                        return;
                    }
                }
            }

            //查看是否已经打开了文件
            foreach (ResourceFileForm rffTemp in this.m_lstResourceFile)
            {
                if (rffTemp.EditFile.FileName.Equals(strFileName))
                {
                    rffTemp.Activate();
                    return;
                }
            }

            //创建文件和窗体将其打开
            ResourceFile rfEditFile = null;
            ResourceFileForm rffNewForm = null;
            if (iType == ProjectManager.TYPE_RESOURCE_PARTICLE)
            {
                rffNewForm = new ParticleFileForm();
                this.tmUpdate.Enabled = true;
            }
            else
            {
                this.StatusBarText1 = String.Empty;
                return;
            }

            this.StatusBarText1 = String.Format("正在打开文件: \"{0}\" ......", strFileName);
            try
            {
                rfEditFile = ProjectManager.Project.OpenResourceFile(strFileName, iType);
            }
            catch (Exception e)
            {
                String errmsg = String.Format("打开文件失败。\nFile:{0}\nErr:{1}", strFileName, e.Message);
                MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.StatusBarText1 = String.Empty;
                return;
            }

            //初始化文件窗体
            rffNewForm.EditFile = rfEditFile;
            rffNewForm.FormClosing += new FormClosingEventHandler(ResourceForm_FormClosing);
            rffNewForm.Show(this.dkpMainForm, DockState.Document);
            rffNewForm.Activate();
            this.m_lstResourceFile.Add(rffNewForm);
            this.StatusBarText1 = String.Empty;
        }

        /// <summary>
        /// 重命名文件。
        /// </summary>
        /// <param name="strFileName">原来的文件名。</param>
        /// <param name="strNewFileName">新的文件名。</param>
        public void RenameResourceFile(String strFileName, String strNewFileName)
        {
            //找到文件名相同的文件并修改文件名和窗体Text
            foreach (ResourceFileForm rffTemp in this.m_lstResourceFile)
            {
                ResourceFile rfEdit = rffTemp.EditFile;

                if (rfEdit.FileName.Equals(strFileName))
                {
                    rffTemp.SetFileName(strNewFileName);
                    break;
                }
            }
        }

        /// <summary>
        /// 重命名文件夹。
        /// </summary>
        /// <param name="strFolerName">原来的文件夹名。</param>
        /// <param name="strNewFolderName">新的文件夹名。</param>
        public void RenameResourceFolder(String strFolerName, String strNewFolderName)
        {
            //更新所有该文件夹下的文件名
            foreach (ResourceFileForm rffTemp in this.m_lstResourceFile)
            {
                ResourceFile rfEdit = rffTemp.EditFile;

                if (rfEdit.FileName.Contains(strFolerName))
                {
                    rfEdit.FileName = strNewFolderName + rfEdit.FileName.Substring(strFolerName.Length);
                }
            }
        }

        /// <summary>
        /// 直接关闭文档窗口。不保存了。
        /// </summary>
        /// <param name="strFileName">文件绝对路径。若为文件夹则关闭文件夹下的所有文件窗口。</param>
        /// <param name="bFile">true为文件，false为文件夹。</param>
        public void CloseFileWindow(String strFileName, Boolean bFile)
        {
            for (Int32 i = 0; i < this.m_lstResourceFile.Count; ++i)
            {
                ResourceFileForm rffForm = this.m_lstResourceFile[i];

                if (bFile)
                {
                    if (rffForm.EditFile.FileName == strFileName)
                    {
                        rffForm.NeedSave = false;
                        rffForm.Close();
                        break;
                    }
                }
                else if (rffForm.EditFile.FileName.Contains(strFileName))
                {
                    rffForm.NeedSave = false;
                    rffForm.Close();
                    i--;        //删掉了一个，下标才可以这么玩
                }
            }
        }

        /// <summary>
        /// 判断是否打开了某个文件或某个文件夹下的文件。
        /// </summary>
        /// <param name="file">文件或文件夹的绝对路径。</param>
        /// <returns>文件是否打开了。</returns>
        public Boolean IsOpenFile(String file)
        {
            foreach (ResourceFileForm rf in m_lstResourceFile)
            {
                if (rf.EditFile.FileName.StartsWith(file))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 进行生成粒子操作。
        /// </summary>
        public void DoBuildParticle()
        {
            if (!Directory.Exists(ProjectManager.Project.ParticleBuildFolder))
            {
                String str = "粒子生成目录不存在。\n" + ProjectManager.Project.ParticleBuildFolder;
                MessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.EditFileForm is ParticleFileForm)
            {
                ParticleFile pfedit = this.EditFileForm.EditFile as ParticleFile;
                this.StatusBarText1 = "正在生成粒子:" + pfedit.FileName;
                this.Refresh();
                pfedit.BuildParticleFile();
                this.StatusBarText1 = String.Empty;
            }
        }

        /// <summary>
        /// 进行生成所有粒子操作。
        /// </summary>
        public void DoBuildAllParticle()
        {
            if (!Directory.Exists(ProjectManager.Project.ParticleBuildFolder))
            {
                String str = "粒子生成目录不存在。\n" + ProjectManager.Project.ParticleBuildFolder;
                MessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.StatusBarText1 = "正在生成所有粒子...";
            this.Refresh();
            this.BuildFolderParticle(ProjectManager.Project.ParticleRootFolder);
            this.StatusBarText1 = String.Empty;
        }

        /// <summary>
        /// 进行粒子属性设置。
        /// </summary>
        public void DoParticleProperty()
        {
            ParticleFileForm pff = this.EditFileForm as ParticleFileForm;
            if (pff != null)
            {
                //打开窗口进行编辑
                ParticlePropertyForm ppf = new ParticlePropertyForm();
                ppf.EditFile = pff.EditFile as ParticleFile;
                ppf.ShowDialog();
            }
        }

        /// <summary>
        /// 进行粒子背景图像设置。
        /// </summary>
        public void DoBackImage()
        {
            ParticleFileForm pff = this.EditFileForm as ParticleFileForm;
            if (pff != null)
            {
                //打开窗口进行编辑
                BackImageForm bif = new BackImageForm();
                if (bif.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    (pff.EditFile as ParticleFile).BackImageName = bif.ImageName;
                    pff.SetBackImage(bif.ImageName);
                }
            }
        }

        /// <summary>
        /// 进行重置粒子系统操作。
        /// </summary>
        public void DoResetParticle()
        {
            ParticleFileForm pff = this.EditFileForm as ParticleFileForm;
            if (pff != null)
            {
                pff.ResetParticle();
            }
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置状态栏第一个文本。
        /// </summary>
        public String StatusBarText1
        {
            get
            {
                return this.tsslMainText1.Text;
            }
            set
            {
                this.tsslMainText1.Text = value;
                this.stsMainForm.Refresh();
            }
        }

        /// <summary>
        /// 获取或设置状态栏第二个文本。
        /// </summary>
        public String StatusBarText2
        {
            get
            {
                return this.tsslMainText2.Text;
            }
            set
            {
                this.tsslMainText2.Text = value;
                this.stsMainForm.Refresh();
            }
        }

        /// <summary>
        /// 获取或设置状态栏第三个文本。
        /// </summary>
        public String StatusBarText3
        {
            get
            {
                return this.tsslMainText3.Text;
            }
            set
            {
                this.tsslMainText3.Text = value;
                this.stsMainForm.Refresh();
            }
        }

        /// <summary>
        /// 获取或设置状态栏第四个文本。
        /// </summary>
        public String StatusBarText4
        {
            get
            {
                return this.tsslMainText4.Text;
            }
            set
            {
                this.tsslMainText4.Text = value;
                this.stsMainForm.Refresh();
            }
        }

        /// <summary>
        /// 获取或设置状态栏第五个文本。
        /// </summary>
        public String StatusBarText5
        {
            get
            {
                return this.tsslMainText5.Text;
            }
            set
            {
                this.tsslMainText5.Text = value;
                this.stsMainForm.Refresh();
            }
        }

        /// <summary>
        /// 获取工程资源管理窗体。
        /// </summary>
        public ProjectManageForm ProjectManage
        {
            get
            {
                return this.m_pmfProjectResource;
            }
        }

        /// <summary>
        /// 获取效果属性窗体。
        /// </summary>
        public EffectPropertyForm EffectProperty
        {
            get
            {
                return this.m_epfEffectProperty;
            }
        }

        /// <summary>
        /// 获取编辑中的资源文件窗口
        /// </summary>
        public ResourceFileForm EditFileForm
        {
            get
            {
                return this.dkpMainForm.ActiveDocument as ResourceFileForm;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 设置窗体布局。
        /// </summary>
        /// <param name="iType">资源类型。</param>
        protected void SetFormLayout(Int32 iType)
        {
            //粒子页面
            this.m_epfEffectProperty.SetVisible(iType == ProjectManager.TYPE_RESOURCE_PARTICLE);
            this.tsmiMainParticle.Enabled = iType == ProjectManager.TYPE_RESOURCE_PARTICLE;
            this.tsbMainPlay.Enabled = iType == ProjectManager.TYPE_RESOURCE_PARTICLE;
        }

        /// <summary>
        /// 设置工程是否打开或关闭对应的窗体状态 。
        /// </summary>
        protected void SetProjectOpenOrClose(Boolean bOpen)
        {
            this.tsmiMainEdit.Enabled = bOpen;
            this.tsmiMainProject.Enabled = bOpen;
            this.tsmiMainNewParticle.Enabled = bOpen;
            this.tsmiMainFileNewParticle.Enabled = bOpen;
            this.tsmiMainFileSave.Enabled = bOpen;
            this.tsmiMainFileSaveAll.Enabled = bOpen;
            this.tsmiMainFileClose.Enabled = bOpen;
            this.tsmiMainFileCloseProject.Enabled = bOpen;
            this.tsbMainSave.Enabled = bOpen;
            this.tsbMainSaveAll.Enabled = bOpen;
            this.tsbMainDelete.Enabled = bOpen;
            this.tsbMainUndo.Enabled = bOpen;
            this.tsbMainRedo.Enabled = bOpen;
            this.SetFormLayout(ProjectManager.TYPE_RESOURCE_UNKNOW);
        }

        /// <summary>
        /// 创建粒子文件。
        /// </summary>
        /// <param name="strFolderName">文件所在的文件夹</param>
        protected String NewParticleFile(String strFolderName)
        {
            String strFileName = String.Empty;
            NewParticleFileForm npffNewMapFile = new NewParticleFileForm();

            npffNewMapFile.CreateFolder = strFolderName;
            if (npffNewMapFile.ShowDialog() == DialogResult.OK)
            {
                String strName = npffNewMapFile.ParticleName;
                String strID = npffNewMapFile.ParticleID;
                String strEName = npffNewMapFile.EffectName;
                String strEImage = npffNewMapFile.EffectImage;
                EffectType type = npffNewMapFile.EffectType;

                if (ParticleFile.CreateParticleFile(strName, strFolderName, strID, strEName, strEImage, type) == 0)
                {
                    strFileName = strName;
                    this.OpenResourceFile(strFolderName + "\\" + strName + ProjectManager.NAME_EXT_PARTICLE_EDIT, ProjectManager.TYPE_RESOURCE_PARTICLE);
                }
                else
                {
                    MessageBox.Show("创建粒子文件失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return strFileName;
        }

        /// <summary>
        /// 递归生成一个文件夹内的所有粒子。
        /// </summary>
        /// <param name="folder">文件夹路径。</param>
        protected void BuildFolderParticle(String folder)
        {
            //获取该目录下的文件和文件夹信息
            DirectoryInfo diFileFolder = new DirectoryInfo(folder);

            //先加载文件信息
            FileInfo[] fiaFiles = diFileFolder.GetFiles();
            foreach (FileInfo fiTemp in fiaFiles)
            {
                if (fiTemp.Extension.Equals(ProjectManager.NAME_EXT_PARTICLE_EDIT))
                {
                    ParticleFile pfBuild = ParticleFile.LoadFromFile(fiTemp.FullName);
                    if (pfBuild != null)
                    {
                        pfBuild.BuildParticleFile();
                    }
                }
            }

            //加载文件夹信息
            DirectoryInfo[] diaFolders = diFileFolder.GetDirectories();
            foreach (DirectoryInfo diTemp in diaFolders)
            {
                this.BuildFolderParticle(diTemp.FullName);
            }
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 工程资源管理窗体。
        /// </summary>
        private ProjectManageForm m_pmfProjectResource = null;

        /// <summary>
        /// 效果属性窗体。
        /// </summary>
        private EffectPropertyForm m_epfEffectProperty = null;

        /// <summary>
        /// 文件窗体链表。
        /// </summary>
        private List<ResourceFileForm> m_lstResourceFile = null;

        /// <summary>
        /// 上次更新时刻
        /// </summary>
        private Int64 m_iLastTick = 0;

        #endregion

        #region 事件函数=====================================================================================

        #region 窗体处理=====================================================================================

        /// <summary>
        /// 窗体载入，初始化程序。
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Show();
            this.Activate();

            #region 初始化程序数据============================================================================

            MainForm.AppMainForm = this;
            ProjectManager.Project = new ProjectManager();

            #endregion

            #region 设置程序的工作窗体========================================================================

            //初始化窗体，窗体停放顺序要注意，被夹在中间的先放入
            this.dkpMainForm.Visible = false;
            this.StatusBarText1 = "正在初始化窗体 ......";
            this.Refresh();

            //创建控件属性窗体
            this.m_epfEffectProperty = new EffectPropertyForm();
            this.m_epfEffectProperty.Show(this.dkpMainForm, DockState.DockBottom);
            this.m_epfEffectProperty.Hide();

            //创建工程资源管理窗体
            this.m_pmfProjectResource = new ProjectManageForm();
            this.m_pmfProjectResource.Show(this.dkpMainForm, DockState.DockLeft);
            this.m_pmfProjectResource.Hide();

            #endregion

            //界面初始化
            this.dkpMainForm.Visible = true;
            this.SetProjectOpenOrClose(false);
            this.StatusBarText1 = String.Empty;
            this.StatusBarText2 = String.Empty;
            this.StatusBarText3 = String.Empty;
            this.StatusBarText4 = String.Empty;
            this.StatusBarText5 = String.Empty;
        }

        /// <summary>
        /// 主窗体尺寸发生改变。
        /// </summary>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.tsslMainText1.Width = this.stsMainForm.Width - 462;
        }

        /// <summary>
        /// 主窗体关闭，文件保存、关闭处理。
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭工程，如果有窗体未被关闭则意味着工程关闭被取消了
            this.DoCloseProject();
            e.Cancel = m_lstResourceFile.Count > 0;
        }

        /// <summary>
        /// 文件窗体关闭。
        /// </summary>
        private void ResourceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResourceFileForm rffForm = sender as ResourceFileForm;
            ResourceFile rfEditFile = rffForm.EditFile;

            //保存操作
            if (rfEditFile.Amend && rffForm.NeedSave)
            {
                //通过对话框是否确认保存。
                String strMessage = String.Format("是否保存对\"{0}\"的修改。", Path.GetFileNameWithoutExtension(rfEditFile.FileName));
                DialogResult drResult = MessageBox.Show(strMessage, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (drResult == DialogResult.Cancel)
                {
                    e.Cancel = true;

                    return;
                }
                else if (drResult == DialogResult.Yes)
                {
                    rfEditFile.SaveFile();
                }
            }

            //关闭文件前对其他工作窗体的清理工作
            Int32 iType = rfEditFile.ResourceType;
            if (iType == ProjectManager.TYPE_RESOURCE_PARTICLE)
            {

            }
            rffForm.EditFile = null;
            rfEditFile.CloseFile();

            //移除文件窗体
            this.m_lstResourceFile.Remove(rffForm);
            if (this.m_lstResourceFile.Count == 0)
            {
                this.SetFormLayout(ProjectManager.TYPE_RESOURCE_UNKNOW);
                this.tmUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// 活动窗体文档发生改变。
        /// </summary>
        private void dkpMainForm_ActiveDocumentChanged(object sender, EventArgs e)
        {
            ResourceFileForm rffFileForm = this.dkpMainForm.ActiveDocument as ResourceFileForm;

            if (rffFileForm != null)
            {
                ResourceFile rfEditFile = rffFileForm.EditFile;

                if (rfEditFile != null)
                {
                    Int32 iType = rfEditFile.ResourceType;
                    this.SetFormLayout(iType);

                    //初始化各种文件工作窗体信息
                    if (iType == ProjectManager.TYPE_RESOURCE_PARTICLE)
                    {
                        ParticleFileForm pffForm = rffFileForm as ParticleFileForm;
                        ParticleFile pfEdit = rfEditFile as ParticleFile;
                        this.m_epfEffectProperty.EditFile = pfEdit;
                    }
                }
            }
        }

        /// <summary>
        /// 粒子系统更新。
        /// </summary>
        private void tmUpdate_Tick(object sender, EventArgs e)
        {
            ParticleFileForm pff = this.EditFileForm as ParticleFileForm;
            if (pff != null)
            {
                long now = DateTime.Now.Ticks;
                long tm = Math.Min(10000000, now - m_iLastTick);
                m_iLastTick = now;
                pff.UpdateParticle(tm / 10000000.0f);
            }
        }

        #endregion

        #region 主菜单处理===================================================================================

        /// <summary>
        /// 选择主菜单 文件->新建->工程 选项。
        /// </summary>
        private void tsmiMainFileNewProject_Click(object sender, EventArgs e)
        {
            this.DoNewProject();
        }

        /// <summary>
        /// 选择主菜单 文件->新建->界面 选项。
        /// </summary>
        private void tsmiMainFileNewParticle_Click(object sender, EventArgs e)
        {
            String strFileName = this.NewParticleFile(ProjectManager.Project.ParticleRootFolder);
            this.m_pmfProjectResource.AddFileNodeToRoot(strFileName, ProjectManager.TYPE_RESOURCE_PARTICLE);
        }

        /// <summary>
        /// 选择主菜单 文件->打开工程 选项。
        /// </summary>
        private void tsmiMainFileOpenProject_Click(object sender, EventArgs e)
        {
            this.DoOpenProject();
        }

        /// <summary>
        /// 选择主菜单 文件->保存 选项。
        /// </summary>
        private void tsmiMainFileSave_Click(object sender, EventArgs e)
        {
            ResourceFileForm rffEdit = this.dkpMainForm.ActiveDocument as ResourceFileForm;
            if (rffEdit != null)
            {
                rffEdit.DoSaveFile();
            }
        }

        /// <summary>
        /// 选择主菜单 文件->全部保存 选项。
        /// </summary>
        private void tsmiMainFileSaveAll_Click(object sender, EventArgs e)
        {
            foreach (ResourceFileForm rffTemp in this.m_lstResourceFile)
            {
                rffTemp.DoSaveFile();
            }
        }

        /// <summary>
        /// 选择主菜单 文件->关闭 选项。
        /// </summary>
        private void tsmiMainFileClose_Click(object sender, EventArgs e)
        {
            this.DoCloseFile();
        }

        /// <summary>
        /// 选择主菜单 文件->关闭工程 选项。
        /// </summary>
        private void tsmiMainFileCloseProject_Click(object sender, EventArgs e)
        {
            this.DoCloseProject();
        }

        /// <summary>
        /// 选择主菜单 文件->退出 选项。
        /// </summary>
        private void tsmiMainFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择主菜单 编辑->撤消 选项。
        /// </summary>
        private void tsmiMainEditUndo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择主菜单 编辑->重做 选项。
        /// </summary>
        private void tsmiMainEditRedo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择主菜单 编辑->删除 选项。
        /// </summary>
        private void tsmiMainEditDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        /// <summary>
        /// 选择主菜单 编辑->重命名 选项。
        /// </summary>
        private void tsmiMainEditRename_Click(object sender, EventArgs e)
        {
            this.DoRename();
        }

        /// <summary>
        /// 选择主菜单 项目->全部生成 选项。
        /// </summary>
        private void tsmiMainProjectBuildAll_Click(object sender, EventArgs e)
        {
            this.DoBuildAllParticle();
        }

        /// <summary>
        /// 选择主菜单 粒子->播放 选项。
        /// </summary>
        private void tsmiMainEditParticlePlay_Click(object sender, EventArgs e)
        {
            this.DoResetParticle();
        }

        /// <summary>
        /// 选择主菜单 粒子->属性设置 选项。
        /// </summary>
        private void tsmiMainEditParticleProperty_Click(object sender, EventArgs e)
        {
            this.DoParticleProperty();
        }

        /// <summary>
        /// 选择主菜单 粒子->背景图 选项。
        /// </summary>
        private void tsmiMainEditParticleBack_Click(object sender, EventArgs e)
        {
            this.DoBackImage();
        }

        /// <summary>
        /// 选择主菜单 粒子->生成 选项。
        /// </summary>
        private void tsmiMainEditParticleBuild_Click(object sender, EventArgs e)
        {
            this.DoBuildParticle();
        }

        /// <summary>
        /// 选择主菜单 项目->界面列表 选项。
        /// </summary>
        private void tsmiMainProjectParticleList_Click(object sender, EventArgs e)
        {
            ParticleListForm plf = new ParticleListForm();
            plf.ShowDialog();
        }

        /// <summary>
        /// 选择主菜单 项目->属性 选项。
        /// </summary>
        private void tsmiMainProjectProperty_Click(object sender, EventArgs e)
        {
            this.DoProjectProperty();
        }

        /// <summary>
        /// 选择主菜单 帮助->查看帮助 选项。
        /// </summary>
        private void tsmiMainHelpView_Click(object sender, EventArgs e)
        {
            HelpForm hf = new HelpForm();
            hf.ShowDialog();
        }

        /// <summary>
        /// 选择主菜单 帮助->关于 选项。
        /// </summary>
        private void tsmiMainHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm afAbout = new AboutForm();
            afAbout.ShowDialog();
        }

        #endregion

        #region 工具栏处理===================================================================================

        /// <summary>
        /// 单击工具栏上的新建按钮。
        /// </summary>
        private void tssbMainNew_ButtonClick(object sender, EventArgs e)
        {
            this.DoNewProject();
        }

        /// <summary>
        /// 选择工具栏上的 新建->工程 选项。
        /// </summary>
        private void tsmiMainNewProject_Click(object sender, EventArgs e)
        {
            this.DoNewProject();
        }

        /// <summary>
        /// 选择工具栏上的 新建->界面 选项。
        /// </summary>
        private void tsmiMainNewParticle_Click(object sender, EventArgs e)
        {
            String strFileName = this.NewParticleFile(ProjectManager.Project.ParticleRootFolder);
            this.m_pmfProjectResource.AddFileNodeToRoot(strFileName, ProjectManager.TYPE_RESOURCE_PARTICLE);
        }

        /// <summary>
        /// 单击工具栏上的打开按钮。
        /// </summary>
        private void tsbMainOpen_Click(object sender, EventArgs e)
        {
            this.DoOpenProject();
        }

        /// <summary>
        /// 单击工具栏上的保存按钮。
        /// </summary>
        private void tsbMainSave_Click(object sender, EventArgs e)
        {
            ResourceFileForm rffEdit = this.dkpMainForm.ActiveDocument as ResourceFileForm;
            if (rffEdit != null)
            {
                rffEdit.DoSaveFile();
            }
        }

        /// <summary>
        /// 单击工具栏上的全部保存按钮。
        /// </summary>
        private void tsbMainSaveAll_Click(object sender, EventArgs e)
        {
            foreach (ResourceFileForm rffTemp in this.m_lstResourceFile)
            {
                rffTemp.DoSaveFile();
            }
        }

        /// <summary>
        /// 单击工具栏上的删除按钮。
        /// </summary>
        private void tsbMainDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        /// <summary>
        /// 单击工具栏上的撤销按钮。
        /// </summary>
        private void tsbMainUndo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单击工具栏上的重做按钮。
        /// </summary>
        private void tsbMainRedo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单击工具栏上的播放按钮。
        /// </summary>
        private void tsbMainPlay_Click(object sender, EventArgs e)
        {
            this.DoResetParticle();
        }

        #endregion

        #endregion
    }
}
