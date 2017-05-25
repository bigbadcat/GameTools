using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T002.Platform;
using System.Xml;
using T002.Common;
using System.IO;
using System.Drawing;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 表格提供了多项数据显示的功能。
    /// </summary>
    public abstract class Table : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        /// <param name="con">表格的单元格原型。</param>
        public Table(UserInterface ui, Container con)
            : base(ui)
        {
            m_conPrototype = con;
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "TAB_" + this.ConstVar;
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

            //偏移调整，用户点击左上角的原型，实际上原型在右下角
            Point cpt = new Point(p.X - this.X, p.Y - this.Y - (this.Height - this.Prototype.Height));
            Control ctr = this.m_conPrototype.GetControlAtPoint(cpt);
            return ctr == null ? this : ctr;        //不选中子控件就是选中自己
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strScrollBarWidth = XmlUtil.GetAttribute(xmlNode, "ScrollBarWidth");
            String strScrollBar = XmlUtil.GetAttribute(xmlNode, "ScrollBar");
            String strScrollBack = XmlUtil.GetAttribute(xmlNode, "ScrollBack");
            String strChildNumber = XmlUtil.GetAttribute(xmlNode, "ChildNumber");

            this.m_iScrollBarWidth = strScrollBarWidth.Equals(String.Empty) ? 5 : Int32.Parse(strScrollBarWidth);
            this.m_imgScrollBar = strScrollBar == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strScrollBar);
            this.m_imgScrollBack = strScrollBack == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strScrollBack);
            this.m_iChildNumber = strChildNumber.Equals(String.Empty) ? 5 : Int32.Parse(strChildNumber);

            //读入原型
            XmlNode xmlPro = xmlNode.FirstChild;
            if (xmlPro.Name.Equals("Prototype"))
            {
                this.m_conPrototype = UserInterface.LoadControlFromXmlNode(this.Interface, xmlPro.FirstChild) as Container;
                this.m_conPrototype.Parent = this;
            }
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            String scrollbarpath = m_imgScrollBar == null ? String.Empty : m_imgScrollBar.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String scrollbackpath = m_imgScrollBack == null ? String.Empty : m_imgScrollBack.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iScrollBarWidth));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(scrollbarpath.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(scrollbackpath.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iChildNumber));
            this.m_conPrototype.WriteToStream(stream);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置滑动条宽度。
        /// </summary>
        public Int32 ScrollBarWidth
        {
            get
            {
                return this.m_iScrollBarWidth;
            }
            set
            {
                if (value >= 0)
                {
                    this.m_iScrollBarWidth = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置滑动条滑块图像。
        /// </summary>
        public T002.Platform.Image ScrollBar
        {
            get
            {
                return this.m_imgScrollBar;
            }
            set
            {
                if (value != this.m_imgScrollBar)
                {
                    T002.Platform.Image.DeleteImage(this.m_imgScrollBar);
                    this.m_imgScrollBar = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置滑动条背景槽图像。
        /// </summary>
        public T002.Platform.Image ScrollBack
        {
            get
            {
                return this.m_imgScrollBack;
            }
            set
            {
                if (value != this.m_imgScrollBack)
                {
                    T002.Platform.Image.DeleteImage(this.m_imgScrollBack);
                    this.m_imgScrollBack = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置单元数量。
        /// </summary>
        public Int32 ChildNumber
        {
            get
            {
                return this.m_iChildNumber;
            }
            set
            {
                if (value >= 0)
                {
                    this.m_iChildNumber = value;
                }
            }
        }

        /// <summary>
        /// 获取表格的单元格原型。
        /// </summary>
        public Container Prototype
        {
            get
            {
                return this.m_conPrototype;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~Table()
        {
            T002.Platform.Image.DeleteImage(m_imgScrollBar);
            m_imgScrollBar = null;
            T002.Platform.Image.DeleteImage(m_imgScrollBack);
            m_imgScrollBack = null;
            m_conPrototype = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String scrollbarpath = m_imgScrollBar == null ? String.Empty : m_imgScrollBar.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String scrollbackpath = m_imgScrollBack == null ? String.Empty : m_imgScrollBack.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScrollBarWidth")).InnerText = m_iScrollBarWidth.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScrollBar")).InnerText = scrollbarpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScrollBack")).InnerText = scrollbackpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ChildNumber")).InnerText = m_iChildNumber.ToString();

            //保存原型
            XmlNode xmlPrototype = xmlDoc.CreateNode(XmlNodeType.Element, "Prototype", "");
            xmlPrototype.AppendChild(m_conPrototype.GetXmlNode(xmlDoc));
            xmlNode.AppendChild(xmlPrototype);
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 滚动条宽度。
        /// </summary>
        protected Int32 m_iScrollBarWidth = 5;

        /// <summary>
        /// 滑动条滑块图像。
        /// </summary>
        protected T002.Platform.Image m_imgScrollBar = null;

        /// <summary>
        /// 滑动条背景槽图像。
        /// </summary>
        protected T002.Platform.Image m_imgScrollBack = null;

        /// <summary>
        /// 单元数量。
        /// </summary>
        protected Int32 m_iChildNumber = 1;

        /// <summary>
        /// 表格单元格原型。
        /// </summary>
        protected Container m_conPrototype = null;

        #endregion
    }
}
