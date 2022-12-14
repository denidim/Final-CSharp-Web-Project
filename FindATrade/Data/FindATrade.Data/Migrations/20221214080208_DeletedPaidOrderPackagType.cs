using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindATrade.Data.Migrations
{
    public partial class DeletedPaidOrderPackagType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaidOrderPackageTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PaidOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PaidOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Terms",
                table: "PaidOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PaidOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PaidOrders");

            migrationBuilder.DropColumn(
                name: "Terms",
                table: "PaidOrders");

            migrationBuilder.CreateTable(
                name: "PaidOrderPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidOrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidOrderPackageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidOrderPackageTypes_PaidOrders_PaidOrderId",
                        column: x => x.PaidOrderId,
                        principalTable: "PaidOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaidOrderPackageTypes_IsDeleted",
                table: "PaidOrderPackageTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PaidOrderPackageTypes_PaidOrderId",
                table: "PaidOrderPackageTypes",
                column: "PaidOrderId",
                unique: true,
                filter: "[PaidOrderId] IS NOT NULL");
        }
    }
}
