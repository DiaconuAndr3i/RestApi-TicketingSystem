using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketingSystem_Helpdesk.Migrations
{
    public partial class AddRefreshTokenOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokens_Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokens_CreatedByIp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokens_Expires",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokens_ReplacedByToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokens_Revoked",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokens_RevokedByIp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokens_Token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokens_Created",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_CreatedByIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_Expires",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_ReplacedByToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_Revoked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_RevokedByIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokens_Token",
                table: "AspNetUsers");
        }
    }
}
