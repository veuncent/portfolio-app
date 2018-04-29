using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PortfolioMyriam.Migrations
{
    public partial class ProjectTypeNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Projects\" SET \"ProjectType\" = 2 WHERE \"ProjectType\" IS NULL;");
            migrationBuilder.AlterColumn<int>(
                name: "ProjectType",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectType",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
