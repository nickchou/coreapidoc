using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    public class ApiFunc
    {
        /// <summary>
        /// 接口名字
        /// </summary>
        public string Name { set; get; } = "";
        /// <summary>
        /// 接口描述
        /// </summary>
        public string Desc { set; get; } = "";
        ///// <summary>
        ///// 请求参数
        ///// </summary>
        //public List<ParamInfo> ReqParams { set; get; } = new List<ParamInfo>();
        ///// <summary>
        ///// 相应参数
        ///// </summary>
        //public ParamInfo ResParams { set; get; } = new ParamInfo();

    }
}
