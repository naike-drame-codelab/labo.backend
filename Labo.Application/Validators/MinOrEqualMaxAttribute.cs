using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MinOrEqualMaxAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var minEloProperty = validationContext.ObjectType.GetProperty("MinElo");
        var maxEloProperty = validationContext.ObjectType.GetProperty("MaxElo");

        if (minEloProperty == null || maxEloProperty == null)
            return new ValidationResult("MinElo or MaxElo property not found.");

        var minElo = (int?)minEloProperty.GetValue(validationContext.ObjectInstance);
        var maxElo = (int?)maxEloProperty.GetValue(validationContext.ObjectInstance);

        if (minElo.HasValue && maxElo.HasValue && minElo > maxElo)
        {
            return new ValidationResult("MinElo must be less than or equal to MaxElo.");
        }

        return ValidationResult.Success;
    }
}