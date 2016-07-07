using System;
using System.Web.Mvc;
using com.paralib.Mvc.Authorization;
using com.paralib.Mvc.Authentication;
using com.paralib.reference.mvc.Areas.Admin.Models;

namespace com.paralib.reference.mvc.Areas.Admin
{
    [RouteArea("admin")]
    [Permissions(UnauthenticatedUrl = "~/admin/login", UnauthorizedUrl = "~/admin/error", Roles ="star,belly")]
    public abstract class AdminController : Controller
    {

    }
}