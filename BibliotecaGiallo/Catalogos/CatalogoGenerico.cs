using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace BibliotecaGiallo.Catalogos
{
    abstract class CatalogoGenerico<TClase> where TClase : class
    {
        public static TClase RecuperarPorCodigo(int codigo, ISession nhSesion)
        {
            try
            {
                return nhSesion.Get<TClase>(codigo);
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TClase> RecuperarTodos(ISession nhSesion)
        {
            try
            {
                return nhSesion.QueryOver<TClase>().List<TClase>().ToList();
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TClase> RecuperarLista(Expression<Func<TClase, bool>> expresion, ISession nhSesion)
        {
            try
            {
                return nhSesion.QueryOver<TClase>().Where(expresion).List<TClase>().ToList();
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static TClase RecuperarPor(Expression<Func<TClase, bool>> expresion, ISession nhSesion)
        {
            try
            {
                return nhSesion.QueryOver<TClase>().Where(expresion).SingleOrDefault();
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertarActualizarLista(IEnumerable<TClase> listaEntidades, ISession nhSesion)
        {
            try
            {
                foreach (TClase item in listaEntidades)
                {
                    nhSesion.SaveOrUpdate(item);
                    nhSesion.Flush();
                }
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertarActualizar(TClase entidad, ISession nhSesion)
        {
            try
            {
                nhSesion.SaveOrUpdate(entidad);
                nhSesion.Flush();
            }
            catch (ADOException adoex)
            {
                throw adoex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Eliminar(TClase entidad, ISession nhSesion)
        {
            try
            {
                nhSesion.Delete(entidad);
                nhSesion.Flush();
            }
            catch (ADOException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void EliminarLista(IEnumerable<TClase> listaEntidades, ISession nhSesion)
        {
            try
            {
                foreach (TClase item in listaEntidades)
                {
                    nhSesion.Delete(item);
                    nhSesion.Flush();
                }
            }
            catch (ADOException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
