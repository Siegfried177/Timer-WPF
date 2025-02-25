using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Timer_WPF.ViewModels
{
    public class CountdownControl : INotifyPropertyChanged
    {
        public string lbl_Countdown => TimeSpan.FromSeconds(Countdown).ToString(@"hh\:mm\:ss"); // Variable del Binding. Cadena formateada con el tiempo del contador
        private int _countdown = 10; // Valor mostrado del contador en segundos
        private int _initialCountdown = 10; // Valor inicial del contador en segundos
        private DispatcherTimer _timer; // Temporizador para disminuir el contador
        private int _timerState = 0; // El estado del contador. 0 = Stopped, 1 = Running, 2 = Paused

        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        // Visibilidad de los elementos de la parte central de la ventana
        public string StartButtonVisibility => _timerState != 0 ? "Collapsed" : "Visible";
        public string ContinueButtonVisibility => _timerState == 2 ? "Visible" : "Collapsed";
        public string PauseButtonVisibility => _timerState == 1? "Visible" : "Collapsed";
        public string StopButtonVisibility => _timerState == 1? "Visible" : "Collapsed";
        public string OptionsVisibility => _timerState != 0 ? "Collapsed" : "Visible";
        public string ProgressVisibility => _timerState == 0 ? "Collapsed" : "Visible";
        public string OkVisibility => _timerState == 3 ? "Visible" : "Collapsed";

        public ICommand StartCommand { get; }
        public ICommand ContinueCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ChangeTimerCommand { get; }
        public ICommand OkCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Countdown // Propiedad para el valor del contador
        {
            get => _countdown;
            set
            {
                _countdown = value;
                OnPropertyChanged(nameof(lbl_Countdown));
            }
        }

        public CountdownControl()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;

            StartCommand = new RelayCommand(StartTimer);
            ContinueCommand = new RelayCommand(ContinueTimer);
            PauseCommand = new RelayCommand(PauseTimer);
            StopCommand = new RelayCommand(StopTimer);
            ChangeTimerCommand = new RelayCommand(ChangeTimer);
            OkCommand = new RelayCommand(FinishedTimer);
        }

        private void ChangeTimer(object parameter)
        {
            Button button = parameter as Button;
            _countdown = int.Parse(button.Content.ToString()) * 60;
            _initialCountdown = _countdown;

            OnPropertyChanged(nameof(lbl_Countdown));
        }

        private void StartTimer(object parameter)
        {
            Countdown = _countdown;

            if (!_timer.IsEnabled) _timer.Start();

            _timerState = 1;
            Progress = 0;
            OnPropertyChanged(nameof(StartButtonVisibility));
            OnPropertyChanged(nameof(OptionsVisibility));
            OnPropertyChanged(nameof(PauseButtonVisibility));
            OnPropertyChanged(nameof(StopButtonVisibility));
            OnPropertyChanged(nameof(ProgressVisibility));
            OnPropertyChanged(nameof(Progress));
        }

        private void ContinueTimer(object parameter)
        {
            _timer.Start();

            _timerState = 1;
            OnPropertyChanged(nameof(ContinueButtonVisibility));
            OnPropertyChanged(nameof(PauseButtonVisibility));
            OnPropertyChanged(nameof(StopButtonVisibility));
            OnPropertyChanged(nameof(Progress));
        }

        private void PauseTimer(object parameter)
        {
            _timer.Stop();
            _timerState = 2;

            OnPropertyChanged(nameof(ContinueButtonVisibility));
            OnPropertyChanged(nameof(PauseButtonVisibility));
            OnPropertyChanged(nameof(StopButtonVisibility));
            OnPropertyChanged(nameof(Progress));
        }

        private void StopTimer(object parameter)
        {
            _timer.Stop();
            Countdown = 0;
            _timerState = 0;

            OnPropertyChanged(nameof(StartButtonVisibility));
            OnPropertyChanged(nameof(OptionsVisibility));
            OnPropertyChanged(nameof(PauseButtonVisibility));
            OnPropertyChanged(nameof(StopButtonVisibility));
            OnPropertyChanged(nameof(ProgressVisibility));
            OnPropertyChanged(nameof(Progress));
        }

        private void FinishedTimer(object parameter)
        {
            Countdown = _initialCountdown;
            _timerState = 0;

            OnPropertyChanged(nameof(StartButtonVisibility));
            OnPropertyChanged(nameof(OptionsVisibility));
            OnPropertyChanged(nameof(PauseButtonVisibility));
            OnPropertyChanged(nameof(StopButtonVisibility));
            OnPropertyChanged(nameof(ProgressVisibility));
            OnPropertyChanged(nameof(Progress));
            OnPropertyChanged(nameof(OkVisibility));
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (Countdown > 0) // Si todavía hay tiempo en el contador
            {
                Countdown--;
                Progress = (1 - (double)Countdown / _initialCountdown) * 100;
            }
            else // Si el contador llega a 0
            {
                _timer.Stop();
                _timerState = 3;

                OnPropertyChanged(nameof(StartButtonVisibility));
                OnPropertyChanged(nameof(OptionsVisibility));
                OnPropertyChanged(nameof(ProgressVisibility));
                OnPropertyChanged(nameof(PauseButtonVisibility));
                OnPropertyChanged(nameof(StopButtonVisibility));
                OnPropertyChanged(nameof(OkVisibility));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
