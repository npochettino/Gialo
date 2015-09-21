using BibliotecaGiallo.Clases;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Catalogos
{
    class CatalogoEntrega : CatalogoGenerico<Entrega>
    {
        public static IList<Entrega> RecuperarPorTaller(int codigoTaller, ISession nhSesion)
        {
            try
            {
                IList<Entrega> entregas = nhSesion.QueryOver<Entrega>().Where(x => x.Taller.Codigo == codigoTaller).List();
                return entregas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
