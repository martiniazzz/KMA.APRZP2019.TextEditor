using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.RestClient;
using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _login;

        private ICommand _loginCommand;
        private ICommand _registerCommand;

        public LoginViewModel()
        {
            NavigationService.Instance.NavigateModeChanged += OnNavigateModeChanged;
        }

        /// <summary>
        /// Executed when Navigation is performed
        /// </summary>
        /// <param name="mode">Value representing page to navigate to</param>
        private void OnNavigateModeChanged(Mode mode)
        {
            Password = string.Empty;
            Login = string.Empty;
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command executing <see cref="LoginExecuteAsync(object)"/>
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand<object>(LoginExecuteAsync, LoginCanExecute));
            }
        }

        /// <summary>
        /// Command executing <see cref="RegisterExecute(object)"/>
        /// </summary>
        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand<object>(RegisterExecute));
            }
        }


        /// <summary>
        /// Logins user and navigates to file editor view
        /// </summary>
        /// <param name="obj"></param>
        private async void LoginExecuteAsync(object obj)
        {
            LoaderService.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
                    //this code added to show loader working
                    Thread.Sleep(3000);
                    using (var restClient = new UserApiService())
                    {
                        currentUser = restClient.GetUserByLoginOrEmail(Login);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                        ex.Message));
                    return false;
                }
                if (currentUser == null)
                {
                    MessageBox.Show(Resources.SignIn_FailedToLogin);
                    return false;
                }
                try
                {
                    if (!currentUser.CheckPassword(_password))
                    {
                        MessageBox.Show(Resources.SignIn_FailedToLogin);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                        ex.Message));
                    return false;
                }
                AutoLoginService.Instance.CurrentUser = currentUser;
                return true;
            });
            LoaderService.Instance.HideLoader();
            if (result)
                NavigationService.Instance.Navigate(Mode.TextEditor);
        }

        /// <summary>
        /// Specifies condition when <see cref="LoginCommand"/> is available
        /// </summary>
        /// <param name="obj"></param>
        /// <returns><c>true</c> if login and password are not empty, <c>false</c> othervice</returns>
        private bool LoginCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }

        /// <summary>
        /// Navigates to register view
        /// </summary>
        /// <param name="obj"></param>
        private void RegisterExecute(object obj)
        {
            NavigationService.Instance.Navigate(Mode.Register);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
