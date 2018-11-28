using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Hospital_Management_System.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
        public string DoctorSpecialization { get; set; }

        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }

        public string UserName { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAge { get; set; }

        [Display(Name = "Medical Tests")]
        public string MedicalTest1 { get; set; }
        public string MedicalTest2 { get; set; }
        public string MedicalTest3 { get; set; }
        public string MedicalTest4 { get; set; }

        [Display(Name = "Medicine")]
        public string Medicine1 { get; set; }
        [Display(Name = " ")]
        public string Morning1 { get; set; }
        public string Afternoon1 { get; set; }
        public string Evening1 { get; set; }

        public string Medicine2 { get; set; }
        public string Morning2 { get; set; }
        public string Afternoon2 { get; set; }
        public string Evening2 { get; set; }

        public string Medicine3 { get; set; }
        public string Morning3 { get; set; }
        public string Afternoon3 { get; set; }
        public string Evening3 { get; set; }

        public string Medicine4 { get; set; }
        public string Morning4 { get; set; }
        public string Afternoon4 { get; set; }
        public string Evening4 { get; set; }

        public string Medicine5 { get; set; }
        public string Morning5 { get; set; }
        public string Afternoon5 { get; set; }
        public string Evening5 { get; set; }

        public string Medicine6 { get; set; }
        public string Morning6 { get; set; }
        public string Afternoon6 { get; set; }
        public string Evening6 { get; set; }

        public string Medicine7 { get; set; }
        public string Morning7 { get; set; }
        public string Afternoon7 { get; set; }
        public string Evening7 { get; set; }

        [Display(Name = "Checkup After Days")]
        public int CheckUpAfterDays { get; set; }
        public string DoctorTiming { get; set; }

    }
}