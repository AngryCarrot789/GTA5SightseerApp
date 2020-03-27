using GTA5SightseerApp.Controls;
using GTA5SightseerApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GTA5SightseerApp.ViewModels
{
    /// <summary>
    /// A viewmodel containing all interactions of the slot machine thingy. Adding/Removing, etc
    /// </summary>
    public class SlotMachineViewModel : BaseViewModel
    {
        private ObservableCollection<SlotMachineItem> _slotMachineColumn1 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn2 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn3 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn4 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn5 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn6 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn7 = new ObservableCollection<SlotMachineItem>();
        private ObservableCollection<SlotMachineItem> _slotMachineColumn8 = new ObservableCollection<SlotMachineItem>();

        public ObservableCollection<SlotMachineItem> SlotMachineColumn1 { get => _slotMachineColumn1; set => RaisePropertyChanged(ref _slotMachineColumn1, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn2 { get => _slotMachineColumn2; set => RaisePropertyChanged(ref _slotMachineColumn2, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn3 { get => _slotMachineColumn3; set => RaisePropertyChanged(ref _slotMachineColumn3, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn4 { get => _slotMachineColumn4; set => RaisePropertyChanged(ref _slotMachineColumn4, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn5 { get => _slotMachineColumn5; set => RaisePropertyChanged(ref _slotMachineColumn5, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn6 { get => _slotMachineColumn6; set => RaisePropertyChanged(ref _slotMachineColumn6, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn7 { get => _slotMachineColumn7; set => RaisePropertyChanged(ref _slotMachineColumn7, value); }
        public ObservableCollection<SlotMachineItem> SlotMachineColumn8 { get => _slotMachineColumn8; set => RaisePropertyChanged(ref _slotMachineColumn8, value); }

        public string CurrentWord { get; set; }

        private ObservableCollection<SlotMachineItem> _selectedSlotMachine = new ObservableCollection<SlotMachineItem>();
        public ObservableCollection<SlotMachineItem> SelectedSlotMachine
        {
            get => _selectedSlotMachine;
            set => RaisePropertyChanged(ref _selectedSlotMachine, value);
        }

        // The item within the blue bar thingy. idk if highlighted is the right word but i dont give a big fat flying alien fucc lol
        private SlotMachineItem _highlightedItem = new SlotMachineItem();
        public SlotMachineItem HighlightedSlotItem
        {
            get => _highlightedItem;
            set
            {
                RaisePropertyChanged(ref _highlightedItem, value);
                HighlightSelectedItem(value);
            }
        }

        //jesus can i use short method names for once xd
        public Action GetSelectedSlotMachineCollectionCallback { get; set; }
        public Action MoveSlotSelectorRight { get; set; }
        public Action MoveSlotSelectorToStart { get; set; }

        private bool _slotMachine1AllowToCycle;
        private bool _slotMachine2AllowToCycle;
        private bool _slotMachine3AllowToCycle;
        private bool _slotMachine4AllowToCycle;
        private bool _slotMachine5AllowToCycle;
        private bool _slotMachine6AllowToCycle;
        private bool _slotMachine7AllowToCycle;
        private bool _slotMachine8AllowToCycle;

        public bool SlotMachine1AllowedToCycle { get => _slotMachine1AllowToCycle; set => RaisePropertyChanged(ref _slotMachine1AllowToCycle, value); }
        public bool SlotMachine2AllowedToCycle { get => _slotMachine2AllowToCycle; set => RaisePropertyChanged(ref _slotMachine2AllowToCycle, value); }
        public bool SlotMachine3AllowedToCycle { get => _slotMachine3AllowToCycle; set => RaisePropertyChanged(ref _slotMachine3AllowToCycle, value); }
        public bool SlotMachine4AllowedToCycle { get => _slotMachine4AllowToCycle; set => RaisePropertyChanged(ref _slotMachine4AllowToCycle, value); }
        public bool SlotMachine5AllowedToCycle { get => _slotMachine5AllowToCycle; set => RaisePropertyChanged(ref _slotMachine5AllowToCycle, value); }
        public bool SlotMachine6AllowedToCycle { get => _slotMachine6AllowToCycle; set => RaisePropertyChanged(ref _slotMachine6AllowToCycle, value); }
        public bool SlotMachine7AllowedToCycle { get => _slotMachine7AllowToCycle; set => RaisePropertyChanged(ref _slotMachine7AllowToCycle, value); }
        public bool SlotMachine8AllowedToCycle { get => _slotMachine8AllowToCycle; set => RaisePropertyChanged(ref _slotMachine8AllowToCycle, value); }

        public SlotMachineViewModel()
        {
            AllowAllSlotsToCycle();
            CurrentWord = "AMILKMAN";
            AddWordToSlotMachine(CurrentWord);
        }

        public void AddWordToSlotMachine(string word)
        {
            ClearSlotMachine();
            CurrentWord = word;
            AddRandomNonAnswerToSlotMachine(word);
            AddAnswerWordsToSlotMachine(word);
        }

        public void AddRandomNonAnswerToSlotMachine(string answer)
        {
            for (int i = 1; i <= 8; i++)
            {
                string letter = answer[i-1].ToString();
                AddRandomLettersToSlotMachineExcludingGivenLetter(i, letter);
            }
        }
        public void AddAnswerWordsToSlotMachine(string answer)
        {
            for (int i = 1; i <= answer.Length; i++)
            {
                string letter = answer[i-1].ToString();
                InsertAnswerLetterRandomlyIntoSlotMachine(i, letter.ToString());
            }
        }

        public void HighlightSelectedItem(SlotMachineItem smi)
        {
            if (smi != null)
            {
                if (smi.ViewModel.IsAnswerItem)
                {
                    smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Green);
                    MoveSlotSelectorRight?.Invoke();
                }
                else
                {
                    //smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Blue);
                    MoveSlotSelectorToStart?.Invoke();
                    UnHighlightAllItems();
                }
            }
        }

        public void UnHighlightAllItems()
        {
            foreach (SlotMachineItem smi in SlotMachineColumn1) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn2) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn3) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn4) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn5) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn6) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn7) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            foreach (SlotMachineItem smi in SlotMachineColumn8) if (smi.ViewModel.IsAnswerItem) smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
        }

        public void StopSlotWithHighlightedItemIn(SlotMachineItem smi)
        {
            if (SelectedSlotMachine == SlotMachineColumn1) SlotMachine1AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn2) SlotMachine2AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn3) SlotMachine3AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn4) SlotMachine4AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn5) SlotMachine5AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn6) SlotMachine6AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn7) SlotMachine7AllowedToCycle = false;
            else if (SelectedSlotMachine == SlotMachineColumn8) SlotMachine8AllowedToCycle = false;
        }

        public void AllowAllSlotsToCycle()
        {
            SlotMachine1AllowedToCycle = true;
            SlotMachine2AllowedToCycle = true;
            SlotMachine3AllowedToCycle = true;
            SlotMachine4AllowedToCycle = true;
            SlotMachine5AllowedToCycle = true;
            SlotMachine6AllowedToCycle = true;
            SlotMachine7AllowedToCycle = true;
            SlotMachine8AllowedToCycle = true;
        }

        public bool AllSlotMachinesStopped()
        {
            return !SlotMachine1AllowedToCycle &&
                   !SlotMachine2AllowedToCycle &&
                   !SlotMachine3AllowedToCycle &&
                   !SlotMachine4AllowedToCycle &&
                   !SlotMachine5AllowedToCycle &&
                   !SlotMachine6AllowedToCycle &&
                   !SlotMachine7AllowedToCycle &&
                   !SlotMachine8AllowedToCycle;
        }

        public void ResetEverything()
        {
            ClearSlotMachine();
            AllowAllSlotsToCycle();
            AddWordToSlotMachine(CurrentWord);
        }

        public void ClearSlotMachine()
        {
            SlotMachineColumn1.Clear();
            SlotMachineColumn2.Clear();
            SlotMachineColumn3.Clear();
            SlotMachineColumn4.Clear();
            SlotMachineColumn5.Clear();
            SlotMachineColumn6.Clear();
            SlotMachineColumn7.Clear();
            SlotMachineColumn8.Clear();
        }

        public void UpdateSelectorSelectedCollection(int selectorIndex)
        {
            switch (selectorIndex)
            {
                case 1: SelectedSlotMachine = SlotMachineColumn1; break;
                case 2: SelectedSlotMachine = SlotMachineColumn2; break;
                case 3: SelectedSlotMachine = SlotMachineColumn3; break;
                case 4: SelectedSlotMachine = SlotMachineColumn4; break;
                case 5: SelectedSlotMachine = SlotMachineColumn5; break;
                case 6: SelectedSlotMachine = SlotMachineColumn6; break;
                case 7: SelectedSlotMachine = SlotMachineColumn7; break;
                case 8: SelectedSlotMachine = SlotMachineColumn8; break;
            }
        }

        public void SetHighlightedSlotMachineItem()
        {
            StopSlotWithHighlightedItemIn(HighlightedSlotItem);
            HighlightedSlotItem = SelectedSlotMachine[3];
        }

        private Random RandNumber = new Random();
        private string ExcludedLetter;
        public void AddRandomLettersToSlotMachineExcludingGivenLetter(int slotMachine, string excludedLetter, int numberOfLetters = 8)
        {
            ExcludedLetter += excludedLetter;
            for (int i = 0; i < numberOfLetters; i++)
            {
                int excludedCharLoopCount = 0;
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string str = alphabet[RandNumber.Next(0, 26)].ToString();
                while (ExcludedLetter.Contains(str))
                {
                    if (excludedCharLoopCount > 100)
                        break;
                    str = alphabet[RandNumber.Next(0, 26)].ToString();
                    excludedCharLoopCount++;
                }
                excludedCharLoopCount = 0;
                SlotMachineItem smi = new SlotMachineItem();
                smi.ViewModel.ContentText = str;
                smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.White);
                AddItem(slotMachine, smi);
                ExcludedLetter += str;
            }
            ExcludedLetter = "";
        }

        Random RandNumb2 = new Random();

        public void InsertAnswerLetterRandomlyIntoSlotMachine(int slotMachine, string letter)
        {
            SlotMachineItem smi = new SlotMachineItem();
            smi.ViewModel.ContentText = letter;
            smi.ViewModel.ContentForeground = new SolidColorBrush(Colors.Red);
            smi.ViewModel.IsAnswerItem = true;
            switch (slotMachine)
            {
                //mixin' it up with different rands ;))))) probably does nothing.. shut up i dont care
                case 1: SlotMachineColumn1.Insert(RandNumber.Next(0, SlotMachineColumn1.Count()), smi); break;
                case 2: SlotMachineColumn2.Insert(RandNumb2.Next(0, SlotMachineColumn2.Count()), smi); break;
                case 3: SlotMachineColumn3.Insert(RandNumber.Next(0, SlotMachineColumn3.Count()), smi); break;
                case 4: SlotMachineColumn4.Insert(RandNumber.Next(0, SlotMachineColumn4.Count()), smi); break;
                case 5: SlotMachineColumn5.Insert(RandNumber.Next(0, SlotMachineColumn5.Count()), smi); break;//quad5914 xd
                case 6: SlotMachineColumn6.Insert(RandNumb2.Next(0, SlotMachineColumn6.Count()), smi); break;
                case 7: SlotMachineColumn7.Insert(RandNumb2.Next(0, SlotMachineColumn7.Count()), smi); break;
                case 8: SlotMachineColumn8.Insert(RandNumber.Next(0, SlotMachineColumn8.Count()), smi); break;
            }
        }

        // Moving items from the top to bottom
        // cant think of a better name
        public void MoveItemFromBottomToTopOfList(int slotMachine)
        {
            try
            {
                switch (slotMachine)
                {
                    case 1: SlotMachineColumn1.Move(SlotMachineColumn1.Count - 1, 0); break;
                    case 2: SlotMachineColumn2.Move(SlotMachineColumn2.Count - 1, 0); break;
                    case 3: SlotMachineColumn3.Move(SlotMachineColumn3.Count - 1, 0); break;
                    case 4: SlotMachineColumn4.Move(SlotMachineColumn4.Count - 1, 0); break;
                    case 5: SlotMachineColumn5.Move(SlotMachineColumn5.Count - 1, 0); break;
                    case 6: SlotMachineColumn6.Move(SlotMachineColumn6.Count - 1, 0); break;
                    case 7: SlotMachineColumn7.Move(SlotMachineColumn7.Count - 1, 0); break;
                    case 8: SlotMachineColumn8.Move(SlotMachineColumn8.Count - 1, 0); break;
                }
            }
            catch { }
        }

        // Spin-sort-of a slot machine. Aka, move every item down 1

        public void CycleItems(int slotMachine)
        {
            switch (slotMachine)
            {
                case 1: if (SlotMachine1AllowedToCycle) MoveItemFromBottomToTopOfList(1); break;
                case 2: if (SlotMachine2AllowedToCycle) MoveItemFromBottomToTopOfList(2); break;
                case 3: if (SlotMachine3AllowedToCycle) MoveItemFromBottomToTopOfList(3); break;
                case 4: if (SlotMachine4AllowedToCycle) MoveItemFromBottomToTopOfList(4); break;
                case 5: if (SlotMachine5AllowedToCycle) MoveItemFromBottomToTopOfList(5); break;
                case 6: if (SlotMachine6AllowedToCycle) MoveItemFromBottomToTopOfList(6); break;
                case 7: if (SlotMachine7AllowedToCycle) MoveItemFromBottomToTopOfList(7); break;
                case 8: if (SlotMachine8AllowedToCycle) MoveItemFromBottomToTopOfList(8); break;
            }
        }

        // Adding Items (not really needed but oh well)
        public void AddItem(int slotMachine, SlotMachineItem item)
        {
            switch (slotMachine)
            {
                case 1: SlotMachineColumn1.Add(item); break;
                case 2: SlotMachineColumn2.Add(item); break;
                case 3: SlotMachineColumn3.Add(item); break;
                case 4: SlotMachineColumn4.Add(item); break;
                case 5: SlotMachineColumn5.Add(item); break;
                case 6: SlotMachineColumn6.Add(item); break;
                case 7: SlotMachineColumn7.Add(item); break;
                case 8: SlotMachineColumn8.Add(item); break;
            }
        }

        //Removing Items (not really needed as well... oh well xd)
        public void RemoveItem(int slotMachine, SlotMachineItem item)
        {
            switch (slotMachine)
            {
                case 1: SlotMachineColumn1.Remove(item); break;
                case 2: SlotMachineColumn2.Remove(item); break;
                case 3: SlotMachineColumn3.Remove(item); break;
                case 4: SlotMachineColumn4.Remove(item); break;
                case 5: SlotMachineColumn5.Remove(item); break;
                case 6: SlotMachineColumn6.Remove(item); break;
                case 7: SlotMachineColumn7.Remove(item); break;
                case 8: SlotMachineColumn8.Remove(item); break;
            }
        }
    }
}
