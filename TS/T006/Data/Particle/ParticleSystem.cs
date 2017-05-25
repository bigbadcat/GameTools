using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;
using System.IO;
using CsGL.OpenGL;
using System.ComponentModel;
using T006.Common;
using XuXiang.ClassLibrary;

namespace T006.Data.Particle
{
    /// <summary>
    /// 复合粒子系统，里面保存了一组粒子效果。
    /// </summary>
    public class ParticleSystem
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数，创建一个粒子系统。
        /// </summary>
        /// <param name="id">粒子系统编号。</param>
        public ParticleSystem(String id)
        {
            m_strID = id;
            m_lstEffects = new List<ParticleEffect>();
        }

        /// <summary>
        /// 从XML文件加载粒子系统对象。
        /// </summary>
        /// <param name="file">XML文件路径。</param>
        /// <returns>粒子系统对象。</returns>
        public static ParticleSystem LoadFromXmlFile(String file)
        {
            //打开XML文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);

            //读入界面基本信息
            
            XmlNode xmlParticle = xmlDoc.SelectSingleNode("ParticleSystem");
            String strBack = XmlUtil.GetAttribute(xmlParticle, "BackImage");
            String id = xmlParticle.Attributes["ID"].InnerText;
            Point pos = DataUtil.ParsePoint(xmlParticle.Attributes["Position"].InnerText);
            ParticleSystem par = new ParticleSystem(id);
            par.BackImageName = strBack;
            par.m_ptPosition = pos;

            //粒子列表
            foreach (XmlNode node in xmlParticle.ChildNodes)
            {
                par.m_lstEffects.Add(ParticleEffect.LoadFromXmlNode(par, node));
            }
            return par;
        }

        /// <summary>
        /// 将界面返回成XML的粒子系统节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档。</param>
        /// <returns>XML的Interface节点</returns>
        public XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            //Interface根节点
            XmlNode xmlParticle = xmlDoc.CreateNode(XmlNodeType.Element, "ParticleSystem", "");
            xmlParticle.Attributes.Append(xmlDoc.CreateAttribute("ID")).InnerText = m_strID;
            xmlParticle.Attributes.Append(xmlDoc.CreateAttribute("Position")).InnerText = DataUtil.ToStringValue(this.m_ptPosition);
            xmlParticle.Attributes.Append(xmlDoc.CreateAttribute("BackImage")).InnerText = m_strBackImageName;
            xmlDoc.AppendChild(xmlParticle);

            //效果列表
            foreach (ParticleEffect eft in this.m_lstEffects)
            {
                xmlParticle.AppendChild(eft.GetXmlNode(xmlDoc));
            }

            return xmlParticle;
        }

        /// <summary>
        /// 将用粒子系统入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_lstEffects.Count));
            foreach (ParticleEffect pe in this.m_lstEffects)
            {
                pe.WriteToStream(stream);
            }
        }

        /// <summary>
        /// 重置粒子效果，开始播放。
        /// </summary>
        public void ResetSystem()
        {
            foreach (ParticleEffect pe in this.m_lstEffects)
            {
                pe.ResetEffect();
            }
        }

        /// <summary>
        /// 更新粒子效果。
        /// </summary>
        /// <param name="dt">粒子效果经过的时间。</param>
        public void Update(Single dt)
        {
            foreach (ParticleEffect pe in this.m_lstEffects)
            {
                pe.Update(dt);
            }
        }

        /// <summary>
        /// 绘制粒子效果。
        /// </summary>
        public void Paint()
        {
            //绘制交叉线条
            GL.glColor4ub(0, 255, 0, 255);
            GL.glBegin(GL.GL_LINES);
                GL.glVertex2i(0, this.m_ptPosition.Y);
                GL.glVertex2i(ProjectManager.Project.SceneWidth, this.m_ptPosition.Y);
                GL.glVertex2i(this.m_ptPosition.X, 0);
                GL.glVertex2i(this.m_ptPosition.X, ProjectManager.Project.SceneHeight);
            GL.glEnd();

            foreach (ParticleEffect pe in this.m_lstEffects)
            {
                pe.Paint(m_ptPosition);
            }
        }

        /// <summary>
        /// 移动粒子效果。
        /// </summary>
        /// <param name="mx">水平方向的移动量。</param>
        /// <param name="my">竖直方向的移动量。</param>
        public void Move(Int32 mx, Int32 my)
        {
            this.m_ptPosition.X = this.m_ptPosition.X + mx;
            this.m_ptPosition.Y = this.m_ptPosition.Y + my;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置粒子系统编号。
        /// </summary>
        [Browsable(false)]
        public String ID
        {
            get
            {
                return this.m_strID;
            }
            set
            {
                this.m_strID = value;
            }
        }

        /// <summary>
        /// 获取或设置背景图像图像。
        /// </summary>
        public String BackImageName
        {
            get
            {
                return this.m_strBackImageName;
            }
            set
            {
                this.m_strBackImageName = value;
            }
        }

        /// <summary>
        /// 获取效果列表。
        /// </summary>
        [Browsable(false)]
        public List<ParticleEffect> Effects
        {
            get
            {
                return this.m_lstEffects;
            }
        }

        /// <summary>
        /// 获取或设置粒子系统的位置。
        /// </summary>
        [CategoryAttribute("基本"), DescriptionAttribute("粒子系统在编辑器中的坐标。")]
        public Point Position
        {
            get
            {
                return this.m_ptPosition;
            }
            set
            {
                this.m_ptPosition = value;
            }
        }

        /// <summary>
        /// 获取粒子数量。
        /// </summary>
        [Browsable(false)]
        public Int32 ParticleNumber
        {
            get
            {
                Int32 number = 0;
                foreach (ParticleEffect eft in this.m_lstEffects)
                {
                    number += eft.ParticleCount;
                }
                return number;
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 粒子系统编号。
        /// </summary>
        private String m_strID = String.Empty;

        /// <summary>
        /// 粒子效果列表。
        /// </summary>
        private List<ParticleEffect> m_lstEffects = null;

        /// <summary>
        /// 粒子系统的位置。
        /// </summary>
        private Point m_ptPosition = Point.Empty;

        /// <summary>
        /// 背景图像。
        /// </summary>
        private String m_strBackImageName = String.Empty;

        #endregion
    }
}
