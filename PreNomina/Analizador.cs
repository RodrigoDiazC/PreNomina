using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreNomina
{
    class Analizador
    {
        // Data privada
        public DateTime fechaInicio;
        public DateTime fechaFin;

        // No es necesario contructor

        // Metodos Publicos ------------------------------------
        public List<Empleado> getEmpleados(string texto, HorasLaborales horarioLaboral)
        {
            // Busca fecha de inicio
            string toBeSearched = "Desde:";
            string sFechaIni = texto.Substring(texto.IndexOf(toBeSearched) + toBeSearched.Length + 1, 10);
            toBeSearched = "Hasta:";
            string sFechaFin = texto.Substring(texto.IndexOf(toBeSearched) + toBeSearched.Length + 1, 10);

            // Asigna fechas a objeto
            this.fechaInicio = DateTime.Parse(sFechaIni);
            this.fechaFin = DateTime.Parse(sFechaFin);

            // Busca la cantidad de personas, crea los objetos y asigna su información.
            toBeSearched = ")";
            string[] tokens = texto.Split(new[] { ")" }, StringSplitOptions.None);

            Empleado[] empleados = new Empleado[tokens.Length - 1];

            for (int i = 1; i < tokens.Length; i++)
            {
                empleados[i-1] = new Empleado(tokens[i], i - 1 ,horarioLaboral);
                
                // Analiza el status de cada dia
                foreach(TiemposDia t in empleados[i - 1].Dias)
                {
                    t.checkStatus();
                }    
            }

            return empleados.ToList();
        }
        public DateTime getFechaInicio() { return fechaInicio; }
        public DateTime getFechaFin() { return fechaFin; }
       
    }
}
