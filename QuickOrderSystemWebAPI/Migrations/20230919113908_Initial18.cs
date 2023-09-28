using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickOrderSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Orders",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Orders",
                newName: "OrderID");
        }
    }
}
