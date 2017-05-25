namespace T002.Forms
{
    partial class NewControlForm
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
            this.iibType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.tibName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tibConstVar = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(216, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // iibType
            // 
            this.iibType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibType.Caption = "标题";
            this.iibType.CaptionWidth = 60;
            this.iibType.InputIndex = -1;
            this.iibType.InputItems = new string[] {
        "容器",
        "图像",
        "标签",
        "文本域",
        "文本框",
        "进度条",
        "滑动条",
        "普通按钮",
        "单选按钮",
        "复选按钮",
        "数字图像",
        "图像数字",
        "滑动表格",
        "翻页表格",
        "滚动面板",
        "粒子视图",
        "Spine视图"};
            this.iibType.Location = new System.Drawing.Point(12, 12);
            this.iibType.Name = "iibType";
            this.iibType.Size = new System.Drawing.Size(230, 24);
            this.iibType.TabIndex = 26;
            // 
            // tibName
            // 
            this.tibName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibName.Caption = "名称";
            this.tibName.CaptionWidth = 60;
            this.tibName.InputValue = "";
            this.tibName.Location = new System.Drawing.Point(12, 42);
            this.tibName.Name = "tibName";
            this.tibName.Size = new System.Drawing.Size(360, 24);
            this.tibName.TabIndex = 27;
            // 
            // tibConstVar
            // 
            this.tibConstVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibConstVar.Caption = "常量";
            this.tibConstVar.CaptionWidth = 60;
            this.tibConstVar.InputValue = "";
            this.tibConstVar.Location = new System.Drawing.Point(12, 72);
            this.tibConstVar.Name = "tibConstVar";
            this.tibConstVar.Size = new System.Drawing.Size(360, 24);
            this.tibConstVar.TabIndex = 28;
            // 
            // NewControlForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 137);
            this.Controls.Add(this.tibConstVar);
            this.Controls.Add(this.tibName);
            this.Controls.Add(this.iibType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加控件";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private XuXiang.Tool.ControlLibrary.ItemInputBox iibType;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibName;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibConstVar;

    }
}