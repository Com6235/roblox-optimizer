using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp1.Properties;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool enableOptimizer = false;
        public string settings = Properties.Resources.settings;

        public MainWindow()
        {
            InitializeComponent();
            label1.Content = "";
            button1.Content = Strings.apply;
            checkbox1.Content = Strings.enableOptimizer;
            aboutButton.Content = Strings.about;
            new About().aboutText.Text = Strings.aboutText;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableOptimizer = (bool)checkbox1.IsChecked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enableOptimizer == true)
            {
                string mainDirPath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Roblox\\Versions";
                DirectoryInfo mainDir;
                DirectoryInfo[] subDirs;
                try
                {
                    mainDir = new DirectoryInfo(mainDirPath);
                    subDirs = mainDir.GetDirectories();
                }
                catch (DirectoryNotFoundException)
                {
                    label1.Content = Strings.notInstalled;
                    return;
                }

                DateTime lastCreated = DateTime.MinValue;
                DirectoryInfo lastCreatedDir = null;

                label1.Content = Strings.findingDir;

                foreach (DirectoryInfo subDir in subDirs)
                {
                    if (subDir.CreationTime > lastCreated)
                    {
                        lastCreated = subDir.CreationTime;
                        lastCreatedDir = subDir;
                    }
                }

                if (lastCreatedDir != null)
                {
                    Console.WriteLine("Last created directory: " + lastCreatedDir.FullName);
                }

                List<string> subDirsOfLastCreatedDir = new List<string>();
                foreach (System.IO.DirectoryInfo dir in lastCreatedDir.EnumerateDirectories())
                {
                    subDirsOfLastCreatedDir.Add(dir.Name);
                    // Console.WriteLine(subDirsOfLastCreatedDir.Count);
                }

                if (subDirsOfLastCreatedDir.Contains("ClientSettings"))
                {
                    label1.Content = Strings.settingsWasFound;

                    Console.WriteLine("ClientSettings was found! Checking for the file...");
                    List<string> yops = new List<string>();
                    foreach (string yop in System.IO.Directory.EnumerateFiles(lastCreatedDir.FullName + "\\ClientSettings"))
                    {
                        yops.Add(yop);
                        Console.WriteLine(yop);
                    }
                    if (yops.Contains(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json"))
                    {
                        label1.Content = Strings.alreadyOptimized;
                        Console.WriteLine("Already optimized!");
                    }
                    else
                    {
                        label1.Content = Strings.creatingFile;
                        Console.WriteLine("Creating the file...");
                        var stream = System.IO.File.CreateText(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json");
                        stream.Write(settings);
                        label1.Content = Strings.fileWasCreated;
                        Console.WriteLine("File was created!");
                        stream.Close();
                    }
                }
                else
                {
                    label1.Content = Strings.creatingDir;
                    Console.WriteLine("ClientSettings wasn\'t found! Creating it...");
                    System.IO.Directory.CreateDirectory(lastCreatedDir.FullName + "\\ClientSettings");
                    var stream = System.IO.File.CreateText(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json");
                    stream.Write(settings);
                    label1.Content = Strings.fileWasCreated;
                    Console.WriteLine("File was created!");
                    stream.Close();
                }
            }
            else
            {
                string mainDirPath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Roblox\\Versions";
                DirectoryInfo mainDir;
                DirectoryInfo[] subDirs;

                try
                {
                    mainDir = new DirectoryInfo(mainDirPath);
                    subDirs = mainDir.GetDirectories();
                }
                catch (DirectoryNotFoundException)
                {
                    label1.Content = Strings.notInstalled;
                    return;
                }

                DateTime lastCreated = DateTime.MinValue;
                DirectoryInfo lastCreatedDir = null;

                label1.Content = Strings.findingDir;

                foreach (DirectoryInfo subDir in subDirs)
                {
                    if (subDir.CreationTime > lastCreated)
                    {
                        lastCreated = subDir.CreationTime;
                        lastCreatedDir = subDir;
                    }
                }

                if (lastCreatedDir != null)
                {
                    Console.WriteLine("Last created directory: " + lastCreatedDir.FullName);
                }

                List<string> subDirsOfLastCreatedDir = new List<string>();
                foreach (System.IO.DirectoryInfo dir in lastCreatedDir.EnumerateDirectories())
                {
                    subDirsOfLastCreatedDir.Add(dir.Name);
                    // Console.WriteLine(subDirsOfLastCreatedDir.Count);
                }

                if (subDirsOfLastCreatedDir.Contains("ClientSettings"))
                {
                    label1.Content = Strings.deletingDir;
                    Console.WriteLine("Deleting...");
                    System.IO.Directory.Delete(lastCreatedDir.FullName + "\\ClientSettings", true);
                    label1.Content = Strings.deletedDir;
                }
                else
                {
                    label1.Content = Strings.everythingIsOK;
                    Console.WriteLine("ClientSettings wasn\'t found! Creating it...");
                }
            };
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            string mainDirPath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Roblox\\Versions";
            DirectoryInfo mainDir;
            DirectoryInfo[] subDirs;

            try
            {
                mainDir = new DirectoryInfo(mainDirPath);
                subDirs = mainDir.GetDirectories();
            }
            catch (DirectoryNotFoundException)
            {
                //label2.Text = "Roblox isn't installed.";
                return;
            }

            DateTime lastCreated = DateTime.MinValue;
            DirectoryInfo lastCreatedDir = null;

            //label2.Text = "Finding the last version folder...";

            foreach (DirectoryInfo subDir in subDirs)
            {
                if (subDir.CreationTime > lastCreated)
                {
                    lastCreated = subDir.CreationTime;
                    lastCreatedDir = subDir;
                }
            }

            if (lastCreatedDir != null)
            {
                Console.WriteLine("Last created directory: " + lastCreatedDir.FullName);
            }

            List<string> subDirsOfLastCreatedDir = new List<string>();
            foreach (System.IO.DirectoryInfo dir in lastCreatedDir.EnumerateDirectories())
            {
                subDirsOfLastCreatedDir.Add(dir.Name);
                // Console.WriteLine(subDirsOfLastCreatedDir.Count);
            }

            if (subDirsOfLastCreatedDir.Contains("ClientSettings"))
            {
                //label2.Text = "ClientSettings was found! Checking for the file...";

                Console.WriteLine("ClientSettings was found! Checking for the file...");
                List<string> yops = new List<string>();
                foreach (string yop in System.IO.Directory.EnumerateFiles(lastCreatedDir.FullName + "\\ClientSettings"))
                {
                    yops.Add(yop);
                    Console.WriteLine(yop);
                }
                if (yops.Contains(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json"))
                {
                    //label2.Text = "Already optimized!";
                    Console.WriteLine("Already optimized!");
                    checkbox1.IsChecked = true;
                    enableOptimizer = true;
                }
                else
                {
                    checkbox1.IsChecked = false;
                    enableOptimizer = false;
                }
            }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            new About().Show();
        }

        private void Window1_Closed(object sender, EventArgs e)
        {
            //base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
