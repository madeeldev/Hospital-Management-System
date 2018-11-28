using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Hospital_Management_System.CollectionViewModels;
using Hospital_Management_System.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        private ApplicationDbContext db;
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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //Add Prescription
        [Authorize]
        public ActionResult AddPrescription()
        {
            var collection = new PrescriptionCollection
            {
                Prescription = new Prescription(),
                Patients = db.Patients.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrescription(PrescriptionCollection model)
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.Id == model.Prescription.PatientId);
            var doctor = db.Doctors.Single(c => c.ApplicationUserId == user);
            var schedule = db.Schedules.Single(c => c.DoctorId == doctor.Id);
            var patientuser = db.Users.Single(c => c.Id == patient.ApplicationUserId);
            var prescription = new Prescription
            {
                PatientId = model.Prescription.PatientId,
                DoctorId = doctor.Id,
                DoctorName = doctor.FullName,
                DoctorSpecialization = doctor.Specialization,
                PatientName = patient.FullName,
                PatientGender = patient.Gender,
                UserName = patientuser.UserName,
                MedicalTest1 = model.Prescription.MedicalTest1,
                MedicalTest2 = model.Prescription.MedicalTest2,
                MedicalTest3 = model.Prescription.MedicalTest3,
                MedicalTest4 = model.Prescription.MedicalTest4,
                Medicine1 = model.Prescription.Medicine1,
                Medicine2 = model.Prescription.Medicine2,
                Medicine3 = model.Prescription.Medicine3,
                Medicine4 = model.Prescription.Medicine4,
                Medicine5 = model.Prescription.Medicine5,
                Medicine6 = model.Prescription.Medicine6,
                Medicine7 = model.Prescription.Medicine7,
                Morning1 = model.Prescription.Morning1,
                Morning2 = model.Prescription.Morning2,
                Morning3 = model.Prescription.Morning3,
                Morning4 = model.Prescription.Morning4,
                Morning5 = model.Prescription.Morning5,
                Morning6 = model.Prescription.Morning6,
                Morning7 = model.Prescription.Morning7,
                Afternoon1 = model.Prescription.Afternoon1,
                Afternoon2 = model.Prescription.Afternoon2,
                Afternoon3 = model.Prescription.Afternoon3,
                Afternoon4 = model.Prescription.Afternoon4,
                Afternoon5 = model.Prescription.Afternoon5,
                Afternoon6 = model.Prescription.Afternoon6,
                Afternoon7 = model.Prescription.Afternoon7,
                Evening1 = model.Prescription.Evening1,
                Evening2 = model.Prescription.Evening2,
                Evening3 = model.Prescription.Evening3,
                Evening4 = model.Prescription.Evening4,
                Evening5 = model.Prescription.Evening5,
                Evening6 = model.Prescription.Evening6,
                Evening7 = model.Prescription.Evening7,
                CheckUpAfterDays = model.Prescription.CheckUpAfterDays,
                DoctorTiming = "From " + schedule.AvailableStartTime.ToShortTimeString() + " to " + schedule.AvailableEndTime.ToShortTimeString()
            };

            db.Prescription.Add(prescription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ListOfPrescription()
        {
            var prescription = db.Prescription.ToList();
            return View(prescription);
        }

        [Authorize]
        public ActionResult ViewPrescription(int id)
        {
            var prescription = db.Prescription.Single(c => c.PatientId == id);
            return View(prescription);
        }
    }
}