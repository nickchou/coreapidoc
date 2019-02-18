using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiDoc.Web.Controllers
{
    /// <summary>
    /// 用户处理控制器
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户处理首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content("Hello world");
        }
    }
}
