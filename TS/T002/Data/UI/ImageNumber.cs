using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using T002.Common;
using System.IO;
using T002.Platform;
using System.Drawing;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 显示一个由图像拼成的数字。
    /// </summary>
    public class ImageNumber : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public ImageNumber(UserInterface ui)
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
            if (this.m_imgNumberImage != null)
            {
                Point cp = new Point(p.X + this.X, p.Y + this.Y);
                Int32 bitlen = 0;								//要绘制的数据位数
                Int32 num = this.m_iNumber;
                do
                {
                    ++bitlen;
                    num /= 10;
                } while (num > 0);

                //根据对齐方式和缩放调整绘制准备
                Single dx = cp.X + this.Width;										//绘制的X坐标
                Single dy = cp.Y + (this.Height >> 1);								//绘制的Y坐标基于控件中心
                Single numw = bitlen * m_imgNumberImage.Width * this.m_fZoom / 10;		//要显示的宽度
                if (this.m_lmAlign == LineMode.Start)
                {
                    dx -= this.Width - numw;
                }
                else if (m_lmAlign == LineMode.Middle)
                {
                    dx -= (this.Width - numw) / 2;
                }

                //从个位开始，一个一个数字位地绘制
                Single clipy = cp.Y + (this.Height - m_imgNumberImage.Height * this.m_fZoom) / 2;
                Single bitw = m_imgNumberImage.Width * m_fZoom / 10;		//一个数据位的图像宽度
                Single bith = m_imgNumberImage.Height * m_fZoom;
                num = m_iNumber;
                do
                {
                    Int32 bit = num % 10;
                    num /= 10;

                    //绘制一个数据位，用左对齐，所以X坐标要减去一个数据位宽度
                    c.Save();
                    c.SetClip(new Rect((Int32)(dx - bitw), (Int32)clipy, (Int32)bitw, (Int32)bith));
                    c.DrawImage(m_imgNumberImage, new Point((Int32)(dx - bitw * bit - bitw), (Int32)dy), m_fZoom, T002.Data.UI.Align.Left, Trans.None);
                    c.Restore();
                    dx -= bitw;
                } while (num > 0);
            }
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_IMAGENUMBER, "");
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
            String strNumber = XmlUtil.GetAttribute(xmlNode, "Number");
            String strZoom = XmlUtil.GetAttribute(xmlNode, "Zoom");
            String strAlign = XmlUtil.GetAttribute(xmlNode, "Align");

            this.m_imgNumberImage = strImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strImage);
            this.m_iNumber = strNumber.Equals(String.Empty) ? 0 : Int32.Parse(strNumber);
            this.m_fZoom = strZoom.Equals(String.Empty) ? 1 : Single.Parse(strZoom);
            this.m_lmAlign = strAlign.Equals(String.Empty) ? LineMode.Start : (LineMode)Int32.Parse(strAlign);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[图像数字]");
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "INB_" + this.ConstVar;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_IMAGENUMBER));
            base.WriteToStream(stream);
            String imgpath = m_imgNumberImage == null ? String.Empty : m_imgNumberImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(imgpath));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iNumber));
            DataUtil.WriteSingle(stream, m_fZoom);
            stream.WriteByte((Byte)m_lmAlign);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置表示数字的图像。
        /// </summary>
        public T002.Platform.Image Image
        {
            get
            {
                return m_imgNumberImage;
            }
            set
            {
                if (m_imgNumberImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgNumberImage);
                    m_imgNumberImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置表示的数字。
        /// </summary>
        public Int32 Number
        {
            get
            {
                return this.m_iNumber;
            }
            set
            {
                if (value >= 0)
                {
                    this.m_iNumber = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置对齐方式。
        /// </summary>
        public LineMode Align
        {
            get
            {
                return this.m_lmAlign;
            }
            set
            {
                this.m_lmAlign = value;
            }
        }

        /// <summary>
        /// 获取或设置缩放大小。
        /// </summary>
        public Single Zoom
        {
            get
            {
                return this.m_fZoom;
            }
            set
            {
                if (value > 0)
                {
                    this.m_fZoom = value;
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~ImageNumber()
        {
            T002.Platform.Image.DeleteImage(m_imgNumberImage);
            m_imgNumberImage = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String imgpath = m_imgNumberImage == null ? "" : m_imgNumberImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Image")).InnerText = imgpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Number")).InnerText = m_iNumber.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Zoom")).InnerText = m_fZoom.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Align")).InnerText = ((Int32)m_lmAlign).ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 表示数字的图像。
        /// </summary>
        private T002.Platform.Image m_imgNumberImage = null;

        /// <summary>
        /// 表示的数字。
        /// </summary>
        private Int32 m_iNumber = 0;

        /// <summary>
        /// 缩放大小。
        /// </summary>
        private Single m_fZoom = 1;

        /// <summary>
        /// 对齐方式。
        /// </summary>
        private LineMode m_lmAlign = LineMode.Start;

        #endregion
    }
}
