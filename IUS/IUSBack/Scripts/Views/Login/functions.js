function aOlvidoContra(frm) {
    console.log("Entro aqui");
    console.log("url", RAIZ + "/Login/sp_usu_solicitarCambioPass");
    actualizarCatalogo(RAIZ + "/Login/sp_usu_solicitarCambioPass", frm, function (data) {
        console.log("La data devuelta es:", data);
        if (data.estado) {

        }
    })
}
//#################
function cambiarActivePestania(pestania) {
    $(".btnTabsLogin").removeClass("activeMenu");
    switch (pestania) {
        case 'admin': {
            $("#btnAdminLogin").addClass("activeMenu");
            break;
        }
        case 'usuario': {
            $("#btnUsuarioLogin").addClass("activeMenu");
            break;
        }
    }
}