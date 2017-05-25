namespace T006.Forms
{
    partial class ProjectManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectManageForm));
            this.tvProjectResource = new System.Windows.Forms.TreeView();
            this.imglProjectResource = new System.Windows.Forms.ImageList(this.components);
            this.cmsResource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsiResourceOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceSP1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsiResourceCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiResourceRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsBranch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsiBranchNewFolderNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiBranchNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiBranchNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiBranchSP1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsiBranchRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiBranchBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiProjectRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectSP1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiProjectProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsResource.SuspendLayout();
            this.cmsBranch.SuspendLayout();
            this.cmsProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvProjectResource
            // 
            this.tvProjectResource.AllowDrop = true;
            this.tvProjectResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(205)))), ((int)(((byte)(240)))));
            this.tvProjectResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProjectResource.ImageIndex = 0;
            this.tvProjectResource.ImageList = this.imglProjectResource;
            this.tvProjectResource.LabelEdit = true;
            this.tvProjectResource.Location = new System.Drawing.Point(0, 0);
            this.tvProjectResource.Name = "tvProjectResource";
            this.tvProjectResource.SelectedImageIndex = 0;
            this.tvProjectResource.Size = new System.Drawing.Size(237, 449);
            this.tvProjectResource.TabIndex = 1;
            this.tvProjectResource.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvProjectResource_BeforeLabelEdit);
            this.tvProjectResource.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvProjectResource_AfterLabelEdit);
            this.tvProjectResource.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProjectResource_AfterCollapse);
            this.tvProjectResource.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProjectResource_AfterExpand);
            this.tvProjectResource.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvProjectResource_ItemDrag);
            this.tvProjectResource.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProjectResource_NodeMouseDoubleClick);
            this.tvProjectResource.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvProjectResource_DragDrop);
            this.tvProjectResource.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvProjectResource_DragEnter);
            this.tvProjectResource.DragOver += new System.Windows.Forms.DragEventHandler(this.tvProjectResource_DragOver);
            this.tvProjectResource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProjectResource_MouseDown);
            // 
            // imglProjectResource
            // 
            this.imglProjectResource.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglProjectResource.ImageStream")));
            this.imglProjectResource.TransparentColor = System.Drawing.Color.White;
            this.imglProjectResource.Images.SetKeyName(0, "Project.bmp");
            this.imglProjectResource.Images.SetKeyName(1, "Particle.bmp");
            this.imglProjectResource.Images.SetKeyName(2, "ParticleFile.bmp");
            this.imglProjectResource.Images.SetKeyName(3, "FolderClose.bmp");
            this.imglProjectResource.Images.SetKeyName(4, "FolderOpen.bmp");
            // 
            // cmsResource
            // 
            this.cmsResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiResourceOpen,
            this.tmsiResourceNew,
            this.tmsiResourceBuild,
            this.tmsiResourceSP1,
            this.tmsiResourceCopy,
            this.tmsiResourceDelete,
            this.tmsiResourceRename});
            this.cmsResource.Name = "cmsTileResource";
            this.cmsResource.Size = new System.Drawing.Size(133, 142);
            this.cmsResource.Opening += new System.ComponentModel.CancelEventHandler(this.cmsResource_Opening);
            // 
            // tmsiResourceOpen
            // 
            this.tmsiResourceOpen.Name = "tmsiResourceOpen";
            this.tmsiResourceOpen.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceOpen.Text = "打开(&O)";
            this.tmsiResourceOpen.Click += new System.EventHandler(this.tmsiResourceOpen_Click);
            // 
            // tmsiResourceNew
            // 
            this.tmsiResourceNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiResourceNewFile,
            this.tmsiResourceNewFolder});
            this.tmsiResourceNew.Name = "tmsiResourceNew";
            this.tmsiResourceNew.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceNew.Text = "新建";
            // 
            // tmsiResourceNewFile
            // 
            this.tmsiResourceNewFile.Name = "tmsiResourceNewFile";
            this.tmsiResourceNewFile.Size = new System.Drawing.Size(129, 22);
            this.tmsiResourceNewFile.Text = "文件(&F)";
            this.tmsiResourceNewFile.Click += new System.EventHandler(this.tmsiResourceNewFile_Click);
            // 
            // tmsiResourceNewFolder
            // 
            this.tmsiResourceNewFolder.Image = global::T006.Properties.Resources.Folder;
            this.tmsiResourceNewFolder.Name = "tmsiResourceNewFolder";
            this.tmsiResourceNewFolder.Size = new System.Drawing.Size(129, 22);
            this.tmsiResourceNewFolder.Text = "文件夹(&D)";
            this.tmsiResourceNewFolder.Click += new System.EventHandler(this.tmsiResourceNewFolder_Click);
            // 
            // tmsiResourceBuild
            // 
            this.tmsiResourceBuild.Name = "tmsiResourceBuild";
            this.tmsiResourceBuild.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceBuild.Text = "生成(&B)";
            this.tmsiResourceBuild.Click += new System.EventHandler(this.tmsiResourceBuild_Click);
            // 
            // tmsiResourceSP1
            // 
            this.tmsiResourceSP1.Name = "tmsiResourceSP1";
            this.tmsiResourceSP1.Size = new System.Drawing.Size(129, 6);
            // 
            // tmsiResourceCopy
            // 
            this.tmsiResourceCopy.Name = "tmsiResourceCopy";
            this.tmsiResourceCopy.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceCopy.Text = "复制(&C)";
            this.tmsiResourceCopy.Click += new System.EventHandler(this.tmsiResourceCopy_Click);
            // 
            // tmsiResourceDelete
            // 
            this.tmsiResourceDelete.Name = "tmsiResourceDelete";
            this.tmsiResourceDelete.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceDelete.Text = "删除(&D)";
            this.tmsiResourceDelete.Click += new System.EventHandler(this.tmsiResourceDelete_Click);
            // 
            // tmsiResourceRename
            // 
            this.tmsiResourceRename.Name = "tmsiResourceRename";
            this.tmsiResourceRename.Size = new System.Drawing.Size(132, 22);
            this.tmsiResourceRename.Text = "重命名(&M)";
            this.tmsiResourceRename.Click += new System.EventHandler(this.tmsiResourceRename_Click);
            // 
            // cmsBranch
            // 
            this.cmsBranch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiBranchNewFolderNew,
            this.tmsiBranchSP1,
            this.tmsiBranchRefresh,
            this.tmsiBranchBuild});
            this.cmsBranch.Name = "cmsBranch";
            this.cmsBranch.Size = new System.Drawing.Size(119, 76);
            this.cmsBranch.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBranch_Opening);
            // 
            // tmsiBranchNewFolderNew
            // 
            this.tmsiBranchNewFolderNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiBranchNewFile,
            this.tmsiBranchNewFolder});
            this.tmsiBranchNewFolderNew.Name = "tmsiBranchNewFolderNew";
            this.tmsiBranchNewFolderNew.Size = new System.Drawing.Size(118, 22);
            this.tmsiBranchNewFolderNew.Text = "新建(&N)";
            // 
            // tmsiBranchNewFile
            // 
            this.tmsiBranchNewFile.Name = "tmsiBranchNewFile";
            this.tmsiBranchNewFile.Size = new System.Drawing.Size(129, 22);
            this.tmsiBranchNewFile.Text = "文件(&F)";
            this.tmsiBranchNewFile.Click += new System.EventHandler(this.tmsiBranchNewFile_Click);
            // 
            // tmsiBranchNewFolder
            // 
            this.tmsiBranchNewFolder.Image = global::T006.Properties.Resources.Folder;
            this.tmsiBranchNewFolder.Name = "tmsiBranchNewFolder";
            this.tmsiBranchNewFolder.Size = new System.Drawing.Size(129, 22);
            this.tmsiBranchNewFolder.Text = "文件夹(&D)";
            this.tmsiBranchNewFolder.Click += new System.EventHandler(this.tmsiBranchNewFolder_Click);
            // 
            // tmsiBranchSP1
            // 
            this.tmsiBranchSP1.Name = "tmsiBranchSP1";
            this.tmsiBranchSP1.Size = new System.Drawing.Size(115, 6);
            // 
            // tmsiBranchRefresh
            // 
            this.tmsiBranchRefresh.Name = "tmsiBranchRefresh";
            this.tmsiBranchRefresh.Size = new System.Drawing.Size(118, 22);
            this.tmsiBranchRefresh.Text = "刷新(&R)";
            this.tmsiBranchRefresh.Click += new System.EventHandler(this.tmsiBranchRefresh_Click);
            // 
            // tmsiBranchBuild
            // 
            this.tmsiBranchBuild.Name = "tmsiBranchBuild";
            this.tmsiBranchBuild.Size = new System.Drawing.Size(118, 22);
            this.tmsiBranchBuild.Text = "生成(&B)";
            this.tmsiBranchBuild.Click += new System.EventHandler(this.tmsiBranchBuild_Click);
            // 
            // cmsProject
            // 
            this.cmsProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProjectRefresh,
            this.tsmiProjectRename,
            this.tsmiProjectSP1,
            this.tsmiProjectProperty});
            this.cmsProject.Name = "cmsProject";
            this.cmsProject.Size = new System.Drawing.Size(133, 76);
            this.cmsProject.Opening += new System.ComponentModel.CancelEventHandler(this.cmsProject_Opening);
            // 
            // tsmiProjectRefresh
            // 
            this.tsmiProjectRefresh.Name = "tsmiProjectRefresh";
            this.tsmiProjectRefresh.Size = new System.Drawing.Size(132, 22);
            this.tsmiProjectRefresh.Text = "刷新(&R)";
            this.tsmiProjectRefresh.Click += new System.EventHandler(this.tsmiProjectRefresh_Click);
            // 
            // tsmiProjectRename
            // 
            this.tsmiProjectRename.Name = "tsmiProjectRename";
            this.tsmiProjectRename.Size = new System.Drawing.Size(132, 22);
            this.tsmiProjectRename.Text = "重命名(&M)";
            this.tsmiProjectRename.Click += new System.EventHandler(this.tsmiProjectRename_Click);
            // 
            // tsmiProjectSP1
            // 
            this.tsmiProjectSP1.Name = "tsmiProjectSP1";
            this.tsmiProjectSP1.Size = new System.Drawing.Size(129, 6);
            // 
            // tsmiProjectProperty
            // 
            this.tsmiProjectProperty.Image = global::T006.Properties.Resources.Property;
            this.tsmiProjectProperty.Name = "tsmiProjectProperty";
            this.tsmiProjectProperty.Size = new System.Drawing.Size(132, 22);
            this.tsmiProjectProperty.Text = "属性(&P)";
            this.tsmiProjectProperty.Click += new System.EventHandler(this.tsmiProjectProperty_Click);
            // 
            // ProjectManageForm
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 449);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tvProjectResource);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectManageForm";
            this.Text = "工程管理";
            this.cmsResource.ResumeLayout(false);
            this.cmsBranch.ResumeLayout(false);
            this.cmsProject.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvProjectResource;
        private System.Windows.Forms.ContextMenuStrip cmsResource;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceOpen;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceNew;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceNewFile;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceNewFolder;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceBuild;
        private System.Windows.Forms.ToolStripSeparator tmsiResourceSP1;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceCopy;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceDelete;
        private System.Windows.Forms.ToolStripMenuItem tmsiResourceRename;
        private System.Windows.Forms.ContextMenuStrip cmsBranch;
        private System.Windows.Forms.ToolStripMenuItem tmsiBranchNewFolderNew;
        private System.Windows.Forms.ToolStripMenuItem tmsiBranchNewFile;
        private System.Windows.Forms.ToolStripMenuItem tmsiBranchNewFolder;
        private System.Windows.Forms.ToolStripMenuItem tmsiBranchRefresh;
        private System.Windows.Forms.ToolStripMenuItem tmsiBranchBuild;
        private System.Windows.Forms.ContextMenuStrip cmsProject;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectRename;
        private System.Windows.Forms.ToolStripSeparator tsmiProjectSP1;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectProperty;
        private System.Windows.Forms.ImageList imglProjectResource;
        private System.Windows.Forms.ToolStripSeparator tmsiBranchSP1;
    }
}