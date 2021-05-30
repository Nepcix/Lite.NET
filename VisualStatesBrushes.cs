using System.Windows.Media;

namespace Lite.NET
{
    public class VisualStatesBrushes
    {
        public Brush MouseOverFore { get; set; } = Brushes.Black;
        public Brush MouseOverBack { get; set; } = Brushes.LightGray;
        public Brush MouseOverBorder { get; set; } = Brushes.DodgerBlue;

        public Brush PressedFore { get; set; } = Brushes.White;
        public Brush PressedBack { get; set; } = Brushes.DodgerBlue;
        public Brush PressedBorder { get; set; } = Brushes.DodgerBlue;

        public Brush DisabledFore { get; set; } = Brushes.Black;
        public Brush DisabledBack { get; set; } = Brushes.LightGray;
        public Brush DisabledBorder { get; set; } = Brushes.Gray;
    }
}