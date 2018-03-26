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
        List<Empleado> gEmpleados = new List<Empleado>();
        Analizador analizador = new Analizador();
        HorasLaborales horasL = new HorasLaborales();
           
        public Form1()
        {
            InitializeComponent();

        }

        // Abre PDF
        private void bt_Abrir_Click(object sender, EventArgs e)
        {

            // Abre archivo PDF
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Seleccionar el PDF a escanear";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "pdf";
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();

            // Setea los horarios laborales
            this.horasL.entrada1 = DateTime.Parse("08:00");
            this.horasL.salida1 = DateTime.Parse("13:00");
            this.horasL.entrada2 = DateTime.Parse("14:00");
            this.horasL.salida2 = DateTime.Parse("18:00");
            this.horasL.limiteRetardo = TimeSpan.Parse("00:30:00");

            // Obtiene empleados
            this.gEmpleados = analizador.getEmpleados(ExtractTextFromPdf(openFileDialog.FileName), horasL);

            // Muestra empleados en tabla
            generateTable(gEmpleados);
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
        private void generateTable(List<Empleado> empleados)
        {
            // Tamaño de celda automático
            this.dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Genera los headers dinamicamente -------------------------------------------------------
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Nombre del empleado", typeof(string)));
            dt.Columns.Add(new DataColumn("Departamento", typeof(string)));
            
            foreach(TiemposDia t in empleados[0].Dias)
            {
                dt.Columns.Add(new DataColumn(t.dia.ToShortDateString(), typeof(double)));
            }
            
            dt.Columns.Add(new DataColumn("TOT", typeof(double)));
            dt.Columns.Add(new DataColumn("Puntualidad", typeof(bool)));
            dt.Columns.Add(new DataColumn("Asistencia", typeof(bool)));
            dt.Columns.Add(new DataColumn("Desempeño", typeof(bool)));


            // llena la ingormacion -------------------------------------------------------
            int i = 0, k = 0;

            foreach (Empleado e in empleados)
            {
                // ID
                DataRow dr = dt.NewRow();
                dr[k++] = e.ID;
                // Nombre del empleado
                dr[k++] = e.Nombre;
                // Departamento
                dr[k++] = "Ingenieria";
           
                foreach (TiemposDia t in e.Dias)
                {
                    // Tiempo de retardo en cada día
                    dr[k++] = Math.Round(e.getRetardoDia(horasL, i).TotalMinutes, 0);
                    i ++;
                }

                // Columna TOT
                dr[k++] = Math.Round(e.getRetardoTotal(horasL).TotalMinutes, 0);
                // Columna Puntualidad
                dr[k++] = e.Puntualidad;
                // Asistencia
                dr[k++] = e.Asistencia;
                // Bono (Se asigna manualmente)
                dr[k++] = false;
                // Añade fila
                dt.Rows.Add(dr);
                // Resetea contadores
                i = 0;
                k = 0;
            }

            // Envía datos a control
            this.dataGrid.DataSource = dt;
            // Inmoviliza columna de nombre
            this.dataGrid.Columns["Nombre del empleado"].Frozen = true;
            // Resalta las filas sin asistencia, puntualidad
            highlightTable();

        }
        private void highlightTable()
        {
            // Resalta las filas sin asistencia, puntualidad
            foreach (DataGridViewRow row in this.dataGrid.Rows)
            {
                if ((bool)row.Cells["Puntualidad"].Value == false || (bool)row.Cells["Asistencia"].Value == false)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
            }
        }
        // Modificar empleados y volver a leer informacion ante cambios en la tabla
        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Modifica propiedad del empleado


            highlightTable();
        }
        // Vuelve a resaltar la información 
        private void dataGrid_Sorted(object sender, EventArgs e)
        {
            highlightTable();
        }

    }
}
