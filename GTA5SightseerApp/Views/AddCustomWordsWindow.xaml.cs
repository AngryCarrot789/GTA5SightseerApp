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

namespace GTA5SightseerApp.Views
{
    /// <summary>
    /// Interaction logic for AddCustomWordsWindow.xaml
    /// </summary>
    public partial class AddCustomWordsWindow : Window
    {
        public AddCustomWordsWindow()
        {
            InitializeComponent();
            t1.Focus();
        }

        public Action<string> AddWordCallback { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
            t6.Text = "";
            t7.Text = "";
            t8.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddWord();
        }

        public void AddWord()
        {
            string word = t1.Text +
                t2.Text +
                t3.Text +
                t4.Text +
                t5.Text +
                t6.Text +
                t7.Text +
                t8.Text;

            word.Trim();
            int len = word.Length;
            for (int i = 0; i < 8 - len; i++)
            {
                word += ".";
            }

            AddWordCallback?.Invoke(word);

            this.Hide();
        }

        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (t1.IsFocused) { t1.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t2.Focus(); }
                else if (t2.IsFocused) { t2.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t3.Focus(); }
                else if (t3.IsFocused) { t3.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t4.Focus(); }
                else if (t4.IsFocused) { t4.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t5.Focus(); }
                else if (t5.IsFocused) { t5.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t6.Focus(); }
                else if (t6.IsFocused) { t6.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t7.Focus(); }
                else if (t7.IsFocused) { t7.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); t8.Focus(); }
                else if (t8.IsFocused) { t8.Text = char.ToUpper(Enum.GetName(typeof(Key), e.Key)[0]).ToString(); }
            }
            catch { }

            if (e.Key == Key.Enter)
                AddWord();
            if (e.Key == Key.Escape)
                AddWord();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
