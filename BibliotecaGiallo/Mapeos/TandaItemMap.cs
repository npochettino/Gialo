using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TandaItemMap: ClassMap<TandaItem>
    {
        public TandaItemMap()
        {
            Table("tandaItems");
            Id(x => x.Codigo).Column("codigoTandaItems").GeneratedBy.Identity();
            Map(x => x.Cantidad).Column("cantidad");

            References(x => x.Talle).Column("codigoTalle").Cascade.None().LazyLoad(Laziness.Proxy);
            References(x => x.Color).Column("codigoColor").Cascade.None().LazyLoad(Laziness.Proxy);
            //HasMany<Entrega>(x => x.Entregas).Table("entregas").KeyColumn("codigoTandaItems").Not.KeyNullable().Cascade.AllDeleteOrphan();
        }
    }
}
