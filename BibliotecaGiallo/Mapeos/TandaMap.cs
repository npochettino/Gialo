using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TandaMap: ClassMap<Tanda>
    {
        public TandaMap()
        {
            Table("tandas");
            Id(x => x.Codigo).Column("codigoTanda").GeneratedBy.Identity();
            Map(x => x.Comentario).Column("comentario");
            Map(x => x.FechaFin).Column("fechaFin");
            Map(x => x.FechaInicio).Column("fechaInicio");
            Map(x => x.Estampado).Column("estampado");

            References(x => x.Articulo).Column("codigoArticulo").Cascade.None().LazyLoad(Laziness.Proxy);
            HasMany<TandaItem>(x => x.TandaItems).Table("tandaItems").KeyColumn("codigoTanda").Not.KeyNullable().Cascade.AllDeleteOrphan();
            HasMany<Entrega>(x => x.Entregas).Table("entregas").KeyColumn("codigoTanda").Not.KeyNullable().Cascade.AllDeleteOrphan();
        }
    }
}
