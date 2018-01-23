using Hotel.Model;
using Hotel.Repository;
using Hotel.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Unity.Attributes;

namespace Hotel.UIValidators
{
    public class RoomBedsValidator : ValidationRule
    {
        const int MinimumBeds = 0;

        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            string contents = value.ToString();
            if (string.IsNullOrEmpty(contents) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Beds cannot be empty.");
            }
            int numberOfBeds;
            bool isValidNumber = int.TryParse(contents, out numberOfBeds);
            if( !isValidNumber )
            {
                return new ValidationResult(false, $"{contents} is not a valid integer.");
            }
            if( numberOfBeds < MinimumBeds )
            {
                return new ValidationResult(false, $"Need at least {MinimumBeds} beds.");
            }
            return ValidationResult.ValidResult;
        }
    }
}