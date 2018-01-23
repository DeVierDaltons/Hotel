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
    public class RoomValidator : ValidationRule
    {
        [Dependency]
        public IEnumerable<Room> Rooms { get; set; }

        //  public IEnumerable<Room> Rooms { get; set; }

        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            if(Rooms.Any(room => room.RoomNumber == value.ToString()))
            {
                return new ValidationResult(false, "Another room has the same name");
            }
            if (value.ToString() == "")
            {
                return new ValidationResult(false, "Room Number cannot be empty.");
            }
            else if (value.ToString().Length > 3)
            {
                return new ValidationResult(false, "Name cannot be more than 3 characters long.");
            }
            return ValidationResult.ValidResult;
        }
    }
}