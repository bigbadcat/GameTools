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
    public class ScrollTable : Table
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        /// <param name="con">表格的单元格原型。</param>
        public ScrollTable(UserInterface ui, Container con)
            : base(ui, con)
        {
            if (con != null)
            {
                con.Parent = this;
            }
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[滑动表格]");
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

            Point cp = new Point(this.X + p.X, this.Y + p.Y);
            base.Paint(c, p);
            c.Save();
            c.SetClip(new Rect(cp, this.Size));
            if (this.m_dDirection == UI.Direction.Horizontal)
            {
                this.PaintHorizontal(c, cp);
            }
            else if (this.m_dDirection == UI.Direction.Vertical)
            {
                this.PaintVertical(c, cp);
            }
            c.Restore();


            //单元格标记，PixelOffsetMode.Half;属性会使像素右下偏移一个像素
            Rect promark = new Rect(cp.X + 1, cp.Y + this.Height - this.Prototype.Height, m_conPrototype.Width - 1, m_conPrototype.Height - 1);
            c.DrawRect(promark, Color.LimeGreen);
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_SCROLLTABLE, "");
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
            String strDirection = XmlUtil.GetAttribute(xmlNode, "Direction");
            String strBasicNumber = XmlUtil.GetAttribute(xmlNode, "BasicNumber");
            this.m_dDirection = strDirection.Equals(String.Empty) ? Direction.Horizontal : (Direction)Int32.Parse(strDirection);
            this.m_iBasicNumber = strBasicNumber.Equals(String.Empty) ? 1 : Int32.Parse(strBasicNumber);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_SCROLLTABLE));
            base.WriteToStream(stream);
            stream.WriteByte((Byte)this.m_dDirection);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iBasicNumber));
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置滑动方向。
        /// </summary>
        public Direction Direction
        {
            get
            {
                return this.m_dDirection;
            }
            set
            {
                this.m_dDirection = value;
            }
        }

        /// <summary>
        /// 获取或设置摆放基数。
        /// </summary>
        public Int32 BasicNumber
        {
            get
            {
                return this.m_iBasicNumber;
            }
            set
            {
                if (value >= 1)
                {
                    this.m_iBasicNumber = value;
                }
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
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Direction")).InnerText = ((Int32)m_dDirection).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BasicNumber")).InnerText = m_iBasicNumber.ToString();
        }

        /// <summary>
        /// 绘制水平方向的表格。
        /// </summary>
        /// <param name="c">要绘制的画布。</param>
        /// <param name="p">表格左上角在画布上的坐标。</param>
        protected void PaintHorizontal(Canvas c, Point p)
        {
            Int32 sy = p.Y + this.Height;
            for (int i = 0; i < this.m_iChildNumber; ++i)
            {
                Int32 iRow = i % this.m_iBasicNumber;
                Int32 iCol = i / this.m_iBasicNumber;
                Int32 ppy = sy - (iRow + 1) * m_conPrototype.Height;
                Point pp = new Point(p.X + iCol * m_conPrototype.Width, ppy);
                m_conPrototype.Paint(c, pp);
            }

            Int32 col = (m_iChildNumber - 1) / m_iBasicNumber + 1;      //列数
            Int32 cw = col * m_conPrototype.Width;                      //总宽度
            Int32 bw = cw < this.Width ? this.Width : this.Width * this.Width / cw;     //比例宽度
            if (this.m_imgScrollBack != null)
            {
                Rect rtBack = new Rect(p.X, p.Y, this.Width, this.m_iScrollBarWidth);
                c.DrawImage(m_imgScrollBack, rtBack);
            }
            if (this.m_imgScrollBar != null)
            {
                Rect rtBar = new Rect(p.X, p.Y, bw, this.m_iScrollBarWidth);
                c.DrawImage(m_imgScrollBar, rtBar);
            }
        }

        /// <summary>
        /// 绘制竖直方向的表格。
        /// </summary>
        /// <param name="c">要绘制的画布。</param>
        /// <param name="p">表格左上角在画布上的坐标。</param>
        protected void PaintVertical(Canvas c, Point p)
        {
            Int32 sy = p.Y + this.Height;
            for (int i = 0; i < this.m_iChildNumber; ++i)
            {
                Int32 iRow = i / this.m_iBasicNumber;
                Int32 iCol = i % this.m_iBasicNumber;
                Int32 ppy = sy - (iRow + 1) * m_conPrototype.Height;
                Point pp = new Point(p.X + iCol * m_conPrototype.Width, ppy);
                m_conPrototype.Paint(c, pp);
            }

            Int32 row = (m_iChildNumber - 1) / m_iBasicNumber + 1;      //行数
            Int32 ch = row * m_conPrototype.Height;                     //总高度
            Int32 bh = ch < this.Height ? this.Height : this.Height * this.Height / ch;     //比例高度
            if (this.m_imgScrollBack != null)
            {
                Rect rtBack = new Rect(p.X + this.Width - this.m_iScrollBarWidth, p.Y, this.m_iScrollBarWidth, this.Height);
                c.DrawImage(m_imgScrollBack, rtBack);
            }
            if (this.m_imgScrollBar != null)
            {
                Rect rtBar = new Rect(p.X + this.Width - this.m_iScrollBarWidth, p.Y + this.Height - bh, this.m_iScrollBarWidth, bh);
                c.DrawImage(m_imgScrollBar, rtBar);
            }
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 表格的滑动方向。
        /// </summary>
        private Direction m_dDirection = Direction.Horizontal;

        /// <summary>
        /// 表格的摆放基数。
        /// </summary>
        private Int32 m_iBasicNumber = 1;

        #endregion
    }
}
