using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class AdministradorMap : ClassMap<Administrador>
    {
        public AdministradorMap()
        {
            Table("administradores");
            Id(x => x.Codigo).Column("idAdministrador").GeneratedBy.Identity();
            Map(x => x.NombreUsuario).Column("usuario");
            Map(x => x.Contraseña).Column("contraseña");
        }
    }
}
