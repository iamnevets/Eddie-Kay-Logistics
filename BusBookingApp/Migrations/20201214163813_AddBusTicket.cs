using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class AddBusTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationId);
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "DestinationId", "Name" },
                values: new object[,]
                {
                    { 1, "Accra" },
                    { 2, "Takoradi" },
                    { 3, "Tema" },
                    { 4, "CapeCoast" },
                    { 5, "Sunyani" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusTickets_TicketNumber",
                table: "BusTickets",
                column: "TicketNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusTickets");

            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
