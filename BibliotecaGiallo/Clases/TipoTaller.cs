using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class TipoTaller
    {
        public TipoTaller()
        {
            Talleres = new List<Taller>();
        }
        public virtual int Codigo { get; set; }
        public virtual string Descripcion { get; set; }

        public virtual IList<Taller> Talleres { get; set; }
    }
}
