using CoreApiDoc.Entity;
using CoreApiDoc.Extension;
using CoreApiDoc.Summary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    //加载注释文档
                    summarySer.LoadSummary();
                }
                //遍历程序集的类找到Controller
                foreach (var classType in assembly.GetTypes())
                {
                    //判断classTypes是Controller的子类
                    if (classType.IsSubclassOf(typeof(Controller)))
                    {
                        apiLib.Count++;
                        ApiInfo apiInfo = new ApiInfo()
                        {
                            Name = classType.FullName.Replace(apiLib.NameSpace + ".", ""),
                            Desc = summarySer.GetSummary($"T:{classType.FullName}")
                        };
                        try
                        {
                            var apiObj = assembly.GetType(classType.FullName);
                            //加载Controller下面的方法（过滤父类和非public的）
                            //!m.IsSpecialName 过滤系统自动生成的  set_xxx()  get_xxx()等
                            foreach (var method in apiObj.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName))
                            {
                                apiInfo.Count++;
                                ApiFunc apiFunc = new ApiFunc()
                                {
                                    Name = method.Name,
                                    Desc = summarySer.GetSummary($"M:{classType.FullName}.{method.GetSummaryName()}")
                                };
                                apiInfo.Funs.Add(apiFunc);
                            }
                            apiLib.Apis.Add(apiInfo);
                        }
                        catch (MissingMethodException ex)
                        {
                            //没有申明空构造函数
                            string msg = ex.Message;
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                //没找到就算了忽略
                string msg = ex.Message;
            }
            return apiLib;
        }

    }
}
