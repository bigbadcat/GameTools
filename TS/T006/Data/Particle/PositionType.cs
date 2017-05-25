using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T006.Data.Particle
{
    /// <summary>
    /// 粒子的位置类型。
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// 自由模式，粒子发射后位置与粒子系统脱离，即粒子系统移动不能改变粒子位置。
        /// </summary>
        Free = 0,

        /// <summary>
        /// 关联模式，粒子发射后位置粒子系统绑定，即粒子系统移动所有粒子也会整体移动。
        /// </summary>
        Relative = 1
    }
}
