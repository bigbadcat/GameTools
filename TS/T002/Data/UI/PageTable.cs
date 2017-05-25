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
    /// 翻页表格。
    /// </summary>
    public class PageTable : Table
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        /// <param name="con">表格的单元格原型。</param>
        public PageTable(UserInterface ui, Container con)
            : base(ui, con)
        {
            if (con != null)
            {
                con.Parent = this;
                con.Width = this.Width / this.m_iCol;
                con.Height = this.Height / this.m_iRow;
            }
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[翻页表格]");
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
            Rect crt = new Rect(cp, this.Size);
            base.Paint(c, p);
            c.Save();
            c.SetClip(crt);
            Int32 shownum = Math.Min(this.m_iChildNumber, this.m_iRow * this.m_iCol);
            Int32 sy = cp.Y + this.Height;
            for (int i = 0; i < shownum; ++i)
            {
                Int32 iRow = i / this.m_iCol;
                Int32 iCol = i % this.m_iCol;
                Int32 ppy = sy - (iRow + 1) * m_conPrototype.Height;
                Point pp = new Point(cp.X + iCol * m_conPrototype.Width, ppy);
                m_conPrototype.Paint(c, pp);
            }
            c.Restore();

            //滑动条
            Int32 bw = this.Width / GetPageNumber();     //比例宽度
            if (this.m_imgScrollBack != null)
            {
                Rect rtBack = new Rect(crt.Left, crt.Bottom, this.Width, this.m_iScrollBarWidth);
                c.DrawImage(m_imgScrollBack, rtBack);
            }
            if (this.m_imgScrollBar != null)
            {
                Rect rtBar = new Rect(crt.Left, crt.Bottom, bw, this.m_iScrollBarWidth);
                c.DrawImage(m_imgScrollBar, rtBar);
            }

            //单元格标记，PixelOffsetMode.Half;属性会使像素右下偏移一个像素
            Rect promark = new Rect(cp.X + 1, sy - this.Prototype.Height, m_conPrototype.Width - 1, m_conPrototype.Height - 1);
            c.DrawRect(promark, Color.LimeGreen);
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_PAGETABLE, "");
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
            String strRow = XmlUtil.GetAttribute(xmlNode, "Row");
            String strCol = XmlUtil.GetAttribute(xmlNode, "Col");
            this.m_iRow = strRow.Equals(String.Empty) ? 1 : Int32.Parse(strRow);
            this.m_iCol = strCol.Equals(String.Empty) ? 1 : Int32.Parse(strCol);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_PAGETABLE));
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iRow));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iCol));
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置一页显示多少行。
        /// </summary>
        public Int32 Row
        {
            get
            {
                return this.m_iRow;
            }
            set
            {
                if (value > 0 && this.m_iRow != value)
                {
                    this.m_iRow = value;
                    this.m_conPrototype.Height = this.Height / this.m_iRow;
                }
            }
        }

        /// <summary>
        /// 获取或设置一页显示多少列。
        /// </summary>
        public Int32 Col
        {
            get
            {
                return this.m_iCol;
            }
            set
            {
                if (value > 0 && this.m_iCol != value)
                {
                    this.m_iCol = value;
                    this.m_conPrototype.Width = this.Width / this.m_iCol;
                }
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
            m_conPrototype.Width = this.Width / this.m_iCol;
            m_conPrototype.Height = this.Height / this.m_iRow;
        }

        /// <summary>
        /// 获取表格页面数量。
        /// </summary>
        /// <returns>表格页面数量。</returns>
        protected Int32 GetPageNumber()
        {
            Int32 pagechild = m_iRow * m_iCol;
            Int32 num = 1;
            if (this.m_iChildNumber > 0)
            {
                num = (m_iChildNumber - 1) / pagechild + 1;
            }

            return num;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Row")).InnerText = m_iRow.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Col")).InnerText = m_iCol.ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 一页显示多少行。
        /// </summary>
        private Int32 m_iRow = 1;

        /// <summary>
        /// 一页显示多少列。
        /// </summary>
        private Int32 m_iCol = 1;

        #endregion
    }
}
