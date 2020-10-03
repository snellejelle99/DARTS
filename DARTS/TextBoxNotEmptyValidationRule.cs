using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    public class TextBoxNotEmptyValidationRule : ValidationRule
    {
        // Checks if textboxes are not empty
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            // If value is string enteredString
            if (value is string enteredString)
            {
                // If a string entered
                if (enteredString.Length > 0)
                    return ValidationResult.ValidResult;
            }
            // If textbox is empty, display error message
            return new ValidationResult(false, "Enter a valid name for the player.");
        }

    }
}
