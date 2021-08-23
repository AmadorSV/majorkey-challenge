using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Persistence.Migrations
{
    public partial class UpdatedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ServiceRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ServiceRequests",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ServiceRequests");
        }
    }
}
