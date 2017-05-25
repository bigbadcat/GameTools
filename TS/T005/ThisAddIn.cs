using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace T005
{
    public partial class ThisAddIn
    {
        /// <summary>
        /// 当前插件
        /// </summary>
        public static ThisAddIn TheAddIn = null;

        /// <summary>
        /// 导出的目录1。
        /// </summary>
        public static String ExportFolder1 = String.Empty;

        /// <summary>
        /// 导出的目录2。
        /// </summary>
        public static String ExportFolder2 = String.Empty;

        /// <summary>
        /// 设置导出文件夹。
        /// </summary>
        /// <param name="i">文件夹序号。</param>
        /// <param name="folder">文件夹路径。</param>
        public void SetExportFolder(int i, String folder)
        {
            if (i == 1)
            {
                ExportFolder1 = folder;
            }
            else if (i == 2)
            {
                ExportFolder2 = folder;
            }
        }

        /// <summary>
        /// 获取导出文件夹。
        /// </summary>
        /// <param name="i">文件夹序号。</param>
        /// <returns>文件夹路径。</returns>
        public String GetExportFolder(int i)
        {
            String ret = String.Empty;
            if (i == 1)
            {
                ret = ExportFolder1;
            }
            else if (i == 2)
            {
                ret = ExportFolder2;
            }
            return ret;
        }

        /// <summary>
        /// 读取导出目录
        /// </summary>
        public void ReadExportFolder(int i)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey softwareXXX = key.OpenSubKey(@"Software\T005");
            if (softwareXXX != null)
            {
                object obj = softwareXXX.GetValue("ExportFolder" + i);
                if (obj != null)
                {
                    SetExportFolder(i, obj.ToString());
                }
            }
        }

        /// <summary>
        /// 保存导出目录
        /// </summary>
        public void SaveExportFolder(int i)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey softwareXXX = key.CreateSubKey(@"Software\T005");
            softwareXXX.SetValue("ExportFolder" + i, GetExportFolder(i));
        }

        /// <summary>
        /// 进行设置文件夹操作
        /// </summary>
        public bool DoSetExportFolder(int i)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SetExportFolder(i, dlg.SelectedPath);
                SaveExportFolder(i);
                return true;
            }
            return false;
        }

        //进行文件导出操作
        public void DoExportFile(int i)
        {
            //判断是否有输出路径
            if (GetExportFolder(i).Equals(String.Empty))
            {
                MessageBox.Show("请先设置要导出的目录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!DoSetExportFolder(i))
                {
                    return;
                }
            }

            //判断输出目录是否存在
            if (!Directory.Exists(GetExportFolder(i)))
            {
                String msg = String.Format("导出目录不存在:\n{0}\n请重新设置要导出的目录。", GetExportFolder(i));
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!DoSetExportFolder(i))
                {
                    return;
                }
            }

            StartExportFile(i);
        }

        /// <summary>
        /// 开始导出文件
        /// </summary>
        private void StartExportFile(int i)
        {
            Excel.Worksheet cursheet = this.Application.ActiveSheet;
            if (cursheet == null)
            {
                return;
            }

            //先删除已经存在的文件，否则会弹出对话框确认
            String name = cursheet.Name;
            String file = GetExportFolder(i) + "\\" + name + ".csv";
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            //复制一份新的工作表
            //**********为什么要复制**********//
            //当工作表保存为CSV时，会将工作表与保存的CSV连接起来
            //将无法对CSV进行写操作，也就无法进行下一步的转码
            cursheet.Copy();

            //重新选择新复制的工作表，并保存为CSV文件
            cursheet = this.Application.ActiveSheet;
            cursheet.SaveAs(file, XlFileFormat.xlCSV);

            //复制新的工作表将会创建新的工作簿，并激活为当前，将当前的工作簿关闭，并放弃保存，断开与CSV文件的连接
            this.Application.ActiveWorkbook.Close(false);

            //保存的CSV文件为ANSI编码，转换为UTF8编码
            StartConvert(file);
        }

        void StartConvert(String file)
        {
            try
            {
                FileStream fread = new FileStream(file, FileMode.Open);
                StreamReader sr = new StreamReader(fread, Encoding.GetEncoding("gb2312"));
                String filestring = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                sr = null;
                fread.Close();
                fread.Dispose();
                fread = null;

                UTF8Encoding utf8 = new UTF8Encoding(true);
                FileStream fwrite = new FileStream(file, FileMode.Create);
                StreamWriter sw = new StreamWriter(fwrite, utf8);
                sw.Write(filestring);
                sw.Flush();
                sw = null;
                fwrite.Close();
                fwrite.Dispose();
                fwrite = null;
            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            TheAddIn = this;
            ReadExportFolder(1);
            ReadExportFolder(2);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }

        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
