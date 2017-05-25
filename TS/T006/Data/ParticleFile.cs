using System;
using System.IO;
using System.Xml;
using System.Drawing;
using T006.Data.Particle;
using XuXiang.ClassLibrary;

namespace T006.Data
{
    /// <summary>
    /// 界面文件。
    /// </summary>
    public class ParticleFile : ResourceFile
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 创建一个粒子文件。
        /// </summary>
        /// <param name="strName">粒子名称。</param>
        /// <param name="strFolder">要保存到的文件夹。</param>
        /// <param name="id">粒子系统ID。</param>
        /// <param name="ename">效果名称。</param>
        /// <param name="eimage">效果图像。</param>
        /// <param name="type">效果类型。</param>
        /// <returns>返回0创建成功。</returns>
        public static Int32 CreateParticleFile(String strName, String strFolder, String id, String ename, String eimage, EffectType type)
        {
            try
            {
                String strFileName = strFolder + "\\" + strName + ProjectManager.NAME_EXT_PARTICLE_EDIT;
                XmlDocument xmlDoc = new XmlDocument(); //建立XmlDomcument对象
                XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);    //Xml Declaration(Xml声明)
                ParticleSystem ps = new ParticleSystem(id);
                ParticleEffect pe = new ParticleEffect(ps);
                pe.Name = ename;
                pe.Image = new Bitmap(ProjectManager.Project.AssetsFolder + eimage);
                pe.ImageName = eimage;
                pe.Visible = true;
                pe.InitPreinstallEffect(type);
                pe.ResetEffect();
                ps.Effects.Add(pe);
                ps.Position = new Point(ProjectManager.Project.SceneWidth / 2, ProjectManager.Project.SceneHeight / 2);
                xmlDoc.AppendChild(Declaration);
                xmlDoc.AppendChild(ps.GetXmlNode(xmlDoc));
                xmlDoc.Save(strFileName);
            }
            catch
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 加载界面文件。
        /// </summary>
        /// <param name="strFileName">界面文件路径。</param>
        /// <returns>返回加载的界面文件，加载失败则返回null。</returns>
        public static ParticleFile LoadFromFile(String strFileName)
        {
            return new ParticleFile(strFileName);
        }

        /// <summary>
        /// 设置文件修改标记。
        /// </summary>
        /// <param name="bAmend">是否已修改。</param>
        public void SetAmend(Boolean bAmend)
        {
            this.m_bAmend = bAmend;
            if (bAmend)
            {
                this.RebuildShow();
                this.OnFileAmend(new EventArgs());
            }
        }

        /// <summary>
        /// 更新粒子系统文件。
        /// </summary>
        /// <param name="dt"></param>
        public void Update(Single dt)
        {
            m_psEdit.Update(dt);
            //RebuildShow();
        }

        /// <summary>
        /// 设置粒子系统属性。
        /// </summary>
        /// <param name="id">粒子系统ID。</param>
        public void SetParticleProperty(String id)
        {
            this.m_psEdit.ID = id;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 添加粒子效果到末尾。
        /// </summary>
        /// <param name="par">要添加的粒子效果。</param>
        public void AddEffect(ParticleEffect par)
        {
            this.m_psEdit.Effects.Add(par);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 添加效果到指定位置。
        /// </summary>
        /// <param name="index">要添加到该位置的效果后面。</param>
        /// <param name="par">要添加的粒子效果。</param>
        public void AddEffect(Int32 index, ParticleEffect par)
        {
            this.m_psEdit.Effects.Insert(index+1, par);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置效果属性。
        /// </summary>
        /// <param name="index">效果的索引。</param>
        /// <param name="name">效果名称。</param>
        /// <param name="img">粒子图像路径。</param>
        public void SetEffectProperty(Int32 index, String name, String img)
        {
            ParticleEffect eft = this.m_psEdit.Effects[index];
            eft.Name = name;
            eft.Image = new Bitmap(ProjectManager.Project.AssetsFolder + img);
            eft.ImageName = img;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置效果是否显示。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="v">是否显示。</param>
        public void SetEffectVisible(Int32 index, Boolean v)
        {
            this.m_psEdit.Effects[index].Visible = v;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 删除效果。
        /// </summary>
        /// <param name="index">要删除的效果索引。</param>
        public void DeleteEffect(Int32 index)
        {
            this.m_psEdit.Effects.RemoveAt(index);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置粒子系统位置。
        /// </summary>
        /// <param name="pt">新的位置</param>
        public void SetSystemPosition(Point pt)
        {
            this.m_psEdit.Position = pt;
            this.SetAmend(true);
        }

        /// <summary>
        /// 移动粒子系统。
        /// </summary>
        /// <param name="x">水平移动量。</param>
        /// <param name="y">竖直移动量。</param>
        public void MoveSystem(Int32 x, Int32 y)
        {
            this.m_psEdit.Move(x, y);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 移动效果层次，将一个效果移动到另一个效果之前。
        /// </summary>
        /// <param name="mv">要移动的效果索引。</param>
        /// <param name="target">要移动的目标索引。</param>
        public void MoveEffectLayer(Int32 mv, Int32 target)
        {
            ParticleEffect eft = this.m_psEdit.Effects[mv];
            this.m_psEdit.Effects.RemoveAt(mv);
            this.m_psEdit.Effects.Insert(target > mv ? target - 1 : target, eft);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 移动效果的位置。
        /// </summary>
        /// <param name="index">要移动的效果索引。</param>
        /// <param name="x">水平移动量。</param>
        /// <param name="y">竖直移动量。</param>
        public void MoveEffectPosition(Int32 index, Int32 x, Int32 y)
        {
            this.m_psEdit.Effects[index].Move(x, y);
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置效果的名称。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="name">效果名称。</param>
        public void SetEffectName(Int32 index, String name)
        {
            this.m_psEdit.Effects[index].Name = name;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的图像路径。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="file">效果图像文件路径。</param>
        public void SetEffectImage(Int32 index, String file)
        {
            this.m_psEdit.Effects[index].ImageName = file;
            this.m_psEdit.Effects[index].Image = new Bitmap(ProjectManager.Project.AssetsFolder + file);
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果位置。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="pos">设置的位置。</param>
        public void SetEffectPosition(Int32 index, Point pos)
        {
            this.m_psEdit.Effects[index].Position = pos;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果位置波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="pos">设置的位置波动值。</param>
        public void SetEffectPositionVar(Int32 index, Point pos)
        {
            this.m_psEdit.Effects[index].PositionVar = pos;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的水平缩放。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="scale">缩放值。</param>
        public void SetEffectScaleX(Int32 index, float scale)
        {
            this.m_psEdit.Effects[index].ScaleX = scale;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的竖直缩放。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="scale">缩放值。</param>
        public void SetEffectScaleY(Int32 index, float scale)
        {
            this.m_psEdit.Effects[index].ScaleY = scale;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射角度。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="angle">发射角度。</param>
        public void SetEffectAngle(Int32 index, float angle)
        {
            this.m_psEdit.Effects[index].Angle = angle;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射角度波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="angle">发射角度波动。</param>
        public void SetEffectAngleVar(Int32 index, float angle)
        {
            this.m_psEdit.Effects[index].AngleVar = angle;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的粒子生命。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="life">生命值。</param>
        public void SetEffectLife(Int32 index, float life)
        {
            this.m_psEdit.Effects[index].Life = life;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的粒子生命波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="life">生命波动值。</param>
        public void SetEffectLifeVar(Int32 index, float life)
        {
            this.m_psEdit.Effects[index].LifeVar = life;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的延时。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="delay">延时。</param>
        public void SetEffectDelay(Int32 index, float delay)
        {
            this.m_psEdit.Effects[index].Delay = delay;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的喷发持续时间。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="duration">喷发持续时间。</param>
        public void SetEffectDuration(Int32 index, float duration)
        {
            this.m_psEdit.Effects[index].Duration = duration;
            this.SetAmend(true);
        }
        
        /// <summary>
        /// 设置效果的位置类型。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="type">位置类型。</param>
        public void SetEffectPositionType(Int32 index, PositionType type)
        {
            this.m_psEdit.Effects[index].PosType = type;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射速率。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="rate">发射速率。</param>
        public void SetEffectEmissionRate(Int32 index, float rate)
        {
            this.m_psEdit.Effects[index].EmissionRate = rate;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的最大粒子数量。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="max">最大粒子数量。</param>
        public void SetEffectTotalParticles(Int32 index, int max)
        {
            this.m_psEdit.Effects[index].TotalParticles = max;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射模式。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="mode">发射模式。</param>
        public void SetEffectEmitterMode(Int32 index, ParticleMode mode)
        {
            this.m_psEdit.Effects[index].EmitterMode = mode;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始颜色。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="color">起始颜色。</param>
        public void SetEffectStartColor(Int32 index, Color color)
        {
            this.m_psEdit.Effects[index].StartColor = color;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始颜色波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="color">起始颜色波动。</param>
        public void SetEffectStartColorVar(Int32 index, Color color)
        {
            this.m_psEdit.Effects[index].StartColorVar = color;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束颜色。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="color">结束颜色。</param>
        public void SetEffectEndColor(Int32 index, Color color)
        {
            this.m_psEdit.Effects[index].EndColor = color;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束颜色波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="color">结束颜色波动。</param>
        public void SetEffectEndColorVar(Int32 index, Color color)
        {
            this.m_psEdit.Effects[index].EndColorVar = color;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始粒子大小。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="size">起始粒子大小。</param>
        public void SetEffectStartSize(Int32 index, Single size)
        {
            this.m_psEdit.Effects[index].StartSize = size;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始粒子大小波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="size">起始粒子大小波动。</param>
        public void SetEffectStartSizeVar(Int32 index, Single size)
        {
            this.m_psEdit.Effects[index].StartSizeVar = size;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束粒子大小。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="size">结束粒子大小。</param>
        public void SetEffectEndSize(Int32 index, Single size)
        {
            this.m_psEdit.Effects[index].EndSize = size;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束粒子大小波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="size">结束粒子大小波动。</param>
        public void SetEffectEndSizeVar(Int32 index, Single size)
        {
            this.m_psEdit.Effects[index].EndSizeVar = size;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始粒子自转。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="spin">起始粒子自转。</param>
        public void SetEffectStartSpin(Int32 index, Single spin)
        {
            this.m_psEdit.Effects[index].StartSpin = spin;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始粒子自转波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="spin">起始粒子自转波动。</param>
        public void SetEffectStartSpinVar(Int32 index, Single spin)
        {
            this.m_psEdit.Effects[index].StartSpinVar = spin;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束粒子自转。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="spin">结束粒子自转。</param>
        public void SetEffectEndSpin(Int32 index, Single spin)
        {
            this.m_psEdit.Effects[index].EndSpin = spin;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束粒子自转波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="spin">结束粒子自转波动。</param>
        public void SetEffectEndSpinVar(Int32 index, Single spin)
        {
            this.m_psEdit.Effects[index].EndSpinVar = spin;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的重力场。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="g">重力场。</param>
        public void SetEffectGravity(Int32 index, Point g)
        {
            this.m_psEdit.Effects[index].GravityX = g.X;
            this.m_psEdit.Effects[index].GravityY = g.Y;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射速度。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="speed">发射速度。</param>
        public void SetEffectSpeed(Int32 index, Single speed)
        {
            this.m_psEdit.Effects[index].Speed = speed;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的发射速度波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="speed">发射速度波动。</param>
        public void SetEffectSpeedVar(Int32 index, Single speed)
        {
            this.m_psEdit.Effects[index].SpeedVar = speed;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的径向加速。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="accel">径向加速。</param>
        public void SetEffectRadialAccel(Int32 index, Single accel)
        {
            this.m_psEdit.Effects[index].RadialAccel = accel;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的径向加速波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="accel">径向加速波动。</param>
        public void SetEffectRadialAccelVar(Int32 index, Single accel)
        {
            this.m_psEdit.Effects[index].RadialAccelVar = accel;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的切向加速。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="accel">切向加速。</param>
        public void SetEffectTangentialAccel(Int32 index, Single accel)
        {
            this.m_psEdit.Effects[index].TangentialAccel = accel;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的切向加速波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="accel">切向加速波动。</param>
        public void SetEffectTangentialAccelVar(Int32 index, Single accel)
        {
            this.m_psEdit.Effects[index].TangentialAccelVar = accel;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始半径。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="radius">起始半径。</param>
        public void SetEffectStartRadius(Int32 index, Single radius)
        {
            this.m_psEdit.Effects[index].StartRadius = radius;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的起始半径。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="radius">起始半径波动。</param>
        public void SetEffectStartRadiusVar(Int32 index, Single radius)
        {
            this.m_psEdit.Effects[index].StartRadiusVar = radius;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束半径。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="radius">结束半径。</param>
        public void SetEffectEndRadius(Int32 index, Single radius)
        {
            this.m_psEdit.Effects[index].EndRadius = radius;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的结束半径。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="radius">结束半径波动。</param>
        public void SetEffectEndRadiusVar(Int32 index, Single radius)
        {
            this.m_psEdit.Effects[index].EndRadiusVar = radius;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的转速。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="rotate">转速。</param>
        public void SetEffectRotate(Int32 index, Single rotate)
        {
            this.m_psEdit.Effects[index].RotatePerSecond = rotate;
            this.SetAmend(true);
        }

        /// <summary>
        /// 设置效果的转速波动。
        /// </summary>
        /// <param name="index">效果索引。</param>
        /// <param name="rotate">转速波动。</param>
        public void SetEffectRotateVar(Int32 index, Single rotate)
        {
            this.m_psEdit.Effects[index].RotatePerSecondVar = rotate;
            this.SetAmend(true);
        }

        /// <summary>
        /// 生成粒子文件。
        /// </summary>
        public void BuildParticleFile()
        {
            String output = ProjectManager.Project.ParticleBuildFolder + m_psEdit.ID + ProjectManager.NAME_EXT_PARTICLE_BUILD;
            FileStream fs = new FileStream(output, FileMode.Create);
            DataUtil.WriteInt32(fs, VERSION);
            this.m_psEdit.WriteToStream(fs);
            fs.Flush();
            fs.Dispose();
            fs = null;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置背景图像图像。
        /// </summary>
        public String BackImageName
        {
            get
            {
                return this.m_psEdit.BackImageName;
            }
            set
            {
                this.m_psEdit.BackImageName = value;
                this.m_bAmend = true;
                this.OnFileAmend(new EventArgs());
            }
        }

        /// <summary>
        /// 获取在编辑的粒子系统。
        /// </summary>
        public ParticleSystem EditParticle
        {
            get
            {
                return this.m_psEdit;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="strFileName">文件路径。</param>
        protected ParticleFile(String strFileName)
            : base(strFileName, ProjectManager.TYPE_RESOURCE_PARTICLE)
        {
            m_psEdit = ParticleSystem.LoadFromXmlFile(strFileName);
            //this.m_bmpDisplayBuffer = new System.Drawing.Bitmap(ProjectManager.Project.SceneWidth, ProjectManager.Project.SceneHeight);
            RebuildShow();
        }

        /// <summary>
        /// 已重载。将地图文件保存到硬盘。
        /// </summary>
        protected override void SaveFileToDisk()
        {
            base.SaveFileToDisk();

            //String strFileName = this.m_strFileName;
            XmlDocument xmlDoc = new XmlDocument(); //建立XmlDomcument对象
            XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);    //Xml Declaration(Xml声明)
            xmlDoc.AppendChild(Declaration);

            XmlNode xmlParticle = m_psEdit.GetXmlNode(xmlDoc);
            xmlParticle.Attributes.Append(xmlDoc.CreateAttribute("Version")).InnerText = VERSION.ToString();
            xmlDoc.AppendChild(xmlParticle);
            xmlDoc.Save(this.m_strFileName);
        }

        /// <summary>
        /// 销毁贴图文件。
        /// </summary>
        protected override void DestroyFile()
        {
            base.DestroyFile();
        }

        /// <summary>
        /// 重新构建显示的内容。
        /// </summary>
        protected void RebuildShow()
        {
            //Graphics g = Graphics.FromImage(this.m_bmpDisplayBuffer);
            //g.Clear(System.Drawing.Color.Transparent);
            //m_psEdit.Paint(g);
        }

        #endregion

        #region 数据变量=====================================================================================

        //粒子系统文件版本
        public const Int32 VERSION = 1;

        /// <summary>
        /// 要编辑的粒子系统。
        /// </summary>
        private ParticleSystem m_psEdit = null;

        #endregion
    }
}
