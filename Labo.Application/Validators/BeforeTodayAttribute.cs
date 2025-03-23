using System.ComponentModel.DataAnnotations;

namespace Labo.Application.Validators
{
    public class BeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is null) {  return false; }

            DateTime date = (DateTime)value;

            ErrorMessage = "The date must be lower than today";

            return date < DateTime.Now;
        }
    }
}
