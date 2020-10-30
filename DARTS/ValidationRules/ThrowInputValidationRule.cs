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
                    return new ValidationResult(false, "Between 1-20, 25 for bull or 50 for bulseye");
                }

                if (throwValue >= 0 && throwValue <= 20 || throwValue == 50 || throwValue == 25) return ValidationResult.ValidResult;
                else return new ValidationResult(false, "Between 1-20, 25 for bull or 50 for bullseye");
            }
            return new ValidationResult(false, "Between 1-20, 25 for bull or 50 for bulseye");
        }
    }
}
