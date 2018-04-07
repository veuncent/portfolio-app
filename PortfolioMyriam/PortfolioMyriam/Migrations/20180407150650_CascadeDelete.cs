using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioMyriam.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItem_ExternalReference_ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioItem_ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.DropColumn(
                name: "ExternalReferenceId",
                table: "PortfolioItem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExternalReference",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalReference_PortfolioItem_Id",
                table: "ExternalReference",
                column: "Id",
                principalTable: "PortfolioItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalReference_PortfolioItem_Id",
                table: "ExternalReference");

            migrationBuilder.AddColumn<int>(
                name: "ExternalReferenceId",
                table: "PortfolioItem",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExternalReference",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

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
    }
}
