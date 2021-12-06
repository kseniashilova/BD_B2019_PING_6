using FluentMigrator;

namespace LibraryApi.Migrations
{
    [Migration(0)]
    public class CreateTableBook : Migration
    {
        public override void Up()
        {
            Create.Table("book")
                .WithColumn("isbn").AsString().PrimaryKey().NotNullable()
                .WithColumn("title").AsString().NotNullable()
                .WithColumn("author").AsString()
                .WithColumn("pages_num").AsInt32()
                .WithColumn("publish_year").AsInt32()
                .WithColumn("publisher_name").AsString();
        }

        public override void Down()
        {
            Delete.Table("book");
        }
    }
}