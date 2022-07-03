using MealOrdering.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.Context
{
    public class MealOrderingDbContext : DbContext
    {
        public MealOrderingDbContext(DbContextOptions<MealOrderingDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "public");
                entity.HasKey(x => x.Id).HasName("pk_user_id");

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid")
                    .HasDefaultValueSql("UUID_GENERATE_V4()")
                    .IsRequired();

                entity.Property(x => x.FirstName).HasColumnName("first_name").HasColumnType("CHARACTER VARYING")
                    .HasMaxLength(100);
                entity.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("CHARACTER VARYING")
                    .HasMaxLength(100);
                entity.Property(x => x.EmailAddress).HasColumnName("email_address").HasColumnType("character varying")
                    .HasMaxLength(100);
                entity.Property(i => i.Password).HasColumnName("password").HasColumnType("character varying").HasMaxLength(250);

                entity.Property(x => x.CreateDate).HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("NOW()");

                entity.Property(x => x.IsActive).HasColumnName("isactive").HasColumnType("boolean")
                    .HasDefaultValueSql("true");

            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("pk_supplier_id");
                entity.ToTable("Suppliers", "public");

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()").IsRequired();

                entity.Property(x => x.IsActive).HasColumnName("isactive").HasColumnType("boolean")
                    .HasDefaultValueSql("true");
                entity.Property(x => x.Name).HasColumnName("name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(x => x.CreateDate).HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()")
                    .ValueGeneratedNever();
                entity.Property(x => x.WebUrl).HasColumnName("web_url").HasColumnType("character varying")
                    .HasMaxLength(100);
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("pk_order_id");
                entity.ToTable("Orders", "public");

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()");
                entity.Property(x => x.Name).HasColumnName("name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(x => x.Description).HasColumnName("description").HasColumnType("character varying")
                    .HasMaxLength(1000);
                entity.Property(x => x.CreateDate).HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedNever();

                entity.Property(x => x.CreateUserId).HasColumnName("create_user_id").HasColumnType("uuid");
                entity.Property(x => x.SupplierId).HasColumnName("supplier_id").HasColumnType("uuid").IsRequired()
                    .ValueGeneratedNever();
                entity.Property(x => x.ExpireDate).HasColumnName("expire_date")
                    .HasColumnType("timestamp without time zone").IsRequired();

                entity.HasOne(x => x.CreatedUser)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.CreateUserId)
                    .HasConstraintName("fk_user_order_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Supplier)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.SupplierId)
                    .HasConstraintName("fk_supplier_order_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("pb_orderItem_id");
                entity.ToTable("OrderItems", "public");
                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid")
                    .HasDefaultValueSql("public.uuid_generate_v4()");
                entity.Property(x => x.Description).HasColumnName("description").HasColumnType("character varying")
                    .HasMaxLength(1000);
                entity.Property(x => x.CreateDate).HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()");
                entity.Property(x => x.CreateUserId).HasColumnName("create_user_id").HasColumnType("uuid");
                entity.Property(x => x.OrderId).HasColumnName("order_id").HasColumnType("uuid");


                entity.HasOne(x => x.Order)
                    .WithMany(x => x.OrderItems)
                    .HasForeignKey(x => x.OrderId)
                    .HasConstraintName("fk_orderItems_order_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.CreatedUser)
                    .WithMany(x => x.CreatedOrderItems)
                    .HasForeignKey(x => x.CreateUserId)
                    .HasConstraintName("fk_orderItems_user_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
