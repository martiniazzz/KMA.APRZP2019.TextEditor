using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KMA.APRZP2019.TextEditorProject.TextEditor.Tools;

namespace KMA.APRZP2019.TextEditorProject.TextEditor.Tools.Controls
{
    /// <summary>
    /// Interaction logic for PassswordControl.xaml
    /// </summary>
    public partial class PassswordControl : UserControl
    {
        public PassswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register
      (
          "Password",
          typeof(string),
          typeof(PassswordControl),
          new PropertyMetadata(null)
      );
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
    }
}
