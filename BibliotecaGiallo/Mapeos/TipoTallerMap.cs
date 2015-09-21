using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TipoTallerMap: ClassMap<TipoTaller>
    {
        public TipoTallerMap()
        {
            Table("tipoTalleres");
            Id(x => x.Codigo).Column("codigoTipoTaller").GeneratedBy.Identity();
            Map(x => x.Descripcion).Column("descripcion");

            HasMany<Taller>(x => x.Talleres).Table("talleres").KeyColumn("codigoTipoTaller").Not.KeyNullable().Cascade.AllDeleteOrphan();
        }
    }
}
