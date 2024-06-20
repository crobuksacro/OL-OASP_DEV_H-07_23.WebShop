using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OL_OASP_DEV_H_07_23.WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderStatusMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");
        }
    }
}
