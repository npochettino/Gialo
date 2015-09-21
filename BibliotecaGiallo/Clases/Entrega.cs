using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class Entrega
    {
        public Entrega()
        {
            EntregaItems = new List<EntregaItem>();
        }
        public virtual int Codigo { get; set; }
        public virtual DateTime FechaInicio { get; set; }
        public virtual DateTime? FechaFin { get; set; }
        public virtual Taller Taller { get; set; }

        public virtual IList<EntregaItem> EntregaItems { get; set; }
    }
}
