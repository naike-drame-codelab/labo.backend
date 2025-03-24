using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.Application.Validators
{
    public class GreaterThanTodayAndMinPlayersAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime endOfRegistrationDate)
            {
                return new ValidationResult("Invalid date format.");
            }

            object? instance = validationContext.ObjectInstance;
            if (instance == null)
            {
                return new ValidationResult("Validation context is invalid.");
            }

            if (instance.GetType().GetProperty("MinPlayers")?.GetValue(instance) is not int minPlayers)
            {
                return new ValidationResult("Invalid MinPlayers value.");
            }

            DateTime currentDate = DateTime.UtcNow;
            DateTime requiredEndDate = currentDate.AddDays(minPlayers);

            if (endOfRegistrationDate <= requiredEndDate)
            {
                return new ValidationResult($"EndOfRegistrationDate must be greater than {requiredEndDate:yyyy-MM-dd}.");
            }

            return ValidationResult.Success;
        }
    }
}
