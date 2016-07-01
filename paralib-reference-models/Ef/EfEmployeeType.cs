using System;
using System.Collections.Generic;
using com.paralib.reference.models;

namespace com.paralib.reference.models.Ef
{
	public class EfEmployeeType:EmployeeType
	{
		public virtual List<Employee> Employees { get; set;}
	}
}
