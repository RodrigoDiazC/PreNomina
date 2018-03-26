using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TimeChecker
{
    public partial class Form1 : Form
    {
        // Variables globales
        Empleado[] gEmpleados = new Empleado[] { };
        Analizador analizador = new Analizador();

        public Form1()
        {
            InitializeComponent();

        }

        private void bt_Abrir_Click(object sender, EventArgs e)
        {

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Seleccionar el PDF a escanear";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "pdf";
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();

            gEmpleados = analizador.getEmpleados(ExtractTextFromPdf(openFileDialog.FileName));

            showEmpleadoInfo(gEmpleados);
        }

        // Muestra informacion de los usuarios
        private void showEmpleadoInfo(Empleado[] empleadosArray)
        {

          // Formatea las columnas de los días utilizando el primer empleado
            generateTable(empleadosArray[0].getDias(), empleadosArray);

        }

        // Herramientas
        private static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
        private void generateTable(TiemposDia[] dias, Empleado[] empleados)
        {

            // Genera los headers dinamicamente -------------------------------------------------------
            DataTable dt = new DataTable();
            
            dt.Columns.Add(new DataColumn("Nombre del empleado", typeof(string)));
            dt.Columns.Add(new DataColumn("Departamento", typeof(string)));
            
            foreach(TiemposDia t in dias)
            {
                dt.Columns.Add(new DataColumn(t.dia.ToShortDateString(), typeof(TimeSpan)));
            }

            dt.Columns.Add(new DataColumn("TOT", typeof(TimeSpan)));
            dt.Columns.Add(new DataColumn("Puntualidad", typeof(bool)));
            dt.Columns.Add(new DataColumn("Asistencia", typeof(bool)));
            dt.Columns.Add(new DataColumn("Desempeño", typeof(bool)));


            // llena la ingormacion -------------------------------------------------------
            int i = 0, k = 0;

            // Obtiene las horas de trabajo
            HorasLaborales horasL = new HorasLaborales();
            horasL.entrada1 = DateTime.Parse("08:00");
            horasL.salida1 = DateTime.Parse("13:00");
            horasL.entrada2 = DateTime.Parse("14:00");
            horasL.salida2 = DateTime.Parse("18:00");
            horasL.limiteRetardo = TimeSpan.Parse("00:30:00");

            foreach (Empleado e in empleados)
            {
                // Nombre del empleado
                DataRow dr = dt.NewRow();
                dr[k++] = e.getNombre();
                // Departamento
                dr[k++] = "Ingenieria";
           
                foreach (TiemposDia t in e.getDias())
                {
                    // Tiempo de retardo en cada día
                    dr[k++] = e.getRetardoDia(horasL, i);
                    i ++;
                }

                // Columna TOT
                dr[k++] = e.getRetardoSemanal(horasL);

                // Columna Puntualidad
                if (e.getRetardoSemanal(horasL) > horasL.limiteRetardo)
                     dr[k++] = false;
                else dr[k++] = true;

                // Asistencia
                dr[k++] = e.getAsistencia();

                // Bono (Se asigna manualmente)
                dr[k++] = false;

                // Añade fila
                dt.Rows.Add(dr);

                // Resetea contadores
                i = 0;
                k = 0;
            }


            this.dataGrid.DataSource = dt;
        }
       


    }
}
