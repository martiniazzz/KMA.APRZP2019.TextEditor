using KMA.APRZP2019.TextEditorProject.TextEditor.Models.ValueObjects;
using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels
{
    class MainWindowViewModel
    {

        internal void StartApplication()
        {
            NavigationManager.Instance.Navigate(/*StationManager.CurrentUser != null ? ModesEnum.TextEditor :*/ ModesEnum.LogIn);
        }
    }
}
