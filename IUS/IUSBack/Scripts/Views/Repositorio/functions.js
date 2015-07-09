// generics 
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
        div = "\
        <div class='divCarpetaPublica col-lg-6'>\
            <input type='hidden' class='txtHdIdCarpetaPublica' value='" + carpeta._idCarpetaPublica + "'>\
            <img src='" + RAIZ + "Content/themes/iusback_theme/img/general/repositorio/" + carpeta.getIcono+ "' />\
            <h4>"+carpeta._nombre+"</h4>\
        </div>\
        ";
        return div;
    }
    function getDivArchivosPublicos(archivo) {
        var div = "";
        div = "\
        <div class='divArchivoPublico col-lg-6'>\
            <img src='" + RAIZ + "Content/themes/iusback_theme/img/general/repositorio/" + archivo._archivoUsuario._extension._tipoArchivo._icono + "' />\
            <h4>" + archivo._nombre + "</h4>\
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
                                    <button class='btn btn-xs btnEditarCarpeta'>Actualizar</button>\
                                    <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div>";
        return div;
    }
    function initShareFile(folder) {
        nombreArchivo   = folder.find(".ttlNombreCarpeta").text();
        idArchivo       = folder.find(".txtHdIdArchivo").val();
        console.log(idArchivo);
        $(".nombreFileCompartir").empty().append(nombreArchivo);
        $(".txtNombreFileCompartir").val(nombreArchivo);
        $(".txtHdIdArchivoCompartir").val(idArchivo);

    }
// scripts 
        // compartir publico
            function btnCompartir(frm,seccion) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_compartirArchivoPublico", frm, function (data) {
                    console.log(data);
                    var div = "";
                    if (data.estado) {
                        div = getDivArchivosPublicos(data.archivoPublico);
                        //clearTr(seccion);
                        seccion.find(".nombreFileCompartir").empty();
                        seccion.find(".txtNombreFileCompartir").val("");
                    }
                    $(".divCarpetasPublicasCompartir").append(div);
                });
            }
            function icoPublicoBack(frm) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_atrasCarpetaPublica", frm, function (data) {
                    
                    if (data.estado) {
                        div = "";
                        if (data.carpetas !== null) {
                            $.each(data.carpetas, function (i, carpeta) {
                                div += getDivCarpetasPublicas(carpeta);
                            });
                            $(".txtHdCarpetaPadrePublica").val(data.idCarpetaPadre);
                        }
                        $(".divCarpetasPublicasCompartir").empty().append(div);

                    }
                })
            }
            function divCarpetaPublica(frm) {
                actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_entrarCarpetaPublica", frm, function (data) {
                    console.log(data);
                    if (data.estado) {
                        var div         = "";
                        var divArchivo = "";
                        $(".txtHdCarpetaPadrePublica").val(data.idCarpetaPadre);
                        if (data.carpetas !== null) {
                            $.each(data.carpetas, function (i, carpeta) {
                                div += getDivCarpetasPublicas(carpeta);
                            });
                            
                        }
                        if (data.archivos !== null) {
                            
                            $.each(data.archivos, function (i, archivo) {
                                divArchivo += getDivArchivosPublicos(archivo);
                            })
                        }
                        $(".divCarpetasPublicasCompartir").empty().append(div);
                        $(".divCarpetasPublicasCompartir").append(divArchivo);

                    }
                })
            }
        // directorio
        function spIrBuscar(frm) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    window.location = RAIZ + "Repositorio/index/" + data.carpeta._idCarpeta;
                }
            })
        }
        // eliminar archivos
        function icoEliminarArchivo(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFile", frm, function (data) {
                
                if (data.estado) {
                    seccion.remove();
                }
            });
        }
        // cambiar nombre archivo
        function btnEditarArchivo(frm) {
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
        function cuadritoCarpeta(frm) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_entrarCarpeta", frm, function (data) {
                
                if (data.estado) {
                    var divFolders = "";
                    $.each(data.carpetas, function (i,carpeta) {
                        
                        divFolders += getStandarFolder(carpeta);
                    });
                    window.history.pushState(null, "Titulo", "Repositorio/Index/" + frm.idCarpeta);
                    $(".folders").empty().append(divFolders);
                }
            });
        }
        // subir archivo 
        function frmSubir(data, url, totalFiles) {
            var estadoIndividual = false;
            accionAjaxWithImage(url, data, function (data) {
                
                if (data.estado && !estadoIndividual) {
                    estadoIndividual = true;
                    $(".txtHdEstadoUpload").val("1");
                }
                archivo = data.archivo;
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
        /*
        function ttlNombreCarpeta(seccion,nombre) {
            seccion.find(".normalMode").addClass("hidden");
            seccion.find(".editMode").removeClass("hidden");
            folder = seccion.parents(".cuadritoIcono");
            folder.removeClass("cuadritoIcono");
            folder.addClass("cuadritoIconoAdd");
            seccion.find(".txtNombreCarpeta").val(nombre);
        }*/
        /*
        function btnCancelarEdicionCarpeta(seccion) {
            seccion.find(".editMode").addClass("hidden");
            seccion.find(".normalMode").removeClass("hidden");
            folder = seccion.parents(".cuadritoIcono");
            folder.addClass("cuadritoIcono");
            folder.removeClass("cuadritoIconoAdd");
        }*/
        // guardar carpeta
        function btnGuardarCarpeta(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
                
                if (data.estado) {
                    seccion.find(".cuadritoCarpeta").attr("id", 1);
                    seccion.find(".imgFolder").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");

                    seccion.find(".txtHdIdCarpeta").val(data.carpeta._idCarpeta);
                    seccion.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);

                    seccion.find(".saveMode").remove();
                    seccion.find(".normalMode").removeClass("hidden");
                    
                    x = seccion.find(".cuadritoIconoAdd");
                    x.removeClass("cuadritoIconoAdd");
                    x.addClass("cuadritoIcono");
                } else {
                    alert("Ocurrio un error intentando agregar carpeta");
                }
            });
        }