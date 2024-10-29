using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    public class MySQLTodoDbContext : DbContext
    {
        public MySQLTodoDbContext(DbContextOptions<MySQLTodoDbContext> options)
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

                // Configure ID to use GUIDs stored as CHAR(36)
                entity.Property(t => t.Id)
                      .HasColumnType("char(36)")
                      .HasDefaultValueSql("UUID()");  // Default value for MySQL

                entity.Property(t => t.Title)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
