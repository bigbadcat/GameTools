using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using T002.Data;

namespace T002.Forms
{
    public partial class NewProjectForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数
        /// </summary>
        public NewProjectForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取工程名称。
        /// </summary>
        public String ProjectName
        {
            get
            {
                return this.tibProjectName.InputValue.Trim();
            }
        }

        /// <summary>
        /// 获取工程路径。
        /// </summary>
        public String ProjectPath
        {
            get
            {
                return this.fibProjectPath.InputValue;
            }
        }

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 按下确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //工程名称合法检查
            if (this.tibProjectName.InputValue.Trim() == String.Empty)
            {
                MessageBox.Show("请输入工程名称。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //工程路径合法检查
            if (this.fibProjectPath.InputValue == String.Empty)
            {
                MessageBox.Show("请选择工程存放的目录。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //判断工程是否已经存在，即判断相的工程文件是否存在。
            StringBuilder sbProject = new StringBuilder(this.ProjectPath);
            sbProject.Append("\\");
            sbProject.Append(this.ProjectName);

            //先判断文件夹是否存在
            if (Directory.Exists(sbProject.ToString()))
            {
                sbProject.Append("\\");
                sbProject.Append(this.ProjectName);
                sbProject.Append(ProjectManager.NAME_EXT_PROJECT);
                if (File.Exists(sbProject.ToString()))
                {
                    MessageBox.Show("工程已存在。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
