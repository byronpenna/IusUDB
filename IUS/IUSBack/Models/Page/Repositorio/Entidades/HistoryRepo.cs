using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias externas
    using IUSLibs.REPO.Entidades;
namespace IUSBack.Models.Page.Repositorio.Entidades
{
    public class HistoryRepo
    {
        #region "propiedades"
            private List<Carpeta> _carpetaAnterior;    
            private Carpeta _carpetaActual;
            private List<Carpeta> _carpetaAdelante;
        #endregion
        #region "metodos"
            public Carpeta historyBack()
            {
                Carpeta carpetaRetorno = this._carpetaAnterior[this._carpetaAnterior.Count - 1];
                this._carpetaAdelante.Add( this._carpetaActual);
                this._carpetaActual = carpetaRetorno;
                this._carpetaAnterior.Remove(carpetaRetorno);
                return carpetaRetorno;
            }
            public Carpeta historyFoward()
            {
                Carpeta carpetaRetorno = this._carpetaAdelante[this._carpetaAdelante.Count - 1];
                this._carpetaAnterior.Add(this._carpetaActual);
                this._carpetaActual = carpetaRetorno;
                this._carpetaAdelante.Remove(carpetaRetorno);
                return carpetaRetorno;
            }
            public void insertHistory(int idCarpeta)
            {
                int cn = this._carpetaAnterior.Count; bool ingresar = true;
                Carpeta carpeta = new Carpeta(idCarpeta);
                if (cn > 0)
                {
                    //Carpeta anteriorActual = this._carpetaAnterior[cn - 1];
                    if (this._carpetaActual._idCarpeta == carpeta._idCarpeta)
                    {
                        ingresar =  false;
                    }
                }
                if (ingresar) {
                    this._carpetaAnterior.Add(this._carpetaActual);
                }
                this._carpetaActual = carpeta;
            }
        #endregion
        #region "constructores"
            public HistoryRepo(int idCarpeta) {
                Carpeta carpetaActual = new Carpeta(idCarpeta);
                this._carpetaActual = carpetaActual;
                //
                this._carpetaAnterior = new List<Carpeta>();
                this._carpetaAdelante = new List<Carpeta>();
            }
        #endregion
    }
}