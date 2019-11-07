using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KitchenRP.Web.Migrations
{
    public partial class AddedRefreshTokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "refresh_tokens",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(),
                    sub = table.Column<string>(),
                    expires = table.Column<Instant>()
                },
                constraints: table => { table.PrimaryKey("PK_refresh_tokens", x => x.id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "refresh_tokens");
        }
    }
}