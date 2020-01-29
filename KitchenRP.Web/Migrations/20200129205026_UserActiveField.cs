using Microsoft.EntityFrameworkCore.Migrations;

namespace KitchenRP.Web.Migrations
{
    public partial class UserActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_status_changes_reservations_reservation_id",
                table: "status_changes");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_status_changes_reservations_reservation_id",
                table: "status_changes",
                column: "reservation_id",
                principalTable: "reservations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_status_changes_reservations_reservation_id",
                table: "status_changes");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_status_changes_reservations_reservation_id",
                table: "status_changes",
                column: "reservation_id",
                principalTable: "reservations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
