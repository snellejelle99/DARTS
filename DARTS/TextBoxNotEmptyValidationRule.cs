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
            // If value is string str
            if (value is string str)
            {
                // If a string entered
                if (str.Length > 0)
                    return ValidationResult.ValidResult;
            }
            // If textbox is empty, display error message
            return new ValidationResult(false, Message);
        }

        // Message object for object binding
        public String Message { get; set; }
    }
}
