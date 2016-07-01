using System;

namespace com.paralib.reference.models
{
	public partial class Example
	{
		public int Id { get; set;}
		public string AnsiString { get; set;}
		public byte[] Binary { get; set;}
		public bool Boolean { get; set;}
		public byte Byte { get; set;}
		public decimal Currency { get; set;}
		public DateTime Date { get; set;}
		public DateTime DateTime { get; set;}
		public DateTimeOffset DateTimeOffset { get; set;}
		public decimal Decimal11 { get; set;}
		public decimal Decimal { get; set;}
		public double Double { get; set;}
		public string FixedLengthAnsiString { get; set;}
		public string FixedLengthString { get; set;}
		public float Float { get; set;}
		public Guid Guid { get; set;}
		public short Int16 { get; set;}
		public int? Int32 { get; set;}
		public long Int64 { get; set;}
		public string String { get; set;}
		public TimeSpan Time { get; set;}
	}
}
