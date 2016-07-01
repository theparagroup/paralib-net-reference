using System;
using System.ComponentModel.DataAnnotations;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Models.Metadata;


namespace com.paralib.reference.models.Models
{
	[MetadataType(typeof(employeesMetadata))]
	public partial class employees
	{
	}
}

namespace com.paralib.reference.models.Models.Metadata
{
	public class employeesMetadata
	{

		[Display(Name="id")]
		[Required(ErrorMessage="id is required")]
		[ParaType(ParaTypes.Key)]
		public object id;

		[Display(Name="name")]
		[Required(ErrorMessage="name is required")]
		[ParaType(ParaTypes.Name)]
		public object name;

		[Display(Name="email")]
		[Required(ErrorMessage="email is required")]
		[ParaType(ParaTypes.Email)]
		public object email;

		[Display(Name="company_id")]
		[Required(ErrorMessage="company_id is required")]
		[ParaType(ParaTypes.Key)]
		public object company_id;
	}
}
