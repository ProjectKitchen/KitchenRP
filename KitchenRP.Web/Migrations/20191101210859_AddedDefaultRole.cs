using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KitchenRP.Web.Migrations
{
    public partial class AddedDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_reservations_users_owner_id",
                "reservations");

            migrationBuilder.DropForeignKey(
                "FK_reservations_resources_reserved_resource_id",
                "reservations");

            migrationBuilder.DropForeignKey(
                "FK_resources_resource_types_resource_type_id",
                "resources");

            migrationBuilder.DropForeignKey(
                "FK_restrictions_resources_restricted_resource_id",
                "restrictions");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservation_statuses_current_status_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservation_statuses_previous_status_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservations_reservation_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_users_user_roles_role_id",
                "users");

            migrationBuilder.AlterColumn<string>(
                "sub",
                "users",
                fixedLength: true,
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(8)",
                oldFixedLength: true,
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<long>(
                "role_id",
                "users",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                "email",
                "users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "role",
                "user_roles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                "reservation_id",
                "status_changes",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                "reason",
                "status_changes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                "previous_status_id",
                "status_changes",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                "current_status_id",
                "status_changes",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                "restricted_resource_id",
                "restrictions",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                "display_error",
                "restrictions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                "resource_type_id",
                "resources",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<JsonDocument>(
                "meta_data",
                "resources",
                nullable: true,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "resources",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "description",
                "resources",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "type",
                "resource_types",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "resource_types",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                "reserved_resource_id",
                "reservations",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                "owner_id",
                "reservations",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                "status",
                "reservation_statuses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "reservation_statuses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "sub",
                "refresh_tokens",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                "key",
                "refresh_tokens",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                "user_roles",
                new[] {"id", "role"},
                new object[,]
                {
                    {1L, "user"},
                    {2L, "moderator"},
                    {3L, "admin"}
                });

            migrationBuilder.AddForeignKey(
                "FK_reservations_users_owner_id",
                "reservations",
                "owner_id",
                "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_reservations_resources_reserved_resource_id",
                "reservations",
                "reserved_resource_id",
                "resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_resources_resource_types_resource_type_id",
                "resources",
                "resource_type_id",
                "resource_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_restrictions_resources_restricted_resource_id",
                "restrictions",
                "restricted_resource_id",
                "resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservation_statuses_current_status_id",
                "status_changes",
                "current_status_id",
                "reservation_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservation_statuses_previous_status_id",
                "status_changes",
                "previous_status_id",
                "reservation_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservations_reservation_id",
                "status_changes",
                "reservation_id",
                "reservations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_users_user_roles_role_id",
                "users",
                "role_id",
                "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_reservations_users_owner_id",
                "reservations");

            migrationBuilder.DropForeignKey(
                "FK_reservations_resources_reserved_resource_id",
                "reservations");

            migrationBuilder.DropForeignKey(
                "FK_resources_resource_types_resource_type_id",
                "resources");

            migrationBuilder.DropForeignKey(
                "FK_restrictions_resources_restricted_resource_id",
                "restrictions");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservation_statuses_current_status_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservation_statuses_previous_status_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_status_changes_reservations_reservation_id",
                "status_changes");

            migrationBuilder.DropForeignKey(
                "FK_users_user_roles_role_id",
                "users");

            migrationBuilder.DeleteData(
                "user_roles",
                "id",
                1L);

            migrationBuilder.DeleteData(
                "user_roles",
                "id",
                2L);

            migrationBuilder.DeleteData(
                "user_roles",
                "id",
                3L);

            migrationBuilder.AlterColumn<string>(
                "sub",
                "users",
                "character(8)",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "role_id",
                "users",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "email",
                "users",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "role",
                "user_roles",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "reservation_id",
                "status_changes",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "reason",
                "status_changes",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "previous_status_id",
                "status_changes",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "current_status_id",
                "status_changes",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "restricted_resource_id",
                "restrictions",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "display_error",
                "restrictions",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "resource_type_id",
                "resources",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<JsonDocument>(
                "meta_data",
                "resources",
                "jsonb",
                nullable: false,
                oldClrType: typeof(JsonDocument),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "resources",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "description",
                "resources",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "type",
                "resource_types",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "resource_types",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "reserved_resource_id",
                "reservations",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "owner_id",
                "reservations",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "status",
                "reservation_statuses",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "display_name",
                "reservation_statuses",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "sub",
                "refresh_tokens",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "key",
                "refresh_tokens",
                "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_reservations_users_owner_id",
                "reservations",
                "owner_id",
                "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_reservations_resources_reserved_resource_id",
                "reservations",
                "reserved_resource_id",
                "resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_resources_resource_types_resource_type_id",
                "resources",
                "resource_type_id",
                "resource_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_restrictions_resources_restricted_resource_id",
                "restrictions",
                "restricted_resource_id",
                "resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservation_statuses_current_status_id",
                "status_changes",
                "current_status_id",
                "reservation_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservation_statuses_previous_status_id",
                "status_changes",
                "previous_status_id",
                "reservation_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_status_changes_reservations_reservation_id",
                "status_changes",
                "reservation_id",
                "reservations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_users_user_roles_role_id",
                "users",
                "role_id",
                "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}