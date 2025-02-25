using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer_WPF.View;

namespace Timer_WPF.View.TopBar
{
    public partial class TopBar : UserControl
    {
        public TopBar()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs mouse)
        {
            if (mouse.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Close();
        }
    }
}
