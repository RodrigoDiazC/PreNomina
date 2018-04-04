using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChecker
{

    class TiemposDia
    {
        // Data publica
        public DateTime dia { get; set; }
        public Acceso entrada1 { get; set; }
        public Acceso salida1 { get; set; }
        public Acceso entrada2 { get; set; }
        public Acceso salida2 { get; set; }
        public string status = "A";

        // Constructor
        public TiemposDia(DateTime day, Acceso ent1, Acceso sal1, Acceso ent2, Acceso sal2)
        {
            this.dia = day;
            this.entrada1 = ent1;
            this.salida1 = sal1;
            this.entrada2 = ent2;
            this.salida2 = sal2;
        }

        // Establece automaticamente si es A TF P F V I
        public void checkStatus()
        {
            // Analiza si entrada1 tiene alguna observación y determina la naturaleza de esta
            if(entrada1.observaciones != null)
            {
                // Estatus de Permiso (P)
                if (entrada1.observaciones.Contains("TIEMPO") || entrada1.observaciones.Contains("PERMISO"))
                {
                    this.status = "P";
                    return; // Regresa ya que la información de los accesos restantes no es de interés
                }
                else if (entrada1.observaciones.Contains("VACACIONES"))
                {
                    this.status = "V";
                    return; // Regresa ya que la información de los accesos restantes no es de interés
                }
            }

            // Cuenta la cantidad de no registros. Si es igual o mayor a 4 entonces se considera como falta
            if ((entrada1.status == "NOREGISTRO") &&
                (entrada2.status == "NOREGISTRO") &&
                (salida1.status == "NOREGISTRO") &&
                (salida2.status == "NOREGISTRO")) this.status = "F";
            else this.status = "A";

            return;
        }

    }
}
