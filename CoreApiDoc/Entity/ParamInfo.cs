using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    public class ParamInfo
    {

        /// <summary>
        /// 参数名字
        /// </summary>
        public string Name { set; get; } = "";
        /// <summary>
        /// 参数说明
        /// </summary>
        public string Desc { set; get; } = "";
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { set; get; } = "";
        /// <summary>
        /// 类名路径
        /// </summary>
        public string FileName { set; get; } = "";
    }
}
