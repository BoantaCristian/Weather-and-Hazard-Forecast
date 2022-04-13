using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAndHazardForecastAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HazardArchives");

            migrationBuilder.DropTable(
                name: "HazardTypes");

            migrationBuilder.RenameColumn(
                name: "Wind",
                table: "WeatherArchives",
                newName: "Wind_Direction_Degrees");

            migrationBuilder.RenameColumn(
                name: "WeatherType",
                table: "WeatherArchives",
                newName: "Wind_Direction_Full");

            migrationBuilder.RenameColumn(
                name: "Humidity",
                table: "WeatherArchives",
                newName: "Relative_Humidity");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Locations",
                newName: "Timezone");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Locations",
                newName: "State_code");

            migrationBuilder.AlterColumn<long>(
                name: "Sunset",
                table: "WeatherArchives",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Sunrise",
                table: "WeatherArchives",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Clouds",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WeatherArchives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "WeatherArchives",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precipitations",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Probability_Precipitation",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Snow",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Snow_Depth",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Valid_date",
                table: "WeatherArchives",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Visibility",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Wind_Direction",
                table: "WeatherArchives",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Wind_Gust_Speed",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Wind_Speed",
                table: "WeatherArchives",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "City_name",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_code",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Earthquakes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date = table.Column<string>(nullable: true),
                    time = table.Column<string>(nullable: true),
                    time_ago = table.Column<string>(nullable: true),
                    latitude = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true),
                    depth = table.Column<string>(nullable: true),
                    magnitude = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    timezone = table.Column<string>(nullable: true),
                    region = table.Column<string>(nullable: true),
                    nearest_city = table.Column<string>(nullable: true),
                    effects = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Earthquakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SevereWeathers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Regions = table.Column<string>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    Local_Start_Date = table.Column<string>(nullable: true),
                    Local_End_Date = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SevereWeathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SevereWeathers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SevereWeathers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SevereWeathers_LocationId",
                table: "SevereWeathers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SevereWeathers_UserId",
                table: "SevereWeathers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Earthquakes");

            migrationBuilder.DropTable(
                name: "SevereWeathers");

            migrationBuilder.DropColumn(
                name: "Clouds",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Precipitations",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Probability_Precipitation",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Snow",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Snow_Depth",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Valid_date",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Wind_Direction",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Wind_Gust_Speed",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "Wind_Speed",
                table: "WeatherArchives");

            migrationBuilder.DropColumn(
                name: "City_name",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Country_code",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Wind_Direction_Full",
                table: "WeatherArchives",
                newName: "WeatherType");

            migrationBuilder.RenameColumn(
                name: "Wind_Direction_Degrees",
                table: "WeatherArchives",
                newName: "Wind");

            migrationBuilder.RenameColumn(
                name: "Relative_Humidity",
                table: "WeatherArchives",
                newName: "Humidity");

            migrationBuilder.RenameColumn(
                name: "Timezone",
                table: "Locations",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "State_code",
                table: "Locations",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Sunset",
                table: "WeatherArchives",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "Sunrise",
                table: "WeatherArchives",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "HazardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HazardArchives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HazardTypeId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HazardArchives_HazardTypes_HazardTypeId",
                        column: x => x.HazardTypeId,
                        principalTable: "HazardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HazardArchives_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HazardArchives_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HazardArchives_HazardTypeId",
                table: "HazardArchives",
                column: "HazardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HazardArchives_LocationId",
                table: "HazardArchives",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HazardArchives_UserId",
                table: "HazardArchives",
                column: "UserId");
        }
    }
}
