﻿=============================================================
====	General Naming Conventions
=============================================================

Unless there is a decent reason, we follow Microsoft conventions.

terms we use:
		lower underscored="hello_there"
		upper underscored="Hello_There"
		lower dotted = "hello.there"
		upper dotted = "Hello.There"
		upper camel (UC) = "HelloThere"
		lower camel (LC)= "helloThere"

typenames
		class -> TypeName
		struct -> TypeName
		inteferface -> ITypeName
		enum -> TypeNames (plural, e.g. RenderModes)

members
		private fields ->  _memberName (lower camel with underscore prefix)
		public fields -> MemberName (upper camel)
		methods, public/private properties -> MemberName (upper camel)
		members involving enums -> TypeName (singular, e.g. RenderMode)

local (stack variables)
		lower camel
		NO var keyword (unless you have a damn good reason other than how lazy you are)

=============================================================
====	Namespaces
=============================================================

re-useable library namespaces (e.g. paralib)
		prefixed with reverse DNS name, lower dotted (e.g. com.paralib.mvc)
		then upper dotted (e.g. com.paralib.mvc.Authentication)

in non-library projects (e.g. oovent) use whatever makes sense

use ("using") only what's needed + System

order:
		microsoft first, by length
		externals, by length
		internals

when "wrapping" other classes, re-use the original namecase and alias (uppercase)
the original. example:
	
		using System;
		using System.Collections;
		using NET=System.Configuration;
		using log4net;
		using com.paralib.common.Whatever;

		namespace com.paralib.common.Configuration
		{
			public class ConfigurationManager
			{
				public void WrapSomething()
				{
					return NET.ConfigurationManager.Something();
				}
			}
		}

=============================================================
====	Database Conventions
=============================================================

We use FluentMigrator to create our databases, even when using EF
or other ORMs (no code first).

tables -> lower underscore, plural (e.g. users)
columns -> lower underscore

generally:
	table names should match models names (e.g. user_roles -> UserRole)
	column names should match member names (e.g. role_id -> RoleId)


=============================================================
====	Migrations
=============================================================

filename should be "MMDDYYYYSSSS-TypeName.cs"
	where SSSS is a squence
	TypeName is something meaningful (e.g. "InitialSchema")

MMDDYYYYSSSS goes in MigrationAttribute
TypeName is the name of the class
Down() before Up() in file

Commonly used schema fragments are located in the paralib-migrations 
project.


