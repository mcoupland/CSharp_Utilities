using System.Windows;

namespace Utilities
{
    public class Wpf
    {
        #region Method to Get size of UIElement May or May Not work depending on when it is called (during window layout)
        public static System.Drawing.Size GetElementSize(UIElement element)
        {
            element.Measure(new System.Windows.Size());
            ushort width = Utilities.Numbers.GetIntCeiling(element.DesiredSize.Width);
            ushort height = Utilities.Numbers.GetIntCeiling(element.DesiredSize.Height);
            return new System.Drawing.Size(width, height);
        }
        #endregion

    }
}
