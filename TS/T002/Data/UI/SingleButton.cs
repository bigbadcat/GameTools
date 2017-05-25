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
    /// 简单按钮类，可以显示一行文本，用来响应一次点击。
    /// </summary>
    public class SingleButton : Button
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public SingleButton(UserInterface ui)
            : base(ui)
        {
            //普通文本
            this.m_lbNormalText = new Label(ui);
            this.m_lbNormalText.Align = Align.Center;
            this.m_lbNormalText.LabelType = LabelType.Stroke;

            //按下文本
            this.m_lbDownText = new Label(ui);
            this.m_lbDownText.Align = Align.Center;
            this.m_lbDownText.LabelType = LabelType.Stroke;

            //不可用文本
            this.m_lbDisableText = new Label(ui);
            this.m_lbDisableText.Align = Align.Center;
            this.m_lbDisableText.LabelType = LabelType.Stroke;
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[普通按钮]");
        }

        /// <summary>
        /// 已重载。绘制普通按钮。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Platform.Canvas c, Point p)
        {
            if (!this.Visible)
            {
                return;
            }

            Point cp = new Point(this.X + p.X + this.m_ptTextOffset.X, this.Y + p.Y + this.m_ptTextOffset.Y);
            base.Paint(c, p);
            if (this.Enable)
            {
                if (this.m_bsState == ButtonState.Normal)
                {
                    this.m_lbNormalText.Paint(c, cp);
                }
                else if (this.m_bsState == ButtonState.Down)
                {
                    Point dp = new Point(cp.X + this.m_ptTextDownOffset.X, cp.Y + this.m_ptTextDownOffset.Y);
                    this.m_lbDownText.Paint(c, dp);
                }
            }
            else
            {
                this.m_lbDisableText.Paint(c, cp);
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_SINGLEBUTTON, "");
            this.SetXmlNodeAttribute(xmlDoc, xmlNode);
            return xmlNode;
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strText = XmlUtil.GetAttribute(xmlNode, "Text");
            String strWordSize = XmlUtil.GetAttribute(xmlNode, "WordSize");
            String strTextNormalColor = XmlUtil.GetAttribute(xmlNode, "TextNormalColor");
            String strTextDownColor = XmlUtil.GetAttribute(xmlNode, "TextDownColor");
            String strTextDisableColor = XmlUtil.GetAttribute(xmlNode, "TextDisableColor");
            String strTextNormalStrokeColor = XmlUtil.GetAttribute(xmlNode, "TextNormalStrokeColor");
            String strTextDownStrokeColor = XmlUtil.GetAttribute(xmlNode, "TextDownStrokeColor");
            String strTextDisableStrokeColor = XmlUtil.GetAttribute(xmlNode, "TextDisableStrokeColor");
            String strTextOffset = XmlUtil.GetAttribute(xmlNode, "TextOffset");
            String strTextDownOffset = XmlUtil.GetAttribute(xmlNode, "TextDownOffset");

            this.Text = strText;
            this.WordSize = strWordSize.Equals(String.Empty) ? 22 : Single.Parse(strWordSize);
            this.TextNormalColor = strTextNormalColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextNormalColor);
            this.TextDownColor = strTextDownColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextDownColor);
            this.TextDisableColor = strTextDisableColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextDisableColor);
            this.TextNormalStrokeColor = strTextDownColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextNormalStrokeColor);
            this.TextDownStrokeColor = strTextDownColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextDownStrokeColor);
            this.TextDisableStrokeColor = strTextDownColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strTextDisableStrokeColor);
            this.TextOffset = strTextOffset.Equals(String.Empty) ? Point.Empty : DataUtil.ParsePoint(strTextOffset);
            this.TextDownOffset = strTextDownOffset.Equals(String.Empty) ? Point.Empty : DataUtil.ParsePoint(strTextDownOffset);
            this.OnSizeChanged();
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_SINGLEBUTTON));
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(this.Text));
            DataUtil.WriteSingle(stream, this.WordSize);
            DataUtil.WriteColor(stream, TextNormalColor);
            DataUtil.WriteColor(stream, TextDownColor);
            DataUtil.WriteColor(stream, TextDisableColor);
            DataUtil.WriteColor(stream, TextNormalStrokeColor);
            DataUtil.WriteColor(stream, TextDownStrokeColor);
            DataUtil.WriteColor(stream, TextDisableStrokeColor);
            DataUtil.WritePoint(stream, TextOffset);
            DataUtil.WritePoint(stream, TextDownOffset);
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "BTN_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置按钮上的文本。
        /// </summary>
        public String Text
        {
            get
            {
                return this.m_lbNormalText.Text;
            }
            set
            {
                this.m_lbNormalText.Text = value;
                this.m_lbDownText.Text = value;
                this.m_lbDisableText.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置按钮文本字号。
        /// </summary>
        public Single WordSize
        {
            get
            {
                return this.m_lbNormalText.WordSize;
            }
            set
            {
                this.m_lbNormalText.WordSize = value;
                this.m_lbDownText.WordSize = value;
                this.m_lbDisableText.WordSize = value;
            }
        }

        /// <summary>
        /// 获取或设置普通状态文本颜色。
        /// </summary>
        public Color TextNormalColor
        {
            get
            {
                return this.m_lbNormalText.TextColor;
            }
            set
            {
                this.m_lbNormalText.TextColor = value;
            }
        }

        /// <summary>
        /// 获取或设置按下状态文本颜色。
        /// </summary>
        public Color TextDownColor
        {
            get
            {
                return this.m_lbDownText.TextColor;
            }
            set
            {
                this.m_lbDownText.TextColor = value;
            }
        }

        /// <summary>
        /// 获取或设置不可用状态文本颜色。
        /// </summary>
        public Color TextDisableColor
        {
            get
            {
                return this.m_lbDisableText.TextColor;
            }
            set
            {
                this.m_lbDisableText.TextColor = value;
            }
        }

        /// <summary>
        /// 获取或设置普通状态文本描边颜色。
        /// </summary>
        public Color TextNormalStrokeColor
        {
            get
            {
                return this.m_lbNormalText.StrokeColor;
            }
            set
            {
                this.m_lbNormalText.StrokeColor = value;
            }
        }

        /// <summary>
        /// 获取或设置按下状态文本描边颜色。
        /// </summary>
        public Color TextDownStrokeColor
        {
            get
            {
                return this.m_lbDownText.StrokeColor;
            }
            set
            {
                this.m_lbDownText.StrokeColor = value;
            }
        }

        /// <summary>
        /// 获取或设置不可用状态文本描边颜色。
        /// </summary>
        public Color TextDisableStrokeColor
        {
            get
            {
                return this.m_lbDisableText.StrokeColor;
            }
            set
            {
                this.m_lbDisableText.StrokeColor = value;
            }
        }

        /// <summary>
        /// 获取或设置按钮文本偏移。
        /// </summary>
        public Point TextOffset
        {
            get
            {
                return this.m_ptTextOffset;
            }
            set
            {
                this.m_ptTextOffset = value;
            }
        }

        /// <summary>
        /// 获取或设置文本按下时偏移。
        /// </summary>
        public Point TextDownOffset
        {
            get
            {
                return this.m_ptTextDownOffset;
            }
            set
            {
                this.m_ptTextDownOffset = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 已重载。按钮的尺寸发送改变。
        /// </summary>
        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            this.m_lbNormalText.Size = this.Size;
            this.m_lbDownText.Size = this.Size;
            this.m_lbDisableText.Size = this.Size;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Text")).InnerText = this.Text;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("WordSize")).InnerText = this.WordSize.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextNormalColor")).InnerText = DataUtil.ToStringValue(this.TextNormalColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextDownColor")).InnerText = DataUtil.ToStringValue(this.TextDownColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextDisableColor")).InnerText = DataUtil.ToStringValue(this.TextDisableColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextNormalStrokeColor")).InnerText = DataUtil.ToStringValue(this.TextNormalStrokeColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextDownStrokeColor")).InnerText = DataUtil.ToStringValue(this.TextDownStrokeColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextDisableStrokeColor")).InnerText = DataUtil.ToStringValue(this.TextDisableStrokeColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextOffset")).InnerText = DataUtil.ToStringValue(this.TextOffset);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextDownOffset")).InnerText = DataUtil.ToStringValue(this.TextDownOffset);
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 显示普通状态的文本。
        /// </summary>
        private Label m_lbNormalText = null;

        /// <summary>
        /// 显示按下状态的文本。
        /// </summary>
        private Label m_lbDownText = null;

        /// <summary>
        /// 显示不可用状态的文本。
        /// </summary>
        private Label m_lbDisableText = null;

        /// <summary>
        /// 按钮文本偏移。
        /// </summary>
        private Point m_ptTextOffset = Point.Empty;

        /// <summary>
        /// 按钮文本按下偏移。
        /// </summary>
        private Point m_ptTextDownOffset = Point.Empty;

        #endregion
    }
}
