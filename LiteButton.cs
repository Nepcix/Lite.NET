using System.Windows;
using System.Windows.Controls;

namespace Lite.NET
{
    public class LiteButton : Button
    {
        public static readonly DependencyProperty VisualStatesBrushesProperty =
            LiteWindow.VisualStatesBrushesProperty.AddOwner(typeof(LiteButton));

        public VisualStatesBrushes VisualStatesBrushes
        {
            get { return (VisualStatesBrushes)GetValue(VisualStatesBrushesProperty); }
            set { SetValue(VisualStatesBrushesProperty, value); }
        }

        static LiteButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LiteButton), new FrameworkPropertyMetadata(typeof(LiteButton)));
        }
    }
}