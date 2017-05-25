using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T006.Data;
using T006.Data.Particle;

namespace T006.Forms
{
    /// <summary>
    /// 粒子效果属性设置。
    /// </summary>
    public partial class EffectPropertyForm : WorkAssistantForm
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public EffectPropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        public void UpdateProperty()
        {
            if (m_iEffectIndex == -1)
            {
                this.pibParticlePosition.InputValue = m_pfEditFile.EditParticle.Position;
            }
            else
            {
                ParticleEffect eft = m_pfEditFile.EditParticle.Effects[m_iEffectIndex];

                //基本
                this.tibBasicName.InputValue = eft.Name;
                this.fibBasicImage.InputValue = eft.ImageName;
                this.pibBasicPosition.InputValue = eft.Position;
                this.pibBasicPositionVar.InputValue = eft.PositionVar;
                this.nibBasicXScale.InputValue = eft.ScaleX;
                this.nibBasicYScale.InputValue = eft.ScaleY;
                this.nibBasicAngle.InputValue = eft.Angle;
                this.nibBasicAngleVar.InputValue = eft.AngleVar;
                this.nibBasicLife.InputValue = eft.Life;
                this.nibBasicLifeVar.InputValue = eft.LifeVar;

                //运行
                this.nibRunDelay.InputValue = eft.Delay;
                this.nibRunDuration.InputValue = eft.Duration;
                this.iibRunPositionType.InputIndex = (Int32)eft.PosType;
                this.nibRunRate.InputValue = eft.EmissionRate;
                this.nibRunMax.InputValue = eft.TotalParticles;
                this.iibRunMode.InputIndex = (Int32)eft.EmitterMode;

                //颜色
                this.cibColorStart.InputValue = eft.StartColor;
                this.cibColorStartVar.InputValue = eft.StartColorVar;
                this.cibColorEnd.InputValue = eft.EndColor;
                this.cibColorEndVar.InputValue = eft.EndColorVar;

                //尺寸旋转
                this.nibSizeStart.InputValue = eft.StartSize;
                this.nibSizeStartVar.InputValue = eft.StartSizeVar;
                this.nibSizeEnd.InputValue = eft.EndSize;
                this.nibSizeEndVar.InputValue = eft.EndSizeVar;
                this.nibSpinStart.InputValue = eft.StartSpin;
                this.nibSpinStartVar.InputValue = eft.StartSpinVar;
                this.nibSpinEnd.InputValue = eft.EndSpin;
                this.nibSpinEndVar.InputValue = eft.EndSpinVar;

                //重力
                this.pibGravityAccel.InputValue = new Point((int)eft.GravityX, (int)eft.GravityY);
                this.nibGravitySpeed.InputValue = eft.Speed;
                this.nibGravitySpeedVar.InputValue = eft.SpeedVar;
                this.nibGravityRadialAccel.InputValue = eft.RadialAccel;
                this.nibGravityRadialAccelVar.InputValue = eft.RadialAccelVar;
                this.nibGravityTangentialAccel.InputValue = eft.TangentialAccel;
                this.nibGravityTangentialAccelVar.InputValue = eft.TangentialAccelVar;

                //半径
                this.nibRadiusStart.InputValue = eft.StartRadius;
                this.nibRadiusStartVar.InputValue = eft.StartRadiusVar;
                this.nibRadiusEnd.InputValue = eft.EndRadius;
                this.nibRadiusEndVar.InputValue = eft.EndRadiusVar;
                this.nibRadiusRotate.InputValue = eft.RotatePerSecond;
                this.nibRadiusRotateVar.InputValue = eft.RotatePerSecondVar;
            }            
        }

        /// <summary>
        /// 更新位置信息
        /// </summary>
        public void UpdatePosition()
        {
            this.pibParticlePosition.InputValue = m_pfEditFile.EditParticle.Position;
            if (m_iEffectIndex != -1)
            {
                this.pibBasicPosition.InputValue = m_pfEditFile.EditParticle.Effects[m_iEffectIndex].Position;
            }
        }

        /// <summary>
        /// 设置资源目录。
        /// </summary>
        public String AssetsFolder
        {
            set
            {
                this.fibBasicImage.FolderLimit = value;
            }
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置编辑的粒子文件。
        /// </summary>
        public ParticleFile EditFile
        {
            get
            {
                return m_pfEditFile;
            }
            set
            {
                m_pfEditFile = value;
                tbcProperty.Visible = m_pfEditFile != null;
                m_iEffectIndex = -1;
                InitPropertyPage();
                UpdateProperty();
            }
        }

        /// <summary>
        /// 获取或设置效果索引。
        /// </summary>
        public Int32 EffectIndex
        {
            get
            {
                return m_iEffectIndex;
            }
            set
            {
                Int32 oldindex = m_iEffectIndex;
                m_iEffectIndex = value;
                if ((oldindex != m_iEffectIndex) && (oldindex == -1 || m_iEffectIndex == -1))
                {
                    InitPropertyPage();
                }
                UpdateProperty();
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

            //判断是对系统整体还是对选中的效果
            if (m_iEffectIndex == -1)
            {
                this.tbcProperty.TabPages.Add(this.tbpParticleSystem);
            }
            else
            {
                this.tbcProperty.TabPages.Add(this.tbpBasic);
                this.tbcProperty.TabPages.Add(this.tbpRun);
                this.tbcProperty.TabPages.Add(this.tbpColor);
                this.tbcProperty.TabPages.Add(this.tbpSizeSpin);
                this.tbcProperty.TabPages.Add(this.tbpGravity);
                this.tbcProperty.TabPages.Add(this.tbpRadius);
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 编辑的文件。
        /// </summary>
        private ParticleFile m_pfEditFile = null;

        /// <summary>
        /// 效果索引。
        /// </summary>
        private Int32 m_iEffectIndex = -1;

        #endregion
        
        #region 事件函数=====================================================================================

        /// <summary>
        /// 设置粒子系统位置。
        /// </summary>
        private void pibParticlePosition_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetSystemPosition(this.pibParticlePosition.InputValue);
        }

        /// <summary>
        /// 设置 基本-名称。
        /// </summary>
        private void tibBasicName_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectName(m_iEffectIndex, this.tibBasicName.InputValue);
            (MainForm.AppMainForm.EditFileForm as ParticleFileForm).UpdateEffectName();
        }

        /// <summary>
        /// 设置 基本-图像。
        /// </summary>
        private void fibBasicImage_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectImage(m_iEffectIndex, this.fibBasicImage.InputValue);
        }

        /// <summary>
        /// 设置 基本-位置。
        /// </summary>
        private void pibBasicPosition_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectPosition(m_iEffectIndex, this.pibBasicPosition.InputValue);
        }

        /// <summary>
        /// 设置 基本-位置抖动。
        /// </summary>
        private void pibBasicPositionVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectPositionVar(m_iEffectIndex, this.pibBasicPositionVar.InputValue);
        }

        /// <summary>
        /// 设置 基本-水平缩放。
        /// </summary>
        private void nibBasicXScale_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectScaleX(m_iEffectIndex, this.nibBasicXScale.InputValue);
        }

        /// <summary>
        /// 设置 基本-竖直缩放。
        /// </summary>
        private void nibBasicYScale_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectScaleY(m_iEffectIndex, this.nibBasicYScale.InputValue);
        }

        /// <summary>
        /// 设置 基本-角度。
        /// </summary>
        private void nibBasicAngle_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectAngle(m_iEffectIndex, this.nibBasicAngle.InputValue);
        }

        /// <summary>
        /// 设置 基本-角度波动。
        /// </summary>
        private void nibBasicAngleVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectAngleVar(m_iEffectIndex, this.nibBasicAngleVar.InputValue);
        }

        /// <summary>
        /// 设置 基本-生命。
        /// </summary>
        private void nibBasicLife_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectLife(m_iEffectIndex, this.nibBasicLife.InputValue);
        }

        /// <summary>
        /// 设置 基本-生命波动。
        /// </summary>
        private void nibBasicLifeVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectLifeVar(m_iEffectIndex, this.nibBasicLifeVar.InputValue);
        }

        /// <summary>
        /// 设置 运行-延时。
        /// </summary>
        private void nibRunDelay_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectDelay(m_iEffectIndex, this.nibRunDelay.InputValue);
        }

        /// <summary>
        /// 设置 运行-持续。
        /// </summary>
        private void nibRunDuration_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectDuration(m_iEffectIndex, this.nibRunDuration.InputValue);
        }

        /// <summary>
        /// 设置 运行-位置类型。
        /// </summary>
        private void iibRunPositionType_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectPositionType(m_iEffectIndex, (PositionType)this.iibRunPositionType.InputIndex);
        }

        /// <summary>
        /// 设置 运行-速率。
        /// </summary>
        private void nibRunRate_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEmissionRate(m_iEffectIndex, this.nibRunRate.InputValue);
        }

        /// <summary>
        /// 设置 运行-上限。
        /// </summary>
        private void nibRunMax_Inputed(object sender, EventArgs e)
        {
            int num = (Int32)this.nibRunMax.InputValue;
            m_pfEditFile.SetEffectTotalParticles(m_iEffectIndex, num);
            this.nibRunMax.InputValue = num;
        }

        /// <summary>
        /// 设置 运行-模式。
        /// </summary>
        private void iibRunMode_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEmitterMode(m_iEffectIndex, (ParticleMode)this.iibRunMode.InputIndex);
        }

        /// <summary>
        /// 设置 颜色-起始。
        /// </summary>
        private void cibColorStart_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartColor(m_iEffectIndex, this.cibColorStart.InputValue);
        }

        /// <summary>
        /// 设置 颜色-起始波动。
        /// </summary>
        private void cibColorStartVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartColorVar(m_iEffectIndex, this.cibColorStartVar.InputValue);
        }

        /// <summary>
        /// 设置 颜色-结束。
        /// </summary>
        private void cibColorEnd_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndColor(m_iEffectIndex, this.cibColorEnd.InputValue);
        }

        /// <summary>
        /// 设置 颜色-结束波动。
        /// </summary>
        private void cibColorEndVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndColorVar(m_iEffectIndex, this.cibColorEndVar.InputValue);
        }

        /// <summary>
        /// 设置 大小-起始。
        /// </summary>
        private void nibSizeStart_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartSize(m_iEffectIndex, this.nibSizeStart.InputValue);
        }

        /// <summary>
        /// 设置 大小-起始波动。
        /// </summary>
        private void nibSizeStartVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartSizeVar(m_iEffectIndex, this.nibSizeStartVar.InputValue);
        }

        /// <summary>
        /// 设置 大小-结束。
        /// </summary>
        private void nibSizeEnd_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndSize(m_iEffectIndex, this.nibSizeEnd.InputValue);
        }

        /// <summary>
        /// 设置 大小-结束波动。
        /// </summary>
        private void nibSizeEndVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndSizeVar(m_iEffectIndex, this.nibSizeEndVar.InputValue);
        }

        /// <summary>
        /// 设置 自旋-起始。
        /// </summary>
        private void nibSpinStart_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartSpin(m_iEffectIndex, this.nibSpinStart.InputValue);
        }

        /// <summary>
        /// 设置 自旋-起始波动。
        /// </summary>
        private void nibSpinStartVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartSpinVar(m_iEffectIndex, this.nibSpinStartVar.InputValue);
        }

        /// <summary>
        /// 设置 自旋-结束。
        /// </summary>
        private void nibSpinEnd_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndSpin(m_iEffectIndex, this.nibSpinEnd.InputValue);
        }

        /// <summary>
        /// 设置 自旋-结束波动。
        /// </summary>
        private void nibSpinEndVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndSpinVar(m_iEffectIndex, this.nibSpinEndVar.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-重力加速。
        /// </summary>
        private void pibGravityAccel_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectGravity(m_iEffectIndex, this.pibGravityAccel.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-速度。
        /// </summary>
        private void nibGravitySpeed_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectSpeed(m_iEffectIndex, this.nibGravitySpeed.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-速度波动。
        /// </summary>
        private void nibGravitySpeedVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectSpeedVar(m_iEffectIndex, this.nibGravitySpeedVar.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-径向加速。
        /// </summary>
        private void nibGravityRadialAccel_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectRadialAccel(m_iEffectIndex, this.nibGravityRadialAccel.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-径向加速波动。
        /// </summary>
        private void nibGravityRadialAccelVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectRadialAccelVar(m_iEffectIndex, this.nibGravityRadialAccelVar.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-切向加速。
        /// </summary>
        private void nibGravityTangentialAccel_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectTangentialAccel(m_iEffectIndex, this.nibGravityTangentialAccel.InputValue);
        }

        /// <summary>
        /// 设置 重力模式-切向加速波动。
        /// </summary>
        private void nibGravityTangentialAccelVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectTangentialAccelVar(m_iEffectIndex, this.nibGravityTangentialAccelVar.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-起始。
        /// </summary>
        private void nibRadiusStart_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartRadius(m_iEffectIndex, this.nibRadiusStart.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-起始波动。
        /// </summary>
        private void nibRadiusStartVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectStartRadiusVar(m_iEffectIndex, this.nibRadiusStartVar.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-结束。
        /// </summary>
        private void nibRadiusEnd_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndRadius(m_iEffectIndex, this.nibRadiusEnd.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-结束波动。
        /// </summary>
        private void nibRadiusEndVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectEndRadiusVar(m_iEffectIndex, this.nibRadiusEndVar.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-转速。
        /// </summary>
        private void nibRadiusRotate_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectRotate(m_iEffectIndex, this.nibRadiusRotate.InputValue);
        }

        /// <summary>
        /// 设置 半径模式-转速波动。
        /// </summary>
        private void nibRadiusRotateVar_Inputed(object sender, EventArgs e)
        {
            m_pfEditFile.SetEffectRotateVar(m_iEffectIndex, this.nibRadiusRotateVar.InputValue);
        }

        #endregion
    }
}
