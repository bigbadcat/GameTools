using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using T002.Data;

namespace T002.Forms
{
    /// <summary>
    /// 文件窗口，显示一个打开编辑的文件。
    /// </summary>
    public partial class ResourceFileForm : DockContent
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResourceFileForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置编辑文件的绝对路径。(用于文件名发生改变)
        /// </summary>
        /// <param name="strFileName">文件路径。</param>
        public void SetFileName(String strFileName)
        {
            this.m_rfEditFile.FileName = strFileName;
            this.Text = Path.GetFileNameWithoutExtension(strFileName) + (this.m_rfEditFile.Amend ? "*" : String.Empty);
        }

        /// <summary>
        /// 虚函数。窗体进行删除操作。
        /// </summary>
        public virtual void DoDelete()
        {
        }

        /// <summary>
        /// 虚函数。窗体进行保存文件操作。
        /// </summary>
        public virtual void DoSaveFile()
        {
            if (this.m_rfEditFile.Amend)
            {
                this.m_rfEditFile.SaveFile();
                this.Text = Path.GetFileNameWithoutExtension(this.m_rfEditFile.FileName);
            }
        }

        /// <summary>
        /// 进行撤销操作。
        /// </summary>
        public virtual void DoRevoke()
        {
            this.m_rfEditFile.DoRevoke();
        }

        /// <summary>
        /// 进行重做操作。
        /// </summary>
        public virtual void DoRedo()
        {
            this.m_rfEditFile.DoRedo();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置文件对象。
        /// </summary>
        public ResourceFile EditFile
        {
            get
            {
                return this.m_rfEditFile;
            }
            set
            {
                this.m_rfEditFile = value;
                if (this.m_rfEditFile != null)
                {
                    this.HaveEditFile();
                }
                else
                {
                    this.ClearEditFile();
                }
            }
        }

        /// <summary>
        /// 获取壶设置窗体对应的地图文件是否需要保存。
        /// </summary>
        public Boolean NeedSave
        {
            get
            {
                return this.m_bNeedSave;
            }
            set
            {
                this.m_bNeedSave = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 得到了编辑文件。
        /// </summary>
        protected virtual void HaveEditFile()
        {
            this.Text = Path.GetFileNameWithoutExtension(this.m_rfEditFile.FileName) + (this.m_rfEditFile.Amend ? "*" : String.Empty);
            this.m_rfEditFile.FileAmend += new EventHandler(EditFile_FileAmend);
        }

        /// <summary>
        /// 清掉了编辑文件。
        /// </summary>
        protected virtual void ClearEditFile()
        {
            this.Text = String.Empty;
        }

        /// <summary>
        /// 文件发生修改。
        /// </summary>
        protected void EditFile_FileAmend(object sender, EventArgs e)
        {
            this.Text = Path.GetFileNameWithoutExtension(this.m_rfEditFile.FileName) + " *";
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 编辑的文件对象。
        /// </summary>
        protected ResourceFile m_rfEditFile = null;

        /// <summary>
        /// 标志窗体对应的文件是否需要保存。某些情况下不需要确认保存就直接关闭文件
        /// </summary>
        protected Boolean m_bNeedSave = true;

        #endregion
    }
}
