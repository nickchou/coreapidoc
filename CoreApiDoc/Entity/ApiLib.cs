using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    /// <summary>
    /// API类库信息
    /// </summary>
    public class ApiLib
    {
        /// <summary>
        /// 类库命名空间
        /// </summary>
        public string NameSpace { set; get; } = "";
        /// <summary>
        /// API数量
        /// </summary>
        public int Count { set; get; }
        /// <summary>
        /// 类库API接口列表
        /// </summary>
        public List<ApiInfo> Apis { set; get; } = new List<ApiInfo>();
    }
}
