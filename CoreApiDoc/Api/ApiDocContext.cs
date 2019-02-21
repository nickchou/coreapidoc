using CoreApiDoc.Entity;
using CoreApiDoc.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoreApiDoc.Api
{
    /// <summary>
    /// APIDOC文档生成首页
    /// </summary>
    public class ApiDocContext
    {
        /// <summary>
        /// 控制器程序集名称，用于反射API接口
        /// </summary>
        public List<string> CtrlAssembys { set; get; } = new List<string>();
        /// <summary>
        /// ApplicationBuilder
        /// </summary>
        public IApplicationBuilder app { set; get; }

        #region ApiDoc默认页面
        /// <summary>
        /// ApiDoc默认页面
        /// </summary>
        /// <param name="app"></param>
        public void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                HttpRequest req = context.Request;
                string html = "";
                string baseUrl = $"{req.Scheme}://{req.Host}{req.PathBase}{req.Path}";
                Assembly assembly = Assembly.GetExecutingAssembly();
                string strName = assembly.GetName().Name + ".Pages.index.html";
                using (Stream htmlStream = assembly.GetManifestResourceStream(strName))
                {
                    if (htmlStream != null)
                    {
                        using (StreamReader reader = new StreamReader(htmlStream))
                        {
                            html = reader.ReadToEnd().Replace("{currUrl}", baseUrl);
                        }
                    }
                    else
                    {
                        html = @"<html><head><title>CoreApiDoc Error</title></head>
                                <body> <div style='color:red;'>CoreApiDoc Load Page Error</div></body>
                                </html>";
                    }
                }
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(html);
            });
        }
        #endregion

        #region 获取API接口列表
        /// <summary>
        /// 获取API接口列表
        /// </summary>
        /// <param name="app"></param>
        public void GetApi(IApplicationBuilder app)
        {
            app.Run(async context =>
            {


                HttpRequest req = context.Request;
                List<string> asbs = new List<string>();
                if (!string.IsNullOrEmpty(context.Request.Query["asb"].FirstOrDefault()))
                {
                    string[] ss = context.Request.Query["asb"].FirstOrDefault().Split(',');
                    foreach (var item in ss)
                    {
                        asbs.Add(item);
                    }
                }
                else
                {
                    asbs = CtrlAssembys;
                }
                List<ApiLib> apis = new List<ApiLib>();
                ControllerService service = new ControllerService();
                foreach (var ctrl in asbs)
                {
                    //根据程序集命名空间加载所有的API接口
                    ApiLib apiLib = service.GetApis(ctrl);
                    if (apiLib != null && !string.IsNullOrEmpty(apiLib.NameSpace))
                    {
                        apis.Add(apiLib);
                    }
                }
                string strJson = JsonConvert.SerializeObject(apis);
                context.Response.ContentType = "text/json;charset=utf-8";
                await context.Response.WriteAsync(strJson);
            });
        }
        #endregion

        #region 获取指定方法的参数
        public void GetParam(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                HttpRequest req = context.Request;
                MethodInfoResponse res = new MethodInfoResponse();
                try
                {
                    //获取请求参数
                    string assembyName = req.Query["asb"].FirstOrDefault();
                    string ctrlName = req.Query["ctrl"].FirstOrDefault();
                    string actionName = req.Query["action"].FirstOrDefault();
                    //根据程序集和名字反射对应的方法
                    MethodInfo mi = this.GetMethod(assembyName, ctrlName, actionName);
                    if (mi != null)
                    {
                        //获取请求参数
                        string reqStr = this.ReflexParamInfos(mi.GetParameters());
                        res.ReqParam = reqStr;
                        //获取返回参数
                        string resStr = this.ReflexParamInfo(mi.ReturnParameter);
                        res.ResParam = resStr;
                        res.Code = 200;
                        res.Msg = "请求成功";
                    }
                    else
                    {
                        res.Code = 500;
                        res.Msg = $"未找到指定的方法";
                    }
                }
                catch (Exception ex)
                {
                    res.Code = 500;
                    res.Msg = ex.Message;
                }
                string strJson = JsonConvert.SerializeObject(res);
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(strJson);
            });
        }
        /// <summary>
        /// 反射参数集合
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public string ReflexParamInfos(ParameterInfo[] paras)
        {
            string str = "";
            foreach (var item in paras)
            {
                str += $"{this.ReflexParamInfo(item)},";
            }
            str = str.TrimEnd(',');
            if (!(str.StartsWith('{') || str.StartsWith('[')))
            {
                str = $"{{{str}}}";
            }
            return str;
        }
        /// <summary>
        /// 反射参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public string ReflexParamInfo(ParameterInfo para)
        {
            string json = "";
            if (para == null) return json;

            string paraAssemby = para.ParameterType.Assembly.FullName;
            string paraFileName = para.ParameterType.FullName ?? "";
            if (paraFileName == "System.Void")
            {
                json = "{}";
            }
            else if (paraFileName.StartsWith("Microsoft.AspNetCore.Mvc"))
            {
                json = $"{{ /*{para.ParameterType.FullName}*/ }}";
            }
            else if (paraFileName.StartsWith("System.") || paraFileName.StartsWith("Microsoft."))
            {
                var (pType, pVal) = ParameterService.GetSystemDefault(para.ParameterType.FullName);
                json = $"\"{para.Name}\":{(pType == "string" ? "\"\"" : pVal)} /* 参数 {para.ParameterType.FullName.Replace("System.", "")}*/";
            }
            else
            {
                //反射出参数的对象(如果参数是对象的话)
                Assembly assembly = Assembly.Load(paraAssemby);
                var obj = assembly.CreateInstance(paraFileName);
                using (StringWriter sw = new StringWriter())
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        //开始
                        writer.WriteStartObject();
                        //递归调用写JsonWriter
                        ParameterService.WritePropertyByType(obj, writer);
                        //结束
                        writer.WriteEndObject();
                        writer.Flush();
                        json = sw.GetStringBuilder().ToString();
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 反射获取methond方法
        /// </summary>
        /// <returns></returns>
        private MethodInfo GetMethod(string assembyName, string ctrlName, string actionName)
        {
            MethodInfo mi = null;
            if (!ctrlName.Contains(assembyName))
            {
                ctrlName = $"{assembyName}.{ctrlName}";
            }
            //根据命名空间加载程序集
            Assembly assembly = Assembly.Load(assembyName);
            if (assembly != null)
            {
                Type type = assembly.GetType(ctrlName);
                if (type != null)
                {
                    mi = type.GetMethod(actionName);
                }
            }
            return mi;
        }
        #endregion
    }
}
