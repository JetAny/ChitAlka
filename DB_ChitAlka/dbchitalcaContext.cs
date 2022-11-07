﻿using System;
using System.Collections.Generic;
using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB_ChitAlka
{
    public partial class dbchitalcaContext : DbContext, IDbContext
    {
        public dbchitalcaContext()
        {
        }

        public dbchitalcaContext(DbContextOptions<dbchitalcaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Capter> Capters { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userlibrary> Userlibraries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
//                You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see
//                    https://go.microsoft.com/fwlink/?linkid=2131148.
                          //For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=dbchitalca;uid=root;pwd=Tucha0425#", ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookImage).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Genre).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Capter>(entity =>
            {
                entity.ToTable("capter");

                entity.Property(e => e.CapterName)
                    .HasMaxLength(100)
                    .HasColumnName("capterName");

                entity.Property(e => e.CapterText)
                    .HasMaxLength(100)
                    .HasColumnName("capterText");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => new { e.Id, e.Password, e.FirstName, e.LastName, e.NickName, e.Role }, "user_Id_IDX");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NickName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Role).HasMaxLength(100);
            });

            modelBuilder.Entity<Userlibrary>(entity =>
            {
                entity.ToTable("userlibrary");

                entity.Property(e => e.Book)
                    .HasMaxLength(100)
                    .HasColumnName("book");

                entity.Property(e => e.CapterId)
                    .HasMaxLength(100)
                    .HasColumnName("capterId");

                entity.Property(e => e.User)
                    .HasMaxLength(100)
                    .HasColumnName("user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}