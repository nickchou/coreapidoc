﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Model.Comm
{
    /// <summary>
    /// 乘客对象
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 乘客年龄
        /// </summary>
        public int Age { set; get; }
    }
}
