
## CoreApiDoc

  是一个core api的简单版的文档自动生成器，基于netcoreapi，UI部分实现用elemeui+vue实现
  功能：api接口请求/相应参数 json、文档、调试自动生成
  
  
## 使用
 > step1. https://www.nuget.org/packages/CoreApiDoc api接口类库添加CoreApiDoc的nuget引用
 
 > step2. Startup.cs文件Configure方法中添加 
 ```
  app.UseCoreApiDoc("xxxx.webapi");  //注册CoreApiDoc
 ```
 > step3. 访问地址http://yourdomain:port/apidoc
 
## 注意事项
 如希望方法注释和字段注释的完整呈现，请必须的visual studio设置类库的文档(接口类库or实体类库)
 > setp1.编辑xxxx.csproj 设置GenerateDocumentationFile为true
```xml
<PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

## 版本信息
主要的一些版本列表

Version | Release Date | Notes
-------- | ------------  | -----
1.1.4 | 2019-02-21 |  修复一系列BUG，稳定版本
