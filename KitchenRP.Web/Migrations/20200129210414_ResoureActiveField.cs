using Microsoft.EntityFrameworkCore.Migrations;

namespace KitchenRP.Web.Migrations
{
    public partial class ResoureActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "resources",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "resources");
        }
    }
}
