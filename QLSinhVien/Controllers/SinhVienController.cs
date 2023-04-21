using PagedList;
using PagedList.Mvc;
using QLSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using QLSinhVien.Repositories;
using static Unity.Storage.RegistrationSet;

namespace QLSinhVien.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SinhVienController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        enum Action
        {
            Created,
            Updated,
            Deleted
        }
        // GET: SinhVien
        public ActionResult Index(int? page)
        {

            var listSinhVien = _unitOfWork.SinhVienRepository.GetAll();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var records = listSinhVien.OrderBy(sv => sv.id);
            var pagedRecords = records.ToPagedList(pageNumber, pageSize);

            return View(pagedRecords);
        }

        // GET: SinhVien/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SinhVien/Create
        public ActionResult Create()
        {
            var lophoc = _unitOfWork.LopHocRepository.GetAll();

            ViewBag.IdLopHoc = new SelectList(lophoc, "Id", "TenLop");
            return View();
        }

        // POST: SinhVien/Create
        [HttpPost]
        public ActionResult Create(SinhVien model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    //var sinhVienAdding = new DBSinhVienContext();
                    //sinhVienAdding.SinhViens.Add(model);
                    //sinhVienAdding.SaveChanges();

                    //Using UnitOfWork

                    _unitOfWork.SinhVienRepository.Add(model);
                    _unitOfWork.Commit();

                    //add data to Notify table
                    addNotify(model.HoTen, Action.Created.ToString());
                    return RedirectToAction("Index");
                }
                catch
                {
                    _unitOfWork.Rollback();
                    return View(model);
                }
            }
            return View(model);
        }

        // GET: SinhVien/Edit/5
        public ActionResult Edit(int id)
        {
            //var context = new DBSinhVienContext();
            //var editData = context.SinhViens.Find(id);

            var editData = _unitOfWork.SinhVienRepository.FindByID(id);

            editData.HoTen = editData.HoTen.Trim();
            editData.Email = editData.Email.Trim();
            editData.Password = editData.Password.Trim();
            editData.KhoaHoc = editData.KhoaHoc.Trim();
            var lopHoc = _unitOfWork.LopHocRepository.GetAll();
            ViewBag.IdLopHoc = new SelectList(lopHoc, "Id", "TenLop", editData.IdLopHoc);

            return View(editData);
        }

        // POST: SinhVien/Edit/5
        [HttpPost]
        public ActionResult Edit(SinhVien model)
        {
            try
            {
                // TODO: Add update logic here
                //var context = new DBSinhVienContext();
                //var currentData = context.SinhViens.Find(model.id);
                var currentData = _unitOfWork.SinhVienRepository.FindByID(model.id);
                currentData.HoTen = model.HoTen.Trim();
                currentData.NamSinh = model.NamSinh;
                currentData.KhoaHoc = model.KhoaHoc.Trim();
                currentData.Email = model.Email.Trim();
                currentData.Password = model.Password.Trim();
                currentData.IdLopHoc = model.IdLopHoc;
                _unitOfWork.Commit();

                addNotify(model.HoTen, Action.Updated.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View(model);
            }
        }

        // GET: SinhVien/Delete/5
        public ActionResult Delete(int id)
        {
            //var context = new DBSinhVienContext();
            var deleteData = _unitOfWork.SinhVienRepository.FindByID(id);
            return View(deleteData);
        }

        // POST: SinhVien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                // TODO: Add delete logic here
                //var context = new DBSinhVienContext();
                var deleteData = _unitOfWork.SinhVienRepository.FindByID(id);
                _unitOfWork.SinhVienRepository.Delete(deleteData);
                
                _unitOfWork.Commit();

                addNotify(deleteData.HoTen, Action.Deleted.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                _unitOfWork.Rollback();
                return View();
            }
        }

        public void addNotify(string name, string action)
        {
            var notify = new NotifyController(_unitOfWork);
            notify.AddData(name, action);
        }
        #region  Extended function
        public void SendEmail(string to, string subject, string body)
        {

            string from = "";
            string password = "";


            MailMessage mail = new MailMessage(from, to, subject, body);


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(from, password);

            smtpClient.Send(mail);
        }
        #endregion
    }
}
