namespace T006.Forms
{
    partial class NewEffectForm
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
            this.tibName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.fibImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.iibType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tibName
            // 
            this.tibName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibName.Caption = "效果名称";
            this.tibName.CaptionWidth = 100;
            this.tibName.InputValue = "";
            this.tibName.Location = new System.Drawing.Point(12, 12);
            this.tibName.Name = "tibName";
            this.tibName.Size = new System.Drawing.Size(349, 24);
            this.tibName.TabIndex = 0;
            // 
            // fibImage
            // 
            this.fibImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibImage.Caption = "粒子图像";
            this.fibImage.CaptionWidth = 100;
            this.fibImage.Filter = "";
            this.fibImage.FolderLimit = "";
            this.fibImage.InputValue = "";
            this.fibImage.Location = new System.Drawing.Point(12, 42);
            this.fibImage.Name = "fibImage";
            this.fibImage.Size = new System.Drawing.Size(350, 24);
            this.fibImage.TabIndex = 1;
            // 
            // iibType
            // 
            this.iibType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibType.Caption = "模版效果";
            this.iibType.CaptionWidth = 100;
            this.iibType.InputIndex = -1;
            this.iibType.InputItems = new string[] {
        "漩涡",
        "火焰",
        "花朵",
        "星系",
        "雪花"};
            this.iibType.Location = new System.Drawing.Point(12, 72);
            this.iibType.Name = "iibType";
            this.iibType.Size = new System.Drawing.Size(349, 24);
            this.iibType.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(286, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(205, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // NewEffectForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(373, 137);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.iibType);
            this.Controls.Add(this.fibImage);
            this.Controls.Add(this.tibName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewEffectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建效果";
            this.ResumeLayout(false);

        }

        #endregion

        private XuXiang.Tool.ControlLibrary.TextInputBox tibName;
        private XuXiang.Tool.ControlLibrary.FileInputBox fibImage;
        private XuXiang.Tool.ControlLibrary.ItemInputBox iibType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}