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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Form2 forma2 = new Form2();
                forma2.ShowDialog();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Form11 forma11 = new Form11();
                forma11.ShowDialog();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                Form10 forma10 = new Form10();
                forma10.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 forma3 = new Form3();
            forma3.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                Form4 forma4 = new Form4();
                forma4.ShowDialog();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                Form5 forma5 = new Form5();
                forma5.ShowDialog();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 forma7 = new Form7();
            forma7.ShowDialog();
        }
    }
}
