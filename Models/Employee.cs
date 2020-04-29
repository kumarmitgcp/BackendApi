using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendApi.Models
{
    public partial class Employee
    {
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [StringLength(25)]
        public string Email { get; set; }
        public int? Phone { get; set; }
        [StringLength(15)]
        public string City { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        public int EmployeeId { get; set; }
    }
}
