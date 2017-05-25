using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T006.Data
{
    /// <summary>
    /// 抽象类。撤销重做类。保存了一次撤销重做操作和数据的对象。
    /// </summary>
    public abstract class RevokeRedo
    {
        #region 对外操作=====================================================================================

        /// <summary>
        /// 抽象函数。进行撤销操作。
        /// </summary>
        public abstract void DoRevoke();

        /// <summary>
        /// 抽象函数。进行重做操作。
        /// </summary>
        public abstract void DoRedo();

        #endregion
    }
}
