using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using com.paralib.DataAnnotations;
using com.paralib.Migrations;

namespace com.paralib.reference.migrations
{

    [Migration(062920160001)]
    public class Example : ParaMigration
    {

        public override void OnDown()
        {
            Delete.Table("example");
        }

        public override void OnUp()
        {
            Create.Table("example")
                .WithColumn("id").AsInt32().PrimaryKey()
                .WithColumn("AnsiString").AsAnsiString(100).WithDefaultValue("")
                .WithColumn("Binary").AsBinary(100).WithDefaultValue(new byte[] { 1, 2, 3, 4, 5 })
                .WithColumn("Boolean").AsBoolean().WithDefaultValue(true)
                .WithColumn("Byte").AsByte().WithDefaultValue(0)
                .WithColumn("Currency").AsCurrency().WithDefaultValue(0)
                .WithColumn("Date").AsDate().WithDefaultValue("getdate()")
                .WithColumn("DateTime").AsDateTime().WithDefaultValue("getdate()")
                .WithColumn("DateTimeOffset").AsDateTimeOffset().WithDefaultValue("getdate()")
                .WithColumn("Decimal_1_1").AsDecimal(1, 1).WithDefaultValue(0)
                .WithColumn("Decimal").AsDecimal().WithDefaultValue(0)
                .WithColumn("Double").AsDouble().WithDefaultValue(0)
                .WithColumn("FixedLengthAnsiString").AsFixedLengthAnsiString(100).WithDefaultValue("")
                .WithColumn("FixedLengthString").AsFixedLengthString(100).WithDefaultValue("")
                .WithColumn("Float").AsFloat().WithDefaultValue(0)
                .WithColumn("Guid").AsGuid().WithDefaultValue("newid()")
                .WithColumn("Int16").AsInt16().WithDefaultValue(0)
                .WithColumn("Int32").AsInt32().WithDefaultValue(0)
                .WithColumn("Int64").AsInt64().WithDefaultValue(0)
                .WithColumn("String").AsString(100).WithDefaultValue("")
                .WithColumn("Time").AsTime().WithDefaultValue("getdate()");
              //.WithColumn("int").AsXml(100);

            Insert.IntoTable("example").Row(new {id=1 });

        }
    }
}
