using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApiDoc.Web.Models;
using System.Reflection;
using System.IO;

namespace CoreApiDoc.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string html = "";
            Assembly assembly = Assembly.Load("CoreApiDoc");
            string strName = assembly.GetName().Name + ".Pages.index.html";
            #region 自己反射自己的文件
            //System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
            //string strName = Asmb.GetName().Name + ".Config.Menus.xml";
            //System.IO.Stream ManifestStream = Asmb.GetManifestResourceStream(strName);
            #endregion
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
            Response.ContentType = "text/html;charset=utf-8";
            return Content(html);
            //return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
