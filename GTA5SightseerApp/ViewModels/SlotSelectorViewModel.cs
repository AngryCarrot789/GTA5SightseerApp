using GTA5SightseerApp.Controls;
using GTA5SightseerApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GTA5SightseerApp.ViewModels
{
    /// <summary>
    /// A ViewModel for the selecting of different slots in the slot machine (as in the green and blue bars).
    /// <see cref="MainViewModel"/> can junction between this and the other one.
    /// </summary>
    public class SlotSelectorViewModel : BaseViewModel
    {
        #region stuf
        //qu
        //ad5
        //9
        //14
        //xdddddddddddddddddddddddddddddddddddddddd
        //if u nick this and say i didnt maek it then fuq
        //lul

        private Visibility _slot1;
        private Visibility _slot2;
        private Visibility _slot3;
        private Visibility _slot4;
        private Visibility _slot5;
        private Visibility _slot6;
        private Visibility _slot7;
        private Visibility _slot8;

        public Visibility Slot1Visibility { get => _slot1; set => RaisePropertyChanged(ref _slot1, value); }
        public Visibility Slot2Visibility { get => _slot2; set => RaisePropertyChanged(ref _slot2, value); }
        public Visibility Slot3Visibility { get => _slot3; set => RaisePropertyChanged(ref _slot3, value); }
        public Visibility Slot4Visibility { get => _slot4; set => RaisePropertyChanged(ref _slot4, value); }
        public Visibility Slot5Visibility { get => _slot5; set => RaisePropertyChanged(ref _slot5, value); }
        public Visibility Slot6Visibility { get => _slot6; set => RaisePropertyChanged(ref _slot6, value); }
        public Visibility Slot7Visibility { get => _slot7; set => RaisePropertyChanged(ref _slot7, value); }
        public Visibility Slot8Visibility { get => _slot8; set => RaisePropertyChanged(ref _slot8, value); }

        public Action UpdateSelectedSlotMachineCollection { get; set; }
        public Action GetSelectedSlotMachine { get; set; }

        private ObservableCollection<SlotMachineItem> _selectedSlotMachine = new ObservableCollection<SlotMachineItem>();
        public ObservableCollection<SlotMachineItem> SelectedSlotMachine
        {
            get => _selectedSlotMachine;
            set => RaisePropertyChanged(ref _selectedSlotMachine, value);
        }

        // idk wont use... or might.... idk
        // > life in a nutshell
        //decided i will use xd
        // NOT treated as a proper FrameworkElement Collection;
        // index starts at 1 up to... well 8 for this
        private int _selectedSlotMachineIndex;
        public int SelectedSlotMachineIndex
        {
            get => _selectedSlotMachineIndex;
            set
            {
                RaisePropertyChanged(ref _selectedSlotMachineIndex, value);
                //auto-do this because yes, saves code
                SetSelectedSlotMachine(value);
            }
        }

        #endregion

        public SlotSelectorViewModel()
        {
            SetAllSlotsHidden();
        }

        #region Arrow Key Methods and stuff

        public void KeyDown(Key kii)
        {
            if (kii == Key.Left)
                MoveSelectorLeft();
            else if (kii == Key.Right)
                MoveSelectorRight();
        }

        public void MoveSelectorLeft()
        {
            if (SelectedSlotMachineIndex > 1)
            {
                SelectedSlotMachineIndex -= 1;
            }
        }

        public void MoveSelectorRight()
        {
            if (SelectedSlotMachineIndex < 8) //thers 8 slots soo...
            {
                SelectedSlotMachineIndex += 1;
            }
            else
            {
                SelectedSlotMachineIndex = 1;
            }
        }

        public void MoveSelectorToStart()
        {
            SelectedSlotMachineIndex = 1;
        }

        #endregion

        public void ResetEverything()
        {
            MoveSelectorToStart();
        }

        public void SetSelectedSlotMachine(int selectedSlotMachineIndex)
        {
            UpdateSelectedSlotVisibility(selectedSlotMachineIndex);
            UpdateSelectedSlotMachineCollection?.Invoke();
        }

        public void UpdateSelectedSlotVisibility(int selectedIndex)
        {
            SetAllSlotsHidden();
            switch (selectedIndex)
            {
                case 1: Slot1Visibility = Visibility.Visible; break;
                case 2: Slot2Visibility = Visibility.Visible; break;
                case 3: Slot3Visibility = Visibility.Visible; break;
                case 4: Slot4Visibility = Visibility.Visible; break;
                case 5: Slot5Visibility = Visibility.Visible; break;
                case 6: Slot6Visibility = Visibility.Visible; break;
                case 7: Slot7Visibility = Visibility.Visible; break;
                case 8: Slot8Visibility = Visibility.Visible; break;
            }
        }

        public void UpdateHightlightedSlotItem()
        {

        }

        public void SetAllSlotsHidden()
        {
            Slot1Visibility = Visibility.Hidden;
            Slot2Visibility = Visibility.Hidden;
            Slot3Visibility = Visibility.Hidden;
            Slot4Visibility = Visibility.Hidden;
            Slot5Visibility = Visibility.Hidden;
            Slot6Visibility = Visibility.Hidden;
            Slot7Visibility = Visibility.Hidden;
            Slot8Visibility = Visibility.Hidden;
        }
    }
}
