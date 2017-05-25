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
    /// 粒子属性窗体。
    /// </summary>
    public partial class ParticlePropertyForm : Form
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ParticlePropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置编辑的文件。
        /// </summary>
        public ParticleFile EditFile
        {
            set
            {
                this.m_pfEdit = value;
                this.tibFileName.InputValue = m_pfEdit.FileName.Substring(ProjectManager.Project.ParticleRootFolder.Length + 1);
                this.tibOutCode.InputValue = m_pfEdit.EditParticle.ID;
            }
        }

        /// <summary>
        /// 编辑的文件。
        /// </summary>
        private ParticleFile m_pfEdit = null;

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            String newid = this.tibOutCode.InputValue.Trim();
            if (newid.Equals(String.Empty))
            {
                MessageBox.Show("请输入粒子编号。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!this.m_pfEdit.EditParticle.ID.Equals(newid))
            {
                (MainForm.AppMainForm.EditFileForm as ParticleFileForm).SetParticleProperty(newid);
            }
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
