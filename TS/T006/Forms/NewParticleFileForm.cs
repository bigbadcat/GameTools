using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using T006.Data;
using T006.Data.Particle;

namespace T006.Forms
{
    /// <summary>
    /// 创建粒子文件对话框。
    /// </summary>
    public partial class NewParticleFileForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public NewParticleFileForm()
        {
            InitializeComponent();
            this.fibEffectImage.FolderLimit = ProjectManager.Project.AssetsFolder;
            this.iibEffectType.InputIndex = 0;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置要创建界面所在的目录。
        /// </summary>
        public String CreateFolder
        {
            get
            {
                return m_strCreateFolder;
            }
            set
            {
                m_strCreateFolder = value;
            }
        }

        /// <summary>
        /// 获取粒子名称。
        /// </summary>
        public String ParticleName
        {
            get
            {
                return this.tibFileName.InputValue.Trim();
            }
        }

        /// <summary>
        /// 获取粒子ID。
        /// </summary>
        public String ParticleID
        {
            get
            {
                return this.tibOutCode.InputValue.Trim();
            }
        }

        /// <summary>
        /// 获取输入的名称。
        /// </summary>
        public String EffectName
        {
            get
            {
                return this.tibEffectName.InputValue;
            }
        }

        /// <summary>
        /// 获取效果粒子图像文件路径。
        /// </summary>
        public String EffectImage
        {
            get
            {
                return this.fibEffectImage.InputValue;
            }
        }

        /// <summary>
        /// 获取预设的效果类型。
        /// </summary>
        public EffectType EffectType
        {
            get
            {
                return (EffectType)this.iibEffectType.InputIndex;
            }
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 要创建粒子所在的目录。
        /// </summary>
        private String m_strCreateFolder = String.Empty;

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //验证名称
            String fname = this.tibFileName.InputValue.Trim();
            if (fname == String.Empty)
            {
                MessageBox.Show("请输入粒子名称。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String strFileName = this.m_strCreateFolder + "\\" + fname + ProjectManager.NAME_EXT_PARTICLE_EDIT;
            if (File.Exists(strFileName))
            {

                MessageBox.Show("\"" + fname + "\"已存在。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //验证编号
            if (this.tibOutCode.InputValue.Trim().Equals(String.Empty))
            {
                MessageBox.Show("请输入粒子编号。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.tibEffectName.InputValue.Trim().Equals(String.Empty))
            {
                MessageBox.Show("请输入效果名称。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.fibEffectImage.InputValue.Equals(String.Empty))
            {
                MessageBox.Show("请选择粒子图像。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
