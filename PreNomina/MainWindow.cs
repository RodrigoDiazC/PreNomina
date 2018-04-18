using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Office.Interop.Excel;


namespace PreNomina
{
    public partial class Form1 : Form
    {
        // Variables globales
        public HorasLaborales horasL = new HorasLaborales();
        public bool retardoAnticipo = false;
        public String rutaFolder = "";
        public String departamento = "";

        List<Empleado> gEmpleados = new List<Empleado>();
        Analizador analizador = new Analizador();

        ToolStripDropDown dropDown = new ToolStripDropDown();

        int currentEmpleadoID = 1;
        int currentDay = 1;

        public Form1()
        {
            InitializeComponent();

            // Botones del toolstripdown
            this.tsd_Archivo.DropDown = dropDown;
            ToolStripButton botonAbrirPDF = new ToolStripButton();
            botonAbrirPDF.Text = "Abrir PDF";
            botonAbrirPDF.Click += new EventHandler(archivoDropDownClick);
            this.dropDown.Items.Add(botonAbrirPDF);

            try
            {
                // Configuración de usuario
                Properties.Settings.Default.Reload();

                this.rutaFolder = Properties.Settings.Default["Ruta"].ToString();
                this.departamento = Properties.Settings.Default["Departamento"].ToString();
                this.retardoAnticipo = (bool)Properties.Settings.Default["AnticipoRetardo"];
                // Setea los horarios laborales            
                this.horasL.entrada1 = DateTime.Parse(Properties.Settings.Default["Entrada1"].ToString().Replace(".", string.Empty).Replace(" m", "m"));
                this.horasL.salida1 = DateTime.Parse(Properties.Settings.Default["Salida1"].ToString().Replace(".", string.Empty).Replace(" m", "m"));
                this.horasL.entrada2 = DateTime.Parse(Properties.Settings.Default["Entrada2"].ToString().Replace(".", string.Empty).Replace(" m", "m"));
                this.horasL.salida2 = DateTime.Parse(Properties.Settings.Default["Salida2"].ToString().Replace(".", string.Empty).Replace(" m", "m"));
                this.horasL.limiteRetardo = TimeSpan.FromMinutes((int)Properties.Settings.Default["TiempoLimite"]);

                // LLena la lista de archivos en el historial
                if (Properties.Settings.Default.Archivos != null)
                {
                    foreach (string archivo in Properties.Settings.Default.Archivos)
                    {
                        ToolStripButton botonArchivo = new ToolStripButton();
                        botonArchivo.Text = archivo;
                        botonArchivo.Click += new EventHandler(archivoDropDownClick);
                        this.dropDown.Items.Add(botonArchivo);
                    }
                }

            }
            catch (FormatException e)
            {
                // Setea los horarios laborales por default     
                this.horasL.entrada1 = DateTime.Parse("08:00");
                this.horasL.salida1 = DateTime.Parse("13:00");
                this.horasL.entrada2 = DateTime.Parse("14:00");
                this.horasL.salida2 = DateTime.Parse("18:00");
                this.horasL.limiteRetardo = TimeSpan.Parse("00:30:00");
                MessageBox.Show("Ocurrio un error al leer el archivo de configuración. Parámetros por default establecidos.\n\n Error: " + e.Message);
            }
        }

        // Menu dropdown Archivo
        private void archivoDropDownClick(object sender, EventArgs e)
        {
            ToolStripButton senderButton = (ToolStripButton)sender;
            if (senderButton.Text.Equals("Abrir PDF")) abrirArchivo();
            else abrirArchivo(senderButton.Text);
        }

        // Abre PDF
        private void abrirArchivo(string fileName = null)
        {
            if (fileName == null)
            {
                // Abre archivo PDF
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Title = "Seleccionar el PDF a escanear";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.DefaultExt = "pdf";
                openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = null;
                openFileDialog.ShowDialog();
            }
            else openFileDialog.FileName = fileName;

            if (openFileDialog.FileName != "" )
            {
                // Obtiene empleados
                String archivoTexto = ExtractTextFromPdf(openFileDialog.FileName);

                if (archivoTexto.Contains("Desglose de Registros de Asistencia"))
                {
                    this.gEmpleados = analizador.getEmpleados(archivoTexto, horasL);

                    // Muestra empleados en tabla
                    generateTable(gEmpleados);

                    // Abre PDF en web browser
                    this.wb_pdfViewer.Navigate(openFileDialog.FileName);

                    // Inicializa la tabla secundaria con el primer empleado
                    this.currentEmpleadoID = 0;
                    fillTablaRegistros(this.gEmpleados[currentEmpleadoID]);
                    setEmpleadoPropiedadesUI(this.gEmpleados[currentEmpleadoID]);
                    updateHighlight();

                    // Inicializa la tabla general
                    fillTablaGeneral(this.gEmpleados);

                    // Pone titulo a la ventana
                    this.Text = "Prenomina " + analizador.fechaInicio.Day + " " + analizador.fechaInicio.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture) + " al " + analizador.fechaFin.Day + " " + analizador.fechaFin.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture);

                    // Guarda el nombre del archivo en el historial
                    addHistorialArchivo(openFileDialog.FileName);

                }
                else MessageBox.Show("Formato de archivo no soportado. Compruebe el archivo seleccionado.");
            }
        }

        // Guarda el nombre del archivo en el historial
        private void addHistorialArchivo(string fileName)
        {
            bool alreadyExists = false;

            if (Properties.Settings.Default.Archivos == null) Properties.Settings.Default.Archivos = new System.Collections.Specialized.StringCollection();

            foreach (ToolStripButton item in this.dropDown.Items)
            {
                var perro = item.Text;
                if (item.Text == fileName) alreadyExists = true;
            }

            if (!alreadyExists)
            {
                ToolStripButton boton = new ToolStripButton();
                boton.Text = fileName;
                boton.Click += new EventHandler(archivoDropDownClick);
                if (this.dropDown.Items.Count < 4)
                {
                    Properties.Settings.Default.Archivos.Add(fileName);
                    this.dropDown.Items.Add(boton);
                }
                else
                {
                    Properties.Settings.Default.Archivos.RemoveAt(0);
                    Properties.Settings.Default.Archivos.Add(fileName);

                    this.dropDown.Items.RemoveAt(1);
                    this.dropDown.Items.Add(boton);
                }

                Properties.Settings.Default.Save();
            }
        }

        // Despliega información del usuario
        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Elimina la información
                dataGrid1.DataSource = null;
                // Obtiene ID del empleado
                this.currentEmpleadoID = (int)this.dataGrid.Rows[e.RowIndex].Cells[0].Value;
                // Tamaño de celda automático
                this.dataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // LLena tabla e información
                fillTablaRegistros(this.gEmpleados[currentEmpleadoID]);
                setEmpleadoPropiedadesUI(this.gEmpleados[currentEmpleadoID]);

                // Selecciona el mismo row en la pantalla de vista general y aplica formato
                if (tabControl1.SelectedIndex == 1 && sender == this.dataGrid) // Restringe a que sea el el que llama a la funcion para evitar loops
                {
                    dg_General_CellClick(this.dataGrid, new DataGridViewCellEventArgs(0, this.currentEmpleadoID));
                }

                // Actualiza el highlight 
                updateHighlight();
            }
        }

        // Selección en tabla general
        private void dg_General_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex != this.currentEmpleadoID || sender != this.dg_General)
            {
                formatTablaGeneral(e.RowIndex);
                // Selecciona en la tabla de empleados tambien ejecuta el evento click
                this.dataGrid.ClearSelection();
                this.dataGrid.Rows[e.RowIndex].Cells[1].Selected = true;
                dataGrid_CellClick(this.dg_General, new DataGridViewCellEventArgs(1, e.RowIndex));
                // Selecciona casilla previa
                this.dg_General.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
        }

        // Cambios en vista preliminar
        private void dg_General_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dg_General.Columns[e.ColumnIndex].Name.Equals("Puntualidad") || this.dg_General.Columns[e.ColumnIndex].Name.Equals("Asistencia") || this.dg_General.Columns[e.ColumnIndex].Name.Equals("Desempeño"))
            {
                switch (this.dg_General.Columns[e.ColumnIndex].Name)
                {
                    case "Puntualidad":
                        this.gEmpleados[currentEmpleadoID].Puntualidad = (bool)this.dg_General[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case "Asistencia":
                        this.gEmpleados[currentEmpleadoID].Asistencia = (bool)this.dg_General[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case "Desempeño":
                        this.gEmpleados[currentEmpleadoID].Desempeno = (bool)this.dg_General[e.ColumnIndex, e.RowIndex].Value;
                        break;
                }

                fillTablaGeneral(this.gEmpleados, e.RowIndex);
            }
        }

        // LLena tabla de empleados
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
            int k = 0;

            foreach (Empleado e in empleados)
            {
                // ID
                DataRow dr = dt.NewRow();
                dr[k++] = e.ID;
                dr[k++] = e.Nombre;
                dt.Rows.Add(dr);
                k = 0;
            }

            // Envía datos a control
            this.dataGrid.DataSource = dt;

            // Propiedades dela tabla
            this.dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn col in this.dataGrid.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add(new DataColumn("Acceso", typeof(string)));

                foreach (TiemposDia t in em.Dias)
                {
                    dt.Columns.Add(new DataColumn(t.dia.ToShortDateString(), typeof(TimeSpan)));
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

                // Deshabilita ordenamiento de columnas
                foreach (DataGridViewColumn column in this.dataGrid1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }
            catch // Cuando se hace sort tambien se llama a este evento y provoca una exepción
            {

            }

        }

        // Tabla de vista general
        private void fillTablaGeneral(List<Empleado> empleados, int setSelection = 0)
        {
            // Genera los headers (COLUMNAS)  -------------------------------------------------------
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("Departamento", typeof(string)));

            // Obtiene el empleado con más días
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
            foreach (TiemposDia t in empleados[emID].Dias)
            {
                dt.Columns.Add(new DataColumn(t.dia.Day.ToString(), typeof(string)));
            }

            dt.Columns.Add(new DataColumn("TOT", typeof(int)));
            dt.Columns.Add(new DataColumn("Puntualidad", typeof(bool)));
            dt.Columns.Add(new DataColumn("Asistencia", typeof(bool)));
            dt.Columns.Add(new DataColumn("Desempeño", typeof(bool)));

            // Genera las rows (FILAS) -------------------------------------------------------------------------------
            foreach (Empleado em in empleados)
            {
                DataRow fila = dt.NewRow();
                int i = 0;

                // Departamento
                fila[i++] = this.departamento;

                for (int t = 0; t < em.Dias.Count; t++)
                {
                    skipCol:
                    if (dt.Columns[i].ColumnName == em.Dias[t].dia.Day.ToString())
                    {
                        fila[i++] = em.Dias[t].status;
                    }
                    else
                    {
                        i++;
                        goto skipCol;
                    }
                }

                // TOT
                if (this.retardoAnticipo) fila[i++] = em.getRetardoTotal(horasL).TotalMinutes + em.getAnticipoTotal(horasL).TotalMinutes;
                else fila[i++] = em.getRetardoTotal(horasL).TotalMinutes;
                // Puntualidad
                fila[i++] = em.Puntualidad;
                // Asistencia
                fila[i++] = em.Asistencia;
                // Asistencia
                fila[i++] = em.Desempeno;

                dt.Rows.Add(fila);
            }

            // Pasa a tabla
            this.dg_General.DataSource = dt;

            // Propiedades dela tabla
            this.dg_General.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg_General.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in this.dg_General.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = true;
                if (col.Name.Equals("Puntualidad") || col.Name.Equals("Asistencia") || col.Name.Equals("Desempeño")) col.ReadOnly = false;
            }

            // Formatea la tabla general y selecciona la fila correspondiente
            formatTablaGeneral(setSelection);
        }

        //Setea atributos del empleado en la interfaz
        private void setEmpleadoPropiedadesUI(Empleado em)
        {
            this.cb_Asistencia.Checked = em.Asistencia;
            this.cb_Puntualidad.Checked = em.Puntualidad;
            this.cb_Desempeno.Checked = em.Desempeno;
            this.tb_NombreEmpleado.Text = em.Nombre;

            if (retardoAnticipo) this.lb_TotRet.Text = (((int)em.getRetardoTotal(horasL).TotalMinutes) + ((int)em.getAnticipoTotal(horasL).TotalMinutes)).ToString();
            else this.lb_TotRet.Text = ((int)em.getRetardoTotal(horasL).TotalMinutes).ToString();

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
                this.groupBox4.Text = em.Dias[diaIndx].dia.ToLongDateString();

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
            this.currentDay = e.ColumnIndex - 1;
            setEmpleadoTimeUI(this.gEmpleados[currentEmpleadoID], e.ColumnIndex - 1, e.RowIndex);
        }

        // Exporta tabla a excel
        private void tsb_Exportar_Click(object sender, EventArgs e)
        {
            //---- Ruta del archivo plantilla
            string rutaTemplate = Directory.GetCurrentDirectory() + "\\Template.xlsx";

            if (File.Exists(rutaTemplate))
            {
                if (this.gEmpleados.Count > 0)
                {
                    string rutaCompleta = "";

                    //--- Nueva ruta del archivo
                    string nombreArchivo = "\\Prenomina " + analizador.fechaInicio.Day + " " + analizador.fechaInicio.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture) + " al " + analizador.fechaFin.Day + " " + analizador.fechaFin.ToString("MMMM", System.Globalization.CultureInfo.CurrentCulture) + ".xls";

                    //--- Toolkit para Excel ----//
                    Workbook mWorkBook;
                    Sheets mWorkSheets;
                    Worksheet mWSheet1;
                    Microsoft.Office.Interop.Excel.Application oXL;

                    //--- Creando objeto y configurando parametros
                    oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = false;    //Para que no abra la ventana de excel
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

                    //--------------------------------------------- Nombres de los empleados y departamento
                    int k = 0;
                    int cantRows = 0;
                    foreach (Empleado em in this.gEmpleados)
                    {
                        mWSheet1.Cells[3][5 + (k)] = this.departamento;
                        mWSheet1.Cells[2][5 + (k++)] = em.Nombre;
                    }
                    cantRows = k;
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

                    // ----------------------------------------------------------------------- Comentarios
                    mWSheet1.Cells[3][cantRows + 1 + 5] = this.tb_Comentarios.Text;

                    // ---------------------------------------------------------------------- Guarda el nuevo reporte
                    try
                    {
                        if (this.rutaFolder == "")
                        {
                            var folder = new FolderBrowserDialog();
                            folder.Description = "Seleccione el directrio de destino";

                            while (this.rutaFolder == "")
                            {
                                folder.ShowDialog();
                                this.rutaFolder = folder.SelectedPath;
                            }

                            rutaCompleta = this.rutaFolder + nombreArchivo;

                            // Guarda ruta en archivo de configuración
                            Properties.Settings.Default["Ruta"] = this.rutaFolder;
                            Properties.Settings.Default.Save();

                        }
                        else rutaCompleta = this.rutaFolder + nombreArchivo;

                        mWorkBook.SaveAs(rutaCompleta, XlFileFormat.xlWorkbookNormal,
                            Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlExclusive,
                            Missing.Value, Missing.Value, Missing.Value,
                            Missing.Value, Missing.Value);
                        MessageBox.Show("Reporte generado exitosamente.\n " + rutaCompleta);
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


                    // Abre archivo
                    System.Diagnostics.Process.Start(rutaCompleta);

                }
                else MessageBox.Show("No hay información para exportar. Abra un archivo primero.");
            }
            else MessageBox.Show("No se ha encontrado plantilla. Contacte a desarrollador.");
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

            // Vuelve a analizar asistencia
            this.gEmpleados[currentEmpleadoID].checkAsistenciaUpdate();
            setEmpleadoPropiedadesUI(this.gEmpleados[currentEmpleadoID]);

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
        private void updateHighlight()
        {
            // Actualiza el highlight 
            highlightTable(1, !this.cb_Retardos.Checked);
            highlightTable(2, !this.cb_Anticipo.Checked);
            highlightTable(3, !this.cb_Excedente.Checked);
            highlightTable(4, !this.cb_NoReg.Checked);
        }

        // Vuelve a cargar la tabla de preview (General)
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.gEmpleados.Count != 0)
            {
                if (this.tabControl1.SelectedTab.Name == "tp_General")
                {
                    fillTablaGeneral(this.gEmpleados, this.currentEmpleadoID);
                }
            }
        }

        // Ventana de opciones
        private void tsb_Opciones_Click(object sender, EventArgs e)
        {
            Form ventanaOpciones = new Opciones();
            ventanaOpciones.Show();
        }

        // Vuelve a cargar todas las tablas
        public void updateConfig()
        {
            if (this.gEmpleados.Count > 0)
            {
                foreach (Empleado em in this.gEmpleados)
                {
                    em.Puntualidad = em.checkPuntualidad(this.horasL, this.retardoAnticipo);
                }

                fillTablaRegistros(this.gEmpleados[currentEmpleadoID]);
                setEmpleadoPropiedadesUI(this.gEmpleados[currentEmpleadoID]);

                updateHighlight();
            }

            // Guarda configuración de usuario
            Properties.Settings.Default["Ruta"] = this.rutaFolder;
            Properties.Settings.Default["Departamento"] = this.departamento;
            Properties.Settings.Default["AnticipoRetardo"] = this.retardoAnticipo;
            Properties.Settings.Default["Entrada1"] = this.horasL.entrada1.ToShortTimeString();
            Properties.Settings.Default["Salida1"] = this.horasL.salida1.ToShortTimeString();
            Properties.Settings.Default["Entrada2"] = this.horasL.entrada2.ToShortTimeString();
            Properties.Settings.Default["Salida2"] = this.horasL.salida2.ToShortTimeString();
            Properties.Settings.Default["TiempoLimite"] = (int)this.horasL.limiteRetardo.TotalMinutes;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        // Abre el folder donde se guardan los archivos
        private void tsb_AbrirFolder_Click(object sender, EventArgs e)
        {
            if (this.rutaFolder != "")
            {
                System.Diagnostics.Process.Start(this.rutaFolder);
            }
        }

        // Formato para tabla general
        private void formatTablaGeneral(int rowIdx)
        {
            // Limpia formato anterior
            ((System.Data.DataTable)this.dg_General.DataSource).AcceptChanges();
            this.dg_General.ClearSelection();

            for (int i = 1; i < this.dg_General.Columns.Count; i++)
            {
                // Clasificación del dia
                if (this.dg_General.Columns[i].ValueType == typeof(string))
                {
                    if (this.dg_General[i, rowIdx].Value.ToString() != "A")
                        this.dg_General[i, rowIdx].Style.BackColor = Color.Yellow;
                }

                // Puntualidad asistencua desempeño
                else if (this.dg_General.Columns[i].ValueType == typeof(Boolean))
                {
                    if ((bool)this.dg_General[i, rowIdx].Value) this.dg_General[i, rowIdx].Style.BackColor = Color.LightGreen;
                    else this.dg_General[i, rowIdx].Style.BackColor = Color.LightSalmon;
                }
            }

            // Formato rojo a los minutos que sobrepasen el tiempo máximi
            for (int i = 0; i < this.dg_General.Rows.Count; i++)
            {
                if ((int)this.dg_General["TOT", i].Value > horasL.limiteRetardo.TotalMinutes) this.dg_General["TOT", i].Style.ForeColor = Color.Red;
            }

            this.dg_General[0, rowIdx].Selected = true;
        }

        // Redirige a tabla detallada al hacer doble click sobre el día a inspeccionar
        private void dg_General_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool regExists = false;

            if ((e.ColumnIndex > 0) && (e.ColumnIndex < this.dg_General.ColumnCount - 4))
            {
                // Variable para seleccionar el día apropiado
                int dia = int.Parse(this.dg_General.Columns[e.ColumnIndex].Name);

                // Seleccciona el dia en la tabla detallada
                foreach (DataGridViewColumn col in this.dataGrid1.Columns)
                {
                    if (col.ValueType == typeof(string)) continue;

                    if ((DateTime.Parse(col.Name).Day == dia))
                    {
                        this.dataGrid1.ClearSelection();
                        this.dataGrid1[col.Name, 0].Selected = true;

                        this.dataGrid1.FirstDisplayedScrollingColumnIndex = col.Index > 3 ? col.Index - 2 : col.Index;

                        dataGrid1_CellClick(this.dataGrid1, new DataGridViewCellEventArgs(col.Index, 0));
                        // Cambia de pantalla
                        this.tabControl1.SelectedIndex = 0;
                        regExists = true;
                        break;
                    }

                }

                if (!regExists) MessageBox.Show("El empleado seleccionado no tiene registros del día " + this.dg_General.Columns[e.ColumnIndex].Name + ".");
            }

        }


    }
}


