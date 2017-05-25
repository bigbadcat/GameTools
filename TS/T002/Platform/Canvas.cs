using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using T002.Data.UI;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using XuXiang.ClassLibrary;

namespace T002.Platform
{
    /// <summary>
    /// 画布。
    /// </summary>
    public class Canvas
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="g">绘图设备。</param>
        /// <param name="w">画布宽度。</param>
        /// <param name="h">画布高度。</param>
        public Canvas(Graphics g, Int32 w, Int32 h)
        {
            m_gDraw = g;
            m_iWidth = w;
            m_iHeight = h;
        }

        /// <summary>
        /// 将整个画布清空成某种颜色。
        /// </summary>
        /// <param name="c">清空颜色，忽略Alpha成分</param>
        public void Clear(Color c)
        {
            m_gDraw.Clear(c);
        }

        /// <summary>
        /// 在画布上绘制一条直线。
        /// </summary>
        /// <param name="p1">直线的起点。</param>
        /// <param name="p2">直线的终点。</param>
        /// <param name="c">直线颜色。</param>
        public void DrawLine(Point p1, Point p2, Color c)
        {
            p1.Y = m_iHeight - p1.Y;
            p2.Y = m_iHeight - p2.Y;
            m_gDraw.DrawLine(new Pen(c), p1, p2);
        }

        /// <summary>
        /// 绘制矩形框。
        /// </summary>
        /// <param name="rt">矩形框位置。</param>
        /// <param name="c">矩形框颜色。</param>
        public void DrawRect(Rect rt, Color c)
        {
            Rectangle drt = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            m_gDraw.DrawRectangle(new Pen(c), drt);
        }

        /// <summary>
        /// 填充矩形框。
        /// </summary>
        /// <param name="rt">矩形框位置。</param>
        /// <param name="c">矩形框填充颜色。</param>
        public void FillRect(Rect rt, Color c)
        {
            Rectangle drt = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            m_gDraw.FillRectangle(new SolidBrush(c), drt);
        }

        /// <summary>
        /// 往画布上绘制一张图像，基于图像左下角。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="p">图像左上角坐标。</param>
        public void DrawImage(Image img, Point p)
        {
            Point dp = new Point(p.X, m_iHeight - p.Y - img.Height - 1);
            m_gDraw.DrawImage(img.Content, dp);
        }

        /// <summary>
        /// 往画布上绘制一张图像，可以设置锚点、缩放比和变换方式。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="p">图像锚点坐标。</param>
        /// <param name="zm">图像的缩放比例。</param>
        /// <param name="a">图像绘制的锚点。</param>
        /// <param name="t">图像的变换方式。</param>
        public void DrawImage(Image img, Point p, Single zm, Align a, Trans t)
        {
            Int32 dx = p.X;
            Int32 dy = m_iHeight - p.Y;
            Int32 dw = (Int32)(img.Width * zm);
            Int32 dh = (Int32)(img.Height * zm);

            //有四种变换会宽高互换
            if (t == Trans.Rot90 || t == Trans.MirrorRot90 || t == Trans.MirrorRot270 || t == Trans.Rot270)
            {
                Int32 tmp = dw;
                dw = dh;
                dh = tmp;
            }

            //水平偏移
            if (a == Align.Top || a == Align.Center || a == Align.Bottom)
            {
                dx -= dw / 2;
            }
            else if (a == Align.TopRight || a == Align.Right || a == Align.BottomRight)
            {
                dx -= dw;
            }

            //竖直偏移
            if (a == Align.Left || a == Align.Center || a == Align.Right)
            {
                dy -= dh / 2;
            }
            else if (a == Align.BottomLeft || a == Align.Bottom || a == Align.BottomRight)
            {
                dy -= dh;
            }

            //图像变换
            Rectangle rt = new Rectangle(0, 0, img.Content.Width, img.Content.Height);
            System.Drawing.Point[] points = GetTranPoints(dx, dy, dw, dh, t);
            m_gDraw.DrawImage(img.Content, points, rt, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 往画布上绘制一张图像，可以设置锚点、缩放比和变换方式。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="p">图像锚点坐标。</param>
        /// <param name="scalex">图像的x缩放比例。</param>
        /// <param name="scaley">图像的y缩放比例。</param>
        /// <param name="a">图像绘制的锚点。</param>
        /// <param name="t">图像的变换方式。</param>
        /// <param name="channel">颜色通道。</param>
        public void DrawImage(Image img, Point p, Single sx, Single sy, Align a, Trans t, Color channel)
        {
            Int32 dx = p.X;
            Int32 dy = m_iHeight - p.Y;
            Int32 dw = (Int32)(img.Width * sx);
            Int32 dh = (Int32)(img.Height * sy);

            //有四种变换会宽高互换
            if (t == Trans.Rot90 || t == Trans.MirrorRot90 || t == Trans.MirrorRot270 || t == Trans.Rot270)
            {
                Int32 tmp = dw;
                dw = dh;
                dh = tmp;
            }

            //水平偏移
            if (a == Align.Top || a == Align.Center || a == Align.Bottom)
            {
                dx -= dw / 2;
            }
            else if (a == Align.TopRight || a == Align.Right || a == Align.BottomRight)
            {
                dx -= dw;
            }

            //竖直偏移
            if (a == Align.Left || a == Align.Center || a == Align.Right)
            {
                dy -= dh / 2;
            }
            else if (a == Align.BottomLeft || a == Align.Bottom || a == Align.BottomRight)
            {
                dy -= dh;
            }

            //图像变换
            Rectangle rt = new Rectangle(0, 0, img.Content.Width, img.Content.Height);
            System.Drawing.Point[] points = GetTranPoints(dx, dy, dw, dh, t);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(GetColorMatrix(channel));
            m_gDraw.DrawImage(img.Content, points, rt, GraphicsUnit.Pixel, ia);
        }

        /// <summary>
        /// 往画布上绘制一张图像，根据指定矩形对图像进行拉伸。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="rt">要拉伸图像的目标矩形。</param>
        public void DrawImage(Image img, Rect rt)
        {
            Rectangle drt = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            m_gDraw.DrawImage(img.Content, drt, 0, 0, img.Content.Width, img.Content.Height, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 往画布上绘制一张图像，根据指定矩形对图像进行拉伸和变换。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="rt">要拉伸图像的目标矩形。</param>
        /// <param name="t">图像的变换方式。</param>
        public void DrawImage(Image img, Rect rt, Trans t)
        {
            Rectangle drt = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            System.Drawing.Point[] points = GetTranPoints(drt.Left, drt.Top, drt.Width, drt.Height, t);
            Rectangle dst = new Rectangle(0, 0, img.Content.Width, img.Content.Height);
            m_gDraw.DrawImage(img.Content, points, dst, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 往画布上绘制一张图像，根据指定矩形对图像进行拉伸和变换。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="rt">要拉伸图像的目标矩形。</param>
        /// <param name="t">图像的变换方式。</param>
        /// <param name="channel">颜色通道。</param>
        public void DrawImage(Image img, Rect rt, Trans t, Color channel)
        {
            Rectangle drt = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            System.Drawing.Point[] points = GetTranPoints(drt.Left, drt.Top, drt.Width, drt.Height, t);
            Rectangle dst = new Rectangle(0, 0, img.Content.Width, img.Content.Height);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(GetColorMatrix(channel));
            m_gDraw.DrawImage(img.Content, points, dst, GraphicsUnit.Pixel, ia);
        }

        /// <summary>
        /// 将图像分成9份分别拉伸绘制到矩形中。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="dst">绘制的目标矩形，宽度和高度都不要小于图像的2/3。</param>
        public void DrawImageNinePatch(Image img, Rect dst)
        {
            DrawImageNinePatch(img, dst, Color.White);
        }

        /// <summary>
        /// 将图像分成9份分别拉伸绘制到矩形中。
        /// </summary>
        /// <param name="img">要绘制的图像。</param>
        /// <param name="dst">绘制的目标矩形，宽度和高度都不要小于图像的2/3。</param>
        /// <param name="channel">图像的颜色通道。</param>
        public void DrawImageNinePatch(Image img, Rect dst, Color channel)
        {
            Rectangle src = new Rectangle(0, 0, img.Content.Width, img.Content.Height);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(GetColorMatrix(channel));

            Rectangle ddst = new Rectangle(dst.Left, m_iHeight - dst.Top, dst.Width, dst.Height);
            Int32 sw = img.Width / 3;
            Int32 sh = img.Height / 3;
            Int32 dx0 = ddst.Left;
            Int32 dx1 = ddst.Left + sw;
            Int32 dx2 = ddst.Right - sw;
            Int32 dx3 = ddst.Right;
            Int32 dy0 = ddst.Top;
            Int32 dy1 = ddst.Top + sh;
            Int32 dy2 = ddst.Bottom - sh;
            Int32 dy3 = ddst.Bottom;

            m_gDraw.DrawImage(img.Content, new Rectangle(dx0, dy0, dx1 - dx0, dy1 - dy0), 0, 0, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx1, dy0, dx2 - dx1, dy1 - dy0), sw, 0, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx2, dy0, dx3 - dx2, dy1 - dy0), img.Width - sw, 0, sw, sh, GraphicsUnit.Pixel, ia);

            m_gDraw.DrawImage(img.Content, new Rectangle(dx0, dy1, dx1 - dx0, dy2 - dy1), 0, sh, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx1, dy1, dx2 - dx1, dy2 - dy1), sw, sh, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx2, dy1, dx3 - dx2, dy2 - dy1), img.Width - sw, sh, sw, sh, GraphicsUnit.Pixel, ia);

            m_gDraw.DrawImage(img.Content, new Rectangle(dx0, dy2, dx1 - dx0, dy3 - dy2), 0, img.Height - sh, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx1, dy2, dx2 - dx1, dy3 - dy2), sw, img.Height - sh, sw, sh, GraphicsUnit.Pixel, ia);
            m_gDraw.DrawImage(img.Content, new Rectangle(dx2, dy2, dx3 - dx2, dy3 - dy2), img.Width - sw, img.Height - sh, sw, sh, GraphicsUnit.Pixel, ia);
        }

        /// <summary>
        /// 保存当前画布状态到状态堆栈中。
        /// </summary>
        public void Save()
        {
            m_stkState.Push(m_gDraw.Save());
        }

        /// <summary>
        /// 从状态堆栈顶读出状态恢复画布。
        /// </summary>
        public void Restore()
        {
            m_gDraw.Restore(m_stkState.Pop());
        }

        /// <summary>
        /// 设置剪辑区域，重复设置取交集。
        /// </summary>
        /// <param name="rt">剪辑区域。</param>
        public void SetClip(Rect rt)
        {
            Rectangle ddst = new Rectangle(rt.Left, m_iHeight - rt.Top, rt.Width, rt.Height);
            m_gDraw.SetClip(ddst, CombineMode.Intersect);
        }

        /// <summary>
        /// 将剪辑区域清除掉。
        /// </summary>
        public void ResetClip()
        {
            m_gDraw.ResetClip();
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 获取矩形内绘制的变换坐标。
        /// </summary>
        /// <param name="x">绘制矩形左上角X坐标。</param>
        /// <param name="y">绘制矩形左上角Y坐标。</param>
        /// <param name="w">绘制矩形宽度。</param>
        /// <param name="h">绘制矩形高度。</param>
        /// <param name="t">绘制变换方式。</param>
        /// <returns>坐标数组。</returns>
        protected static System.Drawing.Point[] GetTranPoints(Int32 x, Int32 y, Int32 w, Int32 h, Trans t)
        {
            System.Drawing.Point lt = new System.Drawing.Point(x, y);
            System.Drawing.Point rt = new System.Drawing.Point(x + w, y);
            System.Drawing.Point lb = new System.Drawing.Point(x, y + h);
            System.Drawing.Point rb = new System.Drawing.Point(x + w, y + h);
            System.Drawing.Point[] pts = new System.Drawing.Point[3];

            switch (t)
            {
                case Trans.None:
                    pts[0] = lt;
                    pts[1] = rt;
                    pts[2] = lb;
                    break;
                case Trans.MirrorRot180:
                    pts[0] = lb;
                    pts[1] = rb;
                    pts[2] = lt;
                    break;
                case Trans.Mirror:
                    pts[0] = rt;
                    pts[1] = lt;
                    pts[2] = rb;
                    break;
                case Trans.Rot180:
                    pts[0] = rb;
                    pts[1] = lb;
                    pts[2] = rt;
                    break;
                case Trans.Rot90:
                    pts[0] = rt;
                    pts[1] = rb;
                    pts[2] = lt;
                    break;
                case Trans.Rot270:
                    pts[0] = lb;
                    pts[1] = lt;
                    pts[2] = rb;
                    break;
                case Trans.MirrorRot90:
                    pts[0] = rb;
                    pts[1] = rt;
                    pts[2] = lb;
                    break;
                case Trans.MirrorRot270:
                    pts[0] = lt;
                    pts[1] = lb;
                    pts[2] = rt;
                    break;
                default:
                    break;
            }

            return pts;
        }

        /// <summary>
        /// 获取颜色通道对应的颜色矩阵。
        /// </summary>
        /// <param name="channel">颜色通道。</param>
        /// <returns>颜色矩阵</returns>
        protected static ColorMatrix GetColorMatrix(Color channel)
        {
            ColorMatrix mat = new ColorMatrix(new float[][]{
                new float[]{1,0,0,0,0},
                new float[]{0,1,0,0,0},
                new float[]{0,0,1,0,0},
                new float[]{0,0,0,1,0},
                new float[]{0,0,0,0,0}});

            //通道颜色处理
            mat.Matrix00 = 1.0f * channel.R / 255;
            mat.Matrix11 = 1.0f * channel.G / 255;
            mat.Matrix22 = 1.0f * channel.B / 255;
            mat.Matrix33 = 1.0f * channel.A / 255;

            return mat;
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 绘制用的GDI。
        /// </summary>
        private Graphics m_gDraw = null;

        /// <summary>
        /// 画布状态。
        /// </summary>
        private Stack<GraphicsState> m_stkState = new Stack<GraphicsState>();

        /// <summary>
        /// 画布宽度。
        /// </summary>
        private Int32 m_iWidth = 0;

        /// <summary>
        /// 画布高度。
        /// </summary>
        private Int32 m_iHeight = 0;

        #endregion
    }
}
