namespace PreNomina
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bt_Abrir = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_Abrir = new System.Windows.Forms.ToolStripButton();
            this.tsb_Exportar = new System.Windows.Forms.ToolStripButton();
            this.tsb_AbrirFolder = new System.Windows.Forms.ToolStripButton();
            this.tsb_Opciones = new System.Windows.Forms.ToolStripButton();
            this.lb_Empleados = new System.Windows.Forms.Label();
            this.lb_Horarios = new System.Windows.Forms.Label();
            this.cb_Puntualidad = new System.Windows.Forms.CheckBox();
            this.cb_Asistencia = new System.Windows.Forms.CheckBox();
            this.cb_Desempeno = new System.Windows.Forms.CheckBox();
            this.tb_NombreEmpleado = new System.Windows.Forms.TextBox();
            this.lb_Nombre = new System.Windows.Forms.Label();
            this.cb_Retardos = new System.Windows.Forms.CheckBox();
            this.cb_Anticipo = new System.Windows.Forms.CheckBox();
            this.cb_Excedente = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_TotExc = new System.Windows.Forms.Label();
            this.lb_TotRet = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_Observaciones = new System.Windows.Forms.TextBox();
            this.lb_Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_Sta = new System.Windows.Forms.Label();
            this.tb_Acceso = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb_Incapacidad = new System.Windows.Forms.RadioButton();
            this.rb_Falta = new System.Windows.Forms.RadioButton();
            this.rb_Vacaciones = new System.Windows.Forms.RadioButton();
            this.rb_Permiso = new System.Windows.Forms.RadioButton();
            this.rb_TrabajoF = new System.Windows.Forms.RadioButton();
            this.rb_Asistencia = new System.Windows.Forms.RadioButton();
            this.cb_NoReg = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_Detallada = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.tp_General = new System.Windows.Forms.TabPage();
            this.lb_Indicaciones = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dg_General = new System.Windows.Forms.DataGridView();
            this.tb_Comentarios = new System.Windows.Forms.TextBox();
            this.tp_PDF = new System.Windows.Forms.TabPage();
            this.wb_pdfViewer = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_Detallada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.tp_General.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_General)).BeginInit();
            this.tp_PDF.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // bt_Abrir
            // 
            this.bt_Abrir.Location = new System.Drawing.Point(0, 0);
            this.bt_Abrir.Name = "bt_Abrir";
            this.bt_Abrir.Size = new System.Drawing.Size(75, 23);
            this.bt_Abrir.TabIndex = 4;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 48);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(270, 341);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Abrir,
            this.tsb_Exportar,
            this.tsb_AbrirFolder,
            this.tsb_Opciones});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(988, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_Abrir
            // 
            this.tsb_Abrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Abrir.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Abrir.Image")));
            this.tsb_Abrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Abrir.Name = "tsb_Abrir";
            this.tsb_Abrir.Size = new System.Drawing.Size(79, 22);
            this.tsb_Abrir.Text = "Abrir archivo";
            this.tsb_Abrir.Click += new System.EventHandler(this.tsb_Abrir_Click);
            // 
            // tsb_Exportar
            // 
            this.tsb_Exportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Exportar.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exportar.Image")));
            this.tsb_Exportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exportar.Name = "tsb_Exportar";
            this.tsb_Exportar.Size = new System.Drawing.Size(92, 22);
            this.tsb_Exportar.Text = "Exportar a excel";
            this.tsb_Exportar.Click += new System.EventHandler(this.tsb_Exportar_Click);
            // 
            // tsb_AbrirFolder
            // 
            this.tsb_AbrirFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_AbrirFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AbrirFolder.Image")));
            this.tsb_AbrirFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AbrirFolder.Name = "tsb_AbrirFolder";
            this.tsb_AbrirFolder.Size = new System.Drawing.Size(79, 22);
            this.tsb_AbrirFolder.Text = "Abrir carpeta";
            this.tsb_AbrirFolder.Click += new System.EventHandler(this.tsb_AbrirFolder_Click);
            // 
            // tsb_Opciones
            // 
            this.tsb_Opciones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Opciones.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Opciones.Image")));
            this.tsb_Opciones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Opciones.Name = "tsb_Opciones";
            this.tsb_Opciones.Size = new System.Drawing.Size(61, 22);
            this.tsb_Opciones.Text = "Opciones";
            this.tsb_Opciones.Click += new System.EventHandler(this.tsb_Opciones_Click);
            // 
            // lb_Empleados
            // 
            this.lb_Empleados.AutoSize = true;
            this.lb_Empleados.Location = new System.Drawing.Point(12, 30);
            this.lb_Empleados.Name = "lb_Empleados";
            this.lb_Empleados.Size = new System.Drawing.Size(98, 13);
            this.lb_Empleados.TabIndex = 5;
            this.lb_Empleados.Text = "Lista de empleados";
            // 
            // lb_Horarios
            // 
            this.lb_Horarios.AutoSize = true;
            this.lb_Horarios.Location = new System.Drawing.Point(6, 16);
            this.lb_Horarios.Name = "lb_Horarios";
            this.lb_Horarios.Size = new System.Drawing.Size(97, 13);
            this.lb_Horarios.TabIndex = 5;
            this.lb_Horarios.Text = "Detalle de registros";
            // 
            // cb_Puntualidad
            // 
            this.cb_Puntualidad.AutoSize = true;
            this.cb_Puntualidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Puntualidad.Location = new System.Drawing.Point(9, 81);
            this.cb_Puntualidad.Name = "cb_Puntualidad";
            this.cb_Puntualidad.Size = new System.Drawing.Size(92, 19);
            this.cb_Puntualidad.TabIndex = 6;
            this.cb_Puntualidad.Text = "Puntualidad";
            this.cb_Puntualidad.UseVisualStyleBackColor = true;
            this.cb_Puntualidad.CheckedChanged += new System.EventHandler(this.cb_Puntualidad_CheckedChanged);
            // 
            // cb_Asistencia
            // 
            this.cb_Asistencia.AutoSize = true;
            this.cb_Asistencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Asistencia.Location = new System.Drawing.Point(9, 106);
            this.cb_Asistencia.Name = "cb_Asistencia";
            this.cb_Asistencia.Size = new System.Drawing.Size(81, 19);
            this.cb_Asistencia.TabIndex = 6;
            this.cb_Asistencia.Text = "Asistencia";
            this.cb_Asistencia.UseVisualStyleBackColor = true;
            this.cb_Asistencia.CheckedChanged += new System.EventHandler(this.cb_Asistencia_CheckedChanged);
            // 
            // cb_Desempeno
            // 
            this.cb_Desempeno.AutoSize = true;
            this.cb_Desempeno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Desempeno.Location = new System.Drawing.Point(9, 131);
            this.cb_Desempeno.Name = "cb_Desempeno";
            this.cb_Desempeno.Size = new System.Drawing.Size(94, 19);
            this.cb_Desempeno.TabIndex = 6;
            this.cb_Desempeno.Text = "Desempeño";
            this.cb_Desempeno.UseVisualStyleBackColor = true;
            this.cb_Desempeno.CheckedChanged += new System.EventHandler(this.cb_Desempeno_CheckedChanged);
            // 
            // tb_NombreEmpleado
            // 
            this.tb_NombreEmpleado.Location = new System.Drawing.Point(6, 41);
            this.tb_NombreEmpleado.Name = "tb_NombreEmpleado";
            this.tb_NombreEmpleado.ReadOnly = true;
            this.tb_NombreEmpleado.Size = new System.Drawing.Size(273, 20);
            this.tb_NombreEmpleado.TabIndex = 8;
            // 
            // lb_Nombre
            // 
            this.lb_Nombre.AutoSize = true;
            this.lb_Nombre.Location = new System.Drawing.Point(6, 21);
            this.lb_Nombre.Name = "lb_Nombre";
            this.lb_Nombre.Size = new System.Drawing.Size(47, 13);
            this.lb_Nombre.TabIndex = 9;
            this.lb_Nombre.Text = "Nombre:";
            // 
            // cb_Retardos
            // 
            this.cb_Retardos.AutoSize = true;
            this.cb_Retardos.Checked = true;
            this.cb_Retardos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Retardos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Retardos.Location = new System.Drawing.Point(334, 16);
            this.cb_Retardos.Name = "cb_Retardos";
            this.cb_Retardos.Size = new System.Drawing.Size(76, 19);
            this.cb_Retardos.TabIndex = 10;
            this.cb_Retardos.Text = "Retardos";
            this.cb_Retardos.UseVisualStyleBackColor = true;
            this.cb_Retardos.CheckedChanged += new System.EventHandler(this.cb_Retardos_CheckedChanged);
            // 
            // cb_Anticipo
            // 
            this.cb_Anticipo.AutoSize = true;
            this.cb_Anticipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Anticipo.Location = new System.Drawing.Point(517, 16);
            this.cb_Anticipo.Name = "cb_Anticipo";
            this.cb_Anticipo.Size = new System.Drawing.Size(75, 19);
            this.cb_Anticipo.TabIndex = 10;
            this.cb_Anticipo.Text = "Anticipos";
            this.cb_Anticipo.UseVisualStyleBackColor = true;
            this.cb_Anticipo.CheckedChanged += new System.EventHandler(this.cb_Anticipo_CheckedChanged);
            // 
            // cb_Excedente
            // 
            this.cb_Excedente.AutoSize = true;
            this.cb_Excedente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Excedente.Location = new System.Drawing.Point(597, 16);
            this.cb_Excedente.Name = "cb_Excedente";
            this.cb_Excedente.Size = new System.Drawing.Size(84, 19);
            this.cb_Excedente.TabIndex = 10;
            this.cb_Excedente.Text = "Excedente";
            this.cb_Excedente.UseVisualStyleBackColor = true;
            this.cb_Excedente.CheckedChanged += new System.EventHandler(this.cb_Excedente_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_TotExc);
            this.groupBox1.Controls.Add(this.lb_TotRet);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_NombreEmpleado);
            this.groupBox1.Controls.Add(this.lb_Nombre);
            this.groupBox1.Controls.Add(this.cb_Puntualidad);
            this.groupBox1.Controls.Add(this.cb_Asistencia);
            this.groupBox1.Controls.Add(this.cb_Desempeno);
            this.groupBox1.Location = new System.Drawing.Point(6, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 164);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info General";
            // 
            // lb_TotExc
            // 
            this.lb_TotExc.AutoSize = true;
            this.lb_TotExc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_TotExc.Location = new System.Drawing.Point(238, 110);
            this.lb_TotExc.Name = "lb_TotExc";
            this.lb_TotExc.Size = new System.Drawing.Size(0, 15);
            this.lb_TotExc.TabIndex = 11;
            // 
            // lb_TotRet
            // 
            this.lb_TotRet.AutoSize = true;
            this.lb_TotRet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_TotRet.Location = new System.Drawing.Point(238, 85);
            this.lb_TotRet.Name = "lb_TotRet";
            this.lb_TotRet.Size = new System.Drawing.Size(0, 15);
            this.lb_TotRet.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(144, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Tot. Exc. (min):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Tot. Retardo (min):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_Observaciones);
            this.groupBox2.Controls.Add(this.lb_Status);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lb_Sta);
            this.groupBox2.Controls.Add(this.tb_Acceso);
            this.groupBox2.Location = new System.Drawing.Point(308, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 77);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info de Registro";
            // 
            // tb_Observaciones
            // 
            this.tb_Observaciones.Location = new System.Drawing.Point(142, 41);
            this.tb_Observaciones.Name = "tb_Observaciones";
            this.tb_Observaciones.ReadOnly = true;
            this.tb_Observaciones.Size = new System.Drawing.Size(226, 20);
            this.tb_Observaciones.TabIndex = 13;
            this.tb_Observaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Location = new System.Drawing.Point(49, 21);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(0, 13);
            this.lb_Status.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Observaciones:";
            // 
            // lb_Sta
            // 
            this.lb_Sta.AutoSize = true;
            this.lb_Sta.Location = new System.Drawing.Point(3, 21);
            this.lb_Sta.Name = "lb_Sta";
            this.lb_Sta.Size = new System.Drawing.Size(40, 13);
            this.lb_Sta.TabIndex = 12;
            this.lb_Sta.Text = "Status:";
            // 
            // tb_Acceso
            // 
            this.tb_Acceso.Location = new System.Drawing.Point(6, 41);
            this.tb_Acceso.Name = "tb_Acceso";
            this.tb_Acceso.ReadOnly = true;
            this.tb_Acceso.Size = new System.Drawing.Size(108, 20);
            this.tb_Acceso.TabIndex = 8;
            this.tb_Acceso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_Incapacidad);
            this.groupBox4.Controls.Add(this.rb_Falta);
            this.groupBox4.Controls.Add(this.rb_Vacaciones);
            this.groupBox4.Controls.Add(this.rb_Permiso);
            this.groupBox4.Controls.Add(this.rb_TrabajoF);
            this.groupBox4.Controls.Add(this.rb_Asistencia);
            this.groupBox4.Location = new System.Drawing.Point(308, 254);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 83);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Info de Día";
            this.groupBox4.Validated += new System.EventHandler(this.groupBox4_Validated);
            // 
            // rb_Incapacidad
            // 
            this.rb_Incapacidad.AutoSize = true;
            this.rb_Incapacidad.Location = new System.Drawing.Point(119, 53);
            this.rb_Incapacidad.Name = "rb_Incapacidad";
            this.rb_Incapacidad.Size = new System.Drawing.Size(84, 17);
            this.rb_Incapacidad.TabIndex = 13;
            this.rb_Incapacidad.TabStop = true;
            this.rb_Incapacidad.Text = "Incapacidad";
            this.rb_Incapacidad.UseVisualStyleBackColor = true;
            // 
            // rb_Falta
            // 
            this.rb_Falta.AutoSize = true;
            this.rb_Falta.Location = new System.Drawing.Point(207, 52);
            this.rb_Falta.Name = "rb_Falta";
            this.rb_Falta.Size = new System.Drawing.Size(48, 17);
            this.rb_Falta.TabIndex = 13;
            this.rb_Falta.TabStop = true;
            this.rb_Falta.Text = "Falta";
            this.rb_Falta.UseVisualStyleBackColor = true;
            // 
            // rb_Vacaciones
            // 
            this.rb_Vacaciones.AutoSize = true;
            this.rb_Vacaciones.Location = new System.Drawing.Point(119, 30);
            this.rb_Vacaciones.Name = "rb_Vacaciones";
            this.rb_Vacaciones.Size = new System.Drawing.Size(81, 17);
            this.rb_Vacaciones.TabIndex = 13;
            this.rb_Vacaciones.TabStop = true;
            this.rb_Vacaciones.Text = "Vacaciones";
            this.rb_Vacaciones.UseVisualStyleBackColor = true;
            // 
            // rb_Permiso
            // 
            this.rb_Permiso.AutoSize = true;
            this.rb_Permiso.Location = new System.Drawing.Point(206, 30);
            this.rb_Permiso.Name = "rb_Permiso";
            this.rb_Permiso.Size = new System.Drawing.Size(62, 17);
            this.rb_Permiso.TabIndex = 13;
            this.rb_Permiso.TabStop = true;
            this.rb_Permiso.Text = "Permiso";
            this.rb_Permiso.UseVisualStyleBackColor = true;
            // 
            // rb_TrabajoF
            // 
            this.rb_TrabajoF.AutoSize = true;
            this.rb_TrabajoF.Location = new System.Drawing.Point(12, 53);
            this.rb_TrabajoF.Name = "rb_TrabajoF";
            this.rb_TrabajoF.Size = new System.Drawing.Size(103, 17);
            this.rb_TrabajoF.TabIndex = 13;
            this.rb_TrabajoF.TabStop = true;
            this.rb_TrabajoF.Text = "Trabajo Foraneo";
            this.rb_TrabajoF.UseVisualStyleBackColor = true;
            // 
            // rb_Asistencia
            // 
            this.rb_Asistencia.AutoSize = true;
            this.rb_Asistencia.Location = new System.Drawing.Point(12, 30);
            this.rb_Asistencia.Name = "rb_Asistencia";
            this.rb_Asistencia.Size = new System.Drawing.Size(73, 17);
            this.rb_Asistencia.TabIndex = 13;
            this.rb_Asistencia.TabStop = true;
            this.rb_Asistencia.Text = "Asistencia";
            this.rb_Asistencia.UseVisualStyleBackColor = true;
            // 
            // cb_NoReg
            // 
            this.cb_NoReg.AutoSize = true;
            this.cb_NoReg.Checked = true;
            this.cb_NoReg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_NoReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_NoReg.Location = new System.Drawing.Point(415, 16);
            this.cb_NoReg.Name = "cb_NoReg";
            this.cb_NoReg.Size = new System.Drawing.Size(97, 19);
            this.cb_NoReg.TabIndex = 15;
            this.cb_NoReg.Text = "No Registros";
            this.cb_NoReg.UseVisualStyleBackColor = true;
            this.cb_NoReg.CheckedChanged += new System.EventHandler(this.cb_NoReg_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_Detallada);
            this.tabControl1.Controls.Add(this.tp_General);
            this.tabControl1.Controls.Add(this.tp_PDF);
            this.tabControl1.Location = new System.Drawing.Point(288, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 369);
            this.tabControl1.TabIndex = 16;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tp_Detallada
            // 
            this.tp_Detallada.BackColor = System.Drawing.SystemColors.Control;
            this.tp_Detallada.Controls.Add(this.dataGrid1);
            this.tp_Detallada.Controls.Add(this.cb_NoReg);
            this.tp_Detallada.Controls.Add(this.lb_Horarios);
            this.tp_Detallada.Controls.Add(this.groupBox4);
            this.tp_Detallada.Controls.Add(this.cb_Retardos);
            this.tp_Detallada.Controls.Add(this.groupBox2);
            this.tp_Detallada.Controls.Add(this.cb_Anticipo);
            this.tp_Detallada.Controls.Add(this.groupBox1);
            this.tp_Detallada.Controls.Add(this.cb_Excedente);
            this.tp_Detallada.Location = new System.Drawing.Point(4, 22);
            this.tp_Detallada.Name = "tp_Detallada";
            this.tp_Detallada.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Detallada.Size = new System.Drawing.Size(690, 343);
            this.tp_Detallada.TabIndex = 0;
            this.tp_Detallada.Text = "Vista Detallada";
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AllowUserToResizeRows = false;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid1.Location = new System.Drawing.Point(6, 40);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(676, 127);
            this.dataGrid1.TabIndex = 2;
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick);
            // 
            // tp_General
            // 
            this.tp_General.BackColor = System.Drawing.SystemColors.Control;
            this.tp_General.Controls.Add(this.lb_Indicaciones);
            this.tp_General.Controls.Add(this.label4);
            this.tp_General.Controls.Add(this.dg_General);
            this.tp_General.Controls.Add(this.tb_Comentarios);
            this.tp_General.Location = new System.Drawing.Point(4, 22);
            this.tp_General.Name = "tp_General";
            this.tp_General.Padding = new System.Windows.Forms.Padding(3);
            this.tp_General.Size = new System.Drawing.Size(690, 343);
            this.tp_General.TabIndex = 2;
            this.tp_General.Text = "Vista Preliminar";
            // 
            // lb_Indicaciones
            // 
            this.lb_Indicaciones.AutoSize = true;
            this.lb_Indicaciones.Location = new System.Drawing.Point(401, 212);
            this.lb_Indicaciones.Name = "lb_Indicaciones";
            this.lb_Indicaciones.Size = new System.Drawing.Size(283, 13);
            this.lb_Indicaciones.TabIndex = 19;
            this.lb_Indicaciones.Text = "Hacer doble click sobre la celda para obtener más detalles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Comentarios";
            // 
            // dg_General
            // 
            this.dg_General.AllowUserToAddRows = false;
            this.dg_General.AllowUserToDeleteRows = false;
            this.dg_General.AllowUserToResizeRows = false;
            this.dg_General.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_General.Location = new System.Drawing.Point(6, 2);
            this.dg_General.Name = "dg_General";
            this.dg_General.ReadOnly = true;
            this.dg_General.RowHeadersVisible = false;
            this.dg_General.Size = new System.Drawing.Size(678, 207);
            this.dg_General.TabIndex = 3;
            this.dg_General.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_General_CellClick);
            this.dg_General.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_General_CellDoubleClick);
            // 
            // tb_Comentarios
            // 
            this.tb_Comentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Comentarios.Location = new System.Drawing.Point(6, 241);
            this.tb_Comentarios.Multiline = true;
            this.tb_Comentarios.Name = "tb_Comentarios";
            this.tb_Comentarios.Size = new System.Drawing.Size(678, 96);
            this.tb_Comentarios.TabIndex = 17;
            // 
            // tp_PDF
            // 
            this.tp_PDF.BackColor = System.Drawing.SystemColors.Control;
            this.tp_PDF.Controls.Add(this.wb_pdfViewer);
            this.tp_PDF.Location = new System.Drawing.Point(4, 22);
            this.tp_PDF.Name = "tp_PDF";
            this.tp_PDF.Padding = new System.Windows.Forms.Padding(3);
            this.tp_PDF.Size = new System.Drawing.Size(690, 343);
            this.tp_PDF.TabIndex = 1;
            this.tp_PDF.Text = "Vista PDF";
            // 
            // wb_pdfViewer
            // 
            this.wb_pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb_pdfViewer.Location = new System.Drawing.Point(3, 3);
            this.wb_pdfViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_pdfViewer.Name = "wb_pdfViewer";
            this.wb_pdfViewer.Size = new System.Drawing.Size(684, 337);
            this.wb_pdfViewer.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 398);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lb_Empleados);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.bt_Abrir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PreNomina";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tp_Detallada.ResumeLayout(false);
            this.tp_Detallada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.tp_General.ResumeLayout(false);
            this.tp_General.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_General)).EndInit();
            this.tp_PDF.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button bt_Abrir;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_Abrir;
        private System.Windows.Forms.Label lb_Empleados;
        private System.Windows.Forms.Label lb_Horarios;
        private System.Windows.Forms.CheckBox cb_Puntualidad;
        private System.Windows.Forms.CheckBox cb_Asistencia;
        private System.Windows.Forms.CheckBox cb_Desempeno;
        private System.Windows.Forms.TextBox tb_NombreEmpleado;
        private System.Windows.Forms.Label lb_Nombre;
        private System.Windows.Forms.CheckBox cb_Retardos;
        private System.Windows.Forms.CheckBox cb_Anticipo;
        private System.Windows.Forms.CheckBox cb_Excedente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_TotExc;
        private System.Windows.Forms.Label lb_TotRet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_Sta;
        private System.Windows.Forms.TextBox tb_Acceso;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Observaciones;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rb_Asistencia;
        private System.Windows.Forms.RadioButton rb_Incapacidad;
        private System.Windows.Forms.RadioButton rb_Falta;
        private System.Windows.Forms.RadioButton rb_Vacaciones;
        private System.Windows.Forms.RadioButton rb_Permiso;
        private System.Windows.Forms.RadioButton rb_TrabajoF;
        private System.Windows.Forms.CheckBox cb_NoReg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_Detallada;
        private System.Windows.Forms.TabPage tp_PDF;
        private System.Windows.Forms.WebBrowser wb_pdfViewer;
        private System.Windows.Forms.TextBox tb_Comentarios;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tp_General;
        private System.Windows.Forms.DataGridView dg_General;
        private System.Windows.Forms.DataGridView dataGrid1;
        private System.Windows.Forms.ToolStripButton tsb_Exportar;
        private System.Windows.Forms.ToolStripButton tsb_Opciones;
        private System.Windows.Forms.ToolStripButton tsb_AbrirFolder;
        private System.Windows.Forms.Label lb_Indicaciones;
    }
}

