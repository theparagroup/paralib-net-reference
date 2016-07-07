using System;
using System.Web.Mvc;
using com.paralib.Mvc.Infrastructure;

namespace com.paralib.reference.mvc.Areas.Admin
{
    public abstract class AdminView<TModel> : ParaView<TModel, AdminController>
    {
    }
}