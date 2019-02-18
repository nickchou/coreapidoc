using CoreApiDoc.Entity;
using CoreApiDoc.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreApiDoc.Api
{
    /// <summary>
    /// APIDOC文档生成首页
    /// </summary>
    public class ApiDocController
    {
        /// <summary>
        /// 控制器程序集名称，用于反射API接口
        /// </summary>
        public List<string> CtrlAssembys { set; get; } = new List<string>();

        #region ApiDoc默认页面
        /// <summary>
        /// ApiDoc默认页面
        /// </summary>
        /// <param name="app"></param>
        public void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string html = "";
                Assembly assembly = Assembly.GetExecutingAssembly();
                string strName = assembly.GetName().Name + ".Pages.index.html";
                using (Stream htmlStream = assembly.GetManifestResourceStream(strName))
                {
                    if (htmlStream != null)
                    {
                        using (StreamReader reader = new StreamReader(htmlStream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        html = @"<html><head><title>CoreApiDoc Error</title></head>
                                <body> <div style='color:red;'>CoreApiDoc Load Page Error</div></body>
                                </html>";
                    }
                }
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(html);
            });
        }
        #endregion

        #region 获取API跟路径
        public void GetPath(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                HttpRequest req = context.Request;
                string str = $"{req.Scheme}://{req.Host}{req.PathBase}";
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(str);
            });
        }
        #endregion
        
        #region 获取API接口列表
        /// <summary>
        /// 获取API接口列表
        /// </summary>
        /// <param name="app"></param>
        public void GetApi(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                HttpRequest req = context.Request;
                List<string> asbs = new List<string>();
                if (context.Request.Query["asb"].FirstOrDefault() != null)
                {
                    string[] ss = context.Request.Query["asb"].FirstOrDefault().Split(',');
                    foreach (var item in ss)
                    {
                        asbs.Add(item);
                    }
                }
                else
                {
                    asbs = CtrlAssembys;
                }
                List<ApiLib> apis = new List<ApiLib>();
                ControllerService service = new ControllerService();
                foreach (var ctrl in asbs)
                {
                    //根据程序集命名空间加载所有的API接口
                    ApiLib apiLib = service.GetApis(ctrl);
                    if (apiLib != null && !string.IsNullOrEmpty(apiLib.NameSpace))
                    {
                        apis.Add(apiLib);
                    }
                }
                string strJson = JsonConvert.SerializeObject(apis);
                context.Response.ContentType = "text/json;charset=utf-8";
                await context.Response.WriteAsync(strJson);
            });
        }
        #endregion
    }
}
