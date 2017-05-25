using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using T002.Data.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using T002.Platform;
using System.Collections;
using XuXiang.ClassLibrary;

namespace T002.Data
{
    /// <summary>
    /// 界面文件。
    /// </summary>
    public class InterfaceFile : ResourceFile
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 创建一个界面文件。
        /// </summary>
        /// <param name="strName">界面名称。</param>
        /// <param name="strFolder">要保存到的文件夹。</param>
        /// <param name="code">界面编号。</param>
        /// <param name="tpTileData">界面宽度。</param>
        /// <param name="iTileRows">界面高度。</param>
        /// <returns>返回0创建成功。</returns>
        public static Int32 CreateInterfaceFile(String strName, String strFolder, int code, Int32 iWidth, Int32 iHeight)
        {
            try
            {
                String strFileName = strFolder + "\\" + strName + ProjectManager.NAME_EXT_INTERFACE_EDIT;
                XmlDocument xmlDoc = new XmlDocument(); //建立XmlDomcument对象
                XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);    //Xml Declaration(Xml声明)
                UserInterface ui = new UserInterface(code, iWidth, iHeight);
                xmlDoc.AppendChild(Declaration);
                xmlDoc.AppendChild(ui.GetXmlNode(xmlDoc));
                xmlDoc.Save(strFileName);
            }
            catch
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 加载界面文件。
        /// </summary>
        /// <param name="strFileName">界面文件路径。</param>
        /// <returns>返回加载的界面文件，加载失败则返回null。</returns>
        public static InterfaceFile LoadFromFile(String strFileName)
        {
            InterfaceFile mfLoad = null;
            try
            {
                mfLoad = new InterfaceFile(strFileName);
            }
            catch (Exception e)
            {
                //String a = e.Message;
                String a = e.StackTrace;
                Console.WriteLine(a);
            }

            return mfLoad;
        }

        /// <summary>
        /// 已重载。对地图编辑进行撤销操作。
        /// </summary>
        public override void DoRevoke()
        {
            this.m_rrRevokeRedoOperate.DoRevoke();

            //内容发生改变
            this.OnFileContentChange(new EventArgs());
        }

        /// <summary>
        /// 已重载。对地图编辑进行重做操作。
        /// </summary>
        public override void DoRedo()
        {
            this.m_rrRevokeRedoOperate.DoRedo();

            //内容发生改变
            this.OnFileContentChange(new EventArgs());
        }

        /// <summary>
        /// 设置文件修改标记。
        /// </summary>
        /// <param name="bAmend">是否已修改。</param>
        public void SetAmend(Boolean bAmend)
        {
            this.m_bAmend = bAmend;
            if (bAmend)
            {
                this.RebuildShow();
                this.OnFileAmend(new EventArgs());
            }
        }

        /// <summary>
        /// 获取某个点对应的控件。
        /// </summary>
        /// <param name="p">指定点。</param>
        /// <returns>对应控件。</returns>
        public Control GetControlAtPoint(Point p)
        {
            return this.m_uiEdit.GetControlAtPoint(p);
        }

        /// <summary>
        /// 获取最大编号，每次调用会自动加1。
        /// </summary>
        /// <returns>最大编号号。</returns>
        public Int32 GetMaxCode()
        {
            return ++m_iMaxCode;
        }

        /// <summary>
        /// 重新设置控件编号。
        /// </summary>
        public void ResetControlCode()
        {
            m_iMaxCode = 99;
            this.SetControlCode(this.m_uiEdit.Root);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 生成UI文件。
        /// </summary>
        public void BuildUIFile()
        {
            String name = this.m_uiEdit.Code.ToString();
            String output = ProjectManager.Project.InterfaceBuildFolder + name + ProjectManager.NAME_EXT_INTERFACE_BUILD;
            FileStream fs = new FileStream(output, FileMode.Create);
            DataUtil.WriteInt32(fs, VERSION);
            this.m_uiEdit.WriteToStream(fs);
            fs.Flush();
            fs.Dispose();
            fs = null;
        }

        /// <summary>
        /// 生成Java版常量。
        /// </summary>
        /// <param name="sbConstVar">保存常量数据。</param>
        /// <param name="sbError">保存错误数据。</param>
        /// <returns>是否生成成功。</returns>
        public Boolean BuildConstVarJava(StringBuilder sbConstVar, StringBuilder sbError)
        {
            SortedList<String, List<Control>> constvarset = this.m_uiEdit.GetConstVarSet();
            Boolean bSuccess = true ;         //标记是否发生错误

            //一边生成程序常量一边判断是否有重复的
            sbConstVar.AppendLine("public static class CCODE{");
            foreach (String constvar in constvarset.Keys)
            {
                List<Control> ctrlst = constvarset[constvar];
                if (ctrlst.Count > 1)
                {
                    bSuccess = false;
                    foreach (Control ctr in ctrlst)
                    {
                        sbError.AppendLine(this.GetControlPath(ctr));
                    }
                    sbError.AppendLine();
                }
                else
                {
                    Control ctr = ctrlst[0];
                    String line = String.Format("public static final int {0} = {1}; //{2}", ctr.GetFullConstVar(), ctr.Code, ctr.Name);
                    sbConstVar.AppendLine(line);
                }
            }
            sbConstVar.AppendLine("}");
            return bSuccess;
        }


        /// <summary>
        /// 生成Lua版常量。
        /// </summary>
        /// <param name="sbConstVar">保存常量数据。</param>
        /// <param name="sbError">保存错误数据。</param>
        /// <returns>是否生成成功。</returns>
        public Boolean BuildConstVarLua(StringBuilder sbConstVar, StringBuilder sbError)
        {
            SortedList<String, List<Control>> constvarset = this.m_uiEdit.GetConstVarSet();
            Boolean bSuccess = true;         //标记是否发生错误

            //一边生成程序常量一边判断是否有重复的
            sbConstVar.AppendLine("--控件编号");
            sbConstVar.AppendLine("local CCODE = ");
            sbConstVar.AppendLine("{");
            foreach (String constvar in constvarset.Keys)
            {
                List<Control> ctrlst = constvarset[constvar];
                if (ctrlst.Count > 1)
                {
                    bSuccess = false;
                    foreach (Control ctr in ctrlst)
                    {
                        sbError.AppendLine(this.GetControlPath(ctr));
                    }
                    sbError.AppendLine();
                }
                else
                {
                    Control ctr = ctrlst[0];
                    String line = String.Format("   {0} = {1}, --{2}", ctr.GetFullConstVar(), ctr.Code, ctr.Name);
                    sbConstVar.AppendLine(line);
                }
            }
            sbConstVar.AppendLine("};");
            return bSuccess;
        }

        /// <summary>
        /// 生成C++版常量。
        /// </summary>
        /// <param name="sbConstVar">保存常量数据。</param>
        /// <param name="sbError">保存错误数据。</param>
        /// <returns>是否生成成功。</returns>
        public Boolean BuildConstVarCPP(StringBuilder sbConstVar, StringBuilder sbError)
        {
            SortedList<String, List<Control>> constvarset = this.m_uiEdit.GetConstVarSet();
            Boolean bSuccess = true;         //标记是否发生错误

            //一边生成程序常量一边判断是否有重复的
            sbConstVar.AppendLine("//控件编号");
            sbConstVar.AppendLine("enum CCODE");
            sbConstVar.AppendLine("{");
            foreach (String constvar in constvarset.Keys)
            {
                List<Control> ctrlst = constvarset[constvar];
                if (ctrlst.Count > 1)
                {
                    bSuccess = false;
                    foreach (Control ctr in ctrlst)
                    {
                        sbError.AppendLine(this.GetControlPath(ctr));
                    }
                    sbError.AppendLine();
                }
                else
                {
                    Control ctr = ctrlst[0];
                    String line = String.Format("   {0} = {1}, //{2}", ctr.GetFullConstVar(), ctr.Code, ctr.Name);
                    sbConstVar.AppendLine(line);
                }
            }
            sbConstVar.AppendLine("};");
            return bSuccess;
        }

        #region 结构编辑=====================================================================================

        /// <summary>
        /// 设置界面属性。
        /// </summary>
        /// <param name="code">输出编号。</param>
        /// <param name="width">界面宽度。</param>
        /// <param name="height">界面高度。</param>
        public void SetInterfaceProperty(int code, Int32 width, Int32 height)
        {
            this.m_uiEdit.Code = code;
            if (this.m_uiEdit.Width != width || this.m_uiEdit.Height != height)
            {
                this.m_uiEdit.SetSize(width, height);
                this.m_bmpDisplayBuffer.Dispose();
                this.m_bmpDisplayBuffer = new System.Drawing.Bitmap(m_uiEdit.Width, m_uiEdit.Height);
                this.RebuildShow();
            }
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 添加新的控件。
        /// </summary>
        /// <param name="ctrBase">基准控件，新控件是该控件的下一个兄弟节点。</param>
        /// <param name="ctrAdd">要添加的控件。</param>
        public void AddNewControl(Control ctrBase, Control ctrAdd)
        {
            (ctrBase.Parent as T002.Data.UI.Container).AddChildBelow(ctrBase, ctrAdd);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 添加新的控件。
        /// </summary>
        /// <param name="conParent">添加控件的容器控件。</param>
        /// <param name="ctrAdd">要添加的控件。</param>
        public void AddNewChildControl(Container conParent, Control ctrAdd)
        {
            conParent.AddChild(ctrAdd);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 删除控件。
        /// </summary>
        /// <param name="con">要删除的控件。</param>
        public void DeleteControl(Control con)
        {
            (con.Parent as T002.Data.UI.Container).RemoveChild(con);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 编辑控件。
        /// </summary>
        /// <param name="con">要编辑的控件。</param>
        /// <param name="name">控件名称。</param>
        /// <param name="var">程序常量。</param>
        public void EditControl(Control con, String name, String var)
        {
            con.Name = name;
            con.ConstVar = var;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 上移控件。
        /// </summary>
        /// <param name="con">移动的控件</param>
        public void MoveUpControl(Control con)
        {
            (con.Parent as Container).MoveBottom(con);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 上移控件到顶部。
        /// </summary>
        /// <param name="con">移动的控件</param>
        public void MoveTopControl(Control con)
        {
            (con.Parent as Container).MoveBottommost(con);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 下移控件。
        /// </summary>
        /// <param name="con">移动的控件</param>
        public void MoveDownControl(Control con)
        {
            (con.Parent as Container).MoveTop(con);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 下移控件到低部。
        /// </summary>
        /// <param name="con">移动的控件</param>
        public void MoveBottomControl(Control con)
        {
            (con.Parent as Container).MoveTopmost(con);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 将一个控件移动到另一个控件之上。
        /// </summary>
        /// <param name="move">要移动的控件。</param>
        /// <param name="target">目标控件。</param>
        public void MoveAboveControl(Control move, Control target)
        {
            //不能有Root参与
            if (move.Parent == null || target.Parent == null)
            {
                return;
            }
            (move.Parent as Container).RemoveChild(move);
            (target.Parent as Container).AddChildAbove(move, target);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 将一个控件移动为另一个容器的最后子节点。
        /// </summary>
        /// <param name="ctr">要移动的控件。</param>
        /// <param name="con">目标容器。</param>
        public void MoveChildControl(Control ctr, Container con)
        {
            //根节点不能移动
            if (ctr.Parent == null)
            {
                return;
            }
            (ctr.Parent as Container).RemoveChild(ctr);
            con.AddChild(ctr);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 粘贴控件。
        /// </summary>
        /// <param name="ctrBase">基准控件，粘贴的控件放在该控件下面。</param>
        /// <param name="paste">要粘贴的控件。</param>
        public void PasteControl(Control ctrBase, Control paste)
        {
            this.SetControlCode(paste);
            (ctrBase.Parent as T002.Data.UI.Container).AddChildBelow(ctrBase, paste);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 控件编辑=====================================================================================

        /// <summary>
        /// 设置控件的位置。
        /// </summary>
        /// <param name="con">要设置的控件。</param>
        /// <param name="pos">控件的新位置。</param>
        public void SetControlPosition(Control con, Point pos)
        {
            con.Position = pos;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件的尺寸。
        /// </summary>
        /// <param name="con">要设置的控件。</param>
        /// <param name="pos">控件的新尺寸。</param>
        public void SetControlSize(Control con, Size sz)
        {
            con.Size = sz;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件是否可用。
        /// </summary>
        /// <param name="con">要设置的控件。</param>
        /// <param name="e">是否可用。</param>
        public void SetControlEnable(Control con, Boolean e)
        {
            con.Enable = e;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件是否可见。
        /// </summary>
        /// <param name="con">要设置的控件。</param>
        /// <param name="v">是否可见。</param>
        public void SetControlVisible(Control con, Boolean v)
        {
            con.Visible = v;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件背景色。
        /// </summary>
        /// <param name="con">要设置的控件。</param>
        /// <param name="c">背景色。</param>
        public void SetControlBackColor(Control con, Color c)
        {
            con.BackColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 图像编辑=====================================================================================

        /// <summary>
        /// 设置图像显示的图片。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetPictureImage(Picture pic, String file)
        {
            pic.Image = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像通道。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="c">颜色通道。</param>
        public void SetPictureChannel(Picture pic, Color c)
        {
            pic.Channel = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像模式。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="mode">图像模式。</param>
        public void SetPictureMode(Picture pic, ImageMode mode)
        {
            pic.Mode = mode;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像对齐方式。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="a">图像对齐方式。</param>
        public void SetPictureAlign(Picture pic, Align a)
        {
            pic.Align = a;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像变换方式。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="t">图像变换方式。</param>
        public void SetPictureTrans(Picture pic, Trans t)
        {
            pic.Trans = t;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像X缩放比例。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="scale">图像X缩放比例。</param>
        public void SetPictureScaleX(Picture pic, Single scale)
        {
            pic.ScaleX = scale;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像Y缩放比例。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="scale">图像Y缩放比例。</param>
        public void SetPictureScaleY(Picture pic, Single scale)
        {
            pic.ScaleY = scale;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像是否在生成时清除数据。
        /// </summary>
        /// <param name="pic">图像控件。</param>
        /// <param name="v">要设置的值。</param>
        public void SetPictureClearValue(Picture pic, Boolean v)
        {
            pic.ClearValue = v;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 容器编辑=====================================================================================

        /// <summary>
        /// 设置容器背景图像。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="file">文件路径。</param>
        public void SetContainerBack(Container con, String file)
        {
            con.BackImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置容器背景图像颜色通道。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="channel">背景图像颜色通道。</param>
        public void SetContainerBackChannel(Container con, Color channel)
        {
            con.BackChannel = channel;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置容器背景图像变换方式。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="trans">背景图像变换方式。</param>
        public void SetContainerBackTrans(Container con, Trans trans)
        {
            con.BackTrans = trans;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置容器边框图像。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="file">边框路径。</param>
        public void SetContainerFrame(Container con, String file)
        {
            con.FrameImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置容器边框图像颜色通道。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="channel">边框图像颜色通道。</param>
        public void SetContainerFrameChannel(Container con, Color channel)
        {
            con.FrameChannel = channel;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置容器是否i裁剪子节点。
        /// </summary>
        /// <param name="pic">容器控件。</param>
        /// <param name="clip">是否裁剪。</param>
        public void SetContainerClipping(Container con, Boolean clip)
        {
            con.Clipping = clip;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 文本控件编辑=================================================================================

        /// <summary>
        /// 设置文本控件的文本。
        /// </summary>
        /// <param name="tcon">文本控件。</param>
        /// <param name="text">要设置的文本。</param>
        public void SetTextControlText(TextControl tcon, String text)
        {
            tcon.Text = text;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本控件的字号。
        /// </summary>
        /// <param name="tcon">文本控件。</param>
        /// <param name="ws">新的字号。</param>
        public void SetTextControlWordSize(TextControl tcon, Single ws)
        {
            tcon.WordSize = ws;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本控件的文本色。
        /// </summary>
        /// <param name="tcon">文本控件。</param>
        /// <param name="c">要设置的文本色。</param>
        public void SetTextControlTextColor(TextControl tcon, Color c)
        {
            tcon.TextColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本控件是否在生成时清除数据。
        /// </summary>
        /// <param name="tcon">文本控件。</param>
        /// <param name="v">要设置的值。</param>
        public void SetTextControlClearValue(TextControl tcon, Boolean v)
        {
            tcon.ClearValue = v;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 标签编辑=====================================================================================

        /// <summary>
        /// 设置文本标签的对齐方式。
        /// </summary>
        /// <param name="lb">文本标签。</param>
        /// <param name="a">要设置的对齐方式。</param>
        public void SetLabelAlign(Label lb, Align a)
        {
            lb.Align = a;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置标签类型。
        /// </summary>
        /// <param name="lb">标签控件。</param>
        /// <param name="lt">要设置的标签。</param>
        public void SetLabelType(Label lb, LabelType lt)
        {
            lb.LabelType = lt;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置标签的描边色。
        /// </summary>
        /// <param name="lb">标签控件。</param>
        /// <param name="sc">要设置的描边色。</param>
        public void SetLabelStrokeColor(Label lb, Color sc)
        {
            lb.StrokeColor = sc;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 按钮编辑=====================================================================================

        /// <summary>
        /// 设置按钮普通状态图像。
        /// </summary>
        /// <param name="btn">按扭控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetButtonNormalImage(Button btn, String file)
        {
            btn.NormalImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置按钮按下状态图像。
        /// </summary>
        /// <param name="btn">按扭控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetButtonDownImage(Button btn, String file)
        {
            btn.DownImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置按钮禁用状态图像。
        /// </summary>
        /// <param name="btn">按扭控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetButtonDisableImage(Button btn, String file)
        {
            btn.DisableImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 普通按钮编辑=================================================================================

        /// <summary>
        /// 设置简单按钮文本。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="txt">要设置的文本。</param>
        public void SetSingleButtonText(SingleButton sb, String txt)
        {
            sb.Text = txt;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本字号。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="size">要设置的字号。</param>
        public void SetSingleButtonWordSize(SingleButton sb, Single size)
        {
            sb.WordSize = size;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本偏移。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="offset">要设置的偏移量。</param>
        public void SetSingleButtonTextOffset(SingleButton sb, Point offset)
        {
            sb.TextOffset = offset;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本按下偏移。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="offset">要设置的偏移量。</param>
        public void SetSingleButtonTextDownOffset(SingleButton sb, Point offset)
        {
            sb.TextDownOffset = offset;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本普通状态颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextNormalColor(SingleButton sb, Color c)
        {
            sb.TextNormalColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本按下状态颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextDownColor(SingleButton sb, Color c)
        {
            sb.TextDownColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本不可用状态颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextDisableColor(SingleButton sb, Color c)
        {
            sb.TextDisableColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本普通状态描边颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextNormalStrokeColor(SingleButton sb, Color c)
        {
            sb.TextNormalStrokeColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本按下状态描边颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextDownStrokeColor(SingleButton sb, Color c)
        {
            sb.TextDownStrokeColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置简单按钮文本不可用状态描边颜色。
        /// </summary>
        /// <param name="sb">简单按钮控件。</param>
        /// <param name="c">要设置的颜色。</param>
        public void SetSingleButtonTextDisableStrokeColor(SingleButton sb, Color c)
        {
            sb.TextDisableStrokeColor = c;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 表格编辑=====================================================================================

        /// <summary>
        /// 设置表格单元数量。
        /// </summary>
        /// <param name="tb">表格控件。</param>
        /// <param name="number">要设置的数量。</param>
        public void SetTableChildNumber(Table tb, Int32 number)
        {
            tb.ChildNumber = number;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置表格滑动条宽度。
        /// </summary>
        /// <param name="tb">表格控件。</param>
        /// <param name="w">要设置的宽度。</param>
        public void SetTableScrollBarWidth(Table tb, Int32 w)
        {
            tb.ScrollBarWidth = w;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置表格滑动条图像。
        /// </summary>
        /// <param name="tb">表格控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetTableScrollBar(Table tb, String file)
        {
            tb.ScrollBar = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置表格滑动条背景图像。
        /// </summary>
        /// <param name="tb">表格控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetTableScrollBack(Table tb, String file)
        {
            tb.ScrollBack = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 滑动表格编辑=================================================================================

        /// <summary>
        /// 设置滑动表格的滑动方向。
        /// </summary>
        /// <param name="st">滑动表格控件。</param>
        /// <param name="dir">要设置的方向。</param>
        public void SetScrollTableDirection(ScrollTable st, Direction dir)
        {
            st.Direction = dir;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动表格摆放基数。
        /// </summary>
        /// <param name="st">滑动表格控件。</param>
        /// <param name="number">要设置的基数。</param>
        public void SetScrollTableBasicNumber(ScrollTable st, Int32 number)
        {
            st.BasicNumber = number;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 单选按钮编辑=================================================================================

        /// <summary>
        /// 设置单选按钮所在的组。
        /// </summary>
        /// <param name="rdb">单选按钮控件。</param>
        /// <param name="code">要设置的组编号。</param>
        public void SetRadionButtonGroup(RadioButton rdb, Int32 code)
        {
            rdb.GroupCode = code;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置单选按钮是否选中。
        /// </summary>
        /// <param name="rdb">单选按钮控件。</param>
        /// <param name="ck">要设置是否选中。</param>
        public void SetRadionButtonCheck(RadioButton rdb, Boolean ck)
        {
            rdb.Checked = ck;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 复选按钮编辑=================================================================================

        /// <summary>
        /// 设置复选按钮是否选中。
        /// </summary>
        /// <param name="st">复选按钮控件。</param>
        /// <param name="dir">要设置是否选中。</param>
        public void SetCheckButtonCheck(CheckButton ckb, Boolean ck)
        {
            ckb.Checked = ck;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 复选按钮编辑=================================================================================

        /// <summary>
        /// 设置翻页表格的行数。
        /// </summary>
        /// <param name="pt">翻页表格控件。</param>
        /// <param name="row">要设置的行数。</param>
        public void SetPageTableRow(PageTable pt, Int32 row)
        {
            pt.Row = row;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置翻页表格的列数。
        /// </summary>
        /// <param name="pt">翻页表格控件。</param>
        /// <param name="col">要设置的列数。</param>
        public void SetPageTableCol(PageTable pt, Int32 col)
        {
            pt.Col = col;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 滚动面板编辑=================================================================================

        /// <summary>
        /// 设置滚动面板的移动量。
        /// </summary>
        /// <param name="sp">滚动面板控件。</param>
        /// <param name="mv">要设置的移动量。</param>
        public void SetScrollPanelMove(ScrollPanel sp, Point mv)
        {
            sp.Move = mv;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 数字图像编辑=================================================================================

        /// <summary>
        /// 设置数字图像的图像文件。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="file">要设置的文件路径。</param>
        public void SetNumberImageFile(NumberImage ni, String file)
        {
            ni.Image = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的填充图像文件。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="file">要设置的填充文件路径。</param>
        public void SetNumberUpperLimitImageFile(NumberImage ni, String file)
        {
            ni.UpperLimitImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的表示的值。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="value">要设置的值。</param>
        public void SetNumberImageValue(NumberImage ni, Int32 value)
        {
            ni.Number = value;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的表示的上限值。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="value">要设置的上限值。</param>
        public void SetNumberImageUpperLimitValue(NumberImage ni, Int32 value)
        {
            ni.UpperLimit = value;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的缩放比例。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="zoom">要设置的缩放比例。</param>
        public void SetNumberImageZoom(NumberImage ni, Single zoom)
        {
            ni.Zoom = zoom;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的图像间隔。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="gap">要设置的图像间隔。</param>
        public void SetNumberImageGap(NumberImage ni, Single gap)
        {
            ni.Gap = gap;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的对齐方式。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="align">要设置的对齐方式。</param>
        public void SetNumberImageAlign(NumberImage ni, LineMode align)
        {
            ni.Align = align;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置数字图像的排列方向。
        /// </summary>
        /// <param name="ni">数字图像控件。</param>
        /// <param name="dir">要设置的排列方向。</param>
        public void SetNumberImageDirection(NumberImage ni, Direction dir)
        {
            ni.Direction = dir;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 文本域编辑===================================================================================

        /// <summary>
        /// 设置文本域滑动条宽度。
        /// </summary>
        /// <param name="ta">文本域控件。</param>
        /// <param name="w">要设置的宽度。</param>
        public void SetTextAreaScrollBarWidth(TextArea ta, Int32 w)
        {
            ta.ScrollBarWidth = w;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本域滑动条图像。
        /// </summary>
        /// <param name="ta">文本域控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetTextAreaScrollBar(TextArea ta, String file)
        {
            ta.ScrollBar = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本域滑动条背景图像。
        /// </summary>
        /// <param name="ta">文本域控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetTextAreaScrollBack(TextArea ta, String file)
        {
            ta.ScrollBack = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 图像数字编辑=================================================================================

        /// <summary>
        /// 设置图像数字的图像文件。
        /// </summary>
        /// <param name="inb">图像数字控件。</param>
        /// <param name="file">要设置的文件路径。</param>
        public void SetImageNumberFile(ImageNumber inb, String file)
        {
            inb.Image = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像数字的表示的值。
        /// </summary>
        /// <param name="inb">图像数字控件。</param>
        /// <param name="value">要设置的值。</param>
        public void SetImageNumberValue(ImageNumber inb, Int32 value)
        {
            inb.Number = value;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像数字的缩放比例。
        /// </summary>
        /// <param name="inb">图像数字控件。</param>
        /// <param name="zoom">要设置的缩放比例。</param>
        public void SetImageNumberZoom(ImageNumber inb, Single zoom)
        {
            inb.Zoom = zoom;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置图像数字的对齐方式。
        /// </summary>
        /// <param name="inb">图像数字控件。</param>
        /// <param name="align">要设置的对齐方式。</param>
        public void SetImageNumberAlign(ImageNumber inb, LineMode align)
        {
            inb.Align = align;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 文本框编辑===================================================================================

        /// <summary>
        /// 设置文本框的对齐方式。
        /// </summary>
        /// <param name="tb">文本框控件。</param>
        /// <param name="align">要设置的文本对齐方式。</param>
        public void SetTextBoxAlign(TextBox tb, Align align)
        {
            tb.Align = align;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置文本框输入类型。
        /// </summary>
        /// <param name="tb">文本框控件。</param>
        /// <param name="pwd">要设置的输入类型。</param>
        public void SetTextBoxInputType(TextBox tb, InputType type)
        {
            tb.Type = type;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 进度条编辑===================================================================================

        /// <summary>
        /// 设置进度条填充图像。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="file">要设置的图像文件路径。</param>
        public void SetProgressBarFillImage(ProgressBar pb, String file)
        {
            pb.FillImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置进度条背景图像。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="file">要设置的图像文件路径。</param>
        public void SetProgressBarSlotImage(ProgressBar pb, String file)
        {
            pb.SlotImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置进度条的填充颜色。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="fc">要设置的填充色。</param>
        public void SetProgressBarFillColor(ProgressBar pb, Color fc)
        {
            pb.FillColor = fc;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置进度条的前进方向。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="pd">要设置的方向。</param>
        public void SetProgressBarDirection(ProgressBar pb, ProgressDirection pd)
        {
            pb.Direction = pd;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件的起始值。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="start">要设置的起始值。</param>
        public void SetProgressBarStartValue(ProgressBar pb, Int32 start)
        {
            pb.StartValue = start;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件的当前值。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="cur">要设置的当前值。</param>
        public void SetProgressBarCurrentValue(ProgressBar pb, Int32 cur)
        {
            pb.CurrentValue = cur;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置控件的结束值。
        /// </summary>
        /// <param name="pb">进度条控件。</param>
        /// <param name="end">要设置的结束值。</param>
        public void SetProgressBarEndValue(ProgressBar pb, Int32 end)
        {
            pb.EndValue = end;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 滑动条编辑===================================================================================

        /// <summary>
        /// 设置滑动条滑块图像。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="file">图像文件路径。</param>
        public void SetSliderBarImage(SliderBar slb, String file)
        {
            slb.BarImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条背景图像。
        /// </summary>
        /// <param name="pb">滑动条控件。</param>
        /// <param name="file">要设置的图像文件路径。</param>
        public void SetSliderBarSlotImage(SliderBar slb, String file)
        {
            slb.SlotImage = file.Equals(String.Empty) ? null : T002.Platform.Image.LoadFromFile(ProjectManager.Project.AssetsFolder + file);
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条滑块缩放比。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="zoom">要设置的缩放值。</param>
        public void SetSliderBarZoom(SliderBar slb, Single zoom)
        {
            slb.BarZoom = zoom;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条的滑动方向。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="dir">要设置的方向。</param>
        public void SetSliderBarDirection(SliderBar slb, Direction dir)
        {
            slb.Direction = dir;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条的起始值。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="start">要设置的起始值。</param>
        public void SetSliderBarStartValue(SliderBar slb, Int32 start)
        {
            slb.StartValue = start;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条的当前值。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="cur">要设置的当前值。</param>
        public void SetSliderBarCurrentValue(SliderBar slb, Int32 cur)
        {
            slb.CurrentValue = cur;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置滑动条的结束值。
        /// </summary>
        /// <param name="slb">滑动条控件。</param>
        /// <param name="end">要设置的结束值。</param>
        public void SetSliderBarEndValue(SliderBar slb, Int32 end)
        {
            slb.EndValue = end;
            this.RebuildShow();
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region 粒子视图编辑=================================================================================

        /// <summary>
        /// 设置粒子视图的粒子文件。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="file">粒子文件路径。</param>
        public void SetParticleViewFile(ParticleView pv, String file)
        {
            pv.ParticleFile = file;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置粒子视图控件的粒子显示是否受父容器限制。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="clip">是否剪辑。</param>
        public void SetParticleViewParentClip(ParticleView pv, Boolean clip)
        {
            pv.ParentClip = clip;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置粒子视图控件的水平缩放。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="scalex">水平缩放值。</param>
        public void SetParticleViewScaleX(ParticleView pv, Single scalex)
        {
            pv.ScaleX = scalex;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置粒子视图控件的竖直缩放。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="scaley">竖直缩放值。</param>
        public void SetParticleViewScaleY(ParticleView pv, Single scaley)
        {
            pv.ScaleY = scaley;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #region Spine动画视图编辑============================================================================

        /// <summary>
        /// 设置Spine动画视图的精灵文件。
        /// </summary>
        /// <param name="sv">Spine动画视图控件。</param>
        /// <param name="file">Spine动画文件路径。</param>
        public void SetSpineViewFile(SpineView sv, String file)
        {
            sv.SpineFile = file;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置Spine动画视图的动画名称。
        /// </summary>
        /// <param name="sv">Spine动画视图控件。</param>
        /// <param name="name">Spine动画名称。</param>
        public void SetSpineViewAnimationName(SpineView sv, String name)
        {
            sv.AnimationName = name;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置Spine动画视图控件是否循环播放。
        /// </summary>
        /// <param name="sv">Spine动画视图控件。</param>
        /// <param name="clear">Spine动画是否循环播放。</param>
        public void SetSpineViewLoop(SpineView sv, Boolean loop)
        {
            sv.Loop = loop;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置Spine动画视图控件的水平缩放。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="scalex">水平缩放值。</param>
        public void SetSpineViewScaleX(SpineView sv, Single scalex)
        {
            sv.ScaleX = scalex;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        /// <summary>
        /// 设置Spine动画视图控件的竖直缩放。
        /// </summary>
        /// <param name="pv">粒子视图控件。</param>
        /// <param name="scaley">竖直缩放值。</param>
        public void SetSpineViewScaleY(SpineView sv, Single scaley)
        {
            sv.ScaleY = scaley;
            this.m_bAmend = true;
            this.OnFileAmend(new EventArgs());
        }

        #endregion

        #endregion

        #region 对外属性=====================================================================================

        /// <summary>
        /// 获取地图宽度。
        /// </summary>
        public Int32 Width
        {
            get
            {
                return this.m_uiEdit.Width;
            }
        }

        /// <summary>
        /// 获取地图的高度。
        /// </summary>
        public Int32 Height
        {
            get
            {
                return this.m_uiEdit.Height;
            }
        }

        /// <summary>
        /// 获取在编辑的UI。
        /// </summary>
        public UserInterface Interface
        {
            get
            {
                return this.m_uiEdit;
            }
        }

        #endregion

        #region 内部操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="strFileName">文件路径。</param>
        protected InterfaceFile(String strFileName)
            : base(strFileName, ProjectManager.TYPE_RESOURCE_INTERFACE)
        {
            m_uiEdit = UserInterface.LoadFromXmlFile(strFileName);
            m_iMaxCode = GetContainerMaxID(m_uiEdit.Root);
            this.m_bmpDisplayBuffer = new System.Drawing.Bitmap(m_uiEdit.Width, m_uiEdit.Height);
            RebuildShow();
        }

        /// <summary>
        /// 已重载。将地图文件保存到硬盘。
        /// </summary>
        protected override void SaveFileToDisk()
        {
            base.SaveFileToDisk();

            //String strFileName = this.m_strFileName;
            XmlDocument xmlDoc = new XmlDocument(); //建立XmlDomcument对象
            XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);    //Xml Declaration(Xml声明)
            xmlDoc.AppendChild(Declaration);
            xmlDoc.AppendChild(m_uiEdit.GetXmlNode(xmlDoc));
            xmlDoc.Save(this.m_strFileName);
        }

        /// <summary>
        /// 销毁贴图文件。
        /// </summary>
        protected override void DestroyFile()
        {
            base.DestroyFile();
        }

        /// <summary>
        /// 重新构建显示的内容。
        /// </summary>
        protected void RebuildShow()
        {
            Graphics g = Graphics.FromImage(this.m_bmpDisplayBuffer);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.Clear(System.Drawing.Color.Transparent);
            Canvas c = new Canvas(g, this.m_uiEdit.Width, this.m_uiEdit.Height);
            m_uiEdit.Paint(c, new Point(0, 0));
        }

        /// <summary>
        /// 获取容器内控件最大编号。
        /// </summary>
        /// <param name="con">容器控件。</param>
        /// <returns>控件最大编号。</returns>
        protected Int32 GetContainerMaxID(Container con)
        {
            Int32 code = 0;
            foreach (Control ctr in con.ChildList)
            {
                code = Math.Max(code, ctr.Code);
                if (ctr is Container)
                {
                    code = Math.Max(code, GetContainerMaxID(ctr as Container));
                }
                else if (ctr is Table)
                {
                    Table tb = ctr as Table;
                    code = Math.Max(code, GetContainerMaxID(tb.Prototype));
                }
                else if (ctr is ScrollPanel)
                {
                    ScrollPanel sp = ctr as ScrollPanel;
                    code = Math.Max(code, GetContainerMaxID(sp.Child));
                }
            }
            return Math.Max(con.Code, code);
        }

        /// <summary>
        /// 设置控件和所有子控件的Code。
        /// </summary>
        /// <param name="ctr">要设置的控件。</param>
        protected void SetControlCode(Control ctr)
        {
            ctr.Code = ++m_iMaxCode;
            if (ctr is Container)
            {
                Container con = ctr as Container;
                foreach (Control tmp in con.ChildList)
                {
                    SetControlCode(tmp);
                }
            }
            else if (ctr is Table)
            {
                SetControlCode((ctr as Table).Prototype);
            }
            else if (ctr is ScrollPanel)
            {
                SetControlCode((ctr as ScrollPanel).Child);
            }
        }

        /// <summary>
        /// 获取控件的路径。
        /// </summary>
        /// <param name="ctr"></param>
        /// <returns></returns>
        protected String GetControlPath(Control ctr)
        {
            Control ctrParent = ctr.Parent;
            StringBuilder sbPath = new StringBuilder();
            sbPath.Insert(0, ctr.GetNodeName());
            while (ctrParent != null)
            {
                sbPath.Insert(0, " -> ");
                sbPath.Insert(0, ctrParent.GetNodeName());
                ctrParent = ctrParent.Parent;
            }
            return sbPath.ToString();
        }

        #endregion

        #region 数据变量=====================================================================================

        //UI文件版本
        public const Int32 VERSION = 4;

        /// <summary>
        /// 要编辑的用户界面。
        /// </summary>
        private UserInterface m_uiEdit = null;

        /// <summary>
        /// 最大控件编号。
        /// </summary>
        private Int32 m_iMaxCode = 0;

        #endregion
    }
}
