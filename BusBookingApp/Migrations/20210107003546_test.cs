using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusTickets_Destinations_DestinationId",
                table: "BusTickets");

            migrationBuilder.DropIndex(
                name: "IX_BusTickets_DestinationId",
                table: "BusTickets");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Fare",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "BusTickets");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "Buses",
                newName: "DestinationId");

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "BusTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusType",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "BusTickets");

            migrationBuilder.DropColumn(
                name: "BusType",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Buses");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Buses",
                newName: "NumberOfSeats");

            migrationBuilder.AddColumn<string>(
                name: "Fare",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "BusTickets",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "DestinationId", "Fare", "Name" },
                values: new object[,]
                {
                    { 1, "GHS 30", "Accra" },
                    { 2, "GHS 30", "Takoradi" },
                    { 3, "GHS 30", "Tema" },
                    { 4, "GHS 30", "CapeCoast" },
                    { 5, "GHS 30", "Sunyani" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusTickets_DestinationId",
                table: "BusTickets",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusTickets_Destinations_DestinationId",
                table: "BusTickets",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
