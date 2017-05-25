using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace T002.Forms
{
    /// <summary>
    /// 工作辅助窗体的基类。
    /// </summary>
    public partial class WorkAssistantForm : DockContent
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public WorkAssistantForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置显示或掩藏。
        /// </summary>
        /// <param name="v">true则显示</param>
        public void SetVisible(Boolean v)
        {
            if (v)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }
    }
}
