using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T002.Data;

namespace T002.Forms
{
    /// <summary>
    /// 工程属性窗体。
    /// </summary>
    public partial class ProjectPropertyForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ProjectPropertyForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置窗口显示的项目属性。
        /// </summary>
        public ProjectManager Project
        {
            get
            {
                return this.m_pmProject;
            }

            set
            {
                this.m_pmProject = value;
                this.tibProjectName.InputValue = this.m_pmProject.ProjectName;
                this.fibProjectPath.InputValue = this.m_pmProject.ProjectFolder;
                this.fibProjectFont.FolderLimit = this.m_pmProject.ProjectFolder;
                this.fibProjectFont.InputValue = this.m_pmProject.FontFile;
                this.fibAssetsFolder.InputValue = this.m_pmProject.AssetsFolder;
                this.fibBuildFolder.InputValue = this.m_pmProject.InterfaceBuildFolder;
            }
        }

        /// <summary>
        /// 获取或设置窗体是否为只读状态。
        /// </summary>
        public Boolean ReadOnly
        {
            get
            {
                return this.m_bReadOnly;
            }
            set
            {
                this.m_bReadOnly = value;
            }
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 要显示的项目。
        /// </summary>
        private ProjectManager m_pmProject = null;

        /// <summary>
        /// 是否为只读状态。
        /// </summary>
        private Boolean m_bReadOnly = false;

        #endregion

        #region 控件事件=====================================================================================

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.m_bReadOnly && (this.m_pmProject.AssetsFolder != this.fibAssetsFolder.InputValue || this.m_pmProject.FontFile != this.fibProjectFont.InputValue))
            {
                MessageBox.Show("要修改资源目录请先关闭打开的界面文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.m_pmProject.AssetsFolder = this.fibAssetsFolder.InputValue;
            this.m_pmProject.FontFile = this.fibProjectFont.InputValue;
            this.m_pmProject.InterfaceBuildFolder = this.fibBuildFolder.InputValue;
            MainForm.AppMainForm.ControlProperty.AssetsFolder = this.m_pmProject.AssetsFolder;
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
