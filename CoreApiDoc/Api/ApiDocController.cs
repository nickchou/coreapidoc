using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Api
{
    /// <summary>
    /// APIDOC文档生成首页
    /// </summary>
    public class ApiDocController : Controller
    {
        public IActionResult Index()
        {
            string html = "";

            System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
            string strName = Asmb.GetName().Name + ".Pages.index.html";
            System.IO.Stream ManifestStream = Asmb.GetManifestResourceStream(strName);

            Response.ContentType = "text/html;charset=utf-8";
            return Content(html);
        }
    }
}
