using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFAccountingSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovedAtInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Invoice",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Invoice");
        }
    }
}
