using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rapporteur.Function;

namespace Rapporteur.Parameter;

public partial class Parameter
{
    private static MainWindow Previous { get; set; } = null!;

    public Parameter(MainWindow previous)
    {
        Previous = previous;
        InitializeComponent();
        
        Initialize();
    }

    private void Initialize()
    {
        var keys = Setting.ReadAllSettings();
        TextBoxStep.Text = keys.Step.ToString();
        TextBoxSize.Text = keys.Size.ToString();
        TextBoxLeftKey.Text = ((Key)keys.LeftKey).ToString();
        TextBoxUpKey.Text = ((Key)keys.UpKey).ToString();
        TextBoxRightKey.Text = ((Key)keys.RightKey).ToString();
        TextBoxDownKey.Text = ((Key)keys.DownKey).ToString();
    }
    
    private void ButtonQuit_OnClick(object sender, RoutedEventArgs e)
    {
        Previous.Close();
    }

    private void TextBoxKey_OnKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = (TextBox)sender;
        textBox.Text = e.Key.ToString();
    }

    private void ButtonUpdateKey_OnClick(object sender, RoutedEventArgs e)
    {
        var keys = new SSetting
        {
            Step = Convert.ToInt32(TextBoxStep.Text),
            Size = Convert.ToInt32(TextBoxSize.Text),
            LeftKey = (int)ConvertKey(TextBoxLeftKey.Text),
            UpKey = (int)ConvertKey(TextBoxUpKey.Text),
            RightKey = (int)ConvertKey(TextBoxRightKey.Text),
            DownKey = (int)ConvertKey(TextBoxDownKey.Text)
        };

        Previous.InitKeys(keys);
        Setting.InitSettings(keys);
    }
    
    private static Key ConvertKey(string key) => (Key)Enum.Parse(typeof(Key), key);
}