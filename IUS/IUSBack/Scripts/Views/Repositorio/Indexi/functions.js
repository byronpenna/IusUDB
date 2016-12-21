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