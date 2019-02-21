using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreApiDoc.Extension
{
    /// <summary>
    /// MethonInfo的扩展方法
    /// </summary>
    public static class MethonInfoExt
    {
        /// <summary>
        /// 获取方法的summary注释名，因方法有重载方法，名字
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetSummaryName(this MethodInfo method)
        {
            //eg:<member name="M:CoreApiDoc.Web.Controllers.BaseBController.Index">
            //eg:<member name="M:CoreApiDoc.Web.Controllers.BaseBController.Test1(System.String,System.Int32,System.Boolean)">
            //eg:<member name="M:CoreApiDoc.Web.Controllers.BaseBController.Test2(CoreApiDoc.Model.Request.FlightRequest)">
            StringBuilder sb = new StringBuilder();
            if (method != null)
            {
                sb.Append(method.Name);
                //获取方法的参数
                var paras = method.GetParameters();
                if (paras.Length > 0)
                {
                    sb.Append("(");
                    for (int i = 0; i < paras.Length; i++)
                    {
                        var para = paras[i];
                        sb.AppendFormat("{0}{1}", i == 0 ? "" : ",", para.ParameterType.FullName);
                    }
                    sb.Append(")");
                }
            }
            //返回内容例：Test1(System.String,System.Int32,System.Boolean)
            return sb.ToString();
        }
    }
}
