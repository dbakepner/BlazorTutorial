using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        // incoming object is email address
        public string AllowedDomain { get; set; }

        // override IsValid().  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] strings = value.ToString().Split('@');
                if (strings.Length > 1 && strings[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null; //Return null indicates no errors occurred.
                }
                //return new ValidationResult($"Domain must be {AllowedDomain}", 
                // put error message into the Employee.cs class instead.
                return new ValidationResult(ErrorMessage,
                    new[] { validationContext.MemberName });
            }
            return null;
        }
    }
}
