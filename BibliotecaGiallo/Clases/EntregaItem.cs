using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Clases
{
    public class EntregaItem
    {
        public virtual int Codigo { get; set; }
        public virtual TandaItem TandaItem { get; set; }
        public virtual int Cantidad { get; set; }
    }
}
