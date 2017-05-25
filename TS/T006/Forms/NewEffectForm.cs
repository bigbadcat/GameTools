using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T006.Data;
using T006.Data.Particle;

namespace T006.Forms
{
    public partial class NewEffectForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public NewEffectForm()
        {
            InitializeComponent();
            this.fibImage.FolderLimit = ProjectManager.Project.AssetsFolder;
            this.iibType.InputIndex = 0;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取输入的名称。
        /// </summary>
        public String EffectName
        {
            get
            {
                return this.tibName.InputValue;
            }
        }

        /// <summary>
        /// 获取效果粒子图像文件路径。
        /// </summary>
        public String EffectImage
        {
            get
            {
                return this.fibImage.InputValue;
            }
        }

        /// <summary>
        /// 获取预设的效果类型。
        /// </summary>
        public EffectType EffectType
        {
            get
            {
                return (EffectType)this.iibType.InputIndex;
            }
        }

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 窗体加载。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.tibName.InputValue.Trim().Equals(String.Empty))
            {
                MessageBox.Show("请输入效果名称。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.fibImage.InputValue.Equals(String.Empty))
            {
                MessageBox.Show("请选择粒子图像。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}
