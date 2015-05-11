using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control
{
    public class ControlSliderImage:PadreLib
    {
        #region "gets"
            public List<SliderImage> sp_adminfe_getSliderImage(int idPaginaFe,int idUsuarioEjecutor, int idPagina)
            {
                List<SliderImage> imagenes = null;
                SliderImage slider;Pagina pagina;
                SPIUS sp = new SPIUS("sp_adminfe_getSliderImage");
                sp.agregarParametro("idPaginaFe",idPaginaFe);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            imagenes = new List<SliderImage>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                pagina = new Pagina((int)row["id_pagina_fk"]);
                                slider = new SliderImage((int)row["idSliderImage"], row["nombre"].ToString(), (byte[])row["imagen"], (bool)row["estado"], pagina, (DateTime)row["fecha_creacion"]);
                                imagenes.Add(slider);
                            }
                        }
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return imagenes;
            }
        #endregion
        #region "acciones"
            public SliderImage sp_adminfe_saveImageSlider(SliderImage imageAgregar,int idUsuarioEjecutor,int idPagina)
            {
                SliderImage imageAgregada = null;
                Pagina pagina;
                SPIUS sp = new SPIUS("sp_adminfe_saveImageSlider");
                sp.agregarParametro("nombre", imageAgregar._nombre);
                sp.agregarParametro("imagen", imageAgregar._imagen);
                sp.agregarParametro("estado", imageAgregar._estado);
                sp.agregarParametro("idPaginaSlider", imageAgregar._pagina._idPagina);
                // seguridad 
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina",idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row     = tb[1].Rows[0];
                            pagina          = new Pagina((int)row["id_pagina_fk"]);
                            imageAgregada   = new SliderImage((int)row["idSliderImage"], row["nombre"].ToString(), (byte[])row["imagen"], (bool)row["estado"], pagina, (DateTime)row["fecha_creacion"]);
                        }
                    }
                }catch(ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return imageAgregada;
            }
        #endregion
    }
}
