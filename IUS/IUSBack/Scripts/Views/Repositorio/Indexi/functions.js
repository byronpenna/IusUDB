﻿function abrirModal(e,callback) {
    e.preventDefault();
    $(".modalContenido").hide();
    $(".divUpload").fadeIn(400, callback);

}
function btnEliminarCarpeta(frm) {
    console.log("Vamo a eliminar");
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFolder", frm, function (data) {
        if (data.estado) {

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