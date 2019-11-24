using KMA.APRZP2019.TextEditorProject.TextEditor.Services;
using KMA.APRZP2019.TextEditorProject.TextEditor.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TextEditorView : UserControl
    {

        public TextEditorView()
        {
            InitializeComponent();
            InitializeFontOptions();
            TextEditorViewModel textEditorViewModel = new TextEditorViewModel(new TextEditorDialogService(), new RtfFileService());
            DataContext = textEditorViewModel;
        }

        private void InitializeFontOptions()
        {
            mFontsList.SelectionChanged += (s, e) => 
                MainTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, e.AddedItems[0]);
            
            mFontSizesList.SelectionChanged += (s, e) =>
                MainTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, e.AddedItems[0]);
        }

        private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            var textRange = new TextRange(MainTextBox.Selection.Start, MainTextBox.Selection.End);

            var fontFamily = textRange.GetPropertyValue(TextElement.FontFamilyProperty);
            mFontsList.SelectedItem = fontFamily;

            var fontSize = textRange.GetPropertyValue(TextElement.FontSizeProperty);
            mFontSizesList.Text = fontSize.ToString();

            if (!String.IsNullOrEmpty(textRange.Text))
            {
                TopBarButtonBold.IsChecked = textRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
                TopBarButtonItalic.IsChecked = textRange.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
                TopBarButtonUnderline.IsChecked = textRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            }
        }

    }
}
