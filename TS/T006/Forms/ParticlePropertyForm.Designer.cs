namespace T006.Forms
{
    partial class ParticlePropertyForm
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
            this.tibOutCode = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tibFileName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(247, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(166, 72);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 50;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tibOutCode
            // 
            this.tibOutCode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibOutCode.Caption = "输出编号";
            this.tibOutCode.CaptionWidth = 100;
            this.tibOutCode.InputValue = "";
            this.tibOutCode.Location = new System.Drawing.Point(12, 42);
            this.tibOutCode.Name = "tibOutCode";
            this.tibOutCode.Size = new System.Drawing.Size(310, 24);
            this.tibOutCode.TabIndex = 56;
            // 
            // tibFileName
            // 
            this.tibFileName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibFileName.Caption = "文件名称";
            this.tibFileName.CaptionWidth = 100;
            this.tibFileName.Enabled = false;
            this.tibFileName.InputValue = "";
            this.tibFileName.Location = new System.Drawing.Point(12, 12);
            this.tibFileName.Name = "tibFileName";
            this.tibFileName.Size = new System.Drawing.Size(310, 24);
            this.tibFileName.TabIndex = 55;
            // 
            // ParticlePropertyForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 107);
            this.Controls.Add(this.tibOutCode);
            this.Controls.Add(this.tibFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParticlePropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "粒子属性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibOutCode;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibFileName;
    }
}