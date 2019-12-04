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
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

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

        /// <summary>
        /// defines whether last changes where saved
        /// </summary>
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

        /// <summary>
        /// Path to currently opened file
        /// </summary>
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
                //update last modified date and time of currently opened file
                OnPropertyChanged("LastModifiedDateStr");
            }
        }

        /// <summary>
        /// Last modified date and time of currently opened file
        /// </summary>
        public string LastModifiedDateStr
        {
            get
            {
                if (File.Exists(FilePath))
                {
                    return File.GetLastWriteTime(FilePath).ToString();
                }
                else
                {
                    return "new file";
                }
            }
          
        }
        #endregion

        #region Commands properties
        /// <summary>
        /// Command executing <see cref="ClearAllExecute(object)"/>
        /// </summary>
        public ICommand ClearAllCommand
        {
            get
            {
                return _clearAllCommand ?? (_clearAllCommand = new RelayCommand<object>(ClearAllExecute));
            }
        }

        /// <summary>
        /// Clear all text
        /// </summary>
        /// <param name="obj"></param>
        private void ClearAllExecute(object obj)
        {
            MainTextBoxText = String.Empty;
        }

        /// <summary>
        /// Command executing <see cref="OpenHistoryExecute(object)"/>
        /// </summary>
        public ICommand OpenHistoryCommand
        {
            get
            {
                return _openHistoryCommand ?? (_openHistoryCommand = new RelayCommand<object>(OpenHistoryExecute));
            }
        }

        /// <summary>
        /// Navigate to user requests history fiew
        /// </summary>
        /// <param name="obj"></param>
        private void OpenHistoryExecute(object obj)
        {
            NavigationService.Instance.Navigate(ModesEnum.History);
        }

        /// <summary>
        /// Command executing <see cref="OpenFileExecuteAsync(FlowDocument)"/>
        /// </summary>
        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new RelayCommand<FlowDocument>(OpenFileExecuteAsync));
            }
        }

        /// <summary>
        /// Opens file dialog and display formatted content of the selected file on specified <see cref="FlowDocument"/>
        /// </summary>
        /// <param name="doc"><see cref="FlowDocument"/> to show file content on</param>
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
                        await ShowSaveFileDialogAsync(doc);
                    }
                }
                if (_dialogService.OpenFileDialog() == true)
                {
                    bool isSuccess = await LoadFileAsync(_dialogService.FilePath, doc);
                    if (!isSuccess)
                    {
                        throw new LoadFileException(String.Format(Resources.LoadFileException_Msg, _dialogService.FilePath));
                    }
                    FilePath = _dialogService.FilePath;
                    SwitchIsSavedExecute(true);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// Command executing <see cref="SaveFileExecuteAsync(FlowDocument)"/>
        /// </summary>
        public ICommand SaveFileCommand
        {
            get
            {
                return _saveFileCommand ?? (_saveFileCommand = new RelayCommand<FlowDocument>(SaveFileExecuteAsync));
            }
        }

        /// <summary>
        /// Shows Save File Dialog and saves formatted content of specified <see cref="FlowDocument"/> to selected file
        /// </summary>
        /// <param name="doc"> <see cref="FlowDocument"/> which content will be saved</param>
        private async void SaveFileExecuteAsync(FlowDocument doc)
        {
            try
            {
                await ShowSaveFileDialogAsync(doc);
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// Command executing <see cref="NewFileExecuteAsync(FlowDocument)"/>
        /// </summary>
        public ICommand NewFileCommand
        {
            get
            {
                return _newFileCommand ?? (_newFileCommand = new RelayCommand<FlowDocument>(NewFileExecuteAsync));
            }
        }

        /// <summary>
        /// Offers user to save last changes if not already saved, then clears all settings 
        /// </summary>
        /// <param name="doc"><see cref="FlowDocument"/> to work with </param>
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
                        await ShowSaveFileDialogAsync(doc);
                    }
                }
                MainTextBoxText = string.Empty;
                FilePath = string.Empty;
                SwitchIsSavedExecute(true);
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        #region Async operations
        /// <summary>
        /// Shows Save File Dialog, saves content of specified <see cref="FlowDocument"/> to selected file, saves  appropriate user request to requests history
        /// </summary>
        /// <param name="doc"><see cref="FlowDocument"/> which content will be saved</param>
        /// <returns>task representing <c>async</c> operation</returns>
        private async Task ShowSaveFileDialogAsync(FlowDocument doc)
        {
                if (_dialogService.SaveFileDialog() == true)
                {
                    bool isFileChanged = IsChanged(MainTextBoxText, _dialogService.FilePath);
                    bool isSuccess = await SaveFileAsync(_dialogService.FilePath, doc);
                    if (!isSuccess)
                    {
                        throw new SaveFileException(String.Format(Resources.SaveFileException_Msg, _dialogService.FilePath));
                    }
                    FilePath = _dialogService.FilePath;
                    _dialogService.ShowMessage(Resources.SaveFile_Success);
                    AddUserRequestAsync(_dialogService.FilePath, isFileChanged);
                  
                }
        }

        /// <summary>
        /// Saves formatted content of <paramref name="doc"/> to <paramref name="filepath"/>
        /// </summary>
        /// <param name="filepath">Filepath to which content of <paramref name="doc"/> will be saved</param>
        /// <param name="doc"><see cref="FlowDocument"/> which content will be saved</param>
        /// <returns>task representing <c>async</c> operation with <c>bool</c> parameter, 
        /// that is <c>true</c> if task completes successfully, <c>false</c> otherwise</returns>
        private async Task<bool> SaveFileAsync(string filepath, FlowDocument doc)
        {
            LoaderService.Instance.ShowLoader();
            bool isSuccess = await Task.Run(async () =>
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
                       return await doc.Dispatcher.InvokeAsync( () =>
                            {
                                try
                                {
                                    _fileService.Save(filepath, doc.ContentStart, doc.ContentEnd);
                                    return true;
                                }
                                catch(Exception)
                                {
                                    return false;
                                }
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
                SwitchIsSavedExecute(true);
            }
            LoaderService.Instance.HideLoader();
            return isSuccess;
        }

        /// <summary>
        /// Opens file <paramref name="filepath"/> and shows its content on <paramref name="doc"/>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="doc"></param>
        /// <returns>task representing <c>async</c> operation with <c>bool</c> parameter, 
        /// that is <c>true</c> if task completes successfully, <c>false</c> otherwise</returns>
        private async Task<bool> LoadFileAsync(string filepath, FlowDocument doc)
        {
            LoaderService.Instance.ShowLoader();
            bool isSuccess = await Task.Run(async () =>
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
                        return await doc.Dispatcher.InvokeAsync(() =>
                        {
                            try
                            {
                                _fileService.Load(filepath, doc.ContentStart, doc.ContentEnd);
                                return true;
                            }
                            catch (Exception)
                            {
                                return false;
                            }
                           
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

        /// <summary>
        /// Creates new user request about changing files and saves it
        /// </summary>
        /// <param name="filepath">File user worked with</param>
        /// <param name="isChanged"><c>true</c> if file <paramref name="filepath"/> was changed, otherwise <c>false</c></param>
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

        /// <summary>
        /// Command executing <see cref="SwitchIsSavedExecute(bool)"/>
        /// </summary>
        public ICommand SwitchIsSavedCommand
        {
            get
            {
                return _switchIsSavedCommand ?? (_switchIsSavedCommand = new RelayCommand<bool>(SwitchIsSavedExecute));
            }
        }

        /// <summary>
        /// Sets the value of property <see cref="IsSaved"/> to <paramref name="isSaved"/>
        /// </summary>
        /// <param name="isSaved">New value for <see cref="IsSaved"/> property</param>
        private void SwitchIsSavedExecute(bool isSaved)
        {
            IsSaved = isSaved;
        }

        /// <summary>
        /// Command executing <see cref="LogoutExecute(object)"/>
        /// </summary>
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand<object>(LogoutExecute));
            }
        }

        /// <summary>
        /// Logouts user and navigates to login page
        /// </summary>
        /// <param name="obj"></param>
        private void LogoutExecute(object obj)
        {
            AutoLoginService.CurrentUser = null;
            NavigationService.Instance.Navigate(ModesEnum.LogIn);
        }
        #endregion

        #region Text comparison
        /// <summary>
        /// Checks if <paramref name="text"/> was changed in comparison to content of file <paramref name="filepath"/>
        /// </summary>
        /// <param name="text">Text to compare with content of <paramref name="filepath"/></param>
        /// <param name="filepath">File which content wil be compared to <paramref name="text"/></param>
        /// <returns><c>true</c> if <paramref name="text"/> was changed in comparison to content of <paramref name="filepath"/>, <c>false</c> otherwsise</returns>
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
