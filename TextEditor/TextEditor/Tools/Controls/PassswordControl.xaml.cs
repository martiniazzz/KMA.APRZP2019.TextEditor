using System.Windows;
using System.Windows.Controls;

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
