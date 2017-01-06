function getTrCarpeta(carpeta) {
    //@Url.Action("Index", "Repositorio", new { id=carpeta._idCarpeta })
    var tr = "\
    <tr class=trRepo>\
        <td class=hidden>\
            <input name=txtHdIdCarpeta class=txtHdIdCarpeta value="+carpeta._idCarpeta+"/>\
        </td>\
        <td>\
            <a href='" + RAIZ + "/Repositorio/Index/"+ carpeta._idCarpeta +"'>\
                <span class='spNombre'>"+carpeta._nombre+"</span>\
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
//@Url.Action('DescargarFichero', 'Repositorio', new { id=archivo._idArchivo })
function getTrArchivo(archivo) {
    var tr = "\
    <tr class='trRepo'>\
        <td class='hidden'>\
            <input class='txtHdIdArchivo' name='txtHdIdArchivo' value='"+archivo._idArchivo+"' />\
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
                <a href='" + RAIZ + "/Repositorio/DescargarFichero/" + archivo._idArchivo + "'>\
                    <i class='fa fa-download btn' aria-hidden='true'></i>\
                </a>\
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
function btnBusqueda(frm, seccion) {
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_searchArchivo", frm, function (data) {
        console.log("respuesta es: ",data)
        if (data.estado) {
            if (data.archivos !== undefined && data.archivos !== null) {
                var trs = "";
                $.each(data.archivos, function (i, archivo) {
                    trs += getTrArchivo(archivo);
                })
                $(".tbTablaRepo").empty().append(trs);
                console.log("justo a limpiar :) ");
            }
        }
    })
}
// eliminar 
    function btnEliminarArchivo(frm,tr) {
        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFile", frm, function (data) {
            console.log("Respuesta elimianr", data);
            if (data.estado) {
                tr.remove();
            }
        });
    }
    function btnEliminarCarpeta(frm,tr) {
        console.log("Vamo a eliminar");
        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFolder", frm, function (data) {
            if (data.estado) {
                tr.remove();
            }
        })
    }
    
function btnEditarNombre(frm,seccion) {
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_changeFileName", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
        if (data.estado) {
            seccion.find(".spNombre").empty().append(data.archivo._nombre);
            controlesEdit(false, seccion);
        }
    })
}
function frmSubir(data, url, totalFiles) {
    var estadoIndividual = false;
    accionAjaxWithImage(url, data, function (data) {
        console.log("La data devuelta es: ", data);
        /*
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
        }*/

    });
}
function spIrBuscar() {
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
        console.log(data);
        if (data.estado) {
            window.location.href = RAIZ + "/Repositorio/Index/" + data.carpeta._idCarpeta;
        }
    });
}
function btnNuevaCarpeta(frm) {
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
    })
}