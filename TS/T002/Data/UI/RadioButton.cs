using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using T002.Common;
using T002.Platform;
using System.IO;
using XuXiang.ClassLibrary;

namespace T002.Data.UI
{
    /// <summary>
    /// 单选按钮用于多选一。
    /// </summary>
    public class RadioButton : Button
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ui">控件所在的UI。</param>
        public RadioButton(UserInterface ui)
            : base(ui)
        {
            ui.GetRadioButtonGroup(this.m_iGroupCode).AddRadioButton(this);
        }

        /// <summary>
        /// 获取节点名称。
        /// </summary>
        /// <returns>节点名称。</returns>
        public override String GetNodeName()
        {
            return GetNodeText("[单选按钮]");
        }

        /// <summary>
        /// 已重载。获取表示控件的XML节点。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <returns>表示控件的XML节点</returns>
        public override XmlNode GetXmlNode(XmlDocument xmlDoc)
        {
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.Element, UserInterface.CONTROL_NAME_RADIOBUTTON, "");
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
            String strGroupID = XmlUtil.GetAttribute(xmlNode, "GroupCode");
            String strChecked = XmlUtil.GetAttribute(xmlNode, "Checked");

            this.GroupCode = strGroupID.Equals(String.Empty) ? 22 : Int32.Parse(strGroupID);
            this.Checked = strChecked.Equals(String.Empty) ? false : Boolean.Parse(strChecked);
        }

        /// <summary>
        /// 将控件写入到数据流中。
        /// </summary>
        /// <param name="stream">要写入到的数据流。</param>
        public override void WriteToStream(Stream stream)
        {
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(UserInterface.CONTROL_TYPE_ID_RADIOBUTTON));
            base.WriteToStream(stream);
            DataUtil.WriteBytes(stream, DataUtil.GetInt32Bytes(this.m_iGroupCode));
            stream.WriteByte(DataUtil.GetBooleanByte(this.m_bChecked));
        }

        /// <summary>
        /// 获取完整的程序常量。
        /// </summary>
        /// <returns>带类型前缀的程序常量。</returns>
        public override String GetFullConstVar()
        {
            return "RDB_" + this.ConstVar;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置所在的组编号。
        /// </summary>
        public Int32 GroupCode
        {
            get
            {
                return this.m_iGroupCode;
            }
            set
            {
                if (this.m_iGroupCode != value)
                {
                    this.m_bChecked = false;
                    this.m_bsState = ButtonState.Normal;
                    this.Interface.GetRadioButtonGroup(this.m_iGroupCode).RemoveRadioButton(this);
                    this.m_iGroupCode = value;
                    this.Interface.GetRadioButtonGroup(this.m_iGroupCode).AddRadioButton(this);
                }
            }
        }

        /// <summary>
        /// 获取或设置按钮是否选中。
        /// </summary>
        public Boolean Checked
        {
            get
            {
                return this.m_bChecked;
            }
            set
            {
                this.Interface.GetRadioButtonGroup(this.m_iGroupCode).SetButtonChecked(this, value);
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~RadioButton()
        {
            this.Interface.GetRadioButtonGroup(this.m_iGroupCode).RemoveRadioButton(this);
        }

        /// <summary>
        /// 已重载。设置XML节点的属性。
        /// </summary>
        /// <param name="xmlDoc">XML文档对象。</param>
        /// <param name="xmlNode">XML节点。</param>
        protected override void SetXmlNodeAttribute(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            base.SetXmlNodeAttribute(xmlDoc, xmlNode);
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("GroupCode")).InnerText = this.m_iGroupCode.ToString();
            xmlNode.Attributes.Append(xmlDoc.CreateAttribute("Checked")).InnerText = this.m_bChecked.ToString();
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 所在的组编号。
        /// </summary>
        private Int32 m_iGroupCode = 0;

        /// <summary>
        /// 按钮是否选中。
        /// </summary>
        internal Boolean m_bChecked = false;

        #endregion
    }
}
