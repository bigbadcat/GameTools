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

namespace T006.Forms
{
    /// <summary>
    /// 选择背景图窗口。
    /// </summary>
    public partial class BackImageForm : Form
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public BackImageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取选择的文件名。
        /// </summary>
        public String ImageName
        {
            get
            {
                return m_strSelectName;
            }
        }

        /// <summary>
        /// 选择的背景图像名称。
        /// </summary>
        private String m_strSelectName = String.Empty;

        /// <summary>
        /// 窗口初始化。
        /// </summary>
        private void BackImageForm_Load(object sender, EventArgs e)
        {
            //获取该目录下的文件和文件夹信息
            DirectoryInfo diFileFolder = new DirectoryInfo(ProjectManager.Project.BackFolder);

            //先加载文件信息
            FileInfo[] fiaFiles = diFileFolder.GetFiles();
            foreach (FileInfo fiTemp in fiaFiles)
            {
                String exname = fiTemp.Extension.ToLower();
                if (exname.Equals(".jpg") || exname.Equals(".jpeg") || exname.Equals(".png") || exname.Equals(".bmp"))
                {
                    String name = fiTemp.FullName.Substring(ProjectManager.Project.BackFolder.Length + 1);
                    this.lvBackList.Items.Add(name);
                }
            }
        }

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 选择背景图。
        /// </summary>
        private void lvBackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvBackList.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = this.lvBackList.SelectedItems[0];
            m_strSelectName = item.Text;

            String file = ProjectManager.Project.BackFolder + "\\" + m_strSelectName;
            Bitmap bmp = new Bitmap(file);
            Int32 swidth = 0;           //显示的宽度
            Int32 sheight = 0;          //显示的高度
            if (bmp.Width > 400 || bmp.Height > 300)
            {
                Single scale = Math.Min(400.0f / bmp.Width, 300.0f / bmp.Height);
                swidth = (Int32)(bmp.Width * scale);
                sheight = (Int32)(bmp.Height * scale);
            }
            else
            {
                swidth = bmp.Width;
                sheight = bmp.Height;
            }

            Bitmap showbmp = new Bitmap(400, 300);
            Graphics g = Graphics.FromImage(showbmp);
            Rectangle src = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle dst = new Rectangle((400 - swidth) / 2, (300 - sheight) / 2, swidth, sheight);
            g.DrawImage(bmp, dst, src, GraphicsUnit.Pixel);
            this.pbBack.Image = showbmp;
            this.pbBack.Refresh();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            m_strSelectName = String.Empty;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
