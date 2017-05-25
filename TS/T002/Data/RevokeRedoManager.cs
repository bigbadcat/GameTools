using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T002.Data
{
    /// <summary>
    /// 撤销重做管理类。
    /// </summary>
    public class RevokeRedoManager
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 构造函数。
        /// </summary>
        public RevokeRedoManager()
        {
            this.m_rrRevokeOperate = new List<RevokeRedo>();
            this.m_rrRedoOperate = new List<RevokeRedo>();
        }

        /// <summary>
        /// 进行撤销操作。
        /// </summary>
        public void DoRevoke()
        {
            //如果可以撤销，将最后一次操作取出，进行撤销操作
            if (this.m_rrRevokeOperate.Count > 0)
            {
                RevokeRedo rrOperate = this.m_rrRevokeOperate[this.m_rrRevokeOperate.Count - 1];

                this.m_rrRevokeOperate.Remove(rrOperate);
                rrOperate.DoRevoke();

                //将操作对象并添加进重做列表末尾
                this.m_rrRedoOperate.Add(rrOperate);
            }
        }

        /// <summary>
        /// 进行重做操作。
        /// </summary>
        public void DoRedo()
        {
            //如果可以重做
            if (this.m_rrRedoOperate.Count > 0)
            {
                RevokeRedo rrOperate = this.m_rrRedoOperate[this.m_rrRedoOperate.Count - 1];

                //将操作对像从重做列表中移除并进行重做
                this.m_rrRedoOperate.Remove(rrOperate);
                rrOperate.DoRedo();

                //添加到撤销列表中
                this.m_rrRevokeOperate.Add(rrOperate);
            }
        }

        /// <summary>
        /// 添加撤销重做操作操作。
        /// </summary>
        /// <param name="rrOperate">要添加的操作。</param>
        public void AddRevokeRedoOperate(RevokeRedo rrOperate)
        {
            //将要撤销的操作添加进列表并清空重做列表
            this.m_rrRevokeOperate.Add(rrOperate);
            this.m_rrRedoOperate.Clear();

            //撤销次数有一定上限
            if (this.m_rrRevokeOperate.Count > 20)
            {
                this.m_rrRevokeOperate.RemoveAt(0);
            }
        }

        /// <summary>
        /// 清除撤销重做。进行不可撤销的操作时，应调用此函数。
        /// </summary>
        public void ClearRevokeRedo()
        {
            this.m_rrRevokeOperate.Clear();
            this.m_rrRedoOperate.Clear();
        }

        #endregion

        #region 数据变量=====================================================================================

        /// <summary>
        /// 进行撤销操作列表。
        /// </summary>
        protected List<RevokeRedo> m_rrRevokeOperate = null;

        /// <summary>
        /// 进行重做操作列表。
        /// </summary>
        protected List<RevokeRedo> m_rrRedoOperate = null;

        #endregion
    }
}
