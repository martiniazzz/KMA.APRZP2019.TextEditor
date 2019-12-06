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

        public RegisterViewModel()
        {
            NavigationService.Instance.NavigateModeChanged += OnNavigateModeChanged;
        }

        /// <summary>
        /// Executed when Navigation is performed
        /// </summary>
        /// <param name="mode">Value representing page to navigate to</param>
        private void OnNavigateModeChanged(Mode mode)
        {
            ClearForm();
        }

        /// <summary>
        /// Clears all inputs
        /// </summary>
        private void ClearForm()
        {
            Password = string.Empty;
            Login = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
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

        /// <summary>
        /// Command executing <see cref="LoginExecute(object)"/>
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand<object>(LoginExecute));
            }
        }

        /// <summary>
        /// Command executing <see cref="RegisterExecute(object)"/>
        /// </summary>
        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand<object>(RegisterExecute, RegisterCanExecute));
            }
        }

        /// <summary>
        /// Navigate to login view
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            NavigationService.Instance.Navigate(Mode.LogIn);
        }


        /// <summary>
        /// Register new user and redirect to text editor view
        /// </summary>
        /// <param name="obj"></param>
        private async void RegisterExecute(object obj)
        {
            LoaderService.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                if (!IsFormValid())
                {
                    return false;
                }
                try
                {
                    //this code added to show loader working
                    Thread.Sleep(3000);
                    var user = new User(Login, FirstName, LastName, Email, Password);
                    using (var restClient = new UserApiService())
                    {
                        restClient.AddUser(user);
                    }
                    AutoLoginService.Instance.CurrentUser = user;
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
                NavigationService.Instance.Navigate(Mode.TextEditor);
            }
        }

        /// <summary>
        /// Checks whether fields are valid and if not displays appropriate message
        /// </summary>
        /// <returns><c>true</c> if fields are valid, <c>false</c> otherwise</returns>
        private bool IsFormValid()
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(_email))
                {
                    MessageBox.Show(String.Format(Resources.SignUp_EmailIsNotValid, _email));
                    return false;
                }
                using (var restClient = new UserApiService())
                {
                    if (restClient.UserExists(Login))
                    {
                        MessageBox.Show(String.Format(Resources.SignUp_UserLoginAlreadyExists, _login));
                        return false;
                    }
                    if (restClient.UserExists(Email))
                    {
                        MessageBox.Show(String.Format(Resources.SignUp_UserEmailAlreadyExists, _email));
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
            return true;
        }

        /// <summary>
        /// Specifies condition when <see cref="RegisterCommand"/> is available
        /// </summary>
        /// <param name="obj"></param>
        /// <returns><c>true</c> if all vields are not empty, <c>false</c> otherwise</returns>
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
