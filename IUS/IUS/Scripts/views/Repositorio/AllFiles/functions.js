// generics
    function btnNavHistory(frm) {
        actualizarCatalogo(RAIZ + "/Repositorio/navHistory", frm, function (data) {
            console.log("Respuesta de servidor", data);
            if (data.estado) {
                window.location = data.url;
            }
        })
    }
    // acciones genericas   
        // acciones vista
            function getNumVistaActual() {
                var vista = getVistaActual();
                var n = -1;
                switch (vista) {
                    case "cuadricula": {
                        n = -1;
                        break;
                    }
                    case "lista": {
                        n = 1;
                        break;
                    }
                }
                return n;
            }
            function getVistaActual() {
                var vista = "";
                if ($(".iconoVistaCuadricula").hasClass("activeVista")) {
                    vista = "cuadricula";
                } else if ($(".icoVistaLista").hasClass("activeVista")) {
                    vista = "lista";
                }
                return vista;
            }
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
    function cancelarBusqueda(frm,seccion,vista) {
        actualizarCatalogo(RAIZ + "Repositorio/getArchivosSinBusqueda", frm, function (data) {
            console.log(data);
            if (data.estado) {
                var div = "";
                if (data.carpetas !== undefined && data.carpetas !== null) {
                    $.each(data.carpetas, function (i, carpeta) {
                        if (vista == "cuadricula") {
                            div += getDivCarpeta(carpeta);
                        } else if (vista == "lista") {
                            div += getDivListaCarpeta(carpeta);
                        }
                        
                    })
                }
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        if (vista == "cuadricula") {
                            div += getDivArchivo(archivo);
                        } else if (vista == "lista") {
                            div += getDivListaArchivo(archivo);
                        }
                    })
                }
                //$(".folders").empty().append(div);
                seccion.empty().append(div);
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
                <a href='" + RAIZ + "/Repositorio/AllFiles/" + carpeta._idCarpetaPublica + "/-1/1' class='aListaCarpeta'>\
                    <div class='row marginNull folderDetalles'>\
                        <div class='col-lg-6'>\
                            <i class='fa fa-folder'></i> " + carpeta._nombre + " \
                        </div>\
                        <div class='col-lg-2'>\
                            Folder\
                        </div>\
                        <div class='col-lg-3'>\
                            \
                        </div>\
                        <div class='col-lg-1'>\
                            \
                        </div>\
                    </div>\
                </a>\
            ";
            return div;
        }
        function getDivListaArchivo(archivo,idCategoria) {
            var accion="";
            if (idCategoria !== undefined && idCategoria == -1) {
                accion = "AllFiles";
            } else {
                accion = "FileByCategory";
            }
            var div = "\
                <div class='row marginNull folderDetalles'>\
                    <div class='col-lg-6'>\
                        " + archivo._nombre + "\
                    </div>\
                    <div class='col-lg-2'>\
                        " + archivo._archivoUsuario._extension._tipoArchivo._tipoArchivo + "\
                    </div>\
                    <div class='col-lg-3'>\
                        "+archivo.getFechaCreacion+"\
                    </div>\
                    <div class='col-lg-1'>\
                        <a href='" + RAIZ + "/Repositorio/downloadFile/" + archivo._idArchivoPublico+ "'>\
                            <i class='fa fa-download iconoHerramientas'></i>\
                        </a>";
                if (idCategoria !== undefined) {
                    div += "\
                            <a href='" + RAIZ + "/Repositorio/" + accion + "/" + archivo._carpetaPublica._idCarpetaPublica + "/" + idCategoria + "/1' class='ico btn btn-default' title='Abrir ubicacion de archivo'>\
                                <i class='fa fa-folder-open'></i>\
                            </a>";
                }
                div += "\
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
            //<div class='row divHerramientasIndividual'>\
            var div = "<div class='col-xs-6 col-sm-4 col-md-3 col-lg-2 folder'>\
                            <div class='row marginNull divTarjetaFile'>\
                        ";
            /*if (idCategoria !== undefined) {
                div += "\
                    <a href='" + RAIZ + "/Repositorio/" + accion + "/" + archivo._carpetaPublica._idCarpetaPublica + "/" + idCategoria + "' class='ico' title='Abrir ubicacion de archivo'>\
                        <i class='fa fa-folder-open'></i>\
                    </a>";
            }*/
            div += "\
                      </div>\
                        <div class='cuadritoIcono '>\
                            <img src='"+RAIZ+"/Content/images/views/repositorio/"+archivo._archivoUsuario._extension._tipoArchivo._icono+"' />\
                        </div>\
                        <div class='row marginNull'>\
                            <h3 class='folderTitle'>"+archivo._nombre+"</h3>\
                            <a href='" + RAIZ + "/Repositorio/downloadFile/" + archivo._idArchivoPublico + "' class='ico btn btn-block btn-default' title='Descargar'>\
                                Descargar\
                            </a>";
            if (idCategoria !== undefined) {
                div += "\
                    <a href='" + RAIZ + "/Repositorio/" + accion + "/" + archivo._carpetaPublica._idCarpetaPublica + "/" + idCategoria + "' class='ico btn btn-block btn-default' title='Abrir ubicacion de archivo'>\
                        Abrir en carpeta\
                    </a>";
            }
            //<i class='fa fa-folder-open'></i>
            div += "\
                        </div>\
                    </div>\
                  </div> ";
            return div;
        }
// acciones scripts 
    // otros eventos
        function isSearch() {
            return $(".btnBuscarCarpeta").hasClass("btnBuscando");
        }
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
                    if (accion == "cuadricula") {
                        $("." + accion + "").empty().append(div);
                    } else if(accion == "lista"){
                        $(".targetListView").empty().append(div);
                    }
                    
                    vistaActiva(accion);
                } else {
                    alert("Error al recuperar vista");
                }
                
            })
        }
    // eventos normales 
        function btnBuscarCarpeta(frm,seccion,vista,callback) {
            var idCategoria = frm.idCategoria;
            console.log("la vista es", vista);
            actualizarCatalogo(RAIZ + "Repositorio/sp_repo_searchArchivoPublico", frm, function (data) {
                console.log("Respuesta de busqueda",data);
                if (data.estado) {
                    $(".tituloPrincipal").empty().append("Resultados busqueda");
                    var div = "";
                    if (data.encontrado) {
                        if (data.archivos !== undefined && data.archivos !== null) {
                            $.each(data.archivos, function (i, archivo) {
                                if (vista == "cuadricula") {
                                    div += getDivArchivo(archivo, idCategoria);
                                } else if (vista == "lista") {
                                    div += getDivListaArchivo(archivo, idCategoria);
                                }

                            })
                        }
                    } else {
                        if (data.notFoundMjs === null) {
                            data.notFoundMjs = "";
                        }
                        div += "\
                        <div class='resultadosNotFound'>\
                            <img src='"+ RAIZ + "Content/images/generales/sadcloud.png'>\
                            <div class='row marginNull'>\
                                "+data.notFoundMjs+"\
                            </div>\
                        </div>\
                        ";
                    }
                    //$(".folders").empty().append(div);
                    seccion.empty().append(div);
                    cambiarEstado("buscar");
                    if (callback !== undefined) {
                        callback();
                    }
                    /*seccion.empty().append("<i class='fa fa-times'></i>");
                    seccion.addClass("btnBuscando");*/
                } else {
                    alert("Ocurrio un error agregando");
                }
            
            })
        }
        function spIrBuscar(frm, seccion) {
            actualizarCatalogo(RAIZ + "Repositorio/sp_repo_front_getCarpetaPublicaByRuta", frm, function (data) {
                if (data.estado) {
                    var accionControlador = "";
                    if (frm.txtIdFiltro == -1) {
                        accionControlador = "AllFiles";
                    } else {
                        accionControlador = "FileByCategory";
                    }
                    var nVista = getNumVistaActual();
                    //console.log(nVista);
                    window.location = RAIZ + "Repositorio/" + accionControlador + "/" + data.carpetaPublica._idCarpetaPublica + "/" + frm.txtIdFiltro + "/" + nVista;
                } else {
                    if (data.error._mostrar) {
                        printMessage($(".divMensajesGenerales"), data.error.Message, false);
                        //alert(data.error.Message);
                    } else {
                        printMessage($(".divMensajesGenerales"), "Ocurrio un error", false);
                        //alert("Ocurrio un error");
                    }
                
                }
            })
        }