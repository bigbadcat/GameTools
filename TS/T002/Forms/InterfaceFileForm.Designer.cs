using XuXiang;
using XuXiang.Tool.ControlLibrary;
namespace T002.Forms
{
    partial class InterfaceFileForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.ivInterfaceContent = new XuXiang.Tool.ControlLibrary.ImageView();
            this.tvControlStruct = new System.Windows.Forms.TreeView();
            this.cmsNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNodeAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeChild = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeSP1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNodeUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeSP2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNodeEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodePaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cmsNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Location = new System.Drawing.Point(4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ivInterfaceContent);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tvControlStruct);
            this.splitContainer.Size = new System.Drawing.Size(684, 463);
            this.splitContainer.SplitterDistance = 523;
            this.splitContainer.TabIndex = 2;
            // 
            // ivInterfaceContent
            // 
            this.ivInterfaceContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ivInterfaceContent.DisplayImage = null;
            this.ivInterfaceContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivInterfaceContent.ImageBackColor = System.Drawing.Color.SlateGray;
            this.ivInterfaceContent.Location = new System.Drawing.Point(0, 0);
            this.ivInterfaceContent.Name = "ivInterfaceContent";
            this.ivInterfaceContent.Size = new System.Drawing.Size(521, 461);
            this.ivInterfaceContent.TabIndex = 0;
            this.ivInterfaceContent.ViewMouseEnter += new System.EventHandler(this.ivInterfaceContent_ViewMouseEnter);
            this.ivInterfaceContent.ViewMouseDown += new System.Windows.Forms.MouseEventHandler(this.ivInterfaceContent_ViewMouseDown);
            this.ivInterfaceContent.ViewMouseMove += new System.Windows.Forms.MouseEventHandler(this.ivInterfaceContent_ViewMouseMove);
            this.ivInterfaceContent.ViewMouseUp += new System.Windows.Forms.MouseEventHandler(this.ivInterfaceContent_ViewMouseUp);
            this.ivInterfaceContent.ViewMouseLeave += new System.EventHandler(this.ivInterfaceContent_ViewMouseLeave);
            // 
            // tvControlStruct
            // 
            this.tvControlStruct.AllowDrop = true;
            this.tvControlStruct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvControlStruct.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvControlStruct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvControlStruct.FullRowSelect = true;
            this.tvControlStruct.HideSelection = false;
            this.tvControlStruct.Location = new System.Drawing.Point(0, 0);
            this.tvControlStruct.Name = "tvControlStruct";
            this.tvControlStruct.Size = new System.Drawing.Size(155, 461);
            this.tvControlStruct.TabIndex = 0;
            this.tvControlStruct.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvControlStruct_ItemDrag);
            this.tvControlStruct.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvControlStruct_DragDrop);
            this.tvControlStruct.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvControlStruct_DragEnter);
            this.tvControlStruct.DragOver += new System.Windows.Forms.DragEventHandler(this.tvControlStruct_DragOver);
            this.tvControlStruct.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvControlStruct_MouseDown);
            // 
            // cmsNode
            // 
            this.cmsNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNodeAdd,
            this.tsmiNodeChild,
            this.tsmiNodeSP1,
            this.tsmiNodeUp,
            this.tsmiNodeTop,
            this.tsmiNodeDown,
            this.tsmiNodeBottom,
            this.tsmiNodeSP2,
            this.tsmiNodeEdit,
            this.tsmiNodeCopy,
            this.tsmiNodePaste,
            this.tsmiNodeDelete});
            this.cmsNode.Name = "cmsNode";
            this.cmsNode.Size = new System.Drawing.Size(137, 236);
            this.cmsNode.Opening += new System.ComponentModel.CancelEventHandler(this.cmsNode_Opening);
            // 
            // tsmiNodeAdd
            // 
            this.tsmiNodeAdd.Name = "tsmiNodeAdd";
            this.tsmiNodeAdd.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeAdd.Text = "添加节点";
            this.tsmiNodeAdd.Click += new System.EventHandler(this.tsmiNodeAdd_Click);
            // 
            // tsmiNodeChild
            // 
            this.tsmiNodeChild.Name = "tsmiNodeChild";
            this.tsmiNodeChild.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeChild.Text = "添加子节点";
            this.tsmiNodeChild.Click += new System.EventHandler(this.tsmiNodeChild_Click);
            // 
            // tsmiNodeSP1
            // 
            this.tsmiNodeSP1.Name = "tsmiNodeSP1";
            this.tsmiNodeSP1.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmiNodeUp
            // 
            this.tsmiNodeUp.Name = "tsmiNodeUp";
            this.tsmiNodeUp.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeUp.Text = "上移";
            this.tsmiNodeUp.Click += new System.EventHandler(this.tsmiNodeUp_Click);
            // 
            // tsmiNodeTop
            // 
            this.tsmiNodeTop.Name = "tsmiNodeTop";
            this.tsmiNodeTop.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeTop.Text = "上移顶部";
            this.tsmiNodeTop.Click += new System.EventHandler(this.tsmiNodeTopmost_Click);
            // 
            // tsmiNodeDown
            // 
            this.tsmiNodeDown.Name = "tsmiNodeDown";
            this.tsmiNodeDown.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeDown.Text = "下移";
            this.tsmiNodeDown.Click += new System.EventHandler(this.tsmiNodeDown_Click);
            // 
            // tsmiNodeBottom
            // 
            this.tsmiNodeBottom.Name = "tsmiNodeBottom";
            this.tsmiNodeBottom.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeBottom.Text = "下移底部";
            this.tsmiNodeBottom.Click += new System.EventHandler(this.tsmiNodeBottommost_Click);
            // 
            // tsmiNodeSP2
            // 
            this.tsmiNodeSP2.Name = "tsmiNodeSP2";
            this.tsmiNodeSP2.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmiNodeEdit
            // 
            this.tsmiNodeEdit.Name = "tsmiNodeEdit";
            this.tsmiNodeEdit.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeEdit.Text = "编辑";
            this.tsmiNodeEdit.Click += new System.EventHandler(this.tsmiNodeEdit_Click);
            // 
            // tsmiNodeCopy
            // 
            this.tsmiNodeCopy.Name = "tsmiNodeCopy";
            this.tsmiNodeCopy.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeCopy.Text = "复制";
            this.tsmiNodeCopy.Click += new System.EventHandler(this.tsmiNodeCopy_Click);
            // 
            // tsmiNodePaste
            // 
            this.tsmiNodePaste.Name = "tsmiNodePaste";
            this.tsmiNodePaste.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodePaste.Text = "粘贴";
            this.tsmiNodePaste.Click += new System.EventHandler(this.tsmiNodePaste_Click);
            // 
            // tsmiNodeDelete
            // 
            this.tsmiNodeDelete.Name = "tsmiNodeDelete";
            this.tsmiNodeDelete.Size = new System.Drawing.Size(136, 22);
            this.tsmiNodeDelete.Text = "删除";
            this.tsmiNodeDelete.Click += new System.EventHandler(this.tsmiNodeDelete_Click);
            // 
            // InterfaceFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 471);
            this.Controls.Add(this.splitContainer);
            this.KeyPreview = true;
            this.Name = "InterfaceFileForm";
            this.Text = "InterfaceFileForm";
            this.Load += new System.EventHandler(this.InterfaceFileForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InterfaceFileForm_KeyDown);
            this.Resize += new System.EventHandler(this.InterfaceFileForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.cmsNode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private ImageView ivInterfaceContent;
        private System.Windows.Forms.TreeView tvControlStruct;
        private System.Windows.Forms.ContextMenuStrip cmsNode;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeChild;
        private System.Windows.Forms.ToolStripSeparator tsmiNodeSP1;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeTop;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeBottom;
        private System.Windows.Forms.ToolStripSeparator tsmiNodeSP2;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodePaste;

    }
}