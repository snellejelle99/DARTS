using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    class LegsValidationRule : ValidationRule
    {
        // Method for checking if legs textbox is empty
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            // If value is not null
            if (value != null)
            {
                int leg;
                // Try to convert value to an int
                try
                {
                    leg = Convert.ToInt32(value);
                }
                // If a different type is entered, throw this error message 
                catch
                {
                    return new ValidationResult(false, "You must enter a number between 1 and 99.");
                }
                // If entered value is higher than zero, validate result
                if (leg > 0)
                    return ValidationResult.ValidResult;

            }
            // If nothing is entered, display error message
            return new ValidationResult(false, "You must enter a number between 1 and 99.");
        }
    }
}
