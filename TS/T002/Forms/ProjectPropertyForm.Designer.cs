namespace T002.Forms
{
    partial class ProjectPropertyForm
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
            this.pnlSP1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.fibProjectFont = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.fibProjectPath = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.tibProjectName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.fibAssetsFolder = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.fibBuildFolder = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.SuspendLayout();
            // 
            // pnlSP1
            // 
            this.pnlSP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSP1.Location = new System.Drawing.Point(12, 102);
            this.pnlSP1.Name = "pnlSP1";
            this.pnlSP1.Size = new System.Drawing.Size(560, 4);
            this.pnlSP1.TabIndex = 29;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(416, 172);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // fibProjectFont
            // 
            this.fibProjectFont.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProjectFont.Caption = "字体";
            this.fibProjectFont.CaptionWidth = 60;
            this.fibProjectFont.Filter = "字体文件 (*.ttf)|*.ttf";
            this.fibProjectFont.FolderLimit = "";
            this.fibProjectFont.InputValue = "";
            this.fibProjectFont.Location = new System.Drawing.Point(12, 72);
            this.fibProjectFont.Name = "fibProjectFont";
            this.fibProjectFont.Size = new System.Drawing.Size(560, 24);
            this.fibProjectFont.TabIndex = 39;
            // 
            // fibProjectPath
            // 
            this.fibProjectPath.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProjectPath.Caption = "位置";
            this.fibProjectPath.CaptionWidth = 60;
            this.fibProjectPath.Enabled = false;
            this.fibProjectPath.FolderLimit = "";
            this.fibProjectPath.InputValue = "";
            this.fibProjectPath.Location = new System.Drawing.Point(12, 42);
            this.fibProjectPath.Name = "fibProjectPath";
            this.fibProjectPath.Size = new System.Drawing.Size(560, 24);
            this.fibProjectPath.TabIndex = 40;
            // 
            // tibProjectName
            // 
            this.tibProjectName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibProjectName.Caption = "名称";
            this.tibProjectName.CaptionWidth = 60;
            this.tibProjectName.Enabled = false;
            this.tibProjectName.InputValue = "";
            this.tibProjectName.Location = new System.Drawing.Point(12, 12);
            this.tibProjectName.Name = "tibProjectName";
            this.tibProjectName.Size = new System.Drawing.Size(300, 24);
            this.tibProjectName.TabIndex = 41;
            // 
            // fibAssetsFolder
            // 
            this.fibAssetsFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibAssetsFolder.Caption = "资源位置";
            this.fibAssetsFolder.CaptionWidth = 100;
            this.fibAssetsFolder.FolderLimit = "";
            this.fibAssetsFolder.InputValue = "";
            this.fibAssetsFolder.Location = new System.Drawing.Point(12, 112);
            this.fibAssetsFolder.Name = "fibAssetsFolder";
            this.fibAssetsFolder.Size = new System.Drawing.Size(560, 24);
            this.fibAssetsFolder.TabIndex = 42;
            // 
            // fibBuildFolder
            // 
            this.fibBuildFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibBuildFolder.Caption = "生成位置";
            this.fibBuildFolder.CaptionWidth = 100;
            this.fibBuildFolder.FolderLimit = "";
            this.fibBuildFolder.InputValue = "";
            this.fibBuildFolder.Location = new System.Drawing.Point(12, 142);
            this.fibBuildFolder.Name = "fibBuildFolder";
            this.fibBuildFolder.Size = new System.Drawing.Size(560, 24);
            this.fibBuildFolder.TabIndex = 43;
            // 
            // ProjectPropertyForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 207);
            this.Controls.Add(this.fibBuildFolder);
            this.Controls.Add(this.fibAssetsFolder);
            this.Controls.Add(this.tibProjectName);
            this.Controls.Add(this.fibProjectPath);
            this.Controls.Add(this.fibProjectFont);
            this.Controls.Add(this.pnlSP1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectPropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工程属性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSP1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.FileInputBox fibProjectFont;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibProjectPath;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibProjectName;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibAssetsFolder;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibBuildFolder;
    }
}