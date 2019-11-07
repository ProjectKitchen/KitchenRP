using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KitchenRP.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "reservation_statuses",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(),
                    display_name = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_reservation_statuses", x => x.id); });

            migrationBuilder.CreateTable(
                "resource_types",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(),
                    display_name = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_resource_types", x => x.id); });

            migrationBuilder.CreateTable(
                "user_roles",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_user_roles", x => x.id); });

            migrationBuilder.CreateTable(
                "resources",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_name = table.Column<string>(),
                    meta_data = table.Column<JsonDocument>(),
                    description = table.Column<string>(),
                    resource_type_id = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resources", x => x.id);
                    table.ForeignKey(
                        "FK_resources_resource_types_resource_type_id",
                        x => x.resource_type_id,
                        "resource_types",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "users",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sub = table.Column<string>(fixedLength: true, maxLength: 8),
                    role_id = table.Column<long>(),
                    email = table.Column<string>(),
                    allow_notifications = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        "FK_users_user_roles_role_id",
                        x => x.role_id,
                        "user_roles",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "restrictions",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    restrict_from = table.Column<Instant>(),
                    restrict_to = table.Column<Instant>(),
                    ignore_year = table.Column<bool>(),
                    restricted_resource_id = table.Column<long>(),
                    display_error = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restrictions", x => x.id);
                    table.ForeignKey(
                        "FK_restrictions_resources_restricted_resource_id",
                        x => x.restricted_resource_id,
                        "resources",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "reservations",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_time = table.Column<Instant>(),
                    end_time = table.Column<Instant>(),
                    owner_id = table.Column<long>(),
                    reserved_resource_id = table.Column<long>(),
                    allow_notifications = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        "FK_reservations_users_owner_id",
                        x => x.owner_id,
                        "users",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_reservations_resources_reserved_resource_id",
                        x => x.reserved_resource_id,
                        "resources",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "restriction_data",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    max_usage_per_month_in_hours = table.Column<int>(nullable: true),
                    max_usage_per_week_in_count = table.Column<int>(nullable: true),
                    restriction_id = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restriction_data", x => x.id);
                    table.ForeignKey(
                        "FK_restriction_data_restrictions_restriction_id",
                        x => x.restriction_id,
                        "restrictions",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "status_changes",
                table => new
                {
                    id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    previous_status_id = table.Column<long>(),
                    current_status_id = table.Column<long>(),
                    reason = table.Column<string>(),
                    changed_at = table.Column<Instant>(),
                    reservation_id = table.Column<long>(),
                    change_by_user_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_changes", x => x.id);
                    table.ForeignKey(
                        "FK_status_changes_users_change_by_user_id",
                        x => x.change_by_user_id,
                        "users",
                        "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_status_changes_reservation_statuses_current_status_id",
                        x => x.current_status_id,
                        "reservation_statuses",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_status_changes_reservation_statuses_previous_status_id",
                        x => x.previous_status_id,
                        "reservation_statuses",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_status_changes_reservations_reservation_id",
                        x => x.reservation_id,
                        "reservations",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_reservations_owner_id",
                "reservations",
                "owner_id");

            migrationBuilder.CreateIndex(
                "IX_reservations_reserved_resource_id",
                "reservations",
                "reserved_resource_id");

            migrationBuilder.CreateIndex(
                "IX_resources_resource_type_id",
                "resources",
                "resource_type_id");

            migrationBuilder.CreateIndex(
                "IX_restriction_data_restriction_id",
                "restriction_data",
                "restriction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_restrictions_restricted_resource_id",
                "restrictions",
                "restricted_resource_id");

            migrationBuilder.CreateIndex(
                "IX_status_changes_change_by_user_id",
                "status_changes",
                "change_by_user_id");

            migrationBuilder.CreateIndex(
                "IX_status_changes_current_status_id",
                "status_changes",
                "current_status_id");

            migrationBuilder.CreateIndex(
                "IX_status_changes_previous_status_id",
                "status_changes",
                "previous_status_id");

            migrationBuilder.CreateIndex(
                "IX_status_changes_reservation_id",
                "status_changes",
                "reservation_id");

            migrationBuilder.CreateIndex(
                "IX_users_role_id",
                "users",
                "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "restriction_data");

            migrationBuilder.DropTable(
                "status_changes");

            migrationBuilder.DropTable(
                "restrictions");

            migrationBuilder.DropTable(
                "reservation_statuses");

            migrationBuilder.DropTable(
                "reservations");

            migrationBuilder.DropTable(
                "users");

            migrationBuilder.DropTable(
                "resources");

            migrationBuilder.DropTable(
                "user_roles");

            migrationBuilder.DropTable(
                "resource_types");
        }
    }
}