using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLSinhVien.Models
{
    public partial class DBSinhVienContext : DbContext
    {
        public DBSinhVienContext()
            : base("name=DBSinhVienConnectionString")
        {
        }

        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LopHoc>()
                .Property(e => e.TenLop)
                .IsFixedLength();

            modelBuilder.Entity<LopHoc>()
                .HasMany(e => e.SinhViens)
                .WithRequired(e => e.LopHoc)
                .HasForeignKey(e => e.IdLopHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.SinhViens)
                .WithRequired(e => e.Permission)
                .HasForeignKey(e => e.IdPermission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.HoTen)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.KhoaHoc)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<ThongBao>()
                .HasMany(e => e.SinhViens)
                .WithOptional(e => e.ThongBao)
                .HasForeignKey(e => e.IdThongBao);
        }
    }
}
