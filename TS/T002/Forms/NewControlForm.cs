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
    /// 添加控件窗体。
    /// </summary>
    public partial class NewControlForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public NewControlForm()
        {
            InitializeComponent();
            this.iibType.InputIndex = 0;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取控件类型。
        /// </summary>
        public Int32 ControlType
        {
            get
            {
                return this.iibType.InputIndex;
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

        #region 控件类型=====================================================================================

        /// <summary>
        /// 控件类型：容器。
        /// </summary>
        public const Int32 CONTROL_TYPE_CONTAINER = 0;

        /// <summary>
        /// 控件类型：图像。
        /// </summary>
        public const Int32 CONTROL_TYPE_PICTURE = 1;

        /// <summary>
        /// 控件类型：标签。
        /// </summary>
        public const Int32 CONTROL_TYPE_LABEL = 2;

        /// <summary>
        /// 控件类型：文本域。
        /// </summary>
        public const Int32 CONTROL_TYPE_TEXTAREA = 3;

        /// <summary>
        /// 控件类型：文本框。
        /// </summary>
        public const Int32 CONTROL_TYPE_TEXTBOX = 4;

        /// <summary>
        /// 控件类型：进度条。
        /// </summary>
        public const Int32 CONTROL_TYPE_PROGRESSBAR = 5;

        /// <summary>
        /// 控件类型：滑动条。
        /// </summary>
        public const Int32 CONTROL_TYPE_SLIDERBAR = 6;

        /// <summary>
        /// 控件类型：普通按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_SINGLEBUTTON = 7;

        /// <summary>
        /// 控件类型：单选按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_RADIOBUTTON = 8;

        /// <summary>
        /// 控件类型：复选按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_CHECKBUTTON = 9;

        /// <summary>
        /// 控件类型：数字图像。
        /// </summary>
        public const Int32 CONTROL_TYPE_NUMBERIMAGE = 10;

        /// <summary>
        /// 控件类型：图像数字。
        /// </summary>
        public const Int32 CONTROL_TYPE_IMAGENUMBER = 11;

        /// <summary>
        /// 控件类型：滑动表格。
        /// </summary>
        public const Int32 CONTROL_TYPE_SCROLLTABLE = 12;

        /// <summary>
        /// 控件类型：翻页表格。
        /// </summary>
        public const Int32 CONTROL_TYPE_PAGETABLE = 13;

        /// <summary>
        /// 控件类型：滚动面板。
        /// </summary>
        public const Int32 CONTROL_TYPE_SCROLLPANEL = 14;

        /// <summary>
        /// 控件类型：粒子视图。
        /// </summary>
        public const Int32 CONTROL_TYPE_PARTICLEVIEW = 15;

        /// <summary>
        /// 控件类型：Spine动画视图。
        /// </summary>
        public const Int32 CONTROL_TYPE_SPINEVIEW = 16;

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 点击确定按钮
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Focus();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}
