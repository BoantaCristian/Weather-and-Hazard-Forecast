﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherAndHazardForecastAPI.Models.ApplicationContext;

namespace WeatherAndHazardForecastAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.CovidArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Active_Cases_text");

                    b.Property<string>("Country_text");

                    b.Property<string>("Last_Update");

                    b.Property<string>("New_Cases_text");

                    b.Property<string>("New_Deaths_text");

                    b.Property<string>("Total_Cases_text");

                    b.Property<string>("Total_Deaths_text");

                    b.Property<string>("Total_Recovered_text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CovidArchive");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.Earthquake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date");

                    b.Property<string>("Depth");

                    b.Property<string>("Effects");

                    b.Property<string>("Latitude");

                    b.Property<string>("Location");

                    b.Property<string>("Longitude");

                    b.Property<string>("Magnitude");

                    b.Property<string>("Nearest_city");

                    b.Property<string>("Region");

                    b.Property<string>("Status");

                    b.Property<string>("Time");

                    b.Property<string>("Time_ago");

                    b.Property<string>("Timezone");

                    b.HasKey("Id");

                    b.ToTable("Earthquakes");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City_name");

                    b.Property<string>("Country_Name");

                    b.Property<string>("Country_code");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("State_code");

                    b.Property<string>("Timezone");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.SevereWeatherArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Local_End_Date");

                    b.Property<string>("Local_Start_Date");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Regions");

                    b.Property<string>("Severity");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("SevereWeathers");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.UserLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LocationId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLocations");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.WeatherArchive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Clouds");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<int?>("LocationId");

                    b.Property<double>("MaxTemperature");

                    b.Property<double>("MinTemperature");

                    b.Property<double>("Precipitations");

                    b.Property<double>("Pressure");

                    b.Property<int>("Probability_Precipitation");

                    b.Property<int>("Relative_Humidity");

                    b.Property<double>("Snow");

                    b.Property<double>("Snow_Depth");

                    b.Property<long>("Sunrise");

                    b.Property<long>("Sunset");

                    b.Property<double>("Temperature");

                    b.Property<double>("UV_Index");

                    b.Property<string>("Valid_date");

                    b.Property<double>("Visibility");

                    b.Property<string>("Wind_Direction");

                    b.Property<int>("Wind_Direction_Degrees");

                    b.Property<string>("Wind_Direction_Full");

                    b.Property<double>("Wind_Gust_Speed");

                    b.Property<double>("Wind_Speed");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("WeatherArchives");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.CovidArchive", b =>
                {
                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.User", "User")
                        .WithMany("CovidArchives")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.SevereWeatherArchive", b =>
                {
                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.Location", "Location")
                        .WithMany("SevereWeathers")
                        .HasForeignKey("LocationId");

                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.User", "User")
                        .WithMany("SevereWeathers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.UserLocation", b =>
                {
                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.Location", "Location")
                        .WithMany("UserLocations")
                        .HasForeignKey("LocationId");

                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.User", "User")
                        .WithMany("UserLocations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WeatherAndHazardForecastAPI.Models.DbModels.WeatherArchive", b =>
                {
                    b.HasOne("WeatherAndHazardForecastAPI.Models.DbModels.Location", "Location")
                        .WithMany("WeatherArchives")
                        .HasForeignKey("LocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
