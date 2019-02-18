using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiDoc.Web.Controllers
{
    /// <summary>
    /// 测试基类B的方法
    /// </summary>
    public class BaseBController : BaseController
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
