namespace T007
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fibFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.btnRender = new System.Windows.Forms.Button();
            this.rbZoom1 = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.rbZoom2 = new System.Windows.Forms.RadioButton();
            this.rbZoom4 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // fibFile
            // 
            this.fibFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibFile.Caption = "文件";
            this.fibFile.CaptionWidth = 60;
            this.fibFile.Filter = "文本文件 (*.txt)|*.txt";
            this.fibFile.FolderLimit = "";
            this.fibFile.InputValue = "";
            this.fibFile.Location = new System.Drawing.Point(12, 12);
            this.fibFile.Name = "fibFile";
            this.fibFile.Size = new System.Drawing.Size(512, 24);
            this.fibFile.TabIndex = 0;
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(530, 42);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(117, 46);
            this.btnRender.TabIndex = 1;
            this.btnRender.Text = "渲 染";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // rbZoom1
            // 
            this.rbZoom1.AutoSize = true;
            this.rbZoom1.Checked = true;
            this.rbZoom1.Location = new System.Drawing.Point(530, 20);
            this.rbZoom1.Name = "rbZoom1";
            this.rbZoom1.Size = new System.Drawing.Size(35, 16);
            this.rbZoom1.TabIndex = 2;
            this.rbZoom1.TabStop = true;
            this.rbZoom1.Text = "x1";
            this.rbZoom1.UseVisualStyleBackColor = true;
            this.rbZoom1.CheckedChanged += new System.EventHandler(this.rbZoom1_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(530, 94);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(117, 46);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导 出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Black;
            this.pbImage.Location = new System.Drawing.Point(12, 42);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(512, 512);
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // rbZoom2
            // 
            this.rbZoom2.AutoSize = true;
            this.rbZoom2.Location = new System.Drawing.Point(571, 20);
            this.rbZoom2.Name = "rbZoom2";
            this.rbZoom2.Size = new System.Drawing.Size(35, 16);
            this.rbZoom2.TabIndex = 5;
            this.rbZoom2.Text = "x2";
            this.rbZoom2.UseVisualStyleBackColor = true;
            this.rbZoom2.CheckedChanged += new System.EventHandler(this.rbZoom2_CheckedChanged);
            // 
            // rbZoom4
            // 
            this.rbZoom4.AutoSize = true;
            this.rbZoom4.Location = new System.Drawing.Point(612, 20);
            this.rbZoom4.Name = "rbZoom4";
            this.rbZoom4.Size = new System.Drawing.Size(35, 16);
            this.rbZoom4.TabIndex = 6;
            this.rbZoom4.Text = "x4";
            this.rbZoom4.UseVisualStyleBackColor = true;
            this.rbZoom4.CheckedChanged += new System.EventHandler(this.rbZoom4_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 566);
            this.Controls.Add(this.rbZoom4);
            this.Controls.Add(this.rbZoom2);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rbZoom1);
            this.Controls.Add(this.btnRender);
            this.Controls.Add(this.fibFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MCMap";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XuXiang.Tool.ControlLibrary.FileInputBox fibFile;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.RadioButton rbZoom1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.RadioButton rbZoom2;
        private System.Windows.Forms.RadioButton rbZoom4;
    }
}

