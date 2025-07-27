using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnProductPriceTOTotalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "ordersDetails",
                newName: "TotalAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "ordersDetails",
                newName: "ProductPrice");
        }
    }
}
