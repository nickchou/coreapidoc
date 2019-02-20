using CoreApiDoc.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiDoc.Web.Controllers
{
    /// <summary>
    /// 学生控制器
    /// </summary>
    public class StuController : BaseBController
    {
        /// <summary>
        /// 获取学生
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public IActionResult GetStu([FromServices]FlightRequest service)
        {
            return Content("aa");
        }
    }
}
