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
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using NHibernate.Util;
using Unity.Attributes;

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        private class RoomRowElements
        {
            public RowDefinition rowDefinition;
            public List<UIElement> uiElements = new List<UIElement>();
        }

        private const int DateBlockSize = 40;
        private int DatesToDisplay = 29;
        private const int DateShiftButtonSize = 3;

        private const int HeaderRows = 3; // january, 1, Tue
        private const int HeaderColumns = 6; // Selected, RoomNumber, Price, Quality, HasNiceView, Beds
        private const int MarginForDateSelectionElement = 15;
        private const int MarginForRoomDateSelectionElement = 10;
        private Dictionary<Room, Dictionary<int, Canvas>> FieldForRoomDateOffset = new Dictionary<Room, Dictionary<int, Canvas>>();

        private List<Tuple<TextBlock, TextBlock, UIElement>> DateHeaderBlocksList = new List<Tuple<TextBlock, TextBlock, UIElement>>();
        private Grid monthsPanel;
        private Button laterButton;
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
        /// For each room, a RoomRowElements defining which elements should be removed if that room is removed
        /// </summary>
        private Dictionary<Room, RoomRowElements> RowElementsForRoom = new Dictionary<Room, RoomRowElements>();
        /// <summary>
        /// The row for the months' names
        /// </summary>
        private const int monthRow = 0;
        /// <summary>
        /// The row containing the number of each day (in the month)
        /// </summary>
        private const int dayNumberRow = 1;
        /// <summary>
        /// The row containing the day of the week name
        /// </summary>
        private const int dayNameRow = 2;

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
        private const int RightSideMargin = 70;

        public AddBookingView()
        {
            InitializeComponent();
            Application.Current.MainWindow.SizeChanged += AddBookingView_SizeChanged;
        }

        public BookingView BookingView { get; set; }

        private void AddBookingView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if( Rooms == null ) { return; } // Initialize() has not been called yet
            if (e.NewSize.Width == 0d) { return; }
            int widthForDateBlocks = ((int)Window.GetWindow(this).ActualWidth - RightSideMargin) - ((int)BookingView.BookingsGrid.ActualWidth + HeaderColumnsWidth());
            int newNumDates = Math.Max(12, (widthForDateBlocks / DateBlockSize));
            SetNumDatesToDisplay(newNumDates);
            RefreshMonthsPanel();
            SetLaterButtonColumn();
            CreateOrUpdateDayLabels();
            ResetRoomDateSelectionElements();
            ResetDateSelectionElement();
        }

        private int HeaderColumnsWidth()
        {
            double total = 0;
            var columns = RoomDateGrid.ColumnDefinitions;
            for(int i = 0; i < HeaderColumns; ++i)
            {
                total += columns[i].ActualWidth;
            }
            return (int)total;
        }

        private void SetNumDatesToDisplay(int newNumDates)
        {
            int oldNumDates = DatesToDisplay;
            DatesToDisplay = newNumDates;
            if (oldNumDates > DatesToDisplay)
            {
                foreach (Dictionary<int, Canvas> dateFields in FieldForRoomDateOffset.Values)
                {
                    for (int i = DatesToDisplay; i < oldNumDates; ++i)
                    {
                        RoomDateGrid.Children.Remove(dateFields[i]);
                        dateFields.Remove(i);
                    }
                }
                RoomDateGrid.ColumnDefinitions.RemoveRange(DatesToDisplay, oldNumDates - DatesToDisplay);
                HeaderGrid.ColumnDefinitions.RemoveRange(DatesToDisplay, oldNumDates - DatesToDisplay);
            }
            else if (DatesToDisplay > oldNumDates)
            {
                for (int i = oldNumDates; i < DatesToDisplay; ++i)
                {
                    foreach (Room room in Rooms)
                    {
                        CreateColouredField(room, i);
                    }
                    AddDateColumnDefinition();
                }
            }
        }

        public void Initialize()
        {
            var viewModel = (AddBookingViewModel)DataContext;
            Rooms = viewModel.AllRooms;
            FillDateGrid();
            ListenForChanges(viewModel.AllBookings);
            BookingView.BookingsGrid.SizeChanged += AddBookingView_SizeChanged;
        }

        private void ListenForChanges(ObservableCollection<Booking> bookings)
        {
            bookings.CollectionChanged += BookingsChanged;
            Rooms.CollectionChanged += OnRoomsChanged;
        }

        private void BookingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( e.OldItems != null)
            {
                RedrawAvailabilityForBooking((Booking)e.OldItems[0]);
            }
            if( e.NewItems != null)
            {
                RedrawAvailabilityForBooking((Booking)e.NewItems[0]);
            }
        }

        private void RedrawAvailabilityForBooking(Booking booking)
        {
            booking.Rooms.ForEach(RedrawAvailabilityForRoom);
        }

        private void FillDateGrid()
        {
            AddColumns();
            AddHeader();
            AddGridRows();
        }

        private void AddColumns()
        {
            RoomDateGrid.ColumnDefinitions.Clear();
            HeaderGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < HeaderColumns; ++i)
            {
                string columnSizeGroupName = "Column" + i;
                RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, SharedSizeGroup = columnSizeGroupName });
                HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, SharedSizeGroup = columnSizeGroupName });
            }
            for (int i = 0; i < DatesToDisplay; ++i)
            {
                AddDateColumnDefinition();
            }
        }

        private void AddDateColumnDefinition()
        {
            RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, MinWidth = DateBlockSize });
            HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, MinWidth = DateBlockSize });
        }

        private void AddGridRows()
        {
            Rooms.ForEach(AddGridRowForRoom);
        }

        private void AddAvailabilityForRooms()
        {
            Rooms.ForEach(AddRoomAvailability);
        }

        private void AddGridRowForRoom(Room room)
        {
            if (!RowElementsForRoom.ContainsKey(room))
            {
                RoomRowElements elements = new RoomRowElements()
                {
                    rowDefinition = new RowDefinition() { Height = GridLength.Auto }
                };
                RoomDateGrid.RowDefinitions.Add(elements.rowDefinition);
                RowElementsForRoom.Add(room, elements);
            }
            AddRoomDescription(room);
            AddRoomAvailability(room);
        }

        private void RedrawAvailabilityForRoom(Room room)
        {
            DateTime date = StartDate;
            int row = Rooms.IndexOf(room);
            for (int i = 0; i < DatesToDisplay; ++i)
            {
                SetFieldColour(room, i);
                date = date.AddDays(1d);
            }
        }

        private void OnRoomsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( e.OldItems != null)
            {
                Room removedRoom = (Room)e.OldItems[0];
                DeselectIfSelected(removedRoom);
                RemoveGridRowForRoom(removedRoom);
            }
            if( e.NewItems != null)
            {
                Room addedRoom = (Room)e.NewItems[0];
                int row = Rooms.Count - 1;
                AddGridRowForRoom(addedRoom);
            }
        }

        private void RemoveGridRowForRoom(Room removedRoom)
        {
            var elements = RowElementsForRoom[removedRoom];
            RoomDateGrid.RowDefinitions.Remove(elements.rowDefinition);
            foreach (UIElement roomElement in elements.uiElements)
            {
                RoomDateGrid.Children.Remove(roomElement);
            }
            RowElementsForRoom.Remove(removedRoom);
            IncludedCheckBoxes.Remove(removedRoom);
            foreach(UIElement roomDateField in FieldForRoomDateOffset[removedRoom].Values)
            {
                RoomDateGrid.Children.Remove(roomDateField);
            }
            FieldForRoomDateOffset.Remove(removedRoom);
            ResetRowsForRooms();
        }

        private void ResetRowsForRooms() {
            int row = 0;
            foreach (Room room in Rooms) {
                var elements = RowElementsForRoom[room];
                elements.uiElements.ForEach(element => Grid.SetRow(element, row));
                ++row;
            }
        }

        private void DeselectIfSelected(Room removedRoom)
        {
            var checkbox = IncludedCheckBoxes[removedRoom];
            if (checkbox.IsChecked.Value)
            {
                checkbox.IsChecked = false;
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

        /// <summary>
        /// Sets the orange markers in the availability grid, and the date selection marker above the dates,
        /// and copies the data into the viewmodel
        /// </summary>
        private void SetSelectionElements()
        {
            ResetRoomDateSelectionElements();
            ResetDateSelectionElement();
            CopyDatesAndRoomsToViewModel();
        }

        private void ResetRoomDateSelectionElements()
        {
            ClearRoomDateSelectionElements();
            BookingPeriod viewPeriod = new BookingPeriod(StartDate, StartDate.AddDays(DatesToDisplay - 1));
            if (SelectedRange.IsValid() && SelectedRange.OverlapsWith(viewPeriod))
            {
                int startColumn, columnSpan;
                GetColumnAndSpanOfSelectedPeriod(out startColumn, out columnSpan);
                int rightMargin = SelectedRange.EndDate <= viewPeriod.EndDate ? MarginForRoomDateSelectionElement : 0;
                int leftMargin = SelectedRange.StartDate >= viewPeriod.StartDate ? MarginForRoomDateSelectionElement : 0;
                foreach (Room room in SelectedRooms)
                {
                    int row = Rooms.IndexOf(room);
                    Canvas SelectionElement = new Canvas();
                    SelectionElement.Background = Brushes.Orange;
                    SelectionElement.Margin = new Thickness(leftMargin, 20, rightMargin, 10);
                    Grid.SetColumnSpan(SelectionElement, columnSpan);
                    AddToRoomDateGrid(startColumn, row, SelectionElement);
                    SelectionElements.Add(SelectionElement);
                }
            }
        }

        private void GetColumnAndSpanOfSelectedPeriod(out int startColumn, out int columnSpan)
        {
            BookingPeriod viewPeriod = new BookingPeriod(StartDate, StartDate.AddDays(DatesToDisplay - 1));
            DateTime firstDate = DateMax(StartDate, SelectedRange.StartDate);
            DateTime lastDate = DateMin(viewPeriod.EndDate, SelectedRange.EndDate);
            startColumn = HeaderColumns + (firstDate - StartDate).Days;
            columnSpan = (lastDate - firstDate).Days + 1;
        }

        private void ResetDateSelectionElement()
        {
            BookingPeriod viewPeriod = new BookingPeriod(StartDate, StartDate.AddDays(DatesToDisplay - 1));
            if (!SelectedRange.IsValid() || SelectedRange.DoesNotoverlapWith(viewPeriod))
            {
                RemoveDateRangeElement();
                return;
            }
            CreateDateRangeElement();
            int startColumn;
            int columnSpan;
            GetColumnAndSpanOfSelectedPeriod(out startColumn, out columnSpan);
            Grid.SetRow(DateRangeElement, dayNumberRow);
            Grid.SetColumn(DateRangeElement, startColumn);
            Grid.SetColumnSpan(DateRangeElement, columnSpan);
            int rightMargin = SelectedRange.EndDate <= viewPeriod.EndDate ? MarginForDateSelectionElement : 0;
            int leftMargin = SelectedRange.StartDate >= viewPeriod.StartDate ? MarginForDateSelectionElement : 0;
            DateRangeElement.Margin = new Thickness(leftMargin, DateRangeElement.Margin.Top, rightMargin, DateRangeElement.Margin.Bottom);
        }

        private DateTime DateMin(DateTime first, DateTime second)
        {
            return first < second ? first : second;
        }

        private DateTime DateMax(DateTime first, DateTime second)
        {
            return first > second ? first : second;
        }

        private void CreateDateRangeElement()
        {
            if (DateRangeElement == null)
            {
                DateRangeElement = new Canvas
                {
                    Background = Brushes.Orange,
                    Margin = new Thickness(MarginForDateSelectionElement, 30, MarginForDateSelectionElement, 5)
                };
                HeaderGrid.Children.Add(DateRangeElement);
            }
        }

        private void RemoveDateRangeElement()
        {
            if (DateRangeElement != null)
            {
                HeaderGrid.Children.Remove(DateRangeElement);
                DateRangeElement = null;
            }
        }

        /// <summary>
        /// Adds a header to the top to give names to the columns
        /// </summary>
        /// <param name="datesToDisplay">number of dates in the grid</param>
        private void AddHeader()
        {
            HeaderGrid.RowDefinitions.Clear();
            for (int i = 0; i < HeaderRows; ++i)
            {
                HeaderGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            int column = 0;
            CreateTextBlock("Selected", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(10d);
            CreateTextBlock("Room", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(10d);
            CreateTextBlock("Quality", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(10d);
            CreateTextBlock("Beds", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(10d);
            CreateTextBlock("Price", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(5d);
            CreateTextBlock("View", true, column++, dayNumberRow, HeaderGrid).Margin = new Thickness(2d);
            CreateDateHeader();
        }

        private void CreateDateShiftButtons()
        {
            Button earlierButton = CreateHeaderButton("Earlier", HeaderColumns, monthRow, DateShiftButtonSize, ShiftDatesBack);
            laterButton = CreateHeaderButton("Later", 0, monthRow, DateShiftButtonSize, ShiftDatesForward);
            SetLaterButtonColumn();
        }

        private void SetLaterButtonColumn()
        {
            Grid.SetColumn(laterButton, HeaderColumns + DateShiftButtonSize + GetMonthLabelsSize());
        }

        private int GetMonthLabelsSize()
        {
            return DatesToDisplay - 2 * DateShiftButtonSize;
        }

        private int NextMonth(int month)
        {
            if( month == 12)
            {
                return 1;
            }
            return month + 1;
        }

        private bool AreSameDay(DateTime one, DateTime two)
        {
            return one.Day == two.Day && one.Month == two.Month && one.Year == two.Year;
        }

        private int NumberOfDaysInPeriodWithinMonth(DateTime startDay, DateTime endDay, int month)
        {
            int numberOfDays = 0;
            DateTime beyondEndRange = endDay.AddDays(1d);
            while(!AreSameDay(startDay, beyondEndRange))
            {
                if( startDay.Month == month)
                {
                    ++numberOfDays;
                }
                startDay = startDay.AddDays(1d);
            }
            return numberOfDays;
        }

        private void RefreshMonthsPanel()
        {
            if( monthsPanel == null)
            {
                monthsPanel = new Grid();
                monthsPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                monthsPanel.Width = double.NaN; // this represents "Auto"
                AddToHeaderGrid(HeaderColumns + DateShiftButtonSize, monthRow, monthsPanel);
            }
            monthsPanel.Children.Clear();
            monthsPanel.ColumnDefinitions.Clear();
            DateTime lastDayToDisplay = StartDate.AddDays(DatesToDisplay - 1);
            int year = StartDate.Year;
            int column = 0;
            for(int i = StartDate.Month; i != NextMonth(lastDayToDisplay.Month); i = NextMonth(i))
            {
                int sizeOfMonthLabel = NumberOfDaysInPeriodWithinMonth(StartDate, lastDayToDisplay, i);
                sizeOfMonthLabel = sizeOfMonthLabel * sizeOfMonthLabel; // make the differences more extreme by raising power
                monthsPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(sizeOfMonthLabel, GridUnitType.Star) });
                TextBlock monthBlock = new TextBlock();
                DateTimeFormatInfo dateInfo = new DateTimeFormatInfo();
                monthBlock.Text = string.Format("{0} {1}", dateInfo.GetMonthName(i), year);
                monthBlock.FontSize = 14;
                monthBlock.Margin = new Thickness(15d);
                monthBlock.FontWeight = StartDate.Month == i ? FontWeights.Bold : FontWeights.Normal;
                monthBlock.Foreground = new SolidColorBrush(Colors.Black);
                monthBlock.VerticalAlignment = VerticalAlignment.Center;
                monthBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(monthBlock, column++);
                Grid.SetRow(monthBlock, 0);
                monthsPanel.Children.Add(monthBlock);
                if( i == 12)
                {
                    ++year;
                }
            }
            Grid.SetColumnSpan(monthsPanel, GetMonthLabelsSize());
        }

        /// <summary>
        /// Create the labels above the availability grid with the number and name of each day
        /// </summary>
        /// <param name="column">which column to start at in the grid</param>
        /// <param name="dayNumberRow">which row the day's numbers should be displayed in</param>
        /// <param name="dayNameRow">which row the day's name should be displayed in</param>
        /// <param name="date">which date to start at</param>
        private void CreateOrUpdateDayLabels()
        {
            int column = HeaderColumns;
            DateTime date = StartDate;
            for (int i = 0; i < DatesToDisplay; ++i)
            {
                TextBlock dayNumberText, dayNameText;
                if (DateHeaderBlocksList.Count <= i)
                {
                    dayNumberText = CreateBasicTextBlock(column, dayNumberRow, HeaderGrid);
                    dayNameText = CreateBasicTextBlock(column, dayNameRow, HeaderGrid);
                    var dateClicker = AddDateBackgroundClicker(column, dayNumberRow, dayNameRow, i);
                    var dateHeader = new Tuple<TextBlock, TextBlock, UIElement>(dayNumberText, dayNameText, dateClicker);
                    DateHeaderBlocksList.Add(dateHeader);
                } else
                {
                    var dateHeader = DateHeaderBlocksList[i];
                    dayNumberText = dateHeader.Item1;
                    dayNameText = dateHeader.Item2;
                }
                bool boldDay = date.Month == StartDate.Month; // use DateNumberList etc.
                dayNumberText.Text = date.Day.ToString();
                dayNumberText.FontWeight = boldDay ? FontWeights.Bold : FontWeights.Normal;
                dayNameText.Text = date.ToString("ddd", CultureInfo.InvariantCulture);
                dayNameText.FontWeight = boldDay ? FontWeights.Bold : FontWeights.Normal;
                date = date.AddDays(1d);
                ++column;
            }
            if (DateHeaderBlocksList.Count > DatesToDisplay)
            {
                for (int i = DatesToDisplay; i < DateHeaderBlocksList.Count; ++i)
                {
                    var dateHeader = DateHeaderBlocksList[i];
                    HeaderGrid.Children.Remove(dateHeader.Item1);
                    HeaderGrid.Children.Remove(dateHeader.Item2);
                    HeaderGrid.Children.Remove(dateHeader.Item3);
                }
                DateHeaderBlocksList.RemoveRange(DatesToDisplay, DateHeaderBlocksList.Count - DatesToDisplay);
            }
        }

        private void ShiftDatesBack()
        {
            ShiftDates(-20d);
        }

        private void ShiftDatesForward()
        {
            ShiftDates(20d);
        }

        private void ShiftDates(double difference)
        {
            StartDate = StartDate.AddDays(difference);
            RefreshMonthsPanel();
            SetLaterButtonColumn();
            CreateOrUpdateDayLabels();
            Rooms.ForEach(RedrawAvailabilityForRoom);
            ResetRoomDateSelectionElements();
            ResetDateSelectionElement();
        }

        private void CreateDateHeader()
        {
            RefreshMonthsPanel();
            CreateDateShiftButtons();
            CreateOrUpdateDayLabels();
        }

        /// <summary>
        /// Adds a transparent clickable Canvas which triggers MouseDown/UpOnDate for the given date
        /// </summary>
        /// <param name="column">the column of the grid to add it to</param>
        /// <param name="startRow">the row to start the element at</param>
        /// <param name="endRow">the last row the element should span across</param>
        /// <param name="date">the date for this element</param>
        private Canvas AddDateBackgroundClicker(int column, int startRow, int endRow, int dateOffset)
        {
            Canvas backgroundClicker = new Canvas
            {
                Background = Brushes.Transparent
            };
            Grid.SetRowSpan(backgroundClicker, endRow - startRow + 1);
            AddToHeaderGrid(column, startRow, backgroundClicker);
            backgroundClicker.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                MouseDownOnDate(dateOffset);
            };
            backgroundClicker.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                MouseUpOnDate(dateOffset);
            };
            Panel.SetZIndex(backgroundClicker, int.MaxValue);
            return backgroundClicker;
        }

        /// <summary>
        /// Adds columns for each of the Room's properties.
        /// </summary>
        /// <param name="row">the row of the grid we're adding to</param>
        /// <param name="room">the room to display properties of</param>
        private void AddRoomDescription(Room room)
        {
            int row = Rooms.IndexOf(room);
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

            var numberText = CreateTextBlock(room.RoomNumber, false, column++, row, RoomDateGrid);
            var numberBinding = MakeOneWayBinding(room, nameof(room.RoomNumber));
            numberText.SetBinding(TextBlock.TextProperty, numberBinding);
            RowElementsForRoom[room].uiElements.Add(numberText);

            var qualityText = CreateTextBlock(room.Quality.ToString(), false, column++, row, RoomDateGrid);
            var qualityBinding = MakeOneWayBinding(room, nameof(room.Quality));
            qualityText.SetBinding(TextBlock.TextProperty, qualityBinding);
            RowElementsForRoom[room].uiElements.Add(qualityText);

            var bedsText = CreateTextBlock("", false, column++, row, RoomDateGrid);
            var bedsBinding = MakeOneWayBinding(room, nameof(room.Beds));
            bedsText.SetBinding(TextBlock.TextProperty, bedsBinding);
            RowElementsForRoom[room].uiElements.Add(bedsText);

            var priceText = CreateTextBlock(room.PricePerDay.ToString(), false, column++, row, RoomDateGrid);
            var priceBinding = MakeOneWayBinding(room, nameof(room.PricePerDay));
            priceText.SetBinding(TextBlock.TextProperty, priceBinding);
            RowElementsForRoom[room].uiElements.Add(priceText);

            var checkbox = CreateCheckBox(room.HasNiceView, column++, row);
            var viewBinding = MakeOneWayBinding(room, nameof(room.HasNiceView));
            checkbox.SetBinding(ToggleButton.IsCheckedProperty, viewBinding);
            RowElementsForRoom[room].uiElements.Add(checkbox);
            checkbox.IsHitTestVisible = false;
        }

        private static Binding MakeOneWayBinding(object source, string propertyName)
        {
            Binding newBinding = new Binding(propertyName);
            newBinding.Source = source;
            newBinding.Mode = BindingMode.OneWay;
            return newBinding;
        }

        private void AddRoomAvailability(Room room)
        {
            DateTime date = StartDate;
            for(int column = 0; column < DatesToDisplay; ++column)
            {
                CreateColouredField(room, column);
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
            viewModel.Rooms = SelectedRooms;
        }

        private void SelectedGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddBookingViewModel viewModel = (AddBookingViewModel)DataContext;
            if( viewModel.Guests == null)
            {
                viewModel.Guests = new List<Guest>();
            }
            viewModel.Guests.Clear();
            foreach(object guest in ((ListBox)sender).SelectedItems)
            {
                viewModel.Guests.Add((Guest)guest);
            }
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

        private void MouseDownOnField(Room room, int dateOffset)
        {
            DateTime date = StartDate.AddDays(dateOffset);
            lastDownRoom = room;
            lastDownDate = date;
            SelectedRange.StartDate = date;
        }

        private void MouseUpOnField(Room room, int dateOffset)
        {
            DateTime date = StartDate.AddDays(dateOffset);
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
            if( beginRoom == null )
            {
                beginRoom = endRoom;
            }
            int room1Index = Rooms.IndexOf(beginRoom);
            int room2Index = Rooms.IndexOf(endRoom);
            if( room1Index == -1 || room2Index == -1 )
            {
                return;
            }
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

        private void MouseDownOnDate(int offset)
        {
            DateTime date = StartDate.AddDays(offset);
            lastDownRoom = null;
            lastDownDate = date;
            SelectedRange.StartDate = date;
        }

        private void MouseUpOnDate(int offset)
        {
            DateTime date = StartDate.AddDays(offset);
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
            SelectedRange.EndDate = date;
            ResetDateSelectionElement();
            SetSelectionElements();
        }
        #endregion

        #region Creating grid elements
        private void CreateColouredField(Room room, int dateOffset)
        {
            int column = dateOffset + HeaderColumns;
            int row = Rooms.IndexOf(room);
            Canvas canvas = CreateRoomDateField(column, row);
            RegisterField(room, dateOffset, canvas);
            SetFieldColour(room, dateOffset);
            RowElementsForRoom[room].uiElements.Add(canvas);
            canvas.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                MouseDownOnField(room, dateOffset);
            };
            canvas.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                MouseUpOnField(room, dateOffset);
            };
        }

        private void RegisterField(Room room, int dateOffset, Canvas canvas)
        {
            if (!FieldForRoomDateOffset.ContainsKey(room))
            {
                FieldForRoomDateOffset.Add(room, new Dictionary<int, Canvas>());
            }
            FieldForRoomDateOffset[room].Add(dateOffset, canvas);
        }

        private void SetFieldColour(Room room, int dateOffset)
        {
            Canvas canvas = FieldForRoomDateOffset[room][dateOffset];
            canvas.Background = room.DayAvailable(StartDate.AddDays(dateOffset)) ? Brushes.Green : Brushes.Red;
            canvas.InvalidateVisual();
        }

        private Canvas CreateRoomDateField(int column, int row)
        {
            Canvas canvas = new Canvas();
            canvas.Width = DateBlockSize;
            canvas.Height = DateBlockSize;
            canvas.Margin = new Thickness(0d);
            canvas.VerticalAlignment = VerticalAlignment.Center;
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            AddToRoomDateGrid(column, row, canvas);
            return canvas;
        }

        private CheckBox CreateCheckBox(bool enabled, int column, int row)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.IsChecked = enabled;
            checkbox.VerticalAlignment = VerticalAlignment.Center;
            checkbox.HorizontalAlignment = HorizontalAlignment.Center;
            AddToRoomDateGrid(column, row, checkbox);
            return checkbox;
        }

        private void AddToRoomDateGrid(int column, int row, UIElement element)
        {
            AddToGrid(column, row, element, RoomDateGrid);
        }

        private void AddToHeaderGrid(int column, int row, UIElement element)
        {
            AddToGrid(column, row, element, HeaderGrid);
        }

        private void AddToGrid(int column, int row, UIElement element, Grid grid)
        {
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);
            grid.Children.Add(element);
        }

        private Button CreateHeaderButton(string text, int column, int row, int columnSpan, Action onClickAction)
        {
            Button newButton = new Button();
            newButton.Click += (object sender, RoutedEventArgs e) =>
            {
                onClickAction();
            };
            newButton.Width = DateBlockSize * columnSpan - 20d;
            newButton.Height = DateBlockSize * 0.8;
            newButton.Margin = new Thickness(10d);
            newButton.Content = text;
            newButton.FontSize = 14;
            newButton.VerticalAlignment = VerticalAlignment.Center;
            newButton.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(newButton, columnSpan);
            AddToHeaderGrid(column, row, newButton);
            return newButton;
        }

        private TextBlock CreateTextBlock(string text, bool bold, int column, int row, Grid grid)
        {
            TextBlock newBlock = CreateBasicTextBlock(column, row, grid);
            newBlock.Text = text;
            newBlock.FontWeight = bold ? FontWeights.Bold : FontWeights.Normal;
            return newBlock;
        }

        private TextBlock CreateBasicTextBlock(int column, int row, Grid grid)
        {
            TextBlock newBlock = new TextBlock();
            newBlock.FontSize = 14;
            newBlock.Foreground = new SolidColorBrush(Colors.Black);
            newBlock.VerticalAlignment = VerticalAlignment.Center;
            newBlock.HorizontalAlignment = HorizontalAlignment.Center;
            AddToGrid(column, row, newBlock, grid);
            return newBlock;
        }
        #endregion
    }
}
