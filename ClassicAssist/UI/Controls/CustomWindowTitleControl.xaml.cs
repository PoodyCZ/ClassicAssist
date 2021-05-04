﻿using System.Windows;
using System.Windows.Input;
using ClassicAssist.Data;
using ClassicAssist.Shared.UI;

namespace ClassicAssist.UI.Controls
{
    /// <summary>
    ///     Interaction logic for CustomWindowTitleControl.xaml
    /// </summary>
    public partial class CustomWindowTitleControl
    {
        public static readonly DependencyProperty AdditionalContentProperty =
            DependencyProperty.Register( "AdditionalContent", typeof( object ), typeof( CustomWindowTitleControl ),
                new PropertyMetadata( null ) );

        public static readonly DependencyProperty AdditionalButtonsProperty =
            DependencyProperty.Register( "AdditionalButtons", typeof( object ), typeof( CustomWindowTitleControl ),
                new PropertyMetadata( null ) );

        public static readonly DependencyProperty CustomTitleProperty = DependencyProperty.Register( "CustomTitle",
            typeof( string ), typeof( CustomWindowTitleControl ),
            new FrameworkPropertyMetadata( default( string ), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty CanCloseProperty = DependencyProperty.Register( "CanClose",
            typeof( bool ), typeof( CustomWindowTitleControl ),
            new FrameworkPropertyMetadata( true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty CanMinimizeProperty = DependencyProperty.Register( "CanMinimize",
            typeof( bool ), typeof( CustomWindowTitleControl ),
            new FrameworkPropertyMetadata( true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        public static readonly DependencyProperty CanMaxmizeProperty = DependencyProperty.Register( "CanMaximize",
            typeof( bool ), typeof( CustomWindowTitleControl ),
            new FrameworkPropertyMetadata( true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );

        private ICommand _maximizeCommand;

        public CustomWindowTitleControl()
        {
            InitializeComponent();
        }

        public object AdditionalButtons
        {
            get => GetValue( AdditionalButtonsProperty );
            set => SetValue( AdditionalButtonsProperty, value );
        }

        public object AdditionalContent
        {
            get => GetValue( AdditionalContentProperty );
            set => SetValue( AdditionalContentProperty, value );
        }

        public bool CanClose
        {
            get => (bool) GetValue( CanCloseProperty );
            set => SetValue( CanCloseProperty, value );
        }

        public bool CanMaximize
        {
            get => (bool) GetValue( CanMaxmizeProperty );
            set => SetValue( CanMaxmizeProperty, value );
        }

        public bool CanMinimize
        {
            get => (bool) GetValue( CanMinimizeProperty );
            set => SetValue( CanMinimizeProperty, value );
        }

        public string CustomTitle
        {
            get => (string) GetValue( CustomTitleProperty );
            set => SetValue( CustomTitleProperty, value );
        }

        public ICommand MaximizeCommand =>
            _maximizeCommand ?? ( _maximizeCommand = new RelayCommand( Maximize, o => true ) );

        private static void Maximize( object obj )
        {
            if ( !( obj is UIElement element ) )
            {
                return;
            }

            Window window = Window.GetWindow( element );

            if ( window == null )
            {
                return;
            }

            window.WindowState = _savedState =
                window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private ICommand _minimizeCommand;
        private static WindowState _savedState = WindowState.Normal;
        public ICommand MinimizeCommand =>
            _minimizeCommand ?? (_minimizeCommand = new RelayCommand(Minimize, o => true));

        private static void Minimize(object obj)
        {
            if (!(obj is UIElement element))
            {
                return;
            }

            Window window = Window.GetWindow(element);

            if (window == null)
            {
                return;
            }

            if (Options.CurrentOptions.SysTray)
            {
                window.Hide();
            }
            else
            {
                window.WindowState = window.WindowState == WindowState.Maximized || window.WindowState == WindowState.Normal ? WindowState.Minimized : _savedState;
            }
        }
    }
}