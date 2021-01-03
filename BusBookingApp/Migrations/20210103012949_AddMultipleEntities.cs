using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBookingApp.Migrations
{
    public partial class AddMultipleEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropColumn(
                name: "CallBack_url",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "BusTickets");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "PaymentTransactions",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionStatus",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "BusTickets",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusTickets_Destinations_DestinationId",
                table: "BusTickets");

            migrationBuilder.DropIndex(
                name: "IX_BusTickets_DestinationId",
                table: "BusTickets");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "BusTickets");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PaymentTransactions",
                newName: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CallBack_url",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "BusTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Cancel_action = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
