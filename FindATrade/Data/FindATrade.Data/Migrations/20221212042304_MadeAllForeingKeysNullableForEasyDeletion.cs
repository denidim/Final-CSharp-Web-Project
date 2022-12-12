using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindATrade.Data.Migrations
{
    public partial class MadeAllForeingKeysNullableForEasyDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTimes_Employees_EmployeeId",
                table: "EmployeeTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Companies_CompanyId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_PaidOrderPackageTypes_PaidOrders_PaidOrderId",
                table: "PaidOrderPackageTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Companies_CompanyId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_PaidOrderPackageTypes_PaidOrderId",
                table: "PaidOrderPackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTimes_EmployeeId",
                table: "EmployeeTimes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaidOrderId",
                table: "PaidOrderPackageTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PaidOrderPackageTypes_PaidOrderId",
                table: "PaidOrderPackageTypes",
                column: "PaidOrderId",
                unique: true,
                filter: "[PaidOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTimes_EmployeeId",
                table: "EmployeeTimes",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTimes_Employees_EmployeeId",
                table: "EmployeeTimes",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Companies_CompanyId",
                table: "Likes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidOrderPackageTypes_PaidOrders_PaidOrderId",
                table: "PaidOrderPackageTypes",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Companies_CompanyId",
                table: "Skills",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTimes_Employees_EmployeeId",
                table: "EmployeeTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Companies_CompanyId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_PaidOrderPackageTypes_PaidOrders_PaidOrderId",
                table: "PaidOrderPackageTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Companies_CompanyId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_PaidOrderPackageTypes_PaidOrderId",
                table: "PaidOrderPackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTimes_EmployeeId",
                table: "EmployeeTimes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaidOrderId",
                table: "PaidOrderPackageTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaidOrderPackageTypes_PaidOrderId",
                table: "PaidOrderPackageTypes",
                column: "PaidOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTimes_EmployeeId",
                table: "EmployeeTimes",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTimes_Employees_EmployeeId",
                table: "EmployeeTimes",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Companies_CompanyId",
                table: "Likes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaidOrderPackageTypes_PaidOrders_PaidOrderId",
                table: "PaidOrderPackageTypes",
                column: "PaidOrderId",
                principalTable: "PaidOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Companies_CompanyId",
                table: "Skills",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
