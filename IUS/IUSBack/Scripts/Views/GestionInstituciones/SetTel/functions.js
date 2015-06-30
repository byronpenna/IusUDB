// acciones script 
    function btnAgregarTel(frm) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_insertTelInstitucion", frm, function (data) {
            console.log(data);

        });
    }