using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreNomina
{
    class Empleado
    {
        // Data privada ----------------------------------
        private string listaAtributos;

        // Data Publica
        public string Nombre { get; set; }
        public List<TiemposDia> Dias { get; set; }
        public bool Puntualidad { get; set; }
        public bool Asistencia { get; set; }
        public bool Desempeno { get; set; }
        public int ID { get; set; }

        // Constructor -----------------------------------
        public Empleado(string atributos, int identifier, HorasLaborales horarioLaboral)
        {
            this.listaAtributos = atributos;
            string[] tokens = atributos.Split(new[] { "\n" }, StringSplitOptions.None);

            // ID para facilitar modificación de los objetos
            this.ID = identifier;
            // Extrae el nombre del empleado
            this.Nombre = tokens[0];
            // Elimina informacion no util y agrupa los días por fila
            tokens = groupDias(tokens);
            // Extrae los dias
            Dias = extraerDiasDeTokens(tokens);
            Dias = borrarDiasDuplicados(Dias);
            // Obtiene si fue puntual
            Puntualidad = this.checkPuntualidad(horarioLaboral, false);
            // Obtiene asistencia
            Asistencia = this.checkAsistenciaInicio();
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
        public int getCantDias()
        {
            return this.Dias.Count;
        }
        public TimeSpan getRetardoTotal(HorasLaborales horas)
        {
            TimeSpan span = TimeSpan.Parse("0");

            foreach (TiemposDia t in this.Dias)
            {
                if (t.entrada1.Hora.TimeOfDay > horas.entrada1.TimeOfDay)
                {
                    span += t.entrada1.Hora.Subtract(horas.entrada1);
                }

                if (t.entrada2.Hora.TimeOfDay > horas.entrada2.TimeOfDay)
                {
                    span += t.entrada2.Hora.Subtract(horas.entrada2);
                }
            }

            return span;
        }
        public TimeSpan getRetardoDia(HorasLaborales horas, int index)
        {
            TimeSpan span = TimeSpan.Parse("0");

            if (this.Dias[index].entrada1.Hora.TimeOfDay > horas.entrada1.TimeOfDay)
            {
                span += this.Dias[index].entrada1.Hora.Subtract(horas.entrada1);
            }

            if (this.Dias[index].entrada2.Hora.TimeOfDay > horas.entrada2.TimeOfDay)
            {
                span += this.Dias[index].entrada2.Hora.Subtract(horas.entrada2);
            }

            return span;
        }
        public TimeSpan getExtraTotal(HorasLaborales horas)
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
        public TimeSpan getAnticipoTotal(HorasLaborales horas)
        {
            TimeSpan span = TimeSpan.Parse("0");

            foreach (TiemposDia t in this.Dias)
            {

                if ((t.salida1.Hora.TimeOfDay < horas.salida1.TimeOfDay) && (t.salida1.Hora.TimeOfDay != TimeSpan.Parse("00:00:00"))) // NOREGISTRO para no tomar en cuenta dias donde no se registró
                {
                    span += horas.salida1.Subtract(t.salida1.Hora);
                }

                if ((t.salida2.Hora.TimeOfDay < horas.salida2.TimeOfDay) && (t.salida2.Hora.TimeOfDay != TimeSpan.Parse("00:00:00")))
                {
                    span += horas.salida2.Subtract(t.salida2.Hora);
                }
            }

            return span;
        }

        public bool checkAsistenciaInicio() // Analiza asistencia al crearse el objeto. Se basa en los registros por día (modo automático)
        {
            int acc1 = 0, acc2 = 0;

            foreach (TiemposDia t in this.Dias)
            {

                if (t.entrada1.status == "NOREGISTRO") acc1++;
                if (t.entrada2.status == "NOREGISTRO") acc1++;
                if (t.salida1.status == "NOREGISTRO") acc1++;
                if (t.salida2.status == "NOREGISTRO") acc1++;
                if (acc1 >= 4) acc2++;
                acc1 = 0;

            }

            if (acc2 >= 1) return false;
            else return true;
        }

        public void checkAsistenciaUpdate() // Analiza la asistencia al modificar el valor de los días en los controles (modo manual). Se basa en el status de los días
        {
            int acc1 = 0;

            foreach (TiemposDia t in this.Dias)
            {
                if (t.status.Equals("F")) acc1 = 1;
            }

            if (acc1 == 1) this.Asistencia = false;
            else this.Asistencia = true;
        }


        public bool checkPuntualidad(HorasLaborales horas, bool incluirAnticipo) // 0 Solo extra 1 Extra y anticipo
        {

            if (!incluirAnticipo) { if (getRetardoTotal(horas).TotalMinutes >= horas.limiteRetardo.TotalMinutes) return false; }
            else { if ((getRetardoTotal(horas).TotalMinutes + getAnticipoTotal(horas).TotalMinutes) >= horas.limiteRetardo.TotalMinutes) return false; }

            return true;
        }
        public void setDia(TiemposDia nuevoHorario, int index)
        {
            this.Dias[index] = nuevoHorario;
        }

        // Metodos privados
        private List<TiemposDia> extraerDiasDeTokens(string[] tokens)
        {
            Acceso entrada1 = new Acceso();
            Acceso salida1 = new Acceso();
            Acceso entrada2 = new Acceso();
            Acceso salida2 = new Acceso();
            DateTime dia = DateTime.Now;

            List<TiemposDia> diasList = new List<TiemposDia>();
            TiemposDia[] diasInfo = new TiemposDia[tokens.Length];

            for (int i = 0; i < tokens.Length; i++)
            {
                string[] grupoDia = tokens[i].Split('\n');

                entrada1 = new Acceso();
                salida1 = new Acceso();
                entrada2 = new Acceso();
                salida2 = new Acceso();

                for (int j = 0; j < grupoDia.Length; j++)
                {
                    // Separa elementos
                    string[] diaElementos = grupoDia[j].Split(' ');

                    Acceso temp = new Acceso();

                    // Obtiene la fecha del día
                    if (j == 0) dia = DateTime.Parse(diaElementos[0]);

                    if (!diaElementos.Contains("A"))
                    {
                        // En caso de nuevo dia
                        if (j == 0)
                        {
                            temp.Hora = DateTime.Parse(diaElementos[1]);
                            // Asigna status
                            temp.status = diaElementos[4];
                            // Observaciones
                            if (diaElementos.Length > 5) temp.observaciones = diaElementos[5];
                        }
                        else
                        {
                            temp.Hora = DateTime.Parse(diaElementos[0]);
                            // Asigna status
                            temp.status = diaElementos[3];
                            // Observaciones
                            if (diaElementos.Length > 4) temp.observaciones = diaElementos[4];

                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            // status
                            temp.status = diaElementos[3];
                            // observaciones
                            if (diaElementos.Length > 4)
                            {
                                string[] observaciones = new string[diaElementos.Length - 4];
                                Array.Copy(diaElementos, 4, observaciones, 0, observaciones.Length);
                                temp.observaciones = string.Join(" ", observaciones);
                            }
                        }
                        else
                        {
                            // Asigna status
                            temp.status = diaElementos[2];
                            // Observaciones
                            if (diaElementos.Length > 3)
                            {
                                string[] observaciones = new string[diaElementos.Length - 3];
                                Array.Copy(diaElementos, 3, observaciones, 0, observaciones.Length);
                                temp.observaciones = string.Join(" ", observaciones);
                            }
                        }

                    }

                    if (diaElementos.Contains("ENTRADA1") || diaElementos.Contains("ENTRADA")) entrada1 = temp;
                    else if (diaElementos.Contains("SALIDA1") || diaElementos.Contains("SALIDA")) salida1 = temp;
                    else if (diaElementos.Contains("ENTRADA2")) entrada2 = temp;
                    else if (diaElementos.Contains("SALIDA2")) salida2 = temp;
                }

                diasInfo[i] = new TiemposDia(dia, entrada1, salida1, entrada2, salida2);
            }

            foreach (TiemposDia t in diasInfo) diasList.Add(t);

            return diasList;
        }
        private string[] groupDias(string[] lista)
        {

            int i = 0, k = 0;
            string[] result = { };

            // Borra información innecesaria
            var list = new List<string>(lista);
            list.RemoveAll(basuraEnLista);

            string perro = string.Join("", list);

            foreach (char c in perro)
            {
                if (c == '/') i++;
            }

            i = i / 2;

            string[] dias = new string[i];

            // Agrupa un dia por renglon
            for (int j = 0; j < i; j++)
            {
                do
                {
                    dias[j] += list[k] + '\n';
                    k++;
                    if (k >= list.Count) break;

                } while (list[k][2] != '/');

                //Borra \n anterior
                dias[j] = dias[j].Substring(0, dias[j].Length - 1);
            }

            return dias;
        }
        private static bool basuraEnLista(string v)
        {
            bool match = false;

            match = !(v.Contains("ENTRADA") | v.Contains("SALIDA"));

            return match;
        }
        private List<TiemposDia> borrarDiasDuplicados(List<TiemposDia> d)
        {
            //Borra dias duplicados producidos cuando un empleado no registra salida2

            DateTime prevDia = DateTime.Parse("01/01/1970");

            for (int i = 0; i < d.Count; i++)
            {
                if (d[i].dia == prevDia) d.RemoveAt(i - 1);
                prevDia = d[i].dia;
            }

            return d;
        }
    }
}
