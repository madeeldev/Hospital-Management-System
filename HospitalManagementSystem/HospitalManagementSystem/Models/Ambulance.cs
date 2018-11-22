using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Ambulance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssignDriver { get; set; }
        public string Status { get; set; }
        public string NumberPlate { get; set; }
    }
}