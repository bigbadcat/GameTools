using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using T002.Platform;
using System.Drawing;
using System.IO;
using T002.Common;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 用户界面类。
    /// </summary>
    public class UserInterface
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 从XML文件加载UI对象。
        /// </summary>
        /// <param name="file">XML文件路径。</param>
        /// <returns>UI对象。</returns>
        public static UserInterface LoadFromXmlFile(String file)
        {
            //打开XML文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);

            //读入界面基本信息
            XmlNode xmlInterface = xmlDoc.SelectSingleNode("Interface");
            String strCode = XmlUtil.GetAttribute(xmlInterface, "Code");
            int code = strCode.Equals(String.Empty) ? 0 : Int32.Parse(strCode);
            Int32 width = Int32.Parse(XmlUtil.GetAttribute(xmlInterface, "Width"));
            Int32 height = Int32.Parse(XmlUtil.GetAttribute(xmlInterface, "Height"));
            UserInterface ui = new UserInterface(code, width, height);

            //Root节点
            XmlNode xmlRoot = xmlInterface.FirstChild;
            ui.m_ctrRoot = LoadControlFromXmlNode(ui, xmlRoot.FirstChild) as Container;
            return ui;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="code">界面Code。</param>
        /// <param name="width">界面宽度。</param>
        /// <param name="height">界面高度。</param>
        public UserInterface(int code, Int32 width, Int32 height)
        {
            this.m_iCode = code;
            this.m_iWidth = width;
            this.m_iHeight = height;
            this.m_ctrRoot = new Container(this);
            this.m_ctrRoot.Code = 100;
            this.m_ctrRoot.Name = "Root";
            this.m_ctrRoot.Bounds = new Rect(0, 0, width, height);
            this.m_dicRadioButtonGroups = new Dictionary<Int32, RadioButtonGroup>();
        }

        /// <summary>
        /// 设置UI尺寸。
        /// </summary>
        /// <param name="width">新的宽度。</param>
        /// <param name="height">新的高度。</param>
        public void SetSize(Int32 width, Int32 height)
        {
            if (width > 0 && height > 0)
            {
                Single ws = width * 1.0f / this.m_ctrRoot.Width;
                Single hs = height * 1.0f / this.m_ctrRoot.Height;
                this.m_iWidth = width;
                this.m_iHeight = height;
                ScaleContainerSize(this.m_ctrRoot, ws, hs);
            }
        }

        /// <summary>
        /// 获取UI的某单选按钮组。
        /// </summary>
        /// <param name="code">按钮组的编号，若该组不存在则创建。</param>
        /// <returns>单选按钮组。</returns>
        internal RadioButtonGroup GetRadioButtonGroup(Int32 code)
        {
            RadioButtonGroup rbg = null;
            if (m_dicRadioButtonGroups.ContainsKey(code))
            {
                rbg = m_dicRadioButtonGroups[code];
            }
            else
            {
                rbg = new RadioButtonGroup(code, this);
                m_dicRadioButtonGroups.Add(code, rbg);
            }

            return rbg;
        }

        /// <summary>
        /// 将界面返回成XML的Interface节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档。</param>
        /// <returns>XML的Interface节点</returns>
        public XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            //Interface根节点
            XmlNode xmlInterface = xmlDoc.CreateNode(XmlNodeType.Element, "Interface", "");
            xmlInterface.Attributes.Append(xmlDoc.CreateAttribute("Code")).InnerText = m_iCode.ToString();
            xmlInterface.Attributes.Append(xmlDoc.CreateAttribute("Width")).InnerText = m_iWidth.ToString();
            xmlInterface.Attributes.Append(xmlDoc.CreateAttribute("Height")).InnerText = m_iHeight.ToString();
            xmlDoc.AppendChild(xmlInterface);

            //Root节点和组成Root的容器
            XmlNode xmlRoot = xmlDoc.CreateNode(XmlNodeType.Element, "Root", "");
            xmlRoot.AppendChild(m_ctrRoot.GetXmlNode(xmlDoc));
            xmlInterface.AppendChild(xmlRoot);

            return xmlInterface;
        }

        /// <summary>
        /// 绘制界面。
        /// </summary>
        /// <param name="c">绘制的画布</param>
        /// <param name="p">所在容器的坐标。</param>
        public void Paint(Canvas c, Point p)
        {
            m_ctrRoot.Paint(c, p);
        }

        /// <summary>
        /// 从XML节点中加载控件。
        /// </summary>
        /// <param name="ui">控件所在的界面。</param>
        /// <param name="xmlNode">XML节点。</param>
        /// <returns>加载的控件。</returns>
        public static Control LoadControlFromXmlNode(UserInterface ui, XmlNode xmlNode)
        {
            Control ctr = null;
            switch (xmlNode.Name)
            {
                case CONTROL_NAME_CONTAINER:
                    ctr = new Container(ui);
                    break;
                case CONTROL_NAME_PICTURE:
                    ctr = new Picture(ui);
                    break;
                case CONTROL_NAME_LABEL:
                    ctr = new Label(ui);
                    break;
                case CONTROL_NAME_SINGLEBUTTON:
                    ctr = new SingleButton(ui);
                    break;
                case CONTROL_NAME_SCROLLTABLE:
                    ctr = new ScrollTable(ui, null);
                    break;
                case CONTROL_NAME_RADIOBUTTON:
                    ctr = new RadioButton(ui);
                    break;
                case CONTROL_NAME_CHECKBUTTON:
                    ctr = new CheckButton(ui);
                    break;
                case CONTROL_NAME_PAGETABLE:
                    ctr = new PageTable(ui, null);
                    break;
                case CONTROL_NAME_SCROLLPANEL:
                    ctr = new ScrollPanel(ui, null);
                    break;
                case CONTROL_NAME_NUMBERIMAGE:
                    ctr = new NumberImage(ui);
                    break;
                case CONTROL_NAME_TEXTAREA:
                    ctr = new TextArea(ui);
                    break;
                case CONTROL_NAME_IMAGENUMBER:
                    ctr = new ImageNumber(ui);
                    break;
                case CONTROL_NAME_TEXTBOX:
                    ctr = new TextBox(ui);
                    break;
                case CONTROL_NAME_PROGRESSBAR:
                    ctr = new ProgressBar(ui);
                    break;
                case CONTROL_NAME_SLIDERBAR:
                    ctr = new SliderBar(ui);
                    break;
                case CONTROL_NAME_PARTICLEVIEW:
                    ctr = new ParticleView(ui);
                    break;
                case CONTROL_NAME_SPINEVIEW:
                    ctr = new SpineView(ui);
                    break;
                default:
                    break;
            }
            ctr.AssignFromXmlNode(xmlNode);
            return ctr;
        }

        /// <summary>
        /// 获取某个点对应的控件。
        /// </summary>
        /// <param name="p">指定点。</param>
        /// <returns>对应控件。</returns>
        public Control GetControlAtPoint(Point p)
        {
            return this.m_ctrRoot.GetControlAtPoint(p);
        }

        /// <summary>
        /// 将用户界面写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iCode));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iWidth));
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iHeight));
            this.m_ctrRoot.WriteToStream(stream);
        }

        /// <summary>
        /// 获取有程序常量的控件集合。
        /// </summary>
        /// <returns>控件集合</returns>
        public SortedList<String, List<Control>> GetConstVarSet()
        {
            SortedList<String, List<Control>> controlset = new SortedList<string, List<Control>>();
            GetControlConstVar(this.m_ctrRoot, controlset);
            return controlset;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置界面编号。
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
        /// 获取或设置界面宽度。
        /// </summary>
        public Int32 Width
        {
            get
            {
                return this.m_iWidth;
            }
            set
            {
                this.m_iWidth = value;
            }
        }

        /// <summary>
        /// 获取或设置界面高度。
        /// </summary>
        public Int32 Height
        {
            get
            {
                return this.m_iHeight;
            }
            set
            {
                this.m_iHeight = value;
            }
        }

        /// <summary>
        /// 获取根控件。
        /// </summary>
        public Container Root
        {
            get
            {
                return this.m_ctrRoot;
            }
        }

        #endregion

        #region 控件常量=====================================================================================

        /// <summary>
        /// 控件名称：容器。
        /// </summary>
        public const String CONTROL_NAME_CONTAINER = "Container";

        /// <summary>
        /// 控件名称：图像。
        /// </summary>
        public const String CONTROL_NAME_PICTURE = "Picture";

        /// <summary>
        /// 控件名称：标签。
        /// </summary>
        public const String CONTROL_NAME_LABEL = "Label";

        /// <summary>
        /// 控件名称：普通按钮。
        /// </summary>
        public const String CONTROL_NAME_SINGLEBUTTON = "SingleButton";

        /// <summary>
        /// 控件名称：滑动表格。
        /// </summary>
        public const String CONTROL_NAME_SCROLLTABLE = "ScrollTable";

        /// <summary>
        /// 控件名称：单选按钮。
        /// </summary>
        public const String CONTROL_NAME_RADIOBUTTON = "RadioButton";

        /// <summary>
        /// 控件名称：复选按钮。
        /// </summary>
        public const String CONTROL_NAME_CHECKBUTTON = "CheckButton";

        /// <summary>
        /// 控件名称：翻页表格。
        /// </summary>
        public const String CONTROL_NAME_PAGETABLE = "PageTable";

        /// <summary>
        /// 控件名称：滚动面板。
        /// </summary>
        public const String CONTROL_NAME_SCROLLPANEL = "ScrollPanel";

        /// <summary>
        /// 控件名称：数字图像。
        /// </summary>
        public const String CONTROL_NAME_NUMBERIMAGE = "NumberImage";

        /// <summary>
        /// 控件名称：文本域。
        /// </summary>
        public const String CONTROL_NAME_TEXTAREA = "TextArea";

        /// <summary>
        /// 控件名称：图像数字。
        /// </summary>
        public const String CONTROL_NAME_IMAGENUMBER = "ImageNumber";

        /// <summary>
        /// 控件名称：文本框。
        /// </summary>
        public const String CONTROL_NAME_TEXTBOX = "TextBox";

        /// <summary>
        /// 控件名称：进度条。
        /// </summary>
        public const String CONTROL_NAME_PROGRESSBAR = "ProgressBar";

        /// <summary>
        /// 控件名称：滑动条。
        /// </summary>
        public const String CONTROL_NAME_SLIDERBAR = "SliderBar";

        /// <summary>
        /// 控件名称：粒子视图。
        /// </summary>
        public const String CONTROL_NAME_PARTICLEVIEW = "ParticleView";

        /// <summary>
        /// 控件名称：精灵视图。
        /// </summary>
        public const String CONTROL_NAME_SPINEVIEW = "SpineView";

        /// <summary>
        /// 控件类型编号：容器。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_CONTAINER = 0;

        /// <summary>
        /// 控件类型编号：图像。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_PICTURE = 1;

        /// <summary>
        /// 控件类型编号：标签。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_LABEL = 2;

        /// <summary>
        /// 控件类型编号：普通按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_SINGLEBUTTON = 3;

        /// <summary>
        /// 控件类型编号：滑动表格。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_SCROLLTABLE = 4;

        /// <summary>
        /// 控件类型编号：单选按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_RADIOBUTTON = 5;

        /// <summary>
        /// 控件类型编号：复选按钮。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_CHECKBUTTON = 6;

        /// <summary>
        /// 控件类型编号：翻页表格。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_PAGETABLE = 7;

        /// <summary>
        /// 控件类型编号：滚动面板。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_SCROLLPANEL = 8;

        /// <summary>
        /// 控件类型编号：数字图像。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_NUMBERIMAGE = 9;

        /// <summary>
        /// 控件类型编号：文本域。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_TEXTAREA = 10;

        /// <summary>
        /// 控件类型编号：图像数字。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_IMAGENUMBER = 11;

        /// <summary>
        /// 控件类型编号：文本框。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_TEXTBOX = 12;

        /// <summary>
        /// 控件类型编号：进度条。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_PROGRESSBAR = 13;

        /// <summary>
        /// 控件类型编号：滑动条。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_SLIDERBAR = 14;

        /// <summary>
        /// 控件类型编号：粒子视图。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_PARTICLEVIEW = 15;

        /// <summary>
        /// 控件类型编号：精灵视图。
        /// </summary>
        public const Int32 CONTROL_TYPE_ID_SPINEVIEW = 16;

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 获取控件的常量。
        /// </summary>
        /// <param name="ctr">要获取常量的控件。</param>
        /// <param name="controlset">保存的常量对应控件的集合。</param>
        protected static void GetControlConstVar(Control ctr, SortedList<String, List<Control>> controlset)
        {
            if (!ctr.ConstVar.Equals(String.Empty))
            {
                String name = ctr.GetFullConstVar();
                if (controlset.ContainsKey(name))
                {
                    controlset[name].Add(ctr);
                }
                else
                {
                    List<Control> ctrset = new List<Control>();
                    ctrset.Add(ctr);
                    controlset.Add(name, ctrset);
                }
            }
            if (ctr is Container)
            {
                Container con = ctr as Container;
                foreach (Control tmp in con.ChildList)
                {
                    GetControlConstVar(tmp, controlset);
                }
            }
            else if (ctr is Table)
            {
                Container con = (ctr as Table).Prototype;
                GetControlConstVar(con, controlset);
            }
            else if (ctr is ScrollPanel)
            {
                Container con = (ctr as ScrollPanel).Child;
                GetControlConstVar(con, controlset);
            }
        }

        /// <summary>
        /// 缩放容器尺寸。
        /// </summary>
        /// <param name="con">要缩放的容器。</param>
        /// <param name="ws">水平缩放比例。</param>
        /// <param name="hs">竖直缩放比例。</param>
        protected static void ScaleContainerSize(Container con, Single ws, Single hs)
        {
            con.Position = new Point((Int32)(con.Left * ws), (Int32)(con.Bottom * hs));
            con.Size = new Size((Int32)(con.Width * ws), (Int32)(con.Height * hs));
            foreach (Control ctr in con.ChildList)
            {
                if (ctr is Container)
                {
                    ScaleContainerSize(ctr as Container, ws, hs);
                }
                else
                {
                    ctr.Position = new Point((Int32)(ctr.Left * ws), (Int32)(ctr.Bottom * hs));
                    ctr.Size = new Size((Int32)(ctr.Width * ws), (Int32)(ctr.Height * hs));
                }
            }
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 界面编号。
        /// </summary>
        private Int32 m_iCode = 0;

        /// <summary>
        /// 界面的宽度。
        /// </summary>
        private Int32 m_iWidth = 0;

        /// <summary>
        /// 界面的高度。
        /// </summary>
        private Int32 m_iHeight = 0;

        /// <summary>
        /// 根节点。
        /// </summary>
        private Container m_ctrRoot = null;

        /// <summary>
        /// 单选按钮组列表。
        /// </summary>
        private Dictionary<Int32, RadioButtonGroup> m_dicRadioButtonGroups = null;

        #endregion
    }
}
