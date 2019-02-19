using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Controllers.Baoxian
{
    /// <summary>
    /// 保险相关的控制器
    /// </summary>
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
        /// <summary>
        /// 哈哈哈
        /// </summary>
        /// <returns></returns>
        public IActionResult Index2()
        {
            return Content("Hello world");
        }
    }
}
