using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using com.paralib.DataAnnotations;
using com.paralib.Migrations;

namespace com.paralib.reference.migrations
{

    [Migration(062820160001)]
    public class Testing : ParaMigration
    {

        public override void OnDown()
        {
            Alter.Column("name").OnTable("companies").AsParaType(ParaTypes.Name);
            Delete.Column("fooville").FromTable("companies");
        }

        public override void OnUp()
        {
            Alter.Column("name").OnTable("companies").AsParaType(ParaTypes.Description);
            Alter.Table("companies").AddColumn("fooville").AsParaType(ParaTypes.City).Nullable();

        }
    }
}
