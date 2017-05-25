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
    /// 界面列表窗口，显示输出冲突的界面。
    /// </summary>
    public partial class InterfaceListForm : Form
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public InterfaceListForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取输出列表。
        /// </summary>
        /// <returns>输出列表集合。</returns>
        public static SortedList<String, List<String>> GetOutPutList()
        {
            SortedList<String, List<String>> outputlist = new SortedList<String, List<String>>();
            GetFolderOutPutList(ProjectManager.Project.InterfaceRootFolder, outputlist);
            return outputlist;
        }

        /// <summary>
        /// 获取某个文件夹下的文件输出列表。
        /// </summary>
        /// <param name="folder">文件夹路径。</param>
        /// <param name="outputlist">保存列表的集合。</param>
        protected static void GetFolderOutPutList(String folder, SortedList<String, List<String>> outputlist)
        {
            //获取该目录下的文件和文件夹信息
            DirectoryInfo diFileFolder = new DirectoryInfo(folder);

            //先加载文件信息
            FileInfo[] fiaFiles = diFileFolder.GetFiles();
            foreach (FileInfo fiTemp in fiaFiles)
            {
                InterfaceFile file = InterfaceFile.LoadFromFile(fiTemp.FullName);
                if (file != null)
                {
                    String output = file.Interface.Code.ToString();
                    String path = fiTemp.FullName.Substring(ProjectManager.Project.InterfaceRootFolder.Length + 1);
                    if (outputlist.ContainsKey(output))
                    {
                        outputlist[output].Add(path);
                    }
                    else
                    {
                        List<String> pathlist = new List<String>();
                        pathlist.Add(path);
                        outputlist.Add(output, pathlist);
                    }
                }
            }

            //加载文件夹信息
            DirectoryInfo[] diaFolders = diFileFolder.GetDirectories();
            foreach (DirectoryInfo diTemp in diaFolders)
            {
                //递归加载该文件夹
                GetFolderOutPutList(diTemp.FullName, outputlist);
            }
        }

        /// <summary>
        /// 初始化输出列表显示。
        /// </summary>
        /// <param name="outputlist">输出列表集合。</param>
        public void InitOutPutList(SortedList<String, List<String>> outputlist)
        {
            foreach (String key in outputlist.Keys)
            {
                List<String> pathlist = outputlist[key];
                foreach (String path in pathlist)
                {
                    ListViewItem lvi = lvOutPutList.Items.Add(key);
                    lvi.SubItems.Add(path);
                    if (pathlist.Count > 1)
                    {
                        lvi.BackColor = Color.FromArgb(255, 150, 150);
                    }
                }
            }
        }

        /// <summary>
        /// 窗体加载。
        /// </summary>
        private void InterfaceListForm_Load(object sender, EventArgs e)
        {
            InitOutPutList(GetOutPutList());
        }
    }
}
