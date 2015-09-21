using BibliotecaGiallo.Clases;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaGiallo.Mapeos
{
    class ArticuloMap: ClassMap<Articulo>
    {
        public ArticuloMap()
        {
            Table("articulos");
            Id(x => x.Codigo).Column("codigoArticulo").GeneratedBy.Identity();
            Map(x => x.Descripcion).Column("descripcion");
        }
    }
}
