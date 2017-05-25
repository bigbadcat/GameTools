using System;
using XuXiang.Tool.ControlLibrary;
namespace T002.Forms
{
    partial class ControlPropertyForm
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
            this.tbpControl = new System.Windows.Forms.TabPage();
            this.bibControlVisible = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.bibControlEnable = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.cibControlBackColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.sibControlSize = new XuXiang.Tool.ControlLibrary.SizeInputBox();
            this.pibControlPosition = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.tbpPicture = new System.Windows.Forms.TabPage();
            this.nibPictureScaleY = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.bibPictureClearValue = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.cibPictureChannel = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.nibPictureScaleX = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibPictureTrans = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.iibPictureAlign = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.iibPictureMode = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibPictureImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpContainer = new System.Windows.Forms.TabPage();
            this.bibContainerClipping = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.cibContainerFramChannel = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.fibContainerFrame = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.cibContainerBackChannel = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.iibContainerBackTrans = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibContainerBack = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpTextControl = new System.Windows.Forms.TabPage();
            this.bibTextControlClearValue = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.cibTextControlTextColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.nibTextControlWordSize = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tibTextControlText = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tbpLabel = new System.Windows.Forms.TabPage();
            this.cibLabelStrokeColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.iibLabelType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.iibLabelAlign = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.tbpButton = new System.Windows.Forms.TabPage();
            this.fibButtonDownImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.fibButtonDisableImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.fibButtonNormalImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpSingleButton = new System.Windows.Forms.TabPage();
            this.cibSingleButtonTextDisableStrokeColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibSingleButtonTextDownStrokeColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibSingleButtonTextNormalStrokeColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibSingleButtonTextDisableColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibSingleButtonTextDownColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.cibSingleButtonTextNormalColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.pibSingleButtonTextDownOffset = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.pibSingleButtonTextOffset = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.nibSingleButtonWordSize = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tibSingleButtonText = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.tbpTable = new System.Windows.Forms.TabPage();
            this.fibTableScrollBack = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.fibTableScrollBar = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.nibTableScrollBarWidth = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibTableChildNumber = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpScrollTable = new System.Windows.Forms.TabPage();
            this.nibScrollTableBasicNumber = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibScrollTableDirection = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.tbpRadioButton = new System.Windows.Forms.TabPage();
            this.bibRadionButtonCheck = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.nibRadionButtonGroup = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpCheckButton = new System.Windows.Forms.TabPage();
            this.bibCheckButtonCheck = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.tbpPageTable = new System.Windows.Forms.TabPage();
            this.nibPageTableCol = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibPageTableRow = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpScrollPanel = new System.Windows.Forms.TabPage();
            this.pibScrollPanelMove = new XuXiang.Tool.ControlLibrary.PointInputBox();
            this.tbpNumberImage = new System.Windows.Forms.TabPage();
            this.nibNumberImageUpperLimitValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.fibNumberImageUpperLimitFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.nibNumberImageGap = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibNumberImageDirection = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.nibNumberImageValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibNumberImageZoom = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibNumberImageAlign = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibNumberImageFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpTextArea = new System.Windows.Forms.TabPage();
            this.fibTextAreaScrollBack = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.fibTextAreaScrollBar = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.nibTextAreaScrollBarWidth = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpImageNumber = new System.Windows.Forms.TabPage();
            this.nibImageNumberValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibImageNumberZoom = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.iibImageNumberAlign = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibImageNumberFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpTextBox = new System.Windows.Forms.TabPage();
            this.iibTextBoxInputType = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.iibTextBoxAlign = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.tbpProgressBar = new System.Windows.Forms.TabPage();
            this.nibProgressBarEndValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibProgressBarCurrentValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibProgressBarStartValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.fibProgressBarSlotImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.cibProgressBarFillColor = new XuXiang.Tool.ControlLibrary.ColorInputBox();
            this.iibProgressBarDirection = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibProgressBarFillImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpSliderBar = new System.Windows.Forms.TabPage();
            this.nibSliderBarZoom = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.fibSliderBarSlotImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.iibSliderBarDirection = new XuXiang.Tool.ControlLibrary.ItemInputBox();
            this.fibSliderBarImage = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.nibSliderBarEndValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSliderBarCurrentValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSliderBarStartValue = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbpParticleView = new System.Windows.Forms.TabPage();
            this.bibParticleViewParentClip = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.fibParticleViewFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.tbpSpineView = new System.Windows.Forms.TabPage();
            this.fibSpineViewAnimation = new XuXiang.Tool.ControlLibrary.TextInputBox();
            this.bibSpineViewLoop = new XuXiang.Tool.ControlLibrary.BooleanInputBox();
            this.fibSpineViewFile = new XuXiang.Tool.ControlLibrary.FileInputBox();
            this.nibParticleViewScaleY = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibParticleViewScaleX = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSpineViewScaleY = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.nibSpineViewScaleX = new XuXiang.Tool.ControlLibrary.NumberInputBox();
            this.tbcProperty.SuspendLayout();
            this.tbpControl.SuspendLayout();
            this.tbpPicture.SuspendLayout();
            this.tbpContainer.SuspendLayout();
            this.tbpTextControl.SuspendLayout();
            this.tbpLabel.SuspendLayout();
            this.tbpButton.SuspendLayout();
            this.tbpSingleButton.SuspendLayout();
            this.tbpTable.SuspendLayout();
            this.tbpScrollTable.SuspendLayout();
            this.tbpRadioButton.SuspendLayout();
            this.tbpCheckButton.SuspendLayout();
            this.tbpPageTable.SuspendLayout();
            this.tbpScrollPanel.SuspendLayout();
            this.tbpNumberImage.SuspendLayout();
            this.tbpTextArea.SuspendLayout();
            this.tbpImageNumber.SuspendLayout();
            this.tbpTextBox.SuspendLayout();
            this.tbpProgressBar.SuspendLayout();
            this.tbpSliderBar.SuspendLayout();
            this.tbpParticleView.SuspendLayout();
            this.tbpSpineView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcProperty
            // 
            this.tbcProperty.Controls.Add(this.tbpControl);
            this.tbcProperty.Controls.Add(this.tbpPicture);
            this.tbcProperty.Controls.Add(this.tbpContainer);
            this.tbcProperty.Controls.Add(this.tbpTextControl);
            this.tbcProperty.Controls.Add(this.tbpLabel);
            this.tbcProperty.Controls.Add(this.tbpButton);
            this.tbcProperty.Controls.Add(this.tbpSingleButton);
            this.tbcProperty.Controls.Add(this.tbpTable);
            this.tbcProperty.Controls.Add(this.tbpScrollTable);
            this.tbcProperty.Controls.Add(this.tbpRadioButton);
            this.tbcProperty.Controls.Add(this.tbpCheckButton);
            this.tbcProperty.Controls.Add(this.tbpPageTable);
            this.tbcProperty.Controls.Add(this.tbpScrollPanel);
            this.tbcProperty.Controls.Add(this.tbpNumberImage);
            this.tbcProperty.Controls.Add(this.tbpTextArea);
            this.tbcProperty.Controls.Add(this.tbpImageNumber);
            this.tbcProperty.Controls.Add(this.tbpTextBox);
            this.tbcProperty.Controls.Add(this.tbpProgressBar);
            this.tbcProperty.Controls.Add(this.tbpSliderBar);
            this.tbcProperty.Controls.Add(this.tbpParticleView);
            this.tbcProperty.Controls.Add(this.tbpSpineView);
            this.tbcProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcProperty.Location = new System.Drawing.Point(0, 0);
            this.tbcProperty.Name = "tbcProperty";
            this.tbcProperty.SelectedIndex = 0;
            this.tbcProperty.Size = new System.Drawing.Size(924, 133);
            this.tbcProperty.TabIndex = 0;
            // 
            // tbpControl
            // 
            this.tbpControl.AutoScroll = true;
            this.tbpControl.BackColor = System.Drawing.Color.White;
            this.tbpControl.Controls.Add(this.bibControlVisible);
            this.tbpControl.Controls.Add(this.bibControlEnable);
            this.tbpControl.Controls.Add(this.cibControlBackColor);
            this.tbpControl.Controls.Add(this.sibControlSize);
            this.tbpControl.Controls.Add(this.pibControlPosition);
            this.tbpControl.Location = new System.Drawing.Point(4, 22);
            this.tbpControl.Name = "tbpControl";
            this.tbpControl.Padding = new System.Windows.Forms.Padding(3);
            this.tbpControl.Size = new System.Drawing.Size(916, 107);
            this.tbpControl.TabIndex = 0;
            this.tbpControl.Text = "控件";
            // 
            // bibControlVisible
            // 
            this.bibControlVisible.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibControlVisible.Caption = "可见";
            this.bibControlVisible.CaptionWidth = 60;
            this.bibControlVisible.InputValue = false;
            this.bibControlVisible.Location = new System.Drawing.Point(162, 36);
            this.bibControlVisible.Name = "bibControlVisible";
            this.bibControlVisible.Size = new System.Drawing.Size(84, 24);
            this.bibControlVisible.TabIndex = 4;
            this.bibControlVisible.Inputed += new System.EventHandler(this.bibControlVisible_Inputed);
            // 
            // bibControlEnable
            // 
            this.bibControlEnable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibControlEnable.Caption = "可用";
            this.bibControlEnable.CaptionWidth = 60;
            this.bibControlEnable.InputValue = false;
            this.bibControlEnable.Location = new System.Drawing.Point(162, 6);
            this.bibControlEnable.Name = "bibControlEnable";
            this.bibControlEnable.Size = new System.Drawing.Size(84, 24);
            this.bibControlEnable.TabIndex = 3;
            this.bibControlEnable.Inputed += new System.EventHandler(this.bibControlEnable_Inputed);
            // 
            // cibControlBackColor
            // 
            this.cibControlBackColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibControlBackColor.Caption = "背景色";
            this.cibControlBackColor.CaptionWidth = 80;
            this.cibControlBackColor.InputValue = System.Drawing.Color.White;
            this.cibControlBackColor.Location = new System.Drawing.Point(252, 6);
            this.cibControlBackColor.Name = "cibControlBackColor";
            this.cibControlBackColor.Size = new System.Drawing.Size(270, 24);
            this.cibControlBackColor.SlideAdjust = false;
            this.cibControlBackColor.TabIndex = 2;
            this.cibControlBackColor.Inputed += new System.EventHandler(this.cibControlBackColor_Inputed);
            // 
            // sibControlSize
            // 
            this.sibControlSize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sibControlSize.Caption = "尺寸";
            this.sibControlSize.CaptionWidth = 60;
            this.sibControlSize.InputValue = new System.Drawing.Size(0, 0);
            this.sibControlSize.Location = new System.Drawing.Point(6, 36);
            this.sibControlSize.Name = "sibControlSize";
            this.sibControlSize.Size = new System.Drawing.Size(150, 24);
            this.sibControlSize.TabIndex = 1;
            this.sibControlSize.Inputed += new System.EventHandler(this.sibControlSize_Inputed);
            // 
            // pibControlPosition
            // 
            this.pibControlPosition.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibControlPosition.Caption = "位置";
            this.pibControlPosition.CaptionWidth = 60;
            this.pibControlPosition.InputValue = new System.Drawing.Point(0, 0);
            this.pibControlPosition.Location = new System.Drawing.Point(6, 6);
            this.pibControlPosition.Name = "pibControlPosition";
            this.pibControlPosition.Size = new System.Drawing.Size(150, 24);
            this.pibControlPosition.TabIndex = 0;
            this.pibControlPosition.Inputed += new System.EventHandler(this.pibControlPosition_Inputed);
            // 
            // tbpPicture
            // 
            this.tbpPicture.AutoScroll = true;
            this.tbpPicture.Controls.Add(this.nibPictureScaleY);
            this.tbpPicture.Controls.Add(this.bibPictureClearValue);
            this.tbpPicture.Controls.Add(this.cibPictureChannel);
            this.tbpPicture.Controls.Add(this.nibPictureScaleX);
            this.tbpPicture.Controls.Add(this.iibPictureTrans);
            this.tbpPicture.Controls.Add(this.iibPictureAlign);
            this.tbpPicture.Controls.Add(this.iibPictureMode);
            this.tbpPicture.Controls.Add(this.fibPictureImage);
            this.tbpPicture.Location = new System.Drawing.Point(4, 22);
            this.tbpPicture.Name = "tbpPicture";
            this.tbpPicture.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPicture.Size = new System.Drawing.Size(916, 107);
            this.tbpPicture.TabIndex = 1;
            this.tbpPicture.Text = "图像";
            this.tbpPicture.UseVisualStyleBackColor = true;
            // 
            // nibPictureScaleY
            // 
            this.nibPictureScaleY.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibPictureScaleY.Caption = "Y缩放";
            this.nibPictureScaleY.CaptionWidth = 70;
            this.nibPictureScaleY.InputValue = 0F;
            this.nibPictureScaleY.Location = new System.Drawing.Point(308, 36);
            this.nibPictureScaleY.Name = "nibPictureScaleY";
            this.nibPictureScaleY.Size = new System.Drawing.Size(140, 24);
            this.nibPictureScaleY.TabIndex = 7;
            this.nibPictureScaleY.Inputed += new System.EventHandler(this.nibPictureScaleY_Inputed);
            // 
            // bibPictureClearValue
            // 
            this.bibPictureClearValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibPictureClearValue.Caption = "清除";
            this.bibPictureClearValue.CaptionWidth = 60;
            this.bibPictureClearValue.InputValue = false;
            this.bibPictureClearValue.Location = new System.Drawing.Point(710, 6);
            this.bibPictureClearValue.Name = "bibPictureClearValue";
            this.bibPictureClearValue.Size = new System.Drawing.Size(84, 24);
            this.bibPictureClearValue.TabIndex = 6;
            this.bibPictureClearValue.Inputed += new System.EventHandler(this.nibPictureClearValue_Inputed);
            // 
            // cibPictureChannel
            // 
            this.cibPictureChannel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibPictureChannel.Caption = "通道";
            this.cibPictureChannel.CaptionWidth = 60;
            this.cibPictureChannel.InputValue = System.Drawing.Color.White;
            this.cibPictureChannel.Location = new System.Drawing.Point(454, 6);
            this.cibPictureChannel.Name = "cibPictureChannel";
            this.cibPictureChannel.Size = new System.Drawing.Size(250, 24);
            this.cibPictureChannel.SlideAdjust = false;
            this.cibPictureChannel.TabIndex = 5;
            this.cibPictureChannel.Inputed += new System.EventHandler(this.cibPictureChannel_Inputed);
            // 
            // nibPictureScaleX
            // 
            this.nibPictureScaleX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibPictureScaleX.Caption = "X缩放";
            this.nibPictureScaleX.CaptionWidth = 70;
            this.nibPictureScaleX.InputValue = 0F;
            this.nibPictureScaleX.Location = new System.Drawing.Point(162, 36);
            this.nibPictureScaleX.Name = "nibPictureScaleX";
            this.nibPictureScaleX.Size = new System.Drawing.Size(140, 24);
            this.nibPictureScaleX.TabIndex = 4;
            this.nibPictureScaleX.Inputed += new System.EventHandler(this.nibPictureScaleX_Inputed);
            // 
            // iibPictureTrans
            // 
            this.iibPictureTrans.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibPictureTrans.Caption = "变换";
            this.iibPictureTrans.CaptionWidth = 60;
            this.iibPictureTrans.InputIndex = -1;
            this.iibPictureTrans.InputItems = new string[] {
        "None",
        "MirrorRot180",
        "Mirror",
        "Rot180",
        "Rot90",
        "MirrorRot90",
        "MirrorRot270",
        "Rot270"};
            this.iibPictureTrans.Location = new System.Drawing.Point(630, 36);
            this.iibPictureTrans.Name = "iibPictureTrans";
            this.iibPictureTrans.Size = new System.Drawing.Size(164, 24);
            this.iibPictureTrans.TabIndex = 3;
            this.iibPictureTrans.Inputed += new System.EventHandler(this.iibPictureTrans_Inputed);
            // 
            // iibPictureAlign
            // 
            this.iibPictureAlign.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibPictureAlign.Caption = "对齐";
            this.iibPictureAlign.CaptionWidth = 60;
            this.iibPictureAlign.InputIndex = -1;
            this.iibPictureAlign.InputItems = new string[] {
        "TopLeft",
        "Top",
        "TopRight",
        "Left",
        "Center",
        "Right",
        "BottomLeft",
        "Bottom",
        "BottomRight"};
            this.iibPictureAlign.Location = new System.Drawing.Point(454, 36);
            this.iibPictureAlign.Name = "iibPictureAlign";
            this.iibPictureAlign.Size = new System.Drawing.Size(170, 24);
            this.iibPictureAlign.TabIndex = 2;
            this.iibPictureAlign.Inputed += new System.EventHandler(this.iibPictureAlign_Inputed);
            // 
            // iibPictureMode
            // 
            this.iibPictureMode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibPictureMode.Caption = "模式";
            this.iibPictureMode.CaptionWidth = 60;
            this.iibPictureMode.InputIndex = -1;
            this.iibPictureMode.InputItems = new string[] {
        "Normal",
        "Stretch",
        "NinePatch"};
            this.iibPictureMode.Location = new System.Drawing.Point(6, 36);
            this.iibPictureMode.Name = "iibPictureMode";
            this.iibPictureMode.Size = new System.Drawing.Size(150, 24);
            this.iibPictureMode.TabIndex = 1;
            this.iibPictureMode.Inputed += new System.EventHandler(this.iibPictureMode_Inputed);
            // 
            // fibPictureImage
            // 
            this.fibPictureImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibPictureImage.Caption = "图像";
            this.fibPictureImage.CaptionWidth = 60;
            this.fibPictureImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibPictureImage.FolderLimit = "";
            this.fibPictureImage.InputValue = "";
            this.fibPictureImage.Location = new System.Drawing.Point(6, 6);
            this.fibPictureImage.Name = "fibPictureImage";
            this.fibPictureImage.Size = new System.Drawing.Size(442, 24);
            this.fibPictureImage.TabIndex = 0;
            this.fibPictureImage.Inputed += new System.EventHandler(this.fibPictureImage_Inputed);
            // 
            // tbpContainer
            // 
            this.tbpContainer.AutoScroll = true;
            this.tbpContainer.Controls.Add(this.bibContainerClipping);
            this.tbpContainer.Controls.Add(this.cibContainerFramChannel);
            this.tbpContainer.Controls.Add(this.fibContainerFrame);
            this.tbpContainer.Controls.Add(this.cibContainerBackChannel);
            this.tbpContainer.Controls.Add(this.iibContainerBackTrans);
            this.tbpContainer.Controls.Add(this.fibContainerBack);
            this.tbpContainer.Location = new System.Drawing.Point(4, 22);
            this.tbpContainer.Name = "tbpContainer";
            this.tbpContainer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpContainer.Size = new System.Drawing.Size(916, 107);
            this.tbpContainer.TabIndex = 2;
            this.tbpContainer.Text = "容器";
            this.tbpContainer.UseVisualStyleBackColor = true;
            // 
            // bibContainerClipping
            // 
            this.bibContainerClipping.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibContainerClipping.Caption = "裁剪";
            this.bibContainerClipping.CaptionWidth = 60;
            this.bibContainerClipping.InputValue = false;
            this.bibContainerClipping.Location = new System.Drawing.Point(670, 36);
            this.bibContainerClipping.Name = "bibContainerClipping";
            this.bibContainerClipping.Size = new System.Drawing.Size(84, 24);
            this.bibContainerClipping.TabIndex = 11;
            this.bibContainerClipping.Inputed += new System.EventHandler(this.bibContainerClipping_Inputed);
            // 
            // cibContainerFramChannel
            // 
            this.cibContainerFramChannel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibContainerFramChannel.Caption = "通道";
            this.cibContainerFramChannel.CaptionWidth = 60;
            this.cibContainerFramChannel.InputValue = System.Drawing.Color.White;
            this.cibContainerFramChannel.Location = new System.Drawing.Point(414, 36);
            this.cibContainerFramChannel.Name = "cibContainerFramChannel";
            this.cibContainerFramChannel.Size = new System.Drawing.Size(250, 24);
            this.cibContainerFramChannel.SlideAdjust = false;
            this.cibContainerFramChannel.TabIndex = 10;
            this.cibContainerFramChannel.Inputed += new System.EventHandler(this.cibContainerFramChannel_Inputed);
            // 
            // fibContainerFrame
            // 
            this.fibContainerFrame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibContainerFrame.Caption = "边框";
            this.fibContainerFrame.CaptionWidth = 60;
            this.fibContainerFrame.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibContainerFrame.FolderLimit = "";
            this.fibContainerFrame.InputValue = "";
            this.fibContainerFrame.Location = new System.Drawing.Point(6, 36);
            this.fibContainerFrame.Name = "fibContainerFrame";
            this.fibContainerFrame.Size = new System.Drawing.Size(402, 24);
            this.fibContainerFrame.TabIndex = 9;
            this.fibContainerFrame.Inputed += new System.EventHandler(this.fibContainerFrame_Inputed);
            // 
            // cibContainerBackChannel
            // 
            this.cibContainerBackChannel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibContainerBackChannel.Caption = "通道";
            this.cibContainerBackChannel.CaptionWidth = 60;
            this.cibContainerBackChannel.InputValue = System.Drawing.Color.White;
            this.cibContainerBackChannel.Location = new System.Drawing.Point(414, 6);
            this.cibContainerBackChannel.Name = "cibContainerBackChannel";
            this.cibContainerBackChannel.Size = new System.Drawing.Size(250, 24);
            this.cibContainerBackChannel.SlideAdjust = false;
            this.cibContainerBackChannel.TabIndex = 8;
            this.cibContainerBackChannel.Inputed += new System.EventHandler(this.cibContainerBackChannel_Inputed);
            // 
            // iibContainerBackTrans
            // 
            this.iibContainerBackTrans.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibContainerBackTrans.Caption = "变换";
            this.iibContainerBackTrans.CaptionWidth = 60;
            this.iibContainerBackTrans.InputIndex = -1;
            this.iibContainerBackTrans.InputItems = new string[] {
        "None",
        "MirrorRot180",
        "Mirror",
        "Rot180",
        "Rot90",
        "MirrorRot90",
        "MirrorRot270",
        "Rot270"};
            this.iibContainerBackTrans.Location = new System.Drawing.Point(670, 6);
            this.iibContainerBackTrans.Name = "iibContainerBackTrans";
            this.iibContainerBackTrans.Size = new System.Drawing.Size(170, 24);
            this.iibContainerBackTrans.TabIndex = 7;
            this.iibContainerBackTrans.Inputed += new System.EventHandler(this.iibContainerBackTrans_Inputed);
            // 
            // fibContainerBack
            // 
            this.fibContainerBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibContainerBack.Caption = "背景";
            this.fibContainerBack.CaptionWidth = 60;
            this.fibContainerBack.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibContainerBack.FolderLimit = "";
            this.fibContainerBack.InputValue = "";
            this.fibContainerBack.Location = new System.Drawing.Point(6, 6);
            this.fibContainerBack.Name = "fibContainerBack";
            this.fibContainerBack.Size = new System.Drawing.Size(402, 24);
            this.fibContainerBack.TabIndex = 6;
            this.fibContainerBack.Inputed += new System.EventHandler(this.fibContainerBack_Inputed);
            // 
            // tbpTextControl
            // 
            this.tbpTextControl.AutoScroll = true;
            this.tbpTextControl.Controls.Add(this.bibTextControlClearValue);
            this.tbpTextControl.Controls.Add(this.cibTextControlTextColor);
            this.tbpTextControl.Controls.Add(this.nibTextControlWordSize);
            this.tbpTextControl.Controls.Add(this.tibTextControlText);
            this.tbpTextControl.Location = new System.Drawing.Point(4, 22);
            this.tbpTextControl.Name = "tbpTextControl";
            this.tbpTextControl.Size = new System.Drawing.Size(916, 107);
            this.tbpTextControl.TabIndex = 3;
            this.tbpTextControl.Text = "文本控件";
            this.tbpTextControl.UseVisualStyleBackColor = true;
            // 
            // bibTextControlClearValue
            // 
            this.bibTextControlClearValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibTextControlClearValue.Caption = "清除";
            this.bibTextControlClearValue.CaptionWidth = 60;
            this.bibTextControlClearValue.InputValue = false;
            this.bibTextControlClearValue.Location = new System.Drawing.Point(418, 36);
            this.bibTextControlClearValue.Name = "bibTextControlClearValue";
            this.bibTextControlClearValue.Size = new System.Drawing.Size(84, 24);
            this.bibTextControlClearValue.TabIndex = 5;
            this.bibTextControlClearValue.Inputed += new System.EventHandler(this.bibTextControlClearValue_Inputed);
            // 
            // cibTextControlTextColor
            // 
            this.cibTextControlTextColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibTextControlTextColor.Caption = "文本色";
            this.cibTextControlTextColor.CaptionWidth = 80;
            this.cibTextControlTextColor.InputValue = System.Drawing.Color.White;
            this.cibTextControlTextColor.Location = new System.Drawing.Point(162, 36);
            this.cibTextControlTextColor.Name = "cibTextControlTextColor";
            this.cibTextControlTextColor.Size = new System.Drawing.Size(250, 24);
            this.cibTextControlTextColor.SlideAdjust = false;
            this.cibTextControlTextColor.TabIndex = 2;
            this.cibTextControlTextColor.Inputed += new System.EventHandler(this.cibTextControlTextColor_Inputed);
            // 
            // nibTextControlWordSize
            // 
            this.nibTextControlWordSize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibTextControlWordSize.Caption = "字号";
            this.nibTextControlWordSize.CaptionWidth = 60;
            this.nibTextControlWordSize.InputValue = 0F;
            this.nibTextControlWordSize.Location = new System.Drawing.Point(6, 36);
            this.nibTextControlWordSize.Name = "nibTextControlWordSize";
            this.nibTextControlWordSize.Size = new System.Drawing.Size(150, 24);
            this.nibTextControlWordSize.TabIndex = 1;
            this.nibTextControlWordSize.Inputed += new System.EventHandler(this.nibTextControlWordSize_Inputed);
            // 
            // tibTextControlText
            // 
            this.tibTextControlText.AutoScroll = true;
            this.tibTextControlText.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibTextControlText.Caption = "文本";
            this.tibTextControlText.CaptionWidth = 60;
            this.tibTextControlText.InputValue = "";
            this.tibTextControlText.Location = new System.Drawing.Point(6, 6);
            this.tibTextControlText.Name = "tibTextControlText";
            this.tibTextControlText.Size = new System.Drawing.Size(600, 24);
            this.tibTextControlText.TabIndex = 0;
            this.tibTextControlText.Inputed += new System.EventHandler(this.tibTextControlText_Inputed);
            // 
            // tbpLabel
            // 
            this.tbpLabel.AutoScroll = true;
            this.tbpLabel.Controls.Add(this.cibLabelStrokeColor);
            this.tbpLabel.Controls.Add(this.iibLabelType);
            this.tbpLabel.Controls.Add(this.iibLabelAlign);
            this.tbpLabel.Location = new System.Drawing.Point(4, 22);
            this.tbpLabel.Name = "tbpLabel";
            this.tbpLabel.Size = new System.Drawing.Size(916, 107);
            this.tbpLabel.TabIndex = 4;
            this.tbpLabel.Text = "标签";
            this.tbpLabel.UseVisualStyleBackColor = true;
            // 
            // cibLabelStrokeColor
            // 
            this.cibLabelStrokeColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibLabelStrokeColor.Caption = "描边色";
            this.cibLabelStrokeColor.CaptionWidth = 80;
            this.cibLabelStrokeColor.InputValue = System.Drawing.Color.White;
            this.cibLabelStrokeColor.Location = new System.Drawing.Point(358, 6);
            this.cibLabelStrokeColor.Name = "cibLabelStrokeColor";
            this.cibLabelStrokeColor.Size = new System.Drawing.Size(250, 24);
            this.cibLabelStrokeColor.SlideAdjust = false;
            this.cibLabelStrokeColor.TabIndex = 2;
            this.cibLabelStrokeColor.Inputed += new System.EventHandler(this.cibLabelStrokeColor_Inputed);
            // 
            // iibLabelType
            // 
            this.iibLabelType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibLabelType.Caption = "类型";
            this.iibLabelType.CaptionWidth = 60;
            this.iibLabelType.InputIndex = -1;
            this.iibLabelType.InputItems = new string[] {
        "Normal",
        "Stroke",
        "MultiColor"};
            this.iibLabelType.Location = new System.Drawing.Point(182, 6);
            this.iibLabelType.Name = "iibLabelType";
            this.iibLabelType.Size = new System.Drawing.Size(170, 24);
            this.iibLabelType.TabIndex = 1;
            this.iibLabelType.Inputed += new System.EventHandler(this.iibLabelType_Inputed);
            // 
            // iibLabelAlign
            // 
            this.iibLabelAlign.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibLabelAlign.Caption = "对齐";
            this.iibLabelAlign.CaptionWidth = 60;
            this.iibLabelAlign.InputIndex = -1;
            this.iibLabelAlign.InputItems = new string[] {
        "TopLeft",
        "Top",
        "TopRight",
        "Left",
        "Center",
        "Right",
        "BottomLeft",
        "Bottom",
        "BottomRight"};
            this.iibLabelAlign.Location = new System.Drawing.Point(6, 6);
            this.iibLabelAlign.Name = "iibLabelAlign";
            this.iibLabelAlign.Size = new System.Drawing.Size(170, 24);
            this.iibLabelAlign.TabIndex = 0;
            this.iibLabelAlign.Inputed += new System.EventHandler(this.iibLabelAlign_Inputed);
            // 
            // tbpButton
            // 
            this.tbpButton.AutoScroll = true;
            this.tbpButton.Controls.Add(this.fibButtonDownImage);
            this.tbpButton.Controls.Add(this.fibButtonDisableImage);
            this.tbpButton.Controls.Add(this.fibButtonNormalImage);
            this.tbpButton.Location = new System.Drawing.Point(4, 22);
            this.tbpButton.Name = "tbpButton";
            this.tbpButton.Size = new System.Drawing.Size(916, 107);
            this.tbpButton.TabIndex = 5;
            this.tbpButton.Text = "按钮";
            this.tbpButton.UseVisualStyleBackColor = true;
            // 
            // fibButtonDownImage
            // 
            this.fibButtonDownImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibButtonDownImage.Caption = "按下图像";
            this.fibButtonDownImage.CaptionWidth = 100;
            this.fibButtonDownImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibButtonDownImage.FolderLimit = "";
            this.fibButtonDownImage.InputValue = "";
            this.fibButtonDownImage.Location = new System.Drawing.Point(8, 36);
            this.fibButtonDownImage.Name = "fibButtonDownImage";
            this.fibButtonDownImage.Size = new System.Drawing.Size(400, 24);
            this.fibButtonDownImage.TabIndex = 8;
            this.fibButtonDownImage.Inputed += new System.EventHandler(this.fibButtonDownImage_Inputed);
            // 
            // fibButtonDisableImage
            // 
            this.fibButtonDisableImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibButtonDisableImage.Caption = "禁用图像";
            this.fibButtonDisableImage.CaptionWidth = 100;
            this.fibButtonDisableImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibButtonDisableImage.FolderLimit = "";
            this.fibButtonDisableImage.InputValue = "";
            this.fibButtonDisableImage.Location = new System.Drawing.Point(8, 66);
            this.fibButtonDisableImage.Name = "fibButtonDisableImage";
            this.fibButtonDisableImage.Size = new System.Drawing.Size(400, 24);
            this.fibButtonDisableImage.TabIndex = 7;
            this.fibButtonDisableImage.Inputed += new System.EventHandler(this.fibButtonDisableImage_Inputed);
            // 
            // fibButtonNormalImage
            // 
            this.fibButtonNormalImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibButtonNormalImage.Caption = "普通图像";
            this.fibButtonNormalImage.CaptionWidth = 100;
            this.fibButtonNormalImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibButtonNormalImage.FolderLimit = "";
            this.fibButtonNormalImage.InputValue = "";
            this.fibButtonNormalImage.Location = new System.Drawing.Point(8, 6);
            this.fibButtonNormalImage.Name = "fibButtonNormalImage";
            this.fibButtonNormalImage.Size = new System.Drawing.Size(400, 24);
            this.fibButtonNormalImage.TabIndex = 4;
            this.fibButtonNormalImage.Inputed += new System.EventHandler(this.fibButtonNormalImage_Inputed);
            // 
            // tbpSingleButton
            // 
            this.tbpSingleButton.AutoScroll = true;
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextDisableStrokeColor);
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextDownStrokeColor);
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextNormalStrokeColor);
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextDisableColor);
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextDownColor);
            this.tbpSingleButton.Controls.Add(this.cibSingleButtonTextNormalColor);
            this.tbpSingleButton.Controls.Add(this.pibSingleButtonTextDownOffset);
            this.tbpSingleButton.Controls.Add(this.pibSingleButtonTextOffset);
            this.tbpSingleButton.Controls.Add(this.nibSingleButtonWordSize);
            this.tbpSingleButton.Controls.Add(this.tibSingleButtonText);
            this.tbpSingleButton.Location = new System.Drawing.Point(4, 22);
            this.tbpSingleButton.Name = "tbpSingleButton";
            this.tbpSingleButton.Size = new System.Drawing.Size(916, 107);
            this.tbpSingleButton.TabIndex = 6;
            this.tbpSingleButton.Text = "普通按钮";
            this.tbpSingleButton.UseVisualStyleBackColor = true;
            // 
            // cibSingleButtonTextDisableStrokeColor
            // 
            this.cibSingleButtonTextDisableStrokeColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextDisableStrokeColor.Caption = "按下描边";
            this.cibSingleButtonTextDisableStrokeColor.CaptionWidth = 120;
            this.cibSingleButtonTextDisableStrokeColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextDisableStrokeColor.Location = new System.Drawing.Point(598, 66);
            this.cibSingleButtonTextDisableStrokeColor.Name = "cibSingleButtonTextDisableStrokeColor";
            this.cibSingleButtonTextDisableStrokeColor.Size = new System.Drawing.Size(310, 24);
            this.cibSingleButtonTextDisableStrokeColor.SlideAdjust = false;
            this.cibSingleButtonTextDisableStrokeColor.TabIndex = 11;
            this.cibSingleButtonTextDisableStrokeColor.Inputed += new System.EventHandler(this.cibSingleButtonTextDisableStrokeColor_Inputed);
            // 
            // cibSingleButtonTextDownStrokeColor
            // 
            this.cibSingleButtonTextDownStrokeColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextDownStrokeColor.Caption = "按下描边";
            this.cibSingleButtonTextDownStrokeColor.CaptionWidth = 100;
            this.cibSingleButtonTextDownStrokeColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextDownStrokeColor.Location = new System.Drawing.Point(302, 66);
            this.cibSingleButtonTextDownStrokeColor.Name = "cibSingleButtonTextDownStrokeColor";
            this.cibSingleButtonTextDownStrokeColor.Size = new System.Drawing.Size(290, 24);
            this.cibSingleButtonTextDownStrokeColor.SlideAdjust = false;
            this.cibSingleButtonTextDownStrokeColor.TabIndex = 10;
            this.cibSingleButtonTextDownStrokeColor.Inputed += new System.EventHandler(this.cibSingleButtonTextDownStrokeColor_Inputed);
            // 
            // cibSingleButtonTextNormalStrokeColor
            // 
            this.cibSingleButtonTextNormalStrokeColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextNormalStrokeColor.Caption = "普通描边";
            this.cibSingleButtonTextNormalStrokeColor.CaptionWidth = 100;
            this.cibSingleButtonTextNormalStrokeColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextNormalStrokeColor.Location = new System.Drawing.Point(6, 66);
            this.cibSingleButtonTextNormalStrokeColor.Name = "cibSingleButtonTextNormalStrokeColor";
            this.cibSingleButtonTextNormalStrokeColor.Size = new System.Drawing.Size(290, 24);
            this.cibSingleButtonTextNormalStrokeColor.SlideAdjust = false;
            this.cibSingleButtonTextNormalStrokeColor.TabIndex = 9;
            this.cibSingleButtonTextNormalStrokeColor.Inputed += new System.EventHandler(this.cibSingleButtonTextNormalStrokeColor_Inputed);
            // 
            // cibSingleButtonTextDisableColor
            // 
            this.cibSingleButtonTextDisableColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextDisableColor.Caption = "不可用";
            this.cibSingleButtonTextDisableColor.CaptionWidth = 80;
            this.cibSingleButtonTextDisableColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextDisableColor.Location = new System.Drawing.Point(518, 36);
            this.cibSingleButtonTextDisableColor.Name = "cibSingleButtonTextDisableColor";
            this.cibSingleButtonTextDisableColor.Size = new System.Drawing.Size(250, 24);
            this.cibSingleButtonTextDisableColor.SlideAdjust = false;
            this.cibSingleButtonTextDisableColor.TabIndex = 8;
            this.cibSingleButtonTextDisableColor.Inputed += new System.EventHandler(this.cibSingleButtonTextDisableColor_Inputed);
            // 
            // cibSingleButtonTextDownColor
            // 
            this.cibSingleButtonTextDownColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextDownColor.Caption = "按下";
            this.cibSingleButtonTextDownColor.CaptionWidth = 60;
            this.cibSingleButtonTextDownColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextDownColor.Location = new System.Drawing.Point(262, 36);
            this.cibSingleButtonTextDownColor.Name = "cibSingleButtonTextDownColor";
            this.cibSingleButtonTextDownColor.Size = new System.Drawing.Size(250, 24);
            this.cibSingleButtonTextDownColor.SlideAdjust = false;
            this.cibSingleButtonTextDownColor.TabIndex = 7;
            this.cibSingleButtonTextDownColor.Inputed += new System.EventHandler(this.cibSingleButtonTextDownColor_Inputed);
            // 
            // cibSingleButtonTextNormalColor
            // 
            this.cibSingleButtonTextNormalColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibSingleButtonTextNormalColor.Caption = "普通";
            this.cibSingleButtonTextNormalColor.CaptionWidth = 60;
            this.cibSingleButtonTextNormalColor.InputValue = System.Drawing.Color.White;
            this.cibSingleButtonTextNormalColor.Location = new System.Drawing.Point(6, 36);
            this.cibSingleButtonTextNormalColor.Name = "cibSingleButtonTextNormalColor";
            this.cibSingleButtonTextNormalColor.Size = new System.Drawing.Size(250, 24);
            this.cibSingleButtonTextNormalColor.SlideAdjust = false;
            this.cibSingleButtonTextNormalColor.TabIndex = 6;
            this.cibSingleButtonTextNormalColor.Inputed += new System.EventHandler(this.cibSingleButtonTextNormalColor_Inputed);
            // 
            // pibSingleButtonTextDownOffset
            // 
            this.pibSingleButtonTextDownOffset.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibSingleButtonTextDownOffset.Caption = "按下偏移";
            this.pibSingleButtonTextDownOffset.CaptionWidth = 100;
            this.pibSingleButtonTextDownOffset.InputValue = new System.Drawing.Point(0, 0);
            this.pibSingleButtonTextDownOffset.Location = new System.Drawing.Point(541, 6);
            this.pibSingleButtonTextDownOffset.Name = "pibSingleButtonTextDownOffset";
            this.pibSingleButtonTextDownOffset.Size = new System.Drawing.Size(190, 24);
            this.pibSingleButtonTextDownOffset.TabIndex = 5;
            this.pibSingleButtonTextDownOffset.Inputed += new System.EventHandler(this.pibSingleButtonTextDownOffset_Inputed);
            // 
            // pibSingleButtonTextOffset
            // 
            this.pibSingleButtonTextOffset.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibSingleButtonTextOffset.Caption = "偏移";
            this.pibSingleButtonTextOffset.CaptionWidth = 60;
            this.pibSingleButtonTextOffset.InputValue = new System.Drawing.Point(0, 0);
            this.pibSingleButtonTextOffset.Location = new System.Drawing.Point(375, 6);
            this.pibSingleButtonTextOffset.Name = "pibSingleButtonTextOffset";
            this.pibSingleButtonTextOffset.Size = new System.Drawing.Size(160, 24);
            this.pibSingleButtonTextOffset.TabIndex = 4;
            this.pibSingleButtonTextOffset.Inputed += new System.EventHandler(this.pibSingleButtonTextOffset_Inputed);
            // 
            // nibSingleButtonWordSize
            // 
            this.nibSingleButtonWordSize.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSingleButtonWordSize.Caption = "字号";
            this.nibSingleButtonWordSize.CaptionWidth = 60;
            this.nibSingleButtonWordSize.InputValue = 0F;
            this.nibSingleButtonWordSize.Location = new System.Drawing.Point(232, 6);
            this.nibSingleButtonWordSize.Name = "nibSingleButtonWordSize";
            this.nibSingleButtonWordSize.Size = new System.Drawing.Size(137, 24);
            this.nibSingleButtonWordSize.TabIndex = 3;
            this.nibSingleButtonWordSize.Inputed += new System.EventHandler(this.nibSingleButtonWordSize_Inputed);
            // 
            // tibSingleButtonText
            // 
            this.tibSingleButtonText.AutoScroll = true;
            this.tibSingleButtonText.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tibSingleButtonText.Caption = "文本";
            this.tibSingleButtonText.CaptionWidth = 60;
            this.tibSingleButtonText.InputValue = "";
            this.tibSingleButtonText.Location = new System.Drawing.Point(6, 6);
            this.tibSingleButtonText.Name = "tibSingleButtonText";
            this.tibSingleButtonText.Size = new System.Drawing.Size(220, 24);
            this.tibSingleButtonText.TabIndex = 2;
            this.tibSingleButtonText.Inputed += new System.EventHandler(this.tibSingleButtonText_Inputed);
            // 
            // tbpTable
            // 
            this.tbpTable.Controls.Add(this.fibTableScrollBack);
            this.tbpTable.Controls.Add(this.fibTableScrollBar);
            this.tbpTable.Controls.Add(this.nibTableScrollBarWidth);
            this.tbpTable.Controls.Add(this.nibTableChildNumber);
            this.tbpTable.Location = new System.Drawing.Point(4, 22);
            this.tbpTable.Name = "tbpTable";
            this.tbpTable.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTable.Size = new System.Drawing.Size(916, 107);
            this.tbpTable.TabIndex = 7;
            this.tbpTable.Text = "表格";
            this.tbpTable.UseVisualStyleBackColor = true;
            // 
            // fibTableScrollBack
            // 
            this.fibTableScrollBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibTableScrollBack.Caption = "滑动槽";
            this.fibTableScrollBack.CaptionWidth = 80;
            this.fibTableScrollBack.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibTableScrollBack.FolderLimit = "";
            this.fibTableScrollBack.InputValue = "";
            this.fibTableScrollBack.Location = new System.Drawing.Point(6, 66);
            this.fibTableScrollBack.Name = "fibTableScrollBack";
            this.fibTableScrollBack.Size = new System.Drawing.Size(500, 24);
            this.fibTableScrollBack.TabIndex = 11;
            this.fibTableScrollBack.Inputed += new System.EventHandler(this.fibTableScrollBack_Inputed);
            // 
            // fibTableScrollBar
            // 
            this.fibTableScrollBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibTableScrollBar.Caption = "滑块";
            this.fibTableScrollBar.CaptionWidth = 80;
            this.fibTableScrollBar.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibTableScrollBar.FolderLimit = "";
            this.fibTableScrollBar.InputValue = "";
            this.fibTableScrollBar.Location = new System.Drawing.Point(6, 36);
            this.fibTableScrollBar.Name = "fibTableScrollBar";
            this.fibTableScrollBar.Size = new System.Drawing.Size(500, 24);
            this.fibTableScrollBar.TabIndex = 10;
            this.fibTableScrollBar.Inputed += new System.EventHandler(this.fibTableScrollBar_Inputed);
            // 
            // nibTableScrollBarWidth
            // 
            this.nibTableScrollBarWidth.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibTableScrollBarWidth.Caption = "滑动条宽度";
            this.nibTableScrollBarWidth.CaptionWidth = 120;
            this.nibTableScrollBarWidth.InputValue = 0F;
            this.nibTableScrollBarWidth.Location = new System.Drawing.Point(192, 6);
            this.nibTableScrollBarWidth.Name = "nibTableScrollBarWidth";
            this.nibTableScrollBarWidth.Size = new System.Drawing.Size(200, 24);
            this.nibTableScrollBarWidth.TabIndex = 5;
            this.nibTableScrollBarWidth.Inputed += new System.EventHandler(this.nibTableScrollBarWidth_Inputed);
            // 
            // nibTableChildNumber
            // 
            this.nibTableChildNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibTableChildNumber.Caption = "单元数";
            this.nibTableChildNumber.CaptionWidth = 80;
            this.nibTableChildNumber.InputValue = 0F;
            this.nibTableChildNumber.Location = new System.Drawing.Point(6, 6);
            this.nibTableChildNumber.Name = "nibTableChildNumber";
            this.nibTableChildNumber.Size = new System.Drawing.Size(180, 24);
            this.nibTableChildNumber.TabIndex = 4;
            this.nibTableChildNumber.Inputed += new System.EventHandler(this.nibTableChildNumber_Inputed);
            // 
            // tbpScrollTable
            // 
            this.tbpScrollTable.Controls.Add(this.nibScrollTableBasicNumber);
            this.tbpScrollTable.Controls.Add(this.iibScrollTableDirection);
            this.tbpScrollTable.Location = new System.Drawing.Point(4, 22);
            this.tbpScrollTable.Name = "tbpScrollTable";
            this.tbpScrollTable.Size = new System.Drawing.Size(916, 107);
            this.tbpScrollTable.TabIndex = 8;
            this.tbpScrollTable.Text = "滑动表格";
            this.tbpScrollTable.UseVisualStyleBackColor = true;
            // 
            // nibScrollTableBasicNumber
            // 
            this.nibScrollTableBasicNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibScrollTableBasicNumber.Caption = "基数";
            this.nibScrollTableBasicNumber.CaptionWidth = 60;
            this.nibScrollTableBasicNumber.InputValue = 0F;
            this.nibScrollTableBasicNumber.Location = new System.Drawing.Point(182, 6);
            this.nibScrollTableBasicNumber.Name = "nibScrollTableBasicNumber";
            this.nibScrollTableBasicNumber.Size = new System.Drawing.Size(160, 24);
            this.nibScrollTableBasicNumber.TabIndex = 5;
            this.nibScrollTableBasicNumber.Inputed += new System.EventHandler(this.nibScrollTableBasicNumber_Inputed);
            // 
            // iibScrollTableDirection
            // 
            this.iibScrollTableDirection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibScrollTableDirection.Caption = "方向";
            this.iibScrollTableDirection.CaptionWidth = 60;
            this.iibScrollTableDirection.InputIndex = -1;
            this.iibScrollTableDirection.InputItems = new string[] {
        "Horizontal",
        "Vertical"};
            this.iibScrollTableDirection.Location = new System.Drawing.Point(6, 6);
            this.iibScrollTableDirection.Name = "iibScrollTableDirection";
            this.iibScrollTableDirection.Size = new System.Drawing.Size(170, 24);
            this.iibScrollTableDirection.TabIndex = 2;
            this.iibScrollTableDirection.Inputed += new System.EventHandler(this.iibScrollTableDirection_Inputed);
            // 
            // tbpRadioButton
            // 
            this.tbpRadioButton.Controls.Add(this.bibRadionButtonCheck);
            this.tbpRadioButton.Controls.Add(this.nibRadionButtonGroup);
            this.tbpRadioButton.Location = new System.Drawing.Point(4, 22);
            this.tbpRadioButton.Name = "tbpRadioButton";
            this.tbpRadioButton.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRadioButton.Size = new System.Drawing.Size(916, 107);
            this.tbpRadioButton.TabIndex = 9;
            this.tbpRadioButton.Text = "单选按钮";
            this.tbpRadioButton.UseVisualStyleBackColor = true;
            // 
            // bibRadionButtonCheck
            // 
            this.bibRadionButtonCheck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibRadionButtonCheck.Caption = "选中";
            this.bibRadionButtonCheck.CaptionWidth = 60;
            this.bibRadionButtonCheck.InputValue = false;
            this.bibRadionButtonCheck.Location = new System.Drawing.Point(172, 6);
            this.bibRadionButtonCheck.Name = "bibRadionButtonCheck";
            this.bibRadionButtonCheck.Size = new System.Drawing.Size(84, 24);
            this.bibRadionButtonCheck.TabIndex = 7;
            this.bibRadionButtonCheck.Inputed += new System.EventHandler(this.bibRadionButtonCheck_Inputed);
            // 
            // nibRadionButtonGroup
            // 
            this.nibRadionButtonGroup.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibRadionButtonGroup.Caption = "按钮组";
            this.nibRadionButtonGroup.CaptionWidth = 80;
            this.nibRadionButtonGroup.InputValue = 0F;
            this.nibRadionButtonGroup.Location = new System.Drawing.Point(6, 6);
            this.nibRadionButtonGroup.Name = "nibRadionButtonGroup";
            this.nibRadionButtonGroup.Size = new System.Drawing.Size(160, 24);
            this.nibRadionButtonGroup.TabIndex = 6;
            this.nibRadionButtonGroup.Inputed += new System.EventHandler(this.nibRadionButtonGroup_Inputed);
            // 
            // tbpCheckButton
            // 
            this.tbpCheckButton.Controls.Add(this.bibCheckButtonCheck);
            this.tbpCheckButton.Location = new System.Drawing.Point(4, 22);
            this.tbpCheckButton.Name = "tbpCheckButton";
            this.tbpCheckButton.Size = new System.Drawing.Size(916, 107);
            this.tbpCheckButton.TabIndex = 10;
            this.tbpCheckButton.Text = "复选按钮";
            this.tbpCheckButton.UseVisualStyleBackColor = true;
            // 
            // bibCheckButtonCheck
            // 
            this.bibCheckButtonCheck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibCheckButtonCheck.Caption = "选中";
            this.bibCheckButtonCheck.CaptionWidth = 60;
            this.bibCheckButtonCheck.InputValue = false;
            this.bibCheckButtonCheck.Location = new System.Drawing.Point(6, 6);
            this.bibCheckButtonCheck.Name = "bibCheckButtonCheck";
            this.bibCheckButtonCheck.Size = new System.Drawing.Size(84, 24);
            this.bibCheckButtonCheck.TabIndex = 8;
            this.bibCheckButtonCheck.Inputed += new System.EventHandler(this.bibCheckButtonCheck_Inputed);
            // 
            // tbpPageTable
            // 
            this.tbpPageTable.Controls.Add(this.nibPageTableCol);
            this.tbpPageTable.Controls.Add(this.nibPageTableRow);
            this.tbpPageTable.Location = new System.Drawing.Point(4, 22);
            this.tbpPageTable.Name = "tbpPageTable";
            this.tbpPageTable.Size = new System.Drawing.Size(916, 107);
            this.tbpPageTable.TabIndex = 11;
            this.tbpPageTable.Text = "翻页表格";
            this.tbpPageTable.UseVisualStyleBackColor = true;
            // 
            // nibPageTableCol
            // 
            this.nibPageTableCol.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibPageTableCol.Caption = "列数";
            this.nibPageTableCol.CaptionWidth = 60;
            this.nibPageTableCol.InputValue = 0F;
            this.nibPageTableCol.Location = new System.Drawing.Point(152, 6);
            this.nibPageTableCol.Name = "nibPageTableCol";
            this.nibPageTableCol.Size = new System.Drawing.Size(140, 24);
            this.nibPageTableCol.TabIndex = 8;
            this.nibPageTableCol.Inputed += new System.EventHandler(this.nibPageTableCol_Inputed);
            // 
            // nibPageTableRow
            // 
            this.nibPageTableRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibPageTableRow.Caption = "行数";
            this.nibPageTableRow.CaptionWidth = 60;
            this.nibPageTableRow.InputValue = 0F;
            this.nibPageTableRow.Location = new System.Drawing.Point(6, 6);
            this.nibPageTableRow.Name = "nibPageTableRow";
            this.nibPageTableRow.Size = new System.Drawing.Size(140, 24);
            this.nibPageTableRow.TabIndex = 7;
            this.nibPageTableRow.Inputed += new System.EventHandler(this.nibPageTableRow_Inputed);
            // 
            // tbpScrollPanel
            // 
            this.tbpScrollPanel.Controls.Add(this.pibScrollPanelMove);
            this.tbpScrollPanel.Location = new System.Drawing.Point(4, 22);
            this.tbpScrollPanel.Name = "tbpScrollPanel";
            this.tbpScrollPanel.Size = new System.Drawing.Size(916, 107);
            this.tbpScrollPanel.TabIndex = 12;
            this.tbpScrollPanel.Text = "滚动面板";
            this.tbpScrollPanel.UseVisualStyleBackColor = true;
            // 
            // pibScrollPanelMove
            // 
            this.pibScrollPanelMove.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pibScrollPanelMove.Caption = "滚动位置";
            this.pibScrollPanelMove.CaptionWidth = 100;
            this.pibScrollPanelMove.InputValue = new System.Drawing.Point(0, 0);
            this.pibScrollPanelMove.Location = new System.Drawing.Point(6, 6);
            this.pibScrollPanelMove.Name = "pibScrollPanelMove";
            this.pibScrollPanelMove.Size = new System.Drawing.Size(200, 24);
            this.pibScrollPanelMove.TabIndex = 6;
            this.pibScrollPanelMove.Inputed += new System.EventHandler(this.pibScrollPanelMove_Inputed);
            // 
            // tbpNumberImage
            // 
            this.tbpNumberImage.Controls.Add(this.nibNumberImageUpperLimitValue);
            this.tbpNumberImage.Controls.Add(this.fibNumberImageUpperLimitFile);
            this.tbpNumberImage.Controls.Add(this.nibNumberImageGap);
            this.tbpNumberImage.Controls.Add(this.iibNumberImageDirection);
            this.tbpNumberImage.Controls.Add(this.nibNumberImageValue);
            this.tbpNumberImage.Controls.Add(this.nibNumberImageZoom);
            this.tbpNumberImage.Controls.Add(this.iibNumberImageAlign);
            this.tbpNumberImage.Controls.Add(this.fibNumberImageFile);
            this.tbpNumberImage.Location = new System.Drawing.Point(4, 22);
            this.tbpNumberImage.Name = "tbpNumberImage";
            this.tbpNumberImage.Size = new System.Drawing.Size(916, 107);
            this.tbpNumberImage.TabIndex = 13;
            this.tbpNumberImage.Text = "数字图像";
            this.tbpNumberImage.UseVisualStyleBackColor = true;
            // 
            // nibNumberImageUpperLimitValue
            // 
            this.nibNumberImageUpperLimitValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibNumberImageUpperLimitValue.Caption = "上限";
            this.nibNumberImageUpperLimitValue.CaptionWidth = 60;
            this.nibNumberImageUpperLimitValue.InputValue = 0F;
            this.nibNumberImageUpperLimitValue.Location = new System.Drawing.Point(434, 36);
            this.nibNumberImageUpperLimitValue.Name = "nibNumberImageUpperLimitValue";
            this.nibNumberImageUpperLimitValue.Size = new System.Drawing.Size(150, 24);
            this.nibNumberImageUpperLimitValue.TabIndex = 11;
            this.nibNumberImageUpperLimitValue.Inputed += new System.EventHandler(this.nibNumberImageUpLimitValue_Inputed);
            // 
            // fibNumberImageUpperLimitFile
            // 
            this.fibNumberImageUpperLimitFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibNumberImageUpperLimitFile.Caption = "填充";
            this.fibNumberImageUpperLimitFile.CaptionWidth = 60;
            this.fibNumberImageUpperLimitFile.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibNumberImageUpperLimitFile.FolderLimit = "";
            this.fibNumberImageUpperLimitFile.InputValue = "";
            this.fibNumberImageUpperLimitFile.Location = new System.Drawing.Point(6, 36);
            this.fibNumberImageUpperLimitFile.Name = "fibNumberImageUpperLimitFile";
            this.fibNumberImageUpperLimitFile.Size = new System.Drawing.Size(422, 24);
            this.fibNumberImageUpperLimitFile.TabIndex = 10;
            this.fibNumberImageUpperLimitFile.Inputed += new System.EventHandler(this.fibNumberImageUpLimitFile_Inputed);
            // 
            // nibNumberImageGap
            // 
            this.nibNumberImageGap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibNumberImageGap.Caption = "间隔";
            this.nibNumberImageGap.CaptionWidth = 60;
            this.nibNumberImageGap.InputValue = 0F;
            this.nibNumberImageGap.Location = new System.Drawing.Point(132, 66);
            this.nibNumberImageGap.Name = "nibNumberImageGap";
            this.nibNumberImageGap.Size = new System.Drawing.Size(120, 24);
            this.nibNumberImageGap.TabIndex = 9;
            this.nibNumberImageGap.Inputed += new System.EventHandler(this.nibNumberImageGap_Inputed);
            // 
            // iibNumberImageDirection
            // 
            this.iibNumberImageDirection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibNumberImageDirection.Caption = "方向";
            this.iibNumberImageDirection.CaptionWidth = 60;
            this.iibNumberImageDirection.InputIndex = -1;
            this.iibNumberImageDirection.InputItems = new string[] {
        "Horizontal",
        "Vertical"};
            this.iibNumberImageDirection.Location = new System.Drawing.Point(434, 66);
            this.iibNumberImageDirection.Name = "iibNumberImageDirection";
            this.iibNumberImageDirection.Size = new System.Drawing.Size(150, 24);
            this.iibNumberImageDirection.TabIndex = 8;
            this.iibNumberImageDirection.Inputed += new System.EventHandler(this.iibNumberImageDirection_Inputed);
            // 
            // nibNumberImageValue
            // 
            this.nibNumberImageValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibNumberImageValue.Caption = "数值";
            this.nibNumberImageValue.CaptionWidth = 60;
            this.nibNumberImageValue.InputValue = 0F;
            this.nibNumberImageValue.Location = new System.Drawing.Point(434, 6);
            this.nibNumberImageValue.Name = "nibNumberImageValue";
            this.nibNumberImageValue.Size = new System.Drawing.Size(150, 24);
            this.nibNumberImageValue.TabIndex = 7;
            this.nibNumberImageValue.Inputed += new System.EventHandler(this.nibNumberImageValue_Inputed);
            // 
            // nibNumberImageZoom
            // 
            this.nibNumberImageZoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibNumberImageZoom.Caption = "缩放";
            this.nibNumberImageZoom.CaptionWidth = 60;
            this.nibNumberImageZoom.InputValue = 0F;
            this.nibNumberImageZoom.Location = new System.Drawing.Point(6, 66);
            this.nibNumberImageZoom.Name = "nibNumberImageZoom";
            this.nibNumberImageZoom.Size = new System.Drawing.Size(120, 24);
            this.nibNumberImageZoom.TabIndex = 6;
            this.nibNumberImageZoom.Inputed += new System.EventHandler(this.nibNumberImageZoom_Inputed);
            // 
            // iibNumberImageAlign
            // 
            this.iibNumberImageAlign.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibNumberImageAlign.Caption = "对齐";
            this.iibNumberImageAlign.CaptionWidth = 60;
            this.iibNumberImageAlign.InputIndex = -1;
            this.iibNumberImageAlign.InputItems = new string[] {
        "Start",
        "Middle",
        "End"};
            this.iibNumberImageAlign.Location = new System.Drawing.Point(258, 66);
            this.iibNumberImageAlign.Name = "iibNumberImageAlign";
            this.iibNumberImageAlign.Size = new System.Drawing.Size(170, 24);
            this.iibNumberImageAlign.TabIndex = 5;
            this.iibNumberImageAlign.Inputed += new System.EventHandler(this.iibNumberImageAlign_Inputed);
            // 
            // fibNumberImageFile
            // 
            this.fibNumberImageFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibNumberImageFile.Caption = "图像";
            this.fibNumberImageFile.CaptionWidth = 60;
            this.fibNumberImageFile.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibNumberImageFile.FolderLimit = "";
            this.fibNumberImageFile.InputValue = "";
            this.fibNumberImageFile.Location = new System.Drawing.Point(6, 6);
            this.fibNumberImageFile.Name = "fibNumberImageFile";
            this.fibNumberImageFile.Size = new System.Drawing.Size(422, 24);
            this.fibNumberImageFile.TabIndex = 1;
            this.fibNumberImageFile.Inputed += new System.EventHandler(this.fibNumberImageFile_Inputed);
            // 
            // tbpTextArea
            // 
            this.tbpTextArea.Controls.Add(this.fibTextAreaScrollBack);
            this.tbpTextArea.Controls.Add(this.fibTextAreaScrollBar);
            this.tbpTextArea.Controls.Add(this.nibTextAreaScrollBarWidth);
            this.tbpTextArea.Location = new System.Drawing.Point(4, 22);
            this.tbpTextArea.Name = "tbpTextArea";
            this.tbpTextArea.Size = new System.Drawing.Size(916, 107);
            this.tbpTextArea.TabIndex = 14;
            this.tbpTextArea.Text = "文本域";
            this.tbpTextArea.UseVisualStyleBackColor = true;
            // 
            // fibTextAreaScrollBack
            // 
            this.fibTextAreaScrollBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibTextAreaScrollBack.Caption = "滑动槽";
            this.fibTextAreaScrollBack.CaptionWidth = 80;
            this.fibTextAreaScrollBack.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibTextAreaScrollBack.FolderLimit = "";
            this.fibTextAreaScrollBack.InputValue = "";
            this.fibTextAreaScrollBack.Location = new System.Drawing.Point(6, 66);
            this.fibTextAreaScrollBack.Name = "fibTextAreaScrollBack";
            this.fibTextAreaScrollBack.Size = new System.Drawing.Size(500, 24);
            this.fibTextAreaScrollBack.TabIndex = 14;
            this.fibTextAreaScrollBack.Inputed += new System.EventHandler(this.fibTextAreaScrollBack_Inputed);
            // 
            // fibTextAreaScrollBar
            // 
            this.fibTextAreaScrollBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibTextAreaScrollBar.Caption = "滑块";
            this.fibTextAreaScrollBar.CaptionWidth = 80;
            this.fibTextAreaScrollBar.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibTextAreaScrollBar.FolderLimit = "";
            this.fibTextAreaScrollBar.InputValue = "";
            this.fibTextAreaScrollBar.Location = new System.Drawing.Point(6, 36);
            this.fibTextAreaScrollBar.Name = "fibTextAreaScrollBar";
            this.fibTextAreaScrollBar.Size = new System.Drawing.Size(500, 24);
            this.fibTextAreaScrollBar.TabIndex = 13;
            this.fibTextAreaScrollBar.Inputed += new System.EventHandler(this.fibTextAreaScrollBar_Inputed);
            // 
            // nibTextAreaScrollBarWidth
            // 
            this.nibTextAreaScrollBarWidth.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibTextAreaScrollBarWidth.Caption = "滑动条宽度";
            this.nibTextAreaScrollBarWidth.CaptionWidth = 120;
            this.nibTextAreaScrollBarWidth.InputValue = 0F;
            this.nibTextAreaScrollBarWidth.Location = new System.Drawing.Point(6, 6);
            this.nibTextAreaScrollBarWidth.Name = "nibTextAreaScrollBarWidth";
            this.nibTextAreaScrollBarWidth.Size = new System.Drawing.Size(220, 24);
            this.nibTextAreaScrollBarWidth.TabIndex = 12;
            this.nibTextAreaScrollBarWidth.Inputed += new System.EventHandler(this.nibTextAreaScrollBarWidth_Inputed);
            // 
            // tbpImageNumber
            // 
            this.tbpImageNumber.Controls.Add(this.nibImageNumberValue);
            this.tbpImageNumber.Controls.Add(this.nibImageNumberZoom);
            this.tbpImageNumber.Controls.Add(this.iibImageNumberAlign);
            this.tbpImageNumber.Controls.Add(this.fibImageNumberFile);
            this.tbpImageNumber.Location = new System.Drawing.Point(4, 22);
            this.tbpImageNumber.Name = "tbpImageNumber";
            this.tbpImageNumber.Size = new System.Drawing.Size(916, 107);
            this.tbpImageNumber.TabIndex = 15;
            this.tbpImageNumber.Text = "图像数字";
            this.tbpImageNumber.UseVisualStyleBackColor = true;
            // 
            // nibImageNumberValue
            // 
            this.nibImageNumberValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibImageNumberValue.Caption = "数值";
            this.nibImageNumberValue.CaptionWidth = 60;
            this.nibImageNumberValue.InputValue = 0F;
            this.nibImageNumberValue.Location = new System.Drawing.Point(414, 6);
            this.nibImageNumberValue.Name = "nibImageNumberValue";
            this.nibImageNumberValue.Size = new System.Drawing.Size(150, 24);
            this.nibImageNumberValue.TabIndex = 12;
            this.nibImageNumberValue.Inputed += new System.EventHandler(this.nibImageNumberValue_Inputed);
            // 
            // nibImageNumberZoom
            // 
            this.nibImageNumberZoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibImageNumberZoom.Caption = "缩放";
            this.nibImageNumberZoom.CaptionWidth = 60;
            this.nibImageNumberZoom.InputValue = 0F;
            this.nibImageNumberZoom.Location = new System.Drawing.Point(6, 36);
            this.nibImageNumberZoom.Name = "nibImageNumberZoom";
            this.nibImageNumberZoom.Size = new System.Drawing.Size(150, 24);
            this.nibImageNumberZoom.TabIndex = 11;
            this.nibImageNumberZoom.Inputed += new System.EventHandler(this.nibImageNumberZoom_Inputed);
            // 
            // iibImageNumberAlign
            // 
            this.iibImageNumberAlign.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibImageNumberAlign.Caption = "对齐";
            this.iibImageNumberAlign.CaptionWidth = 60;
            this.iibImageNumberAlign.InputIndex = -1;
            this.iibImageNumberAlign.InputItems = new string[] {
        "Start",
        "Middle",
        "End"};
            this.iibImageNumberAlign.Location = new System.Drawing.Point(162, 36);
            this.iibImageNumberAlign.Name = "iibImageNumberAlign";
            this.iibImageNumberAlign.Size = new System.Drawing.Size(170, 24);
            this.iibImageNumberAlign.TabIndex = 10;
            this.iibImageNumberAlign.Inputed += new System.EventHandler(this.iibImageNumberAlign_Inputed);
            // 
            // fibImageNumberFile
            // 
            this.fibImageNumberFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibImageNumberFile.Caption = "图像";
            this.fibImageNumberFile.CaptionWidth = 60;
            this.fibImageNumberFile.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibImageNumberFile.FolderLimit = "";
            this.fibImageNumberFile.InputValue = "";
            this.fibImageNumberFile.Location = new System.Drawing.Point(6, 6);
            this.fibImageNumberFile.Name = "fibImageNumberFile";
            this.fibImageNumberFile.Size = new System.Drawing.Size(402, 24);
            this.fibImageNumberFile.TabIndex = 9;
            this.fibImageNumberFile.Inputed += new System.EventHandler(this.fibImageNumberFile_Inputed);
            // 
            // tbpTextBox
            // 
            this.tbpTextBox.Controls.Add(this.iibTextBoxInputType);
            this.tbpTextBox.Controls.Add(this.iibTextBoxAlign);
            this.tbpTextBox.Location = new System.Drawing.Point(4, 22);
            this.tbpTextBox.Name = "tbpTextBox";
            this.tbpTextBox.Size = new System.Drawing.Size(916, 107);
            this.tbpTextBox.TabIndex = 16;
            this.tbpTextBox.Text = "文本框";
            this.tbpTextBox.UseVisualStyleBackColor = true;
            // 
            // iibTextBoxInputType
            // 
            this.iibTextBoxInputType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibTextBoxInputType.Caption = "输入类型";
            this.iibTextBoxInputType.CaptionWidth = 100;
            this.iibTextBoxInputType.InputIndex = -1;
            this.iibTextBoxInputType.InputItems = new string[] {
        "Text",
        "Password"};
            this.iibTextBoxInputType.Location = new System.Drawing.Point(182, 6);
            this.iibTextBoxInputType.Name = "iibTextBoxInputType";
            this.iibTextBoxInputType.Size = new System.Drawing.Size(210, 24);
            this.iibTextBoxInputType.TabIndex = 2;
            this.iibTextBoxInputType.Inputed += new System.EventHandler(this.iibTextBoxInputType_Inputed);
            // 
            // iibTextBoxAlign
            // 
            this.iibTextBoxAlign.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibTextBoxAlign.Caption = "对齐";
            this.iibTextBoxAlign.CaptionWidth = 60;
            this.iibTextBoxAlign.InputIndex = -1;
            this.iibTextBoxAlign.InputItems = new string[] {
        "TopLeft",
        "Top",
        "TopRight",
        "Left",
        "Center",
        "Right",
        "BottomLeft",
        "Bottom",
        "BottomRight"};
            this.iibTextBoxAlign.Location = new System.Drawing.Point(6, 6);
            this.iibTextBoxAlign.Name = "iibTextBoxAlign";
            this.iibTextBoxAlign.Size = new System.Drawing.Size(170, 24);
            this.iibTextBoxAlign.TabIndex = 1;
            this.iibTextBoxAlign.Inputed += new System.EventHandler(this.iibTextBoxAlign_Inputed);
            // 
            // tbpProgressBar
            // 
            this.tbpProgressBar.Controls.Add(this.nibProgressBarEndValue);
            this.tbpProgressBar.Controls.Add(this.nibProgressBarCurrentValue);
            this.tbpProgressBar.Controls.Add(this.nibProgressBarStartValue);
            this.tbpProgressBar.Controls.Add(this.fibProgressBarSlotImage);
            this.tbpProgressBar.Controls.Add(this.cibProgressBarFillColor);
            this.tbpProgressBar.Controls.Add(this.iibProgressBarDirection);
            this.tbpProgressBar.Controls.Add(this.fibProgressBarFillImage);
            this.tbpProgressBar.Location = new System.Drawing.Point(4, 22);
            this.tbpProgressBar.Name = "tbpProgressBar";
            this.tbpProgressBar.Size = new System.Drawing.Size(916, 107);
            this.tbpProgressBar.TabIndex = 17;
            this.tbpProgressBar.Text = "进度条";
            this.tbpProgressBar.UseVisualStyleBackColor = true;
            // 
            // nibProgressBarEndValue
            // 
            this.nibProgressBarEndValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibProgressBarEndValue.Caption = "结束值";
            this.nibProgressBarEndValue.CaptionWidth = 80;
            this.nibProgressBarEndValue.InputValue = 0F;
            this.nibProgressBarEndValue.Location = new System.Drawing.Point(358, 66);
            this.nibProgressBarEndValue.Name = "nibProgressBarEndValue";
            this.nibProgressBarEndValue.Size = new System.Drawing.Size(170, 24);
            this.nibProgressBarEndValue.TabIndex = 16;
            this.nibProgressBarEndValue.Inputed += new System.EventHandler(this.nibProgressBarEndValue_Inputed);
            // 
            // nibProgressBarCurrentValue
            // 
            this.nibProgressBarCurrentValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibProgressBarCurrentValue.Caption = "当前值";
            this.nibProgressBarCurrentValue.CaptionWidth = 80;
            this.nibProgressBarCurrentValue.InputValue = 0F;
            this.nibProgressBarCurrentValue.Location = new System.Drawing.Point(182, 66);
            this.nibProgressBarCurrentValue.Name = "nibProgressBarCurrentValue";
            this.nibProgressBarCurrentValue.Size = new System.Drawing.Size(170, 24);
            this.nibProgressBarCurrentValue.TabIndex = 15;
            this.nibProgressBarCurrentValue.Inputed += new System.EventHandler(this.nibProgressBarCurrentValue_Inputed);
            // 
            // nibProgressBarStartValue
            // 
            this.nibProgressBarStartValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibProgressBarStartValue.Caption = "起始值";
            this.nibProgressBarStartValue.CaptionWidth = 80;
            this.nibProgressBarStartValue.InputValue = 0F;
            this.nibProgressBarStartValue.Location = new System.Drawing.Point(6, 66);
            this.nibProgressBarStartValue.Name = "nibProgressBarStartValue";
            this.nibProgressBarStartValue.Size = new System.Drawing.Size(170, 24);
            this.nibProgressBarStartValue.TabIndex = 14;
            this.nibProgressBarStartValue.Inputed += new System.EventHandler(this.nibProgressBarStartValue_Inputed);
            // 
            // fibProgressBarSlotImage
            // 
            this.fibProgressBarSlotImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProgressBarSlotImage.Caption = "背景图像";
            this.fibProgressBarSlotImage.CaptionWidth = 100;
            this.fibProgressBarSlotImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibProgressBarSlotImage.FolderLimit = "";
            this.fibProgressBarSlotImage.InputValue = "";
            this.fibProgressBarSlotImage.Location = new System.Drawing.Point(6, 36);
            this.fibProgressBarSlotImage.Name = "fibProgressBarSlotImage";
            this.fibProgressBarSlotImage.Size = new System.Drawing.Size(440, 24);
            this.fibProgressBarSlotImage.TabIndex = 13;
            this.fibProgressBarSlotImage.Inputed += new System.EventHandler(this.fibProgressBarSlotImage_Inputed);
            // 
            // cibProgressBarFillColor
            // 
            this.cibProgressBarFillColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cibProgressBarFillColor.Caption = "填充色";
            this.cibProgressBarFillColor.CaptionWidth = 80;
            this.cibProgressBarFillColor.InputValue = System.Drawing.Color.White;
            this.cibProgressBarFillColor.Location = new System.Drawing.Point(452, 6);
            this.cibProgressBarFillColor.Name = "cibProgressBarFillColor";
            this.cibProgressBarFillColor.Size = new System.Drawing.Size(270, 24);
            this.cibProgressBarFillColor.SlideAdjust = false;
            this.cibProgressBarFillColor.TabIndex = 12;
            this.cibProgressBarFillColor.Inputed += new System.EventHandler(this.cibProgressBarFillColor_Inputed);
            // 
            // iibProgressBarDirection
            // 
            this.iibProgressBarDirection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibProgressBarDirection.Caption = "方向";
            this.iibProgressBarDirection.CaptionWidth = 60;
            this.iibProgressBarDirection.InputIndex = -1;
            this.iibProgressBarDirection.InputItems = new string[] {
        "Left",
        "Right",
        "Up",
        "Down"};
            this.iibProgressBarDirection.Location = new System.Drawing.Point(452, 36);
            this.iibProgressBarDirection.Name = "iibProgressBarDirection";
            this.iibProgressBarDirection.Size = new System.Drawing.Size(170, 24);
            this.iibProgressBarDirection.TabIndex = 11;
            this.iibProgressBarDirection.Inputed += new System.EventHandler(this.iibProgressBarDirection_Inputed);
            // 
            // fibProgressBarFillImage
            // 
            this.fibProgressBarFillImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibProgressBarFillImage.Caption = "填充图像";
            this.fibProgressBarFillImage.CaptionWidth = 100;
            this.fibProgressBarFillImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibProgressBarFillImage.FolderLimit = "";
            this.fibProgressBarFillImage.InputValue = "";
            this.fibProgressBarFillImage.Location = new System.Drawing.Point(6, 6);
            this.fibProgressBarFillImage.Name = "fibProgressBarFillImage";
            this.fibProgressBarFillImage.Size = new System.Drawing.Size(440, 24);
            this.fibProgressBarFillImage.TabIndex = 10;
            this.fibProgressBarFillImage.Inputed += new System.EventHandler(this.fibProgressBarFillImage_Inputed);
            // 
            // tbpSliderBar
            // 
            this.tbpSliderBar.Controls.Add(this.nibSliderBarZoom);
            this.tbpSliderBar.Controls.Add(this.fibSliderBarSlotImage);
            this.tbpSliderBar.Controls.Add(this.iibSliderBarDirection);
            this.tbpSliderBar.Controls.Add(this.fibSliderBarImage);
            this.tbpSliderBar.Controls.Add(this.nibSliderBarEndValue);
            this.tbpSliderBar.Controls.Add(this.nibSliderBarCurrentValue);
            this.tbpSliderBar.Controls.Add(this.nibSliderBarStartValue);
            this.tbpSliderBar.Location = new System.Drawing.Point(4, 22);
            this.tbpSliderBar.Name = "tbpSliderBar";
            this.tbpSliderBar.Size = new System.Drawing.Size(916, 107);
            this.tbpSliderBar.TabIndex = 18;
            this.tbpSliderBar.Text = "滑动条";
            this.tbpSliderBar.UseVisualStyleBackColor = true;
            // 
            // nibSliderBarZoom
            // 
            this.nibSliderBarZoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSliderBarZoom.Caption = "缩放";
            this.nibSliderBarZoom.CaptionWidth = 60;
            this.nibSliderBarZoom.InputValue = 0F;
            this.nibSliderBarZoom.Location = new System.Drawing.Point(452, 6);
            this.nibSliderBarZoom.Name = "nibSliderBarZoom";
            this.nibSliderBarZoom.Size = new System.Drawing.Size(170, 24);
            this.nibSliderBarZoom.TabIndex = 24;
            this.nibSliderBarZoom.Inputed += new System.EventHandler(this.nibSliderBarZoom_Inputed);
            // 
            // fibSliderBarSlotImage
            // 
            this.fibSliderBarSlotImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibSliderBarSlotImage.Caption = "背景图像";
            this.fibSliderBarSlotImage.CaptionWidth = 100;
            this.fibSliderBarSlotImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibSliderBarSlotImage.FolderLimit = "";
            this.fibSliderBarSlotImage.InputValue = "";
            this.fibSliderBarSlotImage.Location = new System.Drawing.Point(6, 36);
            this.fibSliderBarSlotImage.Name = "fibSliderBarSlotImage";
            this.fibSliderBarSlotImage.Size = new System.Drawing.Size(440, 24);
            this.fibSliderBarSlotImage.TabIndex = 23;
            this.fibSliderBarSlotImage.Inputed += new System.EventHandler(this.fibSliderBarSlotImage_Inputed);
            // 
            // iibSliderBarDirection
            // 
            this.iibSliderBarDirection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.iibSliderBarDirection.Caption = "方向";
            this.iibSliderBarDirection.CaptionWidth = 60;
            this.iibSliderBarDirection.InputIndex = -1;
            this.iibSliderBarDirection.InputItems = new string[] {
        "Horizontal",
        "Vertical"};
            this.iibSliderBarDirection.Location = new System.Drawing.Point(452, 36);
            this.iibSliderBarDirection.Name = "iibSliderBarDirection";
            this.iibSliderBarDirection.Size = new System.Drawing.Size(170, 24);
            this.iibSliderBarDirection.TabIndex = 21;
            this.iibSliderBarDirection.Inputed += new System.EventHandler(this.iibSliderBarDirection_Inputed);
            // 
            // fibSliderBarImage
            // 
            this.fibSliderBarImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibSliderBarImage.Caption = "滑块图像";
            this.fibSliderBarImage.CaptionWidth = 100;
            this.fibSliderBarImage.Filter = "图像文件 (*.jpg *.png)|*.jpg;*.png";
            this.fibSliderBarImage.FolderLimit = "";
            this.fibSliderBarImage.InputValue = "";
            this.fibSliderBarImage.Location = new System.Drawing.Point(6, 6);
            this.fibSliderBarImage.Name = "fibSliderBarImage";
            this.fibSliderBarImage.Size = new System.Drawing.Size(440, 24);
            this.fibSliderBarImage.TabIndex = 20;
            this.fibSliderBarImage.Inputed += new System.EventHandler(this.fibSliderBarImage_Inputed);
            // 
            // nibSliderBarEndValue
            // 
            this.nibSliderBarEndValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSliderBarEndValue.Caption = "结束值";
            this.nibSliderBarEndValue.CaptionWidth = 80;
            this.nibSliderBarEndValue.InputValue = 0F;
            this.nibSliderBarEndValue.Location = new System.Drawing.Point(358, 66);
            this.nibSliderBarEndValue.Name = "nibSliderBarEndValue";
            this.nibSliderBarEndValue.Size = new System.Drawing.Size(170, 24);
            this.nibSliderBarEndValue.TabIndex = 19;
            this.nibSliderBarEndValue.Inputed += new System.EventHandler(this.nibSliderBarEndValue_Inputed);
            // 
            // nibSliderBarCurrentValue
            // 
            this.nibSliderBarCurrentValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSliderBarCurrentValue.Caption = "当前值";
            this.nibSliderBarCurrentValue.CaptionWidth = 80;
            this.nibSliderBarCurrentValue.InputValue = 0F;
            this.nibSliderBarCurrentValue.Location = new System.Drawing.Point(182, 66);
            this.nibSliderBarCurrentValue.Name = "nibSliderBarCurrentValue";
            this.nibSliderBarCurrentValue.Size = new System.Drawing.Size(170, 24);
            this.nibSliderBarCurrentValue.TabIndex = 18;
            this.nibSliderBarCurrentValue.Inputed += new System.EventHandler(this.nibSliderBarCurrentValue_Inputed);
            // 
            // nibSliderBarStartValue
            // 
            this.nibSliderBarStartValue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSliderBarStartValue.Caption = "起始值";
            this.nibSliderBarStartValue.CaptionWidth = 80;
            this.nibSliderBarStartValue.InputValue = 0F;
            this.nibSliderBarStartValue.Location = new System.Drawing.Point(6, 66);
            this.nibSliderBarStartValue.Name = "nibSliderBarStartValue";
            this.nibSliderBarStartValue.Size = new System.Drawing.Size(170, 24);
            this.nibSliderBarStartValue.TabIndex = 17;
            this.nibSliderBarStartValue.Inputed += new System.EventHandler(this.nibSliderBarStartValue_Inputed);
            // 
            // tbpParticleView
            // 
            this.tbpParticleView.Controls.Add(this.nibParticleViewScaleY);
            this.tbpParticleView.Controls.Add(this.nibParticleViewScaleX);
            this.tbpParticleView.Controls.Add(this.bibParticleViewParentClip);
            this.tbpParticleView.Controls.Add(this.fibParticleViewFile);
            this.tbpParticleView.Location = new System.Drawing.Point(4, 22);
            this.tbpParticleView.Name = "tbpParticleView";
            this.tbpParticleView.Padding = new System.Windows.Forms.Padding(3);
            this.tbpParticleView.Size = new System.Drawing.Size(916, 107);
            this.tbpParticleView.TabIndex = 19;
            this.tbpParticleView.Text = "粒子";
            this.tbpParticleView.UseVisualStyleBackColor = true;
            // 
            // bibParticleViewParentClip
            // 
            this.bibParticleViewParentClip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibParticleViewParentClip.Caption = "裁剪";
            this.bibParticleViewParentClip.CaptionWidth = 60;
            this.bibParticleViewParentClip.InputValue = false;
            this.bibParticleViewParentClip.Location = new System.Drawing.Point(414, 6);
            this.bibParticleViewParentClip.Name = "bibParticleViewParentClip";
            this.bibParticleViewParentClip.Size = new System.Drawing.Size(84, 24);
            this.bibParticleViewParentClip.TabIndex = 8;
            this.bibParticleViewParentClip.Inputed += new System.EventHandler(this.bibParticleViewParentClip_Inputed);
            // 
            // fibParticleViewFile
            // 
            this.fibParticleViewFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibParticleViewFile.Caption = "粒子";
            this.fibParticleViewFile.CaptionWidth = 60;
            this.fibParticleViewFile.Filter = "粒子文件 (*.ps)|*.ps";
            this.fibParticleViewFile.FolderLimit = "";
            this.fibParticleViewFile.InputValue = "";
            this.fibParticleViewFile.Location = new System.Drawing.Point(6, 6);
            this.fibParticleViewFile.Name = "fibParticleViewFile";
            this.fibParticleViewFile.Size = new System.Drawing.Size(402, 24);
            this.fibParticleViewFile.TabIndex = 7;
            this.fibParticleViewFile.Inputed += new System.EventHandler(this.fibParticleViewFile_Inputed);
            // 
            // tbpSpineView
            // 
            this.tbpSpineView.Controls.Add(this.nibSpineViewScaleY);
            this.tbpSpineView.Controls.Add(this.nibSpineViewScaleX);
            this.tbpSpineView.Controls.Add(this.fibSpineViewAnimation);
            this.tbpSpineView.Controls.Add(this.bibSpineViewLoop);
            this.tbpSpineView.Controls.Add(this.fibSpineViewFile);
            this.tbpSpineView.Location = new System.Drawing.Point(4, 22);
            this.tbpSpineView.Name = "tbpSpineView";
            this.tbpSpineView.Size = new System.Drawing.Size(916, 107);
            this.tbpSpineView.TabIndex = 20;
            this.tbpSpineView.Text = "Spine动画";
            this.tbpSpineView.UseVisualStyleBackColor = true;
            // 
            // fibSpineViewAnimation
            // 
            this.fibSpineViewAnimation.AutoScroll = true;
            this.fibSpineViewAnimation.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibSpineViewAnimation.Caption = "动画";
            this.fibSpineViewAnimation.CaptionWidth = 60;
            this.fibSpineViewAnimation.InputValue = "";
            this.fibSpineViewAnimation.Location = new System.Drawing.Point(6, 36);
            this.fibSpineViewAnimation.Name = "fibSpineViewAnimation";
            this.fibSpineViewAnimation.Size = new System.Drawing.Size(340, 24);
            this.fibSpineViewAnimation.TabIndex = 13;
            this.fibSpineViewAnimation.Inputed += new System.EventHandler(this.fibSpineViewAnimation_Inputed);
            // 
            // bibSpineViewLoop
            // 
            this.bibSpineViewLoop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bibSpineViewLoop.Caption = "循环";
            this.bibSpineViewLoop.CaptionWidth = 60;
            this.bibSpineViewLoop.InputValue = false;
            this.bibSpineViewLoop.Location = new System.Drawing.Point(352, 36);
            this.bibSpineViewLoop.Name = "bibSpineViewLoop";
            this.bibSpineViewLoop.Size = new System.Drawing.Size(84, 24);
            this.bibSpineViewLoop.TabIndex = 12;
            this.bibSpineViewLoop.Inputed += new System.EventHandler(this.bibSpineViewLoop_Inputed);
            // 
            // fibSpineViewFile
            // 
            this.fibSpineViewFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fibSpineViewFile.Caption = "文件";
            this.fibSpineViewFile.CaptionWidth = 60;
            this.fibSpineViewFile.Filter = "动画文件 (*.json)|*.json";
            this.fibSpineViewFile.FolderLimit = "";
            this.fibSpineViewFile.InputValue = "";
            this.fibSpineViewFile.Location = new System.Drawing.Point(6, 6);
            this.fibSpineViewFile.Name = "fibSpineViewFile";
            this.fibSpineViewFile.Size = new System.Drawing.Size(430, 24);
            this.fibSpineViewFile.TabIndex = 8;
            this.fibSpineViewFile.Inputed += new System.EventHandler(this.fibSineViewFile_Inputed);
            // 
            // nibParticleViewScaleY
            // 
            this.nibParticleViewScaleY.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibParticleViewScaleY.Caption = "Y缩放";
            this.nibParticleViewScaleY.CaptionWidth = 70;
            this.nibParticleViewScaleY.InputValue = 0F;
            this.nibParticleViewScaleY.Location = new System.Drawing.Point(152, 36);
            this.nibParticleViewScaleY.Name = "nibParticleViewScaleY";
            this.nibParticleViewScaleY.Size = new System.Drawing.Size(140, 24);
            this.nibParticleViewScaleY.TabIndex = 10;
            this.nibParticleViewScaleY.Inputed += new System.EventHandler(this.nibParticleViewScaleY_Inputed);
            // 
            // nibParticleViewScaleX
            // 
            this.nibParticleViewScaleX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibParticleViewScaleX.Caption = "X缩放";
            this.nibParticleViewScaleX.CaptionWidth = 70;
            this.nibParticleViewScaleX.InputValue = 0F;
            this.nibParticleViewScaleX.Location = new System.Drawing.Point(6, 36);
            this.nibParticleViewScaleX.Name = "nibParticleViewScaleX";
            this.nibParticleViewScaleX.Size = new System.Drawing.Size(140, 24);
            this.nibParticleViewScaleX.TabIndex = 9;
            this.nibParticleViewScaleX.Inputed += new System.EventHandler(this.nibParticleViewScaleX_Inputed);
            // 
            // nibSpineViewScaleY
            // 
            this.nibSpineViewScaleY.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpineViewScaleY.Caption = "Y缩放";
            this.nibSpineViewScaleY.CaptionWidth = 70;
            this.nibSpineViewScaleY.InputValue = 0F;
            this.nibSpineViewScaleY.Location = new System.Drawing.Point(152, 66);
            this.nibSpineViewScaleY.Name = "nibSpineViewScaleY";
            this.nibSpineViewScaleY.Size = new System.Drawing.Size(140, 24);
            this.nibSpineViewScaleY.TabIndex = 15;
            this.nibSpineViewScaleY.Inputed += new System.EventHandler(this.nibSpineViewScaleY_Inputed);
            // 
            // nibSpineViewScaleX
            // 
            this.nibSpineViewScaleX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.nibSpineViewScaleX.Caption = "X缩放";
            this.nibSpineViewScaleX.CaptionWidth = 70;
            this.nibSpineViewScaleX.InputValue = 0F;
            this.nibSpineViewScaleX.Location = new System.Drawing.Point(6, 66);
            this.nibSpineViewScaleX.Name = "nibSpineViewScaleX";
            this.nibSpineViewScaleX.Size = new System.Drawing.Size(140, 24);
            this.nibSpineViewScaleX.TabIndex = 14;
            this.nibSpineViewScaleX.Inputed += new System.EventHandler(this.nibSpineViewScaleX_Inputed);
            // 
            // ControlPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 133);
            this.Controls.Add(this.tbcProperty);
            this.Name = "ControlPropertyForm";
            this.Text = "属性";
            this.tbcProperty.ResumeLayout(false);
            this.tbpControl.ResumeLayout(false);
            this.tbpPicture.ResumeLayout(false);
            this.tbpContainer.ResumeLayout(false);
            this.tbpTextControl.ResumeLayout(false);
            this.tbpLabel.ResumeLayout(false);
            this.tbpButton.ResumeLayout(false);
            this.tbpSingleButton.ResumeLayout(false);
            this.tbpTable.ResumeLayout(false);
            this.tbpScrollTable.ResumeLayout(false);
            this.tbpRadioButton.ResumeLayout(false);
            this.tbpCheckButton.ResumeLayout(false);
            this.tbpPageTable.ResumeLayout(false);
            this.tbpScrollPanel.ResumeLayout(false);
            this.tbpNumberImage.ResumeLayout(false);
            this.tbpTextArea.ResumeLayout(false);
            this.tbpImageNumber.ResumeLayout(false);
            this.tbpTextBox.ResumeLayout(false);
            this.tbpProgressBar.ResumeLayout(false);
            this.tbpSliderBar.ResumeLayout(false);
            this.tbpParticleView.ResumeLayout(false);
            this.tbpSpineView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcProperty;
        private System.Windows.Forms.TabPage tbpPicture;
        private System.Windows.Forms.TabPage tbpContainer;
        private FileInputBox fibPictureImage;
        private ItemInputBox iibPictureMode;
        private ItemInputBox iibPictureAlign;
        private ItemInputBox iibPictureTrans;
        private NumberInputBox nibPictureScaleX;
        private ColorInputBox cibPictureChannel;
        private ColorInputBox cibContainerBackChannel;
        private ItemInputBox iibContainerBackTrans;
        private FileInputBox fibContainerBack;
        private ColorInputBox cibContainerFramChannel;
        private FileInputBox fibContainerFrame;
        private System.Windows.Forms.TabPage tbpTextControl;
        private System.Windows.Forms.TabPage tbpLabel;
        private TextInputBox tibTextControlText;
        private NumberInputBox nibTextControlWordSize;
        private ColorInputBox cibTextControlTextColor;
        private ItemInputBox iibLabelAlign;
        private ItemInputBox iibLabelType;
        private ColorInputBox cibLabelStrokeColor;
        private System.Windows.Forms.TabPage tbpButton;
        private System.Windows.Forms.TabPage tbpSingleButton;
        private FileInputBox fibButtonNormalImage;
        private FileInputBox fibButtonDisableImage;
        private NumberInputBox nibSingleButtonWordSize;
        private TextInputBox tibSingleButtonText;
        private PointInputBox pibSingleButtonTextDownOffset;
        private PointInputBox pibSingleButtonTextOffset;
        private ColorInputBox cibSingleButtonTextDisableColor;
        private ColorInputBox cibSingleButtonTextDownColor;
        private ColorInputBox cibSingleButtonTextNormalColor;
        private ColorInputBox cibSingleButtonTextDisableStrokeColor;
        private ColorInputBox cibSingleButtonTextDownStrokeColor;
        private ColorInputBox cibSingleButtonTextNormalStrokeColor;
        private BooleanInputBox bibTextControlClearValue;
        private BooleanInputBox bibPictureClearValue;
        private System.Windows.Forms.TabPage tbpTable;
        private NumberInputBox nibTableChildNumber;
        private NumberInputBox nibTableScrollBarWidth;
        private FileInputBox fibTableScrollBack;
        private FileInputBox fibTableScrollBar;
        private System.Windows.Forms.TabPage tbpScrollTable;
        private NumberInputBox nibScrollTableBasicNumber;
        private ItemInputBox iibScrollTableDirection;
        private System.Windows.Forms.TabPage tbpControl;
        private BooleanInputBox bibControlVisible;
        private BooleanInputBox bibControlEnable;
        private ColorInputBox cibControlBackColor;
        private SizeInputBox sibControlSize;
        private PointInputBox pibControlPosition;
        private System.Windows.Forms.TabPage tbpRadioButton;
        private NumberInputBox nibRadionButtonGroup;
        private BooleanInputBox bibRadionButtonCheck;
        private System.Windows.Forms.TabPage tbpCheckButton;
        private BooleanInputBox bibCheckButtonCheck;
        private System.Windows.Forms.TabPage tbpPageTable;
        private NumberInputBox nibPageTableRow;
        private NumberInputBox nibPageTableCol;
        private System.Windows.Forms.TabPage tbpScrollPanel;
        private PointInputBox pibScrollPanelMove;
        private System.Windows.Forms.TabPage tbpNumberImage;
        private FileInputBox fibNumberImageFile;
        private NumberInputBox nibNumberImageValue;
        private NumberInputBox nibNumberImageZoom;
        private ItemInputBox iibNumberImageAlign;
        private ItemInputBox iibNumberImageDirection;
        private System.Windows.Forms.TabPage tbpTextArea;
        private FileInputBox fibTextAreaScrollBack;
        private FileInputBox fibTextAreaScrollBar;
        private NumberInputBox nibTextAreaScrollBarWidth;
        private System.Windows.Forms.TabPage tbpImageNumber;
        private NumberInputBox nibImageNumberValue;
        private NumberInputBox nibImageNumberZoom;
        private ItemInputBox iibImageNumberAlign;
        private FileInputBox fibImageNumberFile;
        private System.Windows.Forms.TabPage tbpTextBox;
        private ItemInputBox iibTextBoxAlign;
        private System.Windows.Forms.TabPage tbpProgressBar;
        private FileInputBox fibProgressBarSlotImage;
        private ColorInputBox cibProgressBarFillColor;
        private ItemInputBox iibProgressBarDirection;
        private FileInputBox fibProgressBarFillImage;
        private NumberInputBox nibProgressBarStartValue;
        private NumberInputBox nibProgressBarEndValue;
        private NumberInputBox nibProgressBarCurrentValue;
        private System.Windows.Forms.TabPage tbpSliderBar;
        private NumberInputBox nibSliderBarEndValue;
        private NumberInputBox nibSliderBarCurrentValue;
        private NumberInputBox nibSliderBarStartValue;
        private NumberInputBox nibSliderBarZoom;
        private FileInputBox fibSliderBarSlotImage;
        private ItemInputBox iibSliderBarDirection;
        private FileInputBox fibSliderBarImage;
        private System.Windows.Forms.TabPage tbpParticleView;
        private FileInputBox fibParticleViewFile;
        private BooleanInputBox bibParticleViewParentClip;
        private System.Windows.Forms.TabPage tbpSpineView;
        private FileInputBox fibSpineViewFile;
        private BooleanInputBox bibSpineViewLoop;
        private ItemInputBox iibTextBoxInputType;
        private FileInputBox fibButtonDownImage;
        private TextInputBox fibSpineViewAnimation;
        private NumberInputBox nibPictureScaleY;
        private NumberInputBox nibNumberImageGap;
        private NumberInputBox nibNumberImageUpperLimitValue;
        private FileInputBox fibNumberImageUpperLimitFile;
        private BooleanInputBox bibContainerClipping;
        private NumberInputBox nibParticleViewScaleY;
        private NumberInputBox nibParticleViewScaleX;
        private NumberInputBox nibSpineViewScaleY;
        private NumberInputBox nibSpineViewScaleX;
    }
}