using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T006.Data;

namespace T006.Forms
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
                this.fibAssetsFolder.InputValue = this.m_pmProject.AssetsFolder;
                this.fibBuildFolder.InputValue = this.m_pmProject.ParticleBuildFolder;
                this.sibSceneSize.InputValue = new Size(this.m_pmProject.SceneWidth, this.m_pmProject.SceneHeight);
                this.nibShowFPS.InputValue = this.m_pmProject.ShowFPS;
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
                this.Text = this.m_bReadOnly ? "工程属性(只读)" : "工程属性";
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
            Int32 width = Decimal.ToInt32(this.sibSceneSize.InputValue.Width);
            Int32 height = Decimal.ToInt32(this.sibSceneSize.InputValue.Height);
            Int32 fps = (Int32)this.nibShowFPS.InputValue;
            Boolean modify = this.m_pmProject.SceneWidth != width || this.m_pmProject.SceneHeight != height || this.m_pmProject.AssetsFolder != this.fibAssetsFolder.InputValue || this.m_pmProject.ShowFPS != fps;
            if (this.m_bReadOnly && modify)
            {
                MessageBox.Show("要修改工程属性请先关闭所有打开的文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (width < 100 || height < 100)
            {
                MessageBox.Show("场景尺寸宽高都必须在100以上。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (fps < 10 || fps > 100)
            {
                MessageBox.Show("运行帧率必须在10-100之间。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.m_pmProject.AssetsFolder = this.fibAssetsFolder.InputValue;
            this.m_pmProject.ParticleBuildFolder = this.fibBuildFolder.InputValue;
            this.m_pmProject.SceneWidth = width;
            this.m_pmProject.SceneHeight = height;
            this.m_pmProject.ShowFPS = fps;
            MainForm.AppMainForm.EffectProperty.AssetsFolder = this.m_pmProject.AssetsFolder;
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
