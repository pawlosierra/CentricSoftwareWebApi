using System;
using CentricSoftwareWebApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CentricSoftwareWebApi.Infrastructure.Data
{
    public partial class productsContext : DbContext
    {
        public productsContext()
        {
        }

        public productsContext(DbContextOptions<productsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClothingModel> ClothingModels { get; set; }
        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<TagsModel> TagsModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9LJ95CE; Database=products; Trusted_Connection=True; User=centric;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ClothingModel>(entity =>
            {
                entity.ToTable("clothingModel");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("brand");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("productModel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdClothing).HasColumnName("id_clothing");

                entity.Property(e => e.IdTags).HasColumnName("id_tags");

                entity.HasOne(d => d.IdClothingNavigation)
                    .WithMany(p => p.ProductModels)
                    .HasForeignKey(d => d.IdClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_productModel_clothingModel");

                entity.HasOne(d => d.IdTagsNavigation)
                    .WithMany(p => p.ProductModels)
                    .HasForeignKey(d => d.IdTags)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_productModel_tagsModel");
            });

            modelBuilder.Entity<TagsModel>(entity =>
            {
                entity.ToTable("tagsModel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
