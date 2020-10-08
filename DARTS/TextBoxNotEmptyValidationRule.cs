using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    public class TextBoxNotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is string enteredString)
            {
                if (enteredString.Length > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Enter a valid name for the player.");
        }

    }
}
