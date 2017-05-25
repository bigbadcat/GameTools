namespace T006.Forms
{
    partial class NewParticleFileForm
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
            this.tibFileName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tibOutCode = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.iibEffectType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibEffectImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tibEffectName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(206, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 43;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tibFileName
            // 
            this.tibFileName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibFileName.Caption = "文件名称";
            this.tibFileName.CaptionWidth = 100;
            this.tibFileName.InputValue = "";
            this.tibFileName.Location = new System.Drawing.Point(12, 12);
            this.tibFileName.Name = "tibFileName";
            this.tibFileName.Size = new System.Drawing.Size(350, 24);
            this.tibFileName.TabIndex = 48;
            // 
            // tibOutCode
            // 
            this.tibOutCode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibOutCode.Caption = "输出编号";
            this.tibOutCode.CaptionWidth = 100;
            this.tibOutCode.InputValue = "";
            this.tibOutCode.Location = new System.Drawing.Point(12, 42);
            this.tibOutCode.Name = "tibOutCode";
            this.tibOutCode.Size = new System.Drawing.Size(350, 24);
            this.tibOutCode.TabIndex = 49;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 1);
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // iibEffectType
            // 
            this.iibEffectType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibEffectType.Caption = "模版效果";
            this.iibEffectType.CaptionWidth = 100;
            this.iibEffectType.InputIndex = -1;
            this.iibEffectType.InputItems = new string[] {
        "漩涡",
        "火焰",
        "花朵",
        "星系",
        "雪花"};
            this.iibEffectType.Location = new System.Drawing.Point(12, 139);
            this.iibEffectType.Name = "iibEffectType";
            this.iibEffectType.Size = new System.Drawing.Size(350, 24);
            this.iibEffectType.TabIndex = 53;
            // 
            // fibEffectImage
            // 
            this.fibEffectImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibEffectImage.Caption = "粒子图像";
            this.fibEffectImage.CaptionWidth = 100;
            this.fibEffectImage.Filter = "";
            this.fibEffectImage.FolderLimit = "";
            this.fibEffectImage.InputValue = "";
            this.fibEffectImage.Location = new System.Drawing.Point(12, 109);
            this.fibEffectImage.Name = "fibEffectImage";
            this.fibEffectImage.Size = new System.Drawing.Size(350, 24);
            this.fibEffectImage.TabIndex = 52;
            // 
            // tibEffectName
            // 
            this.tibEffectName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibEffectName.Caption = "效果名称";
            this.tibEffectName.CaptionWidth = 100;
            this.tibEffectName.InputValue = "";
            this.tibEffectName.Location = new System.Drawing.Point(12, 79);
            this.tibEffectName.Name = "tibEffectName";
            this.tibEffectName.Size = new System.Drawing.Size(350, 24);
            this.tibEffectName.TabIndex = 51;
            // 
            // NewParticleFileForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 204);
            this.Controls.Add(this.iibEffectType);
            this.Controls.Add(this.fibEffectImage);
            this.Controls.Add(this.tibEffectName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tibOutCode);
            this.Controls.Add(this.tibFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewParticleFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建粒子";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibFileName;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibOutCode;
        private System.Windows.Forms.PictureBox pictureBox1;
        private XuXiang.Tool.ControlLibrary.ItemInputBox iibEffectType;
        private XuXiang.Tool.ControlLibrary.FileInputBox fibEffectImage;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibEffectName;
    }
}