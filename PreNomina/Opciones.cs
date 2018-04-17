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
            this.cb_TiempoAntRet.Checked = ((Form1)main).retardoAnticipo;
            this.nud_Lim.Value = (int)((Form1)main).horasL.limiteRetardo.TotalMinutes;
            this.tb_Ruta.Text = ((Form1)main).rutaFolder;
            this.tb_Departamento.Text = ((Form1)main).departamento;
        }

        private void bt_Aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                ((Form1)main).horasL.entrada1 = DateTime.Parse(this.tb_Ent1.Text);
                ((Form1)main).horasL.salida1 = DateTime.Parse(this.tb_Sal1.Text);
                ((Form1)main).horasL.entrada2 = DateTime.Parse(this.tb_Ent2.Text);
                ((Form1)main).horasL.salida2 = DateTime.Parse(this.tb_Sal2.Text);

                // Nombre de departamento
                ((Form1)main).departamento = this.tb_Departamento.Text;

                // Retardo mas anticipo
                ((Form1)main).retardoAnticipo = this.cb_TiempoAntRet.Checked;

                // Ruta de folder
                ((Form1)main).rutaFolder = this.tb_Ruta.Text;

                // Limite de retardo
                ((Form1)main).horasL.limiteRetardo = TimeSpan.FromMinutes((double)this.nud_Lim.Value);

                // Actualiza MainForm
                ((Form1)main).updateConfig();

                  // Cierra esta ventana
                this.Close();
            }
            catch
            {
                MessageBox.Show("Compruebe el formato de los parametros");
            }
        }

        private void bt_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_Examinar_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();
            this.tb_Ruta.Text = folder.SelectedPath;
        }
    }
}
