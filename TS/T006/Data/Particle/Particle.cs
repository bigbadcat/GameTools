using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T006.Data.Particle
{
    /// <summary>
    /// 保存一颗发射后的粒子数据。
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Particle()
        {
        }

        //位置
        internal Single m_posX= 0;
        internal Single m_posY= 0;
        internal Single m_startPosX= 0;
        internal Single m_startPosY= 0;

        //颜色
        internal Single m_colorA= 0;
        internal Single m_colorR= 0;
        internal Single m_colorG= 0;
        internal Single m_colorB= 0;
        internal Single m_deltaColorA= 0;
        internal Single m_deltaColorR= 0;
        internal Single m_deltaColorG= 0;
        internal Single m_deltaColorB= 0;

        //尺寸
        internal Single m_size= 0;
        internal Single m_deltaSize= 0;

        //角度
        internal Single m_rotation= 0;
        internal Single m_deltaRotation= 0;

        //生命
        internal Single m_timeToLive= 0;

        //! Mode A: gravity, direction, radial accel, tangential accel
        internal Single m_dirX= 0;
        internal Single m_dirY= 0;
        internal Single m_radialAccel= 0;
        internal Single m_tangentialAccel= 0;

        //! Mode B: radius mode
        internal Single m_angle= 0;
        internal Single m_degreesPerSecond= 0;
        internal Single m_radius= 0;
        internal Single m_deltaRadius= 0;
    }
}
