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
    /// 文本域，用于显示大量图像，可滑动查看。
    /// </summary>
    public class TextArea : TextControl
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public TextArea(UserInterface ui)
            : base(ui)
        {
            this.CreateNewTextImage();
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
            Point dp = new Point(cp.X, cp.Y + this.Height);
            base.Paint(c, p);
            c.Save();
            c.SetClip(new Rect(cp, this.Size));
            c.DrawImage(this.m_imgBuffer, dp, 1, Align.TopLeft, Trans.None);
            c.Restore();

            //竖直滚动条
            Int32 ch = this.m_imgBuffer.Height;                     //总高度
            Int32 bh = ch < this.Height ? this.Height : this.Height * this.Height / ch;     //比例高度
            if (this.m_imgScrollBack != null)
            {
                Rect rtBack = new Rect(cp.X + this.Width - this.m_iScrollBarWidth, cp.Y, this.m_iScrollBarWidth, this.Height);
                c.DrawImage(m_imgScrollBack, rtBack);
            }
            if (this.m_imgScrollBar != null)
            {
                Rect rtBar = new Rect(cp.X + this.Width - this.m_iScrollBarWidth, cp.Y + this.Height - bh, this.m_iScrollBarWidth, bh);
                c.DrawImage(m_imgScrollBar, rtBar);
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_TEXTAREA, "");
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
            String strScrollBarWidth = XmlUtil.GetAttribute(xmlNode, "ScrollBarWidth");
            String strScrollBar = XmlUtil.GetAttribute(xmlNode, "ScrollBar");
            String strScrollBack = XmlUtil.GetAttribute(xmlNode, "ScrollBack");

            this.m_iScrollBarWidth = strScrollBarWidth.Equals(String.Empty) ? 5 : Int32.Parse(strScrollBarWidth);
            this.m_imgScrollBar = strScrollBar == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strScrollBar);
            this.m_imgScrollBack = strScrollBack == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strScrollBack);
            this.CreateNewTextImage();
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[文本域]");
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            String scrollbarpath = m_imgScrollBar == null ? String.Empty : m_imgScrollBar.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String scrollbackpath = m_imgScrollBack == null ? String.Empty : m_imgScrollBack.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_TEXTAREA));
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iScrollBarWidth));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(scrollbarpath.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(scrollbackpath.Replace("\\", "/")));
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

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~TextArea()
        {
            T002.Platform.Image.DeleteImage(m_imgScrollBar);
            m_imgScrollBar = null;
            T002.Platform.Image.DeleteImage(m_imgScrollBack);
            m_imgScrollBack = null;
        }

        /// <summary>
        /// 已重载。按钮的尺寸发送改变。
        /// </summary>
        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            this.CreateNewTextImage();
        }

        /// <summary>
        /// 创建新的文本图像。
        /// </summary>
        protected override void CreateNewTextImage()
        {
            T002.Platform.Image.DeleteImage(this.m_imgBuffer);
            this.m_imgBuffer = T002.Platform.Image.GetColorStringImage(m_strText, m_iWordSize, m_cTextColor, this.Width);
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

        #endregion
    }
}
