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
    public partial class Form9 : Form
    {
        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
            try
            {
                string strSQL = "SELECT * FROM procent1";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "procent1");
                if (ds.Tables["procent1"].Rows[0].Field<string>(0) != textBox1.Text.Trim())
                {
                    MessageBox.Show("Parola incorecta!");
                    textBox2.Text = textBox1.Text = "";
                }
                else
                {
                    if (textBox2.Text.Trim() == "")
                    {
                        MessageBox.Show("Va rugam sa scrieti o parola!");
                        return;
                    }
                    if (textBox2.Text.Trim() == textBox1.Text.Trim())
                    {
                        MessageBox.Show("Parola este identica cu cea veche!");
                        textBox2.Text="";
                        return;
                    }
                    if (textBox2.Text.Trim().Length < 4) 
                    {
                        MessageBox.Show("Parola este prea scurta! Minim 4 caractere!");
                        textBox2.Text = "";
                        return;
                    }
                    if (textBox2.Text.Trim().Length > 15)
                    {
                        MessageBox.Show("Parola este prea lunga! Maxim 15 caractere!");
                        textBox2.Text = "";
                        return;
                    }
                    conn.Open();
                    string query = "UPDATE procent1 SET parola='" + textBox2.Text.Trim() + "'";
                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Modificat cu succes!");
                    this.Close();
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
