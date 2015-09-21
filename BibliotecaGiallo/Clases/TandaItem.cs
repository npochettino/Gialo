using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class TandaItem
    {
        public TandaItem()
        {
            //Entregas = new List<Entrega>();
        }
        public virtual int Codigo { get; set; }
        public virtual int Cantidad { get; set; }
        public virtual Talle Talle { get; set; }
        public virtual Color Color { get; set; }

        //public virtual IList<Entrega> Entregas { get; set; }

    }
}
