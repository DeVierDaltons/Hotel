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

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        private DateTime startDate = DateTime.Today;

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
            RoomDateGrid.ColumnDefinitions.Clear();
            RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room name
            RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room quality
            RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room price
            RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room has nice view
            int DatesToDisplay = 25;
            for(int i = 0; i < DatesToDisplay; ++i)
            {
                RoomDateGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // room has nice view
            }
            int row = AddHeader(DatesToDisplay);
            foreach(Room room in rooms)
            {
                RoomDateGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                int descriptionColumns = AddRoomDescription(row, room);
                AddRoomAvailability(row, room, DatesToDisplay, descriptionColumns);
                ++row;
            }
        }

        /// <summary>
        /// Adds a header to the top to give names to the columns
        /// </summary>
        /// <param name="datesToDisplay">number of dates in the grid</param>
        /// <returns>the number of rows added for the header</returns>
        private int AddHeader(int datesToDisplay)
        {
            int headerRows = 2;
            for (int i = 0; i < headerRows; ++i)
            {
                RoomDateGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            int column = 0;
            const int monthRow = 0;
            const int labelRow = 1;
            CreateTextBlock("Room", true, column++, labelRow).Margin = new Thickness(10d);
            CreateTextBlock("Quality", true, column++, labelRow).Margin = new Thickness(10d);
            CreateTextBlock("Price", true, column++, labelRow).Margin = new Thickness(5d);
            CreateTextBlock("View", true, column++, labelRow).Margin = new Thickness(2d);
            DateTime date = startDate;
            TextBlock monthHeader = CreateTextBlock(date.ToString("MMMM", CultureInfo.InvariantCulture), false, column, monthRow);
            Grid.SetColumnSpan(monthHeader, datesToDisplay);
            for(int i = 0; i < datesToDisplay; ++i)
            {
                bool boldDay = date.Month == startDate.Month;
                CreateTextBlock(date.Day.ToString(), boldDay, column, labelRow);
                date = date.AddDays(1d);
                ++column;
            }
            return headerRows;
        }

        /// <summary>
        /// Adds columns for each of the Room's properties.
        /// </summary>
        /// <param name="row">the row of the grid we're adding to</param>
        /// <param name="room">the room to display properties of</param>
        /// <returns>the number of columns added to the grid</returns>
        private int AddRoomDescription(int row, Room room)
        {
            int column = 0;
            CreateTextBlock(room.RoomNumber, false, column++, row);
            CreateTextBlock(room.Quality.ToString(), false, column++, row);
            CreateTextBlock(room.PricePerDay.ToString(), false, column++, row);
            MakeReadonlyCheckbox(room.HasNiceView, column++, row);
            return column;
        }

        private void AddRoomAvailability(int row, Room room, int datesToDisplay, int column)
        {
            DateTime date = startDate;
            for(int i = 0; i < datesToDisplay; ++i)
            {
                CreateColouredField(room.DayAvailable(date), column, row);
                date = date.AddDays(1d);
                ++column;
            }
        }

        private void CreateColouredField(bool available, int column, int row)
        {
            Canvas canvas = new Canvas();
            canvas.Background = available ? Brushes.Green : Brushes.Red;
            canvas.Width = 50;
            canvas.Height = 50;
            canvas.Margin = new Thickness(1d);
            canvas.VerticalAlignment = VerticalAlignment.Center;
            canvas.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(canvas, column);
            Grid.SetRow(canvas, row);
            RoomDateGrid.Children.Add(canvas);
        }

        private CheckBox MakeReadonlyCheckbox(bool enabled, int column, int row)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.IsHitTestVisible = false;
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

        private void SetGridStyle()
        {
            RoomDateGrid.ShowGridLines = false;
            RoomDateGrid.HorizontalAlignment = HorizontalAlignment.Left;
            RoomDateGrid.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
