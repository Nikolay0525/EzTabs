using System.ComponentModel.DataAnnotations;

namespace EzTabs.Presentation.Services.ValidationServices;

public static class ValidationService
{
    public static Dictionary<string, List<string>> ValidateProperties(object instance, IEnumerable<string>? SpecificMembers = null)
    {
        var errors = new Dictionary<string, List<string>>();

        var results = new List<ValidationResult>();

        if (SpecificMembers != null && SpecificMembers.Any())
        {
            foreach (var propertyName in SpecificMembers)
            {
                var context = new ValidationContext(instance) { MemberName = propertyName };
                var isValid = Validator.TryValidateProperty(instance.GetType().GetProperty(propertyName)?.GetValue(instance), context, results);

                if (!isValid)
                {
                    foreach (var validationResult in results)
                    {
                        if (!errors.TryGetValue(propertyName, out List<string>? value))
                        {
                            value = new List<string>();
                            errors[propertyName] = value;
                        }
                        value.Add(validationResult.ErrorMessage);
                    }
                }
                results.Clear();
            }
        }
        else
        {
            var context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, results, true);

            foreach (var validationResult in results)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    if (!errors.TryGetValue(memberName, out List<string>? value))
                    {
                        value = new List<string>();
                        errors[memberName] = value;
                    }
                    value.Add(validationResult.ErrorMessage);
                }
            }
        }

        return errors;
    }
}
