using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EzTabs.Services.ValidationServices.CustomAttributes
{
    public class AllowedCharactersAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        // Constructor accepts a pattern to define allowed characters
        public AllowedCharactersAttribute(string allowedPattern)
        {
            _pattern = allowedPattern;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null) throw new ArgumentNullException(nameof(value));
            string? stringValue = value.ToString();
            if(stringValue is null) throw new ArgumentNullException(nameof(stringValue));

            if (Regex.IsMatch(stringValue, _pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
