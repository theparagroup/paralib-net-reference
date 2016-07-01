using System;

namespace com.paralib.reference.models
{
	public partial class Employee
	{
		public int Id { get; set;}
		public int EmployeeTypeId { get; set;}
		public string Name { get; set;}
		public string Email { get; set;}
		public int CompanyId { get; set;}
	}
}
