using CoreApiDoc.Model.Request;
using CoreApiDoc.Model.Response;
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
        /// <summary>
        /// 基类首页方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Test1(string Name, int Age, bool IsStu)
        {
            return Content("Hello world");
        }
        /// <summary>
        /// 基类首页方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Test2(FlightRequest req)
        {
            return Content("Hello world");
        }
        /// <summary>
        /// 基类首页方法
        /// </summary>
        /// <returns></returns>
        public FlightResponse Test3(FlightRequest req)
        {

            FlightResponse res = new FlightResponse();
            res.Data = "是我啊";
            res.Passengers = new List<Model.Comm.Passenger>();
            res.Passengers.Add(new Model.Comm.Passenger()
            {
                Name = "张三",
                Age = 15
            });
            return res;
        }
    }
}
