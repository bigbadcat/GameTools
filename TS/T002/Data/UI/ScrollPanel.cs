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
    /// 滚动面板提供了控件区域显示的功能。
    /// </summary>
    public class ScrollPanel : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        /// <param name="child">滚动面板显示的子控件。</param>
        public ScrollPanel(UserInterface ui, Container child)
            : base(ui)
        {
            m_conChild = child;
            if (m_conChild != null)
            {
                m_conChild.Parent = this;
                m_conChild.Position = new Point(0, 0);
            }
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
            m_conChild.Paint(c, new Point(cp.X - this.m_ptMove.X, cp.Y - this.m_ptMove.Y));
            c.Restore();

            //子项标记，PixelOffsetMode.Half;属性会使像素右下偏移一个像素
            Rect promark = new Rect(cp.X - m_ptMove.X + 1, cp.Y - m_ptMove.Y, m_conChild.Width - 1, m_conChild.Height - 1);
            c.DrawRect(promark, Color.LimeGreen);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[滚动面板]");
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_SCROLLPANEL, "");
            this.SetXmlNodeAttribute(xmlDoc, xmlNode);
            return xmlNode;
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "SRP_" + this.ConstVar;
        }

        /// <summary>
        /// 获取某个点对应的控件。
        /// </summary>
        /// <param name="p">指定点。</param>
        /// <returns>对应控件。</returns>
        public override Control GetControlAtPoint(Point p)
        {
            if (!this.Bounds.Contains(p) || !this.Visible)
            {
                return null;
            }

            //不选中子控件就是选中自己
            Point cpt = new Point(p.X - this.Left + m_ptMove.X, p.Y - this.Bottom + m_ptMove.Y);
            Control ctr = this.m_conChild.GetControlAtPoint(cpt);
            return ctr == null ? this : ctr;
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strMove = XmlUtil.GetAttribute(xmlNode, "Move");
            this.m_ptMove = strMove.Equals(String.Empty) ? Point.Empty : DataUtil.ParsePoint(strMove);

            //读入子项
            XmlNode xmlChild = xmlNode.FirstChild;
            if (xmlChild.Name.Equals("Child"))
            {
                this.m_conChild = UserInterface.LoadControlFromXmlNode(this.Interface, xmlChild.FirstChild) as Container;
                this.m_conChild.Parent = this;
            }
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_SCROLLPANEL));
            base.WriteToStream(stream);
            DataUtil.WritePoint(stream, m_ptMove);
            this.m_conChild.WriteToStream(stream);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置滚动面板移动的量。
        /// </summary>
        public Point Move
        {
            get
            {
                return this.m_ptMove;
            }
            set
            {
                this.m_ptMove.X = Math.Max(0, Math.Min(value.X, this.m_conChild.Width - this.Width));
                this.m_ptMove.Y = Math.Max(0, Math.Min(value.Y, this.m_conChild.Height - this.Height));
            }
        }

        /// <summary>
        /// 获取滚动面板显示的子控件。
        /// </summary>
        public Container Child
        {
            get
            {
                return this.m_conChild;
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
            this.m_ptMove.X = Math.Max(0, Math.Min(m_ptMove.X, this.m_conChild.Width - this.Width));
            this.m_ptMove.Y = Math.Max(0, Math.Min(m_ptMove.Y, this.m_conChild.Height - this.Height));
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Move")).InnerText = DataUtil.ToStringValue(m_ptMove);

            //保存原型
            XmlNode xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "Child", "");
            xmlChild.AppendChild(m_conChild.GetXmlNode(xmlDoc));
            xmlNode.AppendChild(xmlChild);
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 要滚动显示的子容器。
        /// </summary>
        private Container m_conChild = null;

        /// <summary>
        /// 子控件移动距离，往左（上）为正。
        /// </summary>
        private Point m_ptMove = Point.Empty;

        #endregion
    }
}
