using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreNomina
{
    public partial class Opciones : Form
    {

        Form main = Application.OpenForms["Form1"];

        public Opciones()
        {
            InitializeComponent();

            // Obtiene propiedades de MainWindow
            this.tb_Ent1.Text = ((Form1)main).horasL.entrada1.ToShortTimeString();
            this.tb_Sal1.Text = ((Form1)main).horasL.salida1.ToShortTimeString();
            this.tb_Ent2.Text = ((Form1)main).horasL.entrada2.ToShortTimeString();
            this.tb_Sal2.Text = ((Form1)main).horasL.salida2.ToShortTimeString();

        }

        private void bt_Aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                ((Form1)main).horasL.entrada1 = DateTime.Parse(this.tb_Ent1.Text);
                ((Form1)main).horasL.salida1 = DateTime.Parse(this.tb_Sal1.Text);
                ((Form1)main).horasL.entrada2 = DateTime.Parse(this.tb_Ent2.Text);
                ((Form1)main).horasL.salida2 = DateTime.Parse(this.tb_Sal2.Text);
            }
            catch
            {
                MessageBox.Show("Compruebe el formato de los parametros");
            }

            // Actualiza MainForm
            ((Form1)main).updateConfig();

            // Cierra esta ventana
            this.Close();

        }

        private void bt_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
