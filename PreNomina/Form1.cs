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

        public Form1()
        {
            InitializeComponent();
        }

        private void bt_Abrir_Click(object sender, EventArgs e)
        {
            Analizador analizador = new Analizador();

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Seleccionar el PDF a escanear";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "pdf";
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
            textBox1.Text = ExtractTextFromPdf(openFileDialog.FileName);

            gEmpleados = analizador.getEmpleados(ExtractTextFromPdf(openFileDialog.FileName));

        }

        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for(int i = 1; i <= reader.NumberOfPages; i++)
                {
                     text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }

    }
}
