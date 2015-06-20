// generics 
    function getDivNewFolder() {
        div = "<div class='col-lg-2 folder'>\
                    <div class='cuadritoIconoAdd'>\
                        <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder-opacity.png' class='imgFolder' />\
                        <div class='detalleCarpeta'>\
                            <div class='row marginNull inputNombreCarpeta'>\
                                <input type='text' class='form-control txtNombreCarpeta'>\
                            </div>\
                            <div class='row marginNull'>\
                                <button class='btn btn-xs btnGuardarCarpeta'>Guardar</button>\
                                <button class='btn btn-xs btnCancelarGuardarCarpeta'>Cancelar</button>\
                            </div>\
                        </div>\
                    </div>\
                </div>";
        return div;
    }
    function getTrArchivo(archivo,estado) {
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
// scripts 
    // eliminar carpeta
        function icoEliminarCarpeta(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFolder", frm, function (data) {
                console.log("respuesta servidor", data);
                if (data.estado) {
                    seccion.remove();
                }
            });
        }
    // entrar a carpeta
        function cuadritoCarpeta(frm) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_entrarCarpeta", frm, function (data) {
                console.log("respuesta servidor", data)
                if (data.estado) {
                    var divFolders = "";
                    $.each(data.carpetas, function (i,carpeta) {
                        console.log(carpeta);
                        divFolders += getStandarFolder(carpeta);
                    });
                    window.history.pushState(null, "Titulo", "Repositorio/Index/" + frm.idCarpeta);
                    $(".folders").empty().append(divFolders);
                }
            });
        }
    // subir archivo 
        function frmSubir(data, url,totalFiles) {
            accionAjaxWithImage(url, data, function (data) {
                console.log("respuesta", data);            
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
                console.log("Respuesta server", data);
                if (data.estado) {
                    folder.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);
                    btnCancelarEdicionCarpeta(folder.find(".detalleCarpeta"));
                } else {
                    alert("Ocurrio un error queriendo renombrar la carpeta");
                }
            })
        }
        function ttlNombreCarpeta(seccion,nombre) {
            seccion.find(".normalMode").addClass("hidden");
            seccion.find(".editMode").removeClass("hidden");
            folder = seccion.parents(".cuadritoIcono");
            folder.removeClass("cuadritoIcono");
            folder.addClass("cuadritoIconoAdd");
            seccion.find(".txtNombreCarpeta").val(nombre);
        }
        function btnCancelarEdicionCarpeta(seccion) {
            seccion.find(".editMode").addClass("hidden");
            seccion.find(".normalMode").removeClass("hidden");
            folder = seccion.parents(".cuadritoIcono");
            folder.addClass("cuadritoIcono");
            folder.removeClass("cuadritoIconoAdd");
        }
    // guardar carpeta
        function btnGuardarCarpeta(frm,seccion) {
            actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
                console.log("Respuesta server", data);
                if (data.estado) {
                    seccion.find(".imgFolder").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");
                    adentro = "<h3>"+data.carpeta._nombre+"</h3>";
                    seccion.find(".detalleCarpeta").empty().append(adentro);
                    x = seccion.find(".cuadritoIconoAdd");
                    x.removeClass("cuadritoIconoAdd");
                    x.addClass("cuadritoIcono");
                } else {
                    alert("Ocurrio un error intentando agregar carpeta");
                }
            });
        }