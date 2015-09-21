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
    class CatalogoTipo : CatalogoGenerico<Tipo>
    {
        public static Tipo RecuperarPorTalle(int codigoTalle, ISession nhSesion)
        {
            try
            {
                Tipo tipo = nhSesion.Query<Tipo>().Where(x => x.Talles.Any(y => y.Codigo == codigoTalle)).SingleOrDefault();
                return tipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
