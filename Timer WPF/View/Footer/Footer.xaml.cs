using System.Windows;
using System.Windows.Controls;

namespace Timer_WPF.View.Footer
{
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.MainContent.Content = new View.SettingsPage();
            }
        }

        private void NightModeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
