using GTA5SightseerApp.Controls;
using GTA5SightseerApp.Utilities;
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

        public Action ResetScreenAnimation { get; set; }

        public MainViewModel()
        {
            CloseWindowCommand = new Command(Application.Current.Shutdown);
            SlotMachine = new SlotMachineViewModel();
            SlotSelector = new SlotSelectorViewModel();
            SlotSelector.UpdateSelectedSlotMachineCollection = UpdateSelectorSelectedCollection;
            SlotMachine.MoveSlotSelectorRight = MoveSlotSelectorRight;
            SlotMachine.MoveSlotSelectorToStart = MoveSlotSelectorToStart;
            SlotSelector.SelectedSlotMachineIndex = 1;
            SlotMachineRunning = false;
            //SlotSelector.UpdateHighlightedSlotMachineItemCallback = SetHighlightedSlotMachineItem;

            MaximumTimerTime = 120;
            Timer = new DispatcherTimer(DispatcherPriority.Render);
            Timer.Tick += TimerTicked;
        }

        public void KeyDown(Key key)
        {
            SlotSelector.KeyDown(key);

            if (key == Key.Enter)
                SetHighlightedSlotMachineItem();
            else if (key == Key.Space)
                SetHighlightedSlotMachineItem();
        }

        public void MoveSlotSelectorRight() { SlotSelector.MoveSelectorRight(); }
        public void MoveSlotSelectorToStart()
        {
            SlotSelector.ResetEverything();
            SlotMachine.ResetEverything();
            ResetScreenAnimation?.Invoke();
            SlotMachine.AllowAllSlotsToCycle();
        }

        public void UpdateSelectorSelectedCollection()
        {
            switch (SlotSelector.SelectedSlotMachineIndex)
            {
                case 1: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn1; break;
                case 2: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn2; break;
                case 3: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn3; break;
                case 4: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn4; break;
                case 5: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn5; break;
                case 6: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn6; break;
                case 7: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn7; break;
                case 8: SlotMachine.SelectedSlotMachine = SlotMachine.SlotMachineColumn8; break;
            }
        }

        public void SetHighlightedSlotMachineItem()
        {
            Point technicallyNotMousePoint = new Point(0, 0);
            switch (SlotSelector.SelectedSlotMachineIndex)
            {
                case 1: technicallyNotMousePoint = new Point(195, 435); break;
                case 2: technicallyNotMousePoint = new Point(292, 435); break;
                case 3: technicallyNotMousePoint = new Point(392, 435); break;
                case 4: technicallyNotMousePoint = new Point(489, 435); break;
                case 5: technicallyNotMousePoint = new Point(589, 435); break;
                case 6: technicallyNotMousePoint = new Point(686, 435); break;
                case 7: technicallyNotMousePoint = new Point(784, 435); break;
                case 8: technicallyNotMousePoint = new Point(882, 435); break;
            }

            HitTestResult slotHitControl = VisualTreeHelper.HitTest(Application.Current.MainWindow, technicallyNotMousePoint);
            if (slotHitControl.VisualHit is TextBlock slotItemContent)
            {
                SlotMachine.StopSlotWithHighlightedItemIn(SlotMachine.HighlightedSlotItem);
                SlotMachine.HighlightedSlotItem = GetSlotItemFromContentControl(slotItemContent);
            }
        }

        public SlotMachineItem GetSlotItemFromContentControl(TextBlock content)
        {
            foreach (SlotMachineItem smi in SlotMachine.SelectedSlotMachine)
            {
                if (smi.Content == content)
                    return smi;
            }
            return null;
        }

        private void TimerTicked(object sender, EventArgs e)
        {
            if (CountdownTimer >= 0)
            {
                CountdownTimer -= Timer.Interval.Milliseconds;
            }
        }

        public void StartCycle()
        {
            Timer.Interval = TimeSpan.FromSeconds(0.1);
            CountdownTimer = MaximumTimerTime * 1000;
            Timer.Start();
            SlotMachineRunning = true;
            //Runs code asynchronously so that delays dont lag the program.
            Task.Run(async() =>
            {
                while (SlotMachineRunning)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SlotMachine.CycleItems(1);
                        SlotMachine.CycleItems(2);
                        SlotMachine.CycleItems(3);
                        SlotMachine.CycleItems(4);
                        SlotMachine.CycleItems(5);
                        SlotMachine.CycleItems(6);
                        SlotMachine.CycleItems(7);
                        SlotMachine.CycleItems(8);
                    });
                    //Delay is every 250ms so that is syncs with the slot machine animation duration.
                    await Task.Delay(250);
                }
            });
        }

        public void StopCycle()
        {
            SlotMachineRunning = false;
            Timer.Stop();
        }
    }
}
