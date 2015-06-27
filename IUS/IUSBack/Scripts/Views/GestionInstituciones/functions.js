function btnAddInstitucion(frm) {
    actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_insertInstitucion", frm, function (data) {
        console.log(data);
        if (data.estado) {

        }
    })
}