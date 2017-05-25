using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T002.Data;
using T002.Data.UI;
using System.Xml;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Forms
{
    /// <summary>
    /// 界面文件窗口。
    /// </summary>
    public partial class InterfaceFileForm : ResourceFileForm
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public InterfaceFileForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 已重载。界面窗口进行删除操作。
        /// </summary>
        public override void DoDelete()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return;
            }
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            if (ctrSelect.Parent == null)
            {
                MessageBox.Show("Root控件不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ctrSelect.Parent is Table)
            {
                MessageBox.Show("表格的单元控件不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult re = MessageBox.Show("是否删除控件及其子控件？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.m_ifEditFile.DeleteControl(ctrSelect);
                TreeNode tnSelect = this.tvControlStruct.SelectedNode;
                this.tvControlStruct.Focus();
                tnSelect.Parent.Nodes.Remove(tnSelect);
                this.ShowSelectControlMark();
                MainForm.AppMainForm.ControlProperty.EditControl = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            }
        }

        /// <summary>
        /// 将当前选择的控件上移动。
        /// </summary>
        /// <returns>是否可以上移。</returns>
        public Boolean DoMoveUp()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return false;
            }
            TreeNode tnSelect = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnSelect.Tag as T002.Data.UI.Control;
            if (ctrSelect.Parent == null)
            {
                MessageBox.Show("Root控件不能移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (tnSelect.Index == 0)
            {
                return false;
            }

            this.m_ifEditFile.MoveUpControl(ctrSelect);
            Int32 index = tnSelect.Index;
            TreeNode tnParent = tnSelect.Parent;
            tnParent.Nodes.Remove(tnSelect);
            tnParent.Nodes.Insert(index - 1, tnSelect);
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = tnSelect;
            this.ShowSelectControlMark();
            return true;
        }

        /// <summary>
        /// 将当前选择的控件上移动到树控件顶部。
        /// </summary>
        /// <returns>是否可以上移。</returns>
        public Boolean DoMoveTop()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return false;
            }
            TreeNode tnSelect = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnSelect.Tag as T002.Data.UI.Control;
            if (ctrSelect.Parent == null)
            {
                MessageBox.Show("Root控件不能移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (tnSelect.Index == 0)
            {
                return false;
            }

            this.m_ifEditFile.MoveTopControl(ctrSelect);
            Int32 index = tnSelect.Index;
            TreeNode tnParent = tnSelect.Parent;
            tnParent.Nodes.Remove(tnSelect);
            tnParent.Nodes.Insert(0, tnSelect);
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = tnSelect;
            this.ShowSelectControlMark();
            return true;
        }

        /// <summary>
        /// 将当前选择的控件下移动。
        /// </summary>
        /// <returns>是否可以下移。</returns>
        public Boolean DoMoveDown()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return false;
            }
            TreeNode tnSelect = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnSelect.Tag as T002.Data.UI.Control;
            if (ctrSelect.Parent == null)
            {
                MessageBox.Show("Root控件不能移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (tnSelect.Index == tnSelect.Parent.Nodes.Count - 1)
            {
                return false;
            }

            this.m_ifEditFile.MoveDownControl(ctrSelect);
            Int32 index = tnSelect.Index;
            TreeNode tnParent = tnSelect.Parent;
            tnParent.Nodes.Remove(tnSelect);
            tnParent.Nodes.Insert(index + 1, tnSelect);
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = tnSelect;
            this.ShowSelectControlMark();
            return true;
        }

        /// <summary>
        /// 将当前选择的控件下移动到树控件底部。
        /// </summary>
        /// <returns>是否可以下移。</returns>
        public Boolean DoMoveBottom()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return false;
            }
            TreeNode tnSelect = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnSelect.Tag as T002.Data.UI.Control;
            if (ctrSelect.Parent == null)
            {
                MessageBox.Show("Root控件不能移动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (tnSelect.Index == tnSelect.Parent.Nodes.Count - 1)
            {
                return false;
            }

            this.m_ifEditFile.MoveBottomControl(ctrSelect);
            Int32 index = tnSelect.Index;
            TreeNode tnParent = tnSelect.Parent;
            tnParent.Nodes.Remove(tnSelect);
            tnParent.Nodes.Add(tnSelect);
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = tnSelect;
            this.ShowSelectControlMark();
            return true;
        }

        /// <summary>
        /// 更新文件显示。
        /// </summary>
        public void UpdateFileShow()
        {
            this.ivInterfaceContent.UpdataShow();
        }

        /// <summary>
        /// 显示选中控件的选择框。
        /// </summary>
        public void ShowSelectControlMark()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                this.ivInterfaceContent.ClearSelectRect(true);
                return;
            }

            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            Int32 x = ctrSelect.Left;
            Int32 y = ctrSelect.Top;
            T002.Data.UI.Control pc = ctrSelect.Parent;
            while (pc != null)
            {
                x += pc.Left;
                y += pc.Bottom;
                if (pc is Table)
                {
                    //原型显示在左上角
                    Table tb = pc as Table;
                    y += pc.Height - tb.Prototype.Height;
                }
                else if (pc is ScrollPanel)
                {
                    //滑动面板减去偏移
                    Point mv = (pc as ScrollPanel).Move;
                    x -= mv.X;
                    y -= mv.Y;
                }
                pc = pc.Parent;
            }

            y = this.m_ifEditFile.Height - y;
            this.ivInterfaceContent.ShowSelectRect(new Rectangle(x, y, ctrSelect.Width - 1, ctrSelect.Height - 1));
        }

        /// <summary>
        /// 重新设置控件编号。
        /// </summary>
        public void ResetControlCode()
        {
            this.m_ifEditFile.ResetControlCode();
            this.UpdateControlNode(this.tvControlStruct.Nodes[0]);
        }

        /// <summary>
        /// 调整当前控件为合适尺寸。
        /// </summary>
        public void AdjustSuitableSize()
        {
            if (this.tvControlStruct.SelectedNode == null)
            {
                return;
            }
            TreeNode tnSelect = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnSelect.Tag as T002.Data.UI.Control;
            Boolean bCan = false;
            Size newsize = Size.Empty;
            if (ctrSelect is T002.Data.UI.Label)
            {
                newsize = (ctrSelect as T002.Data.UI.Label).GetSuitableSize();
                bCan = true;
            }
            else if (ctrSelect is T002.Data.UI.Button)
            {
                newsize = (ctrSelect as T002.Data.UI.Button).GetSuitableSize();
                bCan = true;
            }
            else if (ctrSelect is T002.Data.UI.Picture)
            {
                newsize = (ctrSelect as T002.Data.UI.Picture).GetSuitableSize();
                bCan = true;
            }
            else if (ctrSelect is T002.Data.UI.SliderBar)
            {
                newsize = (ctrSelect as T002.Data.UI.SliderBar).GetSuitableSize();
                bCan = true;
            }
            else if (ctrSelect is T002.Data.UI.ParticleView)
            {
                newsize = (ctrSelect as T002.Data.UI.ParticleView).GetSuitableSize();
                bCan = true;
            }
            else if (ctrSelect is T002.Data.UI.SpineView)
            {
                newsize = (ctrSelect as T002.Data.UI.SpineView).GetSuitableSize();
                bCan = true;
            }

            if (bCan)
            {
                this.m_ifEditFile.SetControlSize(ctrSelect, newsize);
                this.ShowSelectControlMark();
                MainForm.AppMainForm.ControlProperty.UpdateProperty();
            }
        }

        /// <summary>
        /// 设置界面属性。
        /// </summary>
        /// <param name="code">输出编号。</param>
        /// <param name="width">界面宽度。</param>
        /// <param name="height">界面高度。</param>
        public void SetInterfaceProperty(int code, Int32 width, Int32 height)
        {
            this.m_ifEditFile.SetInterfaceProperty(code, width, height);
            this.ivInterfaceContent.DisplayImage = this.m_ifEditFile.DisplayBuffer;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取编辑中的控件。
        /// </summary>
        public T002.Data.UI.Control EditControl
        {
            get
            {
                TreeNode tnNode = this.tvControlStruct.SelectedNode;
                return tnNode == null ? null : tnNode.Tag as T002.Data.UI.Control;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 得到了贴图编辑文件。
        /// </summary>
        protected override void HaveEditFile()
        {
            base.HaveEditFile();
            this.m_ifEditFile = this.m_rfEditFile as InterfaceFile;
            this.ivInterfaceContent.DisplayImage = this.m_ifEditFile.DisplayBuffer;

            //创建控件结构
            T002.Data.UI.Container root = this.m_ifEditFile.Interface.Root;
            TreeNode tnRoot = this.tvControlStruct.Nodes.Add(root.GetNodeName());
            InitControlNode(tnRoot, root);
            tnRoot.Expand();
        }

        /// <summary>
        /// 初始化控件节点。
        /// </summary>
        /// <param name="tnNode">控件节点。</param>
        /// <param name="con">控件对象。</param>
        protected void InitControlNode(TreeNode tnNode, T002.Data.UI.Control ctr)
        {
            tnNode.Tag = ctr;
            tnNode.ContextMenuStrip = this.cmsNode;
            if (ctr is T002.Data.UI.Container)
            {
                T002.Data.UI.Container con = ctr as T002.Data.UI.Container;
                foreach (T002.Data.UI.Control c in con.ChildList)
                {
                    TreeNode tnCon = tnNode.Nodes.Add(c.GetNodeName());
                    InitControlNode(tnCon, c);
                }
            }
            else if (ctr is Table)
            {
                Table tb = ctr as Table;
                TreeNode tnPro = tnNode.Nodes.Add(tb.Prototype.GetNodeName());
                InitControlNode(tnPro, tb.Prototype);
            }
            else if (ctr is ScrollPanel)
            {
                ScrollPanel sp = ctr as ScrollPanel;
                TreeNode tnChild = tnNode.Nodes.Add(sp.Child.GetNodeName());
                InitControlNode(tnChild, sp.Child);
            }
        }

        /// <summary>
        /// 清掉了编辑文件。
        /// </summary>
        protected override void ClearEditFile()
        {
            base.ClearEditFile();
            this.ivInterfaceContent.DisplayImage = null;
            this.m_ifEditFile = null;
        }

        /// <summary>
        /// 改变控件可见性。
        /// </summary>
        protected void ChangeControlVisible()
        {
            T002.Data.UI.Control ctr = this.EditControl;
            if (ctr != null)
            {
                this.m_ifEditFile.SetControlVisible(ctr, !ctr.Visible);
                this.ivInterfaceContent.UpdataShow();
                MainForm.AppMainForm.ControlProperty.UpdateProperty();
            }            
        }

        /// <summary>
        /// 移动控件位置。
        /// </summary>
        /// <param name="x">水平移动量。</param>
        /// <param name="y">竖直移动量。</param>
        /// <returns>是否可以移动控件。</returns>
        protected Boolean MoveControlPosition(Int32 x, Int32 y)
        {
            T002.Data.UI.Control ctr = this.EditControl;
            if (ctr.Parent == null || ctr.Parent is Table || ctr.Parent is ScrollPanel)
            {
                return false;
            }

            Point newpos = new Point(ctr.X + x, ctr.Y + y);
            this.m_ifEditFile.SetControlPosition(ctr, newpos);
            this.ivInterfaceContent.UpdataShow();
            this.ShowSelectControlMark();
            MainForm.AppMainForm.ControlProperty.UpdateProperty();
            return true;
        }

        /// <summary>
        /// 调整控件尺寸。
        /// </summary>
        /// <param name="w">宽度调整量。</param>
        /// <param name="h">高度调整量。</param>
        /// <returns>是否可以移动控件尺寸。</returns>
        protected Boolean ChangeControlSize(Int32 w, Int32 h)
        {
            T002.Data.UI.Control ctr = this.EditControl;
            if (ctr.Parent == null || ctr.Parent is PageTable)
            {
                return false;
            }

            Size newsz = new Size(Math.Max(0, ctr.Width + w), Math.Max(0, ctr.Height + h));
            this.m_ifEditFile.SetControlSize(ctr, newsz);
            this.ivInterfaceContent.UpdataShow();
            this.ShowSelectControlMark();
            MainForm.AppMainForm.ControlProperty.UpdateProperty();
            return true;
        }

        /// <summary>
        /// 更新控件节点及其子节点的信息。
        /// </summary>
        /// <param name="node">要更新的节点。</param>
        protected void UpdateControlNode(TreeNode node)
        {
            node.Text = (node.Tag as T002.Data.UI.Control).GetNodeName();
            foreach (TreeNode temp in node.Nodes)
            {
                this.UpdateControlNode(temp);
            }
        }

        /// <summary>
        /// 获取新建控件。
        /// </summary>
        /// <param name="parent">控件所在的夫控件。</param>
        /// <param name="type">控件类型。</param>
        /// <returns>新建的控件，不能创建对应控件返回null。</returns>
        protected T002.Data.UI.Control GetNewControl(T002.Data.UI.Control parent, Int32 type)
        {
            T002.Data.UI.Control con = null;
            switch (type)
            {
                case NewControlForm.CONTROL_TYPE_CONTAINER:
                    con = new Data.UI.Container(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_PICTURE:
                    con = new Data.UI.Picture(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_LABEL:
                    con = new Data.UI.Label(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_SINGLEBUTTON:
                    con = new Data.UI.SingleButton(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_SCROLLTABLE:
                    {
                        T002.Data.UI.Control par = parent;
                        Boolean can = true;
                        while (par != null)
                        {
                            if (par is Table)
                            {
                                MessageBox.Show("表格内不能嵌套表格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            else if (par is ScrollPanel)
                            {
                                MessageBox.Show("滚动面板内不能嵌套表格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            par = par.Parent;
                        }
                        if (can)
                        {
                            T002.Data.UI.Container pro = new T002.Data.UI.Container(this.m_ifEditFile.Interface);
                            pro.Name = "表格单元";
                            pro.Code = this.m_ifEditFile.GetMaxCode();
                            pro.Bounds = new Rect(0, 0, parent.Width / 4, parent.Height / 4);
                            con = new Data.UI.ScrollTable(this.m_ifEditFile.Interface, pro);
                        }
                        break;
                    }
                case NewControlForm.CONTROL_TYPE_RADIOBUTTON:
                    con = new Data.UI.RadioButton(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_CHECKBUTTON:
                    con = new Data.UI.CheckButton(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_PAGETABLE:
                    {
                        T002.Data.UI.Control par = parent;
                        Boolean can = true;
                        while (par != null)
                        {
                            if (par is Table)
                            {
                                MessageBox.Show("表格内不能嵌套表格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            else if (par is ScrollPanel)
                            {
                                MessageBox.Show("滚动面板内不能嵌套表格", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            par = par.Parent;
                        }
                        if (can)
                        {
                            T002.Data.UI.Container pro = new T002.Data.UI.Container(this.m_ifEditFile.Interface);
                            pro.Name = "表格单元";
                            pro.Code = this.m_ifEditFile.GetMaxCode();
                            pro.Bounds = new Rect(0, 0, parent.Width / 4, parent.Height / 4);
                            con = new Data.UI.PageTable(this.m_ifEditFile.Interface, pro);
                        }
                        break;
                    }
                case NewControlForm.CONTROL_TYPE_SCROLLPANEL:
                    {
                        T002.Data.UI.Control par = parent;
                        Boolean can = true;
                        while (par != null)
                        {
                            if (par is Table)
                            {
                                MessageBox.Show("表格内不能嵌套滚动面板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            else if (par is ScrollPanel)
                            {
                                MessageBox.Show("滚动面板内不能嵌套滚动面板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                can = false;
                                break;
                            }
                            par = par.Parent;
                        }
                        if (can)
                        {
                            T002.Data.UI.Container child = new T002.Data.UI.Container(this.m_ifEditFile.Interface);
                            child.Name = "滚动子项";
                            child.Code = this.m_ifEditFile.GetMaxCode();
                            child.Bounds = new Rect(0, 0, parent.Width / 4, parent.Height / 4);
                            con = new Data.UI.ScrollPanel(this.m_ifEditFile.Interface, child);
                        }
                        break;
                    }
                case NewControlForm.CONTROL_TYPE_NUMBERIMAGE:
                    con = new Data.UI.NumberImage(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_TEXTAREA:
                    con = new Data.UI.TextArea(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_IMAGENUMBER:
                    con = new Data.UI.ImageNumber(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_TEXTBOX:
                    con = new Data.UI.TextBox(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_PROGRESSBAR:
                    con = new Data.UI.ProgressBar(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_SLIDERBAR:
                    con = new Data.UI.SliderBar(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_PARTICLEVIEW:
                    con = new Data.UI.ParticleView(this.m_ifEditFile.Interface);
                    break;
                case NewControlForm.CONTROL_TYPE_SPINEVIEW:
                    con = new Data.UI.SpineView(this.m_ifEditFile.Interface);
                    break;
                default:
                    break;
            }

            //设置控件属性
            if (con != null)
            {
                con.Code = this.m_ifEditFile.GetMaxCode();
                con.Bounds = new Rect(parent.Width / 4, parent.Height / 4, parent.Width / 2, parent.Height / 2);
                con.BackColor = Color.FromArgb(100, 0, 0, 0);
            }
            return con;
        }

        /// <summary>
        /// 设置新建控件的节点。
        /// </summary>
        /// <param name="node">新建的控件节点。</param>
        /// <param name="ctr">新建的控件</param>
        protected void SetNewControlNode(TreeNode node, T002.Data.UI.Control ctr)
        {
            node.Tag = ctr;
            if (ctr is Table)         //如果是表格把原型也显示出来
            {
                T002.Data.UI.Container pro = (ctr as Table).Prototype;
                TreeNode tnPro = node.Nodes.Insert(0, pro.GetNodeName());
                tnPro.Tag = pro;
                tnPro.ContextMenuStrip = this.cmsNode;
                node.Expand();
            }
            else if (ctr is ScrollPanel)         //如果是滚动面板把子项也显示出来
            {
                T002.Data.UI.Container child = (ctr as ScrollPanel).Child;
                TreeNode tnPro = node.Nodes.Insert(0, child.GetNodeName());
                tnPro.Tag = child;
                tnPro.ContextMenuStrip = this.cmsNode;
                node.Expand();
            }
            node.ContextMenuStrip = this.cmsNode;
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = node;
            this.ShowSelectControlMark();
            MainForm.AppMainForm.ControlProperty.EditControl = ctr;
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 界面编辑文件。
        /// </summary>
        private InterfaceFile m_ifEditFile = null;

        /// <summary>
        /// 控件XML文本到剪切板里的标记。
        /// </summary>
        private const String CLIPBOARD_CONTROL_MARK = "ControlCutXXSP";

        #endregion

        #region 事件函数=====================================================================================

        #region 窗体事件=====================================================================================

        /// <summary>
        /// 窗体加载。
        /// </summary>
        private void InterfaceFileForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 窗体尺寸发生改变
        /// </summary>
        private void InterfaceFileForm_Resize(object sender, EventArgs e)
        {
            Rectangle rtClient = this.ClientRectangle;
            this.splitContainer.Width = rtClient.Width - 8;
            this.splitContainer.Height = rtClient.Height - 8;
        }

        /// <summary>
        /// 鼠标进入视图。
        /// </summary>
        private void ivInterfaceContent_ViewMouseEnter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 鼠标在视图上按下。
        /// </summary>
        private void ivInterfaceContent_ViewMouseDown(object sender, MouseEventArgs e)
        {
            //找出选中控件的路径
            Point pos = new Point(e.X, this.m_ifEditFile.Height - e.Y);
            T002.Data.UI.Control ctrSelect = e.Button == MouseButtons.Left ? this.m_ifEditFile.GetControlAtPoint(pos) : null;
            List<T002.Data.UI.Control> path = new List<Data.UI.Control>();
            T002.Data.UI.Control parent = ctrSelect;
            while (parent != null)
            {
                path.Insert(0, parent);
                parent = parent.Parent;
            }

            //根据路径查找对应控件节点
            TreeNodeCollection tnNodes = this.tvControlStruct.Nodes;
            TreeNode tnNode = null;
            foreach (T002.Data.UI.Control con in path)
            {
                foreach (TreeNode tmp in tnNodes)
                {
                    if (tmp.Tag == con)
                    {
                        tnNode = tmp;
                        break;
                    }
                }
                tnNodes = tnNode.Nodes;
            }
            MainForm.AppMainForm.ControlProperty.EditControl = ctrSelect;
            this.tvControlStruct.Focus();
            this.tvControlStruct.SelectedNode = tnNode;
            this.ShowSelectControlMark();
        }

        /// <summary>
        /// 鼠标在视图上移动。
        /// </summary>
        private void ivInterfaceContent_ViewMouseMove(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标在视图上弹起。
        /// </summary>
        private void ivInterfaceContent_ViewMouseUp(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标离开视图。
        /// </summary>
        private void ivInterfaceContent_ViewMouseLeave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 工程资源树结构视图鼠标按下。
        /// </summary>
        private void tvControlStruct_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tvEvent = sender as TreeView;
            TreeNode tnEvent = tvEvent.GetNodeAt(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (tnEvent != null)
                {
                    tvEvent.SelectedNode = tnEvent;
                    MainForm.AppMainForm.ControlProperty.EditControl = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
                    ShowSelectControlMark();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (tnEvent != null)
                {
                    //配合右键快捷菜单使用
                    tvEvent.SelectedNode = tnEvent;
                    MainForm.AppMainForm.ControlProperty.EditControl = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
                    ShowSelectControlMark();
                }
            }
        }

        /// <summary>
        /// 按下按键。
        /// </summary>
        private void InterfaceFileForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.U:
                    if (e.Control)
                    {
                        e.SuppressKeyPress = e.Shift ? this.DoMoveTop() : this.DoMoveUp();
                    }
                    break;
                case Keys.D:
                    if (e.Control)
                    {
                        e.SuppressKeyPress = e.Shift ? this.DoMoveBottom() : this.DoMoveDown();
                    }
                    break;
                case Keys.V:
                    if (e.Control)
                    {
                        this.ChangeControlVisible();
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Up:
                    if (e.Shift)
                    {
                        this.ChangeControlSize(0, e.Control ? 10 : 1);
                    }
                    else
                    {
                        this.MoveControlPosition(0, e.Control ? 10 : 1);
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Down:
                    if (e.Shift)
                    {
                        this.ChangeControlSize(0, e.Control ? -10 : -1);
                    }
                    else
                    {
                        this.MoveControlPosition(0, e.Control ? -10 : -1);
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Left:
                    if (e.Shift)
                    {
                        this.ChangeControlSize(e.Control ? -10 : -1, 0);
                    }
                    else
                    {
                        this.MoveControlPosition(e.Control ? -10 : -1, 0);
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Right:
                    if (e.Shift)
                    {
                        this.ChangeControlSize(e.Control ? 10 : 1, 0);
                    }
                    else
                    {
                        this.MoveControlPosition(e.Control ? 10 : 1, 0);
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.P:
                    if (e.Control)
                    {
                        this.AdjustSuitableSize();
                        e.SuppressKeyPress = true;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 准备拖动控件树结构视图节点。
        /// </summary>
        private void tvControlStruct_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeView tvEvent = sender as TreeView;
                TreeNode tnDrag = e.Item as TreeNode;
                if (tnDrag != null)
                {
                    //文件或文件夹才可以拖动
                    T002.Data.UI.Control ctrTag = tnDrag.Tag as T002.Data.UI.Control;
                    tvEvent.Focus();
                    if (ctrTag.Parent == null)
                    {
                        MessageBox.Show("Root节点不能拖动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (ctrTag.Parent is Table)
                    {
                        MessageBox.Show("表格单元节点不能拖动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
        }

        /// <summary>
        /// 拖动操作进入控件树结构视图区域。
        /// </summary>
        private void tvControlStruct_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent("System.Windows.Forms.TreeNode") ? DragDropEffects.Move : DragDropEffects.None;
        }

        /// <summary>
        /// 拖动操作经过控件树结构视图区域。
        /// </summary>
        private void tvControlStruct_DragOver(object sender, DragEventArgs e)
        {
            //判断是否能拖到该节点上
            TreeView tvEvent = sender as TreeView;
            TreeNode tnOver = tvEvent.GetNodeAt(tvEvent.PointToClient(new Point(e.X, e.Y)));
            if (tnOver != null)
            {
                TreeNode tnDrag = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
                e.Effect = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// 在控件树结构视图上结束拖动。
        /// </summary>
        private void tvControlStruct_DragDrop(object sender, DragEventArgs e)
        {
            //获得拖放中的节点和拖放目标TreeView，且不是自己移动到自己身上
            TreeView tvDragDrop = sender as TreeView;
            TreeNode tnDragNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
            TreeNode tnTargetNode = tvDragDrop.GetNodeAt(tvDragDrop.PointToClient(new Point(e.X, e.Y)));
            if (tnTargetNode != null && tnDragNode != tnTargetNode)
            {
                //目标节点不能是拖动节点的子节点
                TreeNode tnParent = tnTargetNode.Parent;
                while (tnParent != null)
                {
                    if (tnParent == tnDragNode)
                    {
                        MessageBox.Show("不能将控件拖动到自己的子节点上。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    tnParent = tnParent.Parent;
                }

                //拖动处理
                T002.Data.UI.Control ctrMove = tnDragNode.Tag as T002.Data.UI.Control;
                T002.Data.UI.Control ctrTarget = tnTargetNode.Tag as T002.Data.UI.Control;
                Boolean ctr = (e.KeyState & 8) != 0;
                if (ctr)
                {
                    //移动成目标子节点
                    if (ctrTarget is T002.Data.UI.Container)
                    {
                        this.m_ifEditFile.MoveChildControl(ctrMove, ctrTarget as T002.Data.UI.Container);
                        tnDragNode.Remove();
                        tnTargetNode.Nodes.Add(tnDragNode);
                        this.tvControlStruct.SelectedNode = tnDragNode;
                        this.tvControlStruct.Focus();
                        this.ShowSelectControlMark();
                    }
                    else
                    {
                        MessageBox.Show("只能拖动为容器控件的子节点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //移动为目标的上一个兄弟节点
                    if (ctrTarget.Parent == null)
                    {
                        MessageBox.Show("不能将控件拖动为Root节点的兄弟节点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (ctrTarget.Parent is Table)
                    {
                        MessageBox.Show("不能将控件拖动为表格单元节点的兄弟节点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (ctrTarget.Parent is ScrollPanel)
                    {
                        MessageBox.Show("不能将控件拖动为滚动面板子节点的兄弟节点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.m_ifEditFile.MoveAboveControl(ctrMove, ctrTarget);
                    tnDragNode.Remove();
                    tnTargetNode.Parent.Nodes.Insert(tnTargetNode.Index, tnDragNode);
                    this.tvControlStruct.SelectedNode = tnDragNode;
                    this.tvControlStruct.Focus();
                    this.ShowSelectControlMark();
                }
            }
        }

        #endregion

        #region 节点菜单=====================================================================================

        /// <summary>
        /// 准备打开节点快捷菜单。
        /// </summary>
        private void cmsNode_Opening(object sender, CancelEventArgs e)
        {
            //滑动面板的子节点和表格的原型节点只能添加子节点
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            T002.Data.UI.Control partent = ctrSelect.Parent;
            Boolean bRoot = partent == null;
            Boolean bContainer = ctrSelect is T002.Data.UI.Container;
            Boolean bPro = ctrSelect.Parent is Table;
            Boolean bSPChild = ctrSelect.Parent is ScrollPanel;

            tsmiNodeAdd.Visible = !bRoot && !bPro && !bSPChild;
            tsmiNodeChild.Visible = bContainer;
            tsmiNodeSP1.Visible = !bRoot;
            tsmiNodeUp.Visible = !bRoot;
            tsmiNodeTop.Visible = !bRoot;
            tsmiNodeDown.Visible = !bRoot;
            tsmiNodeBottom.Visible = !bRoot;
            tsmiNodeSP2.Visible = !bRoot;
            tsmiNodeEdit.Visible = !bRoot;
            tsmiNodeCopy.Visible = !bRoot;
            tsmiNodePaste.Visible = !bRoot && !bPro && !bSPChild;
            tsmiNodeDelete.Visible = !bRoot && !bPro && !bSPChild;

            //判断粘贴是否可用
            tsmiNodePaste.Enabled = false;
            String ctxt = Clipboard.GetText();
            if (ctxt.StartsWith(CLIPBOARD_CONTROL_MARK))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ctxt.Substring(CLIPBOARD_CONTROL_MARK.Length));
                    tsmiNodePaste.Enabled = true;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 单击节点快捷菜单 添加节点 选项。
        /// </summary>
        private void tsmiNodeAdd_Click(object sender, EventArgs e)
        {
            //添加控件
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            NewControlForm ncf = new NewControlForm();
            while (ncf.ShowDialog() == DialogResult.OK)
            {
                //新建控件
                T002.Data.UI.Control con = this.GetNewControl(ctrSelect.Parent, ncf.ControlType);
                if (con != null)
                {
                    con.Name = ncf.ControlName;
                    con.ConstVar = ncf.ConstVar;
                    this.m_ifEditFile.AddNewControl(ctrSelect, con);

                    //添加节点
                    TreeNode tnSelect = this.tvControlStruct.SelectedNode;
                    TreeNode tnNew = tnSelect.Parent.Nodes.Insert(tnSelect.Index + 1, con.GetNodeName());
                    this.SetNewControlNode(tnNew, con);
                    break;
                }
            }
        }

        /// <summary>
        /// 单击节点快捷菜单 添加子节点 选项。
        /// </summary>
        private void tsmiNodeChild_Click(object sender, EventArgs e)
        {
            //添加控件
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            NewControlForm ncf = new NewControlForm();
            while (ncf.ShowDialog() == DialogResult.OK)
            {
                //新建控件
                T002.Data.UI.Control con = this.GetNewControl(ctrSelect, ncf.ControlType);
                if (con != null)
                {
                    con.Name = ncf.ControlName;
                    con.ConstVar = ncf.ConstVar;
                    this.m_ifEditFile.AddNewChildControl(ctrSelect as T002.Data.UI.Container, con);

                    //添加节点
                    TreeNode tnSelect = this.tvControlStruct.SelectedNode;
                    TreeNode tnNew = tnSelect.Nodes.Add(con.GetNodeName());
                    this.SetNewControlNode(tnNew, con);
                    break;
                }
            }
        }

        /// <summary>
        /// 单击节点快捷菜单 上移 选项。
        /// </summary>
        private void tsmiNodeUp_Click(object sender, EventArgs e)
        {
            this.DoMoveUp();
        }

        /// <summary>
        /// 单击节点快捷菜单 下移 选项。
        /// </summary>
        private void tsmiNodeDown_Click(object sender, EventArgs e)
        {
            this.DoMoveDown();
        }

        /// <summary>
        /// 单击节点快捷菜单 上移顶部 选项。
        /// </summary>
        private void tsmiNodeTopmost_Click(object sender, EventArgs e)
        {
            this.DoMoveTop();
        }

        /// <summary>
        /// 单击节点快捷菜单 下移底部 选项。
        /// </summary>
        private void tsmiNodeBottommost_Click(object sender, EventArgs e)
        {
            this.DoMoveBottom();
        }

        /// <summary>
        /// 单击节点快捷菜单 编辑 选项。
        /// </summary>
        private void tsmiNodeEdit_Click(object sender, EventArgs e)
        {
            ControlEditForm cef = new ControlEditForm();
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            cef.EditControl = ctrSelect;
            if (cef.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String name = cef.ControlName;
                String var = cef.ConstVar;
                if (!name.Equals(ctrSelect.Name) || !var.Equals(ctrSelect.ConstVar))
                {
                    this.m_ifEditFile.EditControl(ctrSelect, name, var);
                    this.tvControlStruct.SelectedNode.Text = ctrSelect.GetNodeName();
                }
            }
        }

        /// <summary>
        /// 单击节点快捷菜单 复制 选项。
        /// </summary>
        private void tsmiNodeCopy_Click(object sender, EventArgs e)
        {
            T002.Data.UI.Control ctrSelect = this.tvControlStruct.SelectedNode.Tag as T002.Data.UI.Control;
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xmlNode = ctrSelect.GetXmlNode(xmlDoc);
            xmlDoc.AppendChild(xmlNode);
            Clipboard.SetText(CLIPBOARD_CONTROL_MARK + xmlDoc.OuterXml);
        }

        /// <summary>
        /// 单击节点快捷菜单 粘贴 选项。
        /// </summary>
        private void tsmiNodePaste_Click(object sender, EventArgs e)
        {
            //解析剪切文本
            String ctxt = Clipboard.GetText();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ctxt.Substring(CLIPBOARD_CONTROL_MARK.Length));

            //添加控件
            TreeNode tnNode = this.tvControlStruct.SelectedNode;
            T002.Data.UI.Control ctrSelect = tnNode.Tag as T002.Data.UI.Control;
            T002.Data.UI.Control ctr = UserInterface.LoadControlFromXmlNode(this.m_ifEditFile.Interface, xmlDoc.FirstChild);
            this.m_ifEditFile.PasteControl(ctrSelect, ctr);
            TreeNode tnCon = tnNode.Parent.Nodes.Insert(tnNode.Index + 1, ctr.GetNodeName());
            InitControlNode(tnCon, ctr);
            this.ivInterfaceContent.UpdataShow();
        }

        /// <summary>
        /// 单击节点快捷菜单 删除 选项。
        /// </summary>
        private void tsmiNodeDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        #endregion

        #endregion
    }
}
