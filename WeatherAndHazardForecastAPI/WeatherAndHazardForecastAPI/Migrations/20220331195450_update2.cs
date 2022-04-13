using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UVIndex",
                table: "WeatherArchives");

            migrationBuilder.AddColumn<double>(
                name: "UV_Index",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UV_Index",
                table: "WeatherArchives");

            migrationBuilder.AddColumn<string>(
                name: "UVIndex",
                table: "WeatherArchives",
                nullable: true);
        }
    }
}
