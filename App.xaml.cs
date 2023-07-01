using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Properties;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string[] args = e.Args;
            if (args.Contains("--culture"))
            {
                string culture = "en";
                try
                {
                    int sus = Array.IndexOf(args, "--culture") + 1;
                    culture = args[sus];
                }
                catch (Exception)
                {
                    culture = "en";
                }
                Strings.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
                System.Windows.Forms.MessageBox.Show("Curent culture: " + culture, "Culture changed!");
            }
        }
    }
}
