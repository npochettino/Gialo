using BibliotecaGiallo.Clases;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace BibliotecaGiallo.Catalogos
{
    class CatalogoTipoTaller : CatalogoGenerico<TipoTaller>
    {
        public static TipoTaller RecuperarPorTaller(int codigoTaller, ISession nhSesion)
        {
            try
            {
                TipoTaller tipoTaller = nhSesion.Query<TipoTaller>().Where(x => x.Talleres.Any(y => y.Codigo == codigoTaller)).SingleOrDefault();
                return tipoTaller;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
