using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class UpdateBusAndBusTicketModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "BusTickets");

            migrationBuilder.DropColumn(
                name: "PickupPoint",
                table: "BusTickets");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BusTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "Buses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PickupPoint",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BusTickets");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "PickupPoint",
                table: "Buses");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "BusTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PickupPoint",
                table: "BusTickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
