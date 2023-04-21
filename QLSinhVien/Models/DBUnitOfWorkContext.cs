using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLSinhVien.Models
{
    public class DBUnitOfWorkContext: DbContext
    {
        public DBUnitOfWorkContext()
            : base("name=DBSinhVienConnectionString")
        {
        }

        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
    }
}