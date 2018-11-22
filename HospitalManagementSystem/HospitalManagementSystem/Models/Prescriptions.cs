using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Prescriptions
    {
        public int Id { get; set; }
        public string Prescription { get; set; }
        public Patients Patients { get; set; }
        public int PatientsId { get; set; }
    }
}