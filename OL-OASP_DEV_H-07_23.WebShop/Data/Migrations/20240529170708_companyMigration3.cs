using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OL_OASP_DEV_H_07_23.WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class companyMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuantityTypeId",
                table: "ProductItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuantityTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QuantityTypes",
                columns: new[] { "Id", "Created", "Description", "Name", "Updated", "Valid" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mjesec", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 3L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Godina", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 4L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Litra", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 6L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 7L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Komad", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_QuantityTypeId",
                table: "ProductItems",
                column: "QuantityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_QuantityTypes_QuantityTypeId",
                table: "ProductItems",
                column: "QuantityTypeId",
                principalTable: "QuantityTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_QuantityTypes_QuantityTypeId",
                table: "ProductItems");

            migrationBuilder.DropTable(
                name: "QuantityTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_QuantityTypeId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "QuantityTypeId",
                table: "ProductItems");
        }
    }
}
