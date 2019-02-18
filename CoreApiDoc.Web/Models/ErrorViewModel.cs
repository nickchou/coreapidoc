using System;

namespace CoreApiDoc.Web.Models
{
    /// <summary>
    /// 错误处理实体
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 显示请求ID
        /// </summary>

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}