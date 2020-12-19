using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class AddDestination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BusTickets",
                newName: "BusTicketId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BusTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BusTickets");

            migrationBuilder.RenameColumn(
                name: "BusTicketId",
                table: "BusTickets",
                newName: "Id");
        }
    }
}
