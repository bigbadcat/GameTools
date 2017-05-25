using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T006.Data;
using T006.Data.Particle;
using System.Xml;

namespace T006.Forms
{
    public partial class ParticleFileForm : ResourceFileForm
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ParticleFileForm()
        {
            InitializeComponent();
            m_peFileView = new ParticleView();
            m_peFileView.Dock = DockStyle.Fill;
            this.pnlView.Width = ProjectManager.Project.SceneWidth;
            this.pnlView.Height = ProjectManager.Project.SceneHeight;
            this.pnlView.Controls.Add(m_peFileView);
        }

        /// <summary>
        /// 已重载。界面窗口进行删除操作。
        /// </summary>
        public override void DoDelete()
        {
            if (this.tvEffectList.SelectedNode == null || this.tvEffectList.SelectedNode.Parent == null)
            {
                return;
            }
            if (this.m_pfEditFile.EditParticle.Effects.Count == 1)
            {
                MessageBox.Show("最后一个效果不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            String msg = String.Format("是否删除效果{0}?", this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex].Name);
            DialogResult re = MessageBox.Show(msg, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.m_pfEditFile.DeleteEffect(m_iSelectIndex);
                this.tvEffectList.Nodes[0].Nodes.RemoveAt(m_iSelectIndex);
                Int32 newindex = m_iSelectIndex >= this.m_pfEditFile.EditParticle.Effects.Count ? m_iSelectIndex - 1 : m_iSelectIndex;
                this.tvEffectList.SelectedNode = this.tvEffectList.Nodes[0].Nodes[newindex];
            }
        }

        /// <summary>
        /// 更新粒子系统。
        /// </summary>
        /// <param name="dt">更新的时间间隔。</param>
        public void UpdateParticle(Single dt)
        {
            m_pfEditFile.Update(dt);
            m_peFileView.Refresh();
            MainForm.AppMainForm.StatusBarText2 = String.Format("PN:{0}", this.m_pfEditFile.EditParticle.ParticleNumber);
        }

        /// <summary>
        /// 更新效果名称。
        /// </summary>
        public void UpdateEffectName()
        {
            ParticleEffect eft = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex];
            this.tvEffectList.SelectedNode.Text = eft.GetNodeName();
        }

        /// <summary>
        /// 设置粒子系统属性。
        /// </summary>
        /// <param name="id">粒子系统ID。</param>
        public void SetParticleProperty(String id)
        {
            m_pfEditFile.SetParticleProperty(id);
            TreeNode tnRoot = this.tvEffectList.TopNode;
            tnRoot.Text = "粒子系统|" + m_pfEditFile.EditParticle.ID;
        }

        /// <summary>
        /// 重置编辑的粒子系统。
        /// </summary>
        public void ResetParticle()
        {
            this.m_pfEditFile.EditParticle.ResetSystem();
        }

        /// <summary>
        /// 设置背景图。
        /// </summary>
        /// <param name="name">背景图名称。</param>
        public void SetBackImage(String name)
        {
            this.m_peFileView.BackImage = name.Equals(String.Empty) ? null : new Bitmap(ProjectManager.Project.BackFolder + "\\" + name);
        }

        #endregion

        #region 对外属性=====================================================================================



        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 得到了编辑文件。
        /// </summary>
        protected override void HaveEditFile()
        {
            base.HaveEditFile();
            this.m_pfEditFile = this.m_rfEditFile as ParticleFile;
            this.m_peFileView.ShowParticle = this.m_pfEditFile;
            this.SetBackImage(this.m_pfEditFile.BackImageName);

            //效果列表
            TreeNode tnRoot = this.tvEffectList.Nodes.Add("粒子系统|" + m_pfEditFile.EditParticle.ID);
            tnRoot.ContextMenuStrip = this.cmsParticle;
            foreach (ParticleEffect eft in m_pfEditFile.EditParticle.Effects)
            {
                TreeNode tnEft = tnRoot.Nodes.Add(eft.GetNodeName());
                tnEft.ContextMenuStrip = this.cmsEffect;
            }
            tnRoot.Expand();
            this.tvEffectList.SelectedNode = tnRoot;
            this.m_pfEditFile.EditParticle.ResetSystem();
        }

        /// <summary>
        /// 清掉了编辑文件。
        /// </summary>
        protected override void ClearEditFile()
        {
            base.ClearEditFile();
            this.m_pfEditFile = null;
        }

        /// <summary>
        /// 移动粒子系统或效果。
        /// </summary>
        /// <param name="x">水平移动量。</param>
        /// <param name="y">竖直移动量。</param>
        protected void MoveSystemEffect(Int32 x, Int32 y)
        {
            if (this.tvEffectList.SelectedNode == null)
            {
                return;
            }

            if (this.tvEffectList.SelectedNode.Parent == null)
            {
                this.m_pfEditFile.MoveSystem(x, y);
            }
            else
            {
                this.m_pfEditFile.MoveEffectPosition(m_iSelectIndex, x, y);
            }
            MainForm.AppMainForm.EffectProperty.UpdatePosition();
        }

        /// <summary>
        /// 改变效果可见性。
        /// </summary>
        protected void ChangeEffectVisible()
        {
            if (this.tvEffectList.SelectedNode == null || this.tvEffectList.SelectedNode.Parent == null)
            {
                return;
            }
            this.m_pfEditFile.SetEffectVisible(m_iSelectIndex, !this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex].Visible);
            this.tvEffectList.SelectedNode.Text = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex].GetNodeName();
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 粒子编辑文件。
        /// </summary>
        private ParticleFile m_pfEditFile = null;

        /// <summary>
        /// 当前选择的效果索引。
        /// </summary>
        private Int32 m_iSelectIndex = 0;

        /// <summary>
        /// 文件视图。
        /// </summary>
        private ParticleView m_peFileView = null;

        /// <summary>
        /// 控件XML文本到剪切板里的标记。
        /// </summary>
        private const String CLIPBOARD_PARTICLE_MARK = "ParticleCutXXT";

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 窗体尺寸发生改变
        /// </summary>
        private void ParticleFileForm_Resize(object sender, EventArgs e)
        {
            Rectangle rtClient = this.ClientRectangle;
            this.splitContainer.Width = rtClient.Width - 8;
            this.splitContainer.Height = rtClient.Height - 8;
        }

        /// <summary>
        /// 单击粒子系统快捷菜单 添加 选项。
        /// </summary>
        private void tsmiParticleAdd_Click(object sender, EventArgs e)
        {
            NewEffectForm nef = new NewEffectForm();
            if (nef.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ParticleEffect pe = new ParticleEffect(this.m_pfEditFile.EditParticle);
                Bitmap bmp = new Bitmap(ProjectManager.Project.AssetsFolder + nef.EffectImage);
                pe.Name = nef.EffectName;
                pe.Image = bmp;
                pe.ImageName = nef.EffectImage;
                pe.Visible = true;
                pe.InitPreinstallEffect(nef.EffectType);
                pe.ResetEffect();
                this.m_pfEditFile.AddEffect(pe);

                //添加新节点
                TreeNode tnNew = this.tvEffectList.Nodes[0].Nodes.Add(pe.GetNodeName());
                tnNew.ContextMenuStrip = this.cmsEffect;
                this.tvEffectList.Nodes[0].Expand();
                this.tvEffectList.SelectedNode = tnNew;
            }
        }

        /// <summary>
        /// 选择新的效果。
        /// </summary>
        private void tvEffectList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tnNode = e.Node;
            this.m_iSelectIndex = tnNode.Parent == null ? -1 : tnNode.Index;
            if (m_iSelectIndex == -1)
            {
                MainForm.AppMainForm.EffectProperty.EditFile = this.m_pfEditFile;
            }
            else
            {
                MainForm.AppMainForm.EffectProperty.EffectIndex = m_iSelectIndex;
            }
        }

        /// <summary>
        /// 效果菜单准备打开。
        /// </summary>
        private void cmsEffect_Opening(object sender, CancelEventArgs e)
        {
            ParticleEffect eft = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex];
            this.tsmiEffectShow.Visible = !eft.Visible;
            this.tsmiEffectHide.Visible = eft.Visible;
            this.tsmiEffectDelete.Visible = this.m_pfEditFile.EditParticle.Effects.Count > 1;

            //判断粘贴是否可用
            tsmiEffectPaste.Enabled = false;
            String ctxt = Clipboard.GetText();
            if (ctxt.StartsWith(CLIPBOARD_PARTICLE_MARK))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ctxt.Substring(CLIPBOARD_PARTICLE_MARK.Length));
                    tsmiEffectPaste.Enabled = true;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 效果列表上按下鼠标。
        /// </summary>
        private void tvEffectList_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tvEvent = sender as TreeView;
            TreeNode tnEvent = tvEvent.GetNodeAt(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (tnEvent != null)
                {
                    tvEvent.SelectedNode = tnEvent;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (tnEvent != null)
                {
                    //配合右键快捷菜单使用
                    tvEvent.SelectedNode = tnEvent;
                }
            }
        }

        /// <summary>
        /// 显示效果。
        /// </summary>
        private void tsmiEffectShow_Click(object sender, EventArgs e)
        {
            this.m_pfEditFile.SetEffectVisible(m_iSelectIndex, true);
            this.tvEffectList.SelectedNode.Text = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex].GetNodeName();
        }

        /// <summary>
        /// 隐藏效果。
        /// </summary>
        private void tsmiEffectHide_Click(object sender, EventArgs e)
        {
            this.m_pfEditFile.SetEffectVisible(m_iSelectIndex, false);
            this.tvEffectList.SelectedNode.Text = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex].GetNodeName();
        }

        /// <summary>
        /// 复制效果。
        /// </summary>
        private void tsmiEffectCopy_Click(object sender, EventArgs e)
        {
            ParticleEffect eft = this.m_pfEditFile.EditParticle.Effects[m_iSelectIndex];
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xmlNode = eft.GetXmlNode(xmlDoc);
            xmlDoc.AppendChild(xmlNode);
            Clipboard.SetText(CLIPBOARD_PARTICLE_MARK + xmlDoc.OuterXml);
        }

        /// <summary>
        /// 粘贴效果。
        /// </summary>
        private void tsmiEffectPaste_Click(object sender, EventArgs e)
        {
            //解析剪切文本
            String ctxt = Clipboard.GetText();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ctxt.Substring(CLIPBOARD_PARTICLE_MARK.Length));

            //添加效果
            ParticleEffect eft = ParticleEffect.LoadFromXmlNode(this.m_pfEditFile.EditParticle, xmlDoc.FirstChild);
            eft.ResetEffect();
            this.m_pfEditFile.AddEffect(this.m_iSelectIndex, eft);
            TreeNode tnNode = this.tvEffectList.SelectedNode;
            TreeNode tnEft = tnNode.Parent.Nodes.Insert(tnNode.Index + 1, eft.GetNodeName());
            tnEft.ContextMenuStrip = this.cmsEffect;
            this.tvEffectList.SelectedNode = tnEft;
        }

        /// <summary>
        /// 删除效果。
        /// </summary>
        private void tsmiEffectDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        /// <summary>
        /// 设置效果参数。
        /// </summary>
        private void ppgEffectProperty_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.m_pfEditFile.SetAmend(true);
        }

        /// <summary>
        /// 准备拖动控件树结构视图节点。
        /// </summary>
        private void tvEffectList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeView tvEvent = sender as TreeView;
                TreeNode tnDrag = e.Item as TreeNode;
                if (tnDrag != null)
                {
                    tvEvent.Focus();
                    if (tnDrag.Parent == null)
                    {
                        MessageBox.Show("只能拖动效果节点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void tvEffectList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent("System.Windows.Forms.TreeNode") ? DragDropEffects.Move : DragDropEffects.None;
        }

        /// <summary>
        /// 拖动操作经过控件树结构视图区域。
        /// </summary>
        private void tvEffectList_DragOver(object sender, DragEventArgs e)
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
        private void tvEffectList_DragDrop(object sender, DragEventArgs e)
        {
            //获得拖放中的节点和拖放目标TreeView，且不是自己移动到自己身上
            TreeView tvDragDrop = sender as TreeView;
            TreeNode tnDragNode = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
            TreeNode tnTargetNode = tvDragDrop.GetNodeAt(tvDragDrop.PointToClient(new Point(e.X, e.Y)));
            if (tnTargetNode != null && tnTargetNode.Parent != null && tnDragNode != tnTargetNode)
            {
                Int32 iDrag = tnDragNode.Index;
                Int32 iTarget = tnTargetNode.Index;
                this.m_pfEditFile.MoveEffectLayer(iDrag, iTarget);
                tnDragNode.Remove();
                tnTargetNode.Parent.Nodes.Insert(tnTargetNode.Index, tnDragNode);
                this.tvEffectList.SelectedNode = tnDragNode;
                this.tvEffectList.Focus();
            }
        }

        /// <summary>
        /// 按下按键。
        /// </summary>
        private void ParticleFileForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.MoveSystemEffect(0, e.Control ? 10 : 1);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Down:
                    this.MoveSystemEffect(0, e.Control ? -10 : -1);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Left:
                    this.MoveSystemEffect(e.Control ? -10 : -1, 0);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Right:
                    this.MoveSystemEffect(e.Control ? 10 : 1, 0);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.V:
                    if (e.Control)
                    {
                        this.ChangeEffectVisible();
                        e.SuppressKeyPress = true;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
