using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class Taller
    {
        public virtual int Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Responsable { get; set; }
        public virtual string Contacto { get; set; }
    }
}
