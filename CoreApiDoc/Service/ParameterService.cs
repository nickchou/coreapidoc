using CoreApiDoc.Entity;
using CoreApiDoc.Summary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreApiDoc.Service
{
    /// <summary>
    /// 反射参数相关方法
    /// </summary>
    public class ParameterService
    {
        public static void WritePropertyByType(object obj, JsonWriter writer)
        {
            BaseSummary summary = new PropertiesSummary(obj.GetType());
            //加载类对应的comment信息
            Dictionary<string, string> commentValues = summary.LoadSummary();
            //遍历属性
            foreach (PropertyInfo p in obj.GetType().GetProperties().OrderBy(p => p.MetadataToken))
            {
                Type pt = p.PropertyType;
                //属性值对应summary的key
                string key = $"{p.MemberType.ToString()[0]}:{p.DeclaringType.FullName}.{p.Name}";
                //准备属性的注释
                string propertyComment = commentValues.ContainsKey(key) ? commentValues[key] : "";
                //(基元类型)IsPrimitive=Boolean,Byte,SByte,Int16,UInt16,Int32,UInt32,Int64,UInt64,Char,Double,Single
                if (pt.IsPrimitive || pt.IsValueType || pt == typeof(string))
                {
                    //基元类型 or 值类型 （基本都算是值类型）
                    writer.WritePropertyName(p.Name);
                    //字符串(引用类型),当值是null的情况赋值""
                    writer.WriteValue(p.GetValue(obj) ?? "");
                    //备注字段的数据类型
                    propertyComment += $"[{pt.FullName.Replace("System.", "")}]";
                    writer.WriteComment(propertyComment);
                }
                else if (pt.IsConstructedGenericType)
                {
                    //获取泛型中具体的对象类型，并创建对应的反射
                    Match ma = Regex.Match(p.PropertyType.AssemblyQualifiedName, @"System\.Collections\.Generic\.List`?[\d]*\[\[([\w\\d.]+),\s*([\w\\d.]+)", RegexOptions.IgnoreCase);
                    //表示构造泛型类型
                    writer.WritePropertyName(p.Name);
                    //如果泛型里的类型是系统类型，如string/datetime/int等，把注释加在字段注释里
                    if (ma.Success && ma.Groups[1].Value.StartsWith("System"))
                    {
                        propertyComment += $"[{ma.Groups[1].Value.Replace("System.", "")}]";
                    }
                    writer.WriteComment(propertyComment);
                    writer.WriteStartArray();
                    if (ma.Success)
                    {
                        if (ma.Groups[1].Value.StartsWith("System"))
                        {
                            //可能存在List<int>、List<string>的情况,集合里面的反射不出来，先用case判断吧
                            var (pType, pVal) = GetSystemDefault(ma.Groups[1].Value);
                            writer.WriteValue(pType == "string" ? "" : pVal);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            //不是系统类型默认是对象                            
                            Assembly assembly = Assembly.Load(ma.Groups[2].Value);
                            object o = assembly.CreateInstance(ma.Groups[1].Value);
                            //创建对象的实例后递归获取
                            WritePropertyByType(o, writer);
                            writer.WriteEndObject();
                        }
                    }
                    writer.WriteEndArray();
                }
                else if (pt.IsClass)
                {
                    //属性是对象的情况
                    writer.WritePropertyName(p.Name);
                    writer.WriteComment(propertyComment);
                    writer.WriteStartObject();
                    object o = p.PropertyType.Assembly.CreateInstance(p.PropertyType.FullName);
                    //创建对象的实例后递归获取
                    WritePropertyByType(o, writer);
                    writer.WriteEndObject();
                }
            }
        }

        public static void GetPropertyInfo(object obj, List<Field> fs, int level = 0)
        {
            BaseSummary summary = new PropertiesSummary(obj.GetType());
            //加载类对应的comment信息
            Dictionary<string, string> commentValues = summary.LoadSummary();
            //遍历属性,并按照MetadataToken进行排序
            foreach (PropertyInfo p in obj.GetType().GetProperties().OrderBy(p => p.MetadataToken))
            {
                Type pt = p.PropertyType;
                //属性值对应summary的key
                string key = $"{p.MemberType.ToString()[0]}:{p.DeclaringType.FullName}.{p.Name}";
                //获取属性的注释
                string propertyComment = commentValues.ContainsKey(key) ? commentValues[key] : "";
                Field f = new Field()
                {
                    //赋值
                    Name = p.Name,
                    TypeName = pt.FullName,
                    Desc = propertyComment,
                    Level = level,
                    Required = false
                };
                fs.Add(f);

                //(基元类型)IsPrimitive=Boolean,Byte,SByte,Int16,UInt16,Int32,UInt32,Int64,UInt64,Char,Double,Single
                if (pt.IsPrimitive || pt.IsValueType || pt == typeof(string))
                {

                }
                else if (pt.IsConstructedGenericType)
                {
                    //获取泛型中具体的对象类型，并创建对应的反射
                    Match ma = Regex.Match(p.PropertyType.AssemblyQualifiedName, @"System\.Collections\.Generic\.List`?[\d]*\[\[([\w\\d.]+),\s*([\w\\d.]+)", RegexOptions.IgnoreCase);
                    //f.Name = p.Name;
                    //如果泛型里的类型是系统类型，如string/datetime/int等，把注释加在字段注释里
                    if (ma.Success)
                    {
                        f.TypeName = $"List<{ma.Groups[1].Value}>";
                        if (ma.Groups[1].Value.StartsWith("System"))
                        {
                            //可能存在List<int>、List<string>的情况,集合里面的反射不出来，先用case判断吧
                            var (pType, pVal) = GetSystemDefault(ma.Groups[1].Value);
                            //writer.WriteValue(pType == "string" ? "" : pVal);
                        }
                        else
                        {
                            //不是系统类型默认是对象                            
                            Assembly assembly = Assembly.Load(ma.Groups[2].Value);
                            object o = assembly.CreateInstance(ma.Groups[1].Value);
                            //创建对象的实例后递归获取
                            //GetPropertyInfo(o, f.SubFields, level + 1); //如果要用字对象的方式，换这个
                            GetPropertyInfo(o, fs, level + 1);
                        }
                    }
                }
                else if (pt.IsClass)
                {
                    object o = p.PropertyType.Assembly.CreateInstance(p.PropertyType.FullName);
                    //GetPropertyInfo(o, f.SubFields, level + 1); //如果要用字对象的方式，换这个
                    GetPropertyInfo(o, fs, level + 1);
                }
            }
        }
        public static (string pType, object pVal) GetSystemDefault(string name)
        {
            string key = "";
            object obj = null;
            switch (name)
            {
                case "System.DateTime":
                case "System.String": key = "string"; obj = ""; break;
                case "System.Single":
                case "System.Decimal":
                case "System.Double":
                case "System.Int32":
                case "System.Int64": obj = 0; break;
                case "System.Boolean": obj = false; break;
                default: obj = ""; break;
            }
            return (key, obj);
        }
    }
}
