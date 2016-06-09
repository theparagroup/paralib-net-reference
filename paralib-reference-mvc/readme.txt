=============================================================
====	.NET Framework, ASP.NET & MVC History
=============================================================

Note: Framework and ASP.NET version numbers are the same.

[year] [Framework/CLR] [Visual Studio (internal/file version]

2002, .Net 1.0/1.0, Visual Studio.NET (7.0/7.0)

		Web Forms (*.aspx)
		Web Services (*.asmx)

2003, .Net 1.1/1.1, Visual Studio.NET 2003 (7.1/8.0)

2005, .Net 2.0/2.0, Visual Studio 2005 (8.0/9.0)

		Master pages
		Web parts
		64-bit support
		Roles & Membership

2006, .Net 3.0/2.0, (no new Visual Studio release) 

		WPF
		WF
		WCF

2007, .Net 3.5/2.0, Visual Studio 2008 (9.0/10.0)

		AJAX
		LINQ
		EF1 (v1)

		MVC1 (2009):
			WebForms
			Html Helpers
			Ajax Helpers

		MVC2 (2009):
			Strongly-typed helpers
			client-side validation
			Areas
			support for data annotations
			HttpGet, HttpPost, etc attributes

2010, .Net 4.0/4.0, Visual Studio 2010 (10.0/11.0)

		EF4 (v2)
		WebPages
		Razor
		WebMatrix (2011)
		SimpleMembership (2011)
		MVC3 (2011):
			ViewBag (ViewData that uses new dynamic ExpandoObject)
			Global Action Filters
			Unobtrusive JavaScript
			NuGet

2012, .Net 4.5/4.0, Visual Studio 2012 (11.0/12.0)

		EF5 (v3)
		WebPages2 (open sourced)
		WebAPI (open sourced)
		MVC4 (open sourced):
			Bundling & Minification

2013, .Net 4.5.1/4.0, Visual Studio 2013 (12.0/12.0)

		EF6 (open sourced)
		WebAPI2
		Identity 1.0
		Identity 2.0 (2014)
		MVC5:
			Scaffolding
			Authentication Filters

2015, .Net 4.6/4.0, Visual Studio 2015 (14.0/12.0)

2016?, 5.0/.NET Core, Visual Studio "15" (15.0/?)

		EF7
		MVC6
		Razor 4

=============================================================
====	IIS and IIS Express
=============================================================

Visual Studio 2015 can be configured to use either the developer version of IIS,
IIS Express, or a local instance of IIS.

When configuring IIS Express, use the following file:

		{solution}\.vs\config\applicationhost.config

For example, if trying to use Windows Authentication with IIS Express, you'll
need to modify the following entry in applicationhost.config:

		<windowsAuthentication enabled="false">
			<providers>
				<add value="Negotiate" />
				<add value="NTLM" />
			</providers>
		</windowsAuthentication>

Also, be aware that Visual Studio wants to manage some of these IIS Express
properties for you. To see these properties, select the project in the solution
explorer and hit F4:

		Anonymous Authentication
		Windows Authentication
		SSL Enabled
		PipeLine Mode (Integrated, Classic)
		etc.
		

To use IIS with Visual Studio, ensure you have access to the following folder:

		C:\Windows\System32\inetsrv\config

And check:

		ISAPI & CGI Restrictions
		Folder permissions
		Application Pool
		Port
		Verify Web Site is running


=============================================================
====	IIS Authentication
=============================================================

ASP.NET is implemented as an IIS ISAPI Extension (aspnet_isapi.dll). The IIS 
process and extensions of course must run under some security context 
(user account), IIS allow each request to run with an associated access token.
By default, all resource requests (HTML files, etc) will be compared to this
token to determine if the user making the request can access the resource.

IIS creates this access token based on the IIS authentication options, which include:

		Basic Authentication per RFC 2617
		Integrated Windows Authentication (enabled by default)
		Annonymous Authentication (also enabled by default)

With Basic or Windows, IIS will challenge the web browser for credentials. With anonymous,
an predefined, existing account will be used for the request. Any of these accounts may
or may not have access to the actual resource (file).

How this works has changed over the years:

In II5 (and IIS 5 Isolation Mode in II6), IIS (inetinfo.exe) ran in user mode under SYSTEM,
and ASP.NET ran in a single worker process (aspnet_wp.exe) under the ASPNET account. The 
access token for anonymous access was IUSR_MachineName (using the NETBIOS name), but could
be changed.

In IIS6, IIS was moved to kernel mode (HTTP.sys) and ASP.NET work processes (w3wp.exe) run
in "application pools". The access token for anonymous access was still IUSR_MachineName.
IIS6 was the first version of IIS after the .NET Framework was released.

In IIS7, the anonymous account was changed to the built-in IUSR, and optionally can be a
new concept called ApplicationPoolIdentity. Also, ASP.NET has been fully integrated into
IIS, and the ISAPI extension is no longer used (unless you are in "Classic mode").

Note: IIS7 Integrated Mode does not support ASP Impersonation (see below).

VERSION		IIS Process/Account		ASP.NET Worker Process/Account	IIS Anonymous Account
-------		--------------------	------------------------------	-----------------------
5/2000		inetinfo.exe/SYSTEM		aspnet_wp.exe/ASPNET			IUSR_MachineName
6/2003		(kernel)				w3wp.exe/NETWORKSERVICE			IUSR_MachineName
7/2008		(kernel)				w3wp.exe/ApplicationPoolId		IUSR
8/2012
10/2016

You can access the account that is actually executing this way:

		System.Security.Principal.WindowsIdentity.GetCurrent().Name

This will either be the Worker Process Account or something else if impersonation is enabled.

You can see the account in the "access token" that IIS sends to ASP.NET this way:

		Request.LogonUserIdentity

This will either be the authenticated user (basic or windows) or the anonymous user (IUSR),
and is always a WindowsIdentity principal (NOT an IPrincipal - see ASP.NET Authentication).

Note: Microsoft plans to further decouple ASP.NET from IIS via the "Open Web Interface for 
.NET" (OWIN) standard and project Katana.


=============================================================
====	Framework 4.5 Runtime Opt-In
=============================================================

Some of the information below (mostly regarding security) involves breaking changes
to the Framework runtime between version 4.0 and 4.5. Both use CLR 4.0, so the
breaking changes are in "Framework code" only.

When an ASP.NET application includes the following in the web.config:

		<httpRuntime targetFramework="4.5" />

or a non-web application includes this in its app.config:

		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5"/>

Then you use the new runtime. Otherwise you will run in "quirks mode" and be compatible
with the 4.0 Framework.

Note when you turn on 4.5 in an ASP.NET application the following is infered (but
overrideable) in your web.config:

		<configuration>
		  <appSettings>
			<add key="aspnet:UseTaskFriendlySynchronizationContext" value="true" />
			<add key="ValidationSettings:UnobtrusiveValidationMode" value="WebForms" />
		  </appSettings>
			<system.web>
			  <compilation targetFramework="4.5" />
			  <machineKey compatibilityMode="Framework45" />
			  <pages controlRenderingCompatibilityVersion="4.5" />
			</system.web>
		</configuration>

It's a mess.


=============================================================
====	ASP.NET Security
=============================================================

ASP.NET provides cryptographic services to applications, for example encrypting "ViewState"
or "Authentication Tickets".

Cryptographic settings can be changed with the <machineKey> element in web.config:

		<system.web>
			<machineKey decryption="DES" ... />

Controlling ViewState:

		<system.web>
			<pages viewStateEncryptionMode="Always" ... />

Controlling Authentication Tickets:

		<system.web>
			<forms protection="All" ... />

IIS5 ran under the SYSTEM account, and machine keys were stored in as LSA Secrets:

		HKEY_LOCAL_MACHINE\SECURITY\Policy\Secrets

In IIS6, this was changed to NETWORKSERVICE and the HKCU hive:

		HKEY_CURRENT_USER\Software\Microsoft\ASP.NET\4.0.30319.0

Before .NET 4.0, developers didn't have direct access to these functions. The
following classes are marked internal:

		System.Web.Configuration.MachineKeySection.Encode()
		System.Web.Configuration.MachineKeySection.Decode()

However, in 4.0 the MachineKey class was added, providing access to these methods:

		System.Web.Security.MachineKey.Encode()
		System.Web.Security.MachineKey.Decode()

In 4.5 (see the opt-in section above) support for the newer CryptoAPI was added to
ASP.NET:

		System.Web.Security.Cryptography.AspNetCryptoServiceProvider.Protect()
		System.Web.Security.Cryptography.AspNetCryptoServiceProvider.UnProtect()

Again, these are internal, but you can access the functionality via MachineKey:

		System.Web.Security.MachineKey.Protect()
		System.Web.Security.MachineKey.UnProtect()


=============================================================
====	ASP.NET Impersonation & Delegation
=============================================================

Normally ASP.NET applications execute using the security context of the worker process. This is
not to be confused with the IIS-authenticated user (the access token).

If desired, the application can be configured to run in the security context of the requesting
user, or a completely different user:

	<system.web>
		<identity impersonate="true" userName="domain\user" password="password" />

Note: impersonation is not supported in II7 Integrated mode, but it is supported in Classic mode.

Note: a related but distinct concept is delegation. This is where IIS bounces the
user credentials against another server. NTLM doesn't support it, but Kerberos
and Basic do support it. You can always call LogonUser with "Interactive."


=============================================================
====	.NET (and ASP.NET) Authentication
=============================================================

ASP.NET provides an additional authentication layer that build upon .NET security features
found in the System.Security namespace. At this layer, we can set the current authenticated 
user for the current request (and thread) using the following properties:

		System.Threading.Thread.CurrentPrincipal
		System.Web.HttpContext.User

Both of which are of type IPrincipal (NOT WindowsPrincipal, as sent over by IIS). Typically
these are GenericPrincipal objects, and the DefaultAuthenticationModule will create one
whether there is an authenticated user or not.

Unfortunately, these are redundant, and both should point to the same principal and should
be kept in sync. When setting them in custom authentication schemes, both should be set
at the same time. The threading version is what you use in non-ASP applications, and the
HttpContext version is supposed to be "convienent".

There's a lot of strange redundencies in ASP.NET, for example, these always return the 
same value:

		Context.Request.IsAuthenticated 
		Context.User.Identity.IsAuthenticated

The IPrincipal interface is very simple, and basically combines an "identity" with a
generic concepts of authentication and authorization ("roles").

		IPrincipal:
			IIdentity Identity {get;}
			bool IsInRole(string);

		IIdentity
			string AuthenticationType {get;} //NTLM, Basic, etc.
			bool IsAuthenticated {get;}
			string Name {get;}

You can use the PrincipalPermission attribute to mark classes and methods as requiring
authenticated users with specific roles before allowing execution. Understand this is 
a .NET feature, not a ASP.NET-specific feature.

Note: be aware that with the new Identity mechanism added in ASP.NET 4.5, the inheritance
hierarchy for many classes (GenericPrincipal, FormsIdentity, etc.) has been changed to 
include new classes that provice the "claims based" Identity features:

		Claim
		ClaimsPrincipal
		ClaimsIdentity
		ClaimsPrincipalPermission

If not using claims, you can ignore these and use the old interfaces and classes. As of
.NET 4.5, instead of implementing IPrincipal and IIdentity (as in custom Forms Authentication), 
one should derive from these classes. Instead of custom properties (email, location), use claims.

We use the following web.config entry to configure ASP.NET authentication:

		<system.web>
			<authentication mode= "[Windows|Forms|Passport|None]"/>

ASP.NET "Windows" Authentication will take the access token from IIS (only if IIS Authentication
Windows or Basic is enabled) and set various properties of the GenericPrincipal to values in the 
token. This is implemented in the WindowsAuthenticationModule (an HttpModule). It is important to 
understand that IIS actually does the authentication and this module simply populates the principal. 

See also: LOGON_USER and AUTH_TYPE.

"Forms" enables the "Forms Authentication" mechanism where the application can define its own
custom users and roles (stored in a database, for example). We'll discuss this next.

"None" does nothing, and again allows the application to set the IPrincipal in a custom manner.

=============================================================
====	ASP.NET Architecture
=============================================================

Once IIS has mapped an extension to ASP.NET, the request is sent through the ASP.NET Pipeline.
If this is the first request for the application, the Application Domain is created and many
other things which we'll skip over here.

A new HttpApplication object is created (or reused from a pool). If your application has a 
Global.asax defined, then that will be instantiated. Otherwise the base class will be.

If this is the first request through the pipeline, then the following method will be called:

		Application_Start(...)

For all requests:

		Application_BeginRequest(...)
		Application_Error(...)
		Application_EndRequest(...)

If (when) the application is unloaded:

		Application_End(...)

There are additional methods and events that can be hooked into in Global.asax:

		Session Events
		Request Events
		Module Events

Note: some events are proper .NET events (such as )

The HttpContext object is created and wired-up as follows:

		HttpContext
			HttpApplication ApplicationInstance {get;}
			HttpApplicationStateBase Application {get;}
			HttpRequestBase Request {get;}
			HttpResponseBase Response {get;}
			HttpServerUtilityBase Server {get;}
			HttpSessionStateBase Session {get;}

pseudo envents

SessionState_OnStart?


=============================================================
====	Forms Authentication
=============================================================

genericprincipal
formsidentity
formsauthenticationticket
formsauthenticationmodule
defaultauthenticaionmodule
skipauth

=============================================================
====	URL Authorization
=============================================================

urlauthorizationmodule


=============================================================
====	Membership & Identity Frameworks
=============================================================

Here we discuss the following frameworks:

		ASP.NET 2.0 Membership
		SimpleMembership
		ASP.NET Identity

In ASP.NET 2.0 Membership was introduced. This used the new Provider Model,
which is a dependency injection framework implementing the following patterns:

		Strategy
		Factory Method
		Singleton
		Fascade

Other things besides membership used the provider model, such as session storage.

Two built-in membership providers were:

		SQL Server
		Active Directory

Using membership, your application could get authentication, role-based
authorization, and management features "for free". Visual Studio made it "easy"
to build login, change password, and "create user" forms using membership-aware 
Web Form controls (*.ascx).

Membership was fundementally broken. The built-in providers were limited in such
things like table names and hashing algorithms, and the interfaces were heavy and 
painful to implement if you wanted to create your own provider. Also, Membership
was tied to Forms Authentication. The biggest issue that "roles" are not always
the best authorization model.

When the WebMatrix was released in 2001 (around the MVC3/MVC4 releases), a new
membership system (SimpleMembership) was released along with a new WebSecurity
API. While these originally came with WebMatrix, they could be used in MVC.
Unfortunately, SimpleMembership basically wrapped the ASP.NET 2.0 Membership
classes with "simpler" ones:

		SimpleMembershipProvider
		SimpleRoleProvider

A few improvements were made but SimpleMembership essentially has all the same
issues as ASP.NET Membership.

In 2013 ASP.NET Identity was released. It is a complete redesign and rewrite of the
membership system. In 2014 Identity 2.0 was released.

Some ASP.NET Identity 1.0 Features:

		OWIN Support
		External Logins (Facebook, Google, etc.)
		Async Support
		EF6-based provider

Some ASP.NET Identity 2.0 Features:

		Two-Factor Authentication
		Account Confirmation via email
		Account Lock-Out

In short, Identity is better than "classic" Membership, but it's new and in a 
state of change, and still pretty heavy. 



=============================================================
====	ASP.NET Routing vs MVC
=============================================================

MVC builds upon ASP.NET (System.Web.dll) and extends ASP.NET Routing, which is 
implemented in the UrlRoutingModule class (an HttpModule). MVC uses ASP.NET
Routing to route requests to Controllers and Actions.

The UrlRoutingModule is usually registered via the IIS or IIS Express configuration.


=============================================================
====	ASP.NET Web Forms vs Web Pages (and Razor)
=============================================================

Web Forms (*.aspx and *ascx files) are the original ASP.NET HTML rendering mechanism,
replacing the classic ASP paradigm (similar to but predating the similar PHP and JSP).

Web Forms are built into System.Web.dll and Visual Studio provides drag-and-drop designers
that make web development much like using any other RAD tool. They are event-driven 
and component-based, so it is not very different (from the developers point of view)
from developing a desktop application. Web Forms are heavy-ish, have a steep learning 
curve, as well as several limitations such as being limited to a single (server-side)
<form> tag.

In ASP.NET 2.0, support for "BuildProviders" that allow for custom compilation of files
in the ASP.NET framework (*.wsdl and *.xsd files are a good example).

ASP.NET Web Pages (System.Web.WebPages.dll) are a new technology that leverages the
build provider mechanism to simplify the Web Form model, and allows for different 
templating languages (such as Razor) to be used in ASP.NET. Web Pages debuted with MVC3 
and the WebMatrix IDE, but is a distinct technology and now used in MVC. 

Web Pages handles Web Page requests via the WebPageHttpModule and WebPageHttpHandler 
classes, and registers BuildProviders so that Razor view can be compiled at runtime, 
upon the first request.

In MVC, you can force views to be compiled at build by adding this to the *.csproj:

		<MvcBuildViews>true</MvcBuildViews>

But that doesn't actually pre-compile the views and they will still be compiled at
runtime. To do that, see the following NuGet packages:

		RazorGenerator.Mvc
		RazorGenerator.MsBuild

Web Pages are very similiar to classic ASP in that it is a very simple and lightweight 
templating engine. The main differences are that Web Pages are compiled and can use the
full .NET CLR.

Note on Razor: It is important to understand that while Razor is designed to be an HTML
templating engine (it understands HTML <tag> syntax), and it is located in the "System.Web"
namespace (System.Web.Razor.dll), Razor actually has no dependencies on ASP.NET. Razor can
be used completely independently of MVC or ASP.NET.

Certain constructs (@Html, Layouts & @RenderSection, etc.) are actually part of MVC base
view classes and technically not Razor or Web Pages.




=============================================================
====	Zero Configuration (PreApplicationStartMethodAttribute)
=============================================================

Since Web Pages are implemented as HttpModules you may expect to see registration 
entries in your Web.config files like this:

		<configuration>
		  <system.web>
			<httpModules>
			  <add name="HelloWorldModule" type="HelloWorldModule"/>
			 </httpModules>
		  </system.web>
		</configuration>

Or because Razor uses BuildProviders, something like this:

		<compilation debug=”false”>
		 <buildProviders>
		  <remove extension=”.xsd”/>
		  <add appliesTo=”Code” extension=”.xsd” type=”Msdn.Samples.Compilation.XsdClassBuildProvider”/>
		 </buildProviders>
		</compilation>
 
However, in ASP.NET 4 the PreApplicationStartMethodAttribute (System.Web.dll) was added, 
allowing for zero-config ASP.NET applications. This attribute is used at the assembly 
level and allows startup code to run before Application_Start. 

Used with the DynamicModuleUtility class (Microsoft.Web.Infrastructure.dll), assemblies 
such as System.Web.WebPages.dll can register thier HttpModule as soon as they are loaded.

The Razor BuildProviders for VB (*.vbhtml) and C# (*.cshtml) are installed by default for
MVC applications. This is also accomplished via the PreApplicationStartMethod attribute
(in System.Web.WebPages.Razor.dll), so when that assembly is loaded, so is Razor.

The Razor build providers are registered using the System.Web.Compilation.BuildProvider 
class like this:

		BuildProvider.RegisterBuildProvider(".cshtml", typeof(RazorBuildProvider));
		BuildProvider.RegisterBuildProvider(".vbhtml", typeof(RazorBuildProvider));

You can also dynamically add a referenced assembly (as you would in the <assemblies> 
section of web.config) with:

		System.Web.Compilation.BuildManager.AddReferencedAssembly()

=============================================================
====	MVC Overview
=============================================================

The current version of MVC (as of this writing) is 5.2, and it is based on ASP.NET 4.6.

MVC lives in System.Web.Mvc.dll, and works primarily by leveraging and extending ASP.NET's
built-in Routing, Web Forms, and Web Pages features.

For example, ASP.NET Routing is enhanced by several extension methods to the RouteCollection
class (see System.Web.Mvc.RouteCollectionExtensions). These methods route URLs to Controllers
and Actions.

When a view needs to be rendered, MVC relies on ViewEngines to find the appropriate view
and render it. ViewEngines implement the IViewEngine interface.

By default, two ViewEngines are registered:

		WebFormViewEngine
		RazorViewEngine

The "standard" MVC view locations are hard-coded in these view engines. These are the default
locations searched when looking for views:

		~/Views/Hello/Index.aspx
		~/Views/Hello/Index.ascx
		~/Views/Shared/Index.aspx
		~/Views/Shared/Index.ascx
		~/Views/Hello/Index.cshtml
		~/Views/Hello/Index.vbhtml
		~/Views/Shared/Index.cshtml
		~/Views/Shared/Index.vbhtml

The job of the ViewEngine is to locate a view (and object that implements IView) so that it
can be rendered. How it is rendered depends on the underlying view technology. For example,
The WebFormViewEngine expects views to derive from ViewPage, which in turn derives from the 
regular ASP.NET Page class. The RazorViewEngine expects its views to be WebViewPages, which
derive from WebPageBase (a class in System.Web.WebPages.dll).

If the view needs to be compiled, the registered BuildProvider will do that on the first
request.

In this manner MVC jumps the gap between the MVC-world and the WebForm-world (or
WebPage-world).

These are the relevant class hierarchies:

	Web Forms (Razor):

		System.Web.Mvc.VirtualPathProviderViewEngine:IViewEngine
			System.Web.Mvc.BuildManagerViewEngine
				System.Web.Mvc.WebFormViewEngine

		System.Web.UI.Page
			System.Web.Mvc.ViewPage
				System.Web.Mvc.ViewPage<T>

	Web Pages (Razor):

		System.Web.Mvc.VirtualPathProviderViewEngine:IViewEngine
			System.Web.Mvc.BuildManagerViewEngine
				System.Web.Mvc.RazorViewEngine
	
		System.Web.WebPages.WebPageBase
			System.Web.Mvc.WebViewPage
				System.Web.Mvc.WebViewPage<T>


Both Web Form and Razor views have access to MVC-centric properties and methods such
as @Html and @ViewBag, but these are not part Web Forms or Web Pages or Razor. These
members are simply implemented in the base classes for view (ViewPage or WebViewPage).
Therefore, regardless of which of the two built-in view technologies you are using, 
all of MVC idioms are supported and avaliable.




=============================================================
====	Creating an MVC5 Project (the hard way)
=============================================================

The built-in MVC5 templates are very heavy, and include things like ASP.NET
Identity and generally make a mess of things. We don't use the built-in
templates.

Create new project:

		Framework 4.6
		ASP.NET Web Application
		Empty 4.6 Template
		Uncheck "Host in the cloud"

Add Microsoft.AspNet.Mvc 5 via NuGet (or do it manually - see below)

Create a global event handler (Global.asax)

IIS usually has several HttpHandlers installed to process "resources", such as
scripts and other data embedded into assemblies. These handlers are registered
in the IIS (or IIS Express) Web.configs like this:

		<configuration>
			<system.web>
				<httpHandlers>
					<add path="WebResource.axd" verb="GET" 
					type="System.Web.Handlers.AssemblyResourceLoader" 
					validate="True" />
				</httpHandlers>
			</system.web>
		</configuration>

We don't want these paths getting routed to controllers by accident, so all 
MVC applications will have the following instruction in Application_Start:

		RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

Create a controller. The containing folder name doesn't matter, but the class
should derive from Controller and the class name must end in "Controller".
We use attribute-based routing.

Configure attribute-based routing in the application start event. We don't 
use the older MapRoute-based configuration in our projects.

Create a "Views" folder (following the ViewEngine conventions), and add a Web.config 
file to configure razor and prevent *.cshtml and other files from being served or
executed when accessed directly (remember that when a file path exists, it isn't routed).
In other words, stuff in the views folder should only be accessible via routes and the 
view engine, not http://localhost/Views.

Add a view. Visual Studio 2015 will only help you create Razor views these days, but 
if you want to create an *.aspx, create it manually and just make sure you change the 
base class to ViewPage:

		public partial class Index : System.Web.Mvc.ViewPage
		{
			.
			.
			.
		}

Add a model, pass it to the view in the controller. Optionally mark the view as 
strongly-typed. You can do this with *.aspx files as well, just use ViewPage<T>.

Use a layout.

Move the layout code to a _ViewStart file (note case of the word viewstart doesn't matter).

Done.


=============================================================
====	Manual Installation of MVC 5 (with Razor & Roslyn)
=============================================================

The MVC NuGet packages aren't magic, and it helps to understand how to create an MVC project
without using NuGet at all.

Install the "MVC 5.2.3" assembly:

		..\packages\Microsoft.AspNet.Mvc.5.2.3\lib\net45\System.Web.Mvc.dll

If you refrence older assemblies that bind against an older version of MVC, add this to 
web.config for backwards compatibility:

		<runtime>
			<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
				<dependentAssembly>
					<assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35"/>
					<bindingRedirect oldVersion="1.0.0.0-5.2.3.0" newVersion="5.2.3.0"/>
				</dependentAssembly>
			</assemblyBinding>
		</runtime>


Install Razor 3.2.3 and related assemblies:

		..\packages\Microsoft.AspNet.Razor.3.2.3\lib\net45\System.Web.Razor.dll
		..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.Helpers.dll
		..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.dll
		..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Deployment.dll
		..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Razor.dll
		..\packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll

Note: Not all of these are required at compile time - and at runtime they run from the GAC.

Again, for backwards compatibility of older assemblies:

		<runtime>
			<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
			  <dependentAssembly>
				<assemblyIdentity name="System.Web.Helpers" publicKeyToken="31bf3856ad364e35"/>
				<bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0"/>
			  </dependentAssembly>
			  <dependentAssembly>
				<assemblyIdentity name="System.Web.WebPages" publicKeyToken="31bf3856ad364e35"/>
				<bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0"/>
			  </dependentAssembly>
			</assemblyBinding>
		</runtime>


Install Roslyn 1.0.1:

If you want to use C# 6.0 features in your Razor views (and you do), then you'll have to install
the new Roslyn compiler service. Visual Studio 2015 and the 4.6 framework compiler uses the new
C# 6.0 features automatically, but these aren't used for Razor views. Instead, ASP.NET will use
the older CodeDomProvider (System.dll) to compile your views, and this doesn't support C# 6.0.

So if you want to use, say "string interpolation" (in a Razor view), you'll need to install
Roslyn:

		@($"this is foo: {foo}")

Unfortunately, this is pretty complicated to do manually, so you'll still want to use NuGet for
this. But we'll explain what the NuGet package actually does:

Install the NuGet package  "CodeDOM Providers for .NET Compiler" v1.0.1, which will add the
following reference to your project:

		..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.1\
								lib\net45\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll

The Roslyn package depends on the "Microsoft.Net.Compilers" 1.0.0 package, but only for the
toolchain (it doesn't add any references to the project).								
								
The installer will also add the following target to your *.csproj file:

		<Target Name="EnsureNuGetPackageBuildImports" BeforeTargets="PrepareForBuild">
			...
		</Target>

Which will copy the csc.exe compiler to your output folder here:

		bin\roslyn

And add the following to your web.config:

		<system.codedom>
			<compilers>
				<compiler language="c#;cs;csharp" extension=".cs"
				type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
				warningLevel="4" compilerOptions="/langversion:6 /nowarn:1659;1699;1701"/>
				<compiler language="vb;vbs;visualbasic;vbscript" extension=".vb"
				type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
				warningLevel="4" compilerOptions="/langversion:14 /nowarn:41008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+"/>
			</compilers>
		</system.codedom>



=============================================================
====	Forms Authentication & MVC
=============================================================


use anonymous

app domain user
aspnet
net serv

client impersonation
app impersonation

request user

iis vs asp authentication
asp impersonation
 asp identity

 thread user vs httpcontext user

Crypto.HashPassword(password)
Crypto.VerifyHashedPassword

=============================================================
====	Forms Authentication & Ajax
=============================================================

if (401) reload()

302

HttpResponse.SuppressFormsAuthenticationRedirect
Request.IsAjaxRequest

X-Requested-With: XMLHttpRequest


=============================================================
====	XSS & MVC
=============================================================


[AllowHtml] or [ValidateInput(false)]
Html.Raw is used. Html.AntiForgeryToken()
AntiXSS NuGet (if you need to accept HTML from users)

