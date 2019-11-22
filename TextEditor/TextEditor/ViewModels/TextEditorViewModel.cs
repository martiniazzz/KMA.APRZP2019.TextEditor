using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class TextEditorViewModel : INotifyPropertyChanged
    {

        private String _mainTextBoxText;

        private ICommand _clearAllCommand;
        private ICommand _logoutCommand;

        public String MainTextBoxText
        {
            get
            {
                return _mainTextBoxText;
            }
            set
            {
                _mainTextBoxText = value;
                OnPropertyChanged();
            }
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

        public ICommand ClearAllCommand
        {
            get
            {
                return _clearAllCommand ?? (_clearAllCommand = new RelayCommand<object>(ClearAllExecute));
            }
        }

        private void ClearAllExecute(object obj)
        {
            MainTextBoxText = String.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
