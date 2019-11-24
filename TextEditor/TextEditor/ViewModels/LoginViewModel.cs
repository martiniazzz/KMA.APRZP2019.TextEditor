using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.RestClient;
using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand<object>(LoginExecuteAsync, LoginCanExecute));
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand<object>(RegisterExecute));
            }
        }

        
        private async void LoginExecuteAsync(object obj)
        {
            LoaderService.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
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
                AutoLoginService.CurrentUser = currentUser;
                return true;
            });
            LoaderService.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ModesEnum.TextEditor);
        }

        private bool LoginCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }

        private void RegisterExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.Register);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
