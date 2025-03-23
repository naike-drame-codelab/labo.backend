namespace Labo.Application.Exceptions
{
    public class DuplicatePropertyException(string propertyName)
        : Exception($"The property {propertyName} must be unique")
    {
    }
}
