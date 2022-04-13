using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class covid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timezone",
                table: "Earthquakes",
                newName: "Timezone");

            migrationBuilder.RenameColumn(
                name: "time_ago",
                table: "Earthquakes",
                newName: "Time_ago");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Earthquakes",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "Earthquakes",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "nearest_city",
                table: "Earthquakes",
                newName: "Nearest_city");

            migrationBuilder.RenameColumn(
                name: "magnitude",
                table: "Earthquakes",
                newName: "Magnitude");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Earthquakes",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Earthquakes",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Earthquakes",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "effects",
                table: "Earthquakes",
                newName: "Effects");

            migrationBuilder.RenameColumn(
                name: "depth",
                table: "Earthquakes",
                newName: "Depth");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Earthquakes",
                newName: "Date");

            migrationBuilder.CreateTable(
                name: "CovidArchive",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active_Cases_text = table.Column<string>(nullable: true),
                    Country_text = table.Column<string>(nullable: true),
                    Last_Update = table.Column<string>(nullable: true),
                    New_Cases_text = table.Column<string>(nullable: true),
                    New_Deaths_text = table.Column<string>(nullable: true),
                    Total_Cases_text = table.Column<string>(nullable: true),
                    Total_Deaths_text = table.Column<string>(nullable: true),
                    Total_Recovered_text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidArchive", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CovidArchive");

            migrationBuilder.RenameColumn(
                name: "Timezone",
                table: "Earthquakes",
                newName: "timezone");

            migrationBuilder.RenameColumn(
                name: "Time_ago",
                table: "Earthquakes",
                newName: "time_ago");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Earthquakes",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Earthquakes",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "Nearest_city",
                table: "Earthquakes",
                newName: "nearest_city");

            migrationBuilder.RenameColumn(
                name: "Magnitude",
                table: "Earthquakes",
                newName: "magnitude");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Earthquakes",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Earthquakes",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Earthquakes",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Effects",
                table: "Earthquakes",
                newName: "effects");

            migrationBuilder.RenameColumn(
                name: "Depth",
                table: "Earthquakes",
                newName: "depth");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Earthquakes",
                newName: "date");
        }
    }
}
