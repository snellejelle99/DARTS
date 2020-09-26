using DARTS.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    // Class voor het valideren van de leg textbox (hoort te werken, maar werkt niet doordat leg object niet goed wordt aangeroepen in XAML file)
    class LegsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int leg;
                try
                {
                    leg = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "You must enter a number between 1 and 99.");
                }

                if (leg > 0)
                    return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "You must enter a number between 1 and 99.");
        }
    }
}
