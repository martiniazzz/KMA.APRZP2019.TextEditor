using KMA.APRZP2019.TextEditorProject.DBModels;
using KMA.APRZP2019.TextEditorProject.RestClient;
using KMA.APRZP2019.TextEditorProject.RestClient.Autologin;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.Exceptions;
using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Properties;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services.interfaces;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;
using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class TextEditorViewModel : INotifyPropertyChanged
    {
        private string _mainTextBoxText;
        private bool _isSaved = true;
        private string _filePath;

        private IDialogService _dialogService;
        private IFileService _fileService;

        private ICommand _clearAllCommand;
        private ICommand _openHistoryCommand;
        private ICommand _switchIsSavedCommand;

        private ICommand _openFileCommand;
        private ICommand _saveFileCommand;
        private ICommand _newFileCommand;

        private ICommand _logoutCommand;


        public TextEditorViewModel(IDialogService dialogService, IFileService fileService)
        {
            this._dialogService = dialogService;
            this._fileService = fileService;
        }

        #region Data properties

        public string MainTextBoxText
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

        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                _isSaved = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands properties
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

        public ICommand OpenHistoryCommand
        {
            get
            {
                return _openHistoryCommand ?? (_openHistoryCommand = new RelayCommand<object>(OpenHistoryExecute));
            }
        }

        private void OpenHistoryExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.History);
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new RelayCommand<FlowDocument>(OpenFileExecuteAsync));
            }
        }

        private async void OpenFileExecuteAsync(FlowDocument doc)
        {
            try
            {
                if (!IsSaved)
                {
                    MessageBoxResult answ = _dialogService.ShowYesNoQuestion(Resources.SaveFile_Question);
                    if (answ == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    if (answ == MessageBoxResult.Yes)
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            bool isFileChanged = IsChanged(MainTextBoxText, _dialogService.FilePath);
                            bool isSuccess = await SaveFileAsync(_dialogService.FilePath, doc);
                            if (isSuccess)
                            {
                                _dialogService.ShowMessage(Resources.SaveFile_Success);
                                AddUserRequestAsync(_dialogService.FilePath, isFileChanged);
                            }
                            else
                            {
                                _dialogService.ShowMessage(Resources.SaveFile_Failed);
                            }
                        }
                    }
                }
                if (_dialogService.OpenFileDialog() == true)
                {
                    bool isSuccess = await LoadFileAsync(_dialogService.FilePath, doc);
                    if (!isSuccess)
                    {
                        throw new LoadFileException(String.Format(Resources.LoadFileException_Msg, _dialogService.FilePath));
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        public ICommand SaveFileCommand
        {
            get
            {
                return _saveFileCommand ?? (_saveFileCommand = new RelayCommand<FlowDocument>(SaveFileExecuteAsync));
            }
        }

        private async void SaveFileExecuteAsync(FlowDocument doc)
        {
            try
            {
                if (_dialogService.SaveFileDialog() == true)
                {
                    bool isFileChanged = IsChanged(MainTextBoxText, _dialogService.FilePath);
                    bool isSuccess = await SaveFileAsync(_dialogService.FilePath, doc);
                    if (!isSuccess)
                    {
                        throw new SaveFileException(String.Format(Resources.SaveFileException_Msg, _dialogService.FilePath));
                    }
                    AddUserRequestAsync(_dialogService.FilePath, isFileChanged);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        public ICommand NewFileCommand
        {
            get
            {
                return _newFileCommand ?? (_newFileCommand = new RelayCommand<FlowDocument>(NewFileExecuteAsync));
            }
        }

        private async void NewFileExecuteAsync(FlowDocument doc)
        {
            try
            {
                if (!IsSaved)
                {
                    MessageBoxResult answ = _dialogService.ShowYesNoQuestion(Resources.SaveFile_Question);
                    if (answ == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    if (answ == MessageBoxResult.Yes)
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            bool isFileChanged = IsChanged(MainTextBoxText, _dialogService.FilePath);
                            bool isSuccess = await SaveFileAsync(_dialogService.FilePath, doc);
                            if (!isSuccess)
                            {
                                throw new SaveFileException(String.Format(Resources.SaveFileException_Msg, _dialogService.FilePath));
                            }
                            AddUserRequestAsync(_dialogService.FilePath, isFileChanged);
                        }
                    }
                }
                MainTextBoxText = string.Empty;
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        #region Async operations
        private async Task<bool> SaveFileAsync(string filepath, FlowDocument doc)
        {
            LoaderService.Instance.ShowLoader();
            bool isSuccess = await Task.Run(() =>
            {
                try
                {
                    //this code added to show loader working
                    Thread.Sleep(3000);
                    if (doc.Dispatcher.CheckAccess())
                    {
                        _fileService.Save(filepath, doc.ContentStart, doc.ContentEnd);
                    }
                    else
                    {
                        doc.Dispatcher.Invoke(() =>
                            {
                                _fileService.Save(filepath, doc.ContentStart, doc.ContentEnd);
                            });

                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            });
            if (isSuccess)
            {
                IsSaved = true;
            }
            LoaderService.Instance.HideLoader();
            return isSuccess;
        }

        private async Task<bool> LoadFileAsync(string filepath, FlowDocument doc)
        {
            LoaderService.Instance.ShowLoader();
            bool isSuccess = await Task.Run(() =>
            {
                try
                {
                    //this code added to show loader working
                    Thread.Sleep(3000);
                    if (doc.Dispatcher.CheckAccess())
                    {
                        _fileService.Load(filepath, doc.ContentStart, doc.ContentEnd);
                    }
                    else
                    {
                        doc.Dispatcher.Invoke(() =>
                        {
                            _fileService.Load(filepath, doc.ContentStart, doc.ContentEnd);
                        });
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            LoaderService.Instance.HideLoader();
            return isSuccess;
        }

        private async void AddUserRequestAsync(string filepath, bool isChanged)
        {

            using (UserRequestApiService requestService = new UserRequestApiService())
            {
                bool isSuccess = await Task.Run(() =>
                {
                    try
                    {
                        requestService.AddUserRequest(AutoLoginService.CurrentUser.Guid, new UserRequest(filepath, isChanged, DateTime.UtcNow));
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                if (!isSuccess)
                { 
                    _dialogService.ShowMessage(Resources.UserRequest_AddFailed);
                }
            }
        }
        #endregion

        public ICommand SwitchIsSavedCommand
        {
            get
            {
                return _switchIsSavedCommand ?? (_switchIsSavedCommand = new RelayCommand<bool>(SwitchIsSavedExecute));
            }
        }

        private void SwitchIsSavedExecute(bool isSaved)
        {
            IsSaved = isSaved;
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
        #endregion

        #region Text comparison
        bool IsChanged(String text, String filepath)
        {
            return !(FileComparison.EqualsTextToFileText(text, filepath));
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        internal virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
