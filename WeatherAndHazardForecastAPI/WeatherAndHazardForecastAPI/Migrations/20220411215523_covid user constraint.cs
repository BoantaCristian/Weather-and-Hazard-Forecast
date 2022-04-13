using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class coviduserconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CovidArchive",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CovidArchive_UserId",
                table: "CovidArchive",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CovidArchive_AspNetUsers_UserId",
                table: "CovidArchive",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CovidArchive_AspNetUsers_UserId",
                table: "CovidArchive");

            migrationBuilder.DropIndex(
                name: "IX_CovidArchive_UserId",
                table: "CovidArchive");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CovidArchive");
        }
    }
}
