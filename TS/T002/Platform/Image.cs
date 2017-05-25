using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using T002.Data.UI;
using XuXiang.ClassLibrary;
using System.IO;

namespace T002.Platform
{
    /// <summary>
    /// 图像。
    /// </summary>
    public class Image
    {
        #region 数据定义=====================================================================================

        /// <summary>
        /// 图像类型。
        /// </summary>
        public enum ImageType
        {
            /// <summary>
            /// 资源图像。
            /// </summary>
            Resource = 0,

            /// <summary>
            /// 内存图像。
            /// </summary>
            Memery = 1
        }

        /// <summary>
        /// 保存图像的和对应的数量引用。
        /// </summary>
        public class ImageHandle
        {
            /// <summary>
            /// 构造函数。
            /// </summary>
            /// <param name="img">引用的图像。</param>
            public ImageHandle(Image img)
            {
                m_imgHandle = img;
                m_iNumber = 1;
            }

            /// <summary>
            /// 引用的图像。
            /// </summary>
            public Image m_imgHandle = null;

            /// <summary>
            /// 图像引用数量。
            /// </summary>
            public Int32 m_iNumber = 0;

        }

        #endregion

        #region 对外操作=====================================================================================

        /// <summary>
        /// 从文件加载图像。
        /// </summary>
        /// <param name="file">文件路径。</param>
        /// <returns>文件图像，加载失败返回null。</returns>
        public static Image LoadFromFile(String file)
        {
            Image img = null;
            if (!ResourceImages.ContainsKey(file))
            {
                //加载图像
                Bitmap bmp = null;
                try
                {
                    bmp = (Bitmap)System.Drawing.Image.FromFile(file);
                }
                catch
                {
                    bmp = null;
                }
                if (bmp != null)
                {
                    //为防止绘制时尺寸不对，重新绘制一份
                    Bitmap tmp = new Bitmap(bmp.Width, bmp.Height);
                    Graphics g = Graphics.FromImage(tmp);
                    Rectangle rt = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    g.DrawImage(bmp, rt, rt, GraphicsUnit.Pixel);
                    bmp.Dispose();
                    bmp = null;

                    //生成Image对象
                    img = new Image(ImageType.Resource);
                    img.m_strName = file;
                    img.m_iWidth = tmp.Width;
                    img.m_iHeight = tmp.Height;
                    img.m_bmpContent = tmp;

                    //缓存处理
                    ImageHandle ih = new ImageHandle(img);
                    ResourceImages.Add(file, ih);
                }
            }
            else
            {
                ImageHandle ih = ResourceImages[file];
                ++ih.m_iNumber;
                img = ih.m_imgHandle;
            }
            return img;
        }

        /// <summary>
        /// 获取字符串图像，图像尺寸等于该字符串显示到画布上占用的尺寸，透明背景。
        /// </summary>
        /// <param name="str">要生成图像的字符串。</param>
        /// <param name="size">字体大小。</param>
        /// <param name="tc">文字的颜色。</param>
        /// <returns>字符串图像。</returns>
        public static Image GetStringImage(String str, Single size, Color tc)
        {
            //计算字符串的尺寸
            Bitmap bmp = new Bitmap(10, 10);
            Graphics gp = Graphics.FromImage(bmp);
            Font font = new Font(StringFont, size);
            SizeF sz = gp.MeasureString(str, font);

            //生成相应大小的图像
            Image img = new Image(ImageType.Memery);
            img.m_strName = str;
            img.m_iWidth = (Int32)sz.Width + 1;
            img.m_iHeight = (Int32)sz.Height + 1;
            img.m_bmpContent = new Bitmap(img.m_iWidth, img.m_iHeight);

            //绘制文本返回
            Graphics gdip = Graphics.FromImage(img.m_bmpContent);
            Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(tc.A, tc.R, tc.G, tc.B));
            gdip.DrawString(str, font, brush, 0, 4);

            return img;
        }

        /// <summary>
        /// 获取字符串图像，图像尺寸等于该字符串显示到画布上占用的尺寸，透明背景。
        /// </summary>
        /// <param name="str">要生成图像的字符串。</param>
        /// <param name="size">字体大小。</param>
        /// <param name="tc">文字的颜色。</param>
        /// <param name="fc">文本描边色。</param>
        /// <returns>字符串图像。</returns>
        public static Image GetStrokeStringImage(String str, Single size, Color tc, Color fc)
        {
            //计算字符串的尺寸
            Bitmap bmp = new Bitmap(10, 10);
            Graphics gp = Graphics.FromImage(bmp);
            Font font = new Font(StringFont, size);
            SizeF sz = gp.MeasureString(str, font);

            //生成相应大小的图像
            Image img = new Image(ImageType.Memery);
            img.m_strName = str;
            img.m_iWidth = (Int32)sz.Width + 1;
            img.m_iHeight = (Int32)sz.Height + 1;
            img.m_bmpContent = new Bitmap(img.m_iWidth, img.m_iHeight);

            //绘制文本返回
            Graphics gdip = Graphics.FromImage(img.m_bmpContent);
            Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(tc.A, tc.R, tc.G, tc.B));
            Brush sbrush = new SolidBrush(System.Drawing.Color.FromArgb(fc.A, fc.R, fc.G, fc.B));
            gdip.DrawString(str, font, sbrush, -1, 3);
            gdip.DrawString(str, font, sbrush, -1, 4);
            gdip.DrawString(str, font, sbrush, -1, 5);
            gdip.DrawString(str, font, sbrush, 0, 3);
            gdip.DrawString(str, font, sbrush, 0, 5);
            gdip.DrawString(str, font, sbrush, 1, 3);
            gdip.DrawString(str, font, sbrush, 1, 4);
            gdip.DrawString(str, font, sbrush, 1, 5);
            gdip.DrawString(str, font, brush, 0, 4);

            return img;
        }

        /// <summary>
        /// 删除图像，所有图像不用时都应该对其调用此函数。
        /// </summary>
        /// <param name="img">要删除的图像</param>
        public static void DeleteImage(Image img)
        {
            if (img == null)
            {
                return;
            }

            if (img.m_itType == ImageType.Resource)
            {
                ImageHandle ih = ResourceImages[img.m_strName];
                --ih.m_iNumber;
                if (ih.m_iNumber == 0)
                {
                    ResourceImages.Remove(img.m_strName);
                    ih.m_imgHandle = null;
                }
            }
        }

        /// <summary>
        /// 获取多种颜色的字符串图像。
        /// </summary>
        /// <param name="str">要生成图像的字符串，字符串包含了颜色控制信息。</param>
        /// <param name="size">字体大小。</param>
        /// <param name="dc">文字的默认颜色。</param>
        /// <returns>字符串图像。</returns>
        public static Image GetColorStringImage(String str, Single size, Color dc)
        {
            //计算最终图像的宽和高
            List<Bitmap> charbmps = ParseColorText(str, size, dc);
            Int32 width = 0;
            Int32 height = 0;
            Int32 wdis = (Int32)(-10 * size / 24);
            foreach (Bitmap bmp in charbmps)
            {
                width += bmp.Width;
                height = Math.Max(height, bmp.Height);
            }
            width += (charbmps.Count - 1) * wdis;

            //成相应大小的图像
            Image img = new Image(ImageType.Memery);
            img.m_strName = str;
            img.m_iWidth = width + 1;
            img.m_iHeight = height + 1;
            img.m_bmpContent = new Bitmap(width, height);

            //图像拼接
            Int32 drawpos = 0;
            Graphics g = Graphics.FromImage(img.m_bmpContent);
            foreach (Bitmap bmp in charbmps)
            {
                g.DrawImage(bmp, drawpos, 4);    //英文字符的高度较小，居中绘制
                drawpos += bmp.Width + wdis;
            }
            
            //图像删除
            foreach (Bitmap bmp in charbmps)
            {
                bmp.Dispose();
            }
            charbmps.Clear();

            return img;
        }

        /// <summary>
        /// 获取多种颜色的字符串图像。
        /// </summary>
        /// <param name="str">要生成图像的字符串，字符串包含了颜色控制信息。</param>
        /// <param name="size">字体大小。</param>
        /// <param name="dc">文字的默认颜色。</param>
        /// <param name="width">图像宽度，超过该宽度字符串自动换行。</param>
        /// <returns>字符串图像。</returns>
        public static Image GetColorStringImage(String str, Single size, Color dc, Int32 width)
        {
            //计算最终图像的宽和高
            List<Bitmap> charbmps = ParseColorText(str, size, dc);
            Int32 w = 0;
            Int32 height = 0;
            Int32 wdis = (Int32)(-10 * size / 24);
            Int32 line = 1;
            foreach (Bitmap bmp in charbmps)
            {
                w += bmp.Width + wdis;
                if (w > width)
                {
                    ++line;
                    w = bmp.Width;
                }
                height = Math.Max(height, bmp.Height);
            }

            //生成相应大小的图像
            Image img = new Image(ImageType.Memery);
            img.m_strName = str;
            img.m_iWidth = width + 1;
            img.m_iHeight = height * line + 1;
            img.m_bmpContent = new Bitmap(img.m_iWidth, img.m_iHeight);

            //图像拼接
            Graphics g = Graphics.FromImage(img.m_bmpContent);
            Int32 drawx = 0;
            Int32 drawy = 0;
            foreach (Bitmap bmp in charbmps)
            {
                Int32 dx = drawx;
                drawx += bmp.Width + wdis;
                if (drawx > width)
                {
                    drawx = bmp.Width + wdis;
                    drawy += height;
                    g.DrawImage(bmp, 0, drawy+4);  //英文字符的高度较小，居中绘制
                }
                else
                {
                    g.DrawImage(bmp, dx, drawy+4);  //英文字符的高度较小，居中绘制
                }
            }

            //图像删除
            foreach (Bitmap bmp in charbmps)
            {
                bmp.Dispose();
            }
            charbmps.Clear();

            return img;
        }

        /// <summary>
        /// 获取图像的副本。
        /// </summary>
        /// <returns>图像的拷贝。</returns>
        public Image Clone()
        {
            Image img = null;
            if (this.m_itType == ImageType.Memery)
            {
                //内存图像直接拷贝
                img = new Image(ImageType.Memery);
                img.m_iWidth = this.m_iWidth;
                img.m_iHeight = this.m_iHeight;
                img.m_bmpContent = (Bitmap)this.m_bmpContent.Clone();
            }
            else if (this.m_itType == ImageType.Resource)
            {
                ImageHandle ih = ResourceImages[this.m_strName];
                ++ih.m_iNumber;
                img = ih.m_imgHandle;
            }
            return img;
        }

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 图像名称。
        /// </summary>
        public String Name
        {
            get
            {
                return this.m_strName;
            }
        }

        /// <summary>
        /// 获取图像宽度。
        /// </summary>
        public Int32 Width
        {
            get
            {
                return this.m_iWidth;
            }
        }

        /// <summary>
        /// 获取图像高度。
        /// </summary>
        public Int32 Height
        {
            get
            {
                return this.m_iHeight;
            }
        }

        /// <summary>
        /// 获取图像尺寸。
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(this.m_iWidth, this.m_iHeight);
            }
        }

        /// <summary>
        /// 获取图像内容。
        /// </summary>
        public Bitmap Content
        {
            get
            {
                return this.m_bmpContent;
            }
        }

        /// <summary>
        /// 设置文本图像的字体文件。
        /// </summary>
        public static String FontFile
        {
            set
            {
                if (value.Equals(String.Empty) || !File.Exists(value))
                {
                    StringFont = new FontFamily("宋体");
                }
                else
                {
                    System.Drawing.Text.PrivateFontCollection FM = new System.Drawing.Text.PrivateFontCollection();
                    FM.AddFontFile(value);
                    StringFont = FM.Families[0];
                }
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="type">图像类型。</param>
        protected Image(ImageType type)
        {
            m_itType = type;
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~Image()
        {
            m_bmpContent.Dispose();
            m_bmpContent = null;
        }

        /// <summary>
        /// 获取字符对应的图像。
        /// </summary>
        /// <param name="ch">要生成图像的字符。</param>
        /// <param name="size">字体大小。</param>
        /// <param name="c"></param>
        /// <returns></returns>
        protected static Bitmap GetCharBitmap(Char ch, Single size, Color c)
        {
            //计算字符串的尺寸
            Bitmap bmp = new Bitmap(10, 10);
            Graphics gp = Graphics.FromImage(bmp);
            Font font = new Font(StringFont, size);
            SizeF sz;

            if (ch == ' ')
            {
                sz = gp.MeasureString("a", font);
            }
            else
            {
                sz = gp.MeasureString(ch.ToString(), font);
            }

            Bitmap charbmp = new Bitmap((Int32)sz.Width, (Int32)sz.Height);
            if (ch != ' ')
            {
                Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B));
                Graphics g = Graphics.FromImage(charbmp);
                g.DrawString(ch.ToString(), font, brush, 0, 0);
            }

            return charbmp;
        }

        /// <summary>
        /// 分析颜色文本，将他们拆分成字符图像，调用者负责销毁字符图像。
        /// </summary>
        /// <param name="str">包含颜色信息的字符串。</param>
        /// <param name="size">字体名称。</param>
        /// <param name="dc">字体大小。</param>
        /// <returns>文本默认颜色。</returns>
        protected static List<Bitmap> ParseColorText(String str, Single size, Color dc)
        {
            Color curcolor = dc;
            Char[] buffer = str.ToCharArray();
            List<Bitmap> charimage = new List<Bitmap>();

            Int32 pos = 0;
            Int32 len = str.Length;
            while (pos < len)
            {
                if (buffer[pos] == '#')
                {
                    if ((pos + 1) < len)
                    {
                        Color tc;
                        if (buffer[pos + 1] == '#')
                        {
                            //接受#字符
                            pos += 2;
                            charimage.Add(GetCharBitmap('#', size, curcolor));
                        }
                        else if (DataUtil.TryParseColor(buffer, pos + 1, out tc))
                        {
                            Int32 newcolor = tc.ToArgb();
                            curcolor = newcolor == 0 ? dc : tc;
                            pos += 9;
                        }
                        else
                        {
                            charimage.Add(GetCharBitmap(buffer[pos++], size, curcolor));
                        }
                    }
                    else
                    {
                        charimage.Add(GetCharBitmap(buffer[pos++], size, curcolor));
                    }
                }
                else
                {
                    charimage.Add(GetCharBitmap(buffer[pos++], size, curcolor));
                }
            }

            return charimage;
        }

        #endregion

        #region 成员变量=====================================================================================

        /// <summary>
        /// 资源图像缓存。
        /// </summary>
        private static Dictionary<String, ImageHandle> ResourceImages = new Dictionary<String, ImageHandle>();

        /// <summary>
        /// 字符串图像使用的字体。
        /// </summary>
        private static FontFamily StringFont = new FontFamily("宋体");

        /// <summary>
        /// 图像类型。
        /// </summary>
        private ImageType m_itType = ImageType.Memery;

        /// <summary>
        /// 图像名称。
        /// </summary>
        private String m_strName = String.Empty;

        /// <summary>
        /// 图像宽度。
        /// </summary>
        private Int32 m_iWidth = 0;

        /// <summary>
        /// 图像高度。
        /// </summary>
        private Int32 m_iHeight = 0;

        /// <summary>
        /// 图像内容。
        /// </summary>
        private Bitmap m_bmpContent = null;

        #endregion
    }
}
