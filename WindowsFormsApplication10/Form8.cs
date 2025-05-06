using Oracle.ManagedDataAccess.Client;
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
    public partial class Form8 : Form
    {
        OracleConnection conn;
        public Form8()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form9 forma9 = new Form9();
            forma9.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Va rugam completati toate campurile!");
                return;
            }
            if (textBox1.Text.Trim().Length > 15 || textBox2.Text.Trim().Length > 15 || textBox3.Text.Trim().Length > 15 )
            {
                MessageBox.Show("Va rugam nu depasiti 15 caractere!");
                textBox3.Text = textBox2.Text = textBox1.Text = "";
                return;
            }
            float i;
            if (!float.TryParse(textBox1.Text.Trim(), out i) || !float.TryParse(textBox2.Text.Trim(), out i) || !float.TryParse(textBox3.Text.Trim(), out i))
            {
                MessageBox.Show("Va rugam introduceti sume corecte!");
                textBox3.Text = textBox2.Text = textBox1.Text = "";
                return;
            }
            conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
            try
            {
                conn.Open();
                string query = "UPDATE procent1 SET cas_=" + float.Parse(textBox1.Text.Trim()) + ",cass_=" + float.Parse(textBox2.Text.Trim()) + ",impozit_=" + float.Parse(textBox3.Text.Trim());
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
                conn.Open();
                string query2 = "UPDATE tabela1 SET nrcrt=nrcrt";
                OracleCommand cmd2 = new OracleCommand(query2, conn);
                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Modificat cu succes!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

    }
}