using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFITIDFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FITID",
                table: "RecordOFX",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FITID",
                table: "RecordOFX",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
