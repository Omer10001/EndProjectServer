using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EndProjectServerBL.Models
{
    public partial class EndProjectDBContext : DbContext
    {
        public EndProjectDBContext()
        {
        }

        public EndProjectDBContext(DbContextOptions<EndProjectDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LikesInComment> LikesInComments { get; set; }
        public virtual DbSet<LikesInPost> LikesInPosts { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagsInPost> TagsInPosts { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=EndProjectDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.RepliedToId).HasColumnName("RepliedToID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_postid_foreign");

                entity.HasOne(d => d.RepliedTo)
                    .WithMany(p => p.InverseRepliedTo)
                    .HasForeignKey(d => d.RepliedToId)
                    .HasConstraintName("comment_repliedtoid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_userid_foreign");
            });

            modelBuilder.Entity<LikesInComment>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CommentId })
                    .HasName("likesincomment_userid_primary");

                entity.ToTable("LikesInComment");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");
            });

            modelBuilder.Entity<LikesInPost>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PostId })
                    .HasName("likesinpost_userid_primary");

                entity.ToTable("LikesInPost");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.PostId).HasColumnName("PostID");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_topicid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_userid_foreign");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_topicid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_userid_foreign");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TagsInPost>(entity =>
            {
                entity.ToTable("TagsInPost");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.TagsInPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tagsinpost_postid_foreign");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagsInPosts)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tagsinpost_tagid_foreign");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AboutText)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
