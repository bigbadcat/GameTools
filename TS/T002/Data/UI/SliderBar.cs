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
    /// 滑动条，拖动选择一个数值。
    /// </summary>
    public class SliderBar : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public SliderBar(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 已重载。绘制滑动条。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Platform.Canvas c, Point p)
        {
            if (!this.Visible)
            {
                return;
            }

            //设置裁剪区域，绘制每个子控件
            Point cp = new Point(p.X + this.X, p.Y + this.Y);
            base.Paint(c, p);
            if (m_imgSlotImage != null)
            {
                c.DrawImageNinePatch(m_imgSlotImage, new Rect(cp, this.Size));
            }
            if (m_imgBarImage != null)
            {
                Int32 dx = this.Width / 2;
                Int32 dy = this.Height / 2;
                switch (m_dirDirection)
                {
                    case Direction.Horizontal:
                        dx = GetSliderPosition();
                        break;
                    case Direction.Vertical:
                        dy = GetSliderPosition();
                        break;
                    default:
                        break;
                }
                c.DrawImage(m_imgBarImage, new Point(cp.X + dx, cp.Y + dy), m_fBarZoom, Align.Center, Trans.None);
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_SLIDERBAR, "");
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
            String strBarImage = XmlUtil.GetAttribute(xmlNode, "BarImage");
            String strSlotImage = XmlUtil.GetAttribute(xmlNode, "SlotImage");
            String strBarZoom = XmlUtil.GetAttribute(xmlNode, "BarZoom");
            String strStartValue = XmlUtil.GetAttribute(xmlNode, "StartValue");
            String strCurrentValue = XmlUtil.GetAttribute(xmlNode, "CurrentValue");
            String strEndValue = XmlUtil.GetAttribute(xmlNode, "EndValue");
            String strDirection = XmlUtil.GetAttribute(xmlNode, "Direction");

            T002.Platform.Image.DeleteImage(this.m_imgBarImage);
            this.m_imgBarImage = strBarImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strBarImage);
            T002.Platform.Image.DeleteImage(this.m_imgSlotImage);
            this.m_imgSlotImage = strSlotImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strSlotImage);
            this.m_fBarZoom = strBarZoom == String.Empty ? 1.0f : Single.Parse(strBarZoom);
            this.m_iStartValue = strStartValue.Equals(String.Empty) ? 0 : Int32.Parse(strStartValue);
            this.m_iCurrentValue = strCurrentValue.Equals(String.Empty) ? 10 : Int32.Parse(strCurrentValue);
            this.m_iEndValue = strEndValue.Equals(String.Empty) ? 100 : Int32.Parse(strEndValue);
            this.m_dirDirection = strDirection.Equals(String.Empty) ? Direction.Horizontal : (Direction)Int32.Parse(strDirection);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[滑动条]");
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "SLB_" + this.ConstVar;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_SLIDERBAR));
            base.WriteToStream(stream);
            String barpath = m_imgBarImage == null ? String.Empty : m_imgBarImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            String slotpath = m_imgSlotImage == null ? String.Empty : m_imgSlotImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(barpath));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(slotpath));
            DataUtil.WriteSingle(stream, m_fBarZoom);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iStartValue));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iCurrentValue));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iEndValue));
            stream.WriteByte((Byte)m_dirDirection);
        }

        /// <summary>
        /// 获取合适的尺寸，刚好能显示滑动槽图像。
        /// </summary>
        /// <returns>合适的尺寸，若没有滑动槽图像则返回控件尺寸。</returns>
        public Size GetSuitableSize()
        {
            return m_imgSlotImage == null ? this.Size : m_imgSlotImage.Size;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置滑块图像。
        /// </summary>
        public T002.Platform.Image BarImage
        {
            get
            {
                return m_imgBarImage;
            }
            set
            {
                if (m_imgBarImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgBarImage);
                    m_imgBarImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置滑动槽图像。
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
        /// 获取或设置滑块缩放比例。
        /// </summary>
        public Single BarZoom
        {
            get
            {
                return this.m_fBarZoom;
            }
            set
            {
                if (value > 0)
                {
                    this.m_fBarZoom = value;
                }
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
        public Direction Direction
        {
            get
            {
                return this.m_dirDirection;
            }
            set
            {
                this.m_dirDirection = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~SliderBar()
        {
            T002.Platform.Image.DeleteImage(m_imgBarImage);
            m_imgBarImage = null;
            T002.Platform.Image.DeleteImage(m_imgSlotImage);
            m_imgSlotImage = null;
        }

        /// <summary>
        /// 获取滑块位置。
        /// </summary>
        /// <returns>滑块的位置。</returns>
        protected Int32 GetSliderPosition()
        {
            Int32 len = m_dirDirection == Direction.Horizontal ? this.Width : this.Height;
            Int32 pos = len * (m_iCurrentValue - m_iStartValue) / (m_iEndValue - m_iStartValue); ;
            return pos;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String fillpath = m_imgBarImage == null ? "" : m_imgBarImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String slotpath = m_imgSlotImage == null ? "" : m_imgSlotImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BarImage")).InnerText = fillpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("SlotImage")).InnerText = slotpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BarZoom")).InnerText = m_fBarZoom.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("StartValue")).InnerText = m_iStartValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("CurrentValue")).InnerText = m_iCurrentValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("EndValue")).InnerText = m_iEndValue.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Direction")).InnerText = ((Int32)m_dirDirection).ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 滑块图像。
        /// </summary>
        private T002.Platform.Image m_imgBarImage = null;

        /// <summary>
        /// 滑动槽图像。
        /// </summary>
        private T002.Platform.Image m_imgSlotImage = null;

        /// <summary>
        /// 滑块缩放比例。
        /// </summary>
        private Single m_fBarZoom = 1.0f;

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
        /// 滑动方向。
        /// </summary>
        private Direction m_dirDirection = Direction.Horizontal;

        #endregion
    }
}
