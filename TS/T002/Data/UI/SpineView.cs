using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T002.Platform;
using System.Drawing;
using System.Xml;
using T002.Common;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 显示一个精灵。
    /// </summary>
    public class SpineView : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public SpineView(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 已重载。绘制容器。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Canvas c, Point p)
        {
            if (!this.Visible)
            {
                return;
            }
            base.Paint(c, p);
            Int32 x = p.X + this.X;
            Int32 y = p.Y + this.Y;
            Int32 cx = x + (this.Width >> 1);
            Int32 cy = y + (this.Height >> 1);
            c.DrawLine(new Point(x, cy), new Point(x + this.Width, cy), Color.Blue);
            c.DrawLine(new Point(cx, y), new Point(cx, y + this.Height), Color.Blue);
            c.DrawRect(new Rect(cx - 5, cy - 5, 10, 10), Color.Blue);
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_SPINEVIEW, "");
            this.SetXmlNodeAttribute(xmlDoc, xmlNode);
            return xmlNode;
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strSpine = XmlUtil.GetAttribute(xmlNode, "Spine");
            String strAnimationName = XmlUtil.GetAttribute(xmlNode, "AnimationName");
            String strLoop = XmlUtil.GetAttribute(xmlNode, "Loop");
            String strScaleX = XmlUtil.GetAttribute(xmlNode, "ScaleX");
            String strScaleY = XmlUtil.GetAttribute(xmlNode, "ScaleY");

            this.m_strSpineFile = strSpine;
            this.m_strAnimationName = strAnimationName;
            this.m_bLoop = strLoop.Equals(String.Empty) ? false : Boolean.Parse(strLoop);
            this.m_fScaleX = strScaleX.Equals(String.Empty) ? 1.0f : Single.Parse(strScaleX);
            this.m_fScaleY = strScaleY.Equals(String.Empty) ? 1.0f : Single.Parse(strScaleY);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[Spine视图]");
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteInt32(stream, UserInterface.CONTROL_TYPE_ID_SPINEVIEW);
            base.WriteToStream(stream);
            DataUtil.WriteString(stream, m_strSpineFile.Replace("\\", "/"));
            DataUtil.WriteString(stream, m_strAnimationName);
            DataUtil.WriteBoolean(stream, this.m_bLoop);
            DataUtil.WriteSingle(stream, m_fScaleX);
            DataUtil.WriteSingle(stream, m_fScaleY);
        }

        /// <summary>
        /// 获取合适的尺寸。
        /// </summary>
        /// <returns>合适的尺寸。</returns>
        public Size GetSuitableSize()
        {
            return new Size(50, 50);
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "SPV_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置显示的动画文件路径。
        /// </summary>
        public String SpineFile
        {
            get
            {
                return m_strSpineFile;
            }
            set
            {
                m_strSpineFile = value;
            }
        }

        /// <summary>
        /// 获取或设置显示的动画名称。
        /// </summary>
        public String AnimationName
        {
            get
            {
                return m_strAnimationName;
            }
            set
            {
                m_strAnimationName = value;
            }
        }

        /// <summary>
        /// 获取或设置是否循环。
        /// </summary>
        public Boolean Loop
        {
            get
            {
                return this.m_bLoop;
            }
            set
            {
                this.m_bLoop = value;
            }
        }

        /// <summary>
        /// 获取或设置效果的水平缩放。
        /// </summary>
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
        /// 获取或设置效果的竖直缩放。
        /// </summary>
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

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Spine")).InnerText = this.m_strSpineFile;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("AnimationName")).InnerText = this.m_strAnimationName;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Loop")).InnerText = this.m_bLoop.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScaleX")).InnerText = this.m_fScaleX.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScaleY")).InnerText = this.m_fScaleY.ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 动画文件路径。
        /// </summary>
        private String m_strSpineFile = String.Empty;

        /// <summary>
        /// 动画名称。
        /// </summary>
        private String m_strAnimationName = String.Empty;

        /// <summary>
        /// 是否循环。
        /// </summary>
        protected Boolean m_bLoop = false;

        /// <summary>
        /// 水平缩放
        /// </summary>
        private Single m_fScaleX = 1.0f;

        /// <summary>
        /// 竖直缩放。
        /// </summary>
        private Single m_fScaleY = 1.0f;

        #endregion
    }
}
