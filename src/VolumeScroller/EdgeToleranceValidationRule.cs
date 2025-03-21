using System.Globalization;
using System.Windows.Controls;

namespace VolumeScroller
{
    public class EdgeToleranceValidationRule : ValidationRule
    {
        const int MIN = 0;
        const int MAX = 1000;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string stringValue)
            {
                // Check if the value can be parsed as an integer
                if (!int.TryParse(stringValue, out int intValue))
                {
                    return new ValidationResult(false, "Please enter a valid number");
                }

                // Check if the value is within the valid range
                if (intValue < MIN || intValue > MAX)
                {
                    return new ValidationResult(false, $"Value must be between {MIN} and {MAX}");
                }

                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Invalid input");
        }
    }
}