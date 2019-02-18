using CoreApiDoc.Entity;
using CoreApiDoc.Summary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CoreApiDoc.Service
{
    public class ControllerService
    {
        /// <summary>
        /// 获取所有集成Controller的API接口
        /// </summary>
        /// <param name="assembyName"></param>
        /// <returns></returns>
        public ApiLib GetApis(string assembyName)
        {
            ApiLib apiLib = new ApiLib();
            try
            {
                Assembly assembly = Assembly.Load(assembyName);
                apiLib.NameSpace = assembly.ManifestModule.ScopeName.Replace(".dll", "");
                //加载API文档摘要
                BaseSummary summarySer = new ClassSummary(assembly.ManifestModule.ScopeName);
                if (assembly != null && assembly.GetTypes() != null && assembly.GetTypes().Length > 0)
                {
                    summarySer.LoadFromXMLFile();
                    //根据第一个类去反射出注释文档
                    //summarySer.LoadFromXMLFile(assembly.GetTypes()[0]);
                }
                //遍历程序集的类找到Controller
                foreach (var classType in assembly.GetTypes())
                {
                    //判断classTypes是Controller的子类
                    if (classType.IsSubclassOf(typeof(Controller)))
                    {
                        ApiInfo apiInfo = new ApiInfo()
                        {
                            Name = classType.FullName.Replace(apiLib.NameSpace + ".", ""),
                            Desc = summarySer.GetSummary($"T:{classType.FullName}")
                        };

                        try
                        {
                            var apiObj = assembly.GetType(classType.FullName);
                            //加载Controller下面的方法（过滤父类和非public的）
                            foreach (var method in apiObj.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                            {
                                ApiFunc apiFunc = new ApiFunc()
                                {
                                    Name = method.Name,
                                    Desc = summarySer.GetSummary($"M:{classType.FullName}.{method.Name}")
                                };
                                ////获取方法的请求参数
                                //foreach (var p in method.GetParameters())
                                //{
                                //    apiFunc.ReqParams.Add(new ParamInfo()
                                //    {
                                //        Name = p.Name,
                                //        NameSpace = p.ParameterType.Assembly.FullName,
                                //        FileName = p.ParameterType.FullName ?? ""
                                //    });
                                //}
                                ////获取方法的返回参数
                                //var returnPara = method.ReturnParameter;
                                ////返回的程序集
                                //apiFunc.ResParams = new ParamInfo()
                                //{
                                //    NameSpace = returnPara.ParameterType.Assembly.FullName,
                                //    FileName = returnPara.ParameterType.FullName ?? ""
                                //};
                                //apiFunc.ResParams = returnPara.ParameterType.FullName;
                                apiInfo.Funs.Add(apiFunc);
                            }
                            apiLib.Apis.Add(apiInfo);
                        }
                        catch (MissingMethodException ex)
                        {
                            //没有申明空构造函数
                        }

                    }
                }
            }
            catch (FileNotFoundException ex)
            {
            }
            return apiLib;
        }
    }
}
