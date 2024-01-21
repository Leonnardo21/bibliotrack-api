using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTrack.Migrations
{
    public partial class NewBookData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisponibility",
                table: "Book",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisponibility",
                table: "Book");
        }
    }
}
