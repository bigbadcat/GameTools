using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace T001
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加密用的钥匙。
        /// </summary>
        public const Byte KEY = 0x58;

        /// <summary>
        /// 要加密的文件扩展名列表。
        /// </summary>
        public List<String> m_lstEncryptionExt = null;

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String path = fbd.SelectedPath;
                if (this.cbFolder.Items.Contains(path))
                {
                    Int32 index = this.cbFolder.Items.IndexOf(path);
                    this.cbFolder.SelectedIndex = index;
                }
                else
                {
                    Int32 index = this.cbFolder.Items.Add(path);
                    this.cbFolder.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// 加密一个文件。
        /// </summary>
        /// <param name="file">文件绝对路径，将会被替换。</param>
        private void EncryptionFile(String file)
        {
            Byte[] data = File.ReadAllBytes(file);
            if (data.Length >= 2)
            {
                data[data.Length - 1] = (Byte)(data[data.Length - 1] ^ KEY);
                for (Int32 i = data.Length - 2; i >= 0; --i)
                {
                    data[i] = (Byte)(data[i] ^ data[i + 1]);
                }
            }

            File.WriteAllBytes(file.Substring(0, file.LastIndexOf('.')), data);
            File.Delete(file);
            this.rtbLog.AppendText(String.Format("加密文件:{0}\n", file));
            this.Refresh();
        }

        /// <summary>
        /// 加密一个文件夹和其子文件夹下的图像文件。
        /// </summary>
        /// <param name="folder">指定的文件夹。</param>
        private void EncryptionFolder(String folder)
        {
            //获取该目录下的文件和文件夹信息
            DirectoryInfo diFileFolder = new DirectoryInfo(folder);

            //先加载文件信息
            FileInfo[] fiaFiles = diFileFolder.GetFiles();
            foreach (FileInfo fiTemp in fiaFiles)
            {
                if (m_lstEncryptionExt.Contains(Path.GetExtension(fiTemp.FullName)))
                {
                    EncryptionFile(fiTemp.FullName);
                }
            }

            //加载文件夹信息
            DirectoryInfo[] diaFolders = diFileFolder.GetDirectories();
            foreach (DirectoryInfo diTemp in diaFolders)
            {
                //递归加载该文件夹
                EncryptionFolder(diTemp.FullName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            String path = this.cbFolder.Text;
            if (Directory.Exists(path))
            {
                String str = String.Format("确定文件已备份，要加密指定文件夹下的所有图像文件吗？\n文件夹:{0}\n加密后的文件将不能再恢复!", path);
                if (MessageBox.Show(str, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    this.rtbLog.AppendText("开始加密文件...\n");
                    EncryptionFolder(path);
                    this.rtbLog.AppendText("加密文件结束。\n");
                }
            }
            else
            {
                MessageBox.Show("文件夹不存在:\n" + path);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_lstEncryptionExt = new List<String>();
            m_lstEncryptionExt.Add(".png");
            m_lstEncryptionExt.Add(".jpg");
            m_lstEncryptionExt.Add(".jpeg");
            m_lstEncryptionExt.Add(".bmp");
        }
    }
}
