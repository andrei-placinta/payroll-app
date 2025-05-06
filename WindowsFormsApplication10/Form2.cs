using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication10
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;

        private void Form2_Load(object sender, EventArgs e)
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
            dataGridView1.Columns["nrcrt"].ReadOnly = true;
            dataGridView1.Columns["total_brut"].ReadOnly = true;
            dataGridView1.Columns["brut_impozabil"].ReadOnly = true;
            dataGridView1.Columns["impozit"].ReadOnly = true;
            dataGridView1.Columns["cas"].ReadOnly = true;
            dataGridView1.Columns["cass"].ReadOnly = true;
            dataGridView1.Columns["virat_card"].ReadOnly = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ds.RejectChanges();
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = false;
                MessageBox.Show("Modificari anulate!");
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
            Form2_Load(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
            textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = false;
            Form2_Load(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
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
                textBox2.Text = ds.Tables["tabela1"].Rows[0]["nume"].ToString();
                textBox3.Text = ds.Tables["tabela1"].Rows[0]["prenume"].ToString();
                textBox4.Text = ds.Tables["tabela1"].Rows[0]["functie"].ToString();
                textBox5.Text = ds.Tables["tabela1"].Rows[0]["salar_baza"].ToString();
                textBox6.Text = ds.Tables["tabela1"].Rows[0]["spor_"].ToString();
                textBox7.Text = ds.Tables["tabela1"].Rows[0]["premii_brute"].ToString();
                textBox8.Text = ds.Tables["tabela1"].Rows[0]["retineri"].ToString();
                textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Enabled == false)
            {
                MessageBox.Show("Va rugam cautati un nume!");
                return;
            }
            if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty || textBox4.Text.Trim() == string.Empty || textBox5.Text.Trim() == string.Empty || textBox6.Text.Trim() == string.Empty || textBox7.Text.Trim() == string.Empty || textBox8.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Va rugam completati toate campurile!");
                return;
            }
            if (textBox1.Text.Trim().Length > 15 || textBox2.Text.Trim().Length > 15 || textBox3.Text.Trim().Length > 15 || textBox4.Text.Trim().Length > 15 || textBox5.Text.Trim().Length > 15 || textBox6.Text.Trim().Length > 15 || textBox7.Text.Trim().Length > 15 || textBox8.Text.Trim().Length > 15)
            {
                MessageBox.Show("Va rugam nu depasiti 15 caractere!");
                return;
            }
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"^[a-zA-Z]+$") || !Regex.IsMatch(textBox2.Text.Trim(), @"^[a-zA-Z]+$") || !Regex.IsMatch(textBox3.Text.Trim(), @"^[a-zA-Z]+$") || !Regex.IsMatch(textBox4.Text.Trim(), @"^[a-zA-Z]+$"))
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
                string query = "UPDATE tabela1 SET nume='" + textBox2.Text.Trim() + "',prenume='" + textBox3.Text.Trim() + "',functie='" + textBox4.Text.Trim() + "',salar_baza=" + float.Parse(textBox5.Text.Trim()) + ",spor_=" + float.Parse(textBox6.Text.Trim()) + ",premii_brute=" + float.Parse(textBox7.Text.Trim()) + ",retineri=" + float.Parse(textBox8.Text.Trim()) + "WHERE LOWER(nume)='" + textBox1.Text.Trim() + "'";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                string strSQL = "SELECT * FROM tabela1";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "tabela1");
                dataGridView1.DataSource = ds.Tables["tabela1"];
                conn.Close();
                MessageBox.Show("Modificat cu succes!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = false;

            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
            try
            {
                conn.Open();
                string query = "UPDATE tabela1 SET nrcrt=nrcrt";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                da.Update(ds.Tables["tabela1"]);
                ds.AcceptChanges();
                conn.Close();
                MessageBox.Show("Modificat cu succes!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = false;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                Form2_Load(sender, e);
                return;
            }
            Form2_Load(sender, e);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;
                if (!Regex.IsMatch(row.Cells["nume"].Value.ToString(), @"^[a-zA-Z]+$") || !Regex.IsMatch(row.Cells["prenume"].Value.ToString(), @"^[a-zA-Z]+$") || !Regex.IsMatch(row.Cells["functie"].Value.ToString(), @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("Va rugam introduceti nume valide");
                    ds.RejectChanges();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            var a = dataGridView1.CurrentCell.ColumnIndex;
            if (a == dataGridView1.Columns["salar_baza"].Index || a == dataGridView1.Columns["spor_"].Index || a == dataGridView1.Columns["premii_brute"].Index || a == dataGridView1.Columns["retineri"].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}