using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS.ValidationRules
{
    public class ThrowInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int throwValue;
                try
                {
                    throwValue = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "You must enter a number between 1 and 20 or 50 for a bullseye.");
                }

                if (throwValue >= 0 && throwValue <= 20 || throwValue == 50 || throwValue == 25) return ValidationResult.ValidResult;
                else return new ValidationResult(false, "You must enter a number between 1 and 20 or 50 for a bullseye.");
            }
            return new ValidationResult(false, "You must enter a number between 1 and 20 or 50 for a bullseye.");
        }
    }
}
