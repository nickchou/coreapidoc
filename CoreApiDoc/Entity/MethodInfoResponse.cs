using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    public class MethodInfoResponse
    {
        /// <summary>
        /// 错误代码，200=成功
        /// </summary>
        public int Code { set; get; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { set; get; } = "未知异常";
        /// <summary>
        /// 请求参数
        /// </summary>
        public string ReqParam { set; get; } = "";
        /// <summary>
        /// 返回
        /// </summary>
        public string ResParam { set; get; } = "";
    }
}
