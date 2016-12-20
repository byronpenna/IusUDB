function abrirModal(e,callback) {
    e.preventDefault();
    $(".modalContenido").hide();
    $(".divUpload").fadeIn(400, callback);

}
function btnEliminarCarpeta(frm) {
    console.log("Vamo a eliminar");
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_deleteFolder", frm, function (data) {

    })
}
function btnNuevaCarpeta(frm) {
    actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
    })
}