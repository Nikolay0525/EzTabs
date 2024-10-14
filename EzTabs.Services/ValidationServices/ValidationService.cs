using EzTabs.Services.NavigationServices;
using System.ComponentModel.DataAnnotations;

namespace EzTabs.Services.ValidationServices
{
    public static class ValidationService
    {
        public static IList<ValidationResult> ValidateModel<T>(T model)
        {
            if(model is null) throw new ArgumentNullException(nameof(model));
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        public static Dictionary<string, List<string>> Validate(object instance)
        {
            var errors = new Dictionary<string, List<string>>();

            var context = new ValidationContext(instance);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(instance, context, results, true);

            if (!isValid)
            {
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
}
