using AspectCore.Injector;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiDoc.Web.Controllers
{ 
    /// <summary>
    /// 测试基类BaseController的
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Redis字符串相关
        /// </summary>
        public string Redis { set; get; }
        /// <summary>
        /// 设置名字
        /// </summary>
        private string Name { set; get; }

        /// <summary>
        /// 测试action'
        /// </summary>
        /// <returns></returns>
        public IActionResult AA() {
            return Content("aa");
        }
        /// <summary>
        /// 从ServicelCollection中获取东西
        /// </summary>
        /// <typeparam name="TA"></typeparam>
        /// <returns></returns>
        public TA ResolveRequired<TA>()
        {
            var service = HttpContext.RequestServices.ResolveRequired<TA>();
            return service;
        }
    }
}
