function getTrCarpeta(carpeta){
    //Url.Action('Index', 'RepositorioPublico', new { id=carpeta._idCarpetaPublica })
    var tr = "\
        <tr>\
            <td class='hidden'>\
                <input name='txtHdIdCarpeta' class='txtHdIdCarpeta' value='"+carpeta._idCarpetaPublica+"' />\
            </td>\
            <td>\
                <a href='" + RAIZ + "/RepositorioPublico/Index/" + carpeta._idCarpetaPublica + "'>\
                    "+carpeta._nombre+"\
                </a>\
            </td>\
            <td>"+carpeta._fechaCreacion+"</td>\
            <td>0.0 MB</td>\
            <td>Carpeta</td>\
            <td>\
                <i class='fa fa-trash btnEliminarCarpeta pointer' aria-hidden='true'></i>\
            </td>\
        </tr>\
    ";
    return tr;
}
function getTrArchivo(archivo) {
    //@Url.Action('DescargarFichero', 'Repositorio', new { id=archivo._archivoUsuario._idArchivo })
    var tr = "\
    <tr>\
        <td class='hidden'>\
            <input class='txtHdIdArchivoPublico' name='txtHdIdArchivoPublico' value='"+archivo._idArchivoPublico+"' />\
        </td>\
        <td>\
            <div class='normalMode'>\
                <span class='spNombre'>"+archivo._nombre+"</span>\
            </div>\
            <div class='editMode hidden'>\
                <input type='text' class='form-control txtArchivoNombre inputBack' />\
            </div>\
        </td>\
        <td>"+archivo._fechaCreacion+"</td>\
        <td>0.0 MB</td>\
        <td>Archivo</td>\
        <td>\
            <div class='normalMode'>\
                <i class='fa fa-trash btnEliminarArchivo pointer' aria-hidden='true'></i>\
                <a href='" + RAIZ + "/Repositorio/DescargarFichero/" + archivo._archivoUsuario._idArchivo + "'><i class='fa fa-download btn' aria-hidden='true'></i></a>\
                <i class='pointer fa fa-pencil-square-o btnCambiarNombre' aria-hidden='true' title='Cambiar nombre'></i>\
                <i class='pointer fa fa-share btnCambiarNombre icoCompartirFile' aria-hidden='true' title='Compartir'></i>\
            </div>\
            <div class='editMode hidden'>\
                <div class='btn-group'>\
                    <button class='btn btn-sm btn-default btnBack btnEditarNombre'>Aceptar</button>\
                    <button class='btn btn-sm btn-default btnBack btnCancelarEdicionCarpeta pointer'>Cancelar</button>\
                </div>\
            </div>\
        </td>\
    </tr>\
    ";
    return tr;
}
function btnBusqueda(frm) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_searchArchivoPublicoBack", frm, function (data) {
        console.log("la data devuelta es ", data);
        if (data.estado) {
            var trs = "";
            if (data.archivos !== undefined && data.archivos !== null) {
                $.each(data.archivos, function (i, archivo) {
                    trs += getTrArchivo(archivo);
                })
            }
            $(".tbTablaRepo").empty().append(trs);
        }
    })
}
function btnGuardarCarpeta(frm, seccion) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_insertCarpetaPublica", frm, function (data) {
        console.log("data de carpeta INGRESADA", data);
        if (data.estado) {

        }
    })
}
function spIrBuscar(frm) {
    //console.log(frm);
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getPublicoByRuta", frm, function (data) {
        console.log(data);
        if (data.estado) {
            if (data.carpetaPublica !== undefined && data.carpetaPublica !== null) {
                window.location = RAIZ + "RepositorioPublico/index/" + data.carpetaPublica._idCarpetaPublica;
            } else {
                //printMessage($(".divMensajesGenerales"), "No se a encontrado ese directorio, por favor digite bien la ruta", false);
            }
            
        }
    })
}
function btnEliminarArchivo(frm, tr) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_removeShareArchivoPublico", frm, function (data) {
        if (data.estado) {
            tr.remove();
        }
    })
}
function btnEliminarCarpeta(frm, tr) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_deleteCarpetaPublica", frm, function (data) {
        console.log(data);
        if (data.estado) {
            tr.remove();
        }
    })
}