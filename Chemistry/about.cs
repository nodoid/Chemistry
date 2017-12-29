using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chemistry
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
            textBox1.Text = "A collection of various chemistry applications all grouped together in one place.";
        }
    }
}
