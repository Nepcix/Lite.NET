using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lite.NET
{
    public enum DialogButton
    {
        None,
        OK,
        OKCancel,
        No,
        YesNo,
        YesNoCancel
    };

    [TemplatePart(Name = "PART_MaskBorder", Type = typeof(Border))]
    public class LiteDialog : ContentControl
    {
        Border PART_MaskBorder;

        public static readonly RoutedEvent LeaveEvent = EventManager.RegisterRoutedEvent("Leave", RoutingStrategy.Direct,
            typeof(RoutedDataEventHandler<DialogButton>), typeof(LiteDialog));

        public event RoutedDataEventHandler<DialogButton> Leave
        {
            add { AddHandler(LeaveEvent, value); }
            remove { RemoveHandler(LeaveEvent, value); }
        }

        public Brush MaskBrush
        {
            get { return (Brush)GetValue(MaskBrushProperty); }
            set { SetValue(MaskBrushProperty, value); }
        }

        public static readonly DependencyProperty MaskBrushProperty =
            DependencyProperty.Register("MaskBrush", typeof(Brush), typeof(LiteDialog), new PropertyMetadata(Brushes.Transparent));



        public DialogButton DialogButton
        {
            get { return (DialogButton)GetValue(DialogButtonProperty); }
            set { SetValue(DialogButtonProperty, value); }
        }

        public static readonly DependencyProperty DialogButtonProperty =
            DependencyProperty.Register("DialogButton", typeof(DialogButton), typeof(LiteDialog), new PropertyMetadata(DialogButton.None));



        static LiteDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LiteDialog), new FrameworkPropertyMetadata(typeof(LiteDialog)));
            CommandManager.RegisterClassCommandBinding(typeof(LiteDialog), new(LiteUICommands.NewDialog, NewDialog_Executed));
        }

        private static void NewDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is not NET.DialogButton)
                return;
            e.Handled = true;
            LiteDialog dialog = sender as LiteDialog;
            dialog.OnLeave(new RoutedDataEventArgs<DialogButton>((DialogButton)e.Parameter, LeaveEvent, sender));
        }

        public virtual void OnLeave(RoutedDataEventArgs<DialogButton> e)
        {
            RaiseEvent(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_MaskBorder = GetTemplateChild(nameof(this.PART_MaskBorder)) as Border;
            this.PART_MaskBorder.MouseDown += PART_MaskBorder_MouseDown;
        }

        private void PART_MaskBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == sender)
                OnLeave(new RoutedDataEventArgs<DialogButton>(DialogButton.None,LeaveEvent,this));
        }
    }
}