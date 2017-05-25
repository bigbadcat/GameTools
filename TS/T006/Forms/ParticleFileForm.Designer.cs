namespace T006.Forms
{
    partial class ParticleFileForm
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
            this.pnlView = new System.Windows.Forms.Panel();
            this.tvEffectList = new System.Windows.Forms.TreeView();
            this.cmsParticle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiParticleAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEffect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEffectShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectHide = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cmsParticle.SuspendLayout();
            this.cmsEffect.SuspendLayout();
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
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.Controls.Add(this.pnlView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tvEffectList);
            this.splitContainer.Size = new System.Drawing.Size(684, 463);
            this.splitContainer.SplitterDistance = 479;
            this.splitContainer.TabIndex = 3;
            // 
            // pnlView
            // 
            this.pnlView.BackColor = System.Drawing.Color.Black;
            this.pnlView.Location = new System.Drawing.Point(-1, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(400, 300);
            this.pnlView.TabIndex = 1;
            // 
            // tvEffectList
            // 
            this.tvEffectList.AllowDrop = true;
            this.tvEffectList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvEffectList.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvEffectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvEffectList.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvEffectList.FullRowSelect = true;
            this.tvEffectList.HideSelection = false;
            this.tvEffectList.Location = new System.Drawing.Point(0, 0);
            this.tvEffectList.Name = "tvEffectList";
            this.tvEffectList.Size = new System.Drawing.Size(199, 461);
            this.tvEffectList.TabIndex = 0;
            this.tvEffectList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvEffectList_ItemDrag);
            this.tvEffectList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEffectList_AfterSelect);
            this.tvEffectList.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvEffectList_DragDrop);
            this.tvEffectList.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvEffectList_DragEnter);
            this.tvEffectList.DragOver += new System.Windows.Forms.DragEventHandler(this.tvEffectList_DragOver);
            this.tvEffectList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvEffectList_MouseDown);
            // 
            // cmsParticle
            // 
            this.cmsParticle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiParticleAdd});
            this.cmsParticle.Name = "cmsParticle";
            this.cmsParticle.Size = new System.Drawing.Size(141, 26);
            // 
            // tsmiParticleAdd
            // 
            this.tsmiParticleAdd.Name = "tsmiParticleAdd";
            this.tsmiParticleAdd.Size = new System.Drawing.Size(140, 22);
            this.tsmiParticleAdd.Text = "添加效果(&A)";
            this.tsmiParticleAdd.Click += new System.EventHandler(this.tsmiParticleAdd_Click);
            // 
            // cmsEffect
            // 
            this.cmsEffect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEffectShow,
            this.tsmiEffectHide,
            this.tsmiEffectCopy,
            this.tsmiEffectPaste,
            this.tsmiEffectDelete});
            this.cmsEffect.Name = "cmsEffect";
            this.cmsEffect.Size = new System.Drawing.Size(101, 114);
            this.cmsEffect.Text = "显示(&S)";
            this.cmsEffect.Opening += new System.ComponentModel.CancelEventHandler(this.cmsEffect_Opening);
            // 
            // tsmiEffectShow
            // 
            this.tsmiEffectShow.Name = "tsmiEffectShow";
            this.tsmiEffectShow.Size = new System.Drawing.Size(100, 22);
            this.tsmiEffectShow.Text = "显示";
            this.tsmiEffectShow.Click += new System.EventHandler(this.tsmiEffectShow_Click);
            // 
            // tsmiEffectHide
            // 
            this.tsmiEffectHide.Name = "tsmiEffectHide";
            this.tsmiEffectHide.Size = new System.Drawing.Size(100, 22);
            this.tsmiEffectHide.Text = "隐藏";
            this.tsmiEffectHide.Click += new System.EventHandler(this.tsmiEffectHide_Click);
            // 
            // tsmiEffectCopy
            // 
            this.tsmiEffectCopy.Name = "tsmiEffectCopy";
            this.tsmiEffectCopy.Size = new System.Drawing.Size(100, 22);
            this.tsmiEffectCopy.Text = "复制";
            this.tsmiEffectCopy.Click += new System.EventHandler(this.tsmiEffectCopy_Click);
            // 
            // tsmiEffectPaste
            // 
            this.tsmiEffectPaste.Name = "tsmiEffectPaste";
            this.tsmiEffectPaste.Size = new System.Drawing.Size(100, 22);
            this.tsmiEffectPaste.Text = "粘贴";
            this.tsmiEffectPaste.Click += new System.EventHandler(this.tsmiEffectPaste_Click);
            // 
            // tsmiEffectDelete
            // 
            this.tsmiEffectDelete.Name = "tsmiEffectDelete";
            this.tsmiEffectDelete.Size = new System.Drawing.Size(100, 22);
            this.tsmiEffectDelete.Text = "删除";
            this.tsmiEffectDelete.Click += new System.EventHandler(this.tsmiEffectDelete_Click);
            // 
            // ParticleFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 471);
            this.Controls.Add(this.splitContainer);
            this.KeyPreview = true;
            this.Name = "ParticleFileForm";
            this.Text = "ParticleFileForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ParticleFileForm_KeyDown);
            this.Resize += new System.EventHandler(this.ParticleFileForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.cmsParticle.ResumeLayout(false);
            this.cmsEffect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView tvEffectList;
        private System.Windows.Forms.ContextMenuStrip cmsParticle;
        private System.Windows.Forms.ToolStripMenuItem tsmiParticleAdd;
        private System.Windows.Forms.ContextMenuStrip cmsEffect;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectHide;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectDelete;
        private System.Windows.Forms.Panel pnlView;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectPaste;
    }
}