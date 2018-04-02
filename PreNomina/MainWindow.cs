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
            this.lb_TotRet.Text = ((int)em.getRetardoTotal(horasL).TotalMinutes).ToString();
            this.lb_TotExc.Text = ((int)em.getExtraTotal(horasL).TotalMinutes).ToString();
        }

        // Muestra en interfaz información detallada de la hora selecionada en la tabla secundaria
        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


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
        private void highlightTable(int mode, bool clear) // 1 Retardos 2 Anticipos 3 Excedente
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
                            check = horasL.salida1.TimeOfDay;
                            break;
                        case 2:
                            check = horasL.entrada2.TimeOfDay;
                            break;
                        case 3:
                            check = horasL.salida2.TimeOfDay;
                            break;
                    }

                    // Modos -----------------------------------------------------------------------

                    if ((mode == 1) && (i == 0 || i == 2)) // Retardo
                    {
                        if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value > check)
                        {
                            if(clear) this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            else this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.Salmon;
                        }
                    }

                    if ((mode == 2) && (i == 1 || i == 3)) // Anticipo
                    {
                        if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value < check)
                        {
                            if (clear) this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            else this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.MediumPurple;
                        }
                    }

                    if ((mode == 3) && (i == 1 || i == 3)) // Excedente
                    {
                        if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value > check)
                        {
                            if (clear) this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            else this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }
        private void cb_Retardos_CheckedChanged(object sender, EventArgs e)
        {
            highlightTable(1, !this.cb_Retardos.Checked);
        }
        private void cb_Anticipo_CheckedChanged(object sender, EventArgs e)
        {
            highlightTable(2, !this.cb_Anticipo.Checked);
        }
        private void cb_Excedente_CheckedChanged(object sender, EventArgs e)
        {
            highlightTable(3, !this.cb_Excedente.Checked);
        }

    }
}
