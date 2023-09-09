using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Faenote.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public string Message { get; internal set; }
        public int DefaultIndex { get; internal set; }
        public int CancelIndex { get; internal set; }
        public ICollection<string>? Buttons { get; internal set; }
        public string? ImageName { get; internal set; }
        public string DefaultButtonText { get; internal set; }
        public string CancelButtonText { get; internal set; }
        public string? UserResult { get; internal set; }

        internal void InitializeAndShowDialog()
        {
            throw new NotImplementedException();
        }

        internal void SetButtons(IEnumerable<string>? buttons)
        {
            throw new NotImplementedException();
        }

        internal void SetImage(string? icon)
        {
            throw new NotImplementedException();
        }
    }
}
