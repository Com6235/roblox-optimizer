﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public bool enableOptimizer = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableOptimizer = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enableOptimizer == true)
            {
                Console.WriteLine("true");
            }
            else 
            {
                Console.WriteLine("false");
            }
        }
    }
}
