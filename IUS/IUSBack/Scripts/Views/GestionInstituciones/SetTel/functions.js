// acciones script 
    function btnEliminarTel(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_deleteTelInstitucion", frm, function (data) {
            if (data.estado) {
                tr.remove();
            }
        })
    }
    function btnAgregarTel(frm) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_insertTelInstitucion", frm, function (data) {
            console.log(data);
            /*Llenar tablita*/
        });
    }