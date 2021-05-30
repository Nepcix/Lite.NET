using System.Windows.Input;

namespace Lite.NET
{
    public class LiteUICommands
    {
        public static RoutedUICommand NewDialog { get; private set; }

        static LiteUICommands()
        {
            NewDialog = new("NewDialog", "NewDialog", typeof(LiteUICommands));
        }
    }
}