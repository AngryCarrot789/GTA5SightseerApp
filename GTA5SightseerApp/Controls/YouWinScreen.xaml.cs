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

namespace GTA5SightseerApp.Controls
{
    /// <summary>
    /// Interaction logic for YouWinScreen.xaml
    /// </summary>
    public partial class YouWinScreen : UserControl
    {
        public YouWinScreen(string timeFormatted)
        {
            InitializeComponent();
            timeStamp.Text = timeFormatted;
        }

        public void AnimateOpacity(int from, int to, TimeSpan duration)
        {
            DoubleAnimation da = new DoubleAnimation(from, to, duration);

            this.BeginAnimation(OpacityProperty, da);
        }
    }
}
