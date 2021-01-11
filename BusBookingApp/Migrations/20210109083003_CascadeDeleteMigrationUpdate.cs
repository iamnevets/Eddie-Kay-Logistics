using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class CascadeDeleteMigrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "BusTickets");

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

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Buses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses",
                column: "DestinationId",
                unique: true,
                filter: "[DestinationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Destinations_DestinationId",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_Buses_DestinationId",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "BusTickets");

            migrationBuilder.DropColumn(
                name: "PickupPoint",
                table: "BusTickets");

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "BusTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
