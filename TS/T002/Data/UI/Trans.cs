using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T002.Data.UI
{
    /// <summary>
    /// 变换方式。
    /// </summary>
    public enum Trans
    {
        /// <summary>
        /// 无变化。
        /// </summary>
        None = 0,

        /// <summary>
        /// 水平翻转后顺时针旋转180度（垂直翻转）。
        /// </summary>
        MirrorRot180 = 1,

        /// <summary>
        /// 水平翻转。
        /// </summary>
        Mirror = 2,

        /// <summary>
        /// 顺时针旋转180度。
        /// </summary>
        Rot180 = 3,

        /// <summary>
        /// 顺时针旋转90度。
        /// </summary>
        Rot90 = 4,

        /// <summary>
        /// 水平翻转后顺时针旋转90度。
        /// </summary>
        MirrorRot90 = 5,

        /// <summary>
        /// 水平翻转后顺时针旋转270度。
        /// </summary>
        MirrorRot270 = 6,

        /// <summary>
        /// 顺时针旋转90度。
        /// </summary>
        Rot270 = 7
    }
}
