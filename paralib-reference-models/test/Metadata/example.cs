using System;
using System.ComponentModel.DataAnnotations;
using com.paralib.DataAnnotations;
using com.paralib.reference.models.Models.Metadata;


namespace com.paralib.reference.models.Models
{
	[MetadataType(typeof(exampleMetadata))]
	public partial class example
	{
	}
}

namespace com.paralib.reference.models.Models.Metadata
{
	public class exampleMetadata
	{

		[Display(Name="id")]
		[Required(ErrorMessage="id is required")]
		public object id;

		[Display(Name="Ansi String")]
		[Required(ErrorMessage="Ansi String is required")]
		[StringLength(100)]
		public object AnsiString;

		[Display(Name="Binary")]
		[Required(ErrorMessage="Binary is required")]
		public object Binary;

		[Display(Name="Boolean")]
		[Required(ErrorMessage="Boolean is required")]
		public object Boolean;

		[Display(Name="Byte")]
		[Required(ErrorMessage="Byte is required")]
		public object Byte;

		[Display(Name="Currency")]
		[Required(ErrorMessage="Currency is required")]
		public object Currency;

		[Display(Name="Date")]
		[Required(ErrorMessage="Date is required")]
		public object Date;

		[Display(Name="Date Time")]
		[Required(ErrorMessage="Date Time is required")]
		public object DateTime;

		[Display(Name="Date Time Offset")]
		[Required(ErrorMessage="Date Time Offset is required")]
		public object DateTimeOffset;

		[Display(Name="Decimal_1_1")]
		[Required(ErrorMessage="Decimal_1_1 is required")]
		public object Decimal_1_1;

		[Display(Name="Decimal")]
		[Required(ErrorMessage="Decimal is required")]
		public object Decimal;

		[Display(Name="Double")]
		[Required(ErrorMessage="Double is required")]
		public object Double;

		[Display(Name="Fixed Length Ansi String")]
		[Required(ErrorMessage="Fixed Length Ansi String is required")]
		[StringLength(100)]
		public object FixedLengthAnsiString;

		[Display(Name="Fixed Length String")]
		[Required(ErrorMessage="Fixed Length String is required")]
		[StringLength(100)]
		public object FixedLengthString;

		[Display(Name="Float")]
		[Required(ErrorMessage="Float is required")]
		public object Float;

		[Display(Name="Guid")]
		[Required(ErrorMessage="Guid is required")]
		public object Guid;

		[Display(Name="Int16")]
		[Required(ErrorMessage="Int16 is required")]
		public object Int16;

		[Display(Name="Int32")]
		public object Int32;

		[Display(Name="Int64")]
		[Required(ErrorMessage="Int64 is required")]
		public object Int64;

		[Display(Name="String")]
		[Required(ErrorMessage="String is required")]
		[StringLength(100)]
		public object String;

		[Display(Name="Time")]
		[Required(ErrorMessage="Time is required")]
		public object Time;
	}
}
