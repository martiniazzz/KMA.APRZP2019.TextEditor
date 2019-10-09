using System.Collections.ObjectModel;
using System.Windows.Media;

namespace KMA.APRZP2019.TextEditorProject.TextEditor
{
    class FontList : ObservableCollection<string>
    {
        public FontList()
        {
            foreach (FontFamily f in Fonts.SystemFontFamilies)
            {
                this.Add(f.ToString());
            }
        }
    }
}
