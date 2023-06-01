using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public bool enableOptimizer = false;

        public string settings = System.IO.File.ReadAllText("settings.json");

        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableOptimizer = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enableOptimizer == true)
            {
                string mainDirPath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Roblox\\Versions"; ;
                DirectoryInfo mainDir = new DirectoryInfo(mainDirPath);
                if (mainDir.Exists)
                {
                    Console.WriteLine("roblox was found");
                    label2.Text = "Roblox was found. Finding ClientSettings...";
                } else
                {
                    label2.Text = "Roblox isn't installed";
                    return;
                }

                DirectoryInfo[] subDirs = mainDir.GetDirectories();
                DateTime lastCreated = DateTime.MinValue;
                DirectoryInfo lastCreatedDir = null;

                label2.Text = "Finding the last version folder...";

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
                    label2.Text = "ClientSettings was found! Checking for the file...";

                    Console.WriteLine("ClientSettings was found! Checking for the file...");
                    List<string> yops = new List<string>();
                    foreach (string yop in System.IO.Directory.EnumerateFiles(lastCreatedDir.FullName + "\\ClientSettings"))
                    {
                        yops.Add(yop);
                        Console.WriteLine(yop);
                    }
                    if (yops.Contains(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json"))
                    {
                        label2.Text = "Already optimized!";
                        Console.WriteLine("Already optimized!");
                    }
                    else
                    {
                        label2.Text = "Creating the file...";
                        Console.WriteLine("Creating the file...");
                        var stream = System.IO.File.CreateText(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json");
                        stream.Write(settings);
                        label2.Text = "File was created!";
                        Console.WriteLine("File was created!");
                        stream.Close();
                    }
                } else
                {
                    label2.Text = "ClientSettings wasn\'t found! Creating it...";
                    Console.WriteLine("ClientSettings wasn\'t found! Creating it...");
                    System.IO.Directory.CreateDirectory(lastCreatedDir.FullName + "\\ClientSettings");
                    var stream = System.IO.File.CreateText(lastCreatedDir.FullName + "\\ClientSettings\\" + "ClientAppSettings.json");
                    stream.Write(settings);
                    label2.Text = "File was created!";
                    Console.WriteLine("File was created!");
                    stream.Close();
                }
            }
            else 
            {
                string mainDirPath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Roblox\\Versions"; ;
                DirectoryInfo mainDir = new DirectoryInfo(mainDirPath);
                DirectoryInfo[] subDirs = mainDir.GetDirectories();
                DateTime lastCreated = DateTime.MinValue;
                DirectoryInfo lastCreatedDir = null;

                if (mainDir.Exists)
                {
                    Console.WriteLine("roblox was found");
                    label2.Text = "Roblox was found. Finding ClientSettings...";
                }
                else
                {
                    label2.Text = "Roblox isn't installed. Everything is OK";
                    return;
                }

                label2.Text = "Finding the last version folder...";

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
                    label2.Text = "ClientSettings was found! Deleting...";
                    Console.WriteLine("Deleting...");
                    System.IO.Directory.Delete(lastCreatedDir.FullName + "\\ClientSettings", true);
                    label2.Text = "Deleted!";
                }
                else
                {
                    label2.Text = "ClientSettings wasn\'t found! Everything is OK.";
                    Console.WriteLine("ClientSettings wasn\'t found! Creating it...");
                }
            };
        }
    }
};
