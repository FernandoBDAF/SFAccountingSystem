using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class Starting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordOFXSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordOFXSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordOFXSubGroups_RecordOFXSubGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "RecordOFXSubGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<int>(type: "int", nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intermediation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intermediation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intermediation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordOFX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bank = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IntermediationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: true),
                    SubGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordOFX", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordOFX_Intermediation_IntermediationId",
                        column: x => x.IntermediationId,
                        principalTable: "Intermediation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordOFX_RecordOFXSubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "RecordOFXSubGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecordOFX_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intermediation_UserId",
                table: "Intermediation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordOFX_IntermediationId",
                table: "RecordOFX",
                column: "IntermediationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordOFX_SubGroupId",
                table: "RecordOFX",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordOFX_UserId",
                table: "RecordOFX",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordOFXSubGroups_ParentId",
                table: "RecordOFXSubGroups",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordOFX");

            migrationBuilder.DropTable(
                name: "Intermediation");

            migrationBuilder.DropTable(
                name: "RecordOFXSubGroups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
