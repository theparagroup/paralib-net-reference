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

In IIS7, the anonymous account was changed to the built-in IUSR, and optionally can be a
new concept called ApplicationPoolIdentity. Also, ASP.NET has been fully integrated into
IIS, and the ISAPI extension is no longer used (unless you are in "Classic mode").

Note: IIS7 Integrated Mode does not support ASP Impersonation (see below).

VERSION		IIS Process/Account		ASP.NET Worker Process/Account	IIS Anonymous Account
-------		--------------------	------------------------------	-----------------------
5			inetinfo.exe/SYSTEM		aspnet_wp.exe/ASPNET			IUSR_MachineName
6			(kernel)				w3wp.exe/NETWORKSERVICE			IUSR_MachineName
7			(kernel)				w3wp.exe/ApplicationPoolId		IUSR


You can access the account that is actually executing this way:

		System.Security.Principal.WindowsIdentity.GetCurrent().Name

This will either be the Worker Process Account or something else if impersonation is enabled.

You can see the account in the "access token" that IIS sends to ASP.NET this way:

		Request.LogonUserIdentity.Name

This will either be the authenticated user (basic or windows) or the anonymous user (IUSR).



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
====	ASP.NET Authentication
=============================================================

ASP.NET provides an additional authentication layer. At this layer, we can set the current
authenticated user for the current request (and thread) using the following properties:

		System.Threading.Thread.CurrentPrincipal
		System.Web.HttpContext.User

Both of which are of type IPrincipal.

We use the following web.config entry:

		<system.web>
			<authentication mode= "[Windows|Forms|Passport|None]"/>

ASP.NET "Windows" Authentication will take the access token from IIS (only if IIS Authentication
Windows or Basic is enabled) and set these properties to the user in the token.

"Forms" enables the "Forms Authentication" mechanism where the application can define its own
custom users and roles (stored in a database, for example).

"None" does nothing, and again allows the application to set the IPrincipal in a custom manner.

=============================================================
====	Forms Authentication
=============================================================

genericprincipal
formsidentity

=============================================================
====	ASP.NET Impersonation & Delegation
=============================================================

Normally ASP.NET applications execute using the security context of the worker process. This is
not to be confused with the authenticated user (the access token).

If desired, the application can be configured to run in the security context of the requesting
user, or a completely different user:

	<system.web>
		<identity impersonate="true" userName="domain\user" password="password" />

Note: impersonation is not supported in II7 Integrated mode, but it is supported in Classic mode.

Note: a related but distinct concept is delegation. This is where IIS bounces the
user credentials against another server. NTLM doesn't support it, but Kerberos
and Basic do support it. You can always call LogonUser with "Interactive."

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

ASP.NET Web Pages (System.Web.WebPages.dll) are a new technology that simplifies the 
Web Form model, and allows for different templating languages to be used in ASP.NET. 
Web Pages debuted with MVC3 and the WebMatrix IDE, but is a distinct technology. 

Web Pages handles web requests via the WebPageHttpModule and WebPageHttpHandler classes,
and leverages the custom ASP.NET BuildProvider mechanism to allow different languages to 
be installed, such as Razor.

Web Pages are very similiar to classic ASP in that it is a very simple and lightweight 
templating engine. The main differences are that Web Pages are compiled and can use the
full .NET CLR.

Note on Razor: It is important to understand that while Razor is designed to be an HTML
templating engine (it understands HTML <tag> syntax), and it is located in the "System.Web"
namespace (System.Web.Razor.dll), Razor actually has no dependencies on ASP.NET. Razor can
be used completely independently of MVC or ASP.NET.

Certain constructs (@Html, Layouts & @RenderSection, etc.) are actually part of MVC and 
technically not Razor or Web Pages.

=============================================================
====	Zero Configuration (PreApplicationStartMethodAttribute)
=============================================================

Since Web Pages are implemented as HttpModules and HttpHandlers, you may expect to see
registration entries in your Web.config files like this:

		<configuration>
		  <system.web>
			<httpModules>
			  <add name="HelloWorldModule" type="HelloWorldModule"/>
			 </httpModules>
		  </system.web>
		</configuration>
 
However, in ASP.NET 4 the PreApplicationStartMethodAttribute (System.Web.dll) was added, 
allowing for zero-config ASP.NET applications. This attribute is used at the assembly 
level and allows startup code to run before Application_Start. Used with the 
DynamicModuleUtility class (Microsoft.Web.Infrastructure.dll), assemblies such as
System.Web.WebPages.dll can configure themselves as soon as they are loaded.

The Razor BuildProviders for VB (*.vbhtml) and C# (*.cshtml) are installed by default for
MVC applications. This is also accomplished via the PreApplicationStartMethod attribute
(in System.Web.WebPages.Razor.dll), so when that assembly is loaded, so is Razor.



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

In this manner MVC jumps the gap between MVC-world and either ASPX-world or Razor-world.

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
as @Html and @ViewBag, but these are not part of any interface or abstract base class
(rather they are simply implemented in ViewPage and WebViewPage). However, regardless 
of which of the two built-in view technologies you are using, all of MVC idioms 
are supported.


=============================================================
====	MVC History
=============================================================

MVC1, 2009, .Net 3.5, Visual Studio 2008
	WebForms
	Html Helpers
	Ajax Helpers

MVC2, 2010, .Net 3.5/4.0, Visual Studio 2008/2010
	Strongly-typed helpers
	client-side validation
	Areas
	support for data annotations
	HttpGet, HttpPost, etc attributes

MVC3, 2011, .Net 4.0, Visual Studio 2010
	Razor (ASP.NET)
	ViewBag (ViewData that uses new dynamic ExpandoObject)
	Global Action Filters
	Unobtrusive JavaScript
	NuGet

MVC4, 2012, .Net 4.0/4.5, Visual Studio 2010/2012
	WebAPI (ASP.NET)
	Bundling & Minification

MVC5, 2013, .Net 4.0/4.5, Visual Studio 2013
    WebAPI2 (ASP.NET)
    Identity (ASP.NET)
    Scaffolding
    Authentication Filters





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

Add Microsoft.AspNet.Mvc 5 via NuGet

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


=============================================================
====	Ajax
=============================================================

if (401) reload()

302

HttpResponse.SuppressFormsAuthenticationRedirect
Request.IsAjaxRequest

X-Requested-With: XMLHttpRequest





