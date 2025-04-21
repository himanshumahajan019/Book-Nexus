using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_DataAccess.Migrations
{
    public partial class SDCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCategory
            @name varchar(50)
            As
            insert Categories values(@name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCategory
            @id int,
            @name varchar(50)
            As
            update Categories set name=@name where id = @id");

            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCategory
            @id int
            As
            delete from Categories where id= @id");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCategorys
            As
            select * from Categories");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCategory
            @id int
            As
            select * from Categories where id=@id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
