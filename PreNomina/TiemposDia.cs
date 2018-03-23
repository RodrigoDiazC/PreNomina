using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChecker
{

    class TiemposDia
    {
        // Data privada
        public DateTime dia;
        public Acceso entrada1;
        public Acceso salida1;
        public Acceso entrada2;
        public Acceso salida2;

        // Constructor
        public TiemposDia(DateTime day, Acceso ent1, Acceso sal1, Acceso ent2, Acceso sal2)
        {
            this.dia = day;
            this.entrada1 = ent1;
            this.salida1 = sal1;
            this.entrada2 = ent2;
            this.salida2 = sal2;
        }

    }
}
