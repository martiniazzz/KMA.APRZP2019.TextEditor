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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class HistoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<UserRequest> _userRequests;

        private ICommand _toFileMenuCommand;
        private ICommand _logoutCommand;

        public HistoryViewModel()
        {
            NavigationManager.Instance.NavigateModeChanged += OnNavigateModeChanged;
        }

        private async void OnNavigateModeChanged(ModesEnum mode)
        {
            if (mode == ModesEnum.History)
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
                            IEnumerable<UserRequest> userRequests = userRequestApi.GetUserRequests(AutoLoginService.CurrentUser.Guid)
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

        public ICommand ToFileMenuCommand
        {
            get
            {
                return _toFileMenuCommand ?? (_toFileMenuCommand = new RelayCommand<object>(ToFileMenuExecute));
            }
        }

        private void ToFileMenuExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.TextEditor);
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand<object>(LogoutExecute));
            }
        }

        private void LogoutExecute(object obj)
        {
            AutoLoginService.CurrentUser = null;
            NavigationManager.Instance.Navigate(ModesEnum.LogIn);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
