using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace wbapi.Models
{
    public partial class wbContext : DbContext
    {
        public wbContext()
        {
        }

        public wbContext(DbContextOptions<wbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Tel> Tel { get; set; }
        public virtual DbSet<Tel1> Tel1 { get; set; }
        public virtual DbSet<Tel2> Tel2 { get; set; }
        public virtual DbSet<Wb> Wb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=wb.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("Information_ID");

                entity.HasIndex(e => e.Seq)
                    .HasName("Information_SEQ");

                entity.HasIndex(e => new { e.Id, e.Seq })
                    .HasName("Information_PrimaryKey")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Seq).HasColumnName("SEQ");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("Message_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("Personal_ID");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Seq)
                    .HasName("Phone_PrimaryKey")
                    .IsUnique();

                entity.Property(e => e.Pname).HasColumnName("PName");

                entity.Property(e => e.Pno).HasColumnName("PNo");

                entity.Property(e => e.Seq).HasColumnName("SEQ");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("Photo_PrimaryKey")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("Place_PrimaryKey")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Tel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEL");

                entity.HasIndex(e => e.Id)
                    .HasName("TEL_ID1");

                entity.HasIndex(e => e.Status)
                    .HasName("TEL_ID");

                entity.HasIndex(e => new { e.Kbn1, e.Seq })
                    .HasName("TEL_PrimaryKey")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Kbn1).HasColumnName("KBN1");

                entity.Property(e => e.Kbn2).HasColumnName("KBN2");

                entity.Property(e => e.Seq).HasColumnName("SEQ");

                entity.Property(e => e.Tel1).HasColumnName("Tel");
            });

            modelBuilder.Entity<Tel1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEL1");

                entity.HasIndex(e => e.Id)
                    .HasName("TEL1_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Tel2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEL2");

                entity.HasIndex(e => e.Id)
                    .HasName("TEL2_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Wb>(entity =>
            {
                entity.HasKey(e => e.Seq);

                entity.ToTable("WB");

                entity.HasIndex(e => e.Id)
                    .HasName("WB_ID");

                entity.Property(e => e.Seq).HasColumnName("SEQ");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ip).HasColumnName("IP");

                entity.Property(e => e.Ok).HasColumnName("OK");

                entity.Property(e => e.UpdateIp).HasColumnName("UpdateIP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
