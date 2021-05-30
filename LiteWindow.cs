using System;
using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lite.NET
{
    [TemplatePart(Name = "PART_DialogContentPresenter", Type = typeof(ContentPresenter))]
    public class LiteWindow : Window
    {
        private ContentPresenter PART_DialogContentPresenter;

        public object ExpansionArea
        {
            get { return GetValue(ExpansionAreaProperty); }
            set { SetValue(ExpansionAreaProperty, value); }
        }

        public static readonly DependencyProperty ExpansionAreaProperty =
            DependencyProperty.Register("ExpansionArea", typeof(object), typeof(LiteWindow));

        public VisualStatesBrushes VisualStatesBrushes
        {
            get { return (VisualStatesBrushes)GetValue(VisualStatesBrushesProperty); }
            set { SetValue(VisualStatesBrushesProperty, value); }
        }

        public static readonly DependencyProperty VisualStatesBrushesProperty =
            DependencyProperty.RegisterAttached("VisualStatesBrushes", typeof(VisualStatesBrushes), typeof(LiteWindow),
                new FrameworkPropertyMetadata(new VisualStatesBrushes(), FrameworkPropertyMetadataOptions.Inherits));

        public static VisualStatesBrushes GetVisualStatesBrushes(DependencyObject obj)
        {
            return (VisualStatesBrushes)obj.GetValue(VisualStatesBrushesProperty);
        }

        public static void SetVisualStatesBrushes(DependencyObject obj, VisualStatesBrushes value)
        {
            obj.SetValue(VisualStatesBrushesProperty, value);
        }

        static LiteWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LiteWindow), new FrameworkPropertyMetadata(typeof(LiteWindow)));
            CommandManager.RegisterClassCommandBinding(typeof(LiteWindow), new(SystemCommands.CloseWindowCommand,
                CloseWindow_Executed));
            CommandManager.RegisterClassCommandBinding(typeof(LiteWindow), new(SystemCommands.MaximizeWindowCommand,
                MaximizeWindow_Executed, MaximizeWindow_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(LiteWindow), new(SystemCommands.MinimizeWindowCommand,
                MinimizeWindow_Executed));
            CommandManager.RegisterClassCommandBinding(typeof(LiteWindow), new(SystemCommands.RestoreWindowCommand,
                RestoreWindow_Executed, RestoreWindow_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(LiteWindow), new(LiteUICommands.NewDialog, NewDialog_Executed));
        }

        private static void NewDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LiteWindow window = GetWindow(sender as DependencyObject) as LiteWindow;
            Keyboard.ClearFocus();
            switch (e.Parameter)
            {
                case Type:
                    object content = Activator.CreateInstance(e.Parameter as Type);
                    window.PART_DialogContentPresenter.Content = content;
                    break;
                case string:
                    window.PART_DialogContentPresenter.Content = window.FindResource(e.Parameter);
                    break;
                default:
                    window.PART_DialogContentPresenter.Content = e.Parameter;
                    break;
            }
        }

        private static void RestoreWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Window window = sender as Window;
            if (window.WindowState != WindowState.Normal)
                e.CanExecute = true;
        }

        private static void RestoreWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window window = sender as Window;
            SystemCommands.RestoreWindow(window);
        }

        private static void MinimizeWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window window = sender as Window;
            SystemCommands.MinimizeWindow(window);
        }

        private static void MaximizeWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Window window = sender as Window;
            if (window.WindowState != WindowState.Maximized)
                e.CanExecute = true;
        }

        private static void MaximizeWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window window = sender as Window;
            SystemCommands.MaximizeWindow(window);
        }

        private static void CloseWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window window = sender as Window;
            SystemCommands.CloseWindow(window);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.PART_DialogContentPresenter.Content != null)
            {
                e.Cancel = true;
                LiteDialog dialog = this.PART_DialogContentPresenter.Content as LiteDialog;
                dialog?.OnLeave(new RoutedDataEventArgs<DialogButton>(DialogButton.None,LiteDialog.LeaveEvent,this));
                SystemSounds.Asterisk.Play();
            }
            base.OnClosing(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_DialogContentPresenter = GetTemplateChild(nameof(this.PART_DialogContentPresenter)) as ContentPresenter;
        }
    }
}