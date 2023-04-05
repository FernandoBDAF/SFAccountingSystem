using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddFITID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FITID",
                table: "RecordOFX",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FITID",
                table: "RecordOFX");
        }
    }
}
