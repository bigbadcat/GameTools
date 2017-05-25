using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T002.Data;
using System.IO;

namespace T002.Forms
{
    /// <summary>
    /// 界面属性编辑窗口。
    /// </summary>
    public partial class InterfacePropertyForm : Form
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public InterfacePropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置编辑的文件。
        /// </summary>
        public InterfaceFile EditFile
        {
            set
            {
                this.tibName.InputValue = value.FileName.Substring(ProjectManager.Project.InterfaceRootFolder.Length + 1);
                this.nibCode.InputValue = value.Interface.Code;
                this.nibWidth.InputValue = value.Width;
                this.nibHeight.InputValue = value.Height;
            }
        }

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            int newid = (Int32)this.nibCode.InputValue;
            Int32 width = (Int32)this.nibWidth.InputValue;
            Int32 height = (Int32)this.nibHeight.InputValue;
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).SetInterfaceProperty(newid, width, height);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
