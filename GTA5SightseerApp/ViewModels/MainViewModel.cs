using GTA5SightseerApp.Controls;
using GTA5SightseerApp.Utilities;
using GTA5SightseerApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GTA5SightseerApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private int _countdown;
        public int CountdownTimer // milliseconds
        {
            get => _countdown;
            set
            {
                RaisePropertyChanged(ref _countdown, value);
                TimeSpan time = TimeSpan.FromMilliseconds(value);
                CountdownTimerFormatted = $"{time.Minutes}:{time.Seconds}:{time.Milliseconds}";
            }
        }

        private int _signalStrength;
        public int SignalStrength
        {
            get => _signalStrength;
            set
            {
                RaisePropertyChanged(ref _signalStrength, value);
            }
        }

        private bool _hasFinished;
        public bool HasCompletedSlotMachine
        {
            get => _hasFinished;
            set
            {
                RaisePropertyChanged(ref _hasFinished, value);
            }
        }

        private bool _statusScreenShowing;
        public bool StatusScreenShowing
        {
            get => _statusScreenShowing;
            set
            {
                RaisePropertyChanged(ref _statusScreenShowing, value);
            }
        }

        private bool _isSlotMachineRunning;
        public bool SlotMachineRunning
        {
            get => _isSlotMachineRunning;
            set => RaisePropertyChanged(ref _isSlotMachineRunning, value);
        }

        private int _timerMaxTime;
        public int MaximumTimerTime // seconds
        {
            get => _timerMaxTime;
            set => RaisePropertyChanged(ref _timerMaxTime, value);
        }

        private string _countdownString;
        public string CountdownTimerFormatted
        {
            get => _countdownString;
            set => RaisePropertyChanged(ref _countdownString, value);
        }

        #endregion
        /// <summary>
        /// Allows you to actually close the window lol
        /// binded through MVVM, aka the datacontext from darktheme.xaml
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }
        public SlotMachineViewModel SlotMachine { get; set; }
        public SlotSelectorViewModel SlotSelector { get; set; }
        public DispatcherTimer Timer { get; set; }
        public AddCustomWordsWindow AddWordWindow { get; set; }
        public Action ResetScreenAnimation { get; set; }
        public Action<int> ShowStatusScreen { get; set; }
        public Action<int> HideStatusScreen { get; set; }

        public MainViewModel()
        {
            CloseWindowCommand = new Command(Application.Current.Shutdown);
            SlotMachine = new SlotMachineViewModel();
            SlotSelector = new SlotSelectorViewModel();
            SlotSelector.UpdateSelectedSlotMachineCollection = UpdateSelectorSelectedCollection;
            SlotMachine.MoveSlotSelectorRight = MoveSlotSelectorRight;
            SlotMachine.MoveSlotSelectorToStart = RestartSlotSelector;
            SlotSelector.GetSelectedSlotMachine = SetSlotSelectorSelectedSlotMachine;
            SlotMachineRunning = false;
            SlotSelector.SelectedSlotMachineIndex = 1;
            AddWordWindow = new AddCustomWordsWindow();
            AddWordWindow.AddWordCallback = AddWordToSlotMachine;

            MaximumTimerTime = 120;
            Timer = new DispatcherTimer(DispatcherPriority.Render);
            Timer.Tick += TimerTicked;
        }

        public void StartCycle()
        {
            Timer.Interval = TimeSpan.FromSeconds(0.1);
            SignalStrength = 100;
            HasCompletedSlotMachine = false;
            StatusScreenShowing = false;
            SlotMachine.AllowAllSlotsToCycle();
            //second to millisecond or something idk
            CountdownTimer = MaximumTimerTime * 1000;
            Timer.Start();
            SlotMachineRunning = true;
            //Runs code asynchronously so that delays wont lag the program.
            Task.Run(async () =>
            {
                while (SlotMachineRunning)
                {
                    //taps into main thread because frameworkelements must be "altered"
                    //on the application thread, not another one (idk why)
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (SlotMachine.AllSlotMachinesStopped())
                        {
                            HasCompletedSlotMachine = true;
                            StatusScreenShowing = true;
                            ShowStatusScreen?.Invoke(1);
                            SlotMachine.ResetEverything();
                            SlotSelector.ResetEverything();
                            StopCycle();
                        }

                        if (CountdownTimer == 0)
                        {
                            StatusScreenShowing = true;
                            ShowStatusScreen?.Invoke(2);
                            StopCycle();
                        }

                        SlotMachine.CycleItems(1);
                        SlotMachine.CycleItems(2);
                        SlotMachine.CycleItems(3);
                        SlotMachine.CycleItems(4);
                        SlotMachine.CycleItems(5);
                        SlotMachine.CycleItems(6);
                        SlotMachine.CycleItems(7);
                        SlotMachine.CycleItems(8);
                    });
                    //Delay is every 220ms so that is syncs with the slot machine animation duration.
                    await Task.Delay(220);
                }
            });
        }

        public void StopCycle()
        {
            SlotMachineRunning = false;
            SlotMachine.ResetEverything();
            Timer.Stop();
        }

        public void KeyDown(Key key)
        {
            if (StatusScreenShowing)
            {
                StatusScreenShowing = false;
                HideStatusScreen?.Invoke(1);
                HideStatusScreen?.Invoke(2);

                StartCycle();
            }

            SlotSelector.KeyDown(key);
            if (key == Key.Enter)
                SlotMachine.SetHighlightedSlotMachineItem();
            else if (key == Key.Space)
                SlotMachine.SetHighlightedSlotMachineItem();
        }

        public void MoveSlotSelectorRight() { SlotSelector.MoveSelectorRight(); }
        public void RestartSlotSelector()
        {
            SlotSelector.ResetEverything();
            SlotMachine.ResetEverything();
            ResetScreenAnimation?.Invoke();
            SlotMachine.AllowAllSlotsToCycle();
            SignalStrength -= 20;

            if (SignalStrength == 0)
            {
                StatusScreenShowing = true;
                ShowStatusScreen?.Invoke(2);
                StopCycle();
            }
        }

        public void UpdateSelectorSelectedCollection()
        {
            SlotMachine.UpdateSelectorSelectedCollection(SlotSelector.SelectedSlotMachineIndex);
        }

        public void SetSlotSelectorSelectedSlotMachine()
        {
            SlotSelector.SelectedSlotMachine = SlotMachine.SelectedSlotMachine;
        }

        public void AddWordToSlotMachine(string word)
        {
            SlotMachine.ResetEverything();
            SlotSelector.ResetEverything();
            ResetScreenAnimation?.Invoke();
            SlotMachine.AddWordToSlotMachine(word);
        }

        #region yuk
        // public void SetHighlightedSlotMachineItem()
        // {
        //     //this is okay but inefficient tbh... well it kinda isnt because this gives
        //     //more control over stuff but still
        //     //Point technicallyNotMousePoint = new Point(0, 0);
        //     //switch (SlotSelector.SelectedSlotMachineIndex)
        //     //{
        //     //    case 1: technicallyNotMousePoint = new Point(195, 435); break;
        //     //    case 2: technicallyNotMousePoint = new Point(292, 435); break;
        //     //    case 3: technicallyNotMousePoint = new Point(392, 435); break;
        //     //    case 4: technicallyNotMousePoint = new Point(489, 435); break;
        //     //    case 5: technicallyNotMousePoint = new Point(589, 435); break;
        //     //    case 6: technicallyNotMousePoint = new Point(686, 435); break;
        //     //    case 7: technicallyNotMousePoint = new Point(784, 435); break;
        //     //    case 8: technicallyNotMousePoint = new Point(882, 435); break;
        //     //}
        //
        //     //HitTestResult slotHitControl = VisualTreeHelper.HitTest(Application.Current.MainWindow, technicallyNotMousePoint);
        //     //if (slotHitControl.VisualHit is TextBlock slotItemContent)
        //     //{
        //     //    SlotMachine.StopSlotWithHighlightedItemIn(SlotMachine.HighlightedSlotItem);
        //     //    SlotMachine.HighlightedSlotItem = GetSlotItemFromContentControl(slotItemContent);
        //     //}
        // }

        //public SlotMachineItem GetSlotItemFromContentControl(TextBlock content)
        //{
        //    foreach (SlotMachineItem smi in SlotMachine.SelectedSlotMachine)
        //    {
        //        if (smi.Content == content)
        //            return smi;
        //    }
        //    return null;
        //}
        #endregion

        private void TimerTicked(object sender, EventArgs e)
        {
            if (CountdownTimer >= 0 && SlotMachineRunning)
            {
                CountdownTimer -= Timer.Interval.Milliseconds;
            }
        }
    }
}
