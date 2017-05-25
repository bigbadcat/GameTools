namespace T002.Forms
{
    partial class InterfacePropertyForm
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
            this.tibName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.nibWidth = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibHeight = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibCode = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.SuspendLayout();
            // 
            // pnlSP1
            // 
            this.pnlSP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSP1.Location = new System.Drawing.Point(12, 102);
            this.pnlSP1.Name = "pnlSP1";
            this.pnlSP1.Size = new System.Drawing.Size(360, 4);
            this.pnlSP1.TabIndex = 45;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(216, 107);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 43;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.tibName.Size = new System.Drawing.Size(360, 24);
            this.tibName.TabIndex = 48;
            // 
            // nibWidth
            // 
            this.nibWidth.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibWidth.Caption = "宽度";
            this.nibWidth.CaptionWidth = 60;
            this.nibWidth.InputValue = 0F;
            this.nibWidth.Location = new System.Drawing.Point(12, 72);
            this.nibWidth.Name = "nibWidth";
            this.nibWidth.Size = new System.Drawing.Size(177, 24);
            this.nibWidth.TabIndex = 50;
            // 
            // nibHeight
            // 
            this.nibHeight.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibHeight.Caption = "高度";
            this.nibHeight.CaptionWidth = 60;
            this.nibHeight.InputValue = 0F;
            this.nibHeight.Location = new System.Drawing.Point(195, 72);
            this.nibHeight.Name = "nibHeight";
            this.nibHeight.Size = new System.Drawing.Size(177, 24);
            this.nibHeight.TabIndex = 51;
            // 
            // nibCode
            // 
            this.nibCode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibCode.Caption = "编号";
            this.nibCode.CaptionWidth = 60;
            this.nibCode.InputValue = 0F;
            this.nibCode.Location = new System.Drawing.Point(12, 42);
            this.nibCode.Name = "nibCode";
            this.nibCode.Size = new System.Drawing.Size(360, 24);
            this.nibCode.TabIndex = 52;
            // 
            // InterfacePropertyForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 142);
            this.Controls.Add(this.nibCode);
            this.Controls.Add(this.nibHeight);
            this.Controls.Add(this.nibWidth);
            this.Controls.Add(this.tibName);
            this.Controls.Add(this.pnlSP1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterfacePropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "界面属性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSP1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibName;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibWidth;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibHeight;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibCode;
    }
}