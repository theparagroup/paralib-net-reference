using System;
using FluentMigrator;
using com.paralib.DataAnnotations;
using com.paralib.Migrations;

namespace com.paralib.reference.migrations
{

    [Migration(062120160001)]
    public class InitialSchema : Migration
    {

        public override void Down()
        {
            Delete.StandardLogTable();

            

            Delete.Table("foo");
        }

        public override void Up()
        {

            Create.Table("foo")
                .WithColumn("id").AsParaType(ParaTypes.Key).NotNullable().PrimaryKey()
                .WithColumn("blob").AsParaType(ParaTypes.Blob).NotNullable()
                .WithColumn("maxtext").AsParaType(ParaTypes.MaxText).NotNullable()
                .WithColumn("name").AsParaType(ParaTypes.Name).NotNullable()
                .WithColumn("email").AsParaType(ParaTypes.Email).NotNullable()
                .WithColumn("parent_tag_id").AsInt32().Nullable();

            Create.StandardLogTable();

        }
    }
}
