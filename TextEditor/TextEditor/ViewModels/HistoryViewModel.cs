using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.RestClient;
using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class HistoryViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// List of user request about changing files
        /// </summary>
        private ObservableCollection<UserRequest> _userRequests;

        private ICommand _toFileMenuCommand;
        private ICommand _logoutCommand;

        public HistoryViewModel()
        {
            NavigationService.Instance.NavigateModeChanged += OnNavigateModeChanged;
        }

        /// <summary>
        /// Executed when Navigation is performed
        /// </summary>
        /// <param name="mode">Value representing page to navigate to</param>
        private async void OnNavigateModeChanged(Mode mode)
        {
            //Load user requests when History view is opened 
            if (mode == Mode.History)
            {
                LoaderService.Instance.ShowLoader();
                bool isSuccess = await Task.Run(() =>
                {
                    try
                    {
                        //this code added to show loader working
                        Thread.Sleep(3000);
                        using (UserRequestApiService userRequestApi = new UserRequestApiService())
                        {
                            IEnumerable<UserRequest> userRequests = userRequestApi.GetUserRequests(AutoLoginService.Instance.CurrentUser.Guid)
                            .Select(r => { r.ChangedAt = r.ChangedAt.ToLocalTime(); return r; });
                            UserRequests = new ObservableCollection<UserRequest>(userRequests);
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                        return false;
                    }
                });
                LoaderService.Instance.HideLoader();
                if (!isSuccess)
                {
                    Logger.Log(Resources.UserRequest_LoadAllFailed);
                    MessageBox.Show(Resources.UserRequest_LoadAllFailed);
                }
            }
            else
            {
                //Clear user requests list when user opens other pages
                UserRequests.Clear();
            }
        }

        public ObservableCollection<UserRequest> UserRequests
        {
            get
            {
                return _userRequests;
            }
            set
            {
                _userRequests = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command executing <see cref="ToFileMenuExecute(object)"></see>
        /// </summary>
        public ICommand ToFileMenuCommand
        {
            get
            {
                return _toFileMenuCommand ?? (_toFileMenuCommand = new RelayCommand<object>(ToFileMenuExecute));
            }
        }

        /// <summary>
        /// Navigate to file editor view
        /// </summary>
        /// <param name="obj"></param>
        private void ToFileMenuExecute(object obj)
        {
            NavigationService.Instance.Navigate(Mode.TextEditor);
        }

        /// <summary>
        /// Command executing <see cref="LogoutExecute(object)"></see>
        /// </summary>
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand<object>(LogoutExecute));
            }
        }

        /// <summary>
        /// Logouts user and navigates to login view
        /// </summary>
        /// <param name="obj"></param>
        private void LogoutExecute(object obj)
        {
            AutoLoginService.Instance.CurrentUser = null;
            NavigationService.Instance.Navigate(Mode.LogIn);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
