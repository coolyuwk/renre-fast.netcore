using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenRen.Fast.Api.Migrations
{
    public partial class _1028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "app_user",
                maxLength: 32,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "sys_config",
                keyColumn: "id",
                keyValue: 1L,
                column: "status",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "user_id",
                keyValue: "00000000000000000000000000000000",
                column: "create_time",
                value: new DateTime(2020, 7, 3, 10, 28, 59, 614, DateTimeKind.Local).AddTicks(7428));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "app_user");

            migrationBuilder.UpdateData(
                table: "sys_config",
                keyColumn: "id",
                keyValue: 1L,
                column: "status",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "user_id",
                keyValue: "00000000000000000000000000000000",
                column: "create_time",
                value: new DateTime(2020, 6, 30, 23, 59, 16, 444, DateTimeKind.Local).AddTicks(6816));
        }
    }
}
