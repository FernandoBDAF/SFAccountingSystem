using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class recordofxsubgroupajust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordOFX_Intermediation_IntermediationId",
                table: "RecordOFX");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOFX_Users_UserId",
                table: "RecordOFX");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RecordOFX",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IntermediationId",
                table: "RecordOFX",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOFX_Intermediation_IntermediationId",
                table: "RecordOFX",
                column: "IntermediationId",
                principalTable: "Intermediation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOFX_Users_UserId",
                table: "RecordOFX",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordOFX_Intermediation_IntermediationId",
                table: "RecordOFX");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordOFX_Users_UserId",
                table: "RecordOFX");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RecordOFX",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IntermediationId",
                table: "RecordOFX",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOFX_Intermediation_IntermediationId",
                table: "RecordOFX",
                column: "IntermediationId",
                principalTable: "Intermediation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordOFX_Users_UserId",
                table: "RecordOFX",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
