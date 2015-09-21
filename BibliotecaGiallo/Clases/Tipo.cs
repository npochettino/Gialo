using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class Tipo
    {
        public Tipo()
        {
            Talles = new List<Talle>();
        }

        public virtual int Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual IList<Talle> Talles { get; set; }
    }
}
