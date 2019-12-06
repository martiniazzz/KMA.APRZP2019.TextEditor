using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    public class MainWindowViewModel: ILoaderOwner
    {
        private ICommand _exitCommand;

        private Visibility _visibility = Visibility.Hidden;
        private bool _isEnabled = true;

        public Visibility LoaderVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command executing <see cref="ExitExecute(object)"/>
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand<object>(ExitExecute));
            }
        }

        /// <summary>
        /// Exit from application
        /// </summary>
        /// <param name="obj"></param>
        private void ExitExecute(object obj)
        {
            AutoLoginService.Instance.CloseApp();
        }

        internal MainWindowViewModel()
        {
            //initialize service that controls loader
            LoaderService.Instance.Initialize(this);
        }

        internal void StartApplication()
        {
            //If user is logged in, navigate to file editor view whe app starts, otherwise navigate to login view
            NavigationService.Instance.Navigate(AutoLoginService.Instance.CurrentUser != null ? ModesEnum.TextEditor : ModesEnum.LogIn);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
