using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using KMA.APRZP2019.TextEditorProject.TextEditor.Views.Authentication;
using KMA.APRZP2019.TextEditorProject.TextEditor.Views.History;
using LoginControl;
using System;
using TextEditor;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools
{
    class NavigationModel
    {
        /// <summary>
        /// Window which navigation will be controlled by the clasa
        /// </summary>
        private IContentWindow _contentWindow;

        #region Views to switch between
        private LoginFormView  _loginView;
        private RegisterFormView _registerView;
        private TextEditorView _textEditorView;
        private HistoryView _historyView;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentWindow">window where navigation will happen</param>
        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        /// <summary>
        /// Navigate to the appropriate page
        /// </summary>
        /// <param name="mode">Enum parameter representing page to navigate to</param>
        internal void Navigate(Mode mode)
        {
            switch (mode)
            {
                case Mode.LogIn:
                    _contentWindow.ContentControl.Content = _loginView ?? (_loginView = new LoginFormView());
                    break;
                case Mode.Register:
                    _contentWindow.ContentControl.Content = _registerView ?? (_registerView = new RegisterFormView());
                    break;
                case Mode.TextEditor:
                    _contentWindow.ContentControl.Content = _textEditorView ?? (_textEditorView = new TextEditorView());
                    break;
                case Mode.History:
                    _contentWindow.ContentControl.Content = _historyView ?? (_historyView = new HistoryView());
                    break;
               default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
