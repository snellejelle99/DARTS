using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    public class ScoreInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int scoreInput;

                try
                {
                    scoreInput = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "You must enter a number between 0 and 60");
                }
                if (scoreInput > 0 && scoreInput <= 60)
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "You must enter a number between 0 and 60");
        }
    }
}
