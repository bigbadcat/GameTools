using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using T002.Common;
using System.IO;
using T002.Platform;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 文本框提供了文本的输入能力。
    /// </summary>
    public class TextBox : TextControl
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public TextBox(UserInterface ui)
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
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_TEXTBOX, "");
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
            String strInputType = XmlUtil.GetAttribute(xmlNode, "InputType");

            this.m_aAlign = strAlign.Equals(String.Empty) ? Align.Center : (Align)Int32.Parse(strAlign);
            this.m_itType = strInputType.Equals(String.Empty) ? InputType.Text : (InputType)Int32.Parse(strInputType);
            this.CreateNewTextImage();
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[文本框]");
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_TEXTBOX));
            base.WriteToStream(stream);
            stream.WriteByte((Byte)m_aAlign);
            stream.WriteByte((Byte)m_itType);
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
        /// 获取或设置输入类型。
        /// </summary>
        public InputType Type
        {
            get
            {
                return this.m_itType;
            }
            set
            {
                if (this.m_itType != value)
                {
                    this.m_itType = value;
                    this.CreateNewTextImage();
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
            if (this.m_itType == InputType.Password)
            {
                String str = new String('*', m_strText.Length);
                this.m_imgBuffer = T002.Platform.Image.GetStringImage(str, m_iWordSize, m_cTextColor);
            }
            else
            {
                this.m_imgBuffer = T002.Platform.Image.GetStringImage(m_strText, m_iWordSize, m_cTextColor);
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
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("InputType")).InnerText = ((Int32)m_itType).ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 文本对齐方式。
        /// </summary>
        private Align m_aAlign = Align.Left;

        /// <summary>
        /// 是否输入密码。
        /// </summary>
        private InputType m_itType = InputType.Text;

        #endregion
    }
}
