using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using T002.Common;
using T002.Platform;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 标签类，用于显示一行文本。
    /// </summary>
    public class Label : TextControl
    {
        #region 对外操作=====================================================================================
        
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public Label(UserInterface ui)
            : base(ui)
        {
            this.CreateNewTextImage();
        }

        /// <summary>
        /// 已重载。绘制标签。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Platform.Canvas c, Point p)
        {
            if (!this.Visible)
            {
                return;
            }

            Int32 xo = this.X + p.X;
            Int32 yo = this.Y + p.Y;
            Int32 wo = this.Width - this.m_imgBuffer.Width;
            Int32 ho = this.Height - this.m_imgBuffer.Height;
            switch (this.m_aAlign)
            {
                case UI.Align.TopLeft:
                    yo += ho;
                    break;
                case UI.Align.Top:
                    xo += wo >> 1;
                    yo += ho;
                    break;
                case UI.Align.TopRight:
                    xo += wo;
                    yo += ho;
                    break;
                case UI.Align.Left:
                    yo += ho >> 1;
                    break;
                case UI.Align.Center:
                    yo += ho >> 1;
                    xo += wo >> 1;
                    break;
                case UI.Align.Right:
                    yo += ho >> 1;
                    xo += wo;
                    break;
                case UI.Align.BottomLeft:
                    break;
                case UI.Align.Bottom:
                    xo += wo >> 1;
                    break;
                case UI.Align.BottomRight:
                    xo += wo;
                    break;
                default:
                    break;
            }
            base.Paint(c, p);
            c.DrawImage(this.m_imgBuffer, new Point(xo, yo));
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_LABEL, "");
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
            String strAlign = XmlUtil.GetAttribute(xmlNode, "Align");
            String strLabelType = XmlUtil.GetAttribute(xmlNode, "LabelType");
            String strStrokeColor = XmlUtil.GetAttribute(xmlNode, "StrokeColor");

            this.m_aAlign = strAlign.Equals(String.Empty) ? Align.Center : (Align)Int32.Parse(strAlign);
            this.m_ltType = strLabelType.Equals(String.Empty) ? LabelType.Normal : (LabelType)Int32.Parse(strLabelType);
            this.m_cStrokeColor = strStrokeColor == String.Empty ? Color.White : DataUtil.ParseColor(strStrokeColor);
            this.CreateNewTextImage();
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[标签]");
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_LABEL));
            base.WriteToStream(stream);
            stream.WriteByte((Byte)m_aAlign);
            stream.WriteByte((Byte)m_ltType);
            DataUtil.WriteColor(stream, m_cStrokeColor);
        }

        /// <summary>
        /// 获取合适的尺寸，刚好能显示完所有文本。
        /// </summary>
        /// <returns>合适的尺寸。</returns>
        public Size GetSuitableSize()
        {
            return this.m_imgBuffer.Size;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置文本的对齐方式。
        /// </summary>
        public Align Align
        {
            get
            {
                return this.m_aAlign;
            }
            set
            {
                this.m_aAlign = value;
            }
        }

        /// <summary>
        /// 获取或设置标签类型。
        /// </summary>
        public LabelType LabelType
        {
            get
            {
                return this.m_ltType;
            }
            set
            {
                if (this.m_ltType != value)
                {
                    this.m_ltType = value;
                    this.CreateNewTextImage();
                }
            }
        }

        /// <summary>
        /// 获取或设置标签的描边色。
        /// </summary>
        public Color StrokeColor
        {
            get
            {
                return this.m_cStrokeColor;
            }
            set
            {
                if (this.m_cStrokeColor.ToArgb() != value.ToArgb())
                {
                    this.m_cStrokeColor = value;
                    if (this.m_ltType == UI.LabelType.Stroke)
                    {
                        this.CreateNewTextImage();
                    }
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 创建新的文本图像。
        /// </summary>
        protected override void CreateNewTextImage()
        {
            T002.Platform.Image.DeleteImage(this.m_imgBuffer);
            this.m_imgBuffer = null;
            switch (this.m_ltType)
            {
                case LabelType.Normal:
                    this.m_imgBuffer = T002.Platform.Image.GetStringImage(m_strText, m_iWordSize, m_cTextColor);
                    break;
                case LabelType.Stroke:
                    this.m_imgBuffer = T002.Platform.Image.GetStrokeStringImage(m_strText, m_iWordSize, m_cTextColor, m_cStrokeColor);
                    break;
                case LabelType.MultiColor:
                    this.m_imgBuffer = T002.Platform.Image.GetColorStringImage(m_strText, m_iWordSize, m_cTextColor);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Align")).InnerText = ((Int32)m_aAlign).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("LabelType")).InnerText = ((Int32)m_ltType).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("StrokeColor")).InnerText = DataUtil.ToStringValue(m_cStrokeColor);
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 文本对齐方式。
        /// </summary>
        private Align m_aAlign = Align.Left;

        /// <summary>
        /// 标签类型。
        /// </summary>
        private LabelType m_ltType = LabelType.Normal;

        /// <summary>
        /// 文本描边色。
        /// </summary>
        private Color m_cStrokeColor = Color.Transparent;

        #endregion
    }
}
