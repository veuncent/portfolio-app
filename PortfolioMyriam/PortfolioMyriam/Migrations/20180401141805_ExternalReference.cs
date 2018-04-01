using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioMyriam.Migrations
{
    public partial class ExternalReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalReference",
                table: "PortfolioItem");

            migrationBuilder.AddColumn<int>(
                name: "ExternalReferenceId",
                table: "PortfolioItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalReference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ExternalReferenceType = table.Column<int>(nullable: false),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalReference", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItem_ExternalReferenceId",
                table: "PortfolioItem",
                column: "ExternalReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioItem_ExternalReference_ExternalReferenceId",
                table: "PortfolioItem",
                column: "ExternalReferenceId",
                principalTable: "ExternalReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItem_ExternalReference_ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.DropTable(
                name: "ExternalReference");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioItem_ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.DropColumn(
                name: "ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.AddColumn<string>(
                name: "ExternalReference",
                table: "PortfolioItem",
                nullable: true);
        }
    }
}
