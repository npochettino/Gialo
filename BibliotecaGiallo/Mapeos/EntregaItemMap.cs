using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class EntregaItemMap : ClassMap<EntregaItem>
    {
        public EntregaItemMap()
        {
            Table("entregaItems");
            Id(x => x.Codigo).Column("codigoEntregaItem").GeneratedBy.Identity();
            Map(x => x.Cantidad).Column("cantidad");

            References(x => x.TandaItem).Column("codigoTandaItems").Cascade.None().LazyLoad(Laziness.Proxy);
        }
    }
}
