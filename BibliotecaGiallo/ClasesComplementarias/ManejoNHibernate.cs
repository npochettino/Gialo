using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BibliotecaGiallo.ClasesComplementarias
{
    class ManejoNHibernate
    {
        private static ISessionFactory CrearSesion()
        {
            ISessionFactory _sessionFactory = Fluently.Configure()


              //.Database(MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString("data source=SUPLENTE4-PC\\EZEQUIELSQL;initial catalog=Giallo;integrated security=True"))
                //.Database(MsSqlConfiguration.MsSql2008.ConnectionString("data source=localhost;initial catalog=AppRouss;user=sa;password=ana"))
                //.Database(MsSqlConfiguration.MsSql2008.ConnectionString("data source=localhost;initial catalog=w1402088_AppRouss;user=w1402088_approuss;password=Algoritmos2015"))
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString("data source=localhost;initial catalog=w1402088_Giallo;user=w1402088_Giallo;password=Algoritmos2015"))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BibliotecaGiallo.Class1>())
              .BuildSessionFactory();
            return _sessionFactory;
        }

        internal static ISession IniciarSesion()
        {
            return CrearSesion().OpenSession();
        }
    }
}
