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
    /// <summary>
    /// 默认的首页，用于默认呈现
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 关于我们的页面，用于呈现联系方式等等
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        /// <summary>
        /// 联系我们，主要是联系方式和地图等
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        /// <summary>
        ///  测试页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// 错误处理
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
