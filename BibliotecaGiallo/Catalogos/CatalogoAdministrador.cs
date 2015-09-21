using BibliotecaGiallo.Clases;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Catalogos
{
    class CatalogoAdministrador : CatalogoGenerico<Administrador>
    {
        public static Administrador RecuperarPorUsuarioYContraseña(string nombreUsuario, string contraseña, ISession nhSesion)
        {
            try
            {
                Administrador adm = RecuperarPor(x => x.Contraseña == contraseña && x.NombreUsuario == nombreUsuario, nhSesion);
                return adm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Administrador RecuperarPorUsuario(string nombreUsuario, ISession nhSesion)
        {
            try
            {
                Administrador adm = RecuperarPor(x => x.NombreUsuario == nombreUsuario, nhSesion);
                return adm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
