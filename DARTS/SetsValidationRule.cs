using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    // Class voor het valideren van de set textbox (werkt goed)
    class SetsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int set;
                try
                {
                    set = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "You must enter a number between 1 and 99.");
                }

                if (set > 0)
                    return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "You must enter a number between 1 and 99.");
        }
    }
}
