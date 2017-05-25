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
    /// 显示一张图像。
    /// </summary>
    public class Picture : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public Picture(UserInterface ui)
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
            if (this.m_imgShow != null)
            {
                if (this.m_imMode == ImageMode.Normal)
                {
                    Int32 x = p.X + this.X;
                    Int32 y = p.Y + this.Y;
                    switch (m_aAlign)
                    {
                        case Align.TopLeft:
                            y += this.Height;
                            break;
                        case Align.Top:
                            x += this.Width >> 1;
                            y += this.Height;
                            break;
                        case Align.TopRight:
                            x += this.Width;
                            y += this.Height;
                            break;
                        case Align.Left:
                            y += this.Height >> 1;
                            break;
                        case Align.Center:
                            x += this.Width >> 1;
                            y += this.Height >> 1;
                            break;
                        case Align.Right:
                            x += this.Width;
                            y += this.Height >> 1;
                            break;
                        case Align.BottomLeft:
                            break;
                        case Align.Bottom:
                            x += this.Width >> 1;
                            break;
                        case Align.BottomRight:
                            x += this.Width;
                            break;
                        default:
                            break;
                    }
                    c.DrawImage(m_imgShow, new Point(x, y), m_fScaleX, m_fScaleY, m_aAlign, Trans.None, m_cChannel);
                }
                else if (this.m_imMode == ImageMode.Stretch)
                {
                    c.DrawImage(this.m_imgShow, new Rect(p.X + this.Left, p.Y + this.Y, this.Width, this.Height), m_tTrans, m_cChannel);
                }
                else if (this.m_imMode == ImageMode.NinePatch)
                {
                    c.DrawImageNinePatch(this.m_imgShow, new Rect(p.X + this.Left, p.Y + this.Y, this.Width, this.Height), m_cChannel);
                }
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_PICTURE, "");
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
            String strImage = XmlUtil.GetAttribute(xmlNode, "Image");
            String strImageMode = XmlUtil.GetAttribute(xmlNode, "ImageMode");
            String strAlign = XmlUtil.GetAttribute(xmlNode, "Align");
            String strTrans = XmlUtil.GetAttribute(xmlNode, "Trans");
            String strScaleX = XmlUtil.GetAttribute(xmlNode, "ScaleX");
            String strScaleY = XmlUtil.GetAttribute(xmlNode, "ScaleY");
            String strChannel = XmlUtil.GetAttribute(xmlNode, "Channel");
            String strClearValue = XmlUtil.GetAttribute(xmlNode, "ClearValue");

            this.m_imgShow = strImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strImage);
            this.m_imMode = strImageMode.Equals(String.Empty) ? ImageMode.Normal : (ImageMode)Int32.Parse(strImageMode);
            this.m_aAlign = strAlign.Equals(String.Empty) ? Align.Center : (Align)Int32.Parse(strAlign);
            this.m_tTrans = strTrans.Equals(String.Empty) ? Trans.None : (Trans)Int32.Parse(strTrans);
            this.m_fScaleX = strScaleX.Equals(String.Empty) ? 1 : Single.Parse(strScaleX);
            this.m_fScaleY = strScaleY.Equals(String.Empty) ? 1 : Single.Parse(strScaleY);
            this.m_cChannel = strChannel == String.Empty ? Color.White : DataUtil.ParseColor(strChannel);
            this.m_bClearValue = strClearValue.Equals(String.Empty) ? false : Boolean.Parse(strClearValue);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[图像]");
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_PICTURE));
            base.WriteToStream(stream);
            String imgpath = m_imgShow == null || this.m_bClearValue ? String.Empty : m_imgShow.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(imgpath));
            stream.WriteByte((Byte)m_imMode);
            stream.WriteByte((Byte)m_aAlign);
            stream.WriteByte((Byte)m_tTrans);
            DataUtil.WriteSingle(stream, m_fScaleX);
            DataUtil.WriteSingle(stream, m_fScaleY);
            DataUtil.WriteColor(stream, m_cChannel);
        }

        /// <summary>
        /// 获取合适的尺寸，刚好能显示整张图像。
        /// </summary>
        /// <returns>合适的尺寸，若没有图像或图像为拉伸模式则返回控件尺寸。</returns>
        public Size GetSuitableSize()
        {
            Int32 width = this.Width;
            Int32 height = this.Height;
            if (this.m_imgShow != null && this.m_imMode == ImageMode.Normal)
            {
                width = (Int32)(this.m_imgShow.Width * this.m_fScaleX);
                height = (Int32)(this.m_imgShow.Height * this.m_fScaleY);
            }
            return new Size(width, height);
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "IMG_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置控件显示的图像。
        /// </summary>
        public T002.Platform.Image Image
        {
            get
            {
                return m_imgShow;
            }
            set
            {
                if (m_imgShow != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgShow);
                    m_imgShow = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置图像显示模式。
        /// </summary>
        public ImageMode Mode
        {
            get
            {
                return this.m_imMode;
            }
            set
            {
                this.m_imMode = value;
            }
        }

        /// <summary>
        /// 获取或设置图像对齐方式。
        /// </summary>
        public Align Align
        {
            get
            {
                return this.m_aAlign;
            }
            set
            {
                this.m_aAlign = value;
            }
        }

        /// <summary>
        /// 获取或设置图像的变换方式。
        /// </summary>
        public Trans Trans
        {
            get
            {
                return this.m_tTrans;
            }
            set
            {
                this.m_tTrans = value;
            }
        }

        /// <summary>
        /// 获取或设置图像X缩放比例。
        /// </summary>
        public Single ScaleX
        {
            get
            {
                return this.m_fScaleX;
            }
            set
            {
                this.m_fScaleX = value;
            }
        }

        /// <summary>
        /// 获取或设置图像Y缩放比例。
        /// </summary>
        public Single ScaleY
        {
            get
            {
                return this.m_fScaleY;
            }
            set
            {
                this.m_fScaleY = value;
            }
        }

        /// <summary>
        /// 获取或设置颜色通道。
        /// </summary>
        public Color Channel
        {
            get
            {
                return this.m_cChannel;
            }
            set
            {
                this.m_cChannel = value;
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
        ~Picture()
        {
            T002.Platform.Image.DeleteImage(m_imgShow);
            m_imgShow = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String imgpath = m_imgShow == null ? "" : m_imgShow.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Image")).InnerText = imgpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ImageMode")).InnerText = ((Int32)m_imMode).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Align")).InnerText = ((Int32)m_aAlign).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Trans")).InnerText = ((Int32)m_tTrans).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScaleX")).InnerText = m_fScaleX.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ScaleY")).InnerText = m_fScaleY.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Channel")).InnerText = DataUtil.ToStringValue(m_cChannel);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("ClearValue")).InnerText = this.m_bClearValue.ToString();
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 控件显示的图像。
        /// </summary>
        private T002.Platform.Image m_imgShow = null;

        /// <summary>
        /// 图像显示模式。
        /// </summary>
        private ImageMode m_imMode = ImageMode.Normal;

        /// <summary>
        /// 图像对齐方式。
        /// </summary>
        private Align m_aAlign = Align.Center;

        /// <summary>
        /// 图像的变换方式。
        /// </summary>
        private Trans m_tTrans = Trans.None;

        /// <summary>
        /// 图像X缩放比例。
        /// </summary>
        private Single m_fScaleX = 1.0f;

        /// <summary>
        /// 图像Y缩放比例。
        /// </summary>
        private Single m_fScaleY = 1.0f;

        /// <summary>
        /// 颜色通道。
        /// </summary>
        private Color m_cChannel = Color.White;

        /// <summary>
        /// 是否在生成时清除数据。
        /// </summary>
        protected Boolean m_bClearValue = false;

        #endregion
    }
}
