using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsGL.OpenGL;
using System.Windows.Forms;
using T006.Data.Particle;
using T006.Data;
using System.Drawing;

namespace T006.Forms
{
    /// <summary>
    /// 粒子视图，显示一个粒子系统。
    /// </summary>
    public class ParticleView : OpenGLControl
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ParticleView()
        {
        }

        /// <summary>
        /// 进行GL绘制。
        /// </summary>
        public override void glDraw()
        {
            //初始化变换
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            //清屏
            GL.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);
            if (m_bmpBackImage != null)
            {
                UInt32 oldtextures = 0;							//当前绑定的纹理
                GL.glGetIntegerv(GL.GL_TEXTURE_BINDING_2D, out oldtextures);		//先获得原来绑定的纹理编号，以便在最后进行恢复
                GL.glBindTexture(GL.GL_TEXTURE_2D, this.m_iBackTexID);
                GL.glColor4ub(255, 255, 255, 255);
                GL.glBegin(GL.GL_QUADS);
                GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex2f(0, 0);
                GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex2f(m_bmpBackImage.Width, 0);
                GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex2f(m_bmpBackImage.Width, m_bmpBackImage.Height);
                GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex2f(0, m_bmpBackImage.Height);
                GL.glEnd();
                GL.glBindTexture(GL.GL_TEXTURE_2D, oldtextures);
            }
            if (this.m_pfShow != null)
            {
                this.m_pfShow.EditParticle.Paint();
            }
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取或设置控件显示的粒子系统文件。
        /// </summary>
        public ParticleFile ShowParticle
        {
            get
            {
                return this.m_pfShow;
            }
            set
            {
                this.m_pfShow = value;
            }
        }

        /// <summary>
        /// 获取或设置控件显示的背景图像，若没有则为null。
        /// </summary>
        public Bitmap BackImage
        {
            get
            {
                return m_bmpBackImage;
            }
            set
            {
                if (m_bmpBackImage != null)
                {
                    ParticleEffect.DeleteTex(m_iBackTexID);
                    m_iBackTexID = 0;
                }
                m_bmpBackImage = value;
                if (m_bmpBackImage != null)
                {
                    m_iBackTexID = ParticleEffect.CreateTex(m_bmpBackImage);
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 创建控件。
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        /// <summary>
        /// 控件尺寸发生改变。
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            GL.glOrtho(0, this.Width, 0, this.Height, -1, 0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
        }

        /// <summary>
        /// 初始化GL环境。
        /// </summary>
        protected override void InitGLContext()
        {
            base.InitGLContext();

            //启用混色(用于半透明和通道)
            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

            //启用纹理(用于图像)
            GL.glEnable(GL.GL_TEXTURE_2D);
        }

        #endregion

        #region 数据成员=====================================================================================

        /// <summary>
        /// 背景图像。
        /// </summary>
        private Bitmap m_bmpBackImage = null;

        /// <summary>
        /// 背景图像GL材质ID。
        /// </summary>
        private UInt32 m_iBackTexID = 0;

        /// <summary>
        /// 要显示的粒子文件。
        /// </summary>
        private ParticleFile m_pfShow = null;

        #endregion
    }
}
