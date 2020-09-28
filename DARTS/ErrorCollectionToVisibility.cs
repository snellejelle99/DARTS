using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DARTS
{
    class ErrorCollectionToVisibility : IValueConverter
    {
        // Value converter method for checking if textboxes are filled or not
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ReadOnlyCollection<ValidationError> collection = value as ReadOnlyCollection<ValidationError>;
            // If value in textbox is not zero, it shows as visible
            if (collection != null && collection.Count > 0)
                return Visibility.Visible;
            // If value is zero, visibility is not shown
            else
                return Visibility.Collapsed;
        }

        // Converts visibility after textbox gets emptied again
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
}
