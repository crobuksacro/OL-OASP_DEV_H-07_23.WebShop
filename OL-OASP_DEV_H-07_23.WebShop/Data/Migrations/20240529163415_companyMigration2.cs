using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OL_OASP_DEV_H_07_23.WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class companyMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresss",
                columns: new[] { "Id", "City", "Country", "Created", "Number", "Street", "Updated", "Valid" },
                values: new object[] { 1L, "Zagreb", "Hrvatska", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "100", "Maksimirska", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "AddressId", "Created", "FullName", "ShortName", "Updated", "VAT", "Valid" },
                values: new object[] { 1L, 1L, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tvrtka d.o.o.", "Tvrtka", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "71834573974", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Addresss",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
