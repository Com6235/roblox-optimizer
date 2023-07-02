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
using WpfApp1.Properties;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {
        private static int CLcounter;

        public About()
        {
            InitializeComponent();
            aboutText.Text = Strings.aboutText;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CLcounter++;
            switch (CLcounter)
            {
                case 1:
                    aboutText.Text = "";
                    label.Content = "I";
                    break;
                case 2:
                    label.Content = "IT";
                    break;
                case 3:
                    label.Content = "IT\'";
                    break;
                case 4:
                    label.Content = "IT\'S";
                    break;
                case 5:
                    label.Content = "IT\'S ";
                    break;
                case 6:
                    label.Content = "IT\'S M";
                    break;
                case 7:
                    label.Content = "IT\'S ME";
                    break;
                case 8:
                    aboutWindow.Close();
                    break;
            }
        }
    }
}
