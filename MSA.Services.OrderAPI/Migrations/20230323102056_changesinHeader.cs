using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class changesinHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartotalItems",
                table: "OrderHeaders",
                newName: "CartTotalItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartTotalItems",
                table: "OrderHeaders",
                newName: "CartotalItems");
        }
    }
}
