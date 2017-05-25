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
    /// 新建界面窗体。
    /// </summary>
    public partial class NewInterfaceFileForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public NewInterfaceFileForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置要创建界面所在的目录。
        /// </summary>
        public String CreateFolder
        {
            get
            {
                return m_strCreateFolder;
            }
            set
            {
                m_strCreateFolder = value;
            }
        }

        /// <summary>
        /// 获取界面名称。
        /// </summary>
        public String InterfaceName
        {
            get
            {
                return m_strInterfaceName;
            }
        }

        /// <summary>
        /// 获取界面编号。
        /// </summary>
        public int InterfaceCode
        {
            get
            {
                return this.m_iInterfaceCode;
            }
        }

        /// <summary>
        /// 获取界面的宽度。
        /// </summary>
        public Int32 InterfaceWidth
        {
            get
            {
                return m_iInterfaceWidth;
            }
        }

        /// <summary>
        /// 获取界面的高度。
        /// </summary>
        public Int32 InterfaceHeight
{
            get
            {
                return m_iInterfaceHeight;
            }
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 上次创建的界面宽度。
        /// </summary>
        private static Int32 LastWidth = 800;

        /// <summary>
        /// 上次创建的界面高度。
        /// </summary>
        private static Int32 LastHeight = 480;

        /// <summary>
        /// 要创建界面所在的目录。
        /// </summary>
        private String m_strCreateFolder = String.Empty;

        /// <summary>
        /// 界面名称。
        /// </summary>
        private String m_strInterfaceName = String.Empty;

        /// <summary>
        /// 界面编号。
        /// </summary>
        private int m_iInterfaceCode = 0;

        /// <summary>
        /// 界面的宽度。
        /// </summary>
        private Int32 m_iInterfaceWidth = 1;

        /// <summary>
        /// 界面的高度。
        /// </summary>
        private Int32 m_iInterfaceHeight = 1;
        
        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void NewInterfaceFileForm_Load(object sender, EventArgs e)
        {
            this.nibWidth.InputValue = LastWidth;
            this.nibHeight.InputValue = LastHeight;
        }

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.m_strInterfaceName = this.tibName.InputValue.Trim();
            if (this.m_strInterfaceName == String.Empty)
            {
                MessageBox.Show("请输入界面名称。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            String strFileName = this.m_strCreateFolder + "\\" + this.m_strInterfaceName + ProjectManager.NAME_EXT_INTERFACE_EDIT;
            if (File.Exists(strFileName))
            {

                MessageBox.Show("\"" + this.m_strInterfaceName + "\"已存在。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //获取界面的宽度和高度
            this.m_iInterfaceCode = (Int32)this.nibCode.InputValue;
            this.m_iInterfaceWidth = (Int32)this.nibWidth.InputValue;
            this.m_iInterfaceHeight = (Int32)this.nibHeight.InputValue;
            LastWidth = this.m_iInterfaceWidth;
            LastHeight = this.m_iInterfaceHeight;

            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
