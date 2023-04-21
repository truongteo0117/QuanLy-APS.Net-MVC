using QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLSinhVien.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<SinhVien> SinhVienRepository { get; }
        IGenericRepository<LopHoc> LopHocRepository { get; }
        IGenericRepository<Permission> PermissionRepository { get; }
        IGenericRepository<ThongBao> ThongBaoRepository { get; }
        void Commit();
        void Rollback();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool disposed = false;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            SinhVienRepository = new GenericRepository<SinhVien>(context);
            LopHocRepository = new GenericRepository<LopHoc>(context);
            PermissionRepository = new GenericRepository<Permission>(context);
            ThongBaoRepository = new GenericRepository<ThongBao>(context);
        }

        public IGenericRepository<SinhVien> SinhVienRepository { get; private set; }
        public IGenericRepository<LopHoc> LopHocRepository { get; private set; }
        public IGenericRepository<Permission> PermissionRepository { get; private set; }
        public IGenericRepository<ThongBao> ThongBaoRepository { get; private set; }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList()
                .ForEach(e => e.Reload());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

    }
}