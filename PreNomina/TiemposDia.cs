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
