using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using T002.Common;
using System.IO;
using T002.Platform;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 复选按钮，提供选中可不选中两种状态。
    /// </summary>
    public class CheckButton : Button
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public CheckButton(UserInterface ui)
            : base(ui)
        {
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[复选按钮]");
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_CHECKBUTTON, "");
            this.SetXmlNodeAttribute(xmlDoc, xmlNode);
            return xmlNode;
        }

        /// <summary>
        /// 已重载。从XML节点中赋值。
        /// </summary>
        /// <param name="xmlNode">XML节点。</param>
        public override void AssignFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            base.AssignFromXmlNode(xmlNode);
            String strChecked = XmlUtil.GetAttribute(xmlNode, "Checked");

            this.Checked = strChecked.Equals(String.Empty) ? false : Boolean.Parse(strChecked);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_CHECKBUTTON));
            base.WriteToStream(stream);
            stream.WriteByte(DataUtil.GetBooleanByte(this.m_bChecked));
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "CKB_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置按钮的选中状态。
        /// </summary>
        public Boolean Checked
        {
            get
            {
                return this.m_bChecked;
            }
            set
            {
                if (this.m_bChecked != value)
                {
                    this.m_bChecked = value;
                    this.m_bsState = this.m_bChecked ? ButtonState.Down : ButtonState.Normal;
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Checked")).InnerText = this.m_bChecked.ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 按钮是否选中。
        /// </summary>
        private Boolean m_bChecked = false;

        #endregion
    }
}
