﻿// generics
    // acciones genericas   
    function vistaActiva(txtVista) {
        $(".controlVista").removeClass("activeVista")
        switch (txtVista) {
            
            case "cuadricula":
                {
                    $(".cuadricula").removeClass("hidden");
                    $(".iconoVistaCuadricula").addClass("activeVista");
                    $(".lista").addClass("hidden");
                    break;
                }
            case "lista":
                {
                    $(".lista").removeClass("hidden");
                    $(".icoVistaLista").addClass("activeVista");
                    $(".cuadricula").addClass("hidden");
                    break;
                }
        }
    }
    function cambiarEstado(txt) {
        switch (txt) {
            case "buscar":
                {
                    $(".btnBuscarCarpeta").empty().append("<i class='fa fa-times'></i>");
                    $(".btnBuscarCarpeta").addClass("btnBuscando");
                    break;
                }
            case "cancelar_busqueda":
                {
                    $(".btnBuscarCarpeta").empty().append("Buscar");
                    $(".btnBuscarCarpeta").removeClass("btnBuscando");
                    break;
                }
        }
    }
    function cancelarBusqueda(frm) {
        actualizarCatalogo(RAIZ + "Repositorio/getArchivosSinBusqueda", frm, function (data) {
            console.log(data);
            if (data.estado) {
                var div = "";
                if (data.carpetas !== undefined && data.carpetas !== null) {
                    $.each(data.carpetas, function (i, carpeta) {
                        div += getDivCarpeta(carpeta);
                    })
                }
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        div += getDivArchivo(archivo);
                    })
                }
                $(".folders").empty().append(div);
                cambiarEstado("cancelar_busqueda");
                $(".tituloPrincipal").empty().append(data.carpetaPadre._nombre);

            } else {
                alert("Ocurrio un error mientras se buscaba");
            }
        });
    }
    function buscarCarpeta(nombre) {
        $(".folder").addClass("hidden");
        var folders = $(".folder .folderTitle:containsi(" + nombre + ")");
        folders = folders.parents(".folder");
        folders.removeClass("hidden");
    }
    //div 
        function getDivListaCarpeta(carpeta) {
            var div = "\
                <div class='row marginNull folderDetalles'>\
                    <a href='" + RAIZ + "/Repositorio/AllFiles/" + carpeta._idCarpetaPublica + "/-1'>\
                        <div class='col-lg-6'>\
                            " + carpeta._nombre + "\
                        </div>\
                    </a>\
                    <div class='col-lg-3'>\
                        Folder\
                    </div>\
                    <div class='col-lg-3'>\
                        Fecha de creación\
                    </div>\
                </div>\
            ";
            return div;
        }
        function getDivListaArchivo(archivo) {
            var div = "\
                <div class='row marginNull folderDetalles'>\
                    <div class='col-lg-6'>\
                        " + archivo._nombre + "\
                    </div>\
                    <div class='col-lg-3'>\
                        Folder\
                    </div>\
                    <div class='col-lg-3'>\
                        Fecha de creación\
                    </div>\
                </div>\
            ";
            return div;
        }
        // cuadricula
        function getDivCarpeta(carpeta) {
            var div = "<div class='col-xs-6 col-sm-4 col-md-3 col-lg-2 folder'>\
                    <a href='" + RAIZ + "/Repositorio/" + $(".txtHdAccion").val() + "/" + carpeta._idCarpetaPublica + "/" + $(".txtHdTipoCategoria").val() + "'>\
                        <div class='row divHerramientasIndividual visiHidden'>\
                            <i class='fa fa-download'></i>\
                        </div>\
                        <div class='cuadritoIcono cuadritoCarpeta'>\
                            <img src='"+RAIZ+"/Content/images/views/repositorio/"+carpeta.getIconoFront+"' />\
                            <h3 class='folderTitle'>"+carpeta._nombre+"</h3>\
                        </div>\
                    </a>\
                </div>";
            return div;
        }
        function getDivArchivo(archivo,idCategoria){
            var accion = "";
            if (idCategoria == -1) {
                accion = "AllFiles";
            } else {
                accion = "FileByCategory";
            }
            var div = "<div class='col-xs-6 col-sm-4 col-md-3 col-lg-2 folder'>\
                      <div class='row divHerramientasIndividual'>\
                          <a href='" + RAIZ + "/Repositorio/downloadFile/" + archivo._idArchivoPublico + "' class='ico' title='Descargar'>\
                              <i class='fa fa-download iconoHerramientas'></i>\
                          </a>";
            if (idCategoria !== undefined) {
                div += "\
                    <a href='" + RAIZ + "/Repositorio/" + accion + "/" + archivo._carpetaPublica._idCarpetaPublica + "/" + idCategoria + "' class='ico' title='Abrir ubicacion de archivo'>\
                        <i class='fa fa-folder-open'></i>\
                    </a>";
            }
            div += "\
                      </div>\
                      <div class='cuadritoIcono '>\
                          <img src='"+RAIZ+"/Content/images/views/repositorio/"+archivo._archivoUsuario._extension._tipoArchivo._icono+"' />\
                          <h3 class='folderTitle'>"+archivo._nombre+"</h3>\
                      </div>\
                  </div> ";
            return div;
        }
// acciones scripts 
    // otros eventos
        function icoVistaLista(frm,accion) {
            //console.log("llego aqui ");
            actualizarCatalogo(RAIZ + "Repositorio/getArchivosSinBusqueda", frm, function (data) {
                console.log("Data es: ", data);
                if (data.estado) {
                    var div = "";
                    if (data.carpetas !== undefined && data.carpetas !== null) {
                        $.each(data.carpetas, function (i, carpeta) {
                            if (accion == "lista") {
                                div += getDivListaCarpeta(carpeta);
                            } else if(accion == "cuadricula"){
                                div += getDivCarpeta(carpeta);
                            }
                        })
                    }
                    if (data.archivos !== undefined && data.archivos !== null) {
                        $.each(data.archivos, function (i, archivo) {
                            if (accion == "lista") {
                                div += getDivListaArchivo(archivo);
                            } else if (accion == "cuadricula") {
                                div += getDivArchivo(archivo);
                            }
                        })
                    }
                    $("."+accion+"").empty().append(div);
                    vistaActiva(accion);
                } else {
                    alert("Error al recuperar vista");
                }
                
            })
        }
    // eventos normales 
        function btnBuscarCarpeta(frm,seccion) {
            var idCategoria = frm.idCategoria;
            actualizarCatalogo(RAIZ + "Repositorio/sp_repo_searchArchivoPublico", frm, function (data) {
                console.log("Respuesta de busqueda",data);
                if (data.estado) {
                    $(".tituloPrincipal").empty().append("Resultados busqueda");
                    var div = "";
                    if (data.archivos !== undefined && data.archivos !== null) {
                        $.each(data.archivos,function(i,archivo){
                            div += getDivArchivo(archivo,idCategoria);
                        })
                    } else {
                        div += "\
                        <div>\
                            No se encontraron resultados\
                        </div>\
                        ";
                    }
                    $(".folders").empty().append(div);
                    cambiarEstado("buscar");
                    /*seccion.empty().append("<i class='fa fa-times'></i>");
                    seccion.addClass("btnBuscando");*/
                } else {
                    alert("Ocurrio un error agregando");
                }
            
            })
        }
        function spIrBuscar(frm, seccion) {
            actualizarCatalogo(RAIZ + "Repositorio/sp_repo_front_getCarpetaPublicaByRuta", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    var accionControlador = "";
                    if (frm.txtIdFiltro == -1) {
                        accionControlador = "AllFiles";
                    } else {
                        accionControlador = "FileByCategory";
                    }
                    window.location = RAIZ + "Repositorio/" + accionControlador + "/" + data.carpetaPublica._idCarpetaPublica + "/" + frm.txtIdFiltro;
                } else {
                    if (data.error._mostrar) {
                        alert(data.error.Message);
                    } else {
                        alert("Ocurrio un error");
                    }
                
                }
            })
        }