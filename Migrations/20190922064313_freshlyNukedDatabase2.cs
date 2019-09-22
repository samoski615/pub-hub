using Microsoft.EntityFrameworkCore.Migrations;

namespace PubHub.Migrations
{
    public partial class freshlyNukedDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOwners_AspNetUsers_ApplicationId",
                table: "BarOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkEnthusiasts_AspNetUsers_ApplicationId",
                table: "DrinkEnthusiasts");

            migrationBuilder.DropIndex(
                name: "IX_DrinkEnthusiasts_ApplicationId",
                table: "DrinkEnthusiasts");

            migrationBuilder.DropIndex(
                name: "IX_BarOwners_ApplicationId",
                table: "BarOwners");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "DrinkEnthusiasts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DrinkEnthusiasts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "BarOwners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BarOwners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkEnthusiasts_ApplicationUserId",
                table: "DrinkEnthusiasts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BarOwners_ApplicationUserId",
                table: "BarOwners",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarOwners_AspNetUsers_ApplicationUserId",
                table: "BarOwners",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkEnthusiasts_AspNetUsers_ApplicationUserId",
                table: "DrinkEnthusiasts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOwners_AspNetUsers_ApplicationUserId",
                table: "BarOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkEnthusiasts_AspNetUsers_ApplicationUserId",
                table: "DrinkEnthusiasts");

            migrationBuilder.DropIndex(
                name: "IX_DrinkEnthusiasts_ApplicationUserId",
                table: "DrinkEnthusiasts");

            migrationBuilder.DropIndex(
                name: "IX_BarOwners_ApplicationUserId",
                table: "BarOwners");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DrinkEnthusiasts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BarOwners");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "DrinkEnthusiasts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "BarOwners",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_DrinkEnthusiasts_ApplicationId",
                table: "DrinkEnthusiasts",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_BarOwners_ApplicationId",
                table: "BarOwners",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarOwners_AspNetUsers_ApplicationId",
                table: "BarOwners",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkEnthusiasts_AspNetUsers_ApplicationId",
                table: "DrinkEnthusiasts",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
