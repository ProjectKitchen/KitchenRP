using Microsoft.EntityFrameworkCore.Migrations;

namespace KitchenRP.Web.Migrations
{
    public partial class DefaultStatusValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "reservation_statuses",
                columns: new[] {"id", "display_name", "status"},
                values: new object[,]
                {
                    {1L, "Reservation pending ...", "PENDING"},
                    {2L, "Reservation needs approval ...", "NEEDS_APPROVAL"},
                    {3L, "Reservation was denied!", "DENIED"},
                    {4L, "Reservation was approved!", "APPROVED"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "reservation_statuses",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "reservation_statuses",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "reservation_statuses",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "reservation_statuses",
                keyColumn: "id",
                keyValue: 4L);
        }
    }
}
