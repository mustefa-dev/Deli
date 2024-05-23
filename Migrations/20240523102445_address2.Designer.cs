﻿// <auto-generated />
using System;
using Deli.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Deli.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240523102445_address2")]
    partial class address2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Deli.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("FullAddress")
                        .HasColumnType("text");

                    b.Property<Guid?>("GovernorateId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<double?>("Latidute")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Addresss");
                });

            modelBuilder.Entity("Deli.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int?>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Deli.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("Deli.Entities.Governorate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("DeliveryPrice")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("Deli.Entities.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("GovernorateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Inventorys");
                });

            modelBuilder.Entity("Deli.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("InventoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("MainDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.Property<int?>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string[]>("imaages")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Deli.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("FromUser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Messages")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Deli.Entities.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("NotifyFor")
                        .HasColumnType("text");

                    b.Property<Guid>("NotifyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Deli.Entities.Address", b =>
                {
                    b.HasOne("Deli.Entities.AppUser", null)
                        .WithMany("Addresses")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Deli.Entities.Governorate", "Governorate")
                        .WithMany()
                        .HasForeignKey("GovernorateId");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("Deli.Entities.Inventory", b =>
                {
                    b.HasOne("Deli.Entities.Governorate", "Governorate")
                        .WithMany("Inventories")
                        .HasForeignKey("GovernorateId");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("Deli.Entities.Item", b =>
                {
                    b.HasOne("Deli.Entities.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Deli.Entities.Inventory", "Inventory")
                        .WithMany("Items")
                        .HasForeignKey("InventoryId");

                    b.Navigation("Category");

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("Deli.Entities.AppUser", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Deli.Entities.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Deli.Entities.Governorate", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("Deli.Entities.Inventory", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
