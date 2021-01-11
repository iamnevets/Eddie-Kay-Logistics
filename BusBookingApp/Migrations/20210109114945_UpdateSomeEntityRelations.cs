using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class UpdateSomeEntityRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusTickets_BusId",
                table: "BusTickets");

            migrationBuilder.DropIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses");

            migrationBuilder.CreateIndex(
                name: "IX_BusTickets_BusId",
                table: "BusTickets",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses",
                column: "DestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusTickets_BusId",
                table: "BusTickets");

            migrationBuilder.DropIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses");

            migrationBuilder.CreateIndex(
                name: "IX_BusTickets_BusId",
                table: "BusTickets",
                column: "BusId",
                unique: true,
                filter: "[BusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses",
                column: "DestinationId",
                unique: true,
                filter: "[DestinationId] IS NOT NULL");
        }
    }
}
