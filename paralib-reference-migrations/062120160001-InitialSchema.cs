using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using com.paralib.DataAnnotations;
using com.paralib.Migrations;

namespace com.paralib.reference.migrations
{

    [Migration(062120160001)]
    public class InitialSchema : ParaMigration
    {

        public override void OnDown()
        {
            Delete.Table("employees");
            Delete.Table("companies");
            Delete.ColumnMetadataTable();
            Delete.StandardLogTable();
        }

        public override void OnUp()
        {
            Create.StandardLogTable();
            Create.ColumnMetadataTable();

            Create.Table("companies")
                .WithColumn("id").AsParaType(ParaTypes.Key).PrimaryKey().Identity()
                .WithColumn("name").AsParaType(ParaTypes.Name).NotNullable();

            Create.Table("employees")
                .WithColumn("id").AsParaType(ParaTypes.Key).PrimaryKey().Identity()
                .WithColumn("name").AsParaType(ParaTypes.Name).NotNullable()
                .WithColumn("email").AsParaType(ParaTypes.Email, "employee's corporate email address").NotNullable()
                .WithColumn("company_id").AsParaType(ParaTypes.Key).NotNullable().ForeignKey("companies", "id");


            Insert.IntoTable("companies").WithIdentityInsert().Row(new { id = 1, name="ACME" });
            Insert.IntoTable("employees").Row(new { name = "John", email="john@acme.org", company_id=1 });
            Insert.IntoTable("employees").Row(new { name = "Jack", email = "jack@acme.org", company_id = 1 });

            Insert.IntoTable("companies").WithIdentityInsert().Row(new { Id = 2, Name = "Foo Co." });
            Insert.IntoTable("employees").Row(new { name = "Johnny", email = "johnny@foo.org", company_id = 2 });
            Insert.IntoTable("employees").Row(new { name = "Jackson", email = "jackson@foo.org", company_id = 2 });


        }
    }
}
