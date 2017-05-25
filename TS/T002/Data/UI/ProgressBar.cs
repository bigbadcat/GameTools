using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using T002.Platform;
using System.Xml;
using T002.Common;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 进度条类，表示某个值所占的比例。
    /// </summary>
    public class ProgressBar : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public ProgressBar(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 已重载。绘制容器。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Canvas c, Point p)
        {
            if (!this.Visible)
            {
                return;
            }
            base.Paint(c, p);

            //绘制进度
            Rect rt = new Rect(this.X + p.X, this.Y + p.Y, this.Width, this.Height);
            Rect showrt = rt;
            int ratewidth = this.Width * (m_iCurrentValue - m_iStartValue) / (m_iEndValue - m_iStartValue);
            int rateheight = this.Height * (m_iCurrentValue - m_iStartValue) / (m_iEndValue - m_iStartValue);
            switch (m_pdDirection)
            {
                case ProgressDirection.Left:
                    showrt.Width = ratewidth;
                    showrt.X = rt.Left + this.Width - ratewidth;
                    break;
                case ProgressDirection.Right:
                    showrt.Width = ratewidth;
                    break;
                case ProgressDirection.Up:
                    showrt.Height = rateheight;
                    break;
                case ProgressDirection.Down:
                    showrt.Height = rateheight;
                    showrt.Y = rt.Bottom + this.Height - rateheight;
                    break;
                default:
                    break;
            }
            
            if (m_imgSlotImage != null)
            {
                c.DrawImageNinePatch(m_imgSlotImage, rt);
            }
            c.FillRect(showrt, m_cFillColor);
            if (m_imgFillImage != null)
            {
                c.Save();
                c.SetClip(showrt);
                c.DrawImageNinePatch(m_imgFillImage, rt);
                c.Restore();
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_PROGRESSBAR, "");
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
            String strFillImage = XmlUtil.GetAttribute(xmlNode, "FillImage");
            String strSlotImage = XmlUtil.GetAttribute(xmlNode, "SlotImage");
            String strFillColor = XmlUtil.GetAttribute(xmlNode, "FillColor");
            String strStartValue = XmlUtil.GetAttribute(xmlNode, "StartValue");
            String strCurrentValue = XmlUtil.GetAttribute(xmlNode, "CurrentValue");
            String strEndValue = XmlUtil.GetAttribute(xmlNode, "EndValue");
            String strDirection = XmlUtil.GetAttribute(xmlNode, "Direction");

            T002.Platform.Image.DeleteImage(this.m_imgFillImage);
            this.m_imgFillImage = strFillImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strFillImage);
            T002.Platform.Image.DeleteImage(this.m_imgSlotImage);
            this.m_imgSlotImage = strSlotImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strSlotImage);
            this.m_cFillColor = strFillColor == String.Empty ? Color.White : DataUtil.ParseColor(strFillColor);
            this.m_iStartValue = strStartValue.Equals(String.Empty) ? 0 : Int32.Parse(strStartValue);
            this.m_iCurrentValue = strCurrentValue.Equals(String.Empty) ? 10 : Int32.Parse(strCurrentValue);
            this.m_iEndValue = strEndValue.Equals(String.Empty) ? 100 : Int32.Parse(strEndValue);
            this.m_pdDirection = strDirection.Equals(String.Empty) ? ProgressDirection.Right : (ProgressDirection)Int32.Parse(strDirection);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[进度条]");
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "PRB_" + this.ConstVar;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_PROGRESSBAR));
            base.WriteToStream(stream);
            String fillpath = m_imgFillImage == null ? String.Empty : m_imgFillImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            String slotpath = m_imgSlotImage == null ? String.Empty : m_imgSlotImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(fillpath));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(slotpath));
            DataUtil.WriteColor(stream, m_cFillColor);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iStartValue));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iCurrentValue));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iEndValue));
            stream.WriteByte((Byte)m_pdDirection);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置进度比例图像。
        /// </summary>
        public T002.Platform.Image FillImage
        {
            get
            {
                return m_imgFillImage;
            }
            set
            {
                if (m_imgFillImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgFillImage);
                    m_imgFillImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置进度槽图像。
        /// </summary>
        public T002.Platform.Image SlotImage
        {
            get
            {
                return m_imgSlotImage;
            }
            set
            {
                if (m_imgSlotImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgSlotImage);
                    m_imgSlotImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置进度比例填充的颜色。
        /// </summary>
        public Color FillColor
        {
            get
            {
                return this.m_cFillColor;
            }
            set
            {
                this.m_cFillColor = value;
            }
        }

        /// <summary>
        /// 获取或设置进度条的起始值。
        /// </summary>
        public Int32 StartValue
        {
            get
            {
                return this.m_iStartValue;
            }
            set
            {
                if (value < this.m_iEndValue)
                {
                    this.m_iStartValue = value;
                    this.m_iCurrentValue = Math.Max(this.m_iCurrentValue, this.m_iStartValue);
                }
            }
        }

        /// <summary>
        /// 获取或设置当前值，不在范围内的值会自动调整到边界。
        /// </summary>
        public Int32 CurrentValue
        {
            get
            {
                return this.m_iCurrentValue;
            }
            set
            {
                this.m_iCurrentValue = Math.Min(this.m_iEndValue, Math.Max(this.m_iStartValue, value));
            }
        }

        /// <summary>
        /// 获取或设置进度条的结束值。
        /// </summary>
        public Int32 EndValue
        {
            get
            {
                return this.m_iEndValue;
            }
            set
            {
                if (value > this.m_iStartValue)
                {
                    this.m_iEndValue = value;
                    this.m_iCurrentValue = Math.Min(this.m_iCurrentValue, this.m_iEndValue);
                }
            }
        }

        /// <summary>
        /// 获取或设置进度方向。
        /// </summary>
        public ProgressDirection Direction
        {
            get
            {
                return this.m_pdDirection;
            }
            set
            {
                this.m_pdDirection = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~ProgressBar()
        {
            T002.Platform.Image.DeleteImage(m_imgFillImage);
            m_imgFillImage = null;
            T002.Platform.Image.DeleteImage(m_imgSlotImage);
            m_imgSlotImage = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String fillpath = m_imgFillImage == null ? "" : m_imgFillImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String slotpath = m_imgSlotImage == null ? "" : m_imgSlotImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("FillImage")).InnerText = fillpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("SlotImage")).InnerText = slotpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("FillColor")).InnerText = DataUtil.ToStringValue(m_cFillColor);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("StartValue")).InnerText = m_iStartValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("CurrentValue")).InnerText = m_iCurrentValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("EndValue")).InnerText = m_iEndValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Direction")).InnerText = ((Int32)m_pdDirection).ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 进度比例图像。
        /// </summary>
        private T002.Platform.Image m_imgFillImage = null;

        /// <summary>
        /// 进度槽图像。
        /// </summary>
        private T002.Platform.Image m_imgSlotImage = null;

        /// <summary>
        /// 填充的颜色。
        /// </summary>
        private Color m_cFillColor = Color.Transparent;

        /// <summary>
        /// 起始值。
        /// </summary>
        private Int32 m_iStartValue = 0;

        /// <summary>
        /// 当前值，介于起始值和结束值之间。
        /// </summary>
        private Int32 m_iCurrentValue = 10;

        /// <summary>
        /// 结束值。
        /// </summary>
        private Int32 m_iEndValue = 100;

        /// <summary>
        /// 进度方向。
        /// </summary>
        private ProgressDirection m_pdDirection = ProgressDirection.Right;

        #endregion
    }
}
