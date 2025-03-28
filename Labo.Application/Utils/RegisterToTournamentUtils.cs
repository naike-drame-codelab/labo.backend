using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Enums;

namespace Labo.Application.Utils
{
    public static class RegisterToTournamentUtils
    {
        public static int CalculateAge(DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;
            if (referenceDate < birthDate.AddYears(age)) age--;
            return age;
        }

        public static bool IsEligible(int age, Category[] categories)
        {
            foreach (Category category in categories)
            {
                switch (category)
                {
                    case Category.Junior:
                        if (age <= 18) return true;
                        break;
                    case Category.Senior:
                        if (age > 18 && age <= 40) return true;
                        break;
                    case Category.Veteran:
                        if (age > 40) return true;
                        break;
                }
            }
            return false;
        }
    }
}
