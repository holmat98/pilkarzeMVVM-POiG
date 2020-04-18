using pilkarzeMVVM.Model;
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

namespace pilkarzeMVVM
{
    /// <summary>
    /// Logika interakcji dla klasy PlayerData.xaml
    /// </summary>
    public partial class PlayerData : UserControl
    {
        public PlayerData()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent NumberChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PlayerData));

        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(NumberChangedEvent, value); }
            remove { RemoveHandler(NumberChangedEvent, value); }
        }

        void RaiseNunberChanged()
        {

            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PlayerData.NumberChangedEvent);

            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register(
                    "Text",
                    typeof(string),
                    typeof(PlayerData),
                    new FrameworkPropertyMetadata(null)
                );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseNunberChanged();
        }
    }
}
