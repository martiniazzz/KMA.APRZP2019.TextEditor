using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using KMA.APRZP2019.TextEditorProject.RestClient;
using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.APRZP2019.TextEditorProject.DBModels;
using System.Threading;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        private string _password;
        private string _login;
        private string _firstName;
        private string _lastName;
        private string _email;

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

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand<object>(LoginExecute));
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand<object>(RegisterExecute, RegisterCanExecute));
            }
        }

        private void LoginExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.LogIn);
        }


       
        private async void RegisterExecute(object obj)
        {
            LoaderService.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show(String.Format(Resources.SignUp_EmailIsNotValid, _email));
                        return false;
                    }
                    //this code added to show loader working
                    Thread.Sleep(3000);
                    using (var restClient = new UserApiService())
                    {
                        if (restClient.UserExists(Login))
                        {
                            MessageBox.Show(String.Format(Resources.SignUp_UserAlreadyExists, _login));
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(Resources.SignUp_FailedToValidateData, Environment.NewLine,
                       ex.Message));
                    return false;
                }
                try
                {
                    var user = new User(Login,FirstName,LastName,Email,Password);
                    using (var restClient = new UserApiService())
                    {
                        restClient.AddUser(user);
                    }
                    AutoLoginService.CurrentUser = user;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(Resources.SignUp_FailedToCreateUser, Environment.NewLine,
                        ex.Message));
                    return false;
                }
                MessageBox.Show(String.Format(Resources.SignUp_UserSuccessfulyCreated, _login));
                return true;
            });
            LoaderService.Instance.HideLoader();
            if (result)
            {
                NavigationManager.Instance.Navigate(ModesEnum.TextEditor);
            }
        }

        private bool RegisterCanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(Login) &&
                 !String.IsNullOrWhiteSpace(Password) &&
                 !String.IsNullOrWhiteSpace(FirstName) &&
                 !String.IsNullOrWhiteSpace(LastName) &&
                 !String.IsNullOrWhiteSpace(Email);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
