using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiDoc.Controllers
{
    public class PayController : Controller
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
