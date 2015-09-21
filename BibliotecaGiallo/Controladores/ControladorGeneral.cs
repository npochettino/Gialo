using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaGiallo.Catalogos;
using System.Data;
using NHibernate;
using BibliotecaGiallo.ClasesComplementarias;
using BibliotecaGiallo.Clases;
using System.Globalization;
using System.IO;

namespace BibliotecaGiallo.Controladores
{
    public class ControladorGeneral
    {
        #region Administrador

        public static void InsertarActualizarAdministrador(int codigoAdministrador, string nombreUsuario, string contraseña)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Administrador adm;

                if (codigoAdministrador == 0)
                {
                    adm = new Administrador();
                }
                else
                {
                    adm = CatalogoAdministrador.RecuperarPorCodigo(codigoAdministrador, nhSesion);
                }

                adm.Contraseña = contraseña;
                adm.NombreUsuario = nombreUsuario;

                CatalogoAdministrador.InsertarActualizar(adm, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarNombreAdministradorEnUso(string nombreUsuario)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Administrador admin = CatalogoAdministrador.RecuperarPorUsuario(nombreUsuario, nhSesion);
                if (admin == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarLogueoAdministrador(string nombreUsuario, string contraseña)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaAdm = new DataTable();
                tablaAdm.Columns.Add("codigoAdm");
                tablaAdm.Columns.Add("nombreUsuario");
                tablaAdm.Columns.Add("contraseña");

                Administrador adm = CatalogoAdministrador.RecuperarPorUsuarioYContraseña(nombreUsuario, contraseña, nhSesion);
                if (adm == null)
                    tablaAdm = null;
                else
                    tablaAdm.Rows.Add(new object[] { adm.Codigo, adm.NombreUsuario, adm.Contraseña });

                return tablaAdm;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosAdministradores()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaAdministradores = new DataTable();
                tablaAdministradores.Columns.Add("idAdministrador");
                tablaAdministradores.Columns.Add("usuario");
                tablaAdministradores.Columns.Add("contraseña");
                tablaAdministradores.Columns.Add("contraseñaRepetir");

                List<Administrador> listaAdministradores = CatalogoAdministrador.RecuperarTodos(nhSesion);

                (from s in listaAdministradores select s).Aggregate(tablaAdministradores, (dt, r) => { dt.Rows.Add(r.Codigo, r.NombreUsuario, r.Contraseña); return dt; });
                return tablaAdministradores;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Articulos

        public static void InsertarActualizarArticulo(int codigoArticulo, string descripcion)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Articulo articulo;

                if (codigoArticulo == 0)
                {
                    articulo = new Articulo();
                }
                else
                {
                    articulo = CatalogoArticulo.RecuperarPorCodigo(codigoArticulo, nhSesion);
                }

                articulo.Descripcion = descripcion;

                CatalogoArticulo.InsertarActualizar(articulo, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosArticulos()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaArticulos = new DataTable();
                tablaArticulos.Columns.Add("codigoArticulo");
                tablaArticulos.Columns.Add("descripcion");

                List<Articulo> listaArticulos = CatalogoArticulo.RecuperarTodos(nhSesion);
                listaArticulos.Aggregate(tablaArticulos, (dt, art) => { dt.Rows.Add(art.Codigo, art.Descripcion); return dt; });
                // (from s in listaArticulos select s).Aggregate(tablaArticulos, (dt, r) => { dt.Rows.Add(r.Codigo, r.Descripcion); return dt; });
                return tablaArticulos;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarArticuloAEliminar(int codigoArticulo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                var tandas = CatalogoTanda.RecuperarPorArticulo(codigoArticulo, nhSesion);
                if (tandas.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void EliminarArticulo(int codigoArticulo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Articulo articulo = CatalogoArticulo.RecuperarPorCodigo(codigoArticulo, nhSesion);
                CatalogoArticulo.Eliminar(articulo, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Tipos

        public static void InsertarActualizarTipo(int codigoTipo, string descripcion)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tipo tipo;

                if (codigoTipo == 0)
                {
                    tipo = new Tipo();
                }
                else
                {
                    tipo = CatalogoTipo.RecuperarPorCodigo(codigoTipo, nhSesion);
                }

                tipo.Descripcion = descripcion;

                CatalogoTipo.InsertarActualizar(tipo, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTipos()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTipos = new DataTable();
                tablaTipos.Columns.Add("codigoTipo");
                tablaTipos.Columns.Add("descripcion");

                List<Tipo> listaTipos = CatalogoTipo.RecuperarTodos(nhSesion);
                listaTipos.Aggregate(tablaTipos, (dt, tip) => { dt.Rows.Add(tip.Codigo, tip.Descripcion); return dt; });
                return tablaTipos;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Talles

        public static void InsertarActualizarTalle(int codigoTipo, int codigoTalle, string descripcion)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tipo tipo = CatalogoTipo.RecuperarPorCodigo(codigoTipo, nhSesion);
                Talle talle;
                if (codigoTalle == 0)
                {
                    talle = new Talle();
                    tipo.Talles.Add(talle);
                }
                else
                {
                    talle = tipo.Talles.Where(x => x.Codigo == codigoTalle).First();
                }

                talle.Descripcion = descripcion;

                CatalogoTipo.InsertarActualizar(tipo, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTallesPorTipo(int codigoTipo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTalles = new DataTable();
                tablaTalles.Columns.Add("codigoTalle");
                tablaTalles.Columns.Add("descripcionTalle");
                tablaTalles.Columns.Add("codigoTipo");
                tablaTalles.Columns.Add("descripcionTipo");

                Tipo tipo = CatalogoTipo.RecuperarPorCodigo(codigoTipo, nhSesion);

                List<Talle> listaTalle = tipo.Talles.ToList();
                listaTalle.Aggregate(tablaTalles, (dt, tall) => { dt.Rows.Add(tall.Codigo, tall.Descripcion, tipo.Codigo, tipo.Descripcion); return dt; });
                return tablaTalles;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTalles()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTalles = new DataTable();
                tablaTalles.Columns.Add("codigoTalle");
                tablaTalles.Columns.Add("descripcionTalle");
                tablaTalles.Columns.Add("codigoTipo");
                tablaTalles.Columns.Add("descripcionTipo");

                List<Talle> listaTalle = CatalogoTalle.RecuperarTodos(nhSesion);
                foreach (Talle talle in listaTalle)
                {
                    Tipo tipo = CatalogoTipo.RecuperarPorTalle(talle.Codigo, nhSesion);
                    tablaTalles.Rows.Add(talle.Codigo, talle.Descripcion, tipo.Codigo, tipo.Descripcion);
                }
                //listaTalle.Aggregate(tablaTalles, (dt, tall) => { dt.Rows.Add(tall.Codigo, tall.Descripcion, Catalogo.Codigo, tipo.Descripcion); return dt; });
                return tablaTalles;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Colores

        public static void InsertarActualizarColor(int codigoColor, string descripcion)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Color color;

                if (codigoColor == 0)
                {
                    color = new Color();
                }
                else
                {
                    color = CatalogoColor.RecuperarPorCodigo(codigoColor, nhSesion);
                }

                color.Descripcion = descripcion;

                CatalogoColor.InsertarActualizar(color, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosColores()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaColor = new DataTable();
                tablaColor.Columns.Add("codigoColor");
                tablaColor.Columns.Add("descripcion");

                List<Color> listaColores = CatalogoColor.RecuperarTodos(nhSesion);
                listaColores.Aggregate(tablaColor, (dt, art) => { dt.Rows.Add(art.Codigo, art.Descripcion); return dt; });
                // (from s in listaArticulos select s).Aggregate(tablaArticulos, (dt, r) => { dt.Rows.Add(r.Codigo, r.Descripcion); return dt; });
                return tablaColor;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region TipoTaller

        public static void InsertarActualizarTipoTaller(int codigoTipoTaller, string descripcion)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                TipoTaller tipoTaller;

                if (codigoTipoTaller == 0)
                {
                    tipoTaller = new TipoTaller();
                }
                else
                {
                    tipoTaller = CatalogoTipoTaller.RecuperarPorCodigo(codigoTipoTaller, nhSesion);
                }

                tipoTaller.Descripcion = descripcion;

                CatalogoTipoTaller.InsertarActualizar(tipoTaller, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTipoTalleres()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTiposTaller = new DataTable();
                tablaTiposTaller.Columns.Add("codigoTipoTaller");
                tablaTiposTaller.Columns.Add("descripcion");

                List<TipoTaller> listaTipoTalleres = CatalogoTipoTaller.RecuperarTodos(nhSesion);
                listaTipoTalleres.Aggregate(tablaTiposTaller, (dt, art) => { dt.Rows.Add(art.Codigo, art.Descripcion); return dt; });
                // (from s in listaArticulos select s).Aggregate(tablaArticulos, (dt, r) => { dt.Rows.Add(r.Codigo, r.Descripcion); return dt; });
                return tablaTiposTaller;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Taller

        public static void InsertarActualizarTaller(int codigoTipoTaller, int codigoTaller, string descripcion, string responsable, string contacto)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                TipoTaller tipoTaller = CatalogoTipoTaller.RecuperarPorCodigo(codigoTipoTaller, nhSesion);
                Taller taller;

                if (codigoTaller == 0)
                {
                    taller = new Taller();
                    taller.Descripcion = descripcion;
                    taller.Contacto = contacto;
                    taller.Responsable = responsable;
                    tipoTaller.Talleres.Add(taller);
                }
                else
                {
                    taller = tipoTaller.Talleres.Where(x => x.Codigo == codigoTaller).FirstOrDefault();
                    taller.Descripcion = descripcion;
                    taller.Contacto = contacto;
                    taller.Responsable = responsable;
                }
                CatalogoTipoTaller.InsertarActualizar(tipoTaller, nhSesion);

            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTalleres()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTalleres = new DataTable();
                tablaTalleres.Columns.Add("codigoTaller");
                tablaTalleres.Columns.Add("descripcion");
                tablaTalleres.Columns.Add("responsable");
                tablaTalleres.Columns.Add("contacto");
                tablaTalleres.Columns.Add("codigoTipoTaller");
                tablaTalleres.Columns.Add("tipoTaller");

                List<Taller> listaTaller = CatalogoTaller.RecuperarTodos(nhSesion);
                foreach (Taller taller in listaTaller)
                {
                    TipoTaller tipoTaller = CatalogoTipoTaller.RecuperarPorTaller(taller.Codigo, nhSesion);
                    tablaTalleres.Rows.Add(taller.Codigo, taller.Descripcion, taller.Responsable, taller.Contacto, tipoTaller.Codigo, tipoTaller.Descripcion);
                }
                return tablaTalleres;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTalleresPorTipoTaller(int codigoTipoTaller)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTalleres = new DataTable();
                tablaTalleres.Columns.Add("codigoTaller");
                tablaTalleres.Columns.Add("descripcion");
                tablaTalleres.Columns.Add("responsable");
                tablaTalleres.Columns.Add("contacto");
                tablaTalleres.Columns.Add("codigoTipoTaller");
                tablaTalleres.Columns.Add("tipoTaller");

                TipoTaller tipoTaller = CatalogoTipoTaller.RecuperarPorCodigo(codigoTipoTaller, nhSesion);
                tipoTaller.Talleres.Aggregate(tablaTalleres, (dt, tal) => { dt.Rows.Add(tal.Codigo, tal.Descripcion, tal.Responsable, tal.Contacto, tipoTaller.Codigo, tipoTaller.Descripcion); return dt; });
                return tablaTalleres;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarTallerAEliminar(int codigoTaller)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                var entregas = CatalogoEntrega.RecuperarPorTaller(codigoTaller, nhSesion);
                if (entregas.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void EliminarTaller(int codigoTaller)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Taller taller = CatalogoTaller.RecuperarPorCodigo(codigoTaller, nhSesion);
                CatalogoTaller.Eliminar(taller, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Tanda

        public static void InsertarActualizarTanda(int codigoTanda, string comentario, string estampado, DateTime fechaInicio, DateTime? fechaFin, int codigoArticulo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda;

                if (codigoTanda == 0)
                {
                    tanda = new Tanda();
                }
                else
                {
                    tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                }

                tanda.Comentario = comentario;
                tanda.Estampado = estampado;
                tanda.FechaFin = fechaFin;
                tanda.FechaInicio = fechaInicio;
                tanda.Articulo = CatalogoArticulo.RecuperarPorCodigo(codigoArticulo, nhSesion);

                CatalogoTanda.InsertarActualizar(tanda, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodasTandas()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandas = new DataTable();
                tablaTandas.Columns.Add("codigoTanda");
                tablaTandas.Columns.Add("comentario");
                tablaTandas.Columns.Add("estampado");
                tablaTandas.Columns.Add("codigoArticulo");
                tablaTandas.Columns.Add("fechaInicio", typeof(DateTime));
                tablaTandas.Columns.Add("fechaFin", typeof(DateTime));

                List<Tanda> listaTanda = CatalogoTanda.RecuperarTodos(nhSesion).OrderByDescending(x => x.FechaInicio).ToList();
                listaTanda.Aggregate(tablaTandas, (dt, ta) => { dt.Rows.Add(ta.Codigo, ta.Comentario, ta.Estampado, ta.Articulo.Codigo, ta.FechaInicio, ta.FechaFin); return dt; });
                // (from s in listaArticulos select s).Aggregate(tablaArticulos, (dt, r) => { dt.Rows.Add(r.Codigo, r.Descripcion); return dt; });
                return tablaTandas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTandaPorCodigo(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandas = new DataTable();
                tablaTandas.Columns.Add("codigoTanda");
                tablaTandas.Columns.Add("comentario");
                tablaTandas.Columns.Add("estampado");
                tablaTandas.Columns.Add("codigoArticulo");
                tablaTandas.Columns.Add("fechaInicio", typeof(DateTime));
                tablaTandas.Columns.Add("fechaFin", typeof(DateTime));
                tablaTandas.Columns.Add("descripcionArticulo");

                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                tablaTandas.Rows.Add(tanda.Codigo, tanda.Comentario, tanda.Estampado, tanda.Articulo.Codigo, tanda.FechaInicio, tanda.FechaFin, tanda.Articulo.Descripcion);
                // (from s in listaArticulos select s).Aggregate(tablaArticulos, (dt, r) => { dt.Rows.Add(r.Codigo, r.Descripcion); return dt; });
                return tablaTandas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTandasParaReporte(DateTime fechaDesde, DateTime fechaHasta, int articulo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandas = new DataTable();
                tablaTandas.Columns.Add("CodigoTanda");
                tablaTandas.Columns.Add("CodigoTandaItem");
                tablaTandas.Columns.Add("Comentario");
                tablaTandas.Columns.Add("Articulo");
                tablaTandas.Columns.Add("FechaInicio");
                tablaTandas.Columns.Add("FechaFin");
                tablaTandas.Columns.Add("Tipo");
                tablaTandas.Columns.Add("Talle");
                tablaTandas.Columns.Add("Cantidad");
                tablaTandas.Columns.Add("Color");
                List<Tanda> tandas = new List<Tanda>();
                if (articulo == -1)
                {
                    tandas = CatalogoTanda.RecuperarPorFechas(fechaDesde, fechaHasta, nhSesion).ToList();
                }
                else
                {
                    tandas = CatalogoTanda.RecuperarPorFechasYArticulo(fechaDesde, fechaHasta, articulo, nhSesion).ToList();
                }

                foreach (Tanda tanda in tandas)
                {
                    DataRow fila = tablaTandas.NewRow();
                    fila["CodigoTanda"] = tanda.Codigo;
                    fila["Comentario"] = tanda.Comentario;
                    fila["Articulo"] = tanda.Articulo.Descripcion;
                    fila["FechaInicio"] = tanda.FechaInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tanda.FechaFin != null)
                    {
                        fila["FechaFin"] = tanda.FechaFin != null ? tanda.FechaFin.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                    }
                    tablaTandas.Rows.Add(fila);
                    foreach (TandaItem tandaItem in tanda.TandaItems)
                    {
                        DataRow filaItem = tablaTandas.NewRow();
                        filaItem["CodigoTandaItem"] = tandaItem.Codigo;
                        filaItem["Talle"] = tandaItem.Talle.Descripcion;
                        filaItem["Color"] = tandaItem.Color.Descripcion;
                        filaItem["Cantidad"] = tandaItem.Cantidad;
                        Tipo tipo = CatalogoTipo.RecuperarPorTalle(tandaItem.Talle.Codigo, nhSesion);
                        filaItem["Tipo"] = tipo.Descripcion;
                        tablaTandas.Rows.Add(filaItem);
                    }
                }

                return tablaTandas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarTandaAEliminar(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                if (tanda.FechaFin != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void EliminarTanda(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                CatalogoTanda.Eliminar(tanda, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region TandaItem

        public static void InsertarActualizarTandaItem(int codigoTandaItem, int codigoTanda, int cantidad, int codigoTalle, int codigoColor)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                TandaItem tandaItem;
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);

                if (codigoTandaItem == 0)
                {
                    tandaItem = new TandaItem();
                    tanda.TandaItems.Add(tandaItem);
                }
                else
                {
                    tandaItem = tanda.TandaItems.Where(x => x.Codigo == codigoTandaItem).First();
                }

                tandaItem.Cantidad = cantidad;
                tandaItem.Color = CatalogoColor.RecuperarPorCodigo(codigoColor, nhSesion);
                tandaItem.Talle = CatalogoTalle.RecuperarPorCodigo(codigoTalle, nhSesion);

                CatalogoTanda.InsertarActualizar(tanda, nhSesion);

            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTandaItems()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandaItems = new DataTable();
                tablaTandaItems.Columns.Add("codigoTandaItem");
                tablaTandaItems.Columns.Add("codigoTanda");
                tablaTandaItems.Columns.Add("cantidad");
                tablaTandaItems.Columns.Add("codigoColor");
                tablaTandaItems.Columns.Add("codigoTalle");
                tablaTandaItems.Columns.Add("codigoTipo");

                List<TandaItem> listaTandaItem = CatalogoTandaItem.RecuperarTodos(nhSesion);
                foreach (TandaItem tandaItem in listaTandaItem)
                {
                    Tanda tanda = CatalogoTanda.RecuperarPorTandaItem(tandaItem.Codigo, nhSesion);
                    Tipo tipo = CatalogoTipo.RecuperarPorTalle(tandaItem.Talle.Codigo, nhSesion);
                    tablaTandaItems.Rows.Add(tandaItem.Codigo, tanda.Codigo, tandaItem.Cantidad, tandaItem.Color.Codigo, tandaItem.Talle.Codigo, tipo.Codigo);
                }
                return tablaTandaItems;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTandaItemsPorTanda(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandaItems = new DataTable();
                tablaTandaItems.Columns.Add("codigoTandaItem");
                tablaTandaItems.Columns.Add("codigoTanda");
                tablaTandaItems.Columns.Add("cantidad");
                tablaTandaItems.Columns.Add("codigoColor");
                tablaTandaItems.Columns.Add("codigoTalle");
                tablaTandaItems.Columns.Add("codigoTipo");
                tablaTandaItems.Columns.Add("tipo");
                tablaTandaItems.Columns.Add("talle");
                tablaTandaItems.Columns.Add("color");

                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                List<TandaItem> listaTandaItem = tanda.TandaItems.ToList();
                foreach (TandaItem tandaItem in listaTandaItem)
                {
                    Tipo tipo = CatalogoTipo.RecuperarPorTalle(tandaItem.Talle.Codigo, nhSesion);
                    tablaTandaItems.Rows.Add(tandaItem.Codigo, tanda.Codigo, tandaItem.Cantidad, tandaItem.Color.Codigo, tandaItem.Talle.Codigo, tipo.Codigo, tipo.Descripcion, tandaItem.Talle.Descripcion, tandaItem.Color.Descripcion);
                }
                return tablaTandaItems;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodosTandaItemsPorTandaDisponibles(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandaItems = new DataTable();
                tablaTandaItems.Columns.Add("codigoTandaItem");
                tablaTandaItems.Columns.Add("codigoTanda");
                tablaTandaItems.Columns.Add("cantidad");
                tablaTandaItems.Columns.Add("codigoColor");
                tablaTandaItems.Columns.Add("codigoTalle");
                tablaTandaItems.Columns.Add("codigoTipo");
                tablaTandaItems.Columns.Add("tipo");
                tablaTandaItems.Columns.Add("talle");
                tablaTandaItems.Columns.Add("color");
                tablaTandaItems.Columns.Add("cantidadDisponible");
                tablaTandaItems.Columns.Add("cantidadTotal");

                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                List<TandaItem> listaTandaItem = tanda.TandaItems.ToList();
                List<Entrega> entregasVigentes = tanda.Entregas.Where(x => x.FechaFin == null).ToList();
                foreach (TandaItem tandaItem in listaTandaItem)
                {
                    int cantidad = tandaItem.Cantidad;
                    foreach (Entrega entrega in entregasVigentes)
                    {
                        EntregaItem entregaItem = entrega.EntregaItems.Where(x => x.TandaItem.Codigo == tandaItem.Codigo).FirstOrDefault();
                        if (entregaItem != null)
                        {
                            cantidad -= entregaItem.Cantidad;
                        }
                    }
                    if (cantidad != 0)
                    {
                        Tipo tipo = CatalogoTipo.RecuperarPorTalle(tandaItem.Talle.Codigo, nhSesion);
                        tablaTandaItems.Rows.Add(tandaItem.Codigo, tanda.Codigo, tandaItem.Cantidad, tandaItem.Color.Codigo, tandaItem.Talle.Codigo, tipo.Codigo, tipo.Descripcion, tandaItem.Talle.Descripcion, tandaItem.Color.Descripcion, cantidad, tandaItem.Cantidad);
                    }
                }
                return tablaTandaItems;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarTalleEnTanda(int codigoTanda, int codigoTandaItem, int codigoTalle, int codigoColor)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                TandaItem tandaItem = tanda.TandaItems.Where(x => x.Codigo != codigoTandaItem && x.Color.Codigo == codigoColor && x.Talle.Codigo == codigoTalle).FirstOrDefault();
                if (tandaItem != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarTandaItemAEliminar(int codigoTanda, int codigoTandaItem)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                var entregaItem = tanda.Entregas.Where(x => x.EntregaItems.Where(y => y.TandaItem.Codigo == codigoTandaItem).Count() > 0).Count();
                if (entregaItem > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void EliminarTandaItem(int codigoTanda, int codigoTandaItem)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                TandaItem tandaItem = tanda.TandaItems.Where(x => x.Codigo == codigoTandaItem).First();
                tanda.TandaItems.Remove(tandaItem);
                CatalogoTanda.InsertarActualizar(tanda, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Entrega

        public static void InsertarActualizarEntrega(int codigoEntrega, int codigoTanda, int codigoTaller, DateTime fechaInicio, DateTime? fechaFin)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Entrega entrega;
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);

                if (codigoEntrega == 0)
                {
                    entrega = new Entrega();
                    tanda.Entregas.Add(entrega);
                }
                else
                {
                    entrega = tanda.Entregas.Where(x => x.Codigo == codigoEntrega).First();
                }

                entrega.FechaFin = fechaFin;
                entrega.FechaInicio = fechaInicio;
                entrega.Taller = CatalogoTaller.RecuperarPorCodigo(codigoTaller, nhSesion);

                CatalogoTanda.InsertarActualizar(tanda, nhSesion);

            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void InsertarActualizarEntrega(int codigoEntrega, int codigoTanda, int codigoTaller, DateTime fechaInicio, DateTime? fechaFin, DataTable tablaEntregaItems)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Entrega entrega;
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);

                if (codigoEntrega == 0)
                {
                    entrega = new Entrega();
                    tanda.Entregas.Add(entrega);
                }
                else
                {
                    entrega = tanda.Entregas.Where(x => x.Codigo == codigoEntrega).First();
                }

                entrega.FechaFin = fechaFin;
                entrega.FechaInicio = fechaInicio;
                entrega.Taller = CatalogoTaller.RecuperarPorCodigo(codigoTaller, nhSesion);

                foreach (DataRow fila in tablaEntregaItems.Rows)
                {
                    int codigoTandaItem = Convert.ToInt32(fila["codigoTandaItem"]);
                    EntregaItem entregaItem = entrega.EntregaItems.Where(x => x.TandaItem.Codigo == codigoTandaItem).FirstOrDefault();
                    if (entregaItem == null)
                    {
                        entregaItem = new EntregaItem();
                        entregaItem.TandaItem = CatalogoTandaItem.RecuperarPorCodigo(codigoTandaItem, nhSesion);
                        entrega.EntregaItems.Add(entregaItem);
                    }
                    entregaItem.Cantidad = Convert.ToInt32(fila["cantidad"]);
                }

                CatalogoTanda.InsertarActualizar(tanda, nhSesion);

            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodasEntregas()
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaEntregas = new DataTable();
                tablaEntregas.Columns.Add("codigoEntrega");
                tablaEntregas.Columns.Add("codigoTanda");
                tablaEntregas.Columns.Add("fechaInicio", typeof(DateTime));
                tablaEntregas.Columns.Add("fechaFin", typeof(DateTime));
                tablaEntregas.Columns.Add("codigoTaller");
                tablaEntregas.Columns.Add("codigoTipoTaller");

                List<Entrega> listaEntregas = CatalogoEntrega.RecuperarTodos(nhSesion);
                foreach (Entrega entrega in listaEntregas)
                {
                    Tanda tanda = CatalogoTanda.RecuperarPorEntrega(entrega.Codigo, nhSesion);
                    TipoTaller tipo = CatalogoTipoTaller.RecuperarPorTaller(entrega.Taller.Codigo, nhSesion);
                    tablaEntregas.Rows.Add(entrega.Codigo, tanda.Codigo, entrega.FechaInicio, entrega.FechaFin, entrega.Taller.Codigo, tipo.Codigo);
                }
                return tablaEntregas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarTodasEntregasPorTanda(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaEntregas = new DataTable();
                tablaEntregas.Columns.Add("codigoEntrega");
                tablaEntregas.Columns.Add("codigoTanda");
                tablaEntregas.Columns.Add("fechaInicio", typeof(DateTime));
                tablaEntregas.Columns.Add("fechaFin", typeof(DateTime));
                tablaEntregas.Columns.Add("codigoTaller");
                tablaEntregas.Columns.Add("codigoTipoTaller");

                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                List<Entrega> listaEntregas = tanda.Entregas.ToList();
                foreach (Entrega entrega in listaEntregas)
                {
                    TipoTaller tipo = CatalogoTipoTaller.RecuperarPorTaller(entrega.Taller.Codigo, nhSesion);
                    tablaEntregas.Rows.Add(entrega.Codigo, tanda.Codigo, entrega.FechaInicio, entrega.FechaFin, entrega.Taller.Codigo, tipo.Codigo);
                }
                return tablaEntregas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static string VerificarEntregasPendientesPorTanda(int codigoTanda)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                if (tanda.FechaFin != null)
                {
                    return "No se pueden agregar entregas en una tanda si esta ya tiene fecha de fin";
                }
                else
                {
                    if (tanda.Entregas.Where(x => x.FechaFin == null).Any())
                    {
                        return "No se pueden agregar entregas en una tanda si tiene entregas sin fecha de fin";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarEntregaPorCodigo(int codigoEntrega)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaEntregas = new DataTable();
                tablaEntregas.Columns.Add("codigoEntrega");
                tablaEntregas.Columns.Add("codigoTanda");
                tablaEntregas.Columns.Add("fechaInicio", typeof(DateTime));
                tablaEntregas.Columns.Add("fechaFin", typeof(DateTime));
                tablaEntregas.Columns.Add("codigoTaller");
                tablaEntregas.Columns.Add("codigoTipoTaller");

                Entrega entrega = CatalogoEntrega.RecuperarPorCodigo(codigoEntrega, nhSesion);
                Tanda tanda = CatalogoTanda.RecuperarPorEntrega(entrega.Codigo, nhSesion);
                TipoTaller tipo = CatalogoTipoTaller.RecuperarPorTaller(entrega.Taller.Codigo, nhSesion);
                tablaEntregas.Rows.Add(entrega.Codigo, tanda.Codigo, entrega.FechaInicio, entrega.FechaFin, entrega.Taller.Codigo, tipo.Codigo);
                return tablaEntregas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static DataTable RecuperarEntregasParaReporte(int articulo)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandas = new DataTable();
                tablaTandas.Columns.Add("CodigoTanda");
                tablaTandas.Columns.Add("CodigoTandaItem");
                tablaTandas.Columns.Add("Comentario");
                tablaTandas.Columns.Add("Articulo");
                tablaTandas.Columns.Add("FechaInicio");
                tablaTandas.Columns.Add("FechaFin");
                tablaTandas.Columns.Add("Tipo");
                tablaTandas.Columns.Add("Talle");
                tablaTandas.Columns.Add("Cantidad");
                tablaTandas.Columns.Add("Color");
                tablaTandas.Columns.Add("Taller");
                tablaTandas.Columns.Add("FechaInicioEntrega");
                tablaTandas.Columns.Add("FechaFinEntrega");
                List<Tanda> tandas = new List<Tanda>();
                if (articulo == -1)
                {
                    tandas = CatalogoTanda.RecuperarAbiertas(nhSesion).ToList();
                }
                else
                {
                    tandas = CatalogoTanda.RecuperarAbiertasPorArticulo(articulo, nhSesion).ToList();
                }

                foreach (Tanda tanda in tandas)
                {
                    List<Entrega> entregasPendientes = tanda.Entregas.Where(x => x.FechaFin == null).ToList();
                    if (entregasPendientes.Count > 0)
                    {
                        DataRow fila = tablaTandas.NewRow();
                        fila["CodigoTanda"] = tanda.Codigo;
                        fila["Comentario"] = tanda.Comentario;
                        fila["Articulo"] = tanda.Articulo.Descripcion;
                        fila["FechaInicio"] = tanda.FechaInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (tanda.FechaFin != null)
                        {
                            fila["FechaFin"] = tanda.FechaFin != null ? tanda.FechaFin.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                        }
                        tablaTandas.Rows.Add(fila);
                        foreach (Entrega entrega in entregasPendientes)
                        {
                            DataRow filaEntrega = tablaTandas.NewRow();
                            filaEntrega["Taller"] = entrega.Taller.Descripcion;
                            filaEntrega["FechaInicioEntrega"] = entrega.FechaInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            tablaTandas.Rows.Add(filaEntrega);
                            foreach (EntregaItem entregaItem in entrega.EntregaItems)
                            {
                                DataRow filaItem = tablaTandas.NewRow();
                                filaItem["CodigoTandaItem"] = entregaItem.TandaItem.Codigo;
                                filaItem["Talle"] = entregaItem.TandaItem.Talle.Descripcion;
                                filaItem["Color"] = entregaItem.TandaItem.Color.Descripcion;
                                filaItem["Cantidad"] = entregaItem.Cantidad;
                                Tipo tipo = CatalogoTipo.RecuperarPorTalle(entregaItem.TandaItem.Talle.Codigo, nhSesion);
                                filaItem["Tipo"] = tipo.Descripcion;
                                tablaTandas.Rows.Add(filaItem);
                            }
                        }
                    }
                }

                return tablaTandas;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static bool ValidarEntregaAEliminar(int codigoTanda, int codigoEntrega)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                Entrega entrega = tanda.Entregas.Where(x => x.Codigo == codigoEntrega).First();
                if (entrega.FechaFin == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        public static void EliminarEntrega(int codigoTanda, int codigoEntrega)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                Tanda tanda = CatalogoTanda.RecuperarPorCodigo(codigoTanda, nhSesion);
                Entrega entrega = tanda.Entregas.Where(x => x.Codigo == codigoEntrega).First();
                tanda.Entregas.Remove(entrega);
                CatalogoTanda.InsertarActualizar(tanda, nhSesion);
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        #region Entrega Items

        public static DataTable RecuperarTandaItemsPorEntrega(int codigoEntrega)
        {
            ISession nhSesion = ManejoNHibernate.IniciarSesion();

            try
            {
                DataTable tablaTandaItems = new DataTable();
                tablaTandaItems.Columns.Add("codigoTandaItem");
                tablaTandaItems.Columns.Add("codigoTanda");
                tablaTandaItems.Columns.Add("cantidad");
                tablaTandaItems.Columns.Add("codigoColor");
                tablaTandaItems.Columns.Add("codigoTalle");
                tablaTandaItems.Columns.Add("codigoTipo");
                tablaTandaItems.Columns.Add("tipo");
                tablaTandaItems.Columns.Add("talle");
                tablaTandaItems.Columns.Add("color");
                tablaTandaItems.Columns.Add("cantidadDisponible");
                tablaTandaItems.Columns.Add("cantidadTotal");

                Tanda tanda = CatalogoTanda.RecuperarPorEntrega(codigoEntrega, nhSesion);
                Entrega entrega = CatalogoEntrega.RecuperarPorCodigo(codigoEntrega, nhSesion);

                List<EntregaItem> listaEntregaItem = entrega.EntregaItems.ToList();
                List<Entrega> entregasVigentes = tanda.Entregas.Where(x => x.FechaFin == null).ToList();
                foreach (EntregaItem entregaItem in listaEntregaItem)
                {
                    int cantidad = entregaItem.TandaItem.Cantidad;
                    foreach (Entrega entregaVig in entregasVigentes)
                    {
                        EntregaItem entregaItemVigente = entregaVig.EntregaItems.Where(x => x.TandaItem.Codigo == entregaItem.TandaItem.Codigo).FirstOrDefault();
                        if (entregaItemVigente != null && entregaVig.Codigo != entrega.Codigo)
                        {
                            cantidad -= entregaItemVigente.Cantidad;
                        }
                    }



                    Tipo tipo = CatalogoTipo.RecuperarPorTalle(entregaItem.TandaItem.Talle.Codigo, nhSesion);
                    tablaTandaItems.Rows.Add(entregaItem.TandaItem.Codigo, tanda.Codigo, entregaItem.Cantidad, entregaItem.TandaItem.Color.Codigo,
                        entregaItem.TandaItem.Talle.Codigo, tipo.Codigo, tipo.Descripcion, entregaItem.TandaItem.Talle.Descripcion, entregaItem.TandaItem.Color.Descripcion, cantidad, entregaItem.TandaItem.Cantidad);
                }
                return tablaTandaItems;
            }
            catch (Exception ex)
            {
                EscribirLog(ex);
                throw ex;
            }
            finally
            {
                nhSesion.Close();
                nhSesion.Dispose();
            }
        }

        #endregion

        private static void EscribirLog(Exception ex)
        {
            if (ex != null)
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Error.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\Error.txt").Close();
                }

                string inner = ex.InnerException == null ? string.Empty : ex.InnerException.ToString();

                string[] s = new string[8];

                s[0] = "Error: " + DateTime.Now;
                s[1] = "INNER EXCEPTION: " + inner;
                s[2] = "STACK TRACE: " + ex.StackTrace.ToString();
                s[3] = "MESSAGE: " + ex.Message.ToString();
                s[4] = "SOURCE: " + ex.Source.ToString();
                s[5] = "TARGET SIZE: " + ex.TargetSite.ToString();
                s[6] = "HRESULT: " + ex.HResult.ToString();
                s[7] = "-------------------------------------------------";

                File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Error.txt", s);
            }
        }
    }
}
