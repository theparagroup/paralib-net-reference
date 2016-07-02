using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Metadata;


namespace com.paralib.reference.models
{
	[MetadataType(typeof(EmployeeTypeMetadata))]
	public partial class EmployeeType
	{
	}
}

namespace com.paralib.reference.models.Metadata
{
	public class EmployeeTypeMetadata
	{

		[Key, Column(Order = 0)]
		[Display(Name="Id")]
		[Required(ErrorMessage="Id is required")]
		[ParaType(ParaTypes.Key)]
		public object Id;

		[Display(Name="Name")]
		[Required(ErrorMessage="Name is required")]
		[ParaType(ParaTypes.Name)]
		public object Name;
	}
}
