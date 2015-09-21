using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TalleMap: ClassMap<Talle>
    {
        public TalleMap()
        {
            Table("talles");
            Id(x => x.Codigo).Column("codigoTalle").GeneratedBy.Identity();
            Map(x => x.Descripcion).Column("descripcion");
        }
    }
}
