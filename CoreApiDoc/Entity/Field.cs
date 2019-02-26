using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiDoc.Entity
{
    /// <summary>
    /// 字段参数实体
    /// </summary>
    public class Field
    {
        /// <summary>
        /// 参数名字
        /// </summary>
        public string Name { set; get; } = "";
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { set; get; }
        /// <summary>
        /// 参数类型名称
        /// </summary>
        public string TypeName { set; get; }
        /// <summary>
        /// 字段层级，第一层默认是0，可能存在字段嵌套的情况
        /// </summary>
        public int Level { set; get; }
        /// <summary>
        /// 参数说明
        /// </summary>
        public string Desc { set; get; } = "";
        /// <summary>
        /// 子字段信息
        /// </summary>
        public List<Field> SubFields = new List<Field>();
    }
}
