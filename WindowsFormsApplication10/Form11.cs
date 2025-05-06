using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form11 : Form
    {
        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        public Form11()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form11_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
                string strSQL = "SELECT * FROM tabela1";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "tabela1");
                OracleCommandBuilder cmd = new OracleCommandBuilder(da);
                dataGridView1.DataSource = ds.Tables["tabela1"];
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty || textBox4.Text.Trim() == string.Empty || textBox5.Text.Trim() == string.Empty || textBox6.Text.Trim() == string.Empty || textBox7.Text.Trim() == string.Empty || textBox8.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Va rugam completati toate campurile!");
                return;
            }
            if (textBox2.Text.Trim().Length > 15 || textBox3.Text.Trim().Length > 15 || textBox4.Text.Trim().Length > 15 || textBox5.Text.Trim().Length > 15 || textBox6.Text.Trim().Length > 15 || textBox7.Text.Trim().Length > 15 || textBox8.Text.Trim().Length > 15)
            {
                MessageBox.Show("Va rugam nu depasiti 15 caractere!");
                return;
            }
            if (!Regex.IsMatch(textBox2.Text.Trim(), @"^[a-zA-Z]+$") || !Regex.IsMatch(textBox3.Text.Trim(), @"^[a-zA-Z]+$") || !Regex.IsMatch(textBox4.Text.Trim(), @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Va rugam introduceti nume valide!");
                return;
            }
            float i;
            if (!float.TryParse(textBox5.Text.Trim(), out i) || !float.TryParse(textBox6.Text.Trim(), out i) || !float.TryParse(textBox7.Text.Trim(), out i) || !float.TryParse(textBox8.Text.Trim(), out i))
            {
                MessageBox.Show("Va rugam introduceti sume corecte!");
                return;
            }
            conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
            try
            {
                conn.Open();
                string query = "INSERT INTO tabela1(nrcrt,nume,prenume,functie,salar_baza,spor_,premii_brute,retineri) VALUES ((SELECT nvl(max(nrcrt), 0) + 1 from tabela1),'" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" + textBox4.Text.Trim() + "'," + textBox5.Text.Trim() + "," + textBox6.Text.Trim() + "," + textBox7.Text.Trim() + "," + textBox8.Text.Trim() + ")";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                string strSQL = "SELECT * FROM tabela1";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "tabela1");
                dataGridView1.DataSource = ds.Tables["tabela1"];
                conn.Close();
                MessageBox.Show("Adaugat cu succes!");
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }
    }
}
