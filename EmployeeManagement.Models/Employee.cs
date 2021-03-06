﻿using EmployeeManagement.Models.CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage ="First name must be provided and be at least 2 characters")]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must be provided and be at least 2 characters")]
        [MinLength(2)]
        public string LastName { get; set; }
        
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain ="pragimtech.com", ErrorMessage ="Domain must be pragimtech.com")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }

        [ValidateComplexType]
        public Department Department { get; set; } // nav property - different table
    }
}
