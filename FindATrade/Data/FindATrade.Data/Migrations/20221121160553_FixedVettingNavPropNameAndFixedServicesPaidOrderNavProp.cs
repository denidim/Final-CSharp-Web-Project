#nullable disable

namespace FindATrade.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedVettingNavPropNameAndFixedServicesPaidOrderNavProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Vettings_Employees_VettetByEmpolyeeId",
                table: "Vettings");

            migrationBuilder.DropIndex(
                name: "IX_Vettings_VettetByEmpolyeeId",
                table: "Vettings");

            migrationBuilder.DropColumn(
                name: "VettetByEmpolyeeId",
                table: "Vettings");

            migrationBuilder.DropColumn(
                name: "PaidOrederId",
                table: "Services");

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
                name: "IX_Vettings_VettetByEmployeeId",
                table: "Vettings",
                column: "VettetByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings",
                column: "VettetByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings");

            migrationBuilder.DropIndex(
                name: "IX_Vettings_VettetByEmployeeId",
                table: "Vettings");

            migrationBuilder.AddColumn<int>(
                name: "VettetByEmpolyeeId",
                table: "Vettings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaidOrderId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PaidOrederId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vettings_VettetByEmpolyeeId",
                table: "Vettings",
                column: "VettetByEmpolyeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_PaidOrders_PaidOrderId",
                table: "Services",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vettings_Employees_VettetByEmpolyeeId",
                table: "Vettings",
                column: "VettetByEmpolyeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
