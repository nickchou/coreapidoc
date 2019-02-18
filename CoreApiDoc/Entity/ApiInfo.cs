using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    /// <summary>
    /// API接口信息
    /// </summary>
    public class ApiInfo
    {
        /// <summary>
        /// API接口名称
        /// </summary>
        public string Name { set; get; } = "";
        /// <summary>
        /// API接口描述
        /// </summary>
        public string Desc { set; get; } = "";
        /// <summary>
        /// API接口方法列表
        /// </summary>
        public List<ApiFunc> Funs { set; get; } = new List<ApiFunc>();
    }
}
