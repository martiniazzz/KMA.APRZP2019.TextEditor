using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools.interfaces;
using KMA.APRZP2019.TextEditorProject.TextEditor.Views.Authentication;
using LoginControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TextEditor;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools
{
    class NavigationModel
    {

        private IContentWindow _contentWindow;
        private LoginFormView  _loginView;
        private RegisterFormView _registerView;
        private TextEditorView _textEditorView;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.LogIn:
                    _contentWindow.ContentControl.Content = _loginView ?? (_loginView = new LoginFormView());
                    break;
                case ModesEnum.Register:
                    _contentWindow.ContentControl.Content = _registerView ?? (_registerView = new RegisterFormView());
                    break;
                case ModesEnum.TextEditor:
                    _contentWindow.ContentControl.Content = _textEditorView ?? (_textEditorView = new TextEditorView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }
}
