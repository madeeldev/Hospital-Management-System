using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class MedicineRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MedicineRecords
        public ActionResult Index()
        {
            return View(db.MedicineRecords.ToList());
        }

        // GET: MedicineRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineRecord medicineRecord = db.MedicineRecords.Find(id);
            if (medicineRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicineRecord);
        }

        // GET: MedicineRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicineRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity")] MedicineRecord medicineRecord)
        {
            if (ModelState.IsValid)
            {
                db.MedicineRecords.Add(medicineRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicineRecord);
        }

        // GET: MedicineRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineRecord medicineRecord = db.MedicineRecords.Find(id);
            if (medicineRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicineRecord);
        }

        // POST: MedicineRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity")] MedicineRecord medicineRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicineRecord);
        }

        // GET: MedicineRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineRecord medicineRecord = db.MedicineRecords.Find(id);
            if (medicineRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicineRecord);
        }

        // POST: MedicineRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicineRecord medicineRecord = db.MedicineRecords.Find(id);
            db.MedicineRecords.Remove(medicineRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
