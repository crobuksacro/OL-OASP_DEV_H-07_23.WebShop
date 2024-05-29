using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OL_OASP_DEV_H_07_23.WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class companyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "ProductCategorys",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companys_Addresss_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresss",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategorys_CompanyId",
                table: "ProductCategorys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_AddressId",
                table: "Companys",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategorys_Companys_CompanyId",
                table: "ProductCategorys",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategorys_Companys_CompanyId",
                table: "ProductCategorys");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategorys_CompanyId",
                table: "ProductCategorys");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProductCategorys");
        }
    }
}
