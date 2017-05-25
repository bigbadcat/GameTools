namespace T002.Forms
{
    partial class InterfaceListForm
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
            this.lvOutPutList = new System.Windows.Forms.ListView();
            this.chOutPut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvOutPutList
            // 
            this.lvOutPutList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvOutPutList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chOutPut,
            this.chPath});
            this.lvOutPutList.FullRowSelect = true;
            this.lvOutPutList.GridLines = true;
            this.lvOutPutList.Location = new System.Drawing.Point(12, 12);
            this.lvOutPutList.Name = "lvOutPutList";
            this.lvOutPutList.Size = new System.Drawing.Size(570, 348);
            this.lvOutPutList.TabIndex = 0;
            this.lvOutPutList.UseCompatibleStateImageBehavior = false;
            this.lvOutPutList.View = System.Windows.Forms.View.Details;
            // 
            // chOutPut
            // 
            this.chOutPut.Text = "输出名称";
            this.chOutPut.Width = 150;
            // 
            // chPath
            // 
            this.chPath.Text = "路径";
            this.chPath.Width = 400;
            // 
            // InterfaceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 372);
            this.Controls.Add(this.lvOutPutList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterfaceListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "界面列表";
            this.Load += new System.EventHandler(this.InterfaceListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvOutPutList;
        private System.Windows.Forms.ColumnHeader chOutPut;
        private System.Windows.Forms.ColumnHeader chPath;
    }
}