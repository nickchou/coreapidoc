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
        /// <param name="ctrlAssemby">Controller层的命名空间</param>
        /// <returns></returns>
        public static IApplicationBuilder UseCoreApiDoc(this IApplicationBuilder app, string ctrlAssemby)
        {
            return null;
        }
    }
}
