using System;
using System.ComponentModel.DataAnnotations;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Models.Metadata;


namespace com.paralib.reference.models.Models
{
	[MetadataType(typeof(EmployeeMetadata))]
	public partial class Employee
	{
	}
}

namespace com.paralib.reference.models.Models.Metadata
{
	public class EmployeeMetadata
	{

		[Display(Name="Id")]
		[Required(ErrorMessage="Id is required")]
		[ParaType(ParaTypes.Key)]
		public object Id;

		[Display(Name="Employee Type Id")]
		[Required(ErrorMessage="Employee Type Id is required")]
		[ParaType(ParaTypes.Key)]
		public object EmployeeTypeId;

		[Display(Name="Name")]
		[Required(ErrorMessage="Name is required")]
		[ParaType(ParaTypes.Name)]
		public object Name;

		[Display(Name="Email")]
		[Required(ErrorMessage="Email is required")]
		[ParaType(ParaTypes.Email)]
		public object Email;

		[Display(Name="Company Id")]
		[Required(ErrorMessage="Company Id is required")]
		[ParaType(ParaTypes.Key)]
		public object CompanyId;
	}
}
