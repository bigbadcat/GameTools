namespace T008
{
    partial class EnumForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("AttrType");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("AttackType");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("BuffType");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Unknown",
            "0",
            "未知"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "AddAttack",
            "1",
            "增加攻击"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "AttDefens",
            "255",
            "增加防御力"}, -1);
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvEnumList = new System.Windows.Forms.ListView();
            this.chEnumName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tibNote = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tibName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.lvContent = new System.Windows.Forms.ListView();
            this.chContentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContentValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContentNote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lvEnumList);
            this.splitContainer.Panel1.SizeChanged += new System.EventHandler(this.splitContainer_Panel1_SizeChanged);
            this.splitContainer.Panel1MinSize = 200;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tibNote);
            this.splitContainer.Panel2.Controls.Add(this.tibName);
            this.splitContainer.Panel2.Controls.Add(this.lvContent);
            this.splitContainer.Panel2.SizeChanged += new System.EventHandler(this.splitContainer_Panel2_SizeChanged);
            this.splitContainer.Panel2MinSize = 270;
            this.splitContainer.Size = new System.Drawing.Size(684, 461);
            this.splitContainer.SplitterDistance = 232;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 5;
            this.splitContainer.TabStop = false;
            // 
            // lvEnumList
            // 
            this.lvEnumList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEnumName});
            this.lvEnumList.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvEnumList.FullRowSelect = true;
            this.lvEnumList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvEnumList.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.lvEnumList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lvEnumList.Location = new System.Drawing.Point(12, 12);
            this.lvEnumList.MultiSelect = false;
            this.lvEnumList.Name = "lvEnumList";
            this.lvEnumList.Size = new System.Drawing.Size(206, 437);
            this.lvEnumList.TabIndex = 1;
            this.lvEnumList.UseCompatibleStateImageBehavior = false;
            this.lvEnumList.View = System.Windows.Forms.View.Details;
            this.lvEnumList.SelectedIndexChanged += new System.EventHandler(this.lvEnumList_SelectedIndexChanged);
            // 
            // chEnumName
            // 
            this.chEnumName.Text = "枚举";
            this.chEnumName.Width = 179;
            // 
            // tibNote
            // 
            this.tibNote.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibNote.Caption = "描述";
            this.tibNote.CaptionWidth = 60;
            this.tibNote.Enabled = false;
            this.tibNote.InputValue = "";
            this.tibNote.Location = new System.Drawing.Point(12, 42);
            this.tibNote.Name = "tibNote";
            this.tibNote.Size = new System.Drawing.Size(422, 24);
            this.tibNote.TabIndex = 6;
            // 
            // tibName
            // 
            this.tibName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibName.Caption = "名称";
            this.tibName.CaptionWidth = 60;
            this.tibName.Enabled = false;
            this.tibName.InputValue = "";
            this.tibName.Location = new System.Drawing.Point(12, 12);
            this.tibName.Name = "tibName";
            this.tibName.Size = new System.Drawing.Size(300, 24);
            this.tibName.TabIndex = 5;
            // 
            // lvContent
            // 
            this.lvContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chContentName,
            this.chContentValue,
            this.chContentNote});
            this.lvContent.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvContent.FullRowSelect = true;
            this.lvContent.GridLines = true;
            this.lvContent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvContent.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lvContent.Location = new System.Drawing.Point(12, 72);
            this.lvContent.MultiSelect = false;
            this.lvContent.Name = "lvContent";
            this.lvContent.Size = new System.Drawing.Size(422, 377);
            this.lvContent.TabIndex = 4;
            this.lvContent.UseCompatibleStateImageBehavior = false;
            this.lvContent.View = System.Windows.Forms.View.Details;
            // 
            // chContentName
            // 
            this.chContentName.Text = "名称";
            this.chContentName.Width = 150;
            // 
            // chContentValue
            // 
            this.chContentValue.Text = "值";
            this.chContentValue.Width = 50;
            // 
            // chContentNote
            // 
            this.chContentNote.Text = "描述";
            this.chContentNote.Width = 200;
            // 
            // EnumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "EnumForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "枚举定义";
            this.Load += new System.EventHandler(this.EnumForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView lvEnumList;
        private System.Windows.Forms.ListView lvContent;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibName;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibNote;
        private System.Windows.Forms.ColumnHeader chEnumName;
        private System.Windows.Forms.ColumnHeader chContentName;
        private System.Windows.Forms.ColumnHeader chContentValue;
        private System.Windows.Forms.ColumnHeader chContentNote;
    }
}