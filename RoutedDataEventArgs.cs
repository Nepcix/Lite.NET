using System.Windows;

namespace Lite.NET
{
    public delegate void RoutedDataEventHandler<T>(object sender, RoutedDataEventArgs<T> e);

    public class RoutedDataEventArgs<T> : RoutedEventArgs
    {
        public T Data { get; }

        public RoutedDataEventArgs(T data, RoutedEvent routedEvent, object source)
        {
            Data = data;
            RoutedEvent = routedEvent;
            Source = source;
        }
    }
}