﻿// <auto-generated />
using System;
using System.Text.Json;
using KitchenRP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KitchenRP.Web.Migrations
{
    [DbContext(typeof(KitchenRpContext))]
    partial class KitchenRpContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("KitchenRP.DataAccess.Models.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Instant>("Expires")
                        .HasColumnName("expires")
                        .HasColumnType("timestamp");

                    b.Property<string>("Key")
                        .HasColumnName("key")
                        .HasColumnType("text");

                    b.Property<string>("Sub")
                        .HasColumnName("sub")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("refresh_tokens");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Reservation", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AllowNotifications")
                        .HasColumnName("allow_notifications")
                        .HasColumnType("boolean");

                    b.Property<Instant>("EndTime")
                        .HasColumnName("end_time")
                        .HasColumnType("timestamp");

                    b.Property<long?>("OwnerId")
                        .HasColumnName("owner_id")
                        .HasColumnType("bigint");

                    b.Property<long?>("ReservedResourceId")
                        .HasColumnName("reserved_resource_id")
                        .HasColumnType("bigint");

                    b.Property<Instant>("StartTime")
                        .HasColumnName("start_time")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ReservedResourceId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.ReservationStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("reservation_statuses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DisplayName = "Reservation pending ...",
                            Status = "PENDING"
                        },
                        new
                        {
                            Id = 2L,
                            DisplayName = "Reservation needs approval ...",
                            Status = "NEEDS_APPROVAL"
                        },
                        new
                        {
                            Id = 3L,
                            DisplayName = "Reservation was denied!",
                            Status = "DENIED"
                        },
                        new
                        {
                            Id = 4L,
                            DisplayName = "Reservation was approved!",
                            Status = "APPROVED"
                        });
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<JsonDocument>("MetaData")
                        .HasColumnName("meta_data")
                        .HasColumnType("jsonb");

                    b.Property<long?>("ResourceTypeId")
                        .HasColumnName("resource_type_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResourceTypeId");

                    b.ToTable("resources");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.ResourceType", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnName("type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("resource_types");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Restriction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DisplayError")
                        .HasColumnName("display_error")
                        .HasColumnType("text");

                    b.Property<bool>("IgnoreYear")
                        .HasColumnName("ignore_year")
                        .HasColumnType("boolean");

                    b.Property<Instant>("RestrictFrom")
                        .HasColumnName("restrict_from")
                        .HasColumnType("timestamp");

                    b.Property<Instant>("RestrictTo")
                        .HasColumnName("restrict_to")
                        .HasColumnType("timestamp");

                    b.Property<long?>("RestrictedResourceId")
                        .HasColumnName("restricted_resource_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RestrictedResourceId");

                    b.ToTable("restrictions");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.RestrictionData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MaxUsagePerMonthInHours")
                        .HasColumnName("max_usage_per_month_in_hours")
                        .HasColumnType("integer");

                    b.Property<int?>("MaxUsagePerWeekInCount")
                        .HasColumnName("max_usage_per_week_in_count")
                        .HasColumnType("integer");

                    b.Property<long>("RestrictionId")
                        .HasColumnName("restriction_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RestrictionId")
                        .IsUnique();

                    b.ToTable("restriction_data");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.StatusChange", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("ChangeByUserId")
                        .HasColumnName("change_by_user_id")
                        .HasColumnType("bigint");

                    b.Property<Instant>("ChangedAt")
                        .HasColumnName("changed_at")
                        .HasColumnType("timestamp");

                    b.Property<long?>("CurrentStatusId")
                        .HasColumnName("current_status_id")
                        .HasColumnType("bigint");

                    b.Property<long?>("PreviousStatusID")
                        .HasColumnName("previous_status_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Reason")
                        .HasColumnName("reason")
                        .HasColumnType("text");

                    b.Property<long?>("ReservationId")
                        .HasColumnName("reservation_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChangeByUserId");

                    b.HasIndex("CurrentStatusId");

                    b.HasIndex("PreviousStatusID");

                    b.HasIndex("ReservationId");

                    b.ToTable("status_changes");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.User", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AllowNotifications")
                        .HasColumnName("allow_notifications")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<long?>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Sub")
                        .HasColumnName("sub")
                        .HasColumnType("character(8)")
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoleName")
                        .HasColumnName("role_name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RoleName = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            RoleName = "moderator"
                        },
                        new
                        {
                            Id = 3L,
                            RoleName = "user"
                        });
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Reservation", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("KitchenRP.DataAccess.Models.Resource", "ReservedResource")
                        .WithMany()
                        .HasForeignKey("ReservedResourceId");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Resource", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.ResourceType", "ResourceType")
                        .WithMany()
                        .HasForeignKey("ResourceTypeId");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.Restriction", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.Resource", "RestrictedResource")
                        .WithMany()
                        .HasForeignKey("RestrictedResourceId");
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.RestrictionData", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.Restriction", null)
                        .WithOne("Data")
                        .HasForeignKey("KitchenRP.DataAccess.Models.RestrictionData", "RestrictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.StatusChange", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.User", "ChangedBy")
                        .WithMany()
                        .HasForeignKey("ChangeByUserId");

                    b.HasOne("KitchenRP.DataAccess.Models.ReservationStatus", "CurrentStatus")
                        .WithMany()
                        .HasForeignKey("CurrentStatusId");

                    b.HasOne("KitchenRP.DataAccess.Models.ReservationStatus", "PreviousStatus")
                        .WithMany()
                        .HasForeignKey("PreviousStatusID");

                    b.HasOne("KitchenRP.DataAccess.Models.Reservation", "Reservation")
                        .WithMany("StatusChanges")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KitchenRP.DataAccess.Models.User", b =>
                {
                    b.HasOne("KitchenRP.DataAccess.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
