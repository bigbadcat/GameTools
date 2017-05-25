using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T002.Platform;
using System.Drawing;
using System.Xml;
using T002.Common;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 重复显示图像，图像数量表示数字。
    /// </summary>
    public class NumberImage : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public NumberImage(UserInterface ui)
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
            if (this.m_imgNumberImage != null && m_iNumber > 0)
            {
                Point cp = new Point(this.X + p.X, this.Y + p.Y);
                if (m_dDirection == UI.Direction.Horizontal)
                {
                    this.PaintHorizontal(c, cp);
                }
                else if (m_dDirection == UI.Direction.Vertical)
                {
                    this.PaintVertical(c, cp);
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
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_NUMBERIMAGE, "");
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
            String strUpImage = XmlUtil.GetAttribute(xmlNode, "UpperLimitImage");
            String strNumber = XmlUtil.GetAttribute(xmlNode, "Number");
            String strUpNumber = XmlUtil.GetAttribute(xmlNode, "UpperLimit");
            String strZoom = XmlUtil.GetAttribute(xmlNode, "Zoom");
            String strGap = XmlUtil.GetAttribute(xmlNode, "Gap");
            String strAlign = XmlUtil.GetAttribute(xmlNode, "Align");
            String strDirection = XmlUtil.GetAttribute(xmlNode, "Direction");

            this.m_imgNumberImage = strImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strImage);
            this.m_imgUpperLimitImage = strUpImage == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strUpImage);
            this.m_iNumber = strNumber.Equals(String.Empty) ? 0 : Int32.Parse(strNumber);
            this.m_iUpperLimit = strUpNumber.Equals(String.Empty) ? 0 : Int32.Parse(strUpNumber);
            this.m_fZoom = strZoom.Equals(String.Empty) ? 1 : Single.Parse(strZoom);
            this.m_fGap = strGap.Equals(String.Empty) ? 0 : Single.Parse(strGap);
            this.m_lmAlign = strAlign.Equals(String.Empty) ? LineMode.Start : (LineMode)Int32.Parse(strAlign);
            this.m_dDirection = strDirection.Equals(String.Empty) ? Direction.Horizontal : (Direction)Int32.Parse(strDirection);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[数字图像]");
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "NBI_" + this.ConstVar;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_NUMBERIMAGE));
            base.WriteToStream(stream);
            String imgpath = m_imgNumberImage == null ? String.Empty : m_imgNumberImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            String upimgpath = m_imgUpperLimitImage == null ? String.Empty : m_imgUpperLimitImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length).Replace("\\", "/");
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(imgpath));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(upimgpath));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iNumber));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_iUpperLimit));
            DataUtil.WriteSingle(stream, m_fZoom);
            DataUtil.WriteSingle(stream, m_fGap);
            stream.WriteByte((Byte)m_lmAlign);
            stream.WriteByte((Byte)m_dDirection);
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
        /// 获取或设置表示填充的图像。
        /// </summary>
        public T002.Platform.Image UpperLimitImage
        {
            get
            {
                return m_imgUpperLimitImage;
            }
            set
            {
                if (m_imgUpperLimitImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgUpperLimitImage);
                    m_imgUpperLimitImage = value;
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
        /// 获取或设置上限的数字。
        /// </summary>
        public Int32 UpperLimit
        {
            get
            {
                return this.m_iUpperLimit;
            }
            set
            {
                if (value >= 0)
                {
                    this.m_iUpperLimit = value;
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
        /// 获取或设置摆放方向。
        /// </summary>
        public Direction Direction
        {
            get
            {
                return this.m_dDirection;
            }
            set
            {
                this.m_dDirection = value;
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

        /// <summary>
        /// 图像之间的间隔。
        /// </summary>
        public Single Gap
        {
            get
            {
                return m_fGap;
            }
            set
            {
                m_fGap = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~NumberImage()
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
            String upimgpath = m_imgUpperLimitImage == null ? "" : m_imgUpperLimitImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Image")).InnerText = imgpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("UpperLimitImage")).InnerText = upimgpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Number")).InnerText = m_iNumber.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("UpperLimit")).InnerText = m_iUpperLimit.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Zoom")).InnerText = m_fZoom.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Gap")).InnerText = m_fGap.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Align")).InnerText = ((Int32)m_lmAlign).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Direction")).InnerText = ((Int32)m_dDirection).ToString();
        }

        /// <summary>
        /// 绘制水平方向的表格。
        /// </summary>
        /// <param name="c">要绘制的画布。</param>
        /// <param name="p">表格左上角在画布上的坐标。</param>
        protected void PaintHorizontal(Canvas c, Point p)
        {
            Int32 numw = (Int32)(m_imgNumberImage.Width * m_fZoom);
            Int32 dx = p.X;
            Int32 dy = p.Y + (this.Height >> 1);
            Int32 gap = (Int32)m_fGap;
            Int32 addgap = Math.Max(0, m_iNumber - 1) * gap;

            //根据对其方式调整绘制开始位置
            Boolean fillfirst = false;
            Int32 fillnum = Math.Max(0, m_iUpperLimit - m_iNumber);
            Int32 imgnum = Math.Max(m_iUpperLimit, m_iNumber);
            if (this.m_lmAlign == LineMode.Middle)
            {
                dx += (this.Width - numw * imgnum - addgap) >> 1;
            }
            else if (m_lmAlign == LineMode.End)
            {
                dx += this.Width - numw * imgnum - addgap;
                fillfirst = true;
            }

            //先填充
            if (fillfirst)
            {
                for (int i = 0; i < fillnum; ++i)
                {
                    if (m_imgUpperLimitImage != null)
                    {
                        c.DrawImage(m_imgUpperLimitImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Left, Trans.None);

                    }
                    dx += numw + gap;
                }
            }

            //绘制每一个图像
            for (Int32 i = 0; i < m_iNumber; ++i)
            {
                c.DrawImage(m_imgNumberImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Left, Trans.None);
                dx += numw + gap;
            }

            //后填充
            if (!fillfirst)
            {
                for (int i = 0; i < fillnum; ++i)
                {
                    if (m_imgUpperLimitImage != null)
                    {
                        c.DrawImage(m_imgUpperLimitImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Left, Trans.None);
                    }
                    dx += numw + gap;
                }
            }
        }

        /// <summary>
        /// 绘制竖直方向的表格。
        /// </summary>
        /// <param name="c">要绘制的画布。</param>
        /// <param name="p">表格左上角在画布上的坐标。</param>
        protected void PaintVertical(Canvas c, Point p)
        {
            Int32 numh = (Int32)(m_imgNumberImage.Height * m_fZoom);
            Int32 dx = p.X + (this.Width >> 1);
            Int32 dy = p.Y;
            Int32 gap = (Int32)m_fGap;
            Int32 addgap = Math.Max(0, m_iNumber - 1) * gap;

            //根据对其方式调整绘制开始位置
            Boolean fillfirst = false;
            Int32 fillnum = Math.Max(0, m_iUpperLimit - m_iNumber);
            Int32 imgnum = Math.Max(m_iUpperLimit, m_iNumber);
            if (m_lmAlign == LineMode.Middle)
            {
                dy += (this.Height - numh * imgnum - addgap) >> 1;
            }
            else if (m_lmAlign == LineMode.End)
            {
                dy += this.Height - numh * imgnum - addgap;
                fillfirst = true; 
            }

            //先填充
            if (fillfirst)
            {                
                for (int i = 0; i < fillnum; ++i)
                {
                    if (m_imgUpperLimitImage != null)
                    {
                    c.DrawImage(m_imgUpperLimitImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Bottom, Trans.None);
                    }
                    dy += numh + gap;
                }
            }

            //绘制每一个图像
            for (Int32 i = 0; i < m_iNumber; ++i)
            {
                c.DrawImage(m_imgNumberImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Bottom, Trans.None);
                dy += numh + gap;
            }

            //后填充
            if (!fillfirst)
            {
                for (int i = 0; i < fillnum; ++i)
                {
                    if (m_imgUpperLimitImage != null)
                    {
                        c.DrawImage(m_imgUpperLimitImage, new Point(dx, dy), m_fZoom, T002.Data.UI.Align.Bottom, Trans.None);

                    }
                    dy += numh + gap;
                }
            }
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 表示数字的图像。
        /// </summary>
        private T002.Platform.Image m_imgNumberImage = null;

        /// <summary>
        /// 填充的图像。
        /// </summary>
        private T002.Platform.Image m_imgUpperLimitImage = null;

        /// <summary>
        /// 表示的数字。
        /// </summary>
        private Int32 m_iNumber = 0;

        /// <summary>
        /// 数值上限。
        /// </summary>
        private Int32 m_iUpperLimit = 0;

        /// <summary>
        /// 缩放大小。
        /// </summary>
        private Single m_fZoom = 1;

        /// <summary>
        /// 图像之间的间隔。
        /// </summary>
        private Single m_fGap = 0;

        /// <summary>
        /// 对齐方式。
        /// </summary>
        private LineMode m_lmAlign = LineMode.Start;

        /// <summary>
        /// 图像摆放方向。
        /// </summary>
        private Direction m_dDirection = Direction.Horizontal;

        #endregion
    }
}
