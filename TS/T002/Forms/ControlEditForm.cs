using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace T002.Forms
{
    /// <summary>
    /// 控件属性窗体。
    /// </summary>
    public partial class ControlEditForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ControlEditForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 设置编辑的控件。
        /// </summary>
        public T002.Data.UI.Control EditControl
        {
            set
            {
                this.tibCode.InputValue = value.Code.ToString();
                this.tibName.InputValue = value.Name;
                this.tibConstVar.InputValue = value.ConstVar;
            }
        }

        /// <summary>
        /// 获取控件名称。
        /// </summary>
        public String ControlName
        {
            get
            {
                return this.tibName.InputValue;
            }
        }

        /// <summary>
        /// 获取控件常量。
        /// </summary>
        public String ConstVar
        {
            get
            {
                return this.tibConstVar.InputValue;
            }
        }

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Focus();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}
