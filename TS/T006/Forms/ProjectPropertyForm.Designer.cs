namespace T006.Forms
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pbSP1 = new System.Windows.Forms.PictureBox();
            this.sibSceneSize = new XuXiang.Tool.ControlLibrary.SizeInputBox();
            this.fibProjectPath = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.tibProjectName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.fibBuildFolder = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.fibAssetsFolder = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.nibShowFPS = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSP1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(487, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(406, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pbSP1
            // 
            this.pbSP1.BackColor = System.Drawing.Color.Black;
            this.pbSP1.Location = new System.Drawing.Point(12, 102);
            this.pbSP1.Name = "pbSP1";
            this.pbSP1.Size = new System.Drawing.Size(550, 1);
            this.pbSP1.TabIndex = 32;
            this.pbSP1.TabStop = false;
            // 
            // sibSceneSize
            // 
            this.sibSceneSize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sibSceneSize.Caption = "场景大小";
            this.sibSceneSize.CaptionWidth = 100;
            this.sibSceneSize.InputValue = new System.Drawing.Size(100, 100);
            this.sibSceneSize.Location = new System.Drawing.Point(12, 72);
            this.sibSceneSize.Name = "sibSceneSize";
            this.sibSceneSize.Size = new System.Drawing.Size(350, 24);
            this.sibSceneSize.TabIndex = 56;
            // 
            // fibProjectPath
            // 
            this.fibProjectPath.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProjectPath.Caption = "工程目录";
            this.fibProjectPath.CaptionWidth = 100;
            this.fibProjectPath.Enabled = false;
            this.fibProjectPath.FolderLimit = "";
            this.fibProjectPath.InputValue = "";
            this.fibProjectPath.Location = new System.Drawing.Point(12, 42);
            this.fibProjectPath.Name = "fibProjectPath";
            this.fibProjectPath.Size = new System.Drawing.Size(550, 24);
            this.fibProjectPath.TabIndex = 55;
            // 
            // tibProjectName
            // 
            this.tibProjectName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibProjectName.Caption = "工程名称";
            this.tibProjectName.CaptionWidth = 100;
            this.tibProjectName.Enabled = false;
            this.tibProjectName.InputValue = "";
            this.tibProjectName.Location = new System.Drawing.Point(12, 12);
            this.tibProjectName.Name = "tibProjectName";
            this.tibProjectName.Size = new System.Drawing.Size(550, 24);
            this.tibProjectName.TabIndex = 54;
            // 
            // fibBuildFolder
            // 
            this.fibBuildFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibBuildFolder.Caption = "生成目录";
            this.fibBuildFolder.CaptionWidth = 100;
            this.fibBuildFolder.FolderLimit = "";
            this.fibBuildFolder.InputValue = "";
            this.fibBuildFolder.Location = new System.Drawing.Point(12, 139);
            this.fibBuildFolder.Name = "fibBuildFolder";
            this.fibBuildFolder.Size = new System.Drawing.Size(550, 24);
            this.fibBuildFolder.TabIndex = 57;
            // 
            // fibAssetsFolder
            // 
            this.fibAssetsFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibAssetsFolder.Caption = "资源目录";
            this.fibAssetsFolder.CaptionWidth = 100;
            this.fibAssetsFolder.FolderLimit = "";
            this.fibAssetsFolder.InputValue = "";
            this.fibAssetsFolder.Location = new System.Drawing.Point(12, 109);
            this.fibAssetsFolder.Name = "fibAssetsFolder";
            this.fibAssetsFolder.Size = new System.Drawing.Size(550, 24);
            this.fibAssetsFolder.TabIndex = 58;
            // 
            // nibShowFPS
            // 
            this.nibShowFPS.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibShowFPS.Caption = "运行帧率";
            this.nibShowFPS.CaptionWidth = 100;
            this.nibShowFPS.InputValue = 40F;
            this.nibShowFPS.Location = new System.Drawing.Point(368, 72);
            this.nibShowFPS.Name = "nibShowFPS";
            this.nibShowFPS.Size = new System.Drawing.Size(194, 24);
            this.nibShowFPS.TabIndex = 59;
            // 
            // ProjectPropertyForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(574, 204);
            this.Controls.Add(this.nibShowFPS);
            this.Controls.Add(this.fibAssetsFolder);
            this.Controls.Add(this.fibBuildFolder);
            this.Controls.Add(this.sibSceneSize);
            this.Controls.Add(this.fibProjectPath);
            this.Controls.Add(this.tibProjectName);
            this.Controls.Add(this.pbSP1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectPropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工程属性";
            ((System.ComponentModel.ISupportInitialize)(this.pbSP1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pbSP1;
        private XuXiang.Tool.ControlLibrary.SizeInputBox sibSceneSize;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibProjectPath;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibProjectName;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibBuildFolder;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibAssetsFolder;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibShowFPS;
    }
}