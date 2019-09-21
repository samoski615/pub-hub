using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PubHub.Data.Migrations
{
    public partial class addedDbSetToApplicationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BarOwners",
                columns: table => new
                {
                    BarOwnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HappyHourSpecialsId = table.Column<int>(nullable: false),
                    BarName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<int>(nullable: false),
                    TypeOfBar = table.Column<string>(nullable: true),
                    AverageRating = table.Column<int>(nullable: false),
                    BarOpen = table.Column<string>(nullable: true),
                    BarClose = table.Column<string>(nullable: true),
                    HappyHourStartTime = table.Column<string>(nullable: true),
                    HappyHourEndTime = table.Column<string>(nullable: true),
                    PotentialCustomers = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarOwners", x => x.BarOwnerId);
                    table.ForeignKey(
                        name: "FK_BarOwners_AspNetUsers_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrinkEnthusiasts",
                columns: table => new
                {
                    DrinkEnthusiastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    CheckInStatus = table.Column<bool>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkEnthusiasts", x => x.DrinkEnthusiastId);
                    table.ForeignKey(
                        name: "FK_DrinkEnthusiasts_AspNetUsers_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HappyHourSpecials",
                columns: table => new
                {
                    HappyHourSpecialsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    TypeOfDrink = table.Column<string>(nullable: true),
                    DrinkPrice = table.Column<double>(nullable: false),
                    HappyHourStartTime = table.Column<string>(nullable: true),
                    HappyHourEndTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappyHourSpecials", x => x.HappyHourSpecialsId);
                });

            migrationBuilder.CreateTable(
                name: "RatingsTables",
                columns: table => new
                {
                    RatingsTableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BarOwnerId = table.Column<int>(nullable: false),
                    DrinkEnthusiastId = table.Column<int>(nullable: false),
                    CustomerRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsTables", x => x.RatingsTableId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarOwners_ApplicationId",
                table: "BarOwners",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkEnthusiasts_ApplicationId",
                table: "DrinkEnthusiasts",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarOwners");

            migrationBuilder.DropTable(
                name: "DrinkEnthusiasts");

            migrationBuilder.DropTable(
                name: "HappyHourSpecials");

            migrationBuilder.DropTable(
                name: "RatingsTables");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
        }
    }
}
