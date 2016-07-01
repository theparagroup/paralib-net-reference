using System;
using System.Collections.Generic;
using com.paralib.reference.models;

namespace com.paralib.reference.models.Ef
{
	public class EfCompany:Company
	{
		public virtual List<EfEmployee> Employees { get; set;}
	}
}
