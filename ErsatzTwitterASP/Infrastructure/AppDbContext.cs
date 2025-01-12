using Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DbTweet> Tweets { get; set; }
    public virtual DbSet<DbUser> Users { get; set; }
    public virtual DbSet<DbLike> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbTweet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tweets__3213E83FF71C8956");

            entity.ToTable("tweets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(140)
                .IsUnicode(false)
                .HasColumnName("content");

            entity.HasOne(d => d.DbUser).WithMany(p => p.Tweets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tweets__userId__3A81B327");
        });

        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FE7AC260A");

            entity.ToTable("users");

            entity.HasIndex(e => e.Pseudo, "UQ__users__EA0EEA22721D003D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pseudo");
        });

        modelBuilder.Entity<DbLike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TweetId }).HasName("PK__likes__312B3378950B6244");

            entity.ToTable("likes");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.TweetId).HasColumnName("tweetId");

            entity.HasOne(d => d.DbTweet).WithMany(p => p.Likes)
                .HasForeignKey(d => d.TweetId)
                .HasConstraintName("FK__likes__tweetId__3E52440B");

            entity.HasOne(d => d.DbUser).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__likes__userId__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
