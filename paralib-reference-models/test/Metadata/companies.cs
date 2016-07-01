using System;
using System.ComponentModel.DataAnnotations;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Models.Metadata;


namespace com.paralib.reference.models.Models
{
	[MetadataType(typeof(companiesMetadata))]
	public partial class companies
	{
	}
}

namespace com.paralib.reference.models.Models.Metadata
{
	public class companiesMetadata
	{

		[Display(Name="id")]
		[Required(ErrorMessage="id is required")]
		[ParaType(ParaTypes.Key)]
		public object id;

		[Display(Name="name")]
		[Required(ErrorMessage="name is required")]
		[ParaType(ParaTypes.Description)]
		public object name;

		[Display(Name="fooville")]
		[ParaType(ParaTypes.City)]
		public object fooville;
	}
}
