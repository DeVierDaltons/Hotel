using System.Windows;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for AddGuestView.xaml
    /// </summary>
    public partial class GuestDetailView : Window
    {
        public GuestDetailView()
        {
            InitializeComponent();
        }

        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
