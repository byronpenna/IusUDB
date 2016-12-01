// ###########################
    function openConMotivos() {
        $(".divRechazar").css("display", "block");
        $(".divAprobar").css("display", "none");

        $(".divUpload").fadeIn(400, function () {

        });
        $(".contenedorUpload").click();
    }
// acciones 
    function frmAprobar(frm) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_cambiarEstado", frm, function (data) {
            console.log("La respuesta del servidor", data);
            if (data.estado) {
                alert("Publicada correctamente");
                location.reload();
            } else {
                alert("No pudo publicarse");
            }
        })
    }
    function btnEnviarSolicitud(frm) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/ajax_rechazar", frm, function (data) {
            console.log("La respuesta del servidor", data);
            if (data.estado) {
                alert("Accion ejecutada correctamente");

            }
        })
    }