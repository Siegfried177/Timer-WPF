using System.Windows;
using Timer_WPF.ViewModels;

namespace Timer_WPF
{
    public partial class MainWindow : Window
    {
        private CountdownControl _countdownControl = new CountdownControl();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _countdownControl;
        }

        
    }
}
