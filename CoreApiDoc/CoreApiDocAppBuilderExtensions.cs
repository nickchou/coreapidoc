using CoreApiDoc.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// CoreApiDoc启用入口程序
    /// </summary>
    public static class CoreApiDocAppBuilderExtensions
    {
        /// <summary>
        /// 开启netcore api的文档自动生成
        /// </summary>
        /// <param name="app"></param>
        /// <param name="ControllerAssemby">Controller层的命名空间</param>
        /// <returns></returns>
        public static IApplicationBuilder UseCoreApiDoc(this IApplicationBuilder app, string ControllerAssemby)
        {
            app.Map("/apidoc", lv1 =>
            {
                lv1.Map("/aa", ApiDocController.AA);
                //二级路径没有，必须写在后面，有先后执行顺序
                lv1.Map("", ApiDocController.Index);
            });
            return null;
        }
    }
}
