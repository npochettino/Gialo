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
    class CatalogoTanda : CatalogoGenerico<Tanda>
    {
        public static Tanda RecuperarPorTandaItem(int codigoTandaItem, ISession nhSesion)
        {
            try
            {
                Tanda tanda = nhSesion.QueryOver<Tanda>().Where(x => x.TandaItems.Any(y => y.Codigo == codigoTandaItem)).SingleOrDefault();
                return tanda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Tanda RecuperarPorEntrega(int codigoEntrega, ISession nhSesion)
        {
            try
            {
                Tanda tanda = nhSesion.Query<Tanda>().Where(x => x.Entregas.Any(y => y.Codigo == codigoEntrega)).SingleOrDefault();
                return tanda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IList<Tanda> RecuperarPorFechas(DateTime fechaInicio, DateTime fechaFin, ISession nhSesion)
        {
            try
            {
                IList<Tanda> tandas = nhSesion.QueryOver<Tanda>().Where(x => (x.FechaInicio >= fechaInicio && x.FechaFin <= fechaFin)
                    || (x.FechaInicio >= fechaInicio && x.FechaFin == null)).List();
                return tandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IList<Tanda> RecuperarPorFechasYArticulo(DateTime fechaInicio, DateTime fechaFin, int codigoArticulo, ISession nhSesion)
        {
            try
            {
                IList<Tanda> tandas = nhSesion.QueryOver<Tanda>().Where(x => (x.FechaInicio >= fechaInicio && x.FechaFin <= fechaFin && x.Articulo.Codigo == codigoArticulo)
                    || (x.FechaInicio >= fechaInicio && x.FechaFin == null && x.Articulo.Codigo == codigoArticulo)).List();
                return tandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IList<Tanda> RecuperarPorArticulo(int codigoArticulo, ISession nhSesion)
        {
            try
            {
                IList<Tanda> tandas = nhSesion.QueryOver<Tanda>().Where(x => x.Articulo.Codigo == codigoArticulo).List();
                return tandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IList<Tanda> RecuperarAbiertasPorArticulo(int codigoArticulo, ISession nhSesion)
        {
            try
            {
                IList<Tanda> tandas = nhSesion.QueryOver<Tanda>().Where(x => x.FechaFin == null && x.Articulo.Codigo == codigoArticulo).List();
                return tandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IList<Tanda> RecuperarAbiertas(ISession nhSesion)
        {
            try
            {
                IList<Tanda> tandas = nhSesion.QueryOver<Tanda>().Where(x => x.FechaFin == null).List();
                return tandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
