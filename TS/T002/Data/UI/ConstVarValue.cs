using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T002.Data.UI
{
    /// <summary>
    /// 常量值对象。
    /// </summary>
    public struct ConstVarValue
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="name">常量名称。</param>
        /// <param name="value">常量值。</param>
        public ConstVarValue(String name, Int32 value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 常量名称。
        /// </summary>
        public String Name;

        /// <summary>
        /// 常量值。
        /// </summary>
        public Int32 Value;
    }
}
