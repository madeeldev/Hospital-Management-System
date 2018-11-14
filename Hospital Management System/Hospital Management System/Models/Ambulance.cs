using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class Ambulance
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AmbulanceId { get; set; }
        [Required]             
        public string AmbulanceStatus { get; set; }

        public Ambulance Ambulances { get; set; }

        public int AmbulancesId { get; set; }
    }
}