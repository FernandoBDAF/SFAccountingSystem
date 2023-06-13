using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordOfxIdInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordOfxId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_RecordOfxId",
                table: "Invoice",
                column: "RecordOfxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_RecordOFX_RecordOfxId",
                table: "Invoice",
                column: "RecordOfxId",
                principalTable: "RecordOFX",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_RecordOFX_RecordOfxId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_RecordOfxId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "RecordOfxId",
                table: "Invoice");
        }
    }
}
