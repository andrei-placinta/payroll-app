using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        CrystalReport2 raport2 = new CrystalReport2();
        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new OracleConnection("DATA SOURCE = localhost:1521 / XE; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
                string strSQL = "SELECT * FROM tabela1";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "tabela1");
                OracleCommandBuilder cmd = new OracleCommandBuilder(da);
                crystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                raport2.SetDataSource(ds.Tables["tabela1"]);
                crystalReportViewer1.ReportSource = raport2;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = (new FileInfo(AppDomain.CurrentDomain.BaseDirectory)).Directory.Parent.Parent.FullName;
            string workername="";
           if (ds.Tables["tabela1"].Rows.Count >0 && textBox1.Text.ToLower()== ds.Tables["tabela1"].Rows[0].Field<string>(1).ToLower())
                workername = "_" + textBox1.Text.ToLower();
            string filepath = path + "\\fluturas_" + DateTime.Now.ToString("MMMM_yyyy") + workername + ".pdf";
            raport2.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);
            Process.Start(filepath);
        }

        private void button3_Click(object sender, EventArgs e)
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
                crystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                raport2.SetDataSource(ds.Tables["tabela1"]);
                crystalReportViewer1.ReportSource = raport2;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5_Load(sender, e);
        }
    }
}
