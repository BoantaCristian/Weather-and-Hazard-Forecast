using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country_Name",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country_Name",
                table: "Locations");
        }
    }
}
