using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketingSystem_Helpdesk.Migrations
{
    public partial class AddUsersWithFirstLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "First_name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First_name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "AspNetUsers");
        }
    }
}
