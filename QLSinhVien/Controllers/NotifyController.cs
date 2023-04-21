using QLSinhVien.Models;
using QLSinhVien.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSinhVien.Controllers
{
    public class NotifyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // GET: Notify
        public NotifyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            DateTime now = DateTime.Now;
            DateTime before30Days = now.AddDays(-30);
            var notifyData = _unitOfWork.ThongBaoRepository.GetAll();
            var dataFilter = notifyData.Where(tb => tb.CreateTime >= before30Days)
            .ToList();
            return View(dataFilter != null ? dataFilter : new List<ThongBao>());
        }
        public void AddData(string tenSinhVien, string action)
        {
            try
            {
                string messenger = $"Sinh viên {tenSinhVien} đã được {action}";
                var newThongBao = new ThongBao
                {
                    Messenger = messenger,
                    CreateTime = DateTime.Now
                };

                _unitOfWork.ThongBaoRepository.Add(newThongBao);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
            }

        }
        public ActionResult RemoveNotify(int id)
        {
            try
            {
                var data = _unitOfWork.ThongBaoRepository.FindByID(id);
                _unitOfWork.ThongBaoRepository.Delete(data);
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
            }
            return RedirectToAction("Index");
        }
        // GET: Notify/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notify/Create
        //public ActionResult Create()
        //{

        //    return View();
        //}

        // POST: Notify/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notify/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notify/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notify/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notify/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
