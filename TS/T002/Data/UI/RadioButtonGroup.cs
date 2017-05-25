using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T002.Data.UI
{
    /// <summary>
    /// 单选按钮组，用来管理一组互斥的单选按钮。
    /// </summary>
    internal class RadioButtonGroup
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="code">按钮组编号。</param>
        /// <param name="ui">从属的UI。</param>
        public RadioButtonGroup(int code, UserInterface ui)
        {
            m_iGroupCode = code;
            m_setGroupMember = new HashSet<RadioButton>();
            m_uiInterface = ui;
        }

        /// <summary>
        /// 向组内添加单选按钮。
        /// </summary>
        /// <param name="btn">要添加的单选按钮，重复则不添加。</param>
        public void AddRadioButton(RadioButton btn)
        {
            if (this.m_uiInterface == btn.Interface && this.m_iGroupCode == btn.GroupCode && !this.m_setGroupMember.Contains(btn))
            {
                this.m_setGroupMember.Add(btn);
            }
        }

        /// <summary>
        /// 从组内移除单选按钮。
        /// </summary>
        /// <param name="btn">要移除的单选按钮，不存在则什么也不做。</param>
        public void RemoveRadioButton(RadioButton btn)
        {
            if (this.m_rdbCheckedButton == btn)
            {
                this.m_rdbCheckedButton = null;
            }
            this.m_setGroupMember.Remove(btn);
        }

        /// <summary>
        /// 设置组内的某一按钮为选中状态。
        /// </summary>
        /// <param name="btn">要设置的按钮，若该按钮不在组内则什么也不做。</param>
        public void SetButtonChecked(RadioButton btn)
        {
            SetButtonChecked(btn, true);
        }

        /// <summary>
        /// 设置组内的某一按钮的选中状态。
        /// </summary>
        /// <param name="btn">要设置的按钮，若该按钮不在组内则什么也不做。</param>
        /// <param name="ck">选中状态。</param>
        public void SetButtonChecked(RadioButton btn, Boolean ck)
        {
            if (ck)
            {
                //如果设置按钮在组内且不是按下的按钮
                if (this.m_rdbCheckedButton != btn && this.m_setGroupMember.Contains(btn))
                {
                    if (this.m_rdbCheckedButton != null)
                    {
                        this.m_rdbCheckedButton.m_bChecked = false;
                        this.m_rdbCheckedButton.m_bsState = Button.ButtonState.Normal;
                    }
                    this.m_rdbCheckedButton = btn;
                    this.m_rdbCheckedButton.m_bChecked = true;
                    this.m_rdbCheckedButton.m_bsState = Button.ButtonState.Down;
                }
            }
            else
            {
                //如果是设置按下的按钮
                if (this.m_rdbCheckedButton == btn)
                {
                    this.m_rdbCheckedButton.m_bChecked = false;
                    this.m_rdbCheckedButton.m_bsState = Button.ButtonState.Normal;
                    this.m_rdbCheckedButton = null;
                }
            }
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取组所属的UI。
        /// </summary>
        public UserInterface Interface
        {
            get
            {
                return this.m_uiInterface;
            }
        }

        /// <summary>
        /// 获取组内选中的按钮。
        /// </summary>
        public RadioButton CheckedButton
        {
            get
            {
                return this.m_rdbCheckedButton;
            }
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 所在的组编号。
        /// </summary>
        private Int32 m_iGroupCode = 0;

        /// <summary>
        /// 按钮组所在的界面。
        /// </summary>
        private UserInterface m_uiInterface = null;

        /// <summary>
        /// 组内的按钮集合。
        /// </summary>
        private HashSet<RadioButton> m_setGroupMember = null;

        /// <summary>
        /// 选中的按钮。
        /// </summary>
        private RadioButton m_rdbCheckedButton = null;

        #endregion
    }
}
