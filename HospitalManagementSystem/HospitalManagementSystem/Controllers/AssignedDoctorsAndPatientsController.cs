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
    public class AssignedDoctorsAndPatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignedDoctorsAndPatients
        public ActionResult Index()
        {
            var assignedDoctorsAndPatients = db.AssignedDoctorsAndPatients.Include(a => a.Doctors).Include(a => a.Patients);
            return View(assignedDoctorsAndPatients.ToList());
        }

        // GET: AssignedDoctorsAndPatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedDoctorsAndPatients assignedDoctorsAndPatients = db.AssignedDoctorsAndPatients.Find(id);
            if (assignedDoctorsAndPatients == null)
            {
                return HttpNotFound();
            }
            return View(assignedDoctorsAndPatients);
        }

        // GET: AssignedDoctorsAndPatients/Create
        public ActionResult Create()
        {
            ViewBag.DoctorsId = new SelectList(db.Doctors, "Id", "FirstName");
            ViewBag.PatientsId = new SelectList(db.Patients, "Id", "FirstName");
            return View();
        }

        // POST: AssignedDoctorsAndPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientsId,DoctorsId")] AssignedDoctorsAndPatients assignedDoctorsAndPatients)
        {
            if (ModelState.IsValid)
            {
                db.AssignedDoctorsAndPatients.Add(assignedDoctorsAndPatients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorsId = new SelectList(db.Doctors, "Id", "FirstName", assignedDoctorsAndPatients.DoctorsId);
            ViewBag.PatientsId = new SelectList(db.Patients, "Id", "FirstName", assignedDoctorsAndPatients.PatientsId);
            return View(assignedDoctorsAndPatients);
        }

        // GET: AssignedDoctorsAndPatients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedDoctorsAndPatients assignedDoctorsAndPatients = db.AssignedDoctorsAndPatients.Find(id);
            if (assignedDoctorsAndPatients == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorsId = new SelectList(db.Doctors, "Id", "FirstName", assignedDoctorsAndPatients.DoctorsId);
            ViewBag.PatientsId = new SelectList(db.Patients, "Id", "FirstName", assignedDoctorsAndPatients.PatientsId);
            return View(assignedDoctorsAndPatients);
        }

        // POST: AssignedDoctorsAndPatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientsId,DoctorsId")] AssignedDoctorsAndPatients assignedDoctorsAndPatients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedDoctorsAndPatients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorsId = new SelectList(db.Doctors, "Id", "FirstName", assignedDoctorsAndPatients.DoctorsId);
            ViewBag.PatientsId = new SelectList(db.Patients, "Id", "FirstName", assignedDoctorsAndPatients.PatientsId);
            return View(assignedDoctorsAndPatients);
        }

        // GET: AssignedDoctorsAndPatients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedDoctorsAndPatients assignedDoctorsAndPatients = db.AssignedDoctorsAndPatients.Find(id);
            if (assignedDoctorsAndPatients == null)
            {
                return HttpNotFound();
            }
            return View(assignedDoctorsAndPatients);
        }

        // POST: AssignedDoctorsAndPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedDoctorsAndPatients assignedDoctorsAndPatients = db.AssignedDoctorsAndPatients.Find(id);
            db.AssignedDoctorsAndPatients.Remove(assignedDoctorsAndPatients);
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
