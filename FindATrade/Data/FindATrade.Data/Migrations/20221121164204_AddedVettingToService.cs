using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindATrade.Data.Migrations
{
    public partial class AddedVettingToService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VettingId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_VettingId",
                table: "Services",
                column: "VettingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services",
                column: "VettingId",
                principalTable: "Vettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vettings_VettingId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VettingId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "VettingId",
                table: "Services");
        }
    }
}
