using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T007
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitColorTable();
            this.rbZoom4.Checked = true;
        }

        /// <summary>
        /// 初始化颜色表。
        /// </summary>
        public void InitColorTable()
        {
            String exe = Application.ExecutablePath;
            String file = exe.Substring(0, exe.LastIndexOf('\\') + 1) + "colortable.txt";
            Stream s = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(s);

            //循环读入颜色表
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                
                string[] data = line.Split('=');
                string[] colordata = data[1].Split(',');
                int index = Int32.Parse(data[0]);
                int r = Int32.Parse(colordata[0]);
                int g = Int32.Parse(colordata[1]);
                int b = Int32.Parse(colordata[2]);
                Color c = Color.FromArgb(r, g, b);
                m_dicColorTable.Add(index, c);
            }

            sr.Close();
            sr.Dispose();
            sr = null;
            s.Dispose();
            s = null;
        }

        /// <summary>
        /// 读入颜色索引。
        /// </summary>
        public void ReadColorIndex()
        {
            Stream s = new FileStream(m_strMapFile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(s);

            //1024行 每行16个索引
            for (int i = 0; i < 1024; ++i)
            {
                String line = sr.ReadLine();
                string[] indexstr = line.Split(' ');
                for (int j = 0; j < 16; ++j)
                {
                    int index = Int32.Parse(indexstr[j]);
                    int n = i * 16 + j;
                    int r = n / MAP_WIDTH;
                    int c = n % MAP_HEIGHT;
                    m_aColorIndex[r, c] = index;
                }
            }
            sr.Close();
            sr.Dispose();
            sr = null;
            s.Dispose();
            s = null;
        }

        /// <summary>
        /// 渲染图像。
        /// </summary>
        public void RenderImage()
        {
            //初始化图片像素
            StringBuilder unkowncolor = new StringBuilder();
            int undownnumber = 0;
            for (int i = 0; i < MAP_WIDTH; ++i)
            {
                for (int j = 0; j < MAP_HEIGHT; ++j)
                {
                    int index = m_aColorIndex[i, j];
                    if (m_dicColorTable.ContainsKey(index))
                    {
                        Color c = m_dicColorTable[index];
                        m_bmpImage.SetPixel(j, i, c);   //x代表列 y代表行
                    }
                    else
                    {
                        string str = String.Format("Unkown color index! x:{0} y:{1} i:{2}", j, i, index);
                        unkowncolor.AppendLine(str);
                        ++undownnumber;
                    }
                }
            }

            if (undownnumber > 0)
            {
                string str = String.Format("Unkown color number:{0}", undownnumber);
                unkowncolor.AppendLine(str);
                Clipboard.SetText(unkowncolor.ToString());
                MessageBox.Show("出现未知颜色索引，详细信息请查看剪切板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 显示图像。
        /// </summary>
        public void ShowImage()
        {
            //按比例渲染出来
            Image img = pbImage.Image;
            if (img != null)
            {
                img.Dispose();
                img = null;
            }

            img = new Bitmap(MAP_WIDTH * m_iZoom, MAP_HEIGHT * m_iZoom);
            Graphics g = Graphics.FromImage(img);
            Rectangle rt = new Rectangle(0, 0, img.Width, img.Height);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage(m_bmpImage, rt);
            pbImage.Image = img;
        }

        /// <summary>
        /// 地图宽度。
        /// </summary>
        private const int MAP_WIDTH = 128;

        /// <summary>
        /// 地图高度。
        /// </summary>
        private const int MAP_HEIGHT = 128;

        /// <summary>
        /// 地图文件。
        /// </summary>
        private String m_strMapFile = String.Empty;

        /// <summary>
        /// 颜色索引数据。
        /// </summary>
        private int[,] m_aColorIndex = new int[MAP_WIDTH, MAP_HEIGHT];

        /// <summary>
        /// 颜色表。
        /// </summary>
        private Dictionary<int, Color> m_dicColorTable = new Dictionary<int,Color>();

        /// <summary>
        /// 渲染出来的图像。
        /// </summary>
        private Bitmap m_bmpImage = new Bitmap(MAP_WIDTH, MAP_HEIGHT);

        /// <summary>
        /// 缩放大小。
        /// </summary>
        private int m_iZoom = 1;

        private void btnRender_Click(object sender, EventArgs e)
        {
            String file = fibFile.InputValue;
            if (file.Equals(String.Empty))
            {
                MessageBox.Show("请选择地图数据文件。");
                return;
            }
            if (!File.Exists(file))
            {
                MessageBox.Show("地图文件不存在。\n"+file);
                return;
            }
            m_strMapFile = file;
            ReadColorIndex();
            RenderImage();
            ShowImage();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (m_strMapFile.Equals(String.Empty))
            {
                MessageBox.Show("请先渲染地图文件。");
                return;
            }

            FileDialog dlg = new SaveFileDialog();
            dlg.Filter = "图像文件 (*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                String file = dlg.FileName;
                m_bmpImage.Save(file, ImageFormat.Png);
            }
        }

        private void rbZoom1_CheckedChanged(object sender, EventArgs e)
        {
            m_iZoom = 1;
            ShowImage();
        }

        private void rbZoom2_CheckedChanged(object sender, EventArgs e)
        {
            m_iZoom = 2;
            ShowImage();
        }

        private void rbZoom4_CheckedChanged(object sender, EventArgs e)
        {
            m_iZoom = 4;
            ShowImage();
        }
    }
}
