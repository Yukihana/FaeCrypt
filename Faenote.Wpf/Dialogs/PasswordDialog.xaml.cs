using System.Windows;

namespace Faenote.Wpf.Dialogs;

/// <summary>
/// Interaction logic for PasswordDialog.xaml
/// </summary>
public partial class PasswordDialog : Window
{
    public PasswordDialog()
        => InitializeComponent();

    public void SetTitle(string title)
        => Title = title;

    private void Window_Loaded(object sender, RoutedEventArgs e)
        => DialogResult = false;

    public string Password { get; set; } = string.Empty;
    public string Message { get; internal set; }

    private void Accept_Click(object sender, RoutedEventArgs e)
    {
        Password = PassBox.Password;
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
        => Close();
}