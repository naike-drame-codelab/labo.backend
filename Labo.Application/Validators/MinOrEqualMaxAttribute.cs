using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.Application.Validators
{
    public class MinOrEqualMaxAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) { return false; }

            int eloValue = (int)value;

            ErrorMessage = "The ELO must be between 0 (included) and 3000 (included).";

            return eloValue >= 0 && eloValue <= 3000;
        }
    }
}
