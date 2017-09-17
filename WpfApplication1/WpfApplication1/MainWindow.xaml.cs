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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button _myButton;
        private Label _resultLabel;
        private TextBox _inputTextBox;
        public MainWindow()
        {
            InitializeComponent();

            _myButton = new Button
            {
                Content = @"Process data",
                Margin = new Thickness(0,100, 0, 0)
            };
            _myButton.Click += Process_Click;

            _inputTextBox = new TextBox();
            _resultLabel = new Label
            {
                Content = "",
                Margin = new Thickness(0,100, 0, 0)
            };
            var panel = new StackPanel();
            panel.Children.Add(_inputTextBox);
            panel.Children.Add(_myButton);
            panel.Children.Add(_resultLabel);
            Content = panel;
        }

        void Process_Click(object sender, EventArgs e)
        {
            _resultLabel.Content =
                         string.Format("Your data is: {0}", _inputTextBox.Text);
        }
    }
}
