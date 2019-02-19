using CoreApiDoc.Model.Comm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Model.Request
{
    /// <summary>
    /// 提交乘坐飞机的实体
    /// </summary>
    public class FlightRequest : BaseRequest
    {
        /// <summary>
        /// 乘客信息
        /// </summary>
        List<Passenger> Passengers { set; get; }
    }

}
