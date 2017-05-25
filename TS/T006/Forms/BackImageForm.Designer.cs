namespace T006.Forms
{
    partial class BackImageForm
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
            this.lvBackList = new System.Windows.Forms.ListView();
            this.chBack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            this.SuspendLayout();
            // 
            // lvBackList
            // 
            this.lvBackList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvBackList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBack});
            this.lvBackList.FullRowSelect = true;
            this.lvBackList.GridLines = true;
            this.lvBackList.Location = new System.Drawing.Point(12, 12);
            this.lvBackList.MultiSelect = false;
            this.lvBackList.Name = "lvBackList";
            this.lvBackList.Size = new System.Drawing.Size(200, 300);
            this.lvBackList.TabIndex = 2;
            this.lvBackList.UseCompatibleStateImageBehavior = false;
            this.lvBackList.View = System.Windows.Forms.View.Details;
            this.lvBackList.SelectedIndexChanged += new System.EventHandler(this.lvBackList_SelectedIndexChanged);
            // 
            // chBack
            // 
            this.chBack.Text = "图像";
            this.chBack.Width = 200;
            // 
            // pbBack
            // 
            this.pbBack.BackColor = System.Drawing.Color.Black;
            this.pbBack.InitialImage = null;
            this.pbBack.Location = new System.Drawing.Point(218, 12);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(400, 300);
            this.pbBack.TabIndex = 4;
            this.pbBack.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(543, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 46;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(462, 318);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 45;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(12, 318);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 47;
            this.btnNo.Text = "不使用(&N)";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // BackImageForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(630, 353);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pbBack);
            this.Controls.Add(this.lvBackList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "背景图";
            this.Load += new System.EventHandler(this.BackImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvBackList;
        private System.Windows.Forms.ColumnHeader chBack;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnNo;
    }
}