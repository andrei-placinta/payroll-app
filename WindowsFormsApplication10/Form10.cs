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
    public partial class Form10 : Form
    {
        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        public Form10()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form10_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Va rugam introduceti numele!");
                return;
            }
            if (textBox1.Text.Trim().Length > 15)
            {
                MessageBox.Show("Va rugam nu depasiti 15 caractere!");
                return;
            }
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Va rugam introduceti nume valid!");
                return;
            }
            try
            {
                conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
                string strSQL = "SELECT * FROM tabela1 where LOWER(nume)='" + textBox1.Text.Trim().ToLower() + "'";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "tabela1");
                OracleCommandBuilder cmd = new OracleCommandBuilder(da);
                if (ds.Tables["tabela1"].Rows.Count == 0)
                {
                    MessageBox.Show("Nu exista niciun angajat cu numele: " + textBox1.Text.Trim());
                    return;
                }
                dataGridView1.DataSource = ds.Tables["tabela1"];
                if (ds.Tables["tabela1"].Rows.Count == 1)
                {
                    DialogResult dialog = MessageBox.Show("Doriti stergere?", "Stergere", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        int rownum = (dataGridView1.CurrentCell.RowIndex);
                        DataRow Linie = ds.Tables["tabela1"].Rows[rownum];
                        Linie.Delete();
                        da.Update(ds.Tables["tabela1"]);
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Doriti stergere?", "Stergere", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (ds.Tables["tabela1"].Rows.Count > 0)
                    {
                        int rownum = (dataGridView1.CurrentCell.RowIndex);
                        DataRow Linie = ds.Tables["tabela1"].Rows[rownum];
                        Linie.Delete();
                        da.Update(ds.Tables["tabela1"]);
                        dataGridView1.ReadOnly = true;
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }
    }
}
