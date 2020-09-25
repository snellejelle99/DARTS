using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace DARTS
{
    // Checkt of de player textboxes gevuld zijn, zo niet > displayt error message
    public class TextBoxNotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;
            if (str != null)
            {
                if (str.Length > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message);
        }

        public String Message { get; set; }
    }
}
