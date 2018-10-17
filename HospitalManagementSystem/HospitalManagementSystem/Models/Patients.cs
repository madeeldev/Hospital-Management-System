using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Patients
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public String MiddleName { get; set; }
        public string LastName { get; set; }
        public string HomeCity { get; set; }
        public String HomeAddress { get; set; }
        public String BloodGroup { get; set; }
        public int Age { get; set; }
        public long Contact { get; set; }

    }
}