using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindATrade.Data.Migrations
{
    public partial class VettedByEmployeeMadeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings");

            migrationBuilder.AlterColumn<int>(
                name: "VettetByEmployeeId",
                table: "Vettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings",
                column: "VettetByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings");

            migrationBuilder.AlterColumn<int>(
                name: "VettetByEmployeeId",
                table: "Vettings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vettings_Employees_VettetByEmployeeId",
                table: "Vettings",
                column: "VettetByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
