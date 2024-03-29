﻿// <auto-generated />
using System;
using MealOrdering.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MealOrdering.Server.Data.Migrations
{
    [DbContext(typeof(MealOrderingDbContext))]
    partial class MealOrderingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("create_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid")
                        .HasColumnName("supplier_id");

                    b.HasKey("Id")
                        .HasName("pk_order_id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("create_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.HasKey("Id")
                        .HasName("pb_orderItem_id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("true");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("web_url");

                    b.HasKey("Id")
                        .HasName("pk_supplier_id");

                    b.ToTable("Suppliers", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("UUID_GENERATE_V4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("CHARACTER VARYING")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("true");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("CHARACTER VARYING")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_user_id");

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Order", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.User", "CreatedUser")
                        .WithMany("Orders")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_order_id");

                    b.HasOne("MealOrdering.Server.Data.Models.Supplier", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_order_id");

                    b.Navigation("CreatedUser");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItem", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.User", "CreatedUser")
                        .WithMany("CreatedOrderItems")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderItems_user_id");

                    b.HasOne("MealOrdering.Server.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orderItems_order_id");

                    b.Navigation("CreatedUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Supplier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.User", b =>
                {
                    b.Navigation("CreatedOrderItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
