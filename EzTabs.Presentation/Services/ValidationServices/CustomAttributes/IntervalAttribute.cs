using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Presentation.Services.ValidationServices.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class IntervalAttribute : ValidationAttribute
    {
        public double Minimum { get; }
        public double Maximum { get; }

        public IntervalAttribute(double minimum, double maximum)
        {
            Minimum = minimum;
            Maximum = maximum;

            if (Minimum > Maximum)
            {
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");
            }
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            double numericValue;

            try
            {
                numericValue = Convert.ToDouble(value);
            }
            catch
            {
                return false;
            }

            return numericValue >= Minimum && numericValue <= Maximum;
        }
    }
}
