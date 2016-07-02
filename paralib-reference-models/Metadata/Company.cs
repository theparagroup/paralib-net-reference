using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Metadata;


namespace com.paralib.reference.models
{
	[MetadataType(typeof(CompanyMetadata))]
	public partial class Company
	{
	}
}

namespace com.paralib.reference.models.Metadata
{
	public class CompanyMetadata
	{

		[Key, Column(Order = 0)]
		[Display(Name="Id")]
		[Required(ErrorMessage="Id is required")]
		[ParaType(ParaTypes.Key)]
		public object Id;

		[Display(Name="Name")]
		[Required(ErrorMessage="Name is required")]
		[ParaType(ParaTypes.Description)]
		public object Name;

		[Display(Name="Fooville")]
		[ParaType(ParaTypes.City)]
		public object Fooville;
	}
}
