using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Database.Models;

namespace MyCompany.MyProject.Database
{
    public partial class MyDbContext : DbContext
    {
        //Scaffold-DbContext -Connection "Server=serviceIp;User Id=id;Password=password;Database=database" -Provider "Pomelo.EntityFrameworkCore.MySql" -OutputDir "modelname"
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasColumnName("class_name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.StudentId)
                    .HasColumnName("student_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasColumnName("student_name")
                    .HasColumnType("varchar(255)");
            });
        }
    }
}
