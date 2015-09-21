using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class TipoMap: ClassMap<Tipo>
    {
        public TipoMap()
        {
            Table("tipos");
            Id(x => x.Codigo).Column("codigoTipo").GeneratedBy.Identity();
            Map(x => x.Descripcion).Column("descripcion");
            HasMany<Talle>(x => x.Talles).Table("talles").KeyColumn("codigoTipo").Not.KeyNullable().Cascade.AllDeleteOrphan();
        }
    }
}
