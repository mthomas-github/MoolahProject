using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    public class SQLTodoDbContext : DbContext
    {
        public SQLTodoDbContext(DbContextOptions<SQLTodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                // Set ID as the primary key
                entity.HasKey(t => t.Id);

                // Configure ID to use GUIDs stored as UNIQUEIDENTIFIER for SQL Server
                entity.Property(t => t.Id)
                      .HasColumnType("uniqueidentifier")
                      .HasDefaultValueSql("NEWID()");  // Default GUID generation in SQL Server

                entity.Property(t => t.Title)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
