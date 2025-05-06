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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = "Ajutor";
            label1.Text = "Instructiuni de folosire a aplicatiei \n- AJUTOR - afiseaza pe ecran informatii ajutatoare globale privind modul de operare in program" 
                +"\n- INTRODUCERE DATE - butonul este blocat pana se selecteaza una dintre cele 3 optiuni"
                +"\n- Actualizare date - afisarea tuturor angajatilor si UPDATE a informatiei din campurile editabile pentru un anumit angajat cautat"
                +"\n- Adaugare angajati -"
                +"\n- Stergere angajati - dupa nume"
                + "\n- Tiparire - generare raport de tip tabelar sau fluturas pentru fiecare angajat in parte"
                + "\n- Modif_procente - modificare a formulelor de calcul a salariilor"
                + "\n- Iesire - parasire program"
                ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
