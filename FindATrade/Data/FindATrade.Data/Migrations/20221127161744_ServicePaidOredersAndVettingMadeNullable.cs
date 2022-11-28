using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindATrade.Data.Migrations
{
    public partial class ServicePaidOredersAndVettingMadeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VettingId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "VettingId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaidOrderId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Services_VettingId",
                table: "Services",
                column: "VettingId",
                unique: true,
                filter: "[VettingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services",
                column: "VettingId",
                principalTable: "Vettings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VettingId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "VettingId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaidOrderId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_VettingId",
                table: "Services",
                column: "VettingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services",
                column: "VettingId",
                principalTable: "Vettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
