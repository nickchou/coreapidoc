using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    /// <summary>
    /// 根据方法返回请求，相应参数等数据
    /// </summary>
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
        /// <summary>
        /// 请求参数集合
        /// </summary>
        public List<Field> ReqFields { set; get; } = new List<Field>();
        /// <summary>
        /// 返回参数
        /// </summary>
        public List<Field> ResFields { set; get; } = new List<Field>();
    }
}
