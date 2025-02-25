using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Timer_WPF.View.SelectionButtons
{
    public partial class SelectionButtons : UserControl
    {
        public SelectionButtons()
        {
            InitializeComponent();
            SetButtons();
        }

        private void SetButtons()
        {
            string path = "C:\\Users\\yamiy\\OneDrive\\Escritorio\\Proyectos\\4 -- C#\\Timer WPF\\log.csv";



            btn_option1.Content = "15";
            btn_option2.Content = "30";
            btn_option3.Content = "45";
        }

        private void UpdateLog()
        {
            string path = "C:\\Users\\yamiy\\OneDrive\\Escritorio\\Proyectos\\4 -- C#\\Timer WPF\\log.csv";

            using (StreamWriter sw = new StreamWriter(path, append: false))
            {
                sw.WriteLine($"{btn_option1.Content},{btn_option1.Content},{btn_option1.Content}");
            }
        }

        private void TimerOptionClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
