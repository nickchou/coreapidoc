using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace CoreApiDoc.Summary
{
    /// <summary>
    /// 获取文档摘要的基类
    /// </summary>
    public class BaseSummary
    {
        public BaseSummary() { }
        #region 属性
        /// <summary>
        /// 加载Summary的类型 T:类  M:方法  P：属性
        /// </summary>
        public List<string> SummaryType { set; get; } = new List<string>() { "T", "M", "P" };
        /// <summary>
        /// 指定要加载Summary的类，在生成实体属性会用到，能过滤一些无效数据
        /// </summary>
        public Type CurrType { set; get; }
        /// <summary>
        /// Summary文件路径，由实现类实现
        /// </summary>
        public string SummaryFileURI { set; get; }
        /// <summary>
        /// Summary存放的位置
        /// </summary>
        public Dictionary<string, string> Summarys { set; get; } = new Dictionary<string, string>();
        #endregion

        #region 方法
        /// <summary>
        /// 根据key找到文档注释
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSummary(string key)
        {
            return this.Summarys.ContainsKey(key) ? this.Summarys[key] : "";
        }

        /// <summary>
        /// 根据Type加载对应的摘要文档
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string, string> LoadSummary()
        {
            XmlDocument xmlDocument = new XmlDocument();
            //读取配置summary xml配置文件
            SummaryFileURI = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/{SummaryFileURI}";
            if (File.Exists(SummaryFileURI))
            {
                using (StreamReader streamReader = new StreamReader(SummaryFileURI))
                {
                    xmlDocument.Load(streamReader);
                }
            }
            if (xmlDocument != null)
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("doc/members/member");
                for (int i = 0; i < xmlNodeList.Count; i++)
                {
                    XmlNode xmlNode = xmlNodeList.Item(i);
                    string key = xmlNode.Attributes[0].Value;
                    //string baseType = type.BaseType?.FullName;
                    //只拿指定的summary类型，其他的不要，避免获取垃圾数据
                    if (xmlNode.FirstChild.Name == "summary" && key.Length > 1 && SummaryType.Contains(key.Substring(0, 1)))
                    {
                        //基类的summary属性也要的（如果类继承了好几层基类，可能summary拿不到，mark一下）
                        if (CurrType != null && (key.Contains(CurrType.FullName) || key.Contains(CurrType.BaseType?.FullName)))
                        {
                            this.Summarys.Add(key, xmlNode.InnerText.Trim());
                        }
                        else
                        {
                            this.Summarys.Add(key, xmlNode.InnerText.Trim());
                        }
                    }
                }
            }
            return this.Summarys;
        }
        #endregion
    }
}

