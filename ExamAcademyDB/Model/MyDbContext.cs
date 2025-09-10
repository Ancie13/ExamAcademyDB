using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamAcademyDB
{
    class MyDbContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Lectures> Lectures { get; set; }
        public DbSet<Curators> Curators { get; set; }

        public DbSet<GroupsStudents> GroupStudents { get; set; }
        public DbSet<GroupsLectures> GroupLectures { get; set; }
        public DbSet<GroupsCurators> GroupCurators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subjects>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Departments>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Faculties>()
                .HasIndex(f => f.Name)
                .IsUnique();

            modelBuilder.Entity<Groups>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<GroupsCurators>()
                .HasIndex(gc => new { gc.GroupId, gc.CuratorId })
                .IsUnique();

            modelBuilder.Entity<GroupsLectures>()
                .HasIndex(gl => new { gl.GroupId, gl.LectureId })
                .IsUnique();

            modelBuilder.Entity<GroupsStudents>()
                .HasIndex(gs => new { gs.GroupId, gs.StudentId })
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ExamAcademyDB;Trusted_Connection=True;");
        }
    }
}
