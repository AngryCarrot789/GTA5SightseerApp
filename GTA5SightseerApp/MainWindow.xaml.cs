using GTA5SightseerApp.ViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTA5SightseerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get => DataContext as MainViewModel; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel.ResetScreenAnimation = ResetScreenAnimation;
            //i dont care if this annoys you, fucnk you
            Button_Click(null, null);
        }

        private void ResetScreenAnimation()
        {
            Border ResetScreenBorder = new Border();
            ResetScreenBorder.Background = new SolidColorBrush(Colors.Black);
            MainGrid.Children.Add(ResetScreenBorder);

            //ResetScreenBorder.Visibility = Visibility.Visible;
            DoubleAnimation da1 = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            DoubleAnimation da2 = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(400));
            
            ResetScreenBorder.BeginAnimation(OpacityProperty, da1);
            ResetScreenBorder.BeginAnimation(OpacityProperty, da2);

            Task.Run(async() =>
            {
                await Task.Delay(600);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainGrid.Children.Remove(ResetScreenBorder);//quad5914 xdddd
                });
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.SlotMachineRunning)
            {
                ViewModel.StartCycle();
                strtBtn.Content = "Stop";
             }
            else
            {
                ViewModel.SlotMachineRunning = false;
                strtBtn.Content = "Start";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.KeyDown(e.Key);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewModel.MoveSlotSelectorToStart();
        }
    }
}
