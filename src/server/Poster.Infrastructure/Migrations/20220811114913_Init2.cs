using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poster.Infrastructure.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAuthorId",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAuthorId",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
