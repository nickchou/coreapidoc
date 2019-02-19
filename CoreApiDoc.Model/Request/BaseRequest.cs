using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Model.Request
{
    /// <summary>
    /// 通用的返回基类
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { set; get; }
    }
}
