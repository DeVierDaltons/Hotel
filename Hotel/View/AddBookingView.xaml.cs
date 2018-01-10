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
using System.Collections.Specialized;

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        private const int DateBlockSize = 40;
        private const int DatesToDisplay = 35;

        private const int HeaderRows = 3; // january, 1, Tue
        private const int HeaderColumns = 5; // RoomNumber, Price, Quality, HasNiceView

        /// <summary>
        /// The date of the first element in the view
        /// </summary>
        private DateTime StartDate = DateTime.Today;
        /// <summary>
        /// The currently selected range of dates to be booked
        /// </summary>
        private BookingPeriod SelectedRange = new BookingPeriod();
        /// <summary>
        /// The currently selected rooms to book
        /// </summary>
        private List<Room> SelectedRooms = new List<Room>();
        /// <summary>
        /// The orange blocks on top of the green and red ones, showing the intersection of rooms with dates currently selected
        /// </summary>
        private List<Canvas> SelectionElements = new List<Canvas>();
        /// <summary>
        /// The indicator above the dates in the header, showing which dates are currently selected
        /// </summary>
        private Canvas DateRangeElement = null;
        /// <summary>
        /// Whether the view will interpret a mousedown & up on the same date to mean the enddate for the booking.
        /// This is toggled so that it is possible to select a startdate and then an enddate.
        /// </summary>
        private bool ExpectingEndOfRangeSelection = false;

        /// <summary>
        /// All the rooms
        /// </summary>
        private ObservableCollection<Room> Rooms;
        /// <summary>
        /// Dictionary linking each room to a checkbox; toggling this checkbox includes/removes the room from the booking selection.
        /// </summary>
        private Dictionary<Room, CheckBox> IncludedCheckBoxes = new Dictionary<Room, CheckBox>();
        /// <summary>
        /// The last room the mouse went down on
        /// </summary>
        private Room lastDownRoom;
        /// <summary>
        /// The last date the mouse went down on
        /// </summary>
        private DateTime lastDownDate;

        public AddBookingView()
        {
            InitializeComponent();
            UseDataContextLater(); // TODO: this is fucked up, change this! the problem is that the DataContext is set later, because it is dependent on the parent's datacontext
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
                AddGridRowForRoom(row, room);
                ++row;
            }
            Rooms.CollectionChanged += OnRoomsChanged;
        }

        private class RoomRowElements
        {
            public RowDefinition rowDefinition;
            public List<UIElement> uiElements = new List<UIElement>();
        }

        private Dictionary<Room, RoomRowElements> RowElementsForRoom = new Dictionary<Room, RoomRowElements>();

        private void AddGridRowForRoom(int row, Room room)
        {
            RoomRowElements elements = new RoomRowElements()
            {
                rowDefinition = new RowDefinition() { Height = GridLength.Auto }
            };
            RoomDateGrid.RowDefinitions.Add(elements.rowDefinition);
            RowElementsForRoom.Add(room, elements);
            AddRoomDescription(row, room);
            AddRoomAvailability(row, room);
        }

        private void OnRoomsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( e.OldItems != null)
            {
                Room removedRoom = (Room)e.OldItems[0];
                RemoveGridRowForRoom(removedRoom);
            }
            if( e.NewItems != null)
            {
                Room addedRoom = (Room)e.NewItems[0];
                int row = HeaderRows + Rooms.Count - 1;
                AddGridRowForRoom(row, addedRoom);
            }
        }

        private void RemoveGridRowForRoom(Room removedRoom)
        {
            {
                var checkbox = IncludedCheckBoxes[removedRoom];
                if (checkbox.IsChecked.Value)
                {
                    checkbox.IsChecked = false;
                }
            }
            var elements = RowElementsForRoom[removedRoom];
            RoomDateGrid.RowDefinitions.Remove(elements.rowDefinition);
            foreach (UIElement roomElement in elements.uiElements)
            {
                RoomDateGrid.Children.Remove(roomElement);
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
            if (SelectedRange.IsValid())
            {
                foreach (Room room in SelectedRooms)
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
            }
            ResetDateSelectionElement();
            CopyDatesAndRoomsToViewModel();
        }

        private void ResetDateSelectionElement()
        {
            if( !SelectedRange.IsValid())
            {
                return;
            }
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
                AddBackgroundClicker(column, dayNumberRow, dayNameRow, date);
                date = date.AddDays(1d);
                ++column;
            }
        }

        /// <summary>
        /// Adds a transparent clickable Canvas which triggers MouseDown/UpOnDate for the given date
        /// </summary>
        /// <param name="column">the column of the grid to add it to</param>
        /// <param name="startRow">the row to start the element at</param>
        /// <param name="endRow">the last row the element should span across</param>
        /// <param name="date">the date for this element</param>
        private void AddBackgroundClicker(int column, int startRow, int endRow, DateTime date)
        {
            Canvas backgroundClicker = new Canvas();
            backgroundClicker.Background = Brushes.Transparent;
            Grid.SetColumn(backgroundClicker, column);
            Grid.SetRow(backgroundClicker, startRow);
            Grid.SetRowSpan(backgroundClicker, endRow - startRow + 1);
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
            RowElementsForRoom[room].uiElements.Add(includedCheckbox);
            RoutedEventHandler checkChangeClosure = (object sender, RoutedEventArgs e) =>
            {
                CheckBox box = (CheckBox)sender;
                CheckBoxChanged(box.IsChecked.Value, room);
            };
            includedCheckbox.Checked += checkChangeClosure;
            includedCheckbox.Unchecked += checkChangeClosure;
            IncludedCheckBoxes.Add(room, includedCheckbox);
            var numberText = CreateTextBlock(room.RoomNumber, false, column++, row);
            RowElementsForRoom[room].uiElements.Add(numberText);
            var qualityText = CreateTextBlock(room.Quality.ToString(), false, column++, row);
            RowElementsForRoom[room].uiElements.Add(qualityText);
            var priceText = CreateTextBlock(room.PricePerDay.ToString(), false, column++, row);
            RowElementsForRoom[room].uiElements.Add(priceText);
            var checkbox = CreateCheckBox(room.HasNiceView, column++, row);
            RowElementsForRoom[room].uiElements.Add(checkbox);
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

        private void CopyDatesAndRoomsToViewModel()
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
            lastDownRoom = room;
            lastDownDate = date;
            SelectedRange.StartDate = date;
        }

        private void MouseUpOnField(Room room, DateTime date)
        {
            if (date == lastDownDate)
            {
                if (ExpectingEndOfRangeSelection)
                {
                    SelectedRange.StartDate = SelectedRange.EndDate;
                    ExpectingEndOfRangeSelection = false;
                }
                else
                {
                    ExpectingEndOfRangeSelection = true;
                }
            }
            else
            {
                ExpectingEndOfRangeSelection = false;
            }
            SetOtherDate(date);
            DeselectAllRooms();
            SelectBetween(lastDownRoom, room);
            SetSelectionElements();
        }

        /// <summary>
        /// Assuming SelectedRange.StartDate is already set, make SelectedRange a range from that date to the given one,
        /// flipping the dates as necessary to make sure the end is not before the start date.
        /// </summary>
        /// <param name="date"></param>
        private void SetOtherDate(DateTime date)
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
        }

        private void SelectBetween(Room beginRoom, Room endRoom)
        {
            int room1Index = Rooms.IndexOf(beginRoom);
            int room2Index = Rooms.IndexOf(endRoom);
            int beginIndex = Math.Min(room1Index, room2Index);
            int endIndex = Math.Max(room1Index, room2Index);
            for(int i = beginIndex; i <= endIndex; ++i)
            {
                Room roomToSelect = Rooms[i];
                IncludedCheckBoxes[roomToSelect].IsChecked = true;
            }
        }

        private void DeselectAllRooms()
        {
            foreach (Room previouslySelectedRoom in new List<Room>(SelectedRooms))
            {
                IncludedCheckBoxes[previouslySelectedRoom].IsChecked = false;
            }
        }

        private void MouseDownOnDate(DateTime date)
        {
            SelectedRange.StartDate = date;
            SetSelectionElements();
        }

        private void MouseUpOnDate(DateTime date)
        {
            SelectedRange.EndDate = date;
            ResetDateSelectionElement();
            SetSelectionElements();
        }
        #endregion

        #region Creating grid elements
        private void CreateColouredField(bool available, int column, int row, Room room, DateTime date)
        {
            Canvas canvas = CreateRoomDateField(column, row);
            RowElementsForRoom[room].uiElements.Add(canvas);
            canvas.Background = available ? Brushes.Green : Brushes.Red;
            Canvas clickHandler = CreateRoomDateField(column, row);
            RowElementsForRoom[room].uiElements.Add(clickHandler);
            clickHandler.Background = Brushes.Transparent;
            clickHandler.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                MouseDownOnField(room, date);
            };
            clickHandler.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                MouseUpOnField(room, date);
            };
            Panel.SetZIndex(clickHandler, int.MaxValue);
        }

        private Canvas CreateRoomDateField(int column, int row)
        {
            Canvas canvas = new Canvas();
            canvas.Width = DateBlockSize;
            canvas.Height = DateBlockSize;
            canvas.Margin = new Thickness(0d);
            canvas.VerticalAlignment = VerticalAlignment.Center;
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(canvas, column);
            Grid.SetRow(canvas, row);
            RoomDateGrid.Children.Add(canvas);
            return canvas;
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
