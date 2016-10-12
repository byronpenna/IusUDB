// generics 
    // vistas 
        // generales
            
        // cuadricula
            function verCuadricula(div,callback) {
                var seccion = div.parents(".accionesDiv");
                cambiarVistas("cuadricula");
                var frm = {
                    idCarpeta: $(".txtHdIdCarpetaPadre").val()
                }
                var seccionModificar = $(".cuadriculaView");
                iconoVistaCuadricula(frm, seccionModificar,callback);
                if (callback !== undefined) {
                    callback();
                }
            }
            function verLista(div) {
                cambiarVistas("lista");
                var frm = {
                    idCarpeta: $(".txtHdIdCarpetaPadre").val()
                }
                var seccionModificar = $(".targetListView");
                icoVistaLista(frm, seccionModificar);
            }

            function loadCuadriculaCarpeta(carpeta) {
                /*<div class='row divHerramientasIndividual'>\
                    <a href='#' class='ico' title='Descargar'>\
                        <i class='fa fa-download'></i>\
                    </a>\
                    <a href='#' class='ico icoEliminarCarpeta' title='Eliminar'>\
                        <i class='fa fa-trash-o'></i>\
                    </a>\
                </div>\*/
                var div = "\
                    <div class='col-lg-2 folder'>\
                        <input type='hidden' class='txtHdIdCarpeta' value='"+ carpeta._idCarpeta + "'/>\
                        <div class='cuadritoIcono cuadritoCarpeta'>\
                            <img class='imgCuadritoIcono' src='"+ RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/" + carpeta.getIcono + "' />\
                            <div class='btn-group'>\
                                <div class='ico btnAccion btn btn-default icoEliminarCarpeta ' title='Eliminar'>\
                                    <i class='fa fa-trash-o'></i>\
                                </div>\
                            </div>\
                            <div class='detalleCarpeta'>\
                                <div class='normalMode sinRedirect'>\
                                    <h3 class='ttlNombreCarpeta'>"+ carpeta._nombre + "</h3>\
                                </div>\
                                <div class='row marginNull hidden editMode sinRedirect'>\
                                    <div class='row marginNull inputNombreCarpeta'>\
                                        <input type='text' class='form-control txtNombreCarpeta'>\
                                    </div>\
                                    <div class='row marginNull'>\
                                        <button class='btn btn-xs btnEditarCarpeta'>Actualizar</button>\
                                        <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </div>";
                return div;
            }
            /*<a href='#' class='icoCompartirFile' title='Compartir'>\
                <i class='fa fa-share'></i>\
            </a>\
            <a href='' class='ico' title='Descargar'>\
                <i class='fa fa-download'></i>\
            </a>\
            <a href='#' class='ico icoEliminarArchivo' title='Eliminar'>\
                <i class='fa fa-trash-o'></i>\
            </a>*/
            //<div class='row divHerramientasIndividual'>\
            function loadCuadriculaFiles(archivo, openLocation) {
                var div = "\
                    <div class='col-lg-2 folder folderUni'>\
                        <input type='hidden' class='txtHdIdArchivo' value='"+archivo._idArchivo+"'/>\
                        <input type='hidden' class='txtHdIdCarpetaContenedora' value='" + archivo._carpeta._idCarpeta + "'/>\
                            ";
                /*if (openLocation !== undefined && openLocation == true) {
                    div += "<a href='" + RAIZ + 'Repositorio/ndex/' + archivo._carpeta._idCarpeta + "' class='ico icoOpenLocation' title='Abrir ubicacion'>\
                                <i class='fa fa-folder-open'></i>\
                            </a>";
                }*/
                //</div>\
                var groupClass = "btn-group";
                if (openLocation !== undefined && openLocation == true) {
                    groupClass = "btn-group-vertical";
                }
                div += "\
                        <div class='cuadritoIcono '>\
                            <img  class='imgCuadritoIcono'  src='"+RAIZ+"/Content/themes/iusback_theme/img/general/repositorio/"+archivo._extension._tipoArchivo._icono+"' />\
                            <div class='"+groupClass+"'>\
                                <a class='urlDescargar' href='"+RAIZ+"/Repositorio/DescargarFichero/"+archivo._idArchivo+"'>\
                                </a>\
                                <div class='btnAccion btn btn-default icoCompartirFile' title='Compartir'>\
                                    <i class='fa fa-share'></i>\
                                </div>\
                                <div class='btnAccion btn btn-default ico divDescargar' title='Descargar'>\
                                    <i class='fa fa-download'></i>\
                                </div>";
                if (openLocation !== undefined && openLocation == true) {
                    //<a href='" + RAIZ + 'Repositorio/ndex/' + archivo._carpeta._idCarpeta + "' class='ico icoOpenLocation' title='Abrir ubicacion'>\
                    //</a>
                    div += "</div>\
                            <div class='"+groupClass+"'>\
                                <div class='btnAccion btn btn-default ico'>\
                                    <i class='fa fa-folder-open'></i>\
                                </div>\
                                ";
                }
                div += "\
                                <div class='btnAccion btn btn-default ico icoEliminarArchivo' title='Eliminar'>\
                                    <i class='fa fa-trash-o'></i>\
                                </div>\
                            </div>\
                            <div class='detalleCarpeta'>\
                                <div class='normalMode'>\
                                    <h3 class='ttlNombreCarpeta nombreAcompartir'>" + archivo._nombre + "</h3>\
                                </div>\
                                <div class='row marginNull hidden editMode'>\
                                    <div class='row marginNull inputNombreCarpeta'>\
                                        <input type='text' class='form-control txtNombreCarpeta'>\
                                    </div>\
                                    <div class='row marginNull'>\
                                        <div class='btn-group'>\
                                            <button class='btn btn-xs btn-default btnEditarArchivo'>Hecho</button>\
                                            <button class='btn btn-xs btn-default btnCancelarEdicionCarpeta'>Cancelar</button>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                ";
                return div;
            }
        // lista
            function loadListFiles(file, url) {
                if (url === undefined) {
                    url = '#';
                } else {
                    url += '/' + file._idArchivo;
                }
                var div = "\
                    <div class='row folderDetalles folderUni'>\
                        <input type='hidden' class='txtHdIdArchivo' value='"+file._idArchivo+"'>\
                        <div class='col-lg-6'>\
                            <div class='normalMode inline'>\
                                <span class='spanNombreCarpeta sinRedirect nombreAcompartir ttlNombreCarpeta'>" + file._nombre + "</span>\
                            </div>\
                            <div class='editMode inline hidden'><input class='txtNombreCarpetaDetalle txtNombreArchivoDetalle sinRedirect'></div>\
                        </div>\
                        <div class='col-lg-2'>" + file._extension._tipoArchivo._tipoArchivo + "</div>\
                        <div class='col-lg-2'>" + file.getFechaCreacion + "</div>\
                        <div class='col-lg-2 divAccionesLista'>\
                            <div class='btn-group'>\
                                <a href='#' class='btnAccion btn btn-default icoCompartirFile' title='Compartir'>\
                                    <i class='fa fa-share'></i>\
                                </a>\
                                <a href='" + url + "' class='btnAccion btn btn-default ico' title='Descargar'>\
                                    <i class='fa fa-download'></i>\
                                </a>\
                                <a href='#' class='btnAccion btn btn-default ico icoEliminarArchivo' title='Eliminar'>\
                                    <i class='fa fa-trash-o'></i>\
                                </a>\
                            </div>\
                        </div>\
                    </div>\
                ";
                return div;
            }
            function loadListFolders(folder) {
                var div = "\
                    <div class='row folderDetalles carpetaDetalle '>\
                        <input type='hidden' class='txtHdIdCarpeta' value='"+folder._idCarpeta+"'>\
                        <div class='col-lg-6'><i class='fa fa-folder'></i>\
                        <div class='normalMode inline'><span class='spanNombreCarpeta sinRedirect ttlNombreCarpeta'>" + folder._nombre + "</span></div>\
                        <div class='editMode inline hidden'><input class='txtNombreCarpetaDetalle sinRedirect'></div>\
                        </div>\
                        <div class='col-lg-2'>Folder</div>\
                        <div class='col-lg-2'>" + folder.getFechaCreacion + "</div>\
                        <div class='col-lg-2 btnEliminarLista'>\
                            <div class='btn-block btn-default btnBack'>\
                                <i class='fa fa-trash'></i>\
                            </div>\
                        </div>\
                    </div>\
                ";
                return div;
            }
    // otras 
        function isSearch() {
            return $(".btnBusqueda").hasClass("btnBuscando");
        }
        function editarFolder(folder) {
            var frm = {
                txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                nombre: folder.find(".txtNombreCarpeta").val()
            }
            btnEditarCarpeta(frm, folder);
        }
        function editarArchivo(seccion) {
            var frm = {
                idArchivo: seccion.find(".txtHdIdArchivo").val(),
                nombreArchivo: seccion.find(".txtNombreCarpeta").val()
            }
            btnEditarArchivo(frm, seccion);
        }
        function loadPublicFiles() {
            frm = {};
            actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getRootFolderPublico", frm, function (data) {
            
                if (data.estado) {
                    div = "";
                
                    if (typeof(data.carpetas) !== null) {
                        $.each(data.carpetas, function (i, carpeta) {
                            div += getDivCarpetasPublicas(carpeta);
                        });
                    }
                    $(".divCarpetasPublicasCompartir").empty().append(div);
                }
            });
        }
        function getDivCarpetasPublicas(carpeta) {
            //<img src='" + RAIZ + "Content/themes/iusback_theme/img/general/repositorio/" + carpeta.getIcono+ "' />\
            div = "\
            <div class='divCarpetaPublica col-lg-6 pointer'>\
                <input type='hidden' class='txtHdIdCarpetaPublica' value='" + carpeta._idCarpetaPublica + "'>\
                <i class='fa fa-folder-o icoCuadricula' aria-hidden='true'></i>\
                <h4 class='tituloCarpetaPublica'>"+carpeta._nombre+"</h4>\
            </div>\
            ";
            return div;
        }
        function getImageArchivo(tipo) {
            /*
            if (archivo._extension._tipoArchivo._tipoArchivo == "Imagenes") {

            } else {

            }*/
            var ico = "<i class='fa fa-file-text-o icoCuadricula' aria-hidden='true'></i>";
            switch (tipo) {

                case "Imagenes": {
                    ico = "<i class='fa fa-picture-o icoCuadricula' aria-hidden='true'></i>";
                    break;
                }
                case "Video": {
                    ico = "<i class='fa fa-video-camera icoCuadricula' aria-hidden='true'></i>";
                    break;
                }
                case "Audio": {
                    ico: "<i class='fa fa-music icoCuadricula' aria-hidden='true'></i>";
                }
                case "Documentos": {
                    ico: "<i class='fa fa-file-text-o icoCuadricula' aria-hidden='true'></i>";
                }
            }
            return ico;
        }
        function getDivArchivosPublicos(archivo) {
            var div = "";
            console.log("extension es: ", archivo._extension);
            var ico = getImageArchivo(archivo._archivoUsuario._extension._tipoArchivo._tipoArchivo);
            div = "\
            <div class='divArchivoPublico col-lg-6'>\
                "+ico+"\
                <h4>" + archivo._nombre + "</h4>\
                <a href='" + RAIZFRONT + "Repositorio/downloadFile/" + archivo._idArchivoPublico + "' class='btn btn-default btnBack'>\
                    <i class='fa fa-download'></i>\
                </a>\
            </div>\
            ";
            return div;
        }
        function getTrArchivo(archivo, estado) {
            icoEstado = "";
            if (estado) {
                icoEstado = "<i class='fa fa-check'></i>";
            } else {
                icoEstado = "<i class='fa fa-exclamation-circle'></i>";
            }
            tr = "\
                <tr>\
                    <td>" + archivo._nombre + "</td>\
                    <td>" + archivo._extension._tipoArchivo._tipoArchivo + "</td>\
                    <td>"+ icoEstado + "</td>\
                </tr>\
            ";
            return tr;
        }
        function getStandarFolder(carpeta) {
            div = "<div class='col-lg-2 folder'>\
                        <input type='hidden' class='txtHdIdCarpeta' value='"+carpeta._idCarpeta+"'/>\
                        <div class='row divHerramientasIndividual'>\
                            <a href='#' class='ico' title='Descargar'>\
                                <i class='fa fa-download'></i>\
                            </a>\
                            <a href='#' class='ico' title='Eliminar'>\
                                <i class='fa fa-trash-o'></i>\
                            </a>\
                        </div>\
                        <div class='cuadritoIcono cuadritoCarpeta'>\
                            <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/"+carpeta.getIcono+"' />\
                            <div class='detalleCarpeta'>\
                                <div class='normalMode'>\
                                    <h3 class='ttlNombreCarpeta'>"+carpeta._nombre+"</h3>\
                                </div>\
                                <div class='row marginNull hidden editMode'>\
                                    <div class='row marginNull inputNombreCarpeta'>\
                                        <input type='text' class='form-control txtNombreCarpeta'>\
                                    </div>\
                                    <div class='row marginNull'>\
                                        <div class='btn-group'>\
                                            <button class='btn btn-xs btnEditarCarpeta'>Actualizar</button>\
                                            <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    </div>";
            return div;
        }
        function initShareFile(folder) {
            /*nombreArchivo   = folder.find(".ttlNombreCarpeta").text();
            idArchivo       = folder.find(".txtHdIdArchivo").val();*/
            nombreArchivo = folder.find(".nombreAcompartir").text();
            idArchivo = folder.find(".txtHdIdArchivo").val();
            
            var removeFileShare = "<a href='#' title='Dejar de compartir' class='icoCancelShare'><i class='fa fa-times '></i></a>";
            $(".nombreFileCompartir").empty().append(nombreArchivo+" "+removeFileShare);// aqui 
            $(".txtNombreFileCompartir").val(nombreArchivo);
            $(".txtHdIdArchivoCompartir").val(idArchivo);

        }
        function printSeccionPublica(data) {
            if (data.estado) {
                var div = "";
                var divArchivo = "";
                $(".txtHdCarpetaPadrePublica").val(data.idCarpetaPadre);
                if (data.carpetas !== null) {
                    $.each(data.carpetas, function (i, carpeta) {
                        div += getDivCarpetasPublicas(carpeta);
                    });

                }
                if (data.archivos !== null) {
                    console.log("Los archivos publicos a enviar son: ", data.archivos);
                    $.each(data.archivos, function (i, archivo) {
                        divArchivo += getDivArchivosPublicos(archivo);
                    })
                }
                $(".divCarpetasPublicasCompartir").empty().append(div);
                $(".divCarpetasPublicasCompartir").append(divArchivo);
                $(".txtRutaPublica").val(data.carpetaPadre._strRuta);

            }
            else {
                if (data.error._mostrar) {
                    printMessage($(".divMensajeRepoPublico"), data.error.Message, false);
                    //$(".divMensajeRepoPublico").empty().append("<span class='failMessage'>" + data.error.Message + "</span>");
                } else {
                    printMessage($(".divMensajeRepoPublico"), "Ocurrio un error no controlado", false);
                }
            }
        }
// scripts 
        function spIrPublico(frm) {
            actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getAjaxPublicoByRuta", frm, function (data) {
                console.log(data);
                /*Esta funcion se puede encapsular */
                printSeccionPublica(data);
            })
        }
        function vistaListaBusqueda() {
            //var seccion = $(".listView");
            var seccion = $(".targetListView");
            target = "lista";
            var frm = {
                txtBusqueda: $(".txtBusqueda").val()
            }
            btnBusqueda(frm, seccion, target);
            cambiarVistas("lista");
        }
        function vistaCuadriculaBusqueda() {
            var seccion = $(".cuadriculaView");
            target = "cuadricula";
            var frm = {
                txtBusqueda: $(".txtBusqueda").val()
            }
            btnBusqueda(frm, seccion, target);
            cambiarVistas("cuadricula");
        }
        
        function btnBusqueda(frm,seccion,target) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_searchArchivo", frm, function (data) {
                
                if (data.estado) {
                    var div = "";
                    if (data.archivos !== undefined && data.archivos !== null) {
                        $.each(data.archivos, function (i, archivo) {
                            if (target == "cuadricula") {
                                div += loadCuadriculaFiles(archivo, true);
                            } else{
                                div += loadListFiles(archivo,data.urlBase);
                            }
                        })
                    } else {
                        div += "\
                            <div class='divNofoundResults'>\
                                <img src='" + IMG_GENERALES + "/sadcloud.png'>\
                                No se han encontrado archivos\
                            </div>\
                            ";
                    }
                    seccion.empty().append(div);
                    $(".encabezadoFicheros").empty().append("Resultados de busqueda");
                } else {
                    alert("Ocurrio un error en la busqueda");
                }
            })
        }
        // vistas
            function iconoVistaCuadricula(frm, seccion,callback) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_entrarCarpeta", frm, function (data) {
                    
                    var div = "";
                    if (data.carpetas !== null) {
                        $.each(data.carpetas, function (i, folder) {
                            div += loadCuadriculaCarpeta(folder);
                        })
                    }
                    if (data.archivos !== null) {
                        $.each(data.archivos, function (i, file) {
                            div += loadCuadriculaFiles(file)
                        })
                    }
                    seccion.empty().append(div);
                    $(".encabezadoFicheros").empty().append(data.carpetaPadre._nombre);
                    if (callback !== undefined) {
                        callback();
                    }
                }, function () {
                    seccion.empty().append("<img src='" + RAIZ + "Content/themes/iusback_theme/img/general/ajax-loader.gif" + "'>");
                })
            }
            function icoVistaLista(frm, seccion) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_entrarCarpeta", frm, function (data) {
                    console.log("Vista lista para carpeta", data);
                    var div = "";
                    if (data.carpetas !== null) {
                        $.each(data.carpetas, function (i,folder) {
                            div += loadListFolders(folder);
                        })
                    }
                    if (data.archivos !== null) {
                        $.each(data.archivos, function (i, file) {
                            div += loadListFiles(file, data.urlBase);
                        })
                    }
                    seccion.empty().append(div);
                }, function () {
                    seccion.empty().append("<img src='" + RAIZ + "Content/themes/iusback_theme/img/general/ajax-loader.gif" + "'>");
                })
            }
            
        // compartir publico
            function btnCompartir(frm,seccion) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_compartirArchivoPublico", frm, function (data) {
                    
                    var div = "";
                    if (data.estado) {
                        div = getDivArchivosPublicos(data.archivoPublico);
                        //clearTr(seccion);
                        seccion.find(".nombreFileCompartir").empty();
                        seccion.find(".txtNombreFileCompartir").val("");
                    } else {
                        if (data.error._mostrar) {
                            
                            printMessage($(".divMensajeRepoPublico"), data.error.Message, false);
                            //$(".divMensajeRepoPublico").empty().append("<span class='failMessage'>" + data.error.Message + "</span>");
                        } else {
                            console.log(data);

                        }
                    }
                    $(".divCarpetasPublicasCompartir").append(div);
                });
            }
            function icoPublicoBack(frm) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_atrasCarpetaPublica", frm, function (data) {
                    /*
                    if (data.estado) {
                        div = "";
                        if (data.carpetas !== null) {
                            $.each(data.carpetas, function (i, carpeta) {
                                div += getDivCarpetasPublicas(carpeta);
                            });
                            $(".txtHdCarpetaPadrePublica").val(data.idCarpetaPadre);
                        }
                        $(".divCarpetasPublicasCompartir").empty().append(div);
                        $(".txtRutaPublica").val(data.carpetaPadre._strRuta);
                    }*/
                    console.log("la data del atras", data);
                    printSeccionPublica(data);
                })
            }
            function divCarpetaPublica(frm) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_entrarCarpetaPublica", frm, function (data) {
                    console.log("La carpeta publica es: ",data);
                    printSeccionPublica(data);
                })
            }
        // directorio
            function spIrBuscar(frm) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
                    //console.log(data);
                    if (data.estado) {
                        window.location = RAIZ + "Repositorio/index/" + data.carpeta._idCarpeta;
                    } else {
                        if (data.error._mostrar) {
                            //alert(data.error.Message);
                            printMessage($(".divMensajesGenerales"), "No se a encontrado ese directorio, por favor digite bien la ruta",false)
                        }
                    }
                })
            }
        // eliminar archivos
            function icoEliminarArchivo(frm,seccion,vista) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFile", frm, function (data) {
                    console.log("Respuesta elimianr", data);
                    if (data.estado) {
                        
                        seccion.remove();
                    }
                });
            }
        // cambiar nombre archivo
            
            function btnEditarArchivo(frm,folder) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_changeFileName", frm, function (data) {
                    if (data.estado) {
                        seccion.find(".ttlNombreCarpeta").empty().append(data.archivo._nombre);
                        btnCancelarEdicionCarpeta(folder.find(".detalleCarpeta"));
                    }
                });
            }
        // eliminar carpeta
        function icoEliminarCarpeta(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFolder", frm, function (data) {
                
                if (data.estado) {
                    seccion.remove();
                }
            });
        }
        // entrar a carpeta
        
        // subir archivo 
        function frmSubir(data, url, totalFiles) {
            var estadoIndividual = false;
            accionAjaxWithImage(url, data, function (data) {
                
                if (data.estado && !estadoIndividual) {
                    estadoIndividual = true;
                    $(".txtHdEstadoUpload").val("1");
                }
                var archivo = data.archivo;
                tr = getTrArchivo(archivo, data.estado);
                $(".tbArchivos").append(tr);
                porcentaje = $(".tbArchivos").find("tr").length / totalFiles * 100;
                $(".porcentajeCarga").empty().append(porcentaje + "%");
                if (porcentaje >= 100) {
                    $(".imgCargando").find("img").addClass("hidden");
                    $(".porcentajeCarga").empty().append("100%");
                }
            });
        }
        // actualizar carpetas
            function txtNombreArchivoDetalle(frm,seccion) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_changeFileName", frm, function (data) {
                    
                    if (data.estado) {
                        
                        seccion.find(".spanNombreCarpeta").empty().append(data.archivo._nombre);
                        controlesEdit(false, seccion);
                    }
                    else {
                        alert("Ocurrio un error cambiando de nombre");
                    }
                })
            }
            function txtNombreCarpetaDetalle(frm, seccion) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_updateCarpeta", frm, function (data) {
                    if (data.estado) {
                        seccion.find(".spanNombreCarpeta").empty().append(data.carpeta._nombre);
                        controlesEdit(false, seccion);
                    }
                    else {
                        alert("Ocurrio un error cambiando de nombre");
                    }
                })
            }
            function btnEditarCarpeta(frm,folder) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_updateCarpeta", frm, function (data) {
                
                    if (data.estado) {
                        folder.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);
                        btnCancelarEdicionCarpeta(folder.find(".detalleCarpeta"));
                    } else {
                        alert("Ocurrio un error queriendo renombrar la carpeta");
                    }
                })
            }
        // guardar carpeta
        function btnGuardarCarpeta(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
                if (data.estado) {
                    seccion.find(".cuadritoCarpeta").attr("id", 1);
                    seccion.find(".imgCuadritoIcono").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");

                    seccion.find(".txtHdIdCarpeta").val(data.carpeta._idCarpeta);
                    seccion.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);

                    seccion.find(".saveMode").remove();
                    seccion.find(".normalMode").removeClass("hidden");
                    
                    seccion.find(".divHerramientasIndividual").fadeIn("slow");

                    x = seccion.find(".cuadritoIconoAdd");
                    x.removeClass("cuadritoIconoAdd");
                    x.addClass("cuadritoIcono");
                } else {
                    //alert("Ocurrio un error intentando agregar carpeta");
                    var mjs = "";
                    if (data.error._mostrar) {
                        mjs = data.error.Message;
                    } else {
                        mjs = "Error no controlado";
                    }
                    printMessage($(".mensajeNewFolder"), mjs, false);
                }
            });
        }