using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApiDoc.Api
{
    /// <summary>
    /// APIDOC文档生成首页
    /// </summary>
    public class ApiDocController
    {
        public static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string html = "";
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
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
                                <body>
                                    <div style='color:red;'>CoreApiDoc Load Page Error</div>
                                </body>
                            </html>";
                    }
                }
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(html);
            });
        }
        public static void AA(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string html = "Action";

                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(html);
            });
        }
    }
    //public class ApiDocController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        string html = "";

    //        System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
    //        string strName = Asmb.GetName().Name + ".Pages.index.html";
    //        System.IO.Stream ManifestStream = Asmb.GetManifestResourceStream(strName);

    //        Response.ContentType = "text/html;charset=utf-8";
    //        return Content(html);
    //    }
    //}
}
