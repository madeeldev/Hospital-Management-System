using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Constructor
        public DoctorController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Doctor
        [Authorize(Roles = RoleName.Doctor + "," + RoleName.Admin)]
        public ActionResult Index()
        {
            //var doctors = db.Doctors.Include(d => d.Departments);
            return View();//doctors.ToList()
        }

        // Get View of Patients List
        public ActionResult ViewPatientsList()
        {
            var model = db.Patients.ToList();
            return View(model);
        }

        // Add Prescription
        public ActionResult AddPrescription(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrescription(int id, Prescriptions model)
        {
            var prescription = new Prescriptions
            {
                Id = model.Id,
                Prescription = model.Prescription,
                PatientsId = db.Patients.Single(c => c.Id == id).Id
            };
            db.Prescriptions.Add(prescription);
            db.SaveChanges();
            return RedirectToAction("PrescriptionsList", "Doctor");
        }

        // Prescription List
        public ActionResult PrescriptionsList()
        {
            var prescriptions = db.Prescriptions.ToList();
            return View(prescriptions);
        }

        // Edit Prescription

        public ActionResult EditPrescription(int id)
        {
            var model = db.Prescriptions.SingleOrDefault(c => c.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrescription(int id, Prescriptions model)
        {
            var prescription = db.Prescriptions.Single(c => c.Id == id);
            prescription.Prescription = model.Prescription;
            db.SaveChanges();
            return RedirectToAction("PrescriptionsList");
        }

        // Delete Prescription

        public ActionResult DeletePrescription(int? id)
        {
            var prescription = db.Prescriptions.Single(c => c.Id == id);
            return View(prescription);
        }

        [HttpPost, ActionName("DeletePrescription")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePrescription(int id)
        {
            var prescription = db.Prescriptions.Single(c => c.Id == id);
            db.Prescriptions.Remove(prescription);
            db.SaveChanges();
            return RedirectToAction("PrescriptionsList");
        }
    }
}