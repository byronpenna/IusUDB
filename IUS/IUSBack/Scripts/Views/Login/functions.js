function aOlvidoContra() {
    actualizarCatalogo(RAIZ + "/Home/sp_secpu_reenviarCorreo", frm, function (data) {

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