function btnAddValor() {

}
function frmInstitucional(frm) {
    actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_actualizarInfoConfig", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
        if (data.estado) {
            alert("Actualizado correctamente");
            configuracion = data.configuracion;
            $("#txtAreaVision").val(configuracion._vision);
            $("#txtAreaMision").val(configuracion._mision);
            $("#txtAreaHistoria").val(configuracion._historia);
        } else {
            console.log("error", data.error);
            alert("Ocurrio un error");
        }
    })
}