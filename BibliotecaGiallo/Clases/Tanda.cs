using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class Tanda
    {
        public Tanda()
        {
            TandaItems = new List<TandaItem>();
            Entregas = new List<Entrega>();
        }

        public virtual int Codigo { get; set; }
        public virtual string Comentario { get; set; }
        public virtual DateTime FechaInicio { get; set; }
        public virtual DateTime? FechaFin { get; set; }
        public virtual string Estampado { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual IList<TandaItem> TandaItems { get; set; }
        public virtual IList<Entrega> Entregas { get; set; }
    }
}
