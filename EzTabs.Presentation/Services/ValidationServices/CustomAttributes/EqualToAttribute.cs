using System.ComponentModel.DataAnnotations;

namespace EzTabs.Presentation.Services.ValidationServices.CustomAttributes;

public class EqualToAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public EqualToAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var comparisonValue = validationContext.ObjectType
            .GetProperty(_comparisonProperty)
            ?.GetValue(validationContext.ObjectInstance);

        if (value == null && comparisonValue == null)
        {
            return ValidationResult.Success;
        }

        if (value == null || comparisonValue == null || !value.Equals(comparisonValue))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
