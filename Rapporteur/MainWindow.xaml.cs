using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Rapporteur.Function;

namespace Rapporteur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static Parameter.Parameter? ParameterWindow { get; set; }

        private static int Angle { get; set; }
        private static int Step { get; set; }
        private static int Size { get; set; }
        private int LeftKey { get; set; }
        private int UpKey { get; set; }
        private int RightKey { get; set; }
        private int DownKey { get; set; }
        
        
        public MainWindow()
        {
            InitKeys();
            
            InitializeComponent();

            ChangeSize();

            KeyDown += OnKeyDown;
        }

        public void InitKeys(SSetting? setting=null)
        {
            var keys = setting ?? Setting.ReadAllSettings();
            Step = keys.Step;
            Size = keys.Size;
            LeftKey = keys.LeftKey;
            UpKey = keys.UpKey;
            RightKey = keys.RightKey;
            DownKey = keys.DownKey;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var keyPressed = (int)e.Key;

            if (keyPressed.Equals(LeftKey))
            {
                Angle -= Step;
                Rotate();
            }
            else if (keyPressed.Equals(RightKey))
            {
                Angle += Step;
                Rotate();
            }
            else if (keyPressed.Equals(UpKey))
            {
                Size += Step;
                ChangeSize();
            }
            else if (keyPressed.Equals(DownKey))
            {
                Size -= Step;
                ChangeSize();
            }
        }

        private void Rotate()
        {
            var rotate = new RotateTransform(Angle, Image.ActualHeight/2, Image.ActualWidth/2) ;
            Image.RenderTransform = rotate;
        }

        private void ChangeSize()
        {
            Height = Size;
            Width = Size;
        }
        
        private void Image_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    DragMove();
                    break;
                case MouseButton.Right:
                case MouseButton.Middle:
                case MouseButton.XButton1:
                case MouseButton.XButton2:
                default:
                    break;
            }
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e) => ParameterWindow?.Close();

        private void MenuItemQuit_OnClick(object sender, RoutedEventArgs e) => Close();


        private void MenuItemparameters_OnClick(object sender, RoutedEventArgs e)
        {
            ParameterWindow = new Parameter.Parameter(this);
            ParameterWindow.Show();
        }
    }
}