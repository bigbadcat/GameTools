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
    /// 按钮提供了触摸输入功能。
    /// </summary>
    public abstract class Button : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public Button(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 已重载。绘制标签。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public override void Paint(Platform.Canvas c, Point p)
        {
            Point cp = new Point(this.X + p.X, this.Y + p.Y);
            base.Paint(c, p);

            Point imgpos = new Point(cp.X + this.Width / 2, cp.Y + this.Height / 2);
            if (this.Enable)
            {
                if (m_bsState == ButtonState.Normal)
                {
                    if (m_imgNormalImage != null)
                    {
                        c.DrawImage(m_imgNormalImage, imgpos, 1, Align.Center, Trans.None);
                    }
                }
                else
                {
                    if (m_imgDownImage != null)
                    {
                        c.DrawImage(m_imgDownImage, imgpos, 1, Align.Center, Trans.None);
                    }
                }
            }
            else
            {
                if (m_imgDisableImage != null)
                {
                    c.DrawImage(m_imgDisableImage, imgpos, 1, Align.Center, Trans.None);
                }
            }
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strNormalImage = XmlUtil.GetAttribute(xmlNode, "NormalImage");
            String strDownImage = XmlUtil.GetAttribute(xmlNode, "DownImage");
            String strDisableImage = XmlUtil.GetAttribute(xmlNode, "DisableImage");

            this.m_imgNormalImage = strNormalImage.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strNormalImage);
            this.m_imgDownImage = strDownImage.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strDownImage);
            this.m_imgDisableImage = strDisableImage.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strDisableImage);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            String normalpath = m_imgNormalImage == null ? "" : m_imgNormalImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String downpath = m_imgDownImage == null ? "" : m_imgDownImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String disablepath = m_imgDisableImage == null ? "" : m_imgDisableImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(normalpath.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(downpath.Replace("\\", "/")));
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(disablepath.Replace("\\", "/")));
        }

        /// <summary>
        /// 获取合适的尺寸，刚好能1:1显示一个状态的按钮图像。
        /// </summary>
        /// <returns>合适的尺寸，若没有按钮图像则返回控件尺寸。</returns>
        public Size GetSuitableSize()
        {
            Int32 width = this.Width;
            Int32 height = this.Height;
            T002.Platform.Image img = m_imgNormalImage != null ? m_imgNormalImage : (m_imgDownImage != null ? m_imgDownImage : null);
            if (img != null)
            {
                width = img.Width;
                height = img.Height;
            }
            return new Size(width, height);
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置按钮的普通状态图像。
        /// </summary>
        public T002.Platform.Image NormalImage
        {
            get
            {
                return m_imgNormalImage;
            }
            set
            {
                if (m_imgNormalImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgNormalImage);
                    m_imgNormalImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置按钮的按下状态图像。
        /// </summary>
        public T002.Platform.Image DownImage
        {
            get
            {
                return m_imgDownImage;
            }
            set
            {
                if (m_imgDownImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgDownImage);
                    m_imgDownImage = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置按钮的禁用状态图像。
        /// </summary>
        public T002.Platform.Image DisableImage
        {
            get
            {
                return m_imgDisableImage;
            }
            set
            {
                if (m_imgDisableImage != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgDisableImage);
                    m_imgDisableImage = value;
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~Button()
        {
            T002.Platform.Image.DeleteImage(m_imgNormalImage);
            m_imgNormalImage = null;
            T002.Platform.Image.DeleteImage(m_imgDownImage);
            m_imgDownImage = null;
            T002.Platform.Image.DeleteImage(m_imgDisableImage);
            m_imgDisableImage = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String normalpath = m_imgNormalImage == null ? "" : m_imgNormalImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String downpath = m_imgDownImage == null ? "" : m_imgDownImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String disablepath = m_imgDisableImage == null ? "" : m_imgDisableImage.Name.Substring(ProjectManager.Project.AssetsFolder.Length);

            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("NormalImage")).InnerText = normalpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("DownImage")).InnerText = downpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("DisableImage")).InnerText = disablepath;
        }
        
        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 按钮状态。
        /// </summary>
        internal enum ButtonState
        {
            /// <summary>
            /// 普通。
            /// </summary>
            Normal,

            /// <summary>
            /// 按下。
            /// </summary>
            Down
        }

        /// <summary>
        /// 按钮状态。
        /// </summary>
        internal ButtonState m_bsState = ButtonState.Normal;

        /// <summary>
        /// 普通状态图像。
        /// </summary>
        private T002.Platform.Image m_imgNormalImage = null;

        /// <summary>
        /// 按下状态图像。
        /// </summary>
        private T002.Platform.Image m_imgDownImage = null;

        /// <summary>
        /// 禁用状态图像。
        /// </summary>
        private T002.Platform.Image m_imgDisableImage = null;

        #endregion
    }
}
