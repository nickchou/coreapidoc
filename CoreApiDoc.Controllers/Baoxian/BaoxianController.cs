using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Controllers.Baoxian
{
    public class BaoxianController : Controller
    {
        /// <summary>
        /// 基类首页方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content("Hello world");
        }
    }
}
