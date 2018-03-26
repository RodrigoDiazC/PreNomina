using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChecker
{
    class Empleado
    {
        // Data publica ----------------------------------
        public string listaAtributos;

        // Data privada ----------------------------------
        private string Nombre = null;
        private TiemposDia[] Dias = null;

        // Constructor -----------------------------------
        public Empleado(string atributos)
        {
            this.listaAtributos = atributos;
            string[] tokens = atributos.Split(new[] { "\n" }, StringSplitOptions.None);

            // Extrae el nombre del empleado
            this.Nombre = tokens[0];

            // Elimina informacion no util
            tokens = borrarBasura(tokens);

            // Extrae los dias
            Dias = extraerDiasDeTokens(tokens);

            HorasLaborales horas = new HorasLaborales();
            horas.entrada1 = DateTime.Parse("08:00");
            horas.salida1 = DateTime.Parse("13:00");
            horas.entrada2 = DateTime.Parse("14:00");
            horas.salida2 = DateTime.Parse("18:00");

            TimeSpan e = getExtra(horas, 0);

        }

        // Metodos publicos ------------------------------
        public string getNombre()
        {
            return this.Nombre;
        }
        public TiemposDia getDia(int index)
        {
            return this.Dias[index];
        }
        public TiemposDia[] getDias()
        {
            return this.Dias;
        }
        public int getCantDias()
        {
            return this.Dias.Length;
        }
        public TimeSpan getRetardoSemanal(HorasLaborales horas, int index)
        {
            TimeSpan span = TimeSpan.Parse("0");

            foreach (TiemposDia t in this.Dias)
            {
                if (t.entrada1.status == "RETARDO")
                {
                    span += t.entrada1.Hora.Subtract(horas.entrada1);
                }

                if (t.entrada2.status == "RETARDO")
                {
                    span += t.entrada2.Hora.Subtract(horas.entrada2);
                }
            }
               
            return span;
        }
        public TimeSpan getExtra(HorasLaborales horas, int index)
        {
            TimeSpan span = TimeSpan.Parse("0");

            foreach (TiemposDia t in this.Dias)
            {
                if (t.salida1.Hora > horas.salida1)
                {
                    span += t.salida1.Hora.Subtract(horas.salida1);
                }

                if (t.salida2.Hora > horas.salida2)
                {
                    span += t.salida2.Hora.Subtract(horas.salida2);
                }
            }

            return span;
        }

        // Metodos privados
        private TiemposDia[] extraerDiasDeTokens(string[] registro)
        {
            Acceso entrada1 = new Acceso();
            Acceso salida1 = new Acceso();
            Acceso entrada2 = new Acceso();
            Acceso salida2 = new Acceso();
            DateTime dia = DateTime.Now;
            int elemento = 0;

            TiemposDia[] diasInfo = new TiemposDia[(registro.Length) / 4];

            for (int i = 0; i < (registro.Length) / 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string[] split = registro[elemento].Split(' ');

                    // Obtiene la fecha del día
                    if (j == 0) dia = DateTime.Parse(split[0]);

                    // Acceso temporal
                    DateTime aux;
                    Acceso temp = new Acceso();

                    if (!split.Contains("A"))
                    {
                        // En caso de nuevo dia
                        if (j == 0)
                        {
                            // Asigna hora
                            DateTime.TryParse(split[1], out aux);
                            temp.Hora = aux;
                            // Asigna status
                            temp.status = split[4];
                        }
                        else
                        {
                            // Asigna hora
                            DateTime.TryParse(split[0], out aux);
                            temp.Hora = aux;
                            // Asigna status
                            temp.status = split[3];
                        }
                    }
                    else
                    {
                        temp.status = "NOREGISTRO";
                    }

                    if (split.Contains("ENTRADA1")) entrada1 = temp;
                    else if (split.Contains("SALIDA1")) salida1 = temp;
                    else if (split.Contains("ENTRADA2")) entrada2 = temp;
                    else if (split.Contains("SALIDA2")) salida2 = temp;

                    elemento++;
                }

                diasInfo[i] = new TiemposDia(dia, entrada1, salida1, entrada2, salida2);

            }

            return diasInfo;
        }
        private string[] borrarBasura(string[] lista)
        {
            var list = new List<string>(lista);
            list.RemoveAll(basuraEnLista);

            return list.ToArray();
        }
        private static bool basuraEnLista(string v)
        {
            bool match = false;

            match = !(v.Contains("ENTRADA") | v.Contains("SALIDA"));

            return match;
        }
    }
}
