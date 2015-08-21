// generics 
    // vistas 
        function verCuadricula() {
            
            var seccionModificar = $(".cuadriculaView");
            cambiarVistas("cuadricula");
            var frm = {
                idCarpeta: $(".txtHdIdCarpetaPadre").val()
            }
            vista(frm,seccionModificar,"cuadricula")
        }    
        function verLista() {
            //var seccionModificar = $(".listView");
            var seccionModificar = $(".targetListView");
            cambiarVistas("lista");
            var frm = {
                idCarpeta: $(".txtHdIdCarpetaPadre").val()
            }
            vista(frm,seccionModificar,"lista");
        }
        function vista(frm, seccion, op,callback) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_entrarCarpeta", frm, function (data) {
                var div = "";
                if (data.carpetas !== null) {
                    $.each(data.carpetas, function (i, folder) {
                        if (op == "lista") {
                            div += loadListFolders(folder);
                        } else if (op == "cuadricula") {
                            div += loadCuadriculaCarpeta(folder);
                        }
                    })
                }
                if (data.archivos !== null) {
                    $.each(data.archivos, function (i, file) {
                        if (op == "lista") {
                            div += loadListFiles(file)
                        }
                        else if (op == "cuadricula") {
                            div += loadCuadriculaFiles(file)
                        }
                    })
                }
                seccion.empty().append(div);
                if (callback !== undefined) {
                    callback();
                }
            }, function () {
                seccion.empty().append("<img src='" + RAIZ + "Content/themes/iusback_theme/img/general/ajax-loader.gif" + "'>");
            })
        }
        // cuadricula 
            function loadCuadriculaCarpeta(carpeta) {
                var div = "\
                        <div class='col-lg-2 folder'>\
                            <input type='hidden' class='txtHdIdCarpeta' value='"+ carpeta._idCarpeta + "'/>\
                            <div class='row divHerramientasIndividual'>\
                                <a href='#' class='ico icoDownload' title='Descargar'>\
                                    <i class='fa fa-download'></i>\
                                </a>\
                                <a href='#' class='ico icoEliminarCarpeta' title='Eliminar'>\
                                    <i class='fa fa-trash-o'></i>\
                                </a>\
                            </div>\
                            <div class='cuadritoIcono cuadritoCarpeta'>\
                                <img src='"+ RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/" + carpeta.getIcono + "' />\
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
            function loadCuadriculaFiles(archivo, openLocation) {
                var div = "\
                        <div class='col-lg-2 folder'>\
                            <input type='hidden' class='txtHdIdArchivo' value='"+ archivo._idArchivo + "'/>\
                            <input type='hidden' class='txtHdIdCarpetaContenedora' value='" + archivo._carpeta._idCarpeta + "'/>\
                            <div class='row divHerramientasIndividual'>\
                                <a href='#' class='icoCompartirFile' title='Compartir'>\
                                    <i class='fa fa-share'></i>\
                                </a>\
                                <a href='" + RAIZ + "Repositorio/DescargarFichero/" + archivo._idArchivo + "' class='ico' title='Descargar'>\
                                    <i class='fa fa-download'></i>\
                                </a>\
                            ";
                if (openLocation !== undefined && openLocation == true) {
                    div += "<a href='#' class='ico icoOpenLocation' title='Abrir ubicacion'>\
                                    <i class='fa fa-folder-open'></i>\
                                </a>";
                }
                div += "\
                            </div>\
                            <div class='cuadritoIcono '>\
                                <img src='"+ RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/" + archivo._extension._tipoArchivo._icono + "' />\
                                <div class='detalleCarpeta'>\
                                    <div class='normalMode'>\
                                        <h3 class='ttlNombreCarpeta'>"+ archivo._nombre + "</h3>\
                                    </div>\
                                    <div class='row marginNull hidden editMode'>\
                                        <div class='row marginNull inputNombreCarpeta'>\
                                            <input type='text' class='form-control txtNombreCarpeta'>\
                                        </div>\
                                        <div class='row marginNull'>\
                                            <button class='btn btn-xs btnEditarArchivo'>Actualizar</button>\
                                            <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                    ";
                return div;
            }
        // lista
            function loadListFiles(file) {
                var div = "\
                        <div class='row folderDetalles folderUni'>\
                            <input type='hidden' class='txtHdIdArchivo' value='"+ file._idArchivo + "'>\
                            <div class='col-lg-6'>\
                                <div class='normalMode inline'>\
                                    <span class='spanNombreCarpeta sinRedirect'>" + file._nombre + "</span>\
                                </div>\
                                <div class='editMode inline hidden'><input class='txtNombreCarpetaDetalle txtNombreArchivoDetalle sinRedirect'></div>\
                            </div>\
                            <div class='col-lg-2'>" + file._extension._tipoArchivo._tipoArchivo + "</div>\
                            <div class='col-lg-2'>" + file.getFechaCreacion + "</div>\
                            <div class='col-lg-2 divAccionesLista'>\
                                <a href='#' class='icoCompartirFile' title='Compartir'>\
                                    <i class='fa fa-share'></i>\
                                </a>\
                                <a href='" + RAIZ + "Repositorio/DescargarFichero/" + file._idArchivo + "' class='ico' title='Descargar'>\
                                    <i class='fa fa-download'></i>\
                                </a>\
                            </div>\
                        </div>\
                    ";
                return div;
            }
            function loadListFolders(folder) {
                var div = "\
                        <div class='row folderDetalles carpetaDetalle '>\
                            <input type='hidden' class='txtHdIdCarpeta' value='"+ folder._idCarpeta + "'>\
                            <div class='col-lg-6'><i class='fa fa-folder'></i>\
                            <div class='normalMode inline'><span class='spanNombreCarpeta sinRedirect'>" + folder._nombre + "</span></div>\
                            <div class='editMode inline hidden'><input class='txtNombreCarpetaDetalle sinRedirect'></div>\
                            </div>\
                            <div class='col-lg-2'>Folder</div>\
                            <div class='col-lg-2'>" + folder.getFechaCreacion + "</div>\
                        </div>\
                    ";
                return div;
            }
    // ***************    
            function btnBusqueda(frm, seccion, target) {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_searchArchivo", frm, function (data) {
                    
                    if (data.estado) {
                        var div = "";
                        if (data.archivos !== undefined && data.archivos !== null) {
                            $.each(data.archivos, function (i, archivo) {
                                if (target == "cuadricula") {
                                    div += loadCuadriculaFiles(archivo, true);
                                } else {
                                    div += loadListFiles(archivo);
                                }
                            })
                        } else {
                            div += "\
                    <div class='divNofoundResults'>\
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
    // compartido 
        function cambiarVistaUsuario(vista) {
            
            var pestaUser = $(".icoUser"), pestaUsers = $(".icoUsers");
            $(".herramientaUserSection .activeVista").removeClass("activeVista");
            switch (vista) {
                case "user": {
                    pestaUser.addClass("activeVista");
                    break;
                }
                case "users": {
                    pestaUsers.addClass("activeVista");
                    break;
                }
            }
        }
        function getUsuariosArchivosCompartidos(frm) {
            var vistaActual = getVistaActual();
            var seccion;
            
            switch (vistaActual) {
                case "cuadricula": {
                    seccion = $(".cuadriculaView");
                    break;
                }
                case "lista": {
                    seccion = $(".listView");
                    break;
                }
            }
            actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_getUsuariosArchivosCompartidos", frm, function (data) {
                console.log(data);
                var div = "";
                if (data.usuarios !== null) {
                    $.each(data.usuarios, function (i, usuario) {
                        if (vistaActual == "cuadricula") {
                            div += getDivUsuariosPrincipal(usuario);
                        } else if(vistaActual == "lista"){
                            div += getDivUsuariosPrincipalLista(usuario);
                        }
                    })
                }
                seccion.empty().append(div);
            })
        }
        // lista 
            function getDivUsuariosPrincipal(usuario) {
                var div = "\
                    <div class='col-lg-2 folder divUsuario pointer'>\
                        <input type='hidden' class='txtHdIdUsuario' value='" + usuario._idUsuario + "'/>\
                        <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/profle.png' />\
                        <h4 class='tituloCarpetaPublica'>" + usuario._usuario + "</h4>\
                    </div>\
                ";
                return div;
            }
            function getDivUsuariosPrincipalLista(usuario) {
                var div = "\
                    <div class='row folderDetalles'>\
                        <input type='hidden' class='txtHdIdArchivo' value='" + usuario._idUsuario + "'>\
                        <div class='col-lg-6'>\
                            <div class='normalMode inline'>\
                                <span class='spanNombreCarpeta sinRedirect'>" + usuario._usuario + "</span>\
                            </div>\
                            <div class='editMode inline hidden'><input class='txtNombreCarpetaDetalle txtNombreArchivoDetalle sinRedirect'></div>\
                        </div>\
                        <div class='col-lg-3'>Tipo</div>\
                        <div class='col-lg-3'>Persona</div>\
                    </div>\
                ";
                return div;
            }
        function getDivArchivosCompartidos(archivoCompartido) {
            var div = "\
                <div class='divCarpetaPublica col-lg-6'>\
                    <div class='row marginNull'>\
                        <a href='#' class='icoDejarDeCompartir' title='Compartir'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                        <a href='" + RAIZ + "/Repositorio/DescargarFichero/" + archivoCompartido._archivo._idArchivo + "'>\
                            <i class='fa fa-download'></i>\
                        </a>\
                    </div>\
                    <input type='hidden' class='txtHdIdArchivoCompartido' value='" + archivoCompartido._idArchivoCompartido + "'/>\
                    <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/" + archivoCompartido._archivo._extension._tipoArchivo._icono + "' />\
                    <h4 class='tituloCarpetaPublica'>" + archivoCompartido._archivo._nombre + "</h4>\
                </div>\
            ";
            return div;
        }
        function getDivUsuarios(usuario) {
            var div = "\
                <div class='divCarpetaPublica divCarpetaUsuarioCompartido col-lg-6'>\
                    <input type='hidden' class='txtHdIdUsuario' value='"+usuario._idUsuario+"'/>\
                    <img src='"+RAIZ+"/Content/themes/iusback_theme/img/general/profle.png' />\
                    <div class='row'>\
                        <a href='#' class='btnDejarDeCompartirTodo' title='No compartir nada'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                    </div>\
                    <h4 class='tituloCarpetaPublica'>"+usuario._usuario+"</h4>\
                </div>\
            ";
            return div;
        }
// scripts 
    function txtBusquedaUsuario(txt) {
        if (txt == "") {
            $(".seccionCompartida .divCarpetaUsuarioCompartido").removeClass("hidden");
        } else {
            $(".seccionCompartida .divCarpetaUsuarioCompartido").addClass("hidden");
            var usuarios = $(".seccionCompartida .divCarpetaUsuarioCompartido .tituloCarpetaPublica:containsi(" + txt + ")");
            usuarios = usuarios.parents(".divCarpetaUsuarioCompartido");
            usuarios.removeClass("hidden");
        }

    }
    function icoDejarDeCompartir(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_removeShareFile", frm, function (data) {
            
            if (data.estado) {
                seccion.remove();
            }
        });
    }
    function btnDejarDeCompartirTodo(frm) {
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_dejarDeCompartirTodo", frm, function (data) {
            console.log("data regresada por servidor es", data);
            if (data.estado) {
                printMessage($(".divMessageCompartir"), "Se le dejo de compartir archivos exitosamente", true);
                $(".icoCompartidoBack").click();
            } else {
                
            }
            
        })
    }
    function divCarpetaUsuarioCompartido(frm, seccion) {
        var nombreUsuarioCarpeta = frm.nombreCarpeta;
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_getFilesFromShareUserId", frm, function (data) {
            console.log("data con archivos compartidos", data);
            if (data.estado) {
                $(".divUsuarioCarpeta").find(".hUsuarioCarpeta").empty().append(nombreUsuarioCarpeta);
                $(".divUsuarioCarpeta").removeClass("hidden");
                var div = "";
                if (data.archivos !== null) {
                    $.each(data.archivosCompartidos, function (i, archivoCompartido) {
                        div += getDivArchivosCompartidos(archivoCompartido);
                    });
                }
                seccion.empty().append(div);
                $(".nombreFileCompartir").empty();
                $(".txtHdIdArchivoCompartir").val(-1);
            } else {
                alert("Ocurrio un error cargando los archivos");
            }
        })
    }
    function icoCompartirFile(folder) {
        var nombreArchivo = folder.find(".ttlNombreCarpeta").text();
        var idArchivo = folder.find(".txtHdIdArchivo").val();
        var icoDelete = "<a href='#' title='No compartir archivo' class='icoCancelShare'><i class='fa fa-times '></i></a>";
        $(".nombreFileCompartir").empty().append(nombreArchivo + " " + icoDelete);
        $(".txtHdIdArchivoCompartir").val(idArchivo);
    }
    function btnShareArchivo(frm) {
        var form = frm;
        
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_compartirArchivo", frm, function (data) {
            
            
            if (data.estado) {
                var frm = {
                    idUserFile: form.idUsuario,
                    nombreCarpeta: form.nombreCarpeta
                }
                var seccion = $(".seccionCompartida");
                divCarpetaUsuarioCompartido(frm, seccion);
            } else {
                printMessage($(".divMessageCompartir"), data.error.Message, false);
            }
        })

    }
    function spIrBuscar(frm) {
        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
            
            if (data.estado) {
                window.location = RAIZ + "RepositorioCompartido/index/" + data.carpeta._idCarpeta;
            } else {
                if (data.error._mostrar) {
                    //alert(data.error.Message);
                    printMessage($(".divMensajesGenerales"), "No se a encontrado ese directorio, por favor digite bien la ruta", false)
                }
            }
        })
    }