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
        int currentEmpleadoID = 1;

        public Form1()
        {
            InitializeComponent();

        }

        // Abre PDF
        private void tsb_Abrir_Click(object sender, EventArgs e)
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

            // Elimina la información de la tabla inferior
            dataGrid1.DataSource = null;
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

            // Propiedades de lectura escritura para cada columna
            dt.Columns["ID"].ReadOnly = true;

            // llena la ingormacion -------------------------------------------------------
            int i = 0, k = 0;

            foreach (Empleado e in empleados)
            {
                // ID
                DataRow dr = dt.NewRow();
                dr[k++] = e.ID;
                dr[k++] = e.Nombre;
                dt.Rows.Add(dr);
                i = 0;
                k = 0;
            }

            // Envía datos a control
            this.dataGrid.DataSource = dt;

        }
        private void highlightTable()
        {
    
            for (int i = 0; i < this.dataGrid1.Rows.Count; i++)
            {
                for (int j = 1; j < this.dataGrid1.Columns.Count; j++)
                {

                    TimeSpan check = new TimeSpan();
            
                    switch (i)
                    {
                        case 0:
                            check = horasL.entrada1.TimeOfDay;
                            break;
                        case 1:
                            continue;
                        case 2:
                            check = horasL.entrada2.TimeOfDay;
                            break;
                        case 3:
                            continue;
                    }

                    if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value > check)
                    {
                        this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.Salmon;
                    }
                }

            }

        }

        // Modificar empleados y volver a leer informacion ante cambios en la tabla
        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Obtiene ID del empleado
            int ID = (int)this.dataGrid.Rows[e.RowIndex].Cells[0].Value;

            // Modifica propiedad del empleado
            switch (this.dataGrid.Columns[e.ColumnIndex].Name)
            {
                case "Nombre del empleado":
                    foreach (Empleado em in gEmpleados.Where(x => x.ID == ID)) em.Nombre = (string)this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;

                case "Puntualidad":
                    foreach (Empleado em in gEmpleados.Where(x => x.ID == ID)) em.Puntualidad = (bool)this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;

                case "Asistencia":
                    foreach (Empleado em in gEmpleados.Where(x => x.ID == ID)) em.Asistencia = (bool)this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;

                case "Desempeño":
                    foreach (Empleado em in gEmpleados.Where(x => x.ID == ID)) em.Desempeno = (bool)this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;

                default:
                    MessageBox.Show("No se puede cambiar esta propiedad. Intente cambiarla en la vista detallada.");
                    break;
            }

            // Carga los valores modificados
            generateTable(gEmpleados);
            // Resalta informacion
            highlightTable();
        }

        // Despliega información del usuario
        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Elimina la información
            dataGrid1.DataSource = null;
            // Obtiene ID del empleado
            this.currentEmpleadoID = (int)this.dataGrid.Rows[e.RowIndex].Cells[0].Value;
            // Tamaño de celda automático
            this.dataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (Empleado em in gEmpleados.Where(x => x.ID == this.currentEmpleadoID))
            {
                fillTablaRegistros(em);
                setEmpleadoPropiedadesUI(em);
            }
        }

        // Tabla de detalle de registros
        private void fillTablaRegistros(Empleado em)
        {
            try
            {
                //-------------------------------------------------- TABLA
                // ------------------------- Obtiene la información del empleado seleccionado y la despliega en datagrif
                // Genera los headers de los días dinamicamente -------------------------------------------------------
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("Acceso", typeof(string)));

                foreach (TiemposDia t in em.Dias)
                {
                    try
                    {
                        dt.Columns.Add(new DataColumn(t.dia.ToShortDateString(), typeof(TimeSpan)));
                    }
                    catch (System.Data.DuplicateNameException e)
                    {
                        dt.Columns.Add(new DataColumn(t.dia.ToShortDateString() + " (repetido)", typeof(TimeSpan)));

                    }
                }

                // Propiedades de lectura escritura para cada columna
                dt.Columns[0].ReadOnly = true;

                DataRow drEntrada1 = dt.NewRow();
                DataRow drSalida1 = dt.NewRow();
                DataRow drEntrada2 = dt.NewRow();
                DataRow drSalida2 = dt.NewRow();

                drEntrada1[0] = "Entrada 1";

                for (int i = 0; i < em.Dias.Count; i++)
                {
                    if (em.Dias[i].entrada1.status == "NOREGISTRO") drEntrada1[i + 1] = TimeSpan.Parse("00:00");
                    else drEntrada1[i + 1] = em.Dias[i].entrada1.Hora.TimeOfDay;
                }

                drSalida1[0] = "Salida 1";

                for (int i = 0; i < em.Dias.Count; i++)
                {
                    if (em.Dias[i].salida1.status == "NOREGISTRO") drSalida1[i + 1] = TimeSpan.Parse("00:00");
                    else drSalida1[i + 1] = em.Dias[i].salida1.Hora.TimeOfDay;
                }

                drEntrada2[0] = "Entrada 2";

                for (int i = 0; i < em.Dias.Count; i++)
                {
                    if (em.Dias[i].entrada2.status == "NOREGISTRO") drEntrada2[i + 1] = TimeSpan.Parse("00:00");
                    else drEntrada2[i + 1] = em.Dias[i].entrada2.Hora.TimeOfDay;
                }

                drSalida2[0] = "Salida 2";

                for (int i = 0; i < em.Dias.Count; i++)
                {
                    if (em.Dias[i].salida2.status == "NOREGISTRO") drSalida2[i + 1] = TimeSpan.Parse("00:00");
                    else drSalida2[i + 1] = em.Dias[i].salida2.Hora.TimeOfDay;
                }

                dt.Rows.Add(drEntrada1);
                dt.Rows.Add(drSalida1);
                dt.Rows.Add(drEntrada2);
                dt.Rows.Add(drSalida2);


                // Envía datos a control
                this.dataGrid1.DataSource = dt;
                // Inmoviliza columna de nombre
                this.dataGrid1.Columns[0].Frozen = true;
                // Resalta
                highlightTable();
            }
            catch // Cuando se hace sort tambien se llama a este evento y provoca una exepción
            {

            }

        }

        //Setea atributos de puntualidad, asistencia y desempeño
        private void setEmpleadoPropiedadesUI(Empleado em)
        {
            this.cb_Asistencia.Checked = em.Asistencia;
            this.cb_Puntualidad.Checked = em.Puntualidad;
            this.cb_Desempeno.Checked = em.Desempeno;
            this.tb_NombreEmpleado.Text = em.Nombre;
        }

        // Muestra en interfaz información detallada de la hora selecionada en la tabla secundaria
        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}
