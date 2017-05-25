using System;
using System.Drawing;
using System.Xml;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using CsGL.OpenGL;
using T006.Common;
using XuXiang.ClassLibrary;

namespace T006.Data.Particle
{
    /// <summary>
    /// 粒子效果，保存一粒子变换方式。
    /// </summary>
    public class ParticleEffect
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="par">所从属的粒子系统。</param>
        public ParticleEffect(ParticleSystem par)
        {
            m_psBelong = par;
            if (m_rdRand == null)
            {
                m_rdRand = new Random();
            }
            this.TotalParticles = 1;
        }

        /// <summary>
        /// 重置粒子效果，开始播放。
        /// </summary>
        public void ResetEffect()
        {
            this.m_bIsActive = true;
            this.m_fElapsed = 0;
            this.m_bEmit = false;
            this.m_fDelayCt = 0;
            for (Int32 i = 0; i < this.m_iParticleCount; ++i)
            {
                this.m_arParticles[i].m_timeToLive = 0;
            }
        }

        /// <summary>
        /// 停止粒子效果发射粒子。
        /// </summary>
        public void StopEffect()
        {
            this.m_bIsActive = false;
            this.m_fElapsed = this.m_fDuration;
            this.m_fEmitCounter = 0;
            this.m_bEmit = false;
            this.m_fDelayCt = 0;
        }

        /// <summary>
        /// 更新粒子效果。
        /// </summary>
        /// <param name="dt">粒子效果经过的时间。</param>
        public void Update(Single dt)
        {
            //发射粒子
            if (this.m_bIsActive && this.m_fEmissionRate > 0)
            {
                if (m_bEmit)
                {
                    Single rate = 1 / this.m_fEmissionRate;
                    if (this.m_iParticleCount < this.m_iTotalParticles)
                    {
                        this.m_fEmitCounter += dt;
                    }
                    while (this.m_iParticleCount < this.m_iTotalParticles && this.m_fEmitCounter > rate)
                    {
                        this.AddParticle();
                        this.m_fEmitCounter -= rate;
                    }
                    this.m_fElapsed += dt;
                    if (this.m_fDuration != -1 && this.m_fElapsed >= this.m_fDuration)
                    {
                        this.StopEffect();
                    }
                }
                else
                {
                    m_fDelayCt += dt;
                    if (m_fDelayCt >= this.m_fDelay)
                    {
                        m_bEmit = true;
                        this.m_fElapsed = m_fDelayCt - this.m_fDelay;
                        m_fDelayCt = 0;
                    }
                }
            }

            //更新所有粒子，并重新统计还存活的粒子数
            int index = 0;
            while (index < this.m_iParticleCount)
            {
                Particle par = this.m_arParticles[index];
                par.m_timeToLive -= dt;
                if (par.m_timeToLive > 0)
                {
                    if (this.m_peEmitterMode == ParticleMode.Gravity)
                    {
                        //radial acceleration
                        Single radialx = 0, radialy = 0, tangentialx = 0, tangentialy = 0;
                        Single px = par.m_posX - par.m_startPosX;
                        Single py = par.m_posY - par.m_startPosY;
                        if (px != 0 || py != 0)
                        {
                            Single len = (Single)Math.Sqrt(px * px + py * py);
                            radialx = px / len;
                            radialy = py / len;
                        }
                        tangentialx = radialx;
                        tangentialy = radialy;
                        radialx = radialx * par.m_radialAccel;
                        radialy = radialy * par.m_radialAccel;

                        //tangential acceleration
                        Single newy = tangentialx;
                        tangentialx = -tangentialy;
                        tangentialy = newy;
                        tangentialx = tangentialx * par.m_tangentialAccel;
                        tangentialy = tangentialy * par.m_tangentialAccel;

                        //(gravity + radial + tangential) * dt
                        Single tmpx = radialx + tangentialx + this.m_fGravityX;
                        Single tmpy = radialy + tangentialy + this.m_fGravityY;
                        par.m_dirX += tmpx * dt;		//速度变化
                        par.m_dirY += tmpy * dt;
                        par.m_posX += par.m_dirX * dt;		//位移变化
                        par.m_posY += par.m_dirY * dt;
                        //					System.out.println("Move x:" + tmpx + " y:" + tmpy);
                    }
                    else if (this.m_peEmitterMode == ParticleMode.Radius)
                    {
                        //Update the angle and radius of the particle.
                        par.m_angle += par.m_degreesPerSecond * dt;
                        par.m_radius += par.m_deltaRadius * dt;
                        par.m_posX = par.m_startPosX + (Single)(-Math.Cos(par.m_angle) * par.m_radius);
                        par.m_posY = par.m_startPosY + (Single)(-Math.Sin(par.m_angle) * par.m_radius);
                    }

                    //颜色 尺寸 自旋转
                    par.m_colorA += par.m_deltaColorA * dt;
                    par.m_colorR += par.m_deltaColorR * dt;
                    par.m_colorG += par.m_deltaColorG * dt;
                    par.m_colorB += par.m_deltaColorB * dt;
                    par.m_size = Math.Max(0, par.m_size + par.m_deltaSize * dt);
                    par.m_rotation += par.m_deltaRotation * dt;

                    //下一颗粒子
                    ++index;
                }
                else
                {
                    //把消亡的粒子放到末尾
                    if (index < this.m_iParticleCount - 1)
                    {
                        Particle tmp = this.m_arParticles[index];
                        this.m_arParticles[index] = this.m_arParticles[this.m_iParticleCount - 1];
                        this.m_arParticles[this.m_iParticleCount - 1] = tmp;
                    }
                    --this.m_iParticleCount;
                }
            }
        }

        /// <summary>
        /// 绘制粒子效果。
        /// <param name="p">绘制位置。</param>
        /// </summary>
        public void Paint(Point p)
        {
            if (!this.m_bVisible)
            {
                return;
            }

            UInt32 oldtextures = 0;							//当前绑定的纹理
            GL.glGetIntegerv(GL.GL_TEXTURE_BINDING_2D, out oldtextures);		//先获得原来绑定的纹理编号，以便在最后进行恢复
            GL.glBindTexture(GL.GL_TEXTURE_2D, this.m_iTexID);
            GL.glColor4ub(255, 255, 255, 255);
            GL.glBegin(GL.GL_QUADS);
            Single imgoffset = (float)Math.Atan2(m_bmpImage.Height / 2, m_bmpImage.Width / 2);
            for (Int32 i = m_iParticleCount - 1; i >= 0; --i)
            {
                Particle par = m_arParticles[i];
                float rot = -par.m_rotation * 0.01745329252f;       //C#为逆时针旋转，与CC旋转方式不一致，所以取负数改为顺时针
                float len = par.m_size * 1.414f / 2;
                float rx1 = len * (float)Math.Cos(imgoffset + rot) * m_fScaleX;
                float ry1 = len * (float)Math.Sin(imgoffset + rot) * m_fScaleY;
                float rx2 = len * (float)Math.Cos(imgoffset - rot) * m_fScaleX;
                float ry2 = len * (float)Math.Sin(imgoffset - rot) * m_fScaleY;
                float posx = par.m_posX * m_fScaleX + p.X;
                float posy = par.m_posY * m_fScaleY + p.Y;
                GL.glColor4ub((Byte)par.m_colorR, (Byte)par.m_colorG, (Byte)par.m_colorB, (Byte)par.m_colorA);
                GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex2f(posx - rx1, posy - ry1);
                GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex2f(posx + rx2, posy - ry2);
                GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex2f(posx + rx1, posy + ry1);
                GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex2f(posx - rx2, posy + ry2);
            }

            GL.glEnd();
            GL.glBindTexture(GL.GL_TEXTURE_2D, oldtextures);
        }

        /// <summary>
        /// 移动粒子效果。
        /// </summary>
        /// <param name="mx">水平方向的移动量。</param>
        /// <param name="my">竖直方向的移动量。</param>
        public void Move(Int32 mx, Int32 my)
        {
            this.m_ptPos.X = this.m_ptPos.X + mx;
            this.m_ptPos.Y = this.m_ptPos.Y + my;
            if (this.m_ptPosType == PositionType.Relative)
            {
                for (Int32 i = 0; i < this.m_iParticleCount; ++i)
                {
                    Particle par = this.m_arParticles[i];
                    par.m_posX += mx;
                    par.m_posY += my;
                    par.m_startPosX += mx;
                    par.m_startPosY += my;
                }
            }
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public String GetNodeName()
        {
            String name = this.m_strName;
            if (!this.m_bVisible)
            {
                name = name + "|Hide";
            }
            return name;
        }

        /// <summary>
        /// 初始化预设的效果。
        /// </summary>
        /// <param name="type">效果类型。</param>
        public void InitPreinstallEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Whirlpool:
                    InitWhirlpoolEffect();
                    break;
                case EffectType.Fire:
                    InitFireEffect();
                    break;
                case EffectType.Flower:
                    InitFlowerEffect();
                    break;
                case EffectType.Galaxy:
                    InitGalaxyEffect();
                    break;
                case EffectType.Snow:
                    InitSnowEffect();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 将界面返回成XML的粒子系统节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档。</param>
        /// <returns>XML的Interface节点</returns>
        public XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            //编辑器数据
            XmlNode xmlEffect = xmlDoc.CreateNode(XmlNodeType.Element, "Effect", "");
            xmlEffect.Attributes.Append(xmlDoc.CreateAttribute("Name")).InnerText = m_strName;
            xmlEffect.Attributes.Append(xmlDoc.CreateAttribute("Visible")).InnerText = m_bVisible.ToString();

            //基本数据
            XmlNode xmlBasic = xmlDoc.CreateNode(XmlNodeType.Element, "Basic", "");
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("ImageName")).InnerText = m_strImageName;
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("EmitterMode")).InnerText = ((Int32)m_peEmitterMode).ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("PosType")).InnerText = ((Int32)m_ptPosType).ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("TotalParticles")).InnerText = m_iTotalParticles.ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("EmissionRate")).InnerText = m_fEmissionRate.ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("Duration")).InnerText = m_fDuration.ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("Delay")).InnerText = m_fDelay.ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("ScaleX")).InnerText = m_fScaleX.ToString();
            xmlBasic.Attributes.Append(xmlDoc.CreateAttribute("ScaleY")).InnerText = m_fScaleY.ToString();
            xmlEffect.AppendChild(xmlBasic);

            //通用数据
            XmlNode xmlCommon = xmlDoc.CreateNode(XmlNodeType.Element, "Common", "");
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("Life")).InnerText = m_fLife.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("LifeVar")).InnerText = m_fLifeVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("Angle")).InnerText = m_fAngle.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("AngleVar")).InnerText = m_fAngleVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartSize")).InnerText = m_fStartSize.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartSizeVar")).InnerText = m_fStartSizeVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndSize")).InnerText = m_fEndSize.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndSizeVar")).InnerText = m_fEndSizeVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartSpin")).InnerText = m_fStartSpin.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartSpinVar")).InnerText = m_fStartSpinVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndSpin")).InnerText = m_fEndSpin.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndSpinVar")).InnerText = m_fEndSpinVar.ToString();
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("Pos")).InnerText = DataUtil.ToStringValue(m_ptPos);
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("PosVar")).InnerText = DataUtil.ToStringValue(m_ptPosVar);
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartColor")).InnerText = DataUtil.ToStringValue(m_clStartColor);
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("StartColorVar")).InnerText = DataUtil.ToStringValue(m_clStartColorVar);
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndColor")).InnerText = DataUtil.ToStringValue(m_clEndColor);
            xmlCommon.Attributes.Append(xmlDoc.CreateAttribute("EndColorVar")).InnerText = DataUtil.ToStringValue(m_clEndColorVar);
            xmlEffect.AppendChild(xmlCommon);

            //重力模式数据
            XmlNode xmlGravity = xmlDoc.CreateNode(XmlNodeType.Element, "Gravity", "");
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("GravityX")).InnerText = m_fGravityX.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("GravityY")).InnerText = m_fGravityY.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("Speed")).InnerText = m_fSpeed.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("SpeedVar")).InnerText = m_fSpeedVar.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("TangentialAccel")).InnerText = m_fTangentialAccel.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("TangentialAccelVar")).InnerText = m_fTangentialAccelVar.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("RadialAccel")).InnerText = m_fRadialAccel.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("RadialAccelVar")).InnerText = m_fRadialAccelVar.ToString();
            xmlGravity.Attributes.Append(xmlDoc.CreateAttribute("RotationIsDir")).InnerText = m_fRotationIsDir.ToString();
            xmlEffect.AppendChild(xmlGravity);

            //半径模式数据
            XmlNode xmlRadius = xmlDoc.CreateNode(XmlNodeType.Element, "Radius", "");
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("StartRadius")).InnerText = m_fStartRadius.ToString();
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("StartRadiusVar")).InnerText = m_fStartRadiusVar.ToString();
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("EndRadius")).InnerText = m_fEndRadius.ToString();
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("EndRadiusVar")).InnerText = m_fEndRadiusVar.ToString();
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("RotatePerSecond")).InnerText = m_fRotatePerSecond.ToString();
            xmlRadius.Attributes.Append(xmlDoc.CreateAttribute("RotatePerSecondVar")).InnerText = m_fRotatePerSecondVar.ToString();
            xmlEffect.AppendChild(xmlRadius);

            return xmlEffect;
        }

        /// <summary>
        /// 从XML节点加载粒子效果对象。
        /// </summary>
        /// <param name="par">所从属的粒子系统。</param>
        /// <param name="file">XML节点。</param>
        /// <returns>粒子效果对象。</returns>
        public static ParticleEffect LoadFromXmlNode(ParticleSystem par, XmlNode xmlNode)
        {
            ParticleEffect eft = new ParticleEffect(par);
            eft.m_strName = xmlNode.Attributes["Name"].InnerText;
            eft.m_bVisible = Boolean.Parse(xmlNode.Attributes["Visible"].InnerText);

            //读入基本数据
            XmlNode xmlBasic = xmlNode.SelectSingleNode("Basic");
            eft.m_strImageName = xmlBasic.Attributes["ImageName"].InnerText;
            eft.m_peEmitterMode = (ParticleMode)Int32.Parse(xmlBasic.Attributes["EmitterMode"].InnerText);
            eft.m_ptPosType = (PositionType)Int32.Parse(xmlBasic.Attributes["PosType"].InnerText);
            eft.TotalParticles = Int32.Parse(xmlBasic.Attributes["TotalParticles"].InnerText);
            eft.m_fEmissionRate = Single.Parse(xmlBasic.Attributes["EmissionRate"].InnerText);
            eft.m_fDuration = Single.Parse(xmlBasic.Attributes["Duration"].InnerText);
            eft.m_fDelay = Single.Parse(xmlBasic.Attributes["Delay"].InnerText);
            try
            {
                eft.Image = (Bitmap)Bitmap.FromFile(ProjectManager.Project.AssetsFolder + eft.m_strImageName);
            }
            catch
            {
                throw new Exception(String.Format("打开粒子图像失败!\nImage:\"{0}\"", eft.m_strImageName));
            }
            String strScaleX = XmlUtil.GetAttribute(xmlBasic, "ScaleX");
            String strScaleY = XmlUtil.GetAttribute(xmlBasic, "ScaleY");
            eft.m_fScaleX = strScaleX.Equals(String.Empty) ? 1.0f : Single.Parse(strScaleX);
            eft.m_fScaleY = strScaleY.Equals(String.Empty) ? 1.0f : Single.Parse(strScaleY);

            //读入通用数据
            XmlNode xmlCommon = xmlNode.SelectSingleNode("Common");
            eft.m_fLife = Single.Parse(xmlCommon.Attributes["Life"].InnerText);
            eft.m_fLifeVar = Single.Parse(xmlCommon.Attributes["LifeVar"].InnerText);
            eft.m_fAngle = Single.Parse(xmlCommon.Attributes["Angle"].InnerText);
            eft.m_fAngleVar = Single.Parse(xmlCommon.Attributes["AngleVar"].InnerText);
            eft.m_fStartSize = Single.Parse(xmlCommon.Attributes["StartSize"].InnerText);
            eft.m_fStartSizeVar = Single.Parse(xmlCommon.Attributes["StartSizeVar"].InnerText);
            eft.m_fEndSize = Single.Parse(xmlCommon.Attributes["EndSize"].InnerText);
            eft.m_fEndSizeVar = Single.Parse(xmlCommon.Attributes["EndSizeVar"].InnerText);
            eft.m_fStartSpin = Single.Parse(xmlCommon.Attributes["StartSpin"].InnerText);
            eft.m_fStartSpinVar = Single.Parse(xmlCommon.Attributes["StartSpinVar"].InnerText);
            eft.m_fEndSpin = Single.Parse(xmlCommon.Attributes["EndSpin"].InnerText);
            eft.m_fEndSpinVar = Single.Parse(xmlCommon.Attributes["EndSpinVar"].InnerText);
            eft.m_ptPos = DataUtil.ParsePoint(xmlCommon.Attributes["Pos"].InnerText);
            eft.m_ptPosVar = DataUtil.ParsePoint(xmlCommon.Attributes["PosVar"].InnerText);
            eft.m_clStartColor = DataUtil.ParseColor(xmlCommon.Attributes["StartColor"].InnerText);
            eft.m_clStartColorVar = DataUtil.ParseColor(xmlCommon.Attributes["StartColorVar"].InnerText);
            eft.m_clEndColor = DataUtil.ParseColor(xmlCommon.Attributes["EndColor"].InnerText);
            eft.m_clEndColorVar = DataUtil.ParseColor(xmlCommon.Attributes["EndColorVar"].InnerText);

            //读入重力模式数据
            XmlNode xmlGravity = xmlNode.SelectSingleNode("Gravity");
            eft.m_fGravityX = Single.Parse(xmlGravity.Attributes["GravityX"].InnerText);
            eft.m_fGravityY = Single.Parse(xmlGravity.Attributes["GravityY"].InnerText);
            eft.m_fSpeed = Single.Parse(xmlGravity.Attributes["Speed"].InnerText);
            eft.m_fSpeedVar = Single.Parse(xmlGravity.Attributes["SpeedVar"].InnerText);
            eft.m_fTangentialAccel = Single.Parse(xmlGravity.Attributes["TangentialAccel"].InnerText);
            eft.m_fTangentialAccelVar = Single.Parse(xmlGravity.Attributes["TangentialAccelVar"].InnerText);
            eft.m_fRadialAccel = Single.Parse(xmlGravity.Attributes["RadialAccel"].InnerText);
            eft.m_fRadialAccelVar = Single.Parse(xmlGravity.Attributes["RadialAccelVar"].InnerText);
            eft.m_fRotationIsDir = Boolean.Parse(xmlGravity.Attributes["RotationIsDir"].InnerText);

            //读入半径模式数据
            XmlNode xmlRadius = xmlNode.SelectSingleNode("Radius");
            eft.m_fStartRadius = Single.Parse(xmlRadius.Attributes["StartRadius"].InnerText);
            eft.m_fStartRadiusVar = Single.Parse(xmlRadius.Attributes["StartRadiusVar"].InnerText);
            eft.m_fEndRadius = Single.Parse(xmlRadius.Attributes["EndRadius"].InnerText);
            eft.m_fEndRadiusVar = Single.Parse(xmlRadius.Attributes["EndRadiusVar"].InnerText);
            eft.m_fRotatePerSecond = Single.Parse(xmlRadius.Attributes["RotatePerSecond"].InnerText);
            eft.m_fRotatePerSecondVar = Single.Parse(xmlRadius.Attributes["RotatePerSecondVar"].InnerText);

            return eft;
        }

        /// <summary>
        /// 将用粒子效果入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public void WriteToStream(Stream stream)
        {
            //基本属性
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(m_strImageName.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes((Int32)m_peEmitterMode));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes((Int32)m_ptPosType));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iTotalParticles));
            DataUtil.WriteSingle(stream, m_fEmissionRate);
            DataUtil.WriteSingle(stream, m_fDuration);
            DataUtil.WriteSingle(stream, m_fDelay);
            DataUtil.WriteSingle(stream, m_fScaleX);
            DataUtil.WriteSingle(stream, m_fScaleY);

            //通用属性
            DataUtil.WriteSingle(stream, m_fLife);
            DataUtil.WriteSingle(stream, m_fLifeVar);
            DataUtil.WriteSingle(stream, m_fAngle);
            DataUtil.WriteSingle(stream, m_fAngleVar);
            DataUtil.WriteSingle(stream, m_fStartSize);
            DataUtil.WriteSingle(stream, m_fStartSizeVar);
            DataUtil.WriteSingle(stream, m_fEndSize);
            DataUtil.WriteSingle(stream, m_fEndSizeVar);
            DataUtil.WriteSingle(stream, m_fStartSpin);
            DataUtil.WriteSingle(stream, m_fStartSpinVar);
            DataUtil.WriteSingle(stream, m_fEndSpin);
            DataUtil.WriteSingle(stream, m_fEndSpinVar);
            DataUtil.WritePoint(stream, m_ptPos);
            DataUtil.WritePoint(stream, m_ptPosVar);
            DataUtil.WriteColor(stream, m_clStartColor);
            DataUtil.WriteColor(stream, m_clStartColorVar);
            DataUtil.WriteColor(stream, m_clEndColor);
            DataUtil.WriteColor(stream, m_clEndColorVar);

            //重力模式
            DataUtil.WriteSingle(stream, m_fGravityX);
            DataUtil.WriteSingle(stream, m_fGravityY);
            DataUtil.WriteSingle(stream, m_fSpeed);
            DataUtil.WriteSingle(stream, m_fSpeedVar);
            DataUtil.WriteSingle(stream, m_fTangentialAccel);
            DataUtil.WriteSingle(stream, m_fTangentialAccelVar);
            DataUtil.WriteSingle(stream, m_fRadialAccel);
            DataUtil.WriteSingle(stream, m_fRadialAccelVar);
            stream.WriteByte(DataUtil.GetBooleanByte(m_fRotationIsDir));

            //半径模式
            DataUtil.WriteSingle(stream, m_fStartRadius);
            DataUtil.WriteSingle(stream, m_fStartRadiusVar);
            DataUtil.WriteSingle(stream, m_fEndRadius);
            DataUtil.WriteSingle(stream, m_fEndRadiusVar);
            DataUtil.WriteSingle(stream, m_fRotatePerSecond);
            DataUtil.WriteSingle(stream, m_fRotatePerSecondVar);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 粒子发射器一直喷发。
        /// </summary>
        public const int ParticleDurationInfinity = -1;

        /// <summary>
        /// 粒子起始尺寸等于结束尺寸。
        /// </summary>
        public const int ParticleStartSizeEqualToEndSize = -1;

        /// <summary>
        /// 粒子其实半径等于结束半径。
        /// </summary>
        public const int ParticleStartRadiusEqualToEndRadius = -1;

        [CategoryAttribute("重力模式"), DescriptionAttribute("重力X，只在重力模式下起作用")]
        public Single GravityX
        {
            get
            {
                return this.m_fGravityX;
            }
            set
            {
                this.m_fGravityX = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("重力Y，只在重力模式下起作用")]
        public Single GravityY
        {
            get
            {
                return this.m_fGravityY;
            }
            set
            {
                this.m_fGravityY = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("速度，只在重力模式下起作用")]
        public Single Speed
        {
            get
            {
                return this.m_fSpeed;
            }
            set
            {
                this.m_fSpeed = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("速度变化范围，只在重力模式下起作用")]
        public Single SpeedVar
        {
            get
            {
                return this.m_fSpeedVar;
            }
            set
            {
                this.m_fSpeedVar = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("粒子切向加速度，只在重力模式下起作用")]
        public Single TangentialAccel
        {
            get
            {
                return this.m_fTangentialAccel;
            }
            set
            {
                this.m_fTangentialAccel = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("粒子切向加速度变化范围，只在重力模式下起作用")]
        public Single TangentialAccelVar
        {
            get
            {
                return this.m_fTangentialAccelVar;
            }
            set
            {
                this.m_fTangentialAccelVar = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("粒子径向加速度，只在重力模式下起作用")]
        public Single RadialAccel
        {
            get
            {
                return this.m_fRadialAccel;
            }
            set
            {
                this.m_fRadialAccel = value;
            }
        }

        [CategoryAttribute("重力模式"), DescriptionAttribute("粒子径向加速度变化范围，只在重力模式下起作用")]
        public Single RadialAccelVar
        {
            get
            {
                return this.m_fRadialAccelVar;
            }
            set
            {
                this.m_fRadialAccelVar = value;
            }
        }

        [Browsable(false)]
        public Boolean RotationIsDir
        {
            get
            {
                return this.m_fRotationIsDir;
            }
            set
            {
                this.m_fRotationIsDir = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("初始半径，只在半径模式下起作用")]
        public Single StartRadius
        {
            get
            {
                return this.m_fStartRadius;
            }
            set
            {
                this.m_fStartRadius = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("初始半径变化范围，只在半径模式下起作用")]
        public Single StartRadiusVar
        {
            get
            {
                return this.m_fStartRadiusVar;
            }
            set
            {
                this.m_fStartRadiusVar = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("结束半径，只在半径模式下起作用")]
        public Single EndRadius
        {
            get
            {
                return this.m_fEndRadius;
            }
            set
            {
                this.m_fEndRadius = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("结束半径变化范围，只在半径模式下起作用")]
        public Single EndRadiusVar
        {
            get
            {
                return this.m_fEndRadiusVar;
            }
            set
            {
                this.m_fEndRadiusVar = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("粒子围绕初始点的每秒旋转角度，只在半径模式下起作用")]
        public Single RotatePerSecond
        {
            get
            {
                return this.m_fRotatePerSecond;
            }
            set
            {
                this.m_fRotatePerSecond = value;
            }
        }

        [CategoryAttribute("半径模式"), DescriptionAttribute("粒子围绕初始点的每秒旋转角度的变化范围，只在半径模式下起作用")]
        public Single RotatePerSecondVar
        {
            get
            {
                return this.m_fRotatePerSecondVar;
            }
            set
            {
                this.m_fRotatePerSecondVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子生命。
        /// </summary>
        [Browsable(false)]
        public Single Life
        {
            get
            {
                return this.m_fLife;
            }
            set
            {
                this.m_fLife = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子生命变化范围。
        /// </summary>
        [Browsable(false)]
        public Single LifeVar
        {
            get
            {
                return this.m_fLifeVar;
            }
            set
            {
                this.m_fLifeVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子角度。
        /// </summary>
        [Browsable(false)]
        public Single Angle
        {
            get
            {
                return this.m_fAngle;
            }
            set
            {
                this.m_fAngle = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子角度变化范围。
        /// </summary>
       [Browsable(false)]
        public Single AngleVar
        {
            get
            {
                return this.m_fAngleVar;
            }
            set
            {
                this.m_fAngleVar = value;
            }
        }

        /// <summary>
        /// 获取或设置发射器位置。
        /// </summary>
        [Browsable(false)]
        public Point Position
        {
            get
            {
                return this.m_ptPos;
            }
            set
            {
                this.m_ptPos = value;
            }
        }

        /// <summary>
        /// 获取或设置发射器位置变化范围。
        /// </summary>
        [Browsable(false)]
        public Point PositionVar
        {
            get
            {
                return this.m_ptPosVar;
            }
            set
            {
                this.m_ptPosVar = value;
            }
        }

        /// <summary>
        /// 获取或设置水平缩放比例。
        /// </summary>
        [Browsable(false)]
        public Single ScaleX
        {
            get
            {
                return this.m_fScaleX;
            }
            set
            {
                this.m_fScaleX = value;
            }
        }

        /// <summary>
        /// 获取或设置竖直缩放比例。
        /// </summary>
        [Browsable(false)]
        public Single ScaleY
        {
            get
            {
                return this.m_fScaleY;
            }
            set
            {
                this.m_fScaleY = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始大小。
        /// </summary>
        [Browsable(false)]
        public Single StartSize
        {
            get
            {
                return this.m_fStartSize;
            }
            set
            {
                this.m_fStartSize = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始大小。
        /// </summary>
        [Browsable(false)]
        public Single StartSizeVar
        {
            get
            {
                return this.m_fStartSizeVar;
            }
            set
            {
                this.m_fStartSizeVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始大小。        
        /// </summary>
        [Browsable(false)]
        public Single EndSize
        {
            get
            {
                return this.m_fEndSize;
            }
            set
            {
                this.m_fEndSize = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始大小。
        /// </summary>
        [Browsable(false)]
        public Single EndSizeVar
        {
            get
            {
                return this.m_fEndSizeVar;
            }
            set
            {
                this.m_fEndSizeVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子起始自旋。
        /// </summary>
        [Browsable(false)]
        public Single StartSpin
        {
            get
            {
                return this.m_fStartSpin;
            }
            set
            {
                this.m_fStartSpin = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子起始自旋波动。
        /// </summary>
        [Browsable(false)]
        public Single StartSpinVar
        {
            get
            {
                return this.m_fStartSpinVar;
            }
            set
            {
                this.m_fStartSpinVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子结束自旋。
        /// </summary>
        [Browsable(false)]
        public Single EndSpin
        {
            get
            {
                return this.m_fEndSpin;
            }
            set
            {
                this.m_fEndSpin = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子结束自旋波动。
        /// </summary>
        [Browsable(false)]
        public Single EndSpinVar
        {
            get
            {
                return this.m_fEndSpinVar;
            }
            set
            {
                this.m_fEndSpinVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始颜色。
        /// </summary>
        [Browsable(false)]
        public Color StartColor
        {
            get
            {
                return this.m_clStartColor;
            }
            set
            {
                this.m_clStartColor = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子初始颜色变化范围。
        /// </summary>
        [Browsable(false)]
        public Color StartColorVar
        {
            get
            {
                return this.m_clStartColorVar;
            }
            set
            {
                this.m_clStartColorVar = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子结束颜色。
        /// </summary>
        [Browsable(false)]
        public Color EndColor
        {
            get
            {
                return this.m_clEndColor;
            }
            set
            {
                this.m_clEndColor = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子结束颜色变化范围。
        /// </summary>
        [Browsable(false)]
        public Color EndColorVar
        {
            get
            {
                return this.m_clEndColorVar;
            }
            set
            {
                this.m_clEndColorVar = value;
            }
        }

        /// <summary>
        /// 获取或设置发射的粒子轨迹模式。
        /// </summary>
        [Browsable(false)]
        public ParticleMode EmitterMode
        {
            get
            {
                return this.m_peEmitterMode;
            }
            set
            {
                this.m_peEmitterMode = value;
            }
        }

        /// <summary>
        /// 获取或设置发射出去后的粒子位置与粒子系统的关系。
        /// </summary>
        [Browsable(false)]
        public PositionType PosType
        {
            get
            {
                return this.m_ptPosType;
            }
            set
            {
                this.m_ptPosType = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子最大数量。
        /// </summary>
        [Browsable(false)]
        public Int32 TotalParticles
        {
            get
            {
                return this.m_iTotalParticles;
            }
            set
            {
                if (value > 0)
                {
                    m_iParticleCount = 0;
                    this.m_iTotalParticles = value;
                    m_arParticles = new Particle[m_iTotalParticles];
                    for (int i = 0; i < m_iTotalParticles; ++i)
                    {
                        m_arParticles[i] = new Particle();
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置粒子发射率。
        /// </summary>
        [Browsable(false)]
        public Single EmissionRate
        {
            get
            {
                return this.m_fEmissionRate;
            }
            set
            {
                this.m_fEmissionRate = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子发射持续的时间。
        /// </summary>
        [Browsable(false)]
        public Single Duration
        {
            get
            {
                return this.m_fDuration;
            }
            set
            {
                this.m_fDuration = value;
            }
        }

        /// <summary>
        /// 获取或设置粒子开始发射延时。
        /// </summary>
        [Browsable(false)]
        public Single Delay
        {
            get
            {
                return this.m_fDelay;
            }
            set
            {
                this.m_fDelay = value;
            }
        }

        /// <summary>
        /// 获取或设置效果所使用的粒子图像。
        /// </summary>
        [Browsable(false)]
        public Bitmap Image
        {
            get
            {
                return this.m_bmpImage;
            }
            set
            {
                this.m_bmpImage = value;
                DeleteTex(m_iTexID);
                this.m_iTexID = CreateTex(m_bmpImage);
            }
        }

        /// <summary>
        /// 获取发射器是否还继续发射。
        /// </summary>
        [Browsable(false)]
        public Boolean IsActive
        {
            get
            {
                return this.m_bIsActive;
            }
        }

        /// <summary>
        /// 获取当前粒子数量。
        /// </summary>
        [Browsable(false)]
        public Int32 ParticleCount
        {
            get
            {
                return this.m_iParticleCount;
            }
        }

        /// <summary>
        /// 获取粒子是否已经发射完毕。
        /// </summary>
        [Browsable(false)]
        public Boolean IsFull
        {
            get
            {
                return this.m_iParticleCount >= this.m_iTotalParticles;
            }
        }

        /// <summary>
        /// 获取或设置效果是否显示。
        /// </summary>
        [Browsable(false)]
        public Boolean Visible
        {
            get
            {
                return this.m_bVisible;
            }
            set
            {
                this.m_bVisible = value;
            }
        }

        /// <summary>
        /// 获取或设置效果名称。
        /// </summary>
        [Browsable(false)]
        public String Name
        {
            get
            {
                return this.m_strName;
            }
            set
            {
                this.m_strName = value;
            }
        }

        /// <summary>
        /// 获取或设置图像名称。
        /// </summary>
        [Browsable(false)]
        public String ImageName
        {
            get
            {
                return this.m_strImageName;
            }
            set
            {
                this.m_strImageName = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 获取颜色通道对应的颜色矩阵。
        /// </summary>
        /// <param name="channel">颜色通道。</param>
        /// <returns>颜色矩阵</returns>
        public static ColorMatrix GetColorMatrix(Color channel)
        {
            ColorMatrix mat = new ColorMatrix(new float[][]{
                new float[]{1,0,0,0,0},
                new float[]{0,1,0,0,0},
                new float[]{0,0,1,0,0},
                new float[]{0,0,0,1,0},
                new float[]{0,0,0,0,0}});

            //通道颜色处理
            mat.Matrix00 = 1.0f * channel.R / 255;
            mat.Matrix11 = 1.0f * channel.G / 255;
            mat.Matrix22 = 1.0f * channel.B / 255;
            mat.Matrix33 = 1.0f * channel.A / 255;

            return mat;
        }

        /// <summary>
        /// 获取-1到1之间的随机浮点数。
        /// </summary>
        /// <returns>随机浮点数。</returns>
        public static Single GetRandomM11()
        {
            return (Single)(2.0f * m_rdRand.NextDouble() - 1);
        }

        /// <summary>
        /// 角度转化成弧度。
        /// </summary>
        /// <param name="angle">角度值。</param>
        /// <returns>弧度值。</returns>
        public static Single DegreesToRadians(Single angle)
        {
            return angle * 0.01745329252f; //PI / 180
        }

        /// <summary>
        /// 弧度转化成角度。
        /// </summary>
        /// <param name="angle">弧度值。</param>
        /// <returns>角度值。</returns>
        public static Single RadiansToDegrees(Single angle)
        {
            return angle * 57.29577951f; //180 / PI
        }

        /// <summary>
        /// 创建纹理。
        /// </summary>
        /// <param name="bmp">要创建纹理的位图。</param>
        /// <returns>纹理ID。</returns>
        public static UInt32 CreateTex(Bitmap bmp)
        {
            Int32 oldtex;
            GL.glGetIntegerv(GL.GL_TEXTURE_BINDING_2D, out oldtex);

            //创建新纹理并设置缩放和重复方式
            UInt32[] tex = new UInt32[1];
            GL.glGenTextures(1, tex);
            GL.glBindTexture(GL.GL_TEXTURE_2D, tex[0]);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, GL.GL_REPEAT);

            //保存当前内存对齐方式
            UInt32 alignment = 0;							//当前的数据对齐
            GL.glGetIntegerv(GL.GL_UNPACK_ALIGNMENT, out alignment);
            GL.glPixelStorei(GL.GL_UNPACK_ALIGNMENT, 4);

            //获取图像数据
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bitmapdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //创建纹理
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGBA, bmp.Width, bmp.Height, 0, GL.GL_BGRA_EXT, GL.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

            //恢复当前纹理和内存对齐方式
            GL.glPixelStorei(GL.GL_UNPACK_ALIGNMENT, (Int32)alignment);
            GL.glBindTexture(GL.GL_TEXTURE_2D, (UInt32)oldtex);
            return tex[0];
        }

        /// <summary>
        /// 删除纹理。
        /// </summary>
        public static void DeleteTex(UInt32 id)
        {
            if (id != 0)
            {
                UInt32[] tex = new UInt32[1];
                tex[0] = id;
                GL.glDeleteTextures(1, tex);
            }
        }

        /// <summary>
        /// 增加一颗粒子。
        /// </summary>
        /// <returns>是否增加成功。</returns>
        protected Boolean AddParticle()
        {
            if (this.IsFull)
            {
                return false;
            }
            this.InitParticle(this.m_arParticles[this.m_iParticleCount++]);
            return true;
        }

        /// <summary>
        /// 初始化一颗粒子。
        /// </summary>
        /// <param name="par">要初始化的粒子。</param>
        protected void InitParticle(Particle par)
        {
            //生命和起始位置
            par.m_timeToLive = Math.Max(0.001f, this.m_fLife + this.m_fLifeVar * GetRandomM11());
            par.m_posX = this.m_ptPos.X + this.m_ptPosVar.X * GetRandomM11();
            par.m_posY = this.m_ptPos.Y + this.m_ptPosVar.Y * GetRandomM11();
            par.m_startPosX = par.m_posX;
            par.m_startPosY = par.m_posY;

            //颜色
            Single starta = this.m_clStartColor.A + this.m_clStartColorVar.A * GetRandomM11();
            Single startr = this.m_clStartColor.R + this.m_clStartColorVar.R * GetRandomM11();
            Single startg = this.m_clStartColor.G + this.m_clStartColorVar.G * GetRandomM11();
            Single startb = this.m_clStartColor.B + this.m_clStartColorVar.B * GetRandomM11();
            Single enda = this.m_clEndColor.A + this.m_clEndColorVar.A * GetRandomM11();
            Single endr = this.m_clEndColor.R + this.m_clEndColorVar.R * GetRandomM11();
            Single endg = this.m_clEndColor.G + this.m_clEndColorVar.G * GetRandomM11();
            Single endb = this.m_clEndColor.B + this.m_clEndColorVar.B * GetRandomM11();

            par.m_colorA = starta;
            par.m_colorR = startr;
            par.m_colorG = startg;
            par.m_colorB = startb;
            par.m_deltaColorA = (enda - starta) / par.m_timeToLive;
            par.m_deltaColorR = (endr - startr) / par.m_timeToLive;
            par.m_deltaColorG = (endg - startg) / par.m_timeToLive;
            par.m_deltaColorB = (endb - startb) / par.m_timeToLive;

            //尺寸
            Single startsize = Math.Max(0, this.m_fStartSize + this.m_fStartSizeVar * GetRandomM11());
            par.m_size = startsize;
            if (this.m_fEndSize == ParticleStartSizeEqualToEndSize)
            {
                par.m_deltaSize = 0;
            }
            else
            {
                Single endsize = Math.Max(0, this.m_fEndSize + this.m_fEndSizeVar * GetRandomM11());
                par.m_deltaSize = (endsize - startsize) / par.m_timeToLive;
            }

            //旋转
            Single startspin = this.m_fStartSpin + this.m_fStartSpinVar * GetRandomM11();
            Single endspin = this.m_fEndSpin + this.m_fEndSpinVar * GetRandomM11();
            par.m_rotation = startspin;
            par.m_deltaRotation = (endspin - startspin) / par.m_timeToLive;

            //方向
            Single a = DegreesToRadians(this.m_fAngle + this.m_fAngleVar * GetRandomM11());
            if (this.m_peEmitterMode == ParticleMode.Gravity)
            {
                Single vx = (Single)Math.Cos(a);
                Single vy = (Single)Math.Sin(a);
                Single s = this.m_fSpeed + this.m_fSpeedVar * GetRandomM11();
                par.m_dirX = vx * s;
                par.m_dirY = vy * s;
                par.m_radialAccel = this.m_fRadialAccel + this.m_fRadialAccelVar * GetRandomM11();
                par.m_tangentialAccel = this.m_fTangentialAccel + this.m_fTangentialAccelVar * GetRandomM11();
                if (this.m_fRotationIsDir)
                {
                    par.m_rotation = -RadiansToDegrees((Single)Math.Atan2(par.m_dirY, par.m_dirX));
                }
            }
            else if (this.m_peEmitterMode == ParticleMode.Radius)
            {
                Single startradius = this.m_fStartRadius + this.m_fStartRadiusVar * GetRandomM11();
                par.m_radius = startradius;
                if (this.m_fEndRadius == ParticleStartRadiusEqualToEndRadius)
                {
                    par.m_deltaRadius = 0;
                }
                else
                {
                    Single endradius = this.m_fEndRadius + this.m_fEndRadiusVar * GetRandomM11();
                    par.m_deltaRadius = (endradius - startradius) / par.m_timeToLive;
                }
                par.m_angle = a;
                par.m_degreesPerSecond = DegreesToRadians(this.m_fRotatePerSecond + this.m_fRotatePerSecondVar * GetRandomM11());
            }
        }

        /// <summary>
        /// 初始画化漩涡效果。
        /// </summary>
        public void InitWhirlpoolEffect()
        {
            m_fGravityX = 0;
            m_fGravityY = 0;
            m_fSpeed = 0;
            m_fSpeedVar = 0;
            m_fTangentialAccel = 0;
            m_fTangentialAccelVar = 0;
            m_fRadialAccel = 0;
            m_fRadialAccelVar = 0;
            m_fRotationIsDir = false;

            m_fStartRadius = 100;
            m_fStartRadiusVar = 20;
            m_fEndRadius = 20;
            m_fEndRadiusVar = 10;
            m_fRotatePerSecond = 100;
            m_fRotatePerSecondVar = 10;

            m_fLife = 1;
            m_fLifeVar = 1;
            m_fAngle = 90;
            m_fAngleVar = 180;
            m_fStartSize = 10;
            m_fStartSizeVar = 3;
            m_fEndSize = ParticleStartSizeEqualToEndSize;
            m_fEndSizeVar = 0;
            m_fStartSpin = 0;
            m_fStartSpinVar = 0;
            m_fEndSpin = 0;
            m_fEndSpinVar = 0;
            m_ptPos = Point.Empty;
            m_ptPosVar = Point.Empty;
            m_clStartColor = Color.FromArgb(126, 126, 126);
            m_clStartColorVar = Color.FromArgb(25, 126, 126, 126);
            m_clEndColor = Color.FromArgb(199, 25, 25, 25);
            m_clEndColorVar = Color.FromArgb(51, 25, 25, 25);

            m_peEmitterMode = ParticleMode.Radius;
            m_ptPosType = PositionType.Free;
            this.TotalParticles = 300;
            m_fEmissionRate = 150;
            m_fDuration = ParticleDurationInfinity;
        }

        /// <summary>
        /// 初始火焰效果。
        /// </summary>
        public void InitFireEffect()
        {
            m_fGravityX = 0;
            m_fGravityY = 0;
            m_fSpeed = 200;
            m_fSpeedVar = 20;
            m_fTangentialAccel = 0;
            m_fTangentialAccelVar = 0;
            m_fRadialAccel = 0;
            m_fRadialAccelVar = 0;
            m_fRotationIsDir = false;

            m_fStartRadius = 0;
            m_fStartRadiusVar = 0;
            m_fEndRadius = 0;
            m_fEndRadiusVar = 0;
            m_fRotatePerSecond = 0;
            m_fRotatePerSecondVar = 0;

            m_fLife = 1;
            m_fLifeVar = 0.25f;
            m_fAngle = 90;
            m_fAngleVar = 10;
            m_fStartSize = 30;
            m_fStartSizeVar = 5;
            m_fEndSize = ParticleStartSizeEqualToEndSize;
            m_fEndSizeVar = 0;
            m_fStartSpin = 0;
            m_fStartSpinVar = 0;
            m_fEndSpin = 0;
            m_fEndSpinVar = 0;
            m_ptPos = Point.Empty;
            m_ptPosVar = new Point(15, 10);
            m_clStartColor = Color.FromArgb(255, 178, 34, 34);
            m_clStartColorVar = Color.FromArgb(0, 0, 0, 0);
            m_clEndColor = Color.FromArgb(255, 0, 0, 0);
            m_clEndColorVar = Color.FromArgb(0, 0, 0, 0);

            m_peEmitterMode = ParticleMode.Gravity;
            m_ptPosType = PositionType.Free;
            this.TotalParticles = 300;
            m_fEmissionRate = 300;
            m_fDuration = ParticleDurationInfinity;
        }

        /// <summary>
        /// 初始花朵效果。
        /// </summary>
        public void InitFlowerEffect()
        {
            m_fGravityX = 0;
            m_fGravityY = 0;
            m_fSpeed = 80;
            m_fSpeedVar = 10;
            m_fTangentialAccel = 15;
            m_fTangentialAccelVar = 0;
            m_fRadialAccel = -60;
            m_fRadialAccelVar = 0;
            m_fRotationIsDir = false;

            m_fStartRadius = 0;
            m_fStartRadiusVar = 0;
            m_fEndRadius = 0;
            m_fEndRadiusVar = 0;
            m_fRotatePerSecond = 0;
            m_fRotatePerSecondVar = 0;

            m_fLife = 4;
            m_fLifeVar = 1;
            m_fAngle = 90;
            m_fAngleVar = 360;
            m_fStartSize = 20;
            m_fStartSizeVar = 5;
            m_fEndSize = ParticleStartSizeEqualToEndSize;
            m_fEndSizeVar = 0;
            m_fStartSpin = 0;
            m_fStartSpinVar = 0;
            m_fEndSpin = 0;
            m_fEndSpinVar = 0;
            m_ptPos = Point.Empty;
            m_ptPosVar = new Point(0, 0);
            m_clStartColor = Color.FromArgb(127, 127, 127);
            m_clStartColorVar = Color.FromArgb(127, 127, 127, 127);
            m_clEndColor = Color.FromArgb(0, 0, 0);
            m_clEndColorVar = Color.FromArgb(0, 0, 0, 0);

            m_peEmitterMode = ParticleMode.Gravity;
            m_ptPosType = PositionType.Free;
            this.TotalParticles = 250;
            m_fEmissionRate = 60;
            m_fDuration = ParticleDurationInfinity;
        }

        /// <summary>
        /// 初始星系效果。
        /// </summary>
        public void InitGalaxyEffect()
        {
            m_fGravityX = 0;
            m_fGravityY = 0;
            m_fSpeed = 60;
            m_fSpeedVar = 10;
            m_fTangentialAccel = 80;
            m_fTangentialAccelVar = 0;
            m_fRadialAccel = 0;
            m_fRadialAccelVar = 0;
            m_fRotationIsDir = false;

            m_fStartRadius = 0;
            m_fStartRadiusVar = 0;
            m_fEndRadius = 0;
            m_fEndRadiusVar = 0;
            m_fRotatePerSecond = 0;
            m_fRotatePerSecondVar = 0;

            m_fLife = 4;
            m_fLifeVar = 1;
            m_fAngle = 90;
            m_fAngleVar = 360;
            m_fStartSize = 35;
            m_fStartSizeVar = 10;
            m_fEndSize = ParticleStartSizeEqualToEndSize;
            m_fEndSizeVar = 0;
            m_fStartSpin = 0;
            m_fStartSpinVar = 0;
            m_fEndSpin = 0;
            m_fEndSpinVar = 0;
            m_ptPos = Point.Empty;
            m_ptPosVar = new Point(0, 0);
            m_clStartColor = Color.FromArgb(30, 63, 193);
            m_clStartColorVar = Color.FromArgb(0, 0, 0, 0);
            m_clEndColor = Color.FromArgb(0, 0, 0);
            m_clEndColorVar = Color.FromArgb(0, 0, 0, 0);

            m_peEmitterMode = ParticleMode.Gravity;
            m_ptPosType = PositionType.Free;
            this.TotalParticles = 200;
            m_fEmissionRate = 50;
            m_fDuration = ParticleDurationInfinity;
        }

        /// <summary>
        /// 初始雪花效果。
        /// </summary>
        public void InitSnowEffect()
        {
            m_fGravityX = 0;
            m_fGravityY = 10;
            m_fSpeed = 130;
            m_fSpeedVar = 30;
            m_fTangentialAccel = 0;
            m_fTangentialAccelVar = 1;
            m_fRadialAccel = 0;
            m_fRadialAccelVar = 1;
            m_fRotationIsDir = false;

            m_fStartRadius = 0;
            m_fStartRadiusVar = 0;
            m_fEndRadius = 0;
            m_fEndRadiusVar = 0;
            m_fRotatePerSecond = 0;
            m_fRotatePerSecondVar = 0;

            m_fLife = 2;
            m_fLifeVar = 0.5f;
            m_fAngle = -90;
            m_fAngleVar = 5;
            m_fStartSize = 10;
            m_fStartSizeVar = 5;
            m_fEndSize = ParticleStartSizeEqualToEndSize;
            m_fEndSizeVar = 0;
            m_fStartSpin = 0;
            m_fStartSpinVar = 0;
            m_fEndSpin = 0;
            m_fEndSpinVar = 0;
            m_ptPos = Point.Empty;
            m_ptPosVar = new Point(200, 0);
            m_clStartColor = Color.FromArgb(229, 229, 229);
            m_clStartColorVar = Color.FromArgb(0, 0, 0, 25);
            m_clEndColor = Color.FromArgb(0, 255, 255, 255);
            m_clEndColorVar = Color.FromArgb(0, 0, 0, 0);

            m_peEmitterMode = ParticleMode.Gravity;
            m_ptPosType = PositionType.Free;
            this.TotalParticles = 700;
            m_fEmissionRate = 200;
            m_fDuration = ParticleDurationInfinity;
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 粒子系统的随机数生成器。
        /// </summary>
        private static Random m_rdRand = null;

        #region 重力模式=====================================================================================

        private Single m_fGravityX = 0;
        private Single m_fGravityY = 0;
        private Single m_fSpeed = 0;
        private Single m_fSpeedVar = 0;
        private Single m_fTangentialAccel = 0;
        private Single m_fTangentialAccelVar = 0;
        private Single m_fRadialAccel = 0;
        private Single m_fRadialAccelVar = 0;
        private Boolean m_fRotationIsDir = false;

        #endregion

        #region 半径模式=====================================================================================

        private Single m_fStartRadius = 0;
        private Single m_fStartRadiusVar = 0;
        private Single m_fEndRadius = 0;
        private Single m_fEndRadiusVar = 0;
        private Single m_fRotatePerSecond = 0;
        private Single m_fRotatePerSecondVar = 0;

        #endregion

        #region 通用属性=====================================================================================

        private Single m_fLife = 0;
        private Single m_fLifeVar = 0;
        private Single m_fAngle = 0;
        private Single m_fAngleVar = 0;
        private Single m_fStartSize = 0;
        private Single m_fStartSizeVar = 0;
        private Single m_fEndSize = 0;
        private Single m_fEndSizeVar = 0;
        private Single m_fStartSpin = 0;
        private Single m_fStartSpinVar = 0;
        private Single m_fEndSpin = 0;
        private Single m_fEndSpinVar = 0;
        private Point m_ptPos = Point.Empty;
        private Point m_ptPosVar = Point.Empty;
        private Color m_clStartColor = Color.Empty;
        private Color m_clStartColorVar = Color.Empty;
        private Color m_clEndColor = Color.Empty;
        private Color m_clEndColorVar = Color.Empty;

        #endregion

        #region 基本属性=====================================================================================

        /// <summary>
        /// 效果所使用的粒子图像。
        /// </summary>
        private Bitmap m_bmpImage = null;

        /// <summary>
        /// GL材质ID。
        /// </summary>
        private UInt32 m_iTexID = 0;

        /// <summary>
        /// 图像名称。
        /// </summary>
        private String m_strImageName = String.Empty;

        /// <summary>
        /// 发射的粒子轨迹模式。
        /// </summary>
        private ParticleMode m_peEmitterMode = ParticleMode.Gravity;

        /// <summary>
        /// 发射出去后的粒子位置与粒子系统的关系。
        /// </summary>
        private PositionType m_ptPosType = PositionType.Free;

        /// <summary>
        /// 粒子最大数量。
        /// </summary>
        private Int32 m_iTotalParticles = 0;

        /// <summary>
        /// 粒子发射率。
        /// </summary>
        private Single m_fEmissionRate = 0;

        /// <summary>
        /// 粒子发射持续的时间。
        /// </summary>
        private Single m_fDuration = 0;

        /// <summary>
        /// 播放延时。
        /// </summary>
        private Single m_fDelay = 0;

        /// <summary>
        /// 水平缩放比例。
        /// </summary>
        private Single m_fScaleX = 1.0f;

        /// <summary>
        /// 竖直缩放比例。
        /// </summary>
        private Single m_fScaleY = 1.0f;

        #endregion

        /// <summary>
        /// 效果名称。
        /// </summary>
        private String m_strName = String.Empty;

        /// <summary>
        /// 效果是否显示。
        /// </summary>
        private Boolean m_bVisible = false;

        /// <summary>
        /// 所从属的粒子系统。
        /// </summary>
        private ParticleSystem m_psBelong = null;

        /// <summary>
        /// 发射器是否还继续发射。
        /// </summary>
        private Boolean m_bIsActive = false;

        /// <summary>
        /// 是否开始喷发。
        /// </summary>
        private Boolean m_bEmit = false;

        /// <summary>
        /// 延时计数。
        /// </summary>
        private Single m_fDelayCt = 0;

        /// <summary>
        /// 当前的粒子数量。
        /// </summary>
        private Int32 m_iParticleCount = 0;

        /// <summary>
        /// 效果喷发的时间。
        /// </summary>
        private Single m_fElapsed = 0;

        /// <summary>
        /// 发射计数。
        /// </summary>
        private Single m_fEmitCounter = 0;

        /// <summary>
        /// 粒子缓存。
        /// </summary>
        private Particle[] m_arParticles = null;

        #endregion
    }
}