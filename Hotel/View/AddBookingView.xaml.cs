using Hotel.ViewModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hotel.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        private const int DateBlockSize = 40;
        private const int DatesToDisplay = 35;

        private DateTime StartDate = DateTime.Today;
        private BookingPeriod SelectedRange = new BookingPeriod();
        private List<Room> SelectedRooms = new List<Room>();
        private List<Canvas> SelectionElements = new List<Canvas>();
        private Canvas DateRangeElement = null;

        private ObservableCollection<Room> Rooms;
        private const int HeaderRows = 3; // january, 1, Tue
        private const int HeaderColumns = 5; // RoomNumber, Price, Quality, HasNiceView

        private Dictionary<Room, CheckBox> IncludedCheckBoxes = new Dictionary<Room, CheckBox>();

        public AddBookingView()
        {
            InitializeComponent();
            UseDataContextLater(); // TODO: this is fucked up, change this! the problem is that the DataContext is set later, because it is dependent on the parent's datacontext
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            AddBookingViewModel viewModel = (AddBookingViewModel)DataContext;
            //viewModel.SelectedDates = BookingRangeCalendar.SelectedDates;
        }

        private async void UseDataContextLater()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                App.Current.Dispatcher.Invoke(() =>
                {
                    AddBookingViewModel viewModel = (AddBookingViewModel)DataContext;
                    SetGridStyle();
                    FillDateGrid(viewModel.Rooms);
                });
            });
        }

        private void FillDateGrid(ObservableCollection<Room> rooms)
        {
            Rooms = rooms;
            RoomDateGrid.ColumnDefinitions.Clear();
            for(int i = 0; i < HeaderColumns; ++i)
            {
                RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room name
            }
            for (int i = 0; i < DatesToDisplay; ++i)
            {
                RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room has nice view
            }
            AddHeader();
            int row = HeaderRows;
            foreach (Room room in rooms)
            {
                RoomDateGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                AddRoomDescription(row, room);
                AddRoomAvailability(row, room);
                ++row;
            }
        }

        private void ClearRoomDateSelectionElements()
        {
            foreach(Canvas element in SelectionElements)
            {
                RoomDateGrid.Children.Remove(element);
            }
            SelectionElements.Clear();
        }

        private void SetSelectionElements()
        {
            ClearRoomDateSelectionElements();
            foreach(Room room in SelectedRooms)
            {
                int row = HeaderRows + Rooms.IndexOf(room);
                int startColumn = HeaderColumns + (SelectedRange.StartDate - StartDate).Days;
                int columnSpan = (SelectedRange.EndDate - SelectedRange.StartDate).Days;
                Canvas SelectionElement = new Canvas();
                SelectionElement.Background = Brushes.Orange;
                SelectionElement.Margin = new Thickness(10, 20, 10, 10);
                Grid.SetRow(SelectionElement, row);
                Grid.SetColumn(SelectionElement, startColumn);
                Grid.SetColumnSpan(SelectionElement, columnSpan + 1);
                RoomDateGrid.Children.Add(SelectionElement);
                SelectionElements.Add(SelectionElement);
            }
            ResetDateSelectionElement();
        }

        private void ResetDateSelectionElement()
        {
            if( DateRangeElement == null)
            {
                DateRangeElement = new Canvas();
                DateRangeElement.Background = Brushes.Orange;
                DateRangeElement.Margin = new Thickness(10, 5, 10, 30);
                DateRangeElement.Visibility = Visibility.Hidden;
                RoomDateGrid.Children.Add(DateRangeElement);
            }
            int row = 1;
            int startColumn = HeaderColumns + (SelectedRange.StartDate - StartDate).Days;
            int columnSpan = (SelectedRange.EndDate - SelectedRange.StartDate).Days;
            Grid.SetRow(DateRangeElement, row);
            Grid.SetColumn(DateRangeElement, startColumn);
            Grid.SetColumnSpan(DateRangeElement, columnSpan + 1);
            DateRangeElement.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Adds a header to the top to give names to the columns
        /// </summary>
        /// <param name="datesToDisplay">number of dates in the grid</param>
        private void AddHeader()
        {
            for (int i = 0; i < HeaderRows; ++i)
            {
                RoomDateGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            int column = 0;
            const int monthRow = 0;
            const int dayNumberRow = 1;
            const int dayNameRow = 2;
            CreateTextBlock("Selected", true, column++, dayNumberRow).Margin = new Thickness(10d);
            CreateTextBlock("Room", true, column++, dayNumberRow).Margin = new Thickness(10d);
            CreateTextBlock("Quality", true, column++, dayNumberRow).Margin = new Thickness(10d);
            CreateTextBlock("Price", true, column++, dayNumberRow).Margin = new Thickness(5d);
            CreateTextBlock("View", true, column++, dayNumberRow).Margin = new Thickness(2d);
            DateTime date = StartDate;
            TextBlock monthHeader = CreateTextBlock(date.ToString("MMMM", CultureInfo.InvariantCulture), false, column, monthRow);
            Grid.SetColumnSpan(monthHeader, DatesToDisplay);
            for(int i = 0; i < DatesToDisplay; ++i)
            {
                bool boldDay = date.Month == StartDate.Month;
                CreateTextBlock(date.Day.ToString(), boldDay, column, dayNumberRow);
                CreateTextBlock(date.ToString("ddd", CultureInfo.InvariantCulture), boldDay, column, dayNameRow).VerticalAlignment = VerticalAlignment.Top;
                AddBackgroundClicker(column, monthRow, dayNameRow, date);
                date = date.AddDays(1d);
                ++column;
            }
        }

        private void AddBackgroundClicker(int column, int monthRow, int dayNameRow, DateTime date)
        {
            Canvas backgroundClicker = new Canvas();
            backgroundClicker.Background = Brushes.Transparent;
            Grid.SetColumn(backgroundClicker, column);
            Grid.SetRow(backgroundClicker, monthRow);
            Grid.SetRowSpan(backgroundClicker, dayNameRow - monthRow + 1);
            RoomDateGrid.Children.Add(backgroundClicker);
            backgroundClicker.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                MouseDownOnDate(date);
            };
            backgroundClicker.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                MouseUpOnDate(date);
            };
        }

        /// <summary>
        /// Adds columns for each of the Room's properties.
        /// </summary>
        /// <param name="row">the row of the grid we're adding to</param>
        /// <param name="room">the room to display properties of</param>
        private void AddRoomDescription(int row, Room room)
        {
            int column = 0;
            var includedCheckbox = CreateCheckBox(SelectedRooms.Contains(room), column++, row);
            RoutedEventHandler checkChangeClosure = (object sender, RoutedEventArgs e) =>
            {
                CheckBox box = (CheckBox)sender;
                CheckBoxChanged(box.IsChecked.Value, room);
            };
            includedCheckbox.Checked += checkChangeClosure;
            includedCheckbox.Unchecked += checkChangeClosure;
            IncludedCheckBoxes.Add(room, includedCheckbox);
            CreateTextBlock(room.RoomNumber, false, column++, row);
            CreateTextBlock(room.Quality.ToString(), false, column++, row);
            CreateTextBlock(room.PricePerDay.ToString(), false, column++, row);
            var checkbox = CreateCheckBox(room.HasNiceView, column++, row);
            checkbox.IsHitTestVisible = false;
        }

        private void AddRoomAvailability(int row, Room room)
        {
            DateTime date = StartDate;
            for(int column = HeaderColumns; column < HeaderColumns + DatesToDisplay; ++column)
            {
                CreateColouredField(room.DayAvailable(date), column, row, room, date);
                date = date.AddDays(1d);
            }
        }

        private bool IsValid(DateTime timeSelection)
        {
            return timeSelection != null && timeSelection.Year > 1;
        }

        private void CopyDatesToViewModel()
        {
            AddBookingViewModel viewModel = (AddBookingViewModel)DataContext;
            viewModel.SelectedDates = new BookingPeriod(SelectedRange.StartDate, SelectedRange.EndDate);
            viewModel.Room = SelectedRooms.FirstOrDefault(); // TODO: Modify bookings to actually use all the rooms
        }

        private void SetGridStyle()
        {
            RoomDateGrid.ShowGridLines = false;
            RoomDateGrid.HorizontalAlignment = HorizontalAlignment.Left;
            RoomDateGrid.VerticalAlignment = VerticalAlignment.Top;
        }

        #region Click Handlers
        private void CheckBoxChanged(bool IsChecked, Room room)
        {
            if (IsChecked)
            {
                SelectedRooms.Add(room);
            } else
            {
                SelectedRooms.Remove(room);
            }
            SetSelectionElements();
        }

        private void MouseDownOnField(Room room, DateTime date)
        {
            SelectedRange.StartDate = date;
        }

        private void MouseUpOnField(Room room, DateTime date)
        {
            if (date < SelectedRange.StartDate)
            {
                SelectedRange.EndDate = SelectedRange.StartDate;
                SelectedRange.StartDate = date;
            }
            else
            {
                SelectedRange.EndDate = date;
            }
            DeselectAllRooms();
            IncludedCheckBoxes[room].IsChecked = true;
            CopyDatesToViewModel();
            SetSelectionElements();
        }

        private void DeselectAllRooms()
        {
            foreach (Room previouslySelectedRoom in new List<Room>(SelectedRooms))
            {
                IncludedCheckBoxes[previouslySelectedRoom].IsChecked = false;
            }
            SelectedRooms.Clear();
        }

        private void MouseDownOnDate(DateTime date)
        {
            SelectedRange.StartDate = date;
        }

        private void MouseUpOnDate(DateTime date)
        {
            SelectedRange.EndDate = date;
            ResetDateSelectionElement();
            SetSelectionElements();
            CopyDatesToViewModel();
        }
        #endregion

        #region Creating grid elements
        private void CreateColouredField(bool available, int column, int row, Room room, DateTime date)
        {
            Canvas canvas = new Canvas();
            canvas.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                MouseDownOnField(room, date);
            };
            canvas.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                MouseUpOnField(room, date);
            };
            canvas.Background = available ? Brushes.Green : Brushes.Red;
            canvas.Width = DateBlockSize;
            canvas.Height = DateBlockSize;
            canvas.Margin = new Thickness(0d);
            canvas.VerticalAlignment = VerticalAlignment.Center;
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(canvas, column);
            Grid.SetRow(canvas, row);
            RoomDateGrid.Children.Add(canvas);
        }

        private CheckBox CreateCheckBox(bool enabled, int column, int row)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.IsChecked = enabled;
            checkbox.VerticalAlignment = VerticalAlignment.Center;
            checkbox.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(checkbox, column);
            Grid.SetRow(checkbox, row);
            RoomDateGrid.Children.Add(checkbox);
            return checkbox;
        }

        private TextBlock CreateTextBlock(string text, bool bold, int column, int row)
        {
            TextBlock newBlock = new TextBlock();
            newBlock.Text = text;
            newBlock.FontSize = 14;
            newBlock.FontWeight = bold ? FontWeights.Bold : FontWeights.Normal;
            newBlock.Foreground = new SolidColorBrush(Colors.Black);
            newBlock.VerticalAlignment = VerticalAlignment.Center;
            newBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(newBlock, column);
            Grid.SetRow(newBlock, row);
            RoomDateGrid.Children.Add(newBlock);
            return newBlock;
        }
        #endregion
    }
}
