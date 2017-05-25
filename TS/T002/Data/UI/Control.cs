using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T002.Platform;
using System.Xml;
using T002.Common;
using System.Drawing;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 抽象类，提供了控件的通用界面。
    /// </summary>
    public abstract class Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public Control(UserInterface ui)
        {
            m_uiInterface = ui;
            m_bVisible = true;
            m_bEnable = true;
        }

        /// <summary>
        /// 绘制控件。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public virtual void Paint(Canvas c, Point p)
        {
            Int32 iX = p.X + this.X;
            Int32 iY = p.Y + this.Y;
            c.FillRect(new Rect(iX, iY, this.m_rtBounds.Width, this.m_rtBounds.Height), this.m_clBackColor);
        }

        /// <summary>
        /// 获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public abstract XmlNode GetXmlNode(XmlDocument xmlDoc);

        /// <summary>
        /// 从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public virtual void AssignFromXmlNode(XmlNode xmlNode)
        {
            String strCode = XmlUtil.GetAttribute(xmlNode, "Code");
            String strBounds =  XmlUtil.GetAttribute(xmlNode, "Bounds");
            String strBackColor = XmlUtil.GetAttribute(xmlNode, "BackColor");
            String strVisible = XmlUtil.GetAttribute(xmlNode, "Visible");
            String strEnable = XmlUtil.GetAttribute(xmlNode, "Enable");
            String strName = XmlUtil.GetAttribute(xmlNode, "Name");
            String strConstVar = XmlUtil.GetAttribute(xmlNode, "ConstVar");

            this.Code = strCode.Equals(String.Empty) ? 0 : Int32.Parse(strCode);
            this.m_rtBounds = strBounds == String.Empty ? Rect.Empty : DataUtil.ParseRect(strBounds);
            this.m_clBackColor = strBackColor == String.Empty ? Color.Transparent : DataUtil.ParseColor(strBackColor);
            this.m_bVisible = strVisible.Equals(String.Empty) ? true : Boolean.Parse(strVisible);
            this.m_bEnable = strEnable.Equals(String.Empty) ? true : Boolean.Parse(strEnable);
            this.m_strName = strName;
            this.m_strConstVar = strConstVar;
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public abstract String GetNodeName();

        /// <summary>
        /// 获取某个点对应的控件。
        /// </summary>
        /// <param name="p">指定点。</param>
        /// <returns>对应控件。</returns>
        public virtual Control GetControlAtPoint(Point p)
        {
            if (!this.Bounds.Contains(p) || !this.Visible)
            {
                return null;
            }

            return this;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public virtual void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iCode));
            DataUtil.WriteRect(stream, m_rtBounds);
            DataUtil.WriteColor(stream, m_clBackColor);
            stream.WriteByte(DataUtil.GetBooleanByte(m_bVisible));
            stream.WriteByte(DataUtil.GetBooleanByte(m_bEnable));
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public abstract String GetFullConstVar();

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置控件左下角X坐标。
        /// </summary>
        public Int32 X
        {
            get
            {
                return m_rtBounds.X;
            }
            set
            {
                m_rtBounds.X = value;
            }
        }

        /// <summary>
        /// 获取或设置控件左下角Y坐标。
        /// </summary>
        public Int32 Y
        {
            get
            {
                return m_rtBounds.Y;
            }
            set
            {
                m_rtBounds.Y = value;
            }
        }

        /// <summary>
        /// 获取或设置控件宽度。
        /// </summary>
        public Int32 Width
        {
            get
            {
                return this.m_rtBounds.Width;
            }
            set
            {
                this.m_rtBounds.Width = value;
                OnSizeChanged();
            }
        }

        /// <summary>
        /// 获取或设置控件高度。
        /// </summary>
        public Int32 Height
        {
            get
            {
                return this.m_rtBounds.Height;
            }
            set
            {
                this.m_rtBounds.Height = value;
                OnSizeChanged();
            }
        }

        /// <summary>
        /// 获取或设置控件的左上角X坐标。
        /// </summary>
        public Int32 Left
        {
            get
            {
                return this.m_rtBounds.Left;
            }
        }

        /// <summary>
        /// 获取或设置控件左上角Y坐标。
        /// </summary>
        public Int32 Top
        {
            get
            {
                return this.m_rtBounds.Top;
            }
        }

        /// <summary>
        /// 获取控件右下角X坐标。
        /// </summary>
        public Int32 Right
        {
            get
            {
                return this.m_rtBounds.Right;
            }
        }

        /// <summary>
        /// 获取控件右下角Y坐标。
        /// </summary>
        public Int32 Bottom
        {
            get
            {
                return this.m_rtBounds.Bottom;
            }
        }

        /// <summary>
        /// 获取或设置控件左上角坐标。
        /// </summary>
        public Point Position
        {
            get
            {
                return this.m_rtBounds.Position;
            }
            set
            {
                this.m_rtBounds.Position = value;
            }
        }

        /// <summary>
        /// 获取或设置控件尺寸。
        /// </summary>
        public Size Size
        {
            get
            {
                return this.m_rtBounds.Size;
            }
            set
            {
                this.m_rtBounds.Size = value;
                OnSizeChanged();
            }
        }

        /// <summary>
        /// 获取或设置控件边界矩形。
        /// </summary>
        public Rect Bounds
        {
            get
            {
                return this.m_rtBounds;
            }
            set
            {
                this.m_rtBounds = value;
                OnSizeChanged();
            }
        }

        /// <summary>
        /// 获取或设置控件的背景色。
        /// </summary>
        public Color BackColor
        {
            get
            {
                return this.m_clBackColor;
            }
            set
            {
                this.m_clBackColor = value;
            }
        }

        /// <summary>
        /// 获取或设置控件是否显示。
        /// </summary>
        public Boolean Visible
        {
            get
            {
                return this.m_bVisible;
            }
            set
            {
                this.m_bVisible = value;
            }
        }

        /// <summary>
        /// 获取或设置控件是否可用。
        /// </summary>
        public Boolean Enable
        {
            get
            {
                return this.m_bEnable;
            }
            set
            {
                this.m_bEnable = value;
                OnEnableChanged();
            }
        }

        /// <summary>
        /// 获取或设置控件Code。
        /// </summary>
        public Int32 Code
        {
            get
            {
                return this.m_iCode;
            }
            set
            {
                this.m_iCode = value;
            }
        }

        /// <summary>
        /// 获取或设置控件从属的父控件。
        /// </summary>
        public Control Parent
        {
            get
            {
                return this.m_cParent;
            }
            set
            {
                this.m_cParent = value;
            }
        }

        /// <summary>
        /// 获取控件所从属的界面。
        /// </summary>
        public UserInterface Interface
        {
            get
            {
                return this.m_uiInterface;
            }
        }

        /// <summary>
        /// 获取或设置空名称。
        /// </summary>
        public String Name
        {
            get
            {
                return this.m_strName;
            }
            set
            {
                this.m_strName = value;
            }
        }

        /// <summary>
        /// 获取或设置程序常量。
        /// </summary>
        public String ConstVar
        {
            get
            {
                return this.m_strConstVar;
            }
            set
            {
                this.m_strConstVar = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 控件尺寸发生改变。
        /// </summary>
        protected virtual void OnSizeChanged()
        {
        }

        /// <summary>
        /// 控件可用性发生改变。
        /// </summary>
        protected virtual void OnEnableChanged()
        {
        }

        /// <summary>
        /// 设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected virtual void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Code")).InnerText = m_iCode.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Bounds")).InnerText = DataUtil.ToStringValue(m_rtBounds);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BackColor")).InnerText = DataUtil.ToStringValue(m_clBackColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Visible")).InnerText = m_bVisible.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Enable")).InnerText = m_bEnable.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Name")).InnerText = m_strName;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ConstVar")).InnerText = m_strConstVar;
        }

        /// <summary>
        /// 获取节点文本。
        /// </summary>
        /// <param name="mk">控件标记。</param>
        /// <returns>节点文本。</returns>
        protected String GetNodeText(String mk)
        {
            String constvar = this.m_strConstVar == String.Empty ? String.Empty : String.Format("|{0}", this.m_strConstVar);
            String str = String.Format("{0}{1}({2}{3})", mk, this.m_iCode, this.Name, constvar);
            return str;
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 控件C。
        /// </summary>
        private Int32 m_iCode = 0;

        /// <summary>
        /// 边界矩形。
        /// </summary>
        private Rect m_rtBounds = Rect.Empty;

        /// <summary>
        /// 背景色。
        /// </summary>
        private Color m_clBackColor = Color.Transparent;

        /// <summary>
        /// 控件是否可见。
        /// </summary>
        private Boolean m_bVisible = false;

        /// <summary>
        /// 控件是否可用。
        /// </summary>
        private Boolean m_bEnable = false;

        /// <summary>
        /// 父控件。
        /// </summary>
        private Control m_cParent = null;

        /// <summary>
        /// 控件所在的界面。
        /// </summary>
        private UserInterface m_uiInterface = null;

        /// <summary>
        /// 控件名称。
        /// </summary>
        private String m_strName = String.Empty;

        /// <summary>
        /// 程序常量。
        /// </summary>
        private String m_strConstVar = String.Empty;

        #endregion
    }
}
