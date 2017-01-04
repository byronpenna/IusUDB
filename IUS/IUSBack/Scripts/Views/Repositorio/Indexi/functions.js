/*function abrirModal(e,callback) {
    e.preventDefault();
    $(".modalContenido").hide();
    $(".divUpload").fadeIn(400, callback);

}*/
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