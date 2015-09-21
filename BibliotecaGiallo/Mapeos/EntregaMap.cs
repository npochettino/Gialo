using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class EntregaMap: ClassMap<Entrega>
    {
        public EntregaMap()
        {
            Table("entregas");
            Id(x => x.Codigo).Column("codigoEntrega").GeneratedBy.Identity();
            Map(x => x.FechaFin).Column("fechaFin");
            Map(x => x.FechaInicio).Column("fechaInicio");

            References(x => x.Taller).Column("codigoTaller").Cascade.None().LazyLoad(Laziness.Proxy);
            HasMany<EntregaItem>(x => x.EntregaItems).Table("entregaItems").KeyColumn("codigoEntrega").Not.KeyNullable().Cascade.AllDeleteOrphan();
        }
    }
}
