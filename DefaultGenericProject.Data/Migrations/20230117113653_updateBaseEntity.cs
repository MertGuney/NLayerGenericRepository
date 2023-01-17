using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultGenericProject.Data.Migrations
{
    public partial class updateBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "UserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "UserRefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserRefreshTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
