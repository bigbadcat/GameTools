using System;
using System.Drawing;
using System.Windows.Forms;

namespace T008
{
    /// <summary>
    /// 枚举查看窗口。
    /// </summary>
    public partial class EnumForm : Form
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public EnumForm()
        {
            InitializeComponent();            
        }

        #endregion

        #region 内部操作=====================================================================================
        
        /// <summary>
        /// 初始化枚举列表。
        /// </summary>
        private void InitEnumList()
        {
            lvEnumList.Items.Clear();
            foreach (var kvp in ConfigArchive.Instance.EnumInfos)
            {
                ListViewItem item = new ListViewItem(kvp.Key);
                lvEnumList.Items.Add(item);
            }
            if (lvEnumList.Items.Count > 0)
            {
                lvEnumList.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// 初始化枚举信息。
        /// </summary>
        private void RefreshEnumInfo()
        {
            //如果没有选择的枚举这显示空内容            
            if (lvEnumList.SelectedItems.Count <= 0)
            {
                tibName.InputValue = string.Empty;
                tibNote.InputValue = string.Empty;
                lvContent.Items.Clear();
                return;
            }

            ListViewItem item = lvEnumList.SelectedItems[0];
            EnumInfo info = ConfigArchive.Instance.GetEnumInfo(item.Text);
            tibName.InputValue = info.Name;
            tibNote.InputValue = info.Note;
            lvContent.Items.Clear();
            foreach (EnumItemInfo iteminfo in info.Items)
            {
                ListViewItem contentitem = new ListViewItem(iteminfo.Name);
                contentitem.SubItems.Add(iteminfo.Value.ToString());
                contentitem.SubItems.Add(iteminfo.Note);
                lvContent.Items.Add(contentitem);
            }
        }

        #endregion

        #region 控件事件=====================================================================================

        /// <summary>
        /// 窗口加载。
        /// </summary>
        private void EnumForm_Load(object sender, EventArgs e)
        {
            splitContainer_Panel1_SizeChanged(null, null);
            splitContainer_Panel2_SizeChanged(null, null);
            InitEnumList();
            RefreshEnumInfo();
        }

        /// <summary>
        /// 左侧容器发生改变。
        /// </summary>
        private void splitContainer_Panel1_SizeChanged(object sender, EventArgs e)
        {
            lvEnumList.Size = new Size(splitContainer.Panel1.Width - 24, splitContainer.Panel1.Height - 24);
            chEnumName.Width = lvEnumList.Width - 8;            
        }

        /// <summary>
        /// 右侧容器发生改变。
        /// </summary>
        private void splitContainer_Panel2_SizeChanged(object sender, EventArgs e)
        {
            int rw = splitContainer.Panel2.Width;
            int rh = splitContainer.Panel2.Height;
            tibName.Width = Math.Min(300, rw - 24);
            tibNote.Width = rw - 24;
            lvContent.Size = new Size(rw - 24, rh - 84);
        }

        /// <summary>
        /// 选择枚举时。
        /// </summary>
        private void lvEnumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEnumInfo();
        }

        #endregion
    }
}
