[1mdiff --git a/ClassProject/ClassProject/ClassProject.csproj b/ClassProject/ClassProject/ClassProject.csproj[m
[1mindex cd928d5..804151f 100644[m
[1m--- a/ClassProject/ClassProject/ClassProject.csproj[m
[1m+++ b/ClassProject/ClassProject/ClassProject.csproj[m
[36m@@ -56,6 +56,8 @@[m
       <Private>True</Private>[m
     </Reference>[m
     <Reference Include="Microsoft.CSharp" />[m
[32m+[m[32m    <Reference Include="System.Runtime.Serialization" />[m
[32m+[m[32m    <Reference Include="System.Security" />[m
     <Reference Include="System.Web.DynamicData" />[m
     <Reference Include="System.Web.Entity" />[m
     <Reference Include="System.Web.ApplicationServices" />[m
[36m@@ -111,6 +113,11 @@[m
     <None Include="Scripts\jquery-1.10.2.intellisense.js" />[m
     <Content Include="Scripts\jquery-1.10.2.js" />[m
     <Content Include="Scripts\jquery-1.10.2.min.js" />[m
[32m+[m[32m    <None Include="Scripts\jquery.validate-vsdoc.js" />[m
[32m+[m[32m    <Content Include="Scripts\jquery.validate.js" />[m
[32m+[m[32m    <Content Include="Scripts\jquery.validate.min.js" />[m
[32m+[m[32m    <Content Include="Scripts\jquery.validate.unobtrusive.js" />[m
[32m+[m[32m    <Content Include="Scripts\jquery.validate.unobtrusive.min.js" />[m
     <Content Include="Scripts\modernizr-2.6.2.js" />[m
     <Content Include="Web.config" />[m
     <Content Include="Views\_ViewStart.cshtml" />[m
[36m@@ -120,15 +127,23 @@[m
   <ItemGroup>[m
     <Compile Include="App_Start\RouteConfig.cs" />[m
     <Compile Include="Controllers\HomeController.cs" />[m
[32m+[m[32m    <Compile Include="Controllers\NameController.cs" />[m
     <Compile Include="Global.asax.cs">[m
       <DependentUpon>Global.asax</DependentUpon>[m
     </Compile>[m
[32m+[m[32m    <Compile Include="Models\classNameContext.cs" />[m
[32m+[m[32m    <Compile Include="Models\Name.cs" />[m
     <Compile Include="Properties\AssemblyInfo.cs" />[m
   </ItemGroup>[m
   <ItemGroup>[m
     <Content Include="Views\web.config" />[m
     <Content Include="packages.config" />[m
     <Content Include="Scripts\jquery-1.10.2.min.map" />[m
[32m+[m[32m    <Content Include="Views\Name\Create.cshtml" />[m
[32m+[m[32m    <Content Include="Views\Name\Delete.cshtml" />[m
[32m+[m[32m    <Content Include="Views\Name\Details.cshtml" />[m
[32m+[m[32m    <Content Include="Views\Name\Edit.cshtml" />[m
[32m+[m[32m    <Content Include="Views\Name\Index.cshtml" />[m
     <None Include="Web.Debug.config">[m
       <DependentUpon>Web.config</DependentUpon>[m
     </None>[m
[36m@@ -138,7 +153,6 @@[m
   </ItemGroup>[m
   <ItemGroup>[m
     <Folder Include="App_Data\" />[m
[31m-    <Folder Include="Models\" />[m
   </ItemGroup>[m
   <PropertyGroup>[m
     <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>[m
[1mdiff --git a/ClassProject/ClassProject/Content/Site.css b/ClassProject/ClassProject/Content/Site.css[m
[1mindex 68278b2..14225d5 100644[m
[1m--- a/ClassProject/ClassProject/Content/Site.css[m
[1m+++ b/ClassProject/ClassProject/Content/Site.css[m
[36m@@ -1,5 +1,5 @@[m
 ﻿body {[m
[31m-    padding-top: 5px;[m
[32m+[m[32m    padding-top: 50px;[m
     padding-bottom: 20px;[m
     background-color:white;[m
 }[m
[1mdiff --git a/ClassProject/ClassProject/Views/Home/Index.cshtml b/ClassProject/ClassProject/Views/Home/Index.cshtml[m
[1mindex 6a6850f..49868f2 100644[m
[1m--- a/ClassProject/ClassProject/Views/Home/Index.cshtml[m
[1m+++ b/ClassProject/ClassProject/Views/Home/Index.cshtml[m
[36m@@ -3,4 +3,4 @@[m
     ViewBag.Title = "Index";[m
 }[m
 [m
[31m-<h2>Index</h2>[m
\ No newline at end of file[m
[32m+[m[32m<h2>Welcome</h2>[m
\ No newline at end of file[m
[1mdiff --git a/ClassProject/ClassProject/Views/Shared/_Layout.cshtml b/ClassProject/ClassProject/Views/Shared/_Layout.cshtml[m
[1mindex ad42acf..a0bd9ec 100644[m
[1m--- a/ClassProject/ClassProject/Views/Shared/_Layout.cshtml[m
[1m+++ b/ClassProject/ClassProject/Views/Shared/_Layout.cshtml[m
[36m@@ -12,12 +12,8 @@[m
     <div class="navbar navbar-inverse navbar-fixed-top">[m
         <div class="container">[m
             <div class="navbar-header">[m
[31m-                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">[m
[31m-                    <span class="icon-bar"></span>[m
[31m-                    <span class="icon-bar"></span>[m
[31m-                    <span class="icon-bar"></span>[m
[31m-                </button>[m
[31m-                @Html.ActionLink("Application name", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })[m
[32m+[m[32m                @Html.ActionLink("Home", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })[m
[32m+[m[32m                @Html.ActionLink("Names", "index", "Name",  new { area = "" }, new { @class = "navbar-brand" })[m
             </div>[m
             <div class="navbar-collapse collapse">[m
                 <ul class="nav navbar-nav">[m
[36m@@ -25,7 +21,7 @@[m
             </div>[m
         </div>[m
     </div>[m
[31m-[m
[32m+[m[32m    <br />[m
     <div class="container body-content">[m
         @RenderBody()[m
         <hr />[m
[36m@@ -34,6 +30,8 @@[m
         </footer>[m
     </div>[m
 [m
[32m+[m	[32m@RenderSection("scripts", required: false)[m
[32m+[m[32m    @RenderSection("PageScripts", required: false)[m
     <script src="~/Scripts/jquery-1.10.2.min.js"></script>[m
     <script src="~/Scripts/bootstrap.min.js"></script>[m
 </body>[m
[1mdiff --git a/ClassProject/ClassProject/Web.config b/ClassProject/ClassProject/Web.config[m
[1mindex 51d938e..5dff692 100644[m
[1m--- a/ClassProject/ClassProject/Web.config[m
[1m+++ b/ClassProject/ClassProject/Web.config[m
[36m@@ -50,4 +50,7 @@[m
       <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />[m
     </providers>[m
   </entityFramework>[m
[32m+[m[32m  <connectionStrings>[m
[32m+[m[32m    <add name="classNameContext" connectionString="data source=(localdb)\mssqllocaldb;initial catalog=TempClassDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />[m
[32m+[m[32m  </connectionStrings>[m
 </configuration>[m
\ No newline at end of file[m
[1mdiff --git a/ClassProject/ClassProject/packages.config b/ClassProject/ClassProject/packages.config[m
[1mindex a8e1a40..24e006a 100644[m
[1m--- a/ClassProject/ClassProject/packages.config[m
[1m+++ b/ClassProject/ClassProject/packages.config[m
[36m@@ -3,10 +3,12 @@[m
   <package id="bootstrap" version="3.0.0" targetFramework="net452" />[m
   <package id="EntityFramework" version="6.1.3" targetFramework="net452" />[m
   <package id="jQuery" version="1.10.2" targetFramework="net452" />[m
[32m+[m[32m  <package id="jQuery.Validation" version="1.11.1" targetFramework="net452" />[m
   <package id="Microsoft.AspNet.Mvc" version="5.2.3" targetFramework="net452" />[m
   <package id="Microsoft.AspNet.Razor" version="3.2.3" targetFramework="net452" />[m
   <package id="Microsoft.AspNet.WebPages" version="3.2.3" targetFramework="net452" />[m
   <package id="Microsoft.CodeDom.Providers.DotNetCompilerPlatform" version="1.0.0" targetFramework="net452" />[m
[32m+[m[32m  <package id="Microsoft.jQuery.Unobtrusive.Validation" version="3.2.3" targetFramework="net452" />[m
   <package id="Microsoft.Net.Compilers" version="1.0.0" targetFramework="net452" developmentDependency="true" />[m
   <package id="Microsoft.Web.Infrastructure" version="1.0.0.0" targetFramework="net452" />[m
   <package id="Modernizr" version="2.6.2" targetFramework="net452" />[m
[1mdiff --git a/Resumes/Tyler Connors Resume.docx b/Resumes/Tyler Connors Resume.docx[m
[1mindex adb2f0a..fe37f2f 100644[m
[1m--- a/Resumes/Tyler Connors Resume.docx[m	
[1m+++ b/Resumes/Tyler Connors Resume.docx[m	
[36m@@ -2,18 +2,29 @@[m
    tconnors14@wou.com [HYPERLINK: mailto:tconnors14@wou.com] |(503)-463-1122[m
 [m
 OBJECTIVE[m
[32m+[m
 To be able to create a useful software program for people.[m
 [m
 EDUCATION[m
[32m+[m[41m   [m
     Western Oregon University,      Monmouth, OR[m
[31m-    Bachelor's of Science in Computer Science, June 2017[m
[31m-[m
[32m+[m[32m    Bachelors of Science in Computer Science, will graduate June 2017[m
[32m+[m[41m   [m
[32m+[m[32m   Chemeketa Community College,      Salem, OR[m
[32m+[m	[32mAssociates of Arts Oregon Transfer Degree (AAOT), Summer 2015[m
[32m+[m[41m   [m
[32m+[m[32m   West Salem High School,      Salem, Or[m
[32m+[m	[32mClass of 2012      GPA: 3.5[m
 SKILLS[m
[31m-    I am fluent in Python, C++, C#, C, Java, and Unity.[m
[31m-[m
[31m-EXPERIENCE[m
[31m-	None[m
[32m+[m[41m   [m
[32m+[m[32m   I am fluent in Python, C++, C#, C, Java, and Unity.[m
[32m+[m[32m   I am familiar with Git and the Agile Delivery workflow[m
[32m+[m[32m   I can familiarize myself with new programming languages fairly easily as well.[m
 [m
 PROJECTS[m
[31m-	Senior Project (Winter 2017) - ASP .NET MVC Web Application[m
[31m-                                       [m
[32m+[m
[32m+[m	[32mSenior Project (Winter 2017)[m[41m [m
[32m+[m[32m   - ASP .NET MVC Web Application[m
[32m+[m[32m   PhotoBase (Spring 2016)[m[41m [m
[32m+[m[32m    -  Database with GUI for managing photos[m
[32m+[m[41m	[m
