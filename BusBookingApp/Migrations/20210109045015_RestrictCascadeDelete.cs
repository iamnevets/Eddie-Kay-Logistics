using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class RestrictCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusTickets_Buses_BusId",
                table: "BusTickets");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses",
                column: "DestinationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusTickets_Buses_BusId",
                table: "BusTickets",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusTickets_Buses_BusId",
                table: "BusTickets");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusTickets_Buses_BusId",
                table: "BusTickets",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
