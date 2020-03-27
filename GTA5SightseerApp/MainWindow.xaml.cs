using GTA5SightseerApp.Controls;
using GTA5SightseerApp.ViewModels;
using GTA5SightseerApp.Views;
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
            //e
            InitializeComponent();
            ViewModel.ResetScreenAnimation = ResetScreenAnimation;
            ViewModel.ShowStatusScreen = ShowYouWinScreen;
            ViewModel.HideStatusScreen = HideYouWinScreen;
            yws = new YouWinScreen("00:00:0000");
            yfs = new YouFailedLol();
            //i dont care if this annoys you xdddddddddddddd
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
        YouWinScreen yws;
        YouFailedLol yfs;
        private void ShowYouWinScreen(int completedOrFailed)
        {
            switch (completedOrFailed)
            {
                case 1:
                    yws = new YouWinScreen(ViewModel.CountdownTimerFormatted);
                    yws.Opacity = 0;
                    MainGrid.Children.Add(yws);
                    yws.AnimateOpacity(0, 1, TimeSpan.FromMilliseconds(320));
                    break;
                case 2:
                    yfs = new YouFailedLol();
                    yfs.Opacity = 0;
                    MainGrid.Children.Add(yfs);
                    yfs.AnimateOpacity(0, 1, TimeSpan.FromMilliseconds(320));
                    break;
            }
        }

        private void HideYouWinScreen(int completedOrFailed)
        {
            if (yws != null || yfs != null)
            {
                switch (completedOrFailed)
                {
                    case 1:
                        yws.AnimateOpacity(1, 0, TimeSpan.FromMilliseconds(400));

                        //dirty but idc lul
                        Task.Run(async () =>
                        {
                            await Task.Delay(420);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MainGrid.Children.Remove(yws);
                            });
                        });
                        break;
                    case 2:
                        yfs.AnimateOpacity(1, 0, TimeSpan.FromMilliseconds(400));

                        //dirty but idc lul
                        Task.Run(async () =>
                        {
                            await Task.Delay(420);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MainGrid.Children.Remove(yfs);
                            });
                        });
                        break;
                }
            }
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
                ViewModel.StopCycle();
                strtBtn.Content = "Start";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.KeyDown(e.Key);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewModel.RestartSlotSelector();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ViewModel.AddWordWindow.Show();
        }

        private void MoveandSelectClick(object sender, RoutedEventArgs e)
        {
            switch(int.Parse(((Button)sender).Uid))
            {
                //i dont care lol this is ezpz lemon squeezy
                case 0: ViewModel.KeyDown(Key.Left); break;
                case 1: ViewModel.KeyDown(Key.Enter); break;
                case 2: ViewModel.KeyDown(Key.Right); break;
            }
        }
    }
}
