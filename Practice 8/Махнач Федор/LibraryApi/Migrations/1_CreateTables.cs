using FluentMigrator;

namespace LibraryApi.Migrations
{
    [Migration(1)]
    public class CreateBook : Migration
    {
        public override void Up()
        {
            // Readers
            Create.Table("reader")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("address").AsString()
                .WithColumn("birth_date").AsDate();

            // Copies
            Create.Table("copy")
                .WithColumn("isbn").AsString().NotNullable()
                .WithColumn("copy_number").AsInt32().NotNullable()
                .WithColumn("shelf_position").AsString();
            Create.PrimaryKey()
                .OnTable("copy").Columns("isbn", "copy_number");
            Create.ForeignKey()
                .FromTable("copy").ForeignColumn("isbn")
                .ToTable("book").PrimaryColumn("isbn");
            
            // Borrowings
            Create.Table("borrowing")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("reader_id").AsInt64().NotNullable()
                .WithColumn("isbn").AsString().NotNullable()
                .WithColumn("copy_number").AsInt32().NotNullable()
                .WithColumn("return_date").AsDate();
            Create.ForeignKey()
                .FromTable("borrowing").ForeignColumn("reader_id")
                .ToTable("reader").PrimaryColumn("id");
            Create.ForeignKey()
                .FromTable("borrowing").ForeignColumns("isbn", "copy_number")
                .ToTable("copy").PrimaryColumns("isbn", "copy_number");
            
            // Publishers
            Create.Table("publisher")
                .WithColumn("name").AsString().PrimaryKey().NotNullable()
                .WithColumn("address").AsString();
            
            // Categories
            Create.Table("category")
                .WithColumn("category_name").AsString().PrimaryKey().NotNullable()
                .WithColumn("parent_category").AsString();
            
            // Book categories
            Create.Table("book_category")
                .WithColumn("isbn").AsString().NotNullable()
                .WithColumn("category_name").AsString().NotNullable();
            Create.PrimaryKey()
                .OnTable("book_category").Columns("isbn", "category_name");
            Create.ForeignKey()
                .FromTable("book_category").ForeignColumn("category_name")
                .ToTable("category").PrimaryColumn("category_name");
        }

        public override void Down()
        {
            Delete.Table("book_category");
            Delete.Table("category");
            Delete.Table("publisher");
            Delete.Table("borrowing");
            Delete.Table("copy");
            Delete.Table("reader");
        }
    }
}