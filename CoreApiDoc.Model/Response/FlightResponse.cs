using CoreApiDoc.Model.Comm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Model.Response
{
    /// <summary>
    /// 机票返回信息
    /// </summary>
    public class FlightResponse : BaseResponse<string>
    {
        /// <summary>
        /// 返回乘客信息等
        /// </summary>
        public List<Passenger> Passengers { set; get; }
    }
}
