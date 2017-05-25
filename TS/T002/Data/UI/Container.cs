using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Drawing;
using T002.Common;
using T002.Platform;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 容器类，用于将多个控件组合起来，容器内的坐标相对于容器左上角。
    /// </summary>
    public class Container : Control
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public Container(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_CONTAINER, "");
            this.SetXmlNodeAttribute(xmlDoc, xmlNode);
            return xmlNode;
        }

        /// <summary>
        /// 已重载。绘制容器。
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
            Int32 cx = p.X + this.Left;
            Point cp = new Point(cx, p.Y + this.Bottom);
            Rect rt = new Rect(cp, this.Size);
            base.Paint(c, p);
            if (m_imgBack != null)
            {
                c.DrawImage(m_imgBack, rt, m_tBackTrans, m_cBackChannel);
            }
            if (m_imgFrame != null)
            {
                c.DrawImageNinePatch(m_imgFrame, rt, m_cFrameChannel);
            }

            //绘制子控件
            if (m_bClipping)
            {
                c.Save();
                c.SetClip(rt);
            }
            foreach (Control tmp in this.m_alChildList)
            {
                tmp.Paint(c, cp);
            }
            if (m_bClipping)
            {
                c.Restore();
            }
        }

        /// <summary>
        /// 向容器中添加子控件，若已经是子控件则不做任何操作。
        /// </summary>
        /// <param name="c">要添加的子控件。</param>
        public void AddChild(Control c)
        {
            if (!this.m_alChildList.Contains(c))
            {
                c.Parent = this;
                this.m_alChildList.Add(c);
            }
        }

        /// <summary>
        /// 向容器中添加子控件到某个控件前面，若已经是子控件则不做任何操作。
        /// </summary>
        /// <param name="cf">要参照的子控件。</param>
        /// <param name="c">要添加的子控件。</param>
        public void AddChildAbove(Control move, Control target)
        {
            if (!m_alChildList.Contains(move))
            {
                for (Int32 i = 0; i < this.m_alChildList.Count; ++i)
                {
                    if (this.m_alChildList[i] == target)
                    {
                        move.Parent = this;
                        this.m_alChildList.Insert(i, move);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 向容器中添加子控件到某个控件后面，若已经是子控件则不做任何操作。
        /// </summary>
        /// <param name="cf">要参照的子控件。</param>
        /// <param name="c">要添加的子控件。</param>
        public void AddChildBelow(Control cf, Control c)
        {
            if (!m_alChildList.Contains(c))
            {
                for (Int32 i = 0; i < this.m_alChildList.Count; ++i)
                {
                    if (this.m_alChildList[i] == cf)
                    {
                        c.Parent = this;
                        this.m_alChildList.Insert(i + 1, c);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 从容器中移除子控件。
        /// </summary>
        /// <param name="c">要移除的子控件。</param>
        public void RemoveChild(Control c)
        {
            this.m_alChildList.Remove(c);
            c.Parent = null;
        }

        /// <summary>
        /// 将子控件往上移动一层。
        /// </summary>
        /// <param name="c">要移动的子控件。</param>
        public void MoveTop(Control c)
        {
            Int32 sz = this.m_alChildList.Count - 1;
            for (Int32 i = 0; i < sz; ++i)
            {
                Control cc = this.m_alChildList[i];
                if (cc == c)
                {
                    this.m_alChildList[i] = this.m_alChildList[i + 1];
                    this.m_alChildList[i + 1] = cc;
                    break;
                }
            }
        }

        /// <summary>
        /// 将子控件往下移动一层。
        /// </summary>
        /// <param name="c">要移动的子控件。</param>
        public void MoveBottom(Control c)
        {
            Int32 sz = this.m_alChildList.Count;
            for (Int32 i = 1; i < sz; ++i)
            {
                Control cc = this.m_alChildList[i];
                if (cc == c)
                {
                    this.m_alChildList[i] = this.m_alChildList[i - 1];
                    this.m_alChildList[i - 1] = cc;
                    break;
                }
            }
        }

        /// <summary>
        /// 将子控件移动到最顶端。
        /// </summary>
        /// <param name="c">要移动的子控件。</param>
        public void MoveTopmost(Control c)
        {
            Int32 sz = this.m_alChildList.Count;
            for (int i = 0; i < sz; ++i)
            {
                Control cc = this.m_alChildList[i];
                if (cc == c)
                {
                    this.m_alChildList.RemoveAt(i);
                    this.m_alChildList.Add(cc);
                    break;
                }
            }
        }

        /// <summary>
        /// 将子控件移动到最底端。
        /// </summary>
        /// <param name="c">要移动的子控件。</param>
        public void MoveBottommost(Control c)
        {
            Int32 sz = this.m_alChildList.Count;
            for (int i = 0; i < sz; ++i)
            {
                Control cc = this.m_alChildList[i];
                if (cc == c)
                {
                    this.m_alChildList.RemoveAt(i);
                    this.m_alChildList.Insert(0, cc);
                    break;
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
            String strBack = XmlUtil.GetAttribute(xmlNode, "Back");
            String strBackChannel = XmlUtil.GetAttribute(xmlNode, "BackChannel");
            String strBackTrans = XmlUtil.GetAttribute(xmlNode, "BackTrans");
            String strFrame = XmlUtil.GetAttribute(xmlNode, "Frame");
            String strFrameChannel = XmlUtil.GetAttribute(xmlNode, "FrameChannel");
            String strClipping = XmlUtil.GetAttribute(xmlNode, "Clipping");

            this.m_imgBack = strBack == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strBack);
            this.m_cBackChannel = strBackChannel == String.Empty ? Color.White : DataUtil.ParseColor(strBackChannel);
            this.m_tBackTrans = strBackTrans.Equals(String.Empty) ? Trans.None : (Trans)Int32.Parse(strBackTrans);
            this.m_imgFrame = strFrame == String.Empty ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + strFrame);
            this.m_cFrameChannel = strFrameChannel == String.Empty ? Color.White : DataUtil.ParseColor(strFrameChannel);
            this.m_bClipping = strClipping.Equals(String.Empty) ? false : Boolean.Parse(strClipping);

            //读入子控件
            XmlNode xmlChildList = xmlNode.SelectSingleNode("ChildList");
            this.m_alChildList.Clear();
            foreach (XmlNode tmp in xmlChildList.ChildNodes)
            {
                Control con = UserInterface.LoadControlFromXmlNode(this.Interface, tmp);
                con.Parent = this;
                this.m_alChildList.Add(con);
            }
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[容器]");
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

            Point cp = new Point(p.X - this.X, p.Y - this.Y);
            for (int i = this.m_alChildList.Count - 1; i >= 0; --i)
            {
                Control c = this.m_alChildList[i].GetControlAtPoint(cp);
                if (c != null)
                {
                    return c;
                }
            }

            return this;
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_CONTAINER));
            base.WriteToStream(stream);
            String backpath = m_imgBack == null ? "" : m_imgBack.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String framepath = m_imgFrame == null ? "" : m_imgFrame.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(backpath.Replace("\\", "/")));
            DataUtil.WriteColor(stream, m_cBackChannel);
            stream.WriteByte((byte)m_tBackTrans);
            DataUtil.WriteBytes(stream, DataUtil.GetStringBytes(framepath.Replace("\\", "/")));
            DataUtil.WriteColor(stream, m_cFrameChannel);
            DataUtil.WriteBoolean(stream, m_bClipping);

            //写入子控件
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(m_alChildList.Count));
            foreach (Control ctr in m_alChildList)
            {
                ctr.WriteToStream(stream);
            }
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "CON_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取子控件数量。
        /// </summary>
        public Int32 ChildCount
        {
            get
            {
                return this.m_alChildList.Count;
            }
        }

        /// <summary>
        /// 获取子控件列表。
        /// </summary>
        public List<Control> ChildList
        {
            get
            {
                return this.m_alChildList;
            }
        }

        /// <summary>
        /// 获取或设置容器背景图像。
        /// </summary>
        public T002.Platform.Image BackImage
        {
            get
            {
                return m_imgBack;
            }
            set
            {
                if (m_imgBack != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgBack);
                    m_imgBack = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置容器背景图像颜色通道。
        /// </summary>
        public Color BackChannel
        {
            get
            {
                return this.m_cBackChannel;
            }
            set
            {
                this.m_cBackChannel = value;
            }
        }

        /// <summary>
        /// 获取或设置背景图像的变换方式。
        /// </summary>
        public Trans BackTrans
        {
            get
            {
                return this.m_tBackTrans;
            }
            set
            {
                this.m_tBackTrans = value;
            }
        }

        /// <summary>
        /// 获取或设置容器框架图像。
        /// </summary>
        public T002.Platform.Image FrameImage
        {
            get
            {
                return m_imgFrame;
            }
            set
            {
                if (m_imgFrame != value)
                {
                    T002.Platform.Image.DeleteImage(m_imgFrame);
                    m_imgFrame = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置容器框架图像颜色通道。
        /// </summary>
        public Color FrameChannel
        {
            get
            {
                return this.m_cFrameChannel;
            }
            set
            {
                this.m_cFrameChannel = value;
            }
        }

        /// <summary>
        /// 获取或设置是否裁剪子控件。
        /// </summary>
        public Boolean Clipping
        {
            get
            {
                return m_bClipping;
            }
            set
            {
                m_bClipping = value;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~Container()
        {
            T002.Platform.Image.DeleteImage(m_imgBack);
            m_imgBack = null;
            T002.Platform.Image.DeleteImage(m_imgFrame);
            m_imgFrame = null;
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            String backpath = m_imgBack == null ? "" : m_imgBack.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            String framepath = m_imgFrame == null ? "" : m_imgFrame.Name.Substring(ProjectManager.Project.AssetsFolder.Length);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Back")).InnerText = backpath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BackChannel")).InnerText = DataUtil.ToStringValue(m_cBackChannel);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("BackTrans")).InnerText = ((Int32)m_tBackTrans).ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Frame")).InnerText = framepath;
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("FrameChannel")).InnerText = DataUtil.ToStringValue(m_cFrameChannel);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Clipping")).InnerText = this.m_bClipping.ToString();

            //写入子控件
            XmlNode xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "ChildList", "");
            foreach (Control c in this.m_alChildList)
            {
                xmlChild.AppendChild(c.GetXmlNode(xmlDoc));
            }
            xmlNode.AppendChild(xmlChild);
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 子控件列表。
        /// </summary>
        private List<Control> m_alChildList = new List<Control>();

        /// <summary>
        /// 容器背景图像。
        /// </summary>
        private T002.Platform.Image m_imgBack = null;

        /// <summary>
        /// 容器背景图像颜色通道。
        /// </summary>
        private Color m_cBackChannel = Color.White;

        /// <summary>
        /// 背景图像的变换方式。
        /// </summary>
        private Trans m_tBackTrans = Trans.None;

        /// <summary>
        /// 容器框架图像。
        /// </summary>
        private T002.Platform.Image m_imgFrame = null;

        /// <summary>
        /// 容器框架图像颜色通道。
        /// </summary>
        private Color m_cFrameChannel = Color.White;

        //是否对子控件进行裁剪
        private Boolean m_bClipping = false;

        #endregion
    }
}
