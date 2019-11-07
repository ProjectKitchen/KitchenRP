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
                name: "reservation_statuses",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(nullable: false),
                    display_name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_reservation_statuses", x => x.id); });

            migrationBuilder.CreateTable(
                name: "resource_types",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(nullable: false),
                    display_name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_resource_types", x => x.id); });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_user_roles", x => x.id); });

            migrationBuilder.CreateTable(
                name: "resources",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_name = table.Column<string>(nullable: false),
                    meta_data = table.Column<JsonDocument>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    resource_type_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resources", x => x.id);
                    table.ForeignKey(
                        name: "FK_resources_resource_types_resource_type_id",
                        column: x => x.resource_type_id,
                        principalTable: "resource_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sub = table.Column<string>(fixedLength: true, maxLength: 8, nullable: false),
                    role_id = table.Column<long>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    allow_notifications = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_user_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "restrictions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    restrict_from = table.Column<Instant>(nullable: false),
                    restrict_to = table.Column<Instant>(nullable: false),
                    ignore_year = table.Column<bool>(nullable: false),
                    restricted_resource_id = table.Column<long>(nullable: false),
                    display_error = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restrictions", x => x.id);
                    table.ForeignKey(
                        name: "FK_restrictions_resources_restricted_resource_id",
                        column: x => x.restricted_resource_id,
                        principalTable: "resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_time = table.Column<Instant>(nullable: false),
                    end_time = table.Column<Instant>(nullable: false),
                    owner_id = table.Column<long>(nullable: false),
                    reserved_resource_id = table.Column<long>(nullable: false),
                    allow_notifications = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_resources_reserved_resource_id",
                        column: x => x.reserved_resource_id,
                        principalTable: "resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "restriction_data",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    max_usage_per_month_in_hours = table.Column<int>(nullable: true),
                    max_usage_per_week_in_count = table.Column<int>(nullable: true),
                    restriction_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restriction_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_restriction_data_restrictions_restriction_id",
                        column: x => x.restriction_id,
                        principalTable: "restrictions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status_changes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    previous_status_id = table.Column<long>(nullable: false),
                    current_status_id = table.Column<long>(nullable: false),
                    reason = table.Column<string>(nullable: false),
                    changed_at = table.Column<Instant>(nullable: false),
                    reservation_id = table.Column<long>(nullable: false),
                    change_by_user_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_changes", x => x.id);
                    table.ForeignKey(
                        name: "FK_status_changes_users_change_by_user_id",
                        column: x => x.change_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_status_changes_reservation_statuses_current_status_id",
                        column: x => x.current_status_id,
                        principalTable: "reservation_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_status_changes_reservation_statuses_previous_status_id",
                        column: x => x.previous_status_id,
                        principalTable: "reservation_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_status_changes_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_owner_id",
                table: "reservations",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_reserved_resource_id",
                table: "reservations",
                column: "reserved_resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_resources_resource_type_id",
                table: "resources",
                column: "resource_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_restriction_data_restriction_id",
                table: "restriction_data",
                column: "restriction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_restrictions_restricted_resource_id",
                table: "restrictions",
                column: "restricted_resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_changes_change_by_user_id",
                table: "status_changes",
                column: "change_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_changes_current_status_id",
                table: "status_changes",
                column: "current_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_changes_previous_status_id",
                table: "status_changes",
                column: "previous_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_changes_reservation_id",
                table: "status_changes",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restriction_data");

            migrationBuilder.DropTable(
                name: "status_changes");

            migrationBuilder.DropTable(
                name: "restrictions");

            migrationBuilder.DropTable(
                name: "reservation_statuses");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "resources");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "resource_types");
        }
    }
}