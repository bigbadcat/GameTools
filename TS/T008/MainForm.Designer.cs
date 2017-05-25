﻿namespace T008
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.ttvDataList = new XuXiang.Tool.ControlLibrary.TristateTreeView();
            this.pbExport = new System.Windows.Forms.ProgressBar();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEnum = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.fibConfig = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.btnBuildScript = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ttvDataList);
            this.splitContainer.Panel1.SizeChanged += new System.EventHandler(this.splitContainer_Panel1_SizeChanged);
            this.splitContainer.Panel1MinSize = 200;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnBuildScript);
            this.splitContainer.Panel2.Controls.Add(this.pbExport);
            this.splitContainer.Panel2.Controls.Add(this.btnClear);
            this.splitContainer.Panel2.Controls.Add(this.rtbLog);
            this.splitContainer.Panel2.Controls.Add(this.btnExport);
            this.splitContainer.Panel2.Controls.Add(this.btnEnum);
            this.splitContainer.Panel2.Controls.Add(this.btnRefresh);
            this.splitContainer.Panel2.Controls.Add(this.fibConfig);
            this.splitContainer.Panel2.SizeChanged += new System.EventHandler(this.splitContainer_Panel2_SizeChanged);
            this.splitContainer.Panel2MinSize = 270;
            this.splitContainer.Size = new System.Drawing.Size(784, 561);
            this.splitContainer.SplitterDistance = 237;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 4;
            this.splitContainer.TabStop = false;
            // 
            // ttvDataList
            // 
            this.ttvDataList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ttvDataList.Font = new System.Drawing.Font("楷体", 14F);
            this.ttvDataList.Location = new System.Drawing.Point(12, 12);
            this.ttvDataList.Margin = new System.Windows.Forms.Padding(5);
            this.ttvDataList.Name = "ttvDataList";
            this.ttvDataList.Size = new System.Drawing.Size(213, 537);
            this.ttvDataList.TabIndex = 0;
            // 
            // pbExport
            // 
            this.pbExport.Location = new System.Drawing.Point(12, 521);
            this.pbExport.MarqueeAnimationSpeed = 1000;
            this.pbExport.Name = "pbExport";
            this.pbExport.Size = new System.Drawing.Size(411, 23);
            this.pbExport.Step = 1;
            this.pbExport.TabIndex = 6;
            this.pbExport.Value = 20;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(429, 517);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 32);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清 空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbLog.Location = new System.Drawing.Point(12, 80);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(517, 431);
            this.rtbLog.TabIndex = 4;
            this.rtbLog.Text = "介里是输出日志";
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(224, 42);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 32);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导 出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEnum
            // 
            this.btnEnum.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnum.Location = new System.Drawing.Point(118, 42);
            this.btnEnum.Name = "btnEnum";
            this.btnEnum.Size = new System.Drawing.Size(100, 32);
            this.btnEnum.TabIndex = 2;
            this.btnEnum.Text = "枚 举";
            this.btnEnum.UseVisualStyleBackColor = true;
            this.btnEnum.Click += new System.EventHandler(this.btnEnum_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 42);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷 新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // fibConfig
            // 
            this.fibConfig.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibConfig.Caption = "配置文件";
            this.fibConfig.CaptionWidth = 100;
            this.fibConfig.Filter = "图像文件 (*.xml)|*.xml";
            this.fibConfig.FolderLimit = "";
            this.fibConfig.InputValue = "";
            this.fibConfig.Location = new System.Drawing.Point(12, 12);
            this.fibConfig.Name = "fibConfig";
            this.fibConfig.Size = new System.Drawing.Size(517, 24);
            this.fibConfig.TabIndex = 0;
            this.fibConfig.Inputed += new System.EventHandler(this.fibConfig_Inputed);
            // 
            // btnBuildScript
            // 
            this.btnBuildScript.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBuildScript.Location = new System.Drawing.Point(330, 42);
            this.btnBuildScript.Name = "btnBuildScript";
            this.btnBuildScript.Size = new System.Drawing.Size(100, 32);
            this.btnBuildScript.TabIndex = 7;
            this.btnBuildScript.Text = "生成代码";
            this.btnBuildScript.UseVisualStyleBackColor = true;
            this.btnBuildScript.Click += new System.EventHandler(this.btnBuildScript_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导出";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private XuXiang.Tool.ControlLibrary.FileInputBox fibConfig;
        private System.Windows.Forms.SplitContainer splitContainer;
        private XuXiang.Tool.ControlLibrary.TristateTreeView ttvDataList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEnum;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ProgressBar pbExport;
        private System.Windows.Forms.Button btnBuildScript;
    }
}

