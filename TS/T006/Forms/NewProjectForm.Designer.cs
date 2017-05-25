namespace T006.Forms
{
    partial class NewProjectForm
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
            this.tibProjectName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.fibProjectPath = new XuXiang.Tool.ControlLibrary.FolderInputBox();
            this.sibSceneSize = new XuXiang.Tool.ControlLibrary.SizeInputBox();
            this.nibShowFPS = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(387, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(306, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tibProjectName
            // 
            this.tibProjectName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibProjectName.Caption = "工程名称";
            this.tibProjectName.CaptionWidth = 100;
            this.tibProjectName.InputValue = "";
            this.tibProjectName.Location = new System.Drawing.Point(12, 12);
            this.tibProjectName.Name = "tibProjectName";
            this.tibProjectName.Size = new System.Drawing.Size(450, 24);
            this.tibProjectName.TabIndex = 50;
            // 
            // fibProjectPath
            // 
            this.fibProjectPath.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProjectPath.Caption = "工程路径";
            this.fibProjectPath.CaptionWidth = 100;
            this.fibProjectPath.FolderLimit = "";
            this.fibProjectPath.InputValue = "";
            this.fibProjectPath.Location = new System.Drawing.Point(12, 42);
            this.fibProjectPath.Name = "fibProjectPath";
            this.fibProjectPath.Size = new System.Drawing.Size(450, 24);
            this.fibProjectPath.TabIndex = 51;
            // 
            // sibSceneSize
            // 
            this.sibSceneSize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sibSceneSize.Caption = "场景大小";
            this.sibSceneSize.CaptionWidth = 100;
            this.sibSceneSize.InputValue = new System.Drawing.Size(100, 100);
            this.sibSceneSize.Location = new System.Drawing.Point(12, 72);
            this.sibSceneSize.Name = "sibSceneSize";
            this.sibSceneSize.Size = new System.Drawing.Size(250, 24);
            this.sibSceneSize.TabIndex = 52;
            // 
            // nibShowFPS
            // 
            this.nibShowFPS.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibShowFPS.Caption = "运行帧率";
            this.nibShowFPS.CaptionWidth = 100;
            this.nibShowFPS.InputValue = 0F;
            this.nibShowFPS.Location = new System.Drawing.Point(268, 72);
            this.nibShowFPS.Name = "nibShowFPS";
            this.nibShowFPS.Size = new System.Drawing.Size(194, 24);
            this.nibShowFPS.TabIndex = 53;
            // 
            // NewProjectForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(474, 137);
            this.Controls.Add(this.nibShowFPS);
            this.Controls.Add(this.sibSceneSize);
            this.Controls.Add(this.fibProjectPath);
            this.Controls.Add(this.tibProjectName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建工程";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibProjectName;
        private XuXiang.Tool.ControlLibrary.FolderInputBox fibProjectPath;
        private XuXiang.Tool.ControlLibrary.SizeInputBox sibSceneSize;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibShowFPS;
    }
}