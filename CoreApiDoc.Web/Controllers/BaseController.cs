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
        /// 设置名字
        /// </summary>
        private string Name { set; get; }
    }
}
