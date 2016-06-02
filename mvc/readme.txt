=============================================================
====	Creating an MVC Project (the hard way)
=============================================================

Create new project:
	Framework 4.6
	ASP.NET Web Application
	Empty 4.6 Template
	Uncheck "Host in the cloud"

Add Microsoft.AspNet.Mvc 5 via NuGet

Create a global event handler (Global.asax)

Tell the routing system to NOT pass resource requests (*.axd) to controllers
Instead, the routing system will stop and let IIS handle these as normal.

Create a controller. The containing folder name doesn't matter, but the class
should derive from Controller and the class name must end in "Controller".
We use attribute-based routing.

Configure attribute-based routing in the application start event.