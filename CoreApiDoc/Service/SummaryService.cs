using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CoreApiDoc.Service
{
    /// <summary>
    /// 获取XML文档相关
    /// </summary>
    public class SummaryService
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public SummaryService() { }
        /// <summary>
        /// 默认加载的类型
        /// </summary>
        public List<string> SummaryType { set; get; } = new List<string>() { "T", "M", "P" };
        /// <summary>
        /// 直接根据Type找摘要注释
        /// </summary>
        /// <param name="tp"></param>
        public SummaryService(Type tp)
        {
            this.LoadFromXMLFile(tp);
        }
        public Dictionary<string, string> Summarys { set; get; } = new Dictionary<string, string>();

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
        public Dictionary<string, string> LoadFromXMLFile(Type type)
        {
            //Dictionary<string, string> commentDic = new Dictionary<string, string>();
            //string strFile = new Uri(type.Assembly.CodeBase).AbsolutePath;
            XmlDocument xmlDocument = new XmlDocument();
            string xmlFile = Path.ChangeExtension(new Uri(type.Assembly.CodeBase).AbsolutePath, ".xml");
            //读取配置summary xml配置文件
            if (File.Exists(xmlFile))
            {
                using (StreamReader streamReader = new StreamReader(xmlFile))
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
                    //基类的summary属性也要的（如果类继承了好几层基类，可能summary拿不到，mark一下）
                    string baseType = type.BaseType?.FullName;
                    if (xmlNode.FirstChild.Name == "summary")
                    {
                        this.Summarys.Add(key, xmlNode.InnerText.Trim());
                    }
                    //if (xmlNode.FirstChild.Name == "summary" && (key.Contains(type.FullName) || key.Contains(baseType)))
                    //{
                    //    this.Summarys.Add(key, xmlNode.InnerText.Trim());
                    //}
                }
            }
            return this.Summarys;
        }
    }
}
