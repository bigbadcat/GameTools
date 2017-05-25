using XuXiang.ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T002.Data;
using T002.Data.UI;

namespace T002.Forms
{
    /// <summary>
    /// 控件属性窗体
    /// </summary>
    public partial class ControlPropertyForm : WorkAssistantForm
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数
        /// </summary>
        public ControlPropertyForm()
        {
            InitializeComponent();
            this.EditFile = null;
        }

        /// <summary>
        /// 更新控件属性。
        /// </summary>
        public void UpdateProperty()
        {
            //控件基类
            if (this.m_ctrEditControl is T002.Data.UI.Control)
            {
                Boolean bRoot = this.m_ctrEditControl.Parent == null;
                Boolean bPro = this.m_ctrEditControl.Parent is Table;
                Boolean bPagePro = this.m_ctrEditControl.Parent is PageTable;
                Boolean bSPChild = this.m_ctrEditControl.Parent is ScrollPanel;
                this.pibControlPosition.Enabled = !bRoot && !bPro && !bSPChild;
                this.sibControlSize.Enabled = !bRoot && !bPagePro;

                T002.Data.UI.Control ctrEdit = this.m_ctrEditControl;
                this.pibControlPosition.InputValue = ctrEdit.Position;
                this.sibControlSize.InputValue = ctrEdit.Size;
                this.bibControlEnable.InputValue = ctrEdit.Enable;
                this.bibControlVisible.InputValue = ctrEdit.Visible;
                this.cibControlBackColor.InputValue = ctrEdit.BackColor;
            }

            //容器
            if (this.m_ctrEditControl is T002.Data.UI.Container)
            {
                T002.Data.UI.Container conEdit = this.m_ctrEditControl as T002.Data.UI.Container;
                this.fibContainerBack.InputValue = conEdit.BackImage == null ? String.Empty : conEdit.BackImage.Name;
                this.cibContainerBackChannel.InputValue = conEdit.BackChannel;
                this.iibContainerBackTrans.InputIndex = (Int32)conEdit.BackTrans;
                this.fibContainerFrame.InputValue = conEdit.FrameImage == null ? String.Empty : conEdit.FrameImage.Name;
                this.cibContainerFramChannel.InputValue = conEdit.FrameChannel;
            }

            //图像
            if (this.m_ctrEditControl is T002.Data.UI.Picture)
            {
                T002.Data.UI.Picture picEdit = this.m_ctrEditControl as T002.Data.UI.Picture;
                this.fibPictureImage.InputValue = picEdit.Image == null ? String.Empty : picEdit.Image.Name;
                this.cibPictureChannel.InputValue = picEdit.Channel;
                this.iibPictureMode.InputIndex = (Int32)picEdit.Mode;
                this.iibPictureAlign.InputIndex = (Int32)picEdit.Align;
                this.iibPictureTrans.InputIndex = (Int32)picEdit.Trans;
                this.nibPictureScaleX.InputValue = picEdit.ScaleX;
                this.nibPictureScaleY.InputValue = picEdit.ScaleY;
                this.bibPictureClearValue.InputValue = picEdit.ClearValue;
            }

            //文本控件
            if (this.m_ctrEditControl is T002.Data.UI.TextControl)
            {
                T002.Data.UI.TextControl tcEdit = this.m_ctrEditControl as T002.Data.UI.TextControl;
                this.tibTextControlText.InputValue = tcEdit.Text;
                this.nibTextControlWordSize.InputValue = tcEdit.WordSize;
                this.cibTextControlTextColor.InputValue = tcEdit.TextColor;
                this.bibTextControlClearValue.InputValue = tcEdit.ClearValue;
            }

            //标签
            if (this.m_ctrEditControl is T002.Data.UI.Label)
            {
                T002.Data.UI.Label lbEdit = this.m_ctrEditControl as T002.Data.UI.Label;
                this.iibLabelAlign.InputIndex = (Int32)lbEdit.Align;
                this.iibLabelType.InputIndex = (Int32)lbEdit.LabelType;
                this.cibLabelStrokeColor.InputValue = lbEdit.StrokeColor;
            }

            //按钮
            if (this.m_ctrEditControl is T002.Data.UI.Button)
            {
                T002.Data.UI.Button btnEdit = this.m_ctrEditControl as T002.Data.UI.Button;
                this.fibButtonNormalImage.InputValue = btnEdit.NormalImage == null ? String.Empty : btnEdit.NormalImage.Name;
                this.fibButtonDownImage.InputValue = btnEdit.DownImage == null ? String.Empty : btnEdit.DownImage.Name;
                this.fibButtonDisableImage.InputValue = btnEdit.DisableImage == null ? String.Empty : btnEdit.DisableImage.Name;
            }

            //普通按钮
            if (this.m_ctrEditControl is T002.Data.UI.SingleButton)
            {
                T002.Data.UI.SingleButton sbEdit = this.m_ctrEditControl as T002.Data.UI.SingleButton;
                this.tibSingleButtonText.InputValue = sbEdit.Text;
                this.nibSingleButtonWordSize.InputValue = sbEdit.WordSize;
                this.pibSingleButtonTextOffset.InputValue = sbEdit.TextOffset;
                this.pibSingleButtonTextDownOffset.InputValue = sbEdit.TextDownOffset;
                this.cibSingleButtonTextNormalColor.InputValue = sbEdit.TextNormalColor;
                this.cibSingleButtonTextDownColor.InputValue = sbEdit.TextDownColor;
                this.cibSingleButtonTextDisableColor.InputValue = sbEdit.TextDisableColor;
                this.cibSingleButtonTextNormalStrokeColor.InputValue = sbEdit.TextNormalStrokeColor;
                this.cibSingleButtonTextDownStrokeColor.InputValue = sbEdit.TextDownStrokeColor;
                this.cibSingleButtonTextDisableStrokeColor.InputValue = sbEdit.TextDisableStrokeColor;
            }

            //表格
            if (this.m_ctrEditControl is T002.Data.UI.Table)
            {
                Table tb = this.m_ctrEditControl as T002.Data.UI.Table;
                this.nibTableChildNumber.InputValue = tb.ChildNumber;
                this.nibTableScrollBarWidth.InputValue = tb.ScrollBarWidth;
                this.fibTableScrollBar.InputValue = tb.ScrollBar == null ? String.Empty : tb.ScrollBar.Name;
                this.fibTableScrollBack.InputValue = tb.ScrollBack == null ? String.Empty : tb.ScrollBack.Name;
            }

            //滑动表格
            if (this.m_ctrEditControl is T002.Data.UI.ScrollTable)
            {
                ScrollTable st = this.m_ctrEditControl as ScrollTable;
                this.iibScrollTableDirection.InputIndex = (Int32)st.Direction;
                this.nibScrollTableBasicNumber.InputValue = st.BasicNumber;
            }

            //单选按钮
            if (this.m_ctrEditControl is T002.Data.UI.RadioButton)
            {
                T002.Data.UI.RadioButton rdb = this.m_ctrEditControl as T002.Data.UI.RadioButton;
                this.nibRadionButtonGroup.InputValue = rdb.GroupCode;
                this.bibRadionButtonCheck.InputValue = rdb.Checked;
            }

            //复选按钮
            if (this.m_ctrEditControl is T002.Data.UI.CheckButton)
            {
                T002.Data.UI.CheckButton ckb = this.m_ctrEditControl as T002.Data.UI.CheckButton;
                this.bibCheckButtonCheck.InputValue = ckb.Checked;
            }

            //翻页表格
            if (this.m_ctrEditControl is T002.Data.UI.PageTable)
            {
                T002.Data.UI.PageTable pt = this.m_ctrEditControl as T002.Data.UI.PageTable;
                this.nibPageTableRow.InputValue = pt.Row;
                this.nibPageTableCol.InputValue = pt.Col;
            }

            //滚动面板
            if (this.m_ctrEditControl is T002.Data.UI.ScrollPanel)
            {
                T002.Data.UI.ScrollPanel sp = this.m_ctrEditControl as T002.Data.UI.ScrollPanel;
                this.pibScrollPanelMove.InputValue = sp.Move;
            }

            //数字图像
            if (this.m_ctrEditControl is T002.Data.UI.NumberImage)
            {
                T002.Data.UI.NumberImage ni = this.m_ctrEditControl as T002.Data.UI.NumberImage;
                this.fibNumberImageFile.InputValue = ni.Image == null ? String.Empty : ni.Image.Name;
                this.fibNumberImageUpperLimitFile.InputValue = ni.UpperLimitImage == null ? String.Empty : ni.UpperLimitImage.Name;
                this.nibNumberImageValue.InputValue = ni.Number;
                this.nibNumberImageUpperLimitValue.InputValue = ni.UpperLimit;
                this.nibNumberImageZoom.InputValue = ni.Zoom;
                this.nibNumberImageGap.InputValue = ni.Gap;
                this.iibNumberImageAlign.InputIndex = (Int32)ni.Align;
                this.iibNumberImageDirection.InputIndex = (Int32)ni.Direction;
            }

            //文本域
            if (this.m_ctrEditControl is T002.Data.UI.TextArea)
            {
                TextArea ta = this.m_ctrEditControl as T002.Data.UI.TextArea;
                this.nibTextAreaScrollBarWidth.InputValue = ta.ScrollBarWidth;
                this.fibTextAreaScrollBar.InputValue = ta.ScrollBar == null ? String.Empty : ta.ScrollBar.Name;
                this.fibTextAreaScrollBack.InputValue = ta.ScrollBack == null ? String.Empty : ta.ScrollBack.Name;
            }

            //图像数字
            if (this.m_ctrEditControl is T002.Data.UI.ImageNumber)
            {
                T002.Data.UI.ImageNumber inb = this.m_ctrEditControl as T002.Data.UI.ImageNumber;
                this.fibImageNumberFile.InputValue = inb.Image == null ? String.Empty : inb.Image.Name;
                this.nibImageNumberValue.InputValue = inb.Number;
                this.nibImageNumberZoom.InputValue = inb.Zoom;
                this.iibImageNumberAlign.InputIndex = (Int32)inb.Align;
            }

            //文本域
            if (this.m_ctrEditControl is T002.Data.UI.TextBox)
            {
                T002.Data.UI.TextBox tb = this.m_ctrEditControl as T002.Data.UI.TextBox;
                this.iibTextBoxAlign.InputIndex = (Int32)tb.Align;
                this.iibTextBoxInputType.InputIndex = (Int32)tb.Type;
            }

            //进度条
            if (this.m_ctrEditControl is T002.Data.UI.ProgressBar)
            {
                T002.Data.UI.ProgressBar pb = this.m_ctrEditControl as T002.Data.UI.ProgressBar;
                this.fibProgressBarFillImage.InputValue = pb.FillImage == null ? String.Empty : pb.FillImage.Name;
                this.fibProgressBarSlotImage.InputValue = pb.SlotImage == null ? String.Empty : pb.SlotImage.Name;
                this.cibProgressBarFillColor.InputValue = pb.FillColor;
                this.iibProgressBarDirection.InputIndex = (Int32)pb.Direction;
                this.nibProgressBarStartValue.InputValue = pb.StartValue;
                this.nibProgressBarCurrentValue.InputValue = pb.CurrentValue;
                this.nibProgressBarEndValue.InputValue = pb.EndValue;
            }

            //滑动条
            if (this.m_ctrEditControl is T002.Data.UI.SliderBar)
            {
                T002.Data.UI.SliderBar slb = this.m_ctrEditControl as T002.Data.UI.SliderBar;
                this.fibSliderBarImage.InputValue = slb.BarImage == null ? String.Empty : slb.BarImage.Name;
                this.fibSliderBarSlotImage.InputValue = slb.SlotImage == null ? String.Empty : slb.SlotImage.Name;
                this.nibSliderBarZoom.InputValue = slb.BarZoom;
                this.nibSliderBarStartValue.InputValue = slb.StartValue;
                this.nibSliderBarCurrentValue.InputValue = slb.CurrentValue;
                this.nibSliderBarEndValue.InputValue = slb.EndValue;
                this.iibSliderBarDirection.InputIndex = (Int32)slb.Direction;
            }

            //粒子视图
            if (this.m_ctrEditControl is T002.Data.UI.ParticleView)
            {
                T002.Data.UI.ParticleView pv = this.m_ctrEditControl as T002.Data.UI.ParticleView;
                this.fibParticleViewFile.InputValue = pv.ParticleFile;
                this.bibParticleViewParentClip.InputValue = pv.ParentClip;
                this.nibParticleViewScaleX.InputValue = pv.ScaleX;
                this.nibParticleViewScaleY.InputValue = pv.ScaleY;
            }

            //动画视图
            if (this.m_ctrEditControl is T002.Data.UI.SpineView)
            {
                T002.Data.UI.SpineView sv = this.m_ctrEditControl as T002.Data.UI.SpineView;
                this.fibSpineViewFile.InputValue = sv.SpineFile;
                this.fibSpineViewAnimation.InputValue = sv.AnimationName;
                this.bibSpineViewLoop.InputValue = sv.Loop;
                this.nibSpineViewScaleX.InputValue = sv.ScaleX;
                this.nibSpineViewScaleY.InputValue = sv.ScaleY;
            }
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置属性窗口对应的界面文件。
        /// </summary>
        public InterfaceFile EditFile
        {
            get
            {
                return this.m_ifEditFile;
            }
            set
            {
                this.m_ifEditFile = value;
                this.tbcProperty.Visible = value != null;
                this.EditControl = null;
            }
        }

        /// <summary>
        /// 获取或设置当前编辑的控件。
        /// </summary>
        public T002.Data.UI.Control EditControl
        {
            get
            {
                return this.m_ctrEditControl;
            }
            set
            {
                T002.Data.UI.Control ctrOld = this.m_ctrEditControl;
                this.m_ctrEditControl = value;
                this.SetVisible(value != null);
                if ((ctrOld != value) && (ctrOld == null || value == null || ctrOld.GetType() != value.GetType()))
                {
                    InitPropertyPage();
                }
                UpdateProperty();
            }
        }

        /// <summary>
        /// 设置资源目录。
        /// </summary>
        public String AssetsFolder
        {
            set
            {
                this.fibPictureImage.FolderLimit = value;
                this.fibContainerBack.FolderLimit = value;
                this.fibContainerFrame.FolderLimit = value;
                this.fibButtonNormalImage.FolderLimit = value;
                this.fibButtonDownImage.FolderLimit = value;
                this.fibButtonDisableImage.FolderLimit = value;
                this.fibTableScrollBar.FolderLimit = value;
                this.fibTableScrollBack.FolderLimit = value;
                this.fibNumberImageFile.FolderLimit = value;
                this.fibNumberImageUpperLimitFile.FolderLimit = value;
                this.fibTextAreaScrollBar.FolderLimit = value;
                this.fibTextAreaScrollBack.FolderLimit = value;
                this.fibImageNumberFile.FolderLimit = value;
                this.fibProgressBarFillImage.FolderLimit = value;
                this.fibProgressBarSlotImage.FolderLimit = value;
                this.fibSliderBarImage.FolderLimit = value;
                this.fibSliderBarSlotImage.FolderLimit = value;
                this.fibParticleViewFile.FolderLimit = value;
                this.fibSpineViewFile.FolderLimit = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 初始化控件属性。
        /// </summary>
        protected void InitPropertyPage()
        {
            this.tbcProperty.TabPages.Clear();
            //控件基类
            if (this.m_ctrEditControl is T002.Data.UI.Control)
            {
                this.tbcProperty.TabPages.Add(this.tbpControl);
            }

            //容器
            if (this.m_ctrEditControl is T002.Data.UI.Container)
            {
                this.tbcProperty.TabPages.Add(this.tbpContainer);
            }

            //图像
            if (this.m_ctrEditControl is T002.Data.UI.Picture)
            {
                this.tbcProperty.TabPages.Add(this.tbpPicture);
            }

            //文本控件
            if (this.m_ctrEditControl is T002.Data.UI.TextControl)
            {
                this.tbcProperty.TabPages.Add(this.tbpTextControl);
            }

            //标签
            if (this.m_ctrEditControl is T002.Data.UI.Label)
            {
                this.tbcProperty.TabPages.Add(this.tbpLabel);
            }

            //按钮
            if (this.m_ctrEditControl is T002.Data.UI.Button)
            {
                this.tbcProperty.TabPages.Add(this.tbpButton);
            }

            //普通按钮
            if (this.m_ctrEditControl is T002.Data.UI.SingleButton)
            {
                this.tbcProperty.TabPages.Add(this.tbpSingleButton);
            }

            //表格
            if (this.m_ctrEditControl is T002.Data.UI.Table)
            {
                this.tbcProperty.TabPages.Add(this.tbpTable);
            }

            //滑动表格
            if (this.m_ctrEditControl is ScrollTable)
            {
                this.tbcProperty.TabPages.Add(this.tbpScrollTable);
            }

            //单选按钮
            if (this.m_ctrEditControl is T002.Data.UI.RadioButton)
            {
                this.tbcProperty.TabPages.Add(this.tbpRadioButton);
            }

            //复选按钮
            if (this.m_ctrEditControl is T002.Data.UI.CheckButton)
            {
                this.tbcProperty.TabPages.Add(this.tbpCheckButton);
            }

            //翻页表格
            if (this.m_ctrEditControl is T002.Data.UI.PageTable)
            {
                this.tbcProperty.TabPages.Add(this.tbpPageTable);
            }

            //滚动面板
            if (this.m_ctrEditControl is T002.Data.UI.ScrollPanel)
            {
                this.tbcProperty.TabPages.Add(this.tbpScrollPanel);
            }

            //数字图像
            if (this.m_ctrEditControl is T002.Data.UI.NumberImage)
            {
                this.tbcProperty.TabPages.Add(this.tbpNumberImage);
            }

            //文本域
            if (this.m_ctrEditControl is T002.Data.UI.TextArea)
            {
                this.tbcProperty.TabPages.Add(this.tbpTextArea);
            }

            //图像数字
            if (this.m_ctrEditControl is T002.Data.UI.ImageNumber)
            {
                this.tbcProperty.TabPages.Add(this.tbpImageNumber);
            }

            //文本域
            if (this.m_ctrEditControl is T002.Data.UI.TextBox)
            {
                this.tbcProperty.TabPages.Add(this.tbpTextBox);
            }

            //进度条
            if (this.m_ctrEditControl is T002.Data.UI.ProgressBar)
            {
                this.tbcProperty.TabPages.Add(this.tbpProgressBar);
            }

            //滑动条
            if (this.m_ctrEditControl is T002.Data.UI.SliderBar)
            {
                this.tbcProperty.TabPages.Add(this.tbpSliderBar);
            }

            //粒子视图
            if (this.m_ctrEditControl is T002.Data.UI.ParticleView)
            {
                this.tbcProperty.TabPages.Add(this.tbpParticleView);
            }

            //精灵视图
            if (this.m_ctrEditControl is T002.Data.UI.SpineView)
            {
                this.tbcProperty.TabPages.Add(this.tbpSpineView);
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 编辑的界面文件。
        /// </summary>
        private InterfaceFile m_ifEditFile = null;

        /// <summary>
        /// 编辑的控件。
        /// </summary>
        private T002.Data.UI.Control m_ctrEditControl = null;

        #endregion

        #region 事件函数=====================================================================================

        /// <summary>
        /// 设置控件位置。
        /// </summary>
        private void pibControlPosition_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetControlPosition(this.m_ctrEditControl, pibControlPosition.InputValue);
            InterfaceFileForm iff = MainForm.AppMainForm.EditFileForm as InterfaceFileForm;
            iff.UpdateFileShow();
            iff.ShowSelectControlMark();
        }

        /// <summary>
        /// 设置控件尺寸。
        /// </summary>
        private void sibControlSize_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetControlSize(this.m_ctrEditControl, sibControlSize.InputValue);
            InterfaceFileForm iff = MainForm.AppMainForm.EditFileForm as InterfaceFileForm;
            iff.UpdateFileShow();
            iff.ShowSelectControlMark();
        }

        /// <summary>
        /// 设置控件是否可用。
        /// </summary>
        private void bibControlEnable_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetControlEnable(this.m_ctrEditControl, bibControlEnable.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置控件是否可见。
        /// </summary>
        private void bibControlVisible_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetControlVisible(this.m_ctrEditControl, bibControlVisible.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置控件背景色。
        /// </summary>
        private void cibControlBackColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetControlBackColor(this.m_ctrEditControl, cibControlBackColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像显示的图片。
        /// </summary>
        private void fibPictureImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureImage(this.m_ctrEditControl as Picture, fibPictureImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像通道。
        /// </summary>
        private void cibPictureChannel_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureChannel(this.m_ctrEditControl as Picture, cibPictureChannel.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像显示模式。
        /// </summary>
        private void iibPictureMode_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureMode(this.m_ctrEditControl as Picture, (ImageMode)iibPictureMode.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像对齐方式。
        /// </summary>
        private void iibPictureAlign_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureAlign(this.m_ctrEditControl as Picture, (Align)iibPictureAlign.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像变换方式。
        /// </summary>
        private void iibPictureTrans_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureTrans(this.m_ctrEditControl as Picture, (Trans)iibPictureTrans.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像X缩放大小。
        /// </summary>
        private void nibPictureScaleX_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureScaleX(this.m_ctrEditControl as Picture, (Single)nibPictureScaleX.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像缩Y放大小。
        /// </summary>
        private void nibPictureScaleY_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureScaleY(this.m_ctrEditControl as Picture, (Single)nibPictureScaleY.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置是否在生成时清除数据。
        /// </summary>
        private void nibPictureClearValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPictureClearValue(this.m_ctrEditControl as Picture, bibPictureClearValue.InputValue);
        }

        /// <summary>
        /// 设置容器背景图。
        /// </summary>
        private void fibContainerBack_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerBack(this.m_ctrEditControl as T002.Data.UI.Container, fibContainerBack.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置容器背景图颜色通道。
        /// </summary>
        private void cibContainerBackChannel_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerBackChannel(this.m_ctrEditControl as T002.Data.UI.Container, cibContainerBackChannel.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置容器背景图变换方式。
        /// </summary>
        private void iibContainerBackTrans_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerBackTrans(this.m_ctrEditControl as T002.Data.UI.Container, (Trans)iibContainerBackTrans.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置容器边框图。
        /// </summary>
        private void fibContainerFrame_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerFrame(this.m_ctrEditControl as T002.Data.UI.Container, fibContainerFrame.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置容器边框图颜色通道。
        /// </summary>
        private void cibContainerFramChannel_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerFrameChannel(this.m_ctrEditControl as T002.Data.UI.Container, cibContainerFramChannel.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置容器是否裁剪子节点。
        /// </summary>
        private void bibContainerClipping_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetContainerClipping(this.m_ctrEditControl as T002.Data.UI.Container, bibContainerClipping.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本控件的文本。
        /// </summary>
        private void tibTextControlText_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextControlText(this.m_ctrEditControl as T002.Data.UI.TextControl, tibTextControlText.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本控件的字号。
        /// </summary>
        private void nibTextControlWordSize_Inputed(object sender, EventArgs e)
        {
            Single v = nibTextControlWordSize.InputValue;
            if (v <= 0)
            {
                MessageBox.Show("文本字号必须大于1。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.m_ifEditFile.SetTextControlWordSize(this.m_ctrEditControl as T002.Data.UI.TextControl, v);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本控件的文本颜色。
        /// </summary>
        private void cibTextControlTextColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextControlTextColor(this.m_ctrEditControl as T002.Data.UI.TextControl, cibTextControlTextColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本控件是否清除值。
        /// </summary>
        private void bibTextControlClearValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextControlClearValue(this.m_ctrEditControl as T002.Data.UI.TextControl, bibTextControlClearValue.InputValue);
        }

        /// <summary>
        /// 设置标签的对齐方式。
        /// </summary>
        private void iibLabelAlign_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetLabelAlign(this.m_ctrEditControl as T002.Data.UI.Label, (Align)iibLabelAlign.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置标签的类型。
        /// </summary>
        private void iibLabelType_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetLabelType(this.m_ctrEditControl as T002.Data.UI.Label, (LabelType)iibLabelType.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置标签的描边色。
        /// </summary>
        private void cibLabelStrokeColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetLabelStrokeColor(this.m_ctrEditControl as T002.Data.UI.Label, cibLabelStrokeColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置按钮普通状态图像。
        /// </summary>
        private void fibButtonNormalImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetButtonNormalImage(this.m_ctrEditControl as T002.Data.UI.Button, fibButtonNormalImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置按钮按下状态图像。
        /// </summary>
        private void fibButtonDownImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetButtonDownImage(this.m_ctrEditControl as T002.Data.UI.Button, fibButtonDownImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置按钮禁用状态图像。
        /// </summary>
        private void fibButtonDisableImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetButtonDisableImage(this.m_ctrEditControl as T002.Data.UI.Button, fibButtonDisableImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本。
        /// </summary>
        private void tibSingleButtonText_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonText(this.m_ctrEditControl as T002.Data.UI.SingleButton, tibSingleButtonText.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本字号。
        /// </summary>
        private void nibSingleButtonWordSize_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonWordSize(this.m_ctrEditControl as T002.Data.UI.SingleButton, nibSingleButtonWordSize.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本偏移。
        /// </summary>
        private void pibSingleButtonTextOffset_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextOffset(this.m_ctrEditControl as T002.Data.UI.SingleButton, pibSingleButtonTextOffset.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本按下偏移。
        /// </summary>
        private void pibSingleButtonTextDownOffset_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextDownOffset(this.m_ctrEditControl as T002.Data.UI.SingleButton, pibSingleButtonTextDownOffset.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本普通颜色。
        /// </summary>
        private void cibSingleButtonTextNormalColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextNormalColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextNormalColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本按下颜色。
        /// </summary>
        private void cibSingleButtonTextDownColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextDownColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextDownColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本不可用颜色。
        /// </summary>
        private void cibSingleButtonTextDisableColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextDisableColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextDisableColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本普通描边颜色。
        /// </summary>
        private void cibSingleButtonTextNormalStrokeColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextNormalStrokeColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextNormalStrokeColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本按下描边颜色。
        /// </summary>
        private void cibSingleButtonTextDownStrokeColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextDownStrokeColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextDownStrokeColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置普通按钮的文本不可用描边颜色。
        /// </summary>
        private void cibSingleButtonTextDisableStrokeColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSingleButtonTextDisableStrokeColor(this.m_ctrEditControl as T002.Data.UI.SingleButton, cibSingleButtonTextDisableStrokeColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置表格单元数量。
        /// </summary>
        private void nibTableChildNumber_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTableChildNumber(this.m_ctrEditControl as T002.Data.UI.Table, (Int32)(nibTableChildNumber.InputValue));
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibTableChildNumber.InputValue = (this.m_ctrEditControl as Table).ChildNumber;
        }

        /// <summary>
        /// 设置表格滑动条宽度。
        /// </summary>
        private void nibTableScrollBarWidth_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTableScrollBarWidth(this.m_ctrEditControl as T002.Data.UI.Table, (Int32)(nibTableScrollBarWidth.InputValue));
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibTableScrollBarWidth.InputValue = (this.m_ctrEditControl as Table).ScrollBarWidth;
        }

        /// <summary>
        /// 设置表格滑动条滑块图像。
        /// </summary>
        private void fibTableScrollBar_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTableScrollBar(this.m_ctrEditControl as Table, this.fibTableScrollBar.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置表格滑动条背景图像。
        /// </summary>
        private void fibTableScrollBack_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTableScrollBack(this.m_ctrEditControl as Table, this.fibTableScrollBack.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动表格滑动方向。
        /// </summary>
        private void iibScrollTableDirection_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetScrollTableDirection(this.m_ctrEditControl as ScrollTable, (Direction)this.iibScrollTableDirection.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动表格摆放基数。
        /// </summary>
        private void nibScrollTableBasicNumber_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetScrollTableBasicNumber(this.m_ctrEditControl as ScrollTable, (Int32)this.nibScrollTableBasicNumber.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibScrollTableBasicNumber.InputValue = (this.m_ctrEditControl as ScrollTable).BasicNumber;
        }

        /// <summary>
        /// 设置单选按钮组编号。
        /// </summary>
        private void nibRadionButtonGroup_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetRadionButtonGroup(this.m_ctrEditControl as T002.Data.UI.RadioButton, (Int32)this.nibRadionButtonGroup.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibRadionButtonGroup.InputValue = (this.m_ctrEditControl as T002.Data.UI.RadioButton).GroupCode;
        }

        /// <summary>
        /// 设置单选按钮是否选中。
        /// </summary>
        private void bibRadionButtonCheck_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetRadionButtonCheck(this.m_ctrEditControl as T002.Data.UI.RadioButton, this.bibRadionButtonCheck.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置复选按钮是否选中。
        /// </summary>
        private void bibCheckButtonCheck_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetCheckButtonCheck(this.m_ctrEditControl as T002.Data.UI.CheckButton, this.bibCheckButtonCheck.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置翻页表格的行数。
        /// </summary>
        private void nibPageTableRow_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPageTableRow(this.m_ctrEditControl as T002.Data.UI.PageTable, (Int32)nibPageTableRow.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibPageTableRow.InputValue = (this.m_ctrEditControl as T002.Data.UI.PageTable).Row;
        }

        /// <summary>
        /// 设置翻页表格的列数。
        /// </summary>
        private void nibPageTableCol_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetPageTableCol(this.m_ctrEditControl as T002.Data.UI.PageTable, (Int32)nibPageTableCol.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibPageTableCol.InputValue = (this.m_ctrEditControl as T002.Data.UI.PageTable).Col;
        }

        /// <summary>
        /// 设置滚动面板的移动位置。
        /// </summary>
        private void pibScrollPanelMove_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetScrollPanelMove(this.m_ctrEditControl as T002.Data.UI.ScrollPanel, pibScrollPanelMove.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            pibScrollPanelMove.InputValue = (this.m_ctrEditControl as T002.Data.UI.ScrollPanel).Move;
        }

        /// <summary>
        /// 设置数字图像的图像文件。
        /// </summary>
        private void fibNumberImageFile_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageFile(this.m_ctrEditControl as NumberImage, this.fibNumberImageFile.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置数字图像的填充文件。
        /// </summary>
        private void fibNumberImageUpLimitFile_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberUpperLimitImageFile(this.m_ctrEditControl as NumberImage, this.fibNumberImageUpperLimitFile.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }        

        /// <summary>
        /// 设置数字图像的数值。
        /// </summary>
        private void nibNumberImageValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageValue(this.m_ctrEditControl as T002.Data.UI.NumberImage, (Int32)this.nibNumberImageValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibNumberImageValue.InputValue = (this.m_ctrEditControl as T002.Data.UI.NumberImage).Number;
        }

        /// <summary>
        /// 设置数字图像的上限。
        /// </summary>
        private void nibNumberImageUpLimitValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageUpperLimitValue(this.m_ctrEditControl as T002.Data.UI.NumberImage, (Int32)this.nibNumberImageUpperLimitValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibNumberImageUpperLimitValue.InputValue = (this.m_ctrEditControl as T002.Data.UI.NumberImage).UpperLimit;
        }

        /// <summary>
        /// 设置数字图像的缩放。
        /// </summary>
        private void nibNumberImageZoom_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageZoom(this.m_ctrEditControl as T002.Data.UI.NumberImage, (Single)this.nibNumberImageZoom.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibNumberImageZoom.InputValue = (this.m_ctrEditControl as T002.Data.UI.NumberImage).Zoom;
        }

        /// <summary>
        /// 设置数字图像的间隔。
        /// </summary>
        private void nibNumberImageGap_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageGap(this.m_ctrEditControl as T002.Data.UI.NumberImage, (Single)this.nibNumberImageGap.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibNumberImageGap.InputValue = (this.m_ctrEditControl as T002.Data.UI.NumberImage).Gap;
        }

        /// <summary>
        /// 设置数字图像的对齐方式。
        /// </summary>
        private void iibNumberImageAlign_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageAlign(this.m_ctrEditControl as T002.Data.UI.NumberImage, (LineMode)iibNumberImageAlign.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置数字图像的排列方向。
        /// </summary>
        private void iibNumberImageDirection_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetNumberImageDirection(this.m_ctrEditControl as T002.Data.UI.NumberImage, (Direction)iibNumberImageDirection.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本域滑动条宽度。
        /// </summary>
        private void nibTextAreaScrollBarWidth_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextAreaScrollBarWidth(this.m_ctrEditControl as T002.Data.UI.TextArea, (Int32)(nibTextAreaScrollBarWidth.InputValue));
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibTextAreaScrollBarWidth.InputValue = (this.m_ctrEditControl as TextArea).ScrollBarWidth;
        }

        /// <summary>
        /// 设置文本域滑动条滑块图像。
        /// </summary>
        private void fibTextAreaScrollBar_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextAreaScrollBar(this.m_ctrEditControl as TextArea, this.fibTextAreaScrollBar.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本域滑动条背景。
        /// </summary>
        private void fibTextAreaScrollBack_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextAreaScrollBack(this.m_ctrEditControl as TextArea, this.fibTextAreaScrollBack.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像数字的图像文件。
        /// </summary>
        private void fibImageNumberFile_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetImageNumberFile(this.m_ctrEditControl as ImageNumber, this.fibImageNumberFile.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置图像数字的数值。
        /// </summary>
        private void nibImageNumberValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetImageNumberValue(this.m_ctrEditControl as T002.Data.UI.ImageNumber, (Int32)this.nibImageNumberValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibImageNumberValue.InputValue = (this.m_ctrEditControl as T002.Data.UI.ImageNumber).Number;
        }

        /// <summary>
        /// 设置图像数字的缩放。
        /// </summary>
        private void nibImageNumberZoom_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetImageNumberZoom(this.m_ctrEditControl as T002.Data.UI.ImageNumber, (Single)this.nibImageNumberZoom.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            nibImageNumberZoom.InputValue = (this.m_ctrEditControl as T002.Data.UI.ImageNumber).Zoom;
        }

        /// <summary>
        /// 设置图像数字的对齐方式。
        /// </summary>
        private void iibImageNumberAlign_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetImageNumberAlign(this.m_ctrEditControl as T002.Data.UI.ImageNumber, (LineMode)iibImageNumberAlign.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本框的对齐方式。
        /// </summary>
        private void iibTextBoxAlign_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextBoxAlign(this.m_ctrEditControl as T002.Data.UI.TextBox, (Align)iibTextBoxAlign.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置文本输入类型。
        /// </summary>
        private void iibTextBoxInputType_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetTextBoxInputType(this.m_ctrEditControl as T002.Data.UI.TextBox, (InputType)this.iibTextBoxInputType.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置进度条填充图像。
        /// </summary>
        private void fibProgressBarFillImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarFillImage(this.m_ctrEditControl as T002.Data.UI.ProgressBar, this.fibProgressBarFillImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置进度条背景图像。
        /// </summary>
        private void fibProgressBarSlotImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarSlotImage(this.m_ctrEditControl as T002.Data.UI.ProgressBar, this.fibProgressBarSlotImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置进度条填充颜色。
        /// </summary>
        private void cibProgressBarFillColor_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarFillColor(this.m_ctrEditControl as T002.Data.UI.ProgressBar, this.cibProgressBarFillColor.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置进度条进度方向。
        /// </summary>
        private void iibProgressBarDirection_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarDirection(this.m_ctrEditControl as T002.Data.UI.ProgressBar, (ProgressDirection)this.iibProgressBarDirection.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置进度条起始值。
        /// </summary>
        private void nibProgressBarStartValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarStartValue(this.m_ctrEditControl as T002.Data.UI.ProgressBar, (Int32)this.nibProgressBarStartValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibProgressBarStartValue.InputValue = (m_ctrEditControl as T002.Data.UI.ProgressBar).StartValue;
            this.nibProgressBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.ProgressBar).CurrentValue;
        }

        /// <summary>
        /// 设置进度条当前值。
        /// </summary>
        private void nibProgressBarCurrentValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarCurrentValue(this.m_ctrEditControl as T002.Data.UI.ProgressBar, (Int32)this.nibProgressBarCurrentValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibProgressBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.ProgressBar).CurrentValue;
        }

        /// <summary>
        /// 设置进度条结束值。
        /// </summary>
        private void nibProgressBarEndValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetProgressBarEndValue(this.m_ctrEditControl as T002.Data.UI.ProgressBar, (Int32)this.nibProgressBarEndValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibProgressBarEndValue.InputValue = (m_ctrEditControl as T002.Data.UI.ProgressBar).EndValue;
            this.nibProgressBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.ProgressBar).CurrentValue;
        }

        /// <summary>
        /// 设置滑动条滑块图像。
        /// </summary>
        private void fibSliderBarImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarImage(this.m_ctrEditControl as T002.Data.UI.SliderBar, this.fibSliderBarImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动条背景图像。
        /// </summary>
        private void fibSliderBarSlotImage_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarSlotImage(this.m_ctrEditControl as T002.Data.UI.SliderBar, this.fibSliderBarSlotImage.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动条滑块缩放。
        /// </summary>
        private void nibSliderBarZoom_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarZoom(this.m_ctrEditControl as T002.Data.UI.SliderBar, this.nibSliderBarZoom.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动条方向。
        /// </summary>
        private void iibSliderBarDirection_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarDirection(this.m_ctrEditControl as T002.Data.UI.SliderBar, (Direction)this.iibSliderBarDirection.InputIndex);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
        }

        /// <summary>
        /// 设置滑动条起始值。
        /// </summary>
        private void nibSliderBarStartValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarStartValue(this.m_ctrEditControl as T002.Data.UI.SliderBar, (Int32)this.nibSliderBarStartValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibSliderBarStartValue.InputValue = (m_ctrEditControl as T002.Data.UI.SliderBar).StartValue;
            this.nibSliderBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.SliderBar).CurrentValue;
        }

        /// <summary>
        /// 设置滑动条当前值。
        /// </summary>
        private void nibSliderBarCurrentValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarCurrentValue(this.m_ctrEditControl as T002.Data.UI.SliderBar, (Int32)this.nibSliderBarCurrentValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibSliderBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.SliderBar).CurrentValue;
        }

        /// <summary>
        /// 设置滑动条结束值。
        /// </summary>
        private void nibSliderBarEndValue_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSliderBarEndValue(this.m_ctrEditControl as T002.Data.UI.SliderBar, (Int32)this.nibSliderBarEndValue.InputValue);
            (MainForm.AppMainForm.EditFileForm as InterfaceFileForm).UpdateFileShow();
            this.nibSliderBarEndValue.InputValue = (m_ctrEditControl as T002.Data.UI.SliderBar).EndValue;
            this.nibSliderBarCurrentValue.InputValue = (m_ctrEditControl as T002.Data.UI.SliderBar).CurrentValue;
        }

        /// <summary>
        /// 设置粒子视图粒子文件。
        /// </summary>
        private void fibParticleViewFile_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetParticleViewFile(this.m_ctrEditControl as T002.Data.UI.ParticleView, this.fibParticleViewFile.InputValue);
        }

        /// <summary>
        /// 设置粒子视图显示是否不受容器限制。
        /// </summary>
        private void bibParticleViewParentClip_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetParticleViewParentClip(this.m_ctrEditControl as T002.Data.UI.ParticleView, this.bibParticleViewParentClip.InputValue);
        }

        /// <summary>
        /// 设置粒子视图X缩放。
        /// </summary>
        private void nibParticleViewScaleX_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetParticleViewScaleX(this.m_ctrEditControl as T002.Data.UI.ParticleView, this.nibParticleViewScaleX.InputValue);
        }

        /// <summary>
        /// 设置粒子视图Y缩放。
        /// </summary>
        private void nibParticleViewScaleY_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetParticleViewScaleY(this.m_ctrEditControl as T002.Data.UI.ParticleView, this.nibParticleViewScaleY.InputValue);
        }

        /// <summary>
        /// 设置Spine动画视图精灵文件。
        /// </summary>
        private void fibSineViewFile_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSpineViewFile(this.m_ctrEditControl as T002.Data.UI.SpineView, this.fibSpineViewFile.InputValue);
        }

        /// <summary>
        /// 设置Spine动画名称。
        /// </summary>
        private void fibSpineViewAnimation_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSpineViewAnimationName(this.m_ctrEditControl as T002.Data.UI.SpineView, this.fibSpineViewAnimation.InputValue);
        }

        /// <summary>
        /// 设置Spine动画视图精灵是否循环。
        /// </summary>
        private void bibSpineViewLoop_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSpineViewLoop(this.m_ctrEditControl as T002.Data.UI.SpineView, this.bibSpineViewLoop.InputValue);
        }

        /// <summary>
        /// 设置Spine动画视图X缩放。
        /// </summary>
        private void nibSpineViewScaleX_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSpineViewScaleX(this.m_ctrEditControl as T002.Data.UI.SpineView, this.nibSpineViewScaleX.InputValue);
        }

        /// <summary>
        /// 设置Spine动画视图Y缩放。
        /// </summary>
        private void nibSpineViewScaleY_Inputed(object sender, EventArgs e)
        {
            this.m_ifEditFile.SetSpineViewScaleY(this.m_ctrEditControl as T002.Data.UI.SpineView, this.nibSpineViewScaleY.InputValue);
        }

        #endregion
    }
}
