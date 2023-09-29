using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Entity.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Carts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 26, 8, 46, 29, 613, DateTimeKind.Local).AddTicks(7617),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 25, 15, 22, 3, 874, DateTimeKind.Local).AddTicks(8425));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "699c2266-7a89-49c3-b7c6-3e9ec865a94a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "73c145e5-677b-4286-8d14-1139b2a72514", "AQAAAAEAACcQAAAAECf7MJp0+ErcSu8eTEYZTTjwxIIHiwkV2g3tqEy8DzRjG7pj/FBjhPG7z7gIIjWLMw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2023, 9, 26, 8, 46, 29, 615, DateTimeKind.Local).AddTicks(4006));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2023, 9, 26, 8, 46, 29, 615, DateTimeKind.Local).AddTicks(4008));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Carts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 25, 15, 22, 3, 874, DateTimeKind.Local).AddTicks(8425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 26, 8, 46, 29, 613, DateTimeKind.Local).AddTicks(7617));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "926c2700-07ac-49e1-97b7-ec87090b254d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e1a5d6ab-d0ff-43c0-a75b-af5a06545cd4", "AQAAAAEAACcQAAAAEDVfdvb9KY+xrdk26EFRMpmhldy46LK71dQfiQ+ov5WpAYy/b72xJBXxJR1or1KHXA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2023, 9, 25, 15, 22, 3, 876, DateTimeKind.Local).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2023, 9, 25, 15, 22, 3, 876, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");
        }
    }
}
