using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickOrderSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QrCode",
                table: "QrCode");

            migrationBuilder.RenameTable(
                name: "QrCode",
                newName: "QrCodes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "QrCodes",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QrCodes",
                table: "QrCodes",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QrCodes",
                table: "QrCodes");

            migrationBuilder.RenameTable(
                name: "QrCodes",
                newName: "QrCode");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "QrCode",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QrCode",
                table: "QrCode",
                column: "Id");
        }
    }
}
