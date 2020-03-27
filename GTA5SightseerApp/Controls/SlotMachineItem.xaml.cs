using GTA5SightseerApp.Utilities;
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

namespace GTA5SightseerApp.Controls
{
    /// <summary>
    /// Interaction logic for SlotMachineItem.xaml
    /// </summary>
    public partial class SlotMachineItem : UserControl
    {
        public SlotMachineItemViewModel ViewModel { get; set; }
        public SlotMachineItem()
        {
            InitializeComponent();
            ViewModel = new SlotMachineItemViewModel();
            DataContext = ViewModel;
        }
    }

    public class SlotMachineItemViewModel : BaseViewModel
    {
        private string _contentText;
        public string ContentText
        {
            get => _contentText;
            set => RaisePropertyChanged(ref _contentText, value); 
        }

        private Brush _contentForeground;
        public Brush ContentForeground
        {
            get => _contentForeground;
            set => RaisePropertyChanged(ref _contentForeground, value);
        }

        private bool _isAnswerItem;
        public bool IsAnswerItem
        {
            get => _isAnswerItem;
            set => RaisePropertyChanged(ref _isAnswerItem, value);
        }
    }
}
