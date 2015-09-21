using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TallerMap: ClassMap<Taller>
    {
        public TallerMap()
        {
            Table("talleres");
            Id(x => x.Codigo).Column("codigoTaller").GeneratedBy.Identity();
            Map(x => x.Descripcion).Column("descripcion");
            Map(x => x.Contacto).Column("contacto");
            Map(x => x.Responsable).Column("responsable");
        }
    }
}
