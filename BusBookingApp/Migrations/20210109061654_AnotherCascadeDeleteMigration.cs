using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class AnotherCascadeDeleteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
