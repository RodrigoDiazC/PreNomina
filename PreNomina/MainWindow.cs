using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Office.Interop.Excel;


namespace TimeChecker
{
    public partial class Form1 : Form
    {
        // Variables globales
        List<Empleado> gEmpleados = new List<Empleado>();
        Analizador analizador = new Analizador();
        HorasLaborales horasL = new HorasLaborales();
        int currentEmpleadoID = 1;
        int currentDay = 1;

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

            fillTablaRegistros(this.gEmpleados[currentEmpleadoID]);
            setEmpleadoPropiedadesUI(this.gEmpleados[currentEmpleadoID]);

            // Actualiza el highlight 
            updateHighlight();

        }

        // Tabla de detalle de registros
        private void fillTablaRegistros(Empleado em)
        {
            try
            {
                //-------------------------------------------------- TABLA
                // ------------------------- Obtiene la información del empleado seleccionado y la despliega en datagrif
                // Genera los headers de los días dinamicamente -------------------------------------------------------
                System.Data.DataTable dt = new System.Data.DataTable();

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

        //Setea atributos del empleado en la interfaz
        private void setEmpleadoPropiedadesUI(Empleado em)
        {
            this.cb_Asistencia.Checked = em.Asistencia;
            this.cb_Puntualidad.Checked = em.Puntualidad;
            this.cb_Desempeno.Checked = em.Desempeno;
            this.tb_NombreEmpleado.Text = em.Nombre;
            this.lb_TotRet.Text = ((int)em.getRetardoTotal(horasL).TotalMinutes).ToString();
            this.lb_TotExc.Text = ((int)em.getExtraTotal(horasL).TotalMinutes).ToString();
        }

        //Setea atributos de la casilla seleccionada
        private void setEmpleadoTimeUI(Empleado em, int diaIndx, int acceso) //acceso 0 Entrada1 1 Salida1 2 Entrada2 3 Salida2
        {

            try
            {
                // Informacion del registro
                switch (acceso)
                {
                    case 0:
                        this.lb_Status.Text = em.Dias[diaIndx].entrada1.status;
                        this.tb_Observaciones.Text = em.Dias[diaIndx].entrada1.observaciones;
                        this.tb_Acceso.Text = em.Dias[diaIndx].entrada1.Hora.ToShortTimeString() == "12:00 a. m." ? " " : em.Dias[diaIndx].entrada1.Hora.ToShortTimeString();
                        break;
                    case 1:
                        this.lb_Status.Text = em.Dias[diaIndx].salida1.status;
                        this.tb_Observaciones.Text = em.Dias[diaIndx].salida1.observaciones;
                        this.tb_Acceso.Text = em.Dias[diaIndx].salida1.Hora.ToShortTimeString() == "12:00 a. m." ? " " : em.Dias[diaIndx].salida1.Hora.ToShortTimeString();
                        break;
                    case 2:
                        this.lb_Status.Text = em.Dias[diaIndx].entrada2.status;
                        this.tb_Observaciones.Text = em.Dias[diaIndx].entrada2.observaciones;
                        this.tb_Acceso.Text = em.Dias[diaIndx].entrada2.Hora.ToShortTimeString() == "12:00 a. m." ? " " : em.Dias[diaIndx].entrada2.Hora.ToShortTimeString();
                        break;
                    case 3:
                        this.lb_Status.Text = em.Dias[diaIndx].salida2.status;
                        this.tb_Observaciones.Text = em.Dias[diaIndx].salida2.observaciones;
                        this.tb_Acceso.Text = em.Dias[diaIndx].salida2.Hora.ToShortTimeString() == "12:00 a. m." ? " " : em.Dias[diaIndx].salida2.Hora.ToShortTimeString();
                        break;
                }

                // Información del día
                this.groupBox4.Text = "Información del día " + em.Dias[diaIndx].dia.ToLongDateString();

                switch (em.Dias[diaIndx].status)
                {
                    case "A":
                        this.rb_Asistencia.Checked = true;
                        break;
                    case "TF":
                        this.rb_TrabajoF.Checked = true;
                        break;
                    case "P":
                        this.rb_Permiso.Checked = true;
                        break;
                    case "F":
                        this.rb_Falta.Checked = true;
                        break;
                    case "V":
                        this.rb_Vacaciones.Checked = true;
                        break;
                    case "I":
                        this.rb_Incapacidad.Checked = true;
                        break;
                }
            }
            catch
            {

            }

        }

        // Muestra en interfaz información detallada de la hora selecionada en la tabla secundaria
        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (Empleado em in gEmpleados.Where(x => x.ID == this.currentEmpleadoID))
            {
                this.currentDay = e.ColumnIndex - 1;
                setEmpleadoTimeUI(em, e.ColumnIndex - 1, e.RowIndex);
            }

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
            System.Data.DataTable dt = new System.Data.DataTable();

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
        private void highlightTable(int mode, bool clear) // 1 Retardos 2 Anticipos 3 Excedente 4 No Registro
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
                            if (clear) this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            else this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.Salmon;
                        }
                    }

                    if ((mode == 2) && (i == 1 || i == 3)) // Anticipo
                    {
                        if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value < check && (TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value != TimeSpan.Parse("00:00:00"))
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

                    if ((mode == 4)) // No registro
                    {
                        if ((TimeSpan)this.dataGrid1.Rows[i].Cells[j].Value == TimeSpan.Parse("00:00:00"))
                        {
                            if (clear) this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            else this.dataGrid1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
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
        private void cb_NoReg_CheckedChanged(object sender, EventArgs e)
        {
            highlightTable(4, !this.cb_NoReg.Checked);
        }
        private void groupBox4_Validated(object sender, EventArgs e)
        {
            var checkedButton = groupBox4.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            switch (checkedButton.Name)
            {
                case "rb_Asistencia":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "A";
                    break;

                case "rb_Falta":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "F";
                    break;

                case "rb_Vacaciones":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "V";
                    break;

                case "rb_Permiso":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "P";
                    break;

                case "rb_TrabajoF":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "TF";
                    break;

                case "rb_Incapacidad":
                    this.gEmpleados[currentEmpleadoID].Dias[currentDay].status = "I";
                    break;
            }

        }
        private void cb_Puntualidad_CheckedChanged(object sender, EventArgs e)
        {
            this.gEmpleados[currentEmpleadoID].Puntualidad = this.cb_Puntualidad.Checked;
        }
        private void cb_Asistencia_CheckedChanged(object sender, EventArgs e)
        {
            this.gEmpleados[currentEmpleadoID].Asistencia = this.cb_Asistencia.Checked;
        }
        private void cb_Desempeno_CheckedChanged(object sender, EventArgs e)
        {
            this.gEmpleados[currentEmpleadoID].Desempeno = this.cb_Desempeno.Checked;
        }
        private void tsb_Exportar_Click(object sender, EventArgs e)
        {
            // Generar el archivo

            //---- Ruta del archivo plantilla
            string rutaTemplate = Directory.GetCurrentDirectory() + "\\Template.xlsx";
            string rutaFolder = Directory.GetCurrentDirectory() + "\\Test";
            //--- Crea carpeta
            Directory.CreateDirectory(rutaFolder);

            //--- Nueva ruta del archivo
            string rutaNueva = rutaFolder + "\\test.xls";

            //--- Toolkit para Excel ----//
            Workbook mWorkBook;
            Sheets mWorkSheets;
            Worksheet mWSheet1;
            Microsoft.Office.Interop.Excel.Application oXL;

            //--- Creando objeto y configurando parametros
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;    //Para que no abra la ventana de excel
            oXL.DisplayAlerts = false;

            //--- Abre el archivo
            mWorkBook = oXL.Workbooks.Open(rutaTemplate, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            //--- Get all the sheets in the workbook
            mWorkSheets = mWorkBook.Worksheets;

            //--- Get the allready exists sheet
            mWSheet1 = (Worksheet)mWorkSheets.get_Item("Hoja1");

            //------------------------------------------------------------------ Pone los datos estáticos

            //------------------------------------------- Pone la cantidad de filas de acuerdo a la cantidad de empleados
            for (int i = 0; i < this.gEmpleados.Count - 2; i++)
            {
                Range line = (Range)mWSheet1.Rows[6];
                line.Insert();
            }

            //--------------------------------------------- Nombres de los empleados
            int k = 0;
            foreach (Empleado em in this.gEmpleados)
            {
                mWSheet1.Cells[2][5 + (k++)] = em.Nombre;
            }

            //--------------------------------------------- Los días
            int prevMax = 0;
            int emID = 0;
            foreach (Empleado em in this.gEmpleados)
            {
                if (em.Dias.Count > prevMax)
                {
                    prevMax = em.Dias.Count;
                    emID = em.ID;
                }
            }

            // Dias extra a los 4 por default
            if (prevMax > 4)
            {
                for (int i = 0; i < prevMax - 4; i++)
                {
                    Range rng = mWSheet1.get_Range("E4", Missing.Value);
                    rng.EntireColumn.Insert(XlInsertShiftDirection.xlShiftToRight,
                                            XlInsertFormatOrigin.xlFormatFromRightOrBelow);
                }
            }

            k = 0;
            int offDayIdx = 0;
            int[] offDays = new int[12];

            foreach (TiemposDia t in this.gEmpleados[emID].Dias)
            {
                if (t.entrada1.status == "DESCANSOTRAB" || t.salida1.status == "DESCANSOTRAB") // Dias inhabiles
                {
                    offDays[offDayIdx++] = 4 + (k);
                    mWSheet1.Cells[4 + (k++)][4] = t.dia.Day + "*";
                }
                else mWSheet1.Cells[4 + (k++)][4] = t.dia.Day;
            }

            // --------------------------------------------------------------- Llena los días con el estatus
            {
                int i = 0, j = 0;
                int offset = 0;

                for (i = 0; i < this.gEmpleados.Count; i++)
                {
                    for (j = 0; j < this.gEmpleados[i].Dias.Count; j++)
                    {
                        if (offDays.Contains(4 + j + offset) && ((this.gEmpleados[i].Dias[j].entrada1.status != "DESCANSOTRAB") && (this.gEmpleados[i].Dias[j].salida1.status != "DESCANSOTRAB"))) //&& (offset == 0))
                        {
                            if (offDays.Contains(4 + j + 1 + offset) && ((this.gEmpleados[i].Dias[j + 1].entrada1.status != "DESCANSOTRAB") || (this.gEmpleados[i].Dias[j + 1].salida1.status != "DESCANSOTRAB")))
                            {
                                offset++;
                            }
                            offset++;
                        }
                        mWSheet1.Cells[4 + j + offset][5 + i] = this.gEmpleados[i].Dias[j].status;
                    }

                    // Retardo total
                    mWSheet1.Cells[4 + j + offset][5 + i] = ((int)this.gEmpleados[i].getRetardoTotal(horasL).TotalMinutes).ToString();
                    // Puntualidad
                    mWSheet1.Cells[4 + j + offset + 1][5 + i] = this.gEmpleados[i].Puntualidad ? "SI" : "NO";
                    // Asistencia
                    mWSheet1.Cells[4 + j + offset + 2][5 + i] = this.gEmpleados[i].Asistencia ? "SI" : "NO";
                    // Desempeño
                    mWSheet1.Cells[4 + j + offset + 3][5 + i] = this.gEmpleados[i].Desempeno ? "SI" : "NO";

                    offset = 0;
                }
            }

            // ----------------------------------------------------------------------- Rango de meses
            if (this.analizador.fechaInicio.Month != this.analizador.fechaFin.Month)
            {
                mWSheet1.Cells[4][3] = this.analizador.fechaInicio.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture) + " - " + this.analizador.fechaFin.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);
            }
            else mWSheet1.Cells[4][3] = this.analizador.fechaInicio.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);


            // ---------------------------------------------------------------------- Guarda el nuevo reporte
            try
            {
                mWorkBook.SaveAs(rutaNueva, XlFileFormat.xlWorkbookNormal,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlExclusive,
                    Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value);
                MessageBox.Show("Reporte generado exitosamente.\n " + rutaNueva);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Por favor cierre el documento y vuelva a generar el reporte.\nError " + ex.Message.ToString());
            }

            mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        private void updateHighlight()
        {
            // Actualiza el highlight 
            highlightTable(1, !this.cb_Retardos.Checked);
            highlightTable(2, !this.cb_Anticipo.Checked);
            highlightTable(3, !this.cb_Excedente.Checked);
            highlightTable(4, !this.cb_NoReg.Checked);
        }

    }
}


