namespace TimeChecker
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
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_Abrir = new System.Windows.Forms.ToolStripButton();
            this.tsb_Exportar = new System.Windows.Forms.ToolStripButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.dataGrid.Location = new System.Drawing.Point(12, 46);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(270, 307);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AllowUserToResizeRows = false;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid1.Location = new System.Drawing.Point(296, 46);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(676, 127);
            this.dataGrid1.TabIndex = 2;
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Abrir,
            this.tsb_Exportar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
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
            this.tsb_Exportar.Text = "Exportar a Excel";
            this.tsb_Exportar.ToolTipText = "Exportar a Excel";
            this.tsb_Exportar.Click += new System.EventHandler(this.tsb_Exportar_Click);
            // 
            // lb_Empleados
            // 
            this.lb_Empleados.AutoSize = true;
            this.lb_Empleados.Location = new System.Drawing.Point(12, 25);
            this.lb_Empleados.Name = "lb_Empleados";
            this.lb_Empleados.Size = new System.Drawing.Size(98, 13);
            this.lb_Empleados.TabIndex = 5;
            this.lb_Empleados.Text = "Lista de empleados";
            // 
            // lb_Horarios
            // 
            this.lb_Horarios.AutoSize = true;
            this.lb_Horarios.Location = new System.Drawing.Point(293, 25);
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
            this.cb_Retardos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Retardos.Location = new System.Drawing.Point(624, 22);
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
            this.cb_Anticipo.Location = new System.Drawing.Point(807, 22);
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
            this.cb_Excedente.Location = new System.Drawing.Point(887, 22);
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
            this.groupBox1.Location = new System.Drawing.Point(296, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 174);
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
            this.groupBox2.Location = new System.Drawing.Point(598, 179);
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
            this.tb_Observaciones.Size = new System.Drawing.Size(226, 20);
            this.tb_Observaciones.TabIndex = 13;
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
            this.tb_Acceso.Size = new System.Drawing.Size(108, 20);
            this.tb_Acceso.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_Incapacidad);
            this.groupBox4.Controls.Add(this.rb_Falta);
            this.groupBox4.Controls.Add(this.rb_Vacaciones);
            this.groupBox4.Controls.Add(this.rb_Permiso);
            this.groupBox4.Controls.Add(this.rb_TrabajoF);
            this.groupBox4.Controls.Add(this.rb_Asistencia);
            this.groupBox4.Location = new System.Drawing.Point(598, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 93);
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
            this.cb_NoReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_NoReg.Location = new System.Drawing.Point(705, 22);
            this.cb_NoReg.Name = "cb_NoReg";
            this.cb_NoReg.Size = new System.Drawing.Size(97, 19);
            this.cb_NoReg.TabIndex = 15;
            this.cb_NoReg.Text = "No Registros";
            this.cb_NoReg.UseVisualStyleBackColor = true;
            this.cb_NoReg.CheckedChanged += new System.EventHandler(this.cb_NoReg_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 365);
            this.Controls.Add(this.cb_NoReg);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cb_Excedente);
            this.Controls.Add(this.cb_Anticipo);
            this.Controls.Add(this.cb_Retardos);
            this.Controls.Add(this.lb_Horarios);
            this.Controls.Add(this.lb_Empleados);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.bt_Abrir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "PreNomina";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button bt_Abrir;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridView dataGrid1;
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
        private System.Windows.Forms.ToolStripButton tsb_Exportar;
        private System.Windows.Forms.CheckBox cb_NoReg;
    }
}

