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
    /// 保存了文本相关的基本属性。
    /// </summary>
    public abstract class TextControl : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public TextControl(UserInterface ui)
            : base(ui)
        {
            m_iWordSize = 22;
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strText = XmlUtil.GetAttribute(xmlNode, "Text");
            String strWordSize = XmlUtil.GetAttribute(xmlNode, "WordSize");
            String strColor = XmlUtil.GetAttribute(xmlNode, "TextColor");
            String strClearValue = XmlUtil.GetAttribute(xmlNode, "ClearValue");

            this.m_strText = strText;
            this.m_iWordSize = strWordSize.Equals(String.Empty) ? 22 : Single.Parse(strWordSize);
            this.m_cTextColor = strColor.Equals(String.Empty) ? Color.Black : DataUtil.ParseColor(strColor);
            this.m_bClearValue = strClearValue.Equals(String.Empty) ? false : Boolean.Parse(strClearValue);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(m_bClearValue ? String.Empty : this.m_strText));
            DataUtil.WriteSingle(stream, this.m_iWordSize);
            DataUtil.WriteColor(stream, m_cTextColor);
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "TXT_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置文本内容。
        /// </summary>
        public String Text
        {
            get
            {
                return this.m_strText;
            }
            set
            {
                if (!this.m_strText.Equals(value))
                {
                    this.m_strText = value;
                    this.CreateNewTextImage();
                }
            }
        }

        /// <summary>
        /// 获取或设置文本字体大小。
        /// </summary>
        public Single WordSize
        {
            get
            {
                return this.m_iWordSize;
            }
            set
            {
                this.m_iWordSize = value;
                this.CreateNewTextImage();
            }
        }

        /// <summary>
        /// 获取或设置文本颜色。
        /// </summary>
        public Color TextColor
        {
            get
            {
                return this.m_cTextColor;
            }
            set
            {
                if (this.m_cTextColor.ToArgb() != value.ToArgb())
                {
                    this.m_cTextColor = value;
                    this.CreateNewTextImage();
                }
            }
        }

        /// <summary>
        /// 获取或设置是否在生成时清除数据。
        /// </summary>
        public Boolean ClearValue
        {
            get
            {
                return this.m_bClearValue;
            }
            set
            {
                this.m_bClearValue = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~TextControl()
        {
            T002.Platform.Image.DeleteImage(m_imgBuffer);
            m_imgBuffer = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Text")).InnerText = this.m_strText;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("WordSize")).InnerText = this.m_iWordSize.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("TextColor")).InnerText = DataUtil.ToStringValue(this.m_cTextColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ClearValue")).InnerText = this.m_bClearValue.ToString();
        }

        /// <summary>
        /// 创建新的文本图像。
        /// </summary>
        protected abstract void CreateNewTextImage();

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 控件显示的文本内容。
        /// </summary>
        protected String m_strText = String.Empty;

        /// <summary>
        /// 文本字体大小。
        /// </summary>
        protected Single m_iWordSize = 0;

        /// <summary>
        /// 文本颜色。
        /// </summary>
        protected Color m_cTextColor = Color.Black;

        /// <summary>
        /// 文本图像缓冲。
        /// </summary>
        protected T002.Platform.Image m_imgBuffer = null;

        /// <summary>
        /// 是否在生成时清除数据。
        /// </summary>
        protected Boolean m_bClearValue = false;

        #endregion
    }
}
