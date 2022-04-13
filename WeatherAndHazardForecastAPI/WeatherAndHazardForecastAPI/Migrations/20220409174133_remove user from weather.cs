using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class removeuserfromweather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherArchives_AspNetUsers_UserId",
                table: "WeatherArchives");

            migrationBuilder.DropIndex(
                name: "IX_WeatherArchives_UserId",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WeatherArchives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WeatherArchives",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherArchives_UserId",
                table: "WeatherArchives",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherArchives_AspNetUsers_UserId",
                table: "WeatherArchives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
