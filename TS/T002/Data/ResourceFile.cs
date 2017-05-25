using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace T002.Data
{
    /// <summary>
    /// 打开的资源文件。
    /// </summary>
    public abstract class ResourceFile
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 将文件保存。
        /// </summary>
        public void SaveFile()
        {
            this.SaveFileToDisk();
        }

        /// <summary>
        /// 将文件关闭。
        /// </summary>
        public void CloseFile()
        {
            this.DestroyFile();
        }

        /// <summary>
        /// 资源文件的编辑进行撤销操作。
        /// </summary>
        public virtual void DoRevoke()
        {
        }

        /// <summary>
        /// 资源文件的编辑进行重做操作。
        /// </summary>
        public virtual void DoRedo()
        {
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置文件的绝对路径。
        /// </summary>
        public String FileName
        {
            get
            {
                return this.m_strFileName;
            }
            set
            {
                this.m_strFileName = value;
            }
        }

        /// <summary>
        /// 获取文件的资源类型。
        /// </summary>
        public Int32 ResourceType
        {
            get
            {
                return this.m_iResourceType;
            }
        }

        /// <summary>
        /// 获取文件显示的缓冲。
        /// </summary>
        public Bitmap DisplayBuffer
        {
            get
            {
                return this.m_bmpDisplayBuffer;
            }
        }

        /// <summary>
        /// 获取文件是否修改。
        /// </summary>
        public Boolean Amend
        {
            get
            {
                return this.m_bAmend;
            }
        }

        #endregion

        #region 对外事件=====================================================================================

        /// <summary>
        /// 文件发生修改事件。
        /// </summary>
        public event EventHandler FileAmend;

        /// <summary>
        /// 用于引发FileAmend事件
        /// </summary>
        protected void OnFileAmend(EventArgs e)
        {
            if (this.FileAmend != null)
            {
                this.FileAmend(this, e);
            }
        }

        /// <summary>
        /// 文件内容发生改变。
        /// </summary>
        public event EventHandler FileContentChange;

        /// <summary>
        /// 用于引发FileContentChange事件
        /// </summary>
        protected void OnFileContentChange(EventArgs e)
        {
            if (this.FileContentChange != null)
            {
                this.FileContentChange(this, e);
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 构造函数。内部操作使用。
        /// </summary>
        /// <param name="strFileName">资源文件路径。</param>
        /// <param name="iType">资源类型。</param>
        protected ResourceFile(String strFileName, Int32 iType)
        {
            this.m_strFileName = strFileName;
            this.m_iResourceType = iType;
            this.m_rrRevokeRedoOperate = new RevokeRedoManager();
        }

        /// <summary>
        /// 创建新的显示缓冲。(原来的将会被Dispose，确保缓冲没有被引用)
        /// </summary>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        protected void RebuildDisplayBuffer(Int32 iWidth, Int32 iHeight)
        {
            if (this.m_bmpDisplayBuffer != null)
            {
                this.m_bmpDisplayBuffer.Dispose();
                this.m_bmpDisplayBuffer = null;
            }
            this.m_bmpDisplayBuffer = new Bitmap(iWidth, iHeight);
        }

        /// <summary>
        /// 销毁文件内存数据。
        /// </summary>
        protected virtual void DestroyFile()
        {
            if (this.m_bmpDisplayBuffer != null)
            {
                this.m_bmpDisplayBuffer.Dispose();
                this.m_bmpDisplayBuffer = null;
            }
        }

        /// <summary>
        /// 保存文件到硬盘。
        /// </summary>
        protected virtual void SaveFileToDisk()
        {
            this.m_bAmend = false;
            this.m_rrRevokeRedoOperate.ClearRevokeRedo();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 资源文件的绝对路径。
        /// </summary>
        protected String m_strFileName = String.Empty;

        /// <summary>
        /// 资源类型。
        /// </summary>
        protected Int32 m_iResourceType = ProjectManager.TYPE_RESOURCE_UNKNOW;

        /// <summary>
        /// 文件显示缓冲。
        /// </summary>
        protected Bitmap m_bmpDisplayBuffer = null;

        /// <summary>
        /// 标志文件最近一次保存后是否修改。
        /// </summary>
        protected Boolean m_bAmend = false;

        /// <summary>
        /// 撤销重做对象。
        /// </summary>
        protected RevokeRedoManager m_rrRevokeRedoOperate = null;

        #endregion
    }
}
