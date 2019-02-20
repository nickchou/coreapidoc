using CoreApiDoc.Api;
using CoreApiDoc.Route;
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

            string[] assembys = ControllerAssemby.Split(',');
            ApiDocContext ctrl = new ApiDocContext();
            foreach (var ass in assembys)
            {
                ctrl.CtrlAssembys.Add(ass);
            }
            //方法一
            app.Map("/apidoc", v1 =>
            {
                //获取网站根目录
                v1.Map("/getpath", ctrl.GetPath);
                //获取API参数
                v1.Map("/getparam", ctrl.GetParam);
                //获取API接口列表
                v1.Map("/getapi", ctrl.GetApi);
                //二级路径没有，必须写在后面，有先后执行顺序
                v1.Map("", ctrl.Index);
            });

            //方法二 ,暂时未测试通过
            //app.UseRouter(new ApiRoute(provider, "apidoc", "Home", "Index"));
            return null;
        }
    }
}
