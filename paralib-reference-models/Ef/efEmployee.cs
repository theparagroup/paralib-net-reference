using System;
using System.Collections.Generic;
using com.paralib.reference.models;

namespace com.paralib.reference.models.Ef
{
	public class EfEmployee:Employee
	{
		public virtual EfCompany Company { get; set;}
		public virtual EfEmployeeType EmployeeType { get; set;}
	}
}
