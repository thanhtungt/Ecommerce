using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Entity.Migrations
{
    /// <inheritdoc />
    public partial class add0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 26, 8, 56, 8, 305, DateTimeKind.Local).AddTicks(7307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 26, 8, 46, 29, 613, DateTimeKind.Local).AddTicks(7617));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3948ee9d-a4b8-41ea-84e7-d969d8d3699f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea49fc91-78cf-4940-a931-6d3d918cba18", "AQAAAAEAACcQAAAAEE9WOOQsRka44p1FMrHkCJ0n5QjnG+y3z5XIzF6uF9/3KNJASwS6pUMXKwplMDDLGg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParentId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParentId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2023, 9, 26, 8, 56, 8, 307, DateTimeKind.Local).AddTicks(3604));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2023, 9, 26, 8, 56, 8, 307, DateTimeKind.Local).AddTicks(3605));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 26, 8, 46, 29, 613, DateTimeKind.Local).AddTicks(7617),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 26, 8, 56, 8, 305, DateTimeKind.Local).AddTicks(7307));

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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParentId",
                value: null);

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
    }
}
