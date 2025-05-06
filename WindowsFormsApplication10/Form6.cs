using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form6 : Form
    {
        private string text1 = "";
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length>50)
            {
                MessageBox.Show("Va rugam nu depasiti 50 caractere!");
                return;
            }
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Va rugam completati acest camp!");
                return;
            }
                text1 = "_" + textBox1.Text.Trim();
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            text1 = "";
            this.Close();
        }
        public string Numefisier
        {
            get { return text1; }
            set { text1 = value; }
        }
    }
}
