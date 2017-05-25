namespace T006.Forms
{
    partial class EffectPropertyForm
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
            this.tbcProperty = new System.Windows.Forms.TabControl();
            this.tbpParticleSystem = new System.Windows.Forms.TabPage();
            this.lbBasicNote = new System.Windows.Forms.Label();
            this.pibParticlePosition = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.tbpBasic = new System.Windows.Forms.TabPage();
            this.fibBasicImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tibBasicName = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.nibBasicLifeVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibBasicLife = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibBasicAngleVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibBasicAngle = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibBasicYScale = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibBasicXScale = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.pibBasicPositionVar = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.pibBasicPosition = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.tbpRun = new System.Windows.Forms.TabPage();
            this.nibRunMax = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRunRate = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibRunMode = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.iibRunPositionType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.nibRunDuration = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRunDelay = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpColor = new System.Windows.Forms.TabPage();
            this.cibColorEndVar = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibColorEnd = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibColorStartVar = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibColorStart = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.tbpSizeSpin = new System.Windows.Forms.TabPage();
            this.nibSpinEndVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSpinEnd = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSpinStartVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSpinStart = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSizeEndVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSizeEnd = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSizeStartVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSizeStart = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpGravity = new System.Windows.Forms.TabPage();
            this.nibGravityTangentialAccelVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibGravityTangentialAccel = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibGravityRadialAccelVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibGravityRadialAccel = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibGravitySpeedVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibGravitySpeed = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.pibGravityAccel = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.tbpRadius = new System.Windows.Forms.TabPage();
            this.nibRadiusRotateVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRadiusRotate = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRadiusEndVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRadiusEnd = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRadiusStartVar = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibRadiusStart = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbcProperty.SuspendLayout();
            this.tbpParticleSystem.SuspendLayout();
            this.tbpBasic.SuspendLayout();
            this.tbpRun.SuspendLayout();
            this.tbpColor.SuspendLayout();
            this.tbpSizeSpin.SuspendLayout();
            this.tbpGravity.SuspendLayout();
            this.tbpRadius.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcProperty
            // 
            this.tbcProperty.Controls.Add(this.tbpParticleSystem);
            this.tbcProperty.Controls.Add(this.tbpBasic);
            this.tbcProperty.Controls.Add(this.tbpRun);
            this.tbcProperty.Controls.Add(this.tbpColor);
            this.tbcProperty.Controls.Add(this.tbpSizeSpin);
            this.tbcProperty.Controls.Add(this.tbpGravity);
            this.tbcProperty.Controls.Add(this.tbpRadius);
            this.tbcProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcProperty.Location = new System.Drawing.Point(0, 0);
            this.tbcProperty.Name = "tbcProperty";
            this.tbcProperty.SelectedIndex = 0;
            this.tbcProperty.Size = new System.Drawing.Size(982, 163);
            this.tbcProperty.TabIndex = 1;
            // 
            // tbpParticleSystem
            // 
            this.tbpParticleSystem.Controls.Add(this.lbBasicNote);
            this.tbpParticleSystem.Controls.Add(this.pibParticlePosition);
            this.tbpParticleSystem.Location = new System.Drawing.Point(4, 22);
            this.tbpParticleSystem.Name = "tbpParticleSystem";
            this.tbpParticleSystem.Size = new System.Drawing.Size(974, 137);
            this.tbpParticleSystem.TabIndex = 1;
            this.tbpParticleSystem.Text = "粒子系统";
            this.tbpParticleSystem.UseVisualStyleBackColor = true;
            // 
            // lbBasicNote
            // 
            this.lbBasicNote.AutoSize = true;
            this.lbBasicNote.Font = new System.Drawing.Font("楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBasicNote.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbBasicNote.Location = new System.Drawing.Point(7, 39);
            this.lbBasicNote.Name = "lbBasicNote";
            this.lbBasicNote.Size = new System.Drawing.Size(692, 18);
            this.lbBasicNote.TabIndex = 3;
            this.lbBasicNote.Text = "此位置为粒子系统的编辑时显示的位置，不写入输出文件中，游戏中按具体需要设置。";
            // 
            // pibParticlePosition
            // 
            this.pibParticlePosition.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibParticlePosition.Caption = "系统位置";
            this.pibParticlePosition.CaptionWidth = 100;
            this.pibParticlePosition.InputValue = new System.Drawing.Point(0, 0);
            this.pibParticlePosition.Location = new System.Drawing.Point(6, 6);
            this.pibParticlePosition.Name = "pibParticlePosition";
            this.pibParticlePosition.Size = new System.Drawing.Size(200, 24);
            this.pibParticlePosition.TabIndex = 2;
            this.pibParticlePosition.Inputed += new System.EventHandler(this.pibParticlePosition_Inputed);
            // 
            // tbpBasic
            // 
            this.tbpBasic.AutoScroll = true;
            this.tbpBasic.BackColor = System.Drawing.Color.White;
            this.tbpBasic.Controls.Add(this.fibBasicImage);
            this.tbpBasic.Controls.Add(this.tibBasicName);
            this.tbpBasic.Controls.Add(this.nibBasicLifeVar);
            this.tbpBasic.Controls.Add(this.nibBasicLife);
            this.tbpBasic.Controls.Add(this.nibBasicAngleVar);
            this.tbpBasic.Controls.Add(this.nibBasicAngle);
            this.tbpBasic.Controls.Add(this.nibBasicYScale);
            this.tbpBasic.Controls.Add(this.nibBasicXScale);
            this.tbpBasic.Controls.Add(this.pibBasicPositionVar);
            this.tbpBasic.Controls.Add(this.pibBasicPosition);
            this.tbpBasic.Location = new System.Drawing.Point(4, 22);
            this.tbpBasic.Name = "tbpBasic";
            this.tbpBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBasic.Size = new System.Drawing.Size(974, 137);
            this.tbpBasic.TabIndex = 0;
            this.tbpBasic.Text = "基本属性";
            // 
            // fibBasicImage
            // 
            this.fibBasicImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibBasicImage.Caption = "图像";
            this.fibBasicImage.CaptionWidth = 60;
            this.fibBasicImage.Filter = "";
            this.fibBasicImage.FolderLimit = "";
            this.fibBasicImage.InputValue = "";
            this.fibBasicImage.Location = new System.Drawing.Point(162, 6);
            this.fibBasicImage.Name = "fibBasicImage";
            this.fibBasicImage.Size = new System.Drawing.Size(462, 24);
            this.fibBasicImage.TabIndex = 11;
            this.fibBasicImage.Inputed += new System.EventHandler(this.fibBasicImage_Inputed);
            // 
            // tibBasicName
            // 
            this.tibBasicName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibBasicName.Caption = "名称";
            this.tibBasicName.CaptionWidth = 60;
            this.tibBasicName.InputValue = "";
            this.tibBasicName.Location = new System.Drawing.Point(6, 6);
            this.tibBasicName.Name = "tibBasicName";
            this.tibBasicName.Size = new System.Drawing.Size(150, 24);
            this.tibBasicName.TabIndex = 10;
            this.tibBasicName.Inputed += new System.EventHandler(this.tibBasicName_Inputed);
            // 
            // nibBasicLifeVar
            // 
            this.nibBasicLifeVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicLifeVar.Caption = "波动";
            this.nibBasicLifeVar.CaptionWidth = 70;
            this.nibBasicLifeVar.InputValue = 0F;
            this.nibBasicLifeVar.Location = new System.Drawing.Point(474, 66);
            this.nibBasicLifeVar.Name = "nibBasicLifeVar";
            this.nibBasicLifeVar.Size = new System.Drawing.Size(150, 24);
            this.nibBasicLifeVar.TabIndex = 9;
            this.nibBasicLifeVar.Inputed += new System.EventHandler(this.nibBasicLifeVar_Inputed);
            // 
            // nibBasicLife
            // 
            this.nibBasicLife.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicLife.Caption = "生命";
            this.nibBasicLife.CaptionWidth = 70;
            this.nibBasicLife.InputValue = 0F;
            this.nibBasicLife.Location = new System.Drawing.Point(318, 66);
            this.nibBasicLife.Name = "nibBasicLife";
            this.nibBasicLife.Size = new System.Drawing.Size(150, 24);
            this.nibBasicLife.TabIndex = 8;
            this.nibBasicLife.Inputed += new System.EventHandler(this.nibBasicLife_Inputed);
            // 
            // nibBasicAngleVar
            // 
            this.nibBasicAngleVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicAngleVar.Caption = "波动";
            this.nibBasicAngleVar.CaptionWidth = 60;
            this.nibBasicAngleVar.InputValue = 0F;
            this.nibBasicAngleVar.Location = new System.Drawing.Point(162, 66);
            this.nibBasicAngleVar.Name = "nibBasicAngleVar";
            this.nibBasicAngleVar.Size = new System.Drawing.Size(150, 24);
            this.nibBasicAngleVar.TabIndex = 7;
            this.nibBasicAngleVar.Inputed += new System.EventHandler(this.nibBasicAngleVar_Inputed);
            // 
            // nibBasicAngle
            // 
            this.nibBasicAngle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicAngle.Caption = "角度";
            this.nibBasicAngle.CaptionWidth = 60;
            this.nibBasicAngle.InputValue = 0F;
            this.nibBasicAngle.Location = new System.Drawing.Point(6, 66);
            this.nibBasicAngle.Name = "nibBasicAngle";
            this.nibBasicAngle.Size = new System.Drawing.Size(150, 24);
            this.nibBasicAngle.TabIndex = 6;
            this.nibBasicAngle.Inputed += new System.EventHandler(this.nibBasicAngle_Inputed);
            // 
            // nibBasicYScale
            // 
            this.nibBasicYScale.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicYScale.Caption = "Y缩放";
            this.nibBasicYScale.CaptionWidth = 70;
            this.nibBasicYScale.InputValue = 0F;
            this.nibBasicYScale.Location = new System.Drawing.Point(474, 36);
            this.nibBasicYScale.Name = "nibBasicYScale";
            this.nibBasicYScale.Size = new System.Drawing.Size(150, 24);
            this.nibBasicYScale.TabIndex = 5;
            this.nibBasicYScale.Inputed += new System.EventHandler(this.nibBasicYScale_Inputed);
            // 
            // nibBasicXScale
            // 
            this.nibBasicXScale.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibBasicXScale.Caption = "X缩放";
            this.nibBasicXScale.CaptionWidth = 70;
            this.nibBasicXScale.InputValue = 0F;
            this.nibBasicXScale.Location = new System.Drawing.Point(318, 36);
            this.nibBasicXScale.Name = "nibBasicXScale";
            this.nibBasicXScale.Size = new System.Drawing.Size(150, 24);
            this.nibBasicXScale.TabIndex = 4;
            this.nibBasicXScale.Inputed += new System.EventHandler(this.nibBasicXScale_Inputed);
            // 
            // pibBasicPositionVar
            // 
            this.pibBasicPositionVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibBasicPositionVar.Caption = "波动";
            this.pibBasicPositionVar.CaptionWidth = 60;
            this.pibBasicPositionVar.InputValue = new System.Drawing.Point(0, 0);
            this.pibBasicPositionVar.Location = new System.Drawing.Point(162, 36);
            this.pibBasicPositionVar.Name = "pibBasicPositionVar";
            this.pibBasicPositionVar.Size = new System.Drawing.Size(150, 24);
            this.pibBasicPositionVar.TabIndex = 3;
            this.pibBasicPositionVar.Inputed += new System.EventHandler(this.pibBasicPositionVar_Inputed);
            // 
            // pibBasicPosition
            // 
            this.pibBasicPosition.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibBasicPosition.Caption = "位置";
            this.pibBasicPosition.CaptionWidth = 60;
            this.pibBasicPosition.InputValue = new System.Drawing.Point(0, 0);
            this.pibBasicPosition.Location = new System.Drawing.Point(6, 36);
            this.pibBasicPosition.Name = "pibBasicPosition";
            this.pibBasicPosition.Size = new System.Drawing.Size(150, 24);
            this.pibBasicPosition.TabIndex = 2;
            this.pibBasicPosition.Inputed += new System.EventHandler(this.pibBasicPosition_Inputed);
            // 
            // tbpRun
            // 
            this.tbpRun.Controls.Add(this.nibRunMax);
            this.tbpRun.Controls.Add(this.nibRunRate);
            this.tbpRun.Controls.Add(this.iibRunMode);
            this.tbpRun.Controls.Add(this.iibRunPositionType);
            this.tbpRun.Controls.Add(this.nibRunDuration);
            this.tbpRun.Controls.Add(this.nibRunDelay);
            this.tbpRun.Location = new System.Drawing.Point(4, 22);
            this.tbpRun.Name = "tbpRun";
            this.tbpRun.Size = new System.Drawing.Size(974, 137);
            this.tbpRun.TabIndex = 2;
            this.tbpRun.Text = "运行控制";
            this.tbpRun.UseVisualStyleBackColor = true;
            // 
            // nibRunMax
            // 
            this.nibRunMax.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRunMax.Caption = "上限";
            this.nibRunMax.CaptionWidth = 60;
            this.nibRunMax.InputValue = 0F;
            this.nibRunMax.Location = new System.Drawing.Point(162, 36);
            this.nibRunMax.Name = "nibRunMax";
            this.nibRunMax.Size = new System.Drawing.Size(150, 24);
            this.nibRunMax.TabIndex = 5;
            this.nibRunMax.Inputed += new System.EventHandler(this.nibRunMax_Inputed);
            // 
            // nibRunRate
            // 
            this.nibRunRate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRunRate.Caption = "速率";
            this.nibRunRate.CaptionWidth = 60;
            this.nibRunRate.InputValue = 0F;
            this.nibRunRate.Location = new System.Drawing.Point(6, 36);
            this.nibRunRate.Name = "nibRunRate";
            this.nibRunRate.Size = new System.Drawing.Size(150, 24);
            this.nibRunRate.TabIndex = 4;
            this.nibRunRate.Inputed += new System.EventHandler(this.nibRunRate_Inputed);
            // 
            // iibRunMode
            // 
            this.iibRunMode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibRunMode.Caption = "发射模式";
            this.iibRunMode.CaptionWidth = 100;
            this.iibRunMode.InputIndex = -1;
            this.iibRunMode.InputItems = new string[] {
        "重力",
        "半径"};
            this.iibRunMode.Location = new System.Drawing.Point(318, 36);
            this.iibRunMode.Name = "iibRunMode";
            this.iibRunMode.Size = new System.Drawing.Size(200, 24);
            this.iibRunMode.TabIndex = 3;
            this.iibRunMode.Inputed += new System.EventHandler(this.iibRunMode_Inputed);
            // 
            // iibRunPositionType
            // 
            this.iibRunPositionType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibRunPositionType.Caption = "位置类型";
            this.iibRunPositionType.CaptionWidth = 100;
            this.iibRunPositionType.InputIndex = -1;
            this.iibRunPositionType.InputItems = new string[] {
        "自由",
        "关联"};
            this.iibRunPositionType.Location = new System.Drawing.Point(318, 6);
            this.iibRunPositionType.Name = "iibRunPositionType";
            this.iibRunPositionType.Size = new System.Drawing.Size(200, 24);
            this.iibRunPositionType.TabIndex = 2;
            this.iibRunPositionType.Inputed += new System.EventHandler(this.iibRunPositionType_Inputed);
            // 
            // nibRunDuration
            // 
            this.nibRunDuration.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRunDuration.Caption = "持续";
            this.nibRunDuration.CaptionWidth = 60;
            this.nibRunDuration.InputValue = 0F;
            this.nibRunDuration.Location = new System.Drawing.Point(162, 6);
            this.nibRunDuration.Name = "nibRunDuration";
            this.nibRunDuration.Size = new System.Drawing.Size(150, 24);
            this.nibRunDuration.TabIndex = 1;
            this.nibRunDuration.Inputed += new System.EventHandler(this.nibRunDuration_Inputed);
            // 
            // nibRunDelay
            // 
            this.nibRunDelay.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRunDelay.Caption = "延时";
            this.nibRunDelay.CaptionWidth = 60;
            this.nibRunDelay.InputValue = 0F;
            this.nibRunDelay.Location = new System.Drawing.Point(6, 6);
            this.nibRunDelay.Name = "nibRunDelay";
            this.nibRunDelay.Size = new System.Drawing.Size(150, 24);
            this.nibRunDelay.TabIndex = 0;
            this.nibRunDelay.Inputed += new System.EventHandler(this.nibRunDelay_Inputed);
            // 
            // tbpColor
            // 
            this.tbpColor.Controls.Add(this.cibColorEndVar);
            this.tbpColor.Controls.Add(this.cibColorEnd);
            this.tbpColor.Controls.Add(this.cibColorStartVar);
            this.tbpColor.Controls.Add(this.cibColorStart);
            this.tbpColor.Location = new System.Drawing.Point(4, 22);
            this.tbpColor.Name = "tbpColor";
            this.tbpColor.Size = new System.Drawing.Size(974, 137);
            this.tbpColor.TabIndex = 3;
            this.tbpColor.Text = "颜色调节";
            this.tbpColor.UseVisualStyleBackColor = true;
            // 
            // cibColorEndVar
            // 
            this.cibColorEndVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibColorEndVar.Caption = "波动";
            this.cibColorEndVar.CaptionWidth = 60;
            this.cibColorEndVar.InputValue = System.Drawing.Color.White;
            this.cibColorEndVar.Location = new System.Drawing.Point(714, 6);
            this.cibColorEndVar.Name = "cibColorEndVar";
            this.cibColorEndVar.Size = new System.Drawing.Size(230, 125);
            this.cibColorEndVar.SlideAdjust = true;
            this.cibColorEndVar.TabIndex = 13;
            this.cibColorEndVar.Inputed += new System.EventHandler(this.cibColorEndVar_Inputed);
            // 
            // cibColorEnd
            // 
            this.cibColorEnd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibColorEnd.Caption = "结束";
            this.cibColorEnd.CaptionWidth = 60;
            this.cibColorEnd.InputValue = System.Drawing.Color.White;
            this.cibColorEnd.Location = new System.Drawing.Point(478, 6);
            this.cibColorEnd.Name = "cibColorEnd";
            this.cibColorEnd.Size = new System.Drawing.Size(230, 125);
            this.cibColorEnd.SlideAdjust = true;
            this.cibColorEnd.TabIndex = 12;
            this.cibColorEnd.Inputed += new System.EventHandler(this.cibColorEnd_Inputed);
            // 
            // cibColorStartVar
            // 
            this.cibColorStartVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibColorStartVar.Caption = "波动";
            this.cibColorStartVar.CaptionWidth = 60;
            this.cibColorStartVar.InputValue = System.Drawing.Color.White;
            this.cibColorStartVar.Location = new System.Drawing.Point(242, 6);
            this.cibColorStartVar.Name = "cibColorStartVar";
            this.cibColorStartVar.Size = new System.Drawing.Size(230, 125);
            this.cibColorStartVar.SlideAdjust = true;
            this.cibColorStartVar.TabIndex = 11;
            this.cibColorStartVar.Inputed += new System.EventHandler(this.cibColorStartVar_Inputed);
            // 
            // cibColorStart
            // 
            this.cibColorStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibColorStart.Caption = "起始";
            this.cibColorStart.CaptionWidth = 60;
            this.cibColorStart.InputValue = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cibColorStart.Location = new System.Drawing.Point(6, 6);
            this.cibColorStart.Name = "cibColorStart";
            this.cibColorStart.Size = new System.Drawing.Size(230, 125);
            this.cibColorStart.SlideAdjust = true;
            this.cibColorStart.TabIndex = 10;
            this.cibColorStart.Inputed += new System.EventHandler(this.cibColorStart_Inputed);
            // 
            // tbpSizeSpin
            // 
            this.tbpSizeSpin.Controls.Add(this.nibSpinEndVar);
            this.tbpSizeSpin.Controls.Add(this.nibSpinEnd);
            this.tbpSizeSpin.Controls.Add(this.nibSpinStartVar);
            this.tbpSizeSpin.Controls.Add(this.nibSpinStart);
            this.tbpSizeSpin.Controls.Add(this.nibSizeEndVar);
            this.tbpSizeSpin.Controls.Add(this.nibSizeEnd);
            this.tbpSizeSpin.Controls.Add(this.nibSizeStartVar);
            this.tbpSizeSpin.Controls.Add(this.nibSizeStart);
            this.tbpSizeSpin.Location = new System.Drawing.Point(4, 22);
            this.tbpSizeSpin.Name = "tbpSizeSpin";
            this.tbpSizeSpin.Size = new System.Drawing.Size(974, 137);
            this.tbpSizeSpin.TabIndex = 4;
            this.tbpSizeSpin.Text = "大小旋转";
            this.tbpSizeSpin.UseVisualStyleBackColor = true;
            // 
            // nibSpinEndVar
            // 
            this.nibSpinEndVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpinEndVar.Caption = "波动";
            this.nibSpinEndVar.CaptionWidth = 60;
            this.nibSpinEndVar.InputValue = 0F;
            this.nibSpinEndVar.Location = new System.Drawing.Point(554, 36);
            this.nibSpinEndVar.Name = "nibSpinEndVar";
            this.nibSpinEndVar.Size = new System.Drawing.Size(150, 24);
            this.nibSpinEndVar.TabIndex = 7;
            this.nibSpinEndVar.Inputed += new System.EventHandler(this.nibSpinEndVar_Inputed);
            // 
            // nibSpinEnd
            // 
            this.nibSpinEnd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpinEnd.Caption = "结束旋转";
            this.nibSpinEnd.CaptionWidth = 100;
            this.nibSpinEnd.InputValue = 0F;
            this.nibSpinEnd.Location = new System.Drawing.Point(358, 36);
            this.nibSpinEnd.Name = "nibSpinEnd";
            this.nibSpinEnd.Size = new System.Drawing.Size(190, 24);
            this.nibSpinEnd.TabIndex = 6;
            this.nibSpinEnd.Inputed += new System.EventHandler(this.nibSpinEnd_Inputed);
            // 
            // nibSpinStartVar
            // 
            this.nibSpinStartVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpinStartVar.Caption = "波动";
            this.nibSpinStartVar.CaptionWidth = 60;
            this.nibSpinStartVar.InputValue = 0F;
            this.nibSpinStartVar.Location = new System.Drawing.Point(202, 36);
            this.nibSpinStartVar.Name = "nibSpinStartVar";
            this.nibSpinStartVar.Size = new System.Drawing.Size(150, 24);
            this.nibSpinStartVar.TabIndex = 5;
            this.nibSpinStartVar.Inputed += new System.EventHandler(this.nibSpinStartVar_Inputed);
            // 
            // nibSpinStart
            // 
            this.nibSpinStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpinStart.Caption = "起始旋转";
            this.nibSpinStart.CaptionWidth = 100;
            this.nibSpinStart.InputValue = 0F;
            this.nibSpinStart.Location = new System.Drawing.Point(6, 36);
            this.nibSpinStart.Name = "nibSpinStart";
            this.nibSpinStart.Size = new System.Drawing.Size(190, 24);
            this.nibSpinStart.TabIndex = 4;
            this.nibSpinStart.Inputed += new System.EventHandler(this.nibSpinStart_Inputed);
            // 
            // nibSizeEndVar
            // 
            this.nibSizeEndVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSizeEndVar.Caption = "波动";
            this.nibSizeEndVar.CaptionWidth = 60;
            this.nibSizeEndVar.InputValue = 0F;
            this.nibSizeEndVar.Location = new System.Drawing.Point(554, 6);
            this.nibSizeEndVar.Name = "nibSizeEndVar";
            this.nibSizeEndVar.Size = new System.Drawing.Size(150, 24);
            this.nibSizeEndVar.TabIndex = 3;
            this.nibSizeEndVar.Inputed += new System.EventHandler(this.nibSizeEndVar_Inputed);
            // 
            // nibSizeEnd
            // 
            this.nibSizeEnd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSizeEnd.Caption = "结束大小";
            this.nibSizeEnd.CaptionWidth = 100;
            this.nibSizeEnd.InputValue = 0F;
            this.nibSizeEnd.Location = new System.Drawing.Point(358, 6);
            this.nibSizeEnd.Name = "nibSizeEnd";
            this.nibSizeEnd.Size = new System.Drawing.Size(190, 24);
            this.nibSizeEnd.TabIndex = 2;
            this.nibSizeEnd.Inputed += new System.EventHandler(this.nibSizeEnd_Inputed);
            // 
            // nibSizeStartVar
            // 
            this.nibSizeStartVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSizeStartVar.Caption = "波动";
            this.nibSizeStartVar.CaptionWidth = 60;
            this.nibSizeStartVar.InputValue = 0F;
            this.nibSizeStartVar.Location = new System.Drawing.Point(202, 6);
            this.nibSizeStartVar.Name = "nibSizeStartVar";
            this.nibSizeStartVar.Size = new System.Drawing.Size(150, 24);
            this.nibSizeStartVar.TabIndex = 1;
            this.nibSizeStartVar.Inputed += new System.EventHandler(this.nibSizeStartVar_Inputed);
            // 
            // nibSizeStart
            // 
            this.nibSizeStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSizeStart.Caption = "起始大小";
            this.nibSizeStart.CaptionWidth = 100;
            this.nibSizeStart.InputValue = 0F;
            this.nibSizeStart.Location = new System.Drawing.Point(6, 6);
            this.nibSizeStart.Name = "nibSizeStart";
            this.nibSizeStart.Size = new System.Drawing.Size(190, 24);
            this.nibSizeStart.TabIndex = 0;
            this.nibSizeStart.Inputed += new System.EventHandler(this.nibSizeStart_Inputed);
            // 
            // tbpGravity
            // 
            this.tbpGravity.Controls.Add(this.nibGravityTangentialAccelVar);
            this.tbpGravity.Controls.Add(this.nibGravityTangentialAccel);
            this.tbpGravity.Controls.Add(this.nibGravityRadialAccelVar);
            this.tbpGravity.Controls.Add(this.nibGravityRadialAccel);
            this.tbpGravity.Controls.Add(this.nibGravitySpeedVar);
            this.tbpGravity.Controls.Add(this.nibGravitySpeed);
            this.tbpGravity.Controls.Add(this.pibGravityAccel);
            this.tbpGravity.Location = new System.Drawing.Point(4, 22);
            this.tbpGravity.Name = "tbpGravity";
            this.tbpGravity.Size = new System.Drawing.Size(974, 137);
            this.tbpGravity.TabIndex = 5;
            this.tbpGravity.Text = "重力模式";
            this.tbpGravity.UseVisualStyleBackColor = true;
            // 
            // nibGravityTangentialAccelVar
            // 
            this.nibGravityTangentialAccelVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravityTangentialAccelVar.Caption = "波动";
            this.nibGravityTangentialAccelVar.CaptionWidth = 60;
            this.nibGravityTangentialAccelVar.InputValue = 0F;
            this.nibGravityTangentialAccelVar.Location = new System.Drawing.Point(574, 36);
            this.nibGravityTangentialAccelVar.Name = "nibGravityTangentialAccelVar";
            this.nibGravityTangentialAccelVar.Size = new System.Drawing.Size(150, 24);
            this.nibGravityTangentialAccelVar.TabIndex = 6;
            this.nibGravityTangentialAccelVar.Inputed += new System.EventHandler(this.nibGravityTangentialAccelVar_Inputed);
            // 
            // nibGravityTangentialAccel
            // 
            this.nibGravityTangentialAccel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravityTangentialAccel.Caption = "切向加速";
            this.nibGravityTangentialAccel.CaptionWidth = 100;
            this.nibGravityTangentialAccel.InputValue = 0F;
            this.nibGravityTangentialAccel.Location = new System.Drawing.Point(368, 36);
            this.nibGravityTangentialAccel.Name = "nibGravityTangentialAccel";
            this.nibGravityTangentialAccel.Size = new System.Drawing.Size(200, 24);
            this.nibGravityTangentialAccel.TabIndex = 5;
            this.nibGravityTangentialAccel.Inputed += new System.EventHandler(this.nibGravityTangentialAccel_Inputed);
            // 
            // nibGravityRadialAccelVar
            // 
            this.nibGravityRadialAccelVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravityRadialAccelVar.Caption = "波动";
            this.nibGravityRadialAccelVar.CaptionWidth = 60;
            this.nibGravityRadialAccelVar.InputValue = 0F;
            this.nibGravityRadialAccelVar.Location = new System.Drawing.Point(212, 36);
            this.nibGravityRadialAccelVar.Name = "nibGravityRadialAccelVar";
            this.nibGravityRadialAccelVar.Size = new System.Drawing.Size(150, 24);
            this.nibGravityRadialAccelVar.TabIndex = 4;
            this.nibGravityRadialAccelVar.Inputed += new System.EventHandler(this.nibGravityRadialAccelVar_Inputed);
            // 
            // nibGravityRadialAccel
            // 
            this.nibGravityRadialAccel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravityRadialAccel.Caption = "径向加速";
            this.nibGravityRadialAccel.CaptionWidth = 100;
            this.nibGravityRadialAccel.InputValue = 0F;
            this.nibGravityRadialAccel.Location = new System.Drawing.Point(6, 36);
            this.nibGravityRadialAccel.Name = "nibGravityRadialAccel";
            this.nibGravityRadialAccel.Size = new System.Drawing.Size(200, 24);
            this.nibGravityRadialAccel.TabIndex = 3;
            this.nibGravityRadialAccel.Inputed += new System.EventHandler(this.nibGravityRadialAccel_Inputed);
            // 
            // nibGravitySpeedVar
            // 
            this.nibGravitySpeedVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravitySpeedVar.Caption = "波动";
            this.nibGravitySpeedVar.CaptionWidth = 60;
            this.nibGravitySpeedVar.InputValue = 0F;
            this.nibGravitySpeedVar.Location = new System.Drawing.Point(368, 6);
            this.nibGravitySpeedVar.Name = "nibGravitySpeedVar";
            this.nibGravitySpeedVar.Size = new System.Drawing.Size(150, 24);
            this.nibGravitySpeedVar.TabIndex = 2;
            this.nibGravitySpeedVar.Inputed += new System.EventHandler(this.nibGravitySpeedVar_Inputed);
            // 
            // nibGravitySpeed
            // 
            this.nibGravitySpeed.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibGravitySpeed.Caption = "速度";
            this.nibGravitySpeed.CaptionWidth = 60;
            this.nibGravitySpeed.InputValue = 0F;
            this.nibGravitySpeed.Location = new System.Drawing.Point(212, 6);
            this.nibGravitySpeed.Name = "nibGravitySpeed";
            this.nibGravitySpeed.Size = new System.Drawing.Size(150, 24);
            this.nibGravitySpeed.TabIndex = 1;
            this.nibGravitySpeed.Inputed += new System.EventHandler(this.nibGravitySpeed_Inputed);
            // 
            // pibGravityAccel
            // 
            this.pibGravityAccel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibGravityAccel.Caption = "重力加速";
            this.pibGravityAccel.CaptionWidth = 100;
            this.pibGravityAccel.InputValue = new System.Drawing.Point(0, 0);
            this.pibGravityAccel.Location = new System.Drawing.Point(6, 6);
            this.pibGravityAccel.Name = "pibGravityAccel";
            this.pibGravityAccel.Size = new System.Drawing.Size(200, 24);
            this.pibGravityAccel.TabIndex = 0;
            this.pibGravityAccel.Inputed += new System.EventHandler(this.pibGravityAccel_Inputed);
            // 
            // tbpRadius
            // 
            this.tbpRadius.Controls.Add(this.nibRadiusRotateVar);
            this.tbpRadius.Controls.Add(this.nibRadiusRotate);
            this.tbpRadius.Controls.Add(this.nibRadiusEndVar);
            this.tbpRadius.Controls.Add(this.nibRadiusEnd);
            this.tbpRadius.Controls.Add(this.nibRadiusStartVar);
            this.tbpRadius.Controls.Add(this.nibRadiusStart);
            this.tbpRadius.Location = new System.Drawing.Point(4, 22);
            this.tbpRadius.Name = "tbpRadius";
            this.tbpRadius.Size = new System.Drawing.Size(974, 137);
            this.tbpRadius.TabIndex = 6;
            this.tbpRadius.Text = "半径模式";
            this.tbpRadius.UseVisualStyleBackColor = true;
            // 
            // nibRadiusRotateVar
            // 
            this.nibRadiusRotateVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusRotateVar.Caption = "波动";
            this.nibRadiusRotateVar.CaptionWidth = 60;
            this.nibRadiusRotateVar.InputValue = 0F;
            this.nibRadiusRotateVar.Location = new System.Drawing.Point(162, 36);
            this.nibRadiusRotateVar.Name = "nibRadiusRotateVar";
            this.nibRadiusRotateVar.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusRotateVar.TabIndex = 5;
            this.nibRadiusRotateVar.Inputed += new System.EventHandler(this.nibRadiusRotateVar_Inputed);
            // 
            // nibRadiusRotate
            // 
            this.nibRadiusRotate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusRotate.Caption = "转速";
            this.nibRadiusRotate.CaptionWidth = 60;
            this.nibRadiusRotate.InputValue = 0F;
            this.nibRadiusRotate.Location = new System.Drawing.Point(6, 36);
            this.nibRadiusRotate.Name = "nibRadiusRotate";
            this.nibRadiusRotate.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusRotate.TabIndex = 4;
            this.nibRadiusRotate.Inputed += new System.EventHandler(this.nibRadiusRotate_Inputed);
            // 
            // nibRadiusEndVar
            // 
            this.nibRadiusEndVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusEndVar.Caption = "波动";
            this.nibRadiusEndVar.CaptionWidth = 60;
            this.nibRadiusEndVar.InputValue = 0F;
            this.nibRadiusEndVar.Location = new System.Drawing.Point(474, 6);
            this.nibRadiusEndVar.Name = "nibRadiusEndVar";
            this.nibRadiusEndVar.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusEndVar.TabIndex = 3;
            this.nibRadiusEndVar.Inputed += new System.EventHandler(this.nibRadiusEndVar_Inputed);
            // 
            // nibRadiusEnd
            // 
            this.nibRadiusEnd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusEnd.Caption = "结束";
            this.nibRadiusEnd.CaptionWidth = 60;
            this.nibRadiusEnd.InputValue = 0F;
            this.nibRadiusEnd.Location = new System.Drawing.Point(318, 6);
            this.nibRadiusEnd.Name = "nibRadiusEnd";
            this.nibRadiusEnd.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusEnd.TabIndex = 2;
            this.nibRadiusEnd.Inputed += new System.EventHandler(this.nibRadiusEnd_Inputed);
            // 
            // nibRadiusStartVar
            // 
            this.nibRadiusStartVar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusStartVar.Caption = "波动";
            this.nibRadiusStartVar.CaptionWidth = 60;
            this.nibRadiusStartVar.InputValue = 0F;
            this.nibRadiusStartVar.Location = new System.Drawing.Point(162, 6);
            this.nibRadiusStartVar.Name = "nibRadiusStartVar";
            this.nibRadiusStartVar.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusStartVar.TabIndex = 1;
            this.nibRadiusStartVar.Inputed += new System.EventHandler(this.nibRadiusStartVar_Inputed);
            // 
            // nibRadiusStart
            // 
            this.nibRadiusStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadiusStart.Caption = "起始";
            this.nibRadiusStart.CaptionWidth = 60;
            this.nibRadiusStart.InputValue = 0F;
            this.nibRadiusStart.Location = new System.Drawing.Point(6, 6);
            this.nibRadiusStart.Name = "nibRadiusStart";
            this.nibRadiusStart.Size = new System.Drawing.Size(150, 24);
            this.nibRadiusStart.TabIndex = 0;
            this.nibRadiusStart.Inputed += new System.EventHandler(this.nibRadiusStart_Inputed);
            // 
            // EffectPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 163);
            this.Controls.Add(this.tbcProperty);
            this.Name = "EffectPropertyForm";
            this.Text = "属性";
            this.tbcProperty.ResumeLayout(false);
            this.tbpParticleSystem.ResumeLayout(false);
            this.tbpParticleSystem.PerformLayout();
            this.tbpBasic.ResumeLayout(false);
            this.tbpRun.ResumeLayout(false);
            this.tbpColor.ResumeLayout(false);
            this.tbpSizeSpin.ResumeLayout(false);
            this.tbpGravity.ResumeLayout(false);
            this.tbpRadius.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcProperty;
        private System.Windows.Forms.TabPage tbpBasic;
        private XuXiang.Tool.ControlLibrary.PointInputBox pibBasicPositionVar;
        private XuXiang.Tool.ControlLibrary.PointInputBox pibBasicPosition;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicYScale;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicXScale;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicAngleVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicAngle;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicLifeVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibBasicLife;
        private System.Windows.Forms.TabPage tbpParticleSystem;
        private System.Windows.Forms.Label lbBasicNote;
        private XuXiang.Tool.ControlLibrary.PointInputBox pibParticlePosition;
        private System.Windows.Forms.TabPage tbpRun;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRunDelay;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRunDuration;
        private XuXiang.Tool.ControlLibrary.ItemInputBox iibRunPositionType;
        private XuXiang.Tool.ControlLibrary.ItemInputBox iibRunMode;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRunMax;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRunRate;
        private System.Windows.Forms.TabPage tbpColor;
        private XuXiang.Tool.ControlLibrary.ColorInputBox cibColorEndVar;
        private XuXiang.Tool.ControlLibrary.ColorInputBox cibColorEnd;
        private XuXiang.Tool.ControlLibrary.ColorInputBox cibColorStartVar;
        private XuXiang.Tool.ControlLibrary.ColorInputBox cibColorStart;
        private System.Windows.Forms.TabPage tbpSizeSpin;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSizeEndVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSizeEnd;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSizeStartVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSizeStart;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSpinEndVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSpinEnd;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSpinStartVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibSpinStart;
        private System.Windows.Forms.TabPage tbpGravity;
        private XuXiang.Tool.ControlLibrary.PointInputBox pibGravityAccel;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravitySpeedVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravitySpeed;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravityTangentialAccelVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravityTangentialAccel;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravityRadialAccelVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibGravityRadialAccel;
        private System.Windows.Forms.TabPage tbpRadius;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusRotateVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusRotate;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusEndVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusEnd;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusStartVar;
        private XuXiang.Tool.ControlLibrary.NumberInputBox nibRadiusStart;
        private XuXiang.Tool.ControlLibrary.TextInputBox tibBasicName;
        private XuXiang.Tool.ControlLibrary.FileInputBox fibBasicImage;
    }
}