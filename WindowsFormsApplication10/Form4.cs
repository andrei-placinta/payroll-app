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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OracleCommandBuilder cmd;
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        CrystalReport1 raport = new CrystalReport1();
        private void Form4_Load(object sender, EventArgs e)
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
                raport.SetDataSource(ds.Tables["tabela1"]);
                crystalReportViewer1.ReportSource = raport;
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string path = (new FileInfo(AppDomain.CurrentDomain.BaseDirectory)).Directory.Parent.Parent.FullName;
            DialogResult dialogResult = MessageBox.Show("Doriti informatii aditionale pentru denumirea fisierului?", "Nume fisier", MessageBoxButtons.YesNo);
            Form6 forma6 = new Form6();
            if (dialogResult == DialogResult.Yes)
                forma6.ShowDialog();
            if (forma6.Numefisier != "" || dialogResult == DialogResult.No)
            {
                string filepath = path + "\\statplata_" + DateTime.Now.ToString("MMMM_yyyy") + forma6.Numefisier + ".pdf";
                raport.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);
                Process.Start(filepath);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
