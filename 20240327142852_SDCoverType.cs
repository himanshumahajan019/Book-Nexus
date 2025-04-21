using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_DataAccess.Migrations
{
    public partial class SDCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCoverType
            @name varchar(50)
            As
            insert CoverTypes values (@name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCoverType
            @id int,
            @name varchar(50)
            As
            update CoverTypes set name=@name Where id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCoverType 
            @id int
            As
            delete from CoverTypes Where id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverTypes
            As
            select * from CoverTypes");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverType
            @id int
            As
            select * from CoverTypes Where id=@id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
