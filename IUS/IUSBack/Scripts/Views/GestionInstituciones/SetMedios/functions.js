// acciones script
    function btnAgregar(frm) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_insertEnlaceInstituciones", frm, function (data) {
            console.log(data);
        });
    }