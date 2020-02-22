using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlindDating.Models
{
    public partial class BlindDatingContext : DbContext
    {
        public BlindDatingContext()
        {
        }

        public BlindDatingContext(DbContextOptions<BlindDatingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatingProfile> DatingProfile { get; set; }
        public virtual DbSet<MailMessage> MailMessage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=BlindDating;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatingProfile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasColumnName("bio")
                    .HasColumnType("text");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("displayName")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoPath)
                    .HasColumnName("photoPath")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MailMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FromProfileId).HasColumnName("fromProfileID");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasColumnName("messageText")
                    .HasColumnType("text");

                entity.Property(e => e.MessageTitle)
                    .IsRequired()
                    .HasColumnName("messageTitle")
                    .IsUnicode(false);

                entity.Property(e => e.ToProfileId).HasColumnName("toProfileID");

                entity.HasOne(d => d.FromProfile)
                    .WithMany(p => p.MailMessageFromProfile)
                    .HasForeignKey(d => d.FromProfileId)
                    .HasConstraintName("FK__MailMessa__fromP__02FC7413");

                entity.HasOne(d => d.ToProfile)
                    .WithMany(p => p.MailMessageToProfile)
                    .HasForeignKey(d => d.ToProfileId)
                    .HasConstraintName("FK__MailMessa__toPro__03F0984C");
            });
        }
    }
}
