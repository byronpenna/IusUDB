function btnAddInstitucion(frm) {
    actualizarCatalogo(RAIZ + "/GestionIdiomaWebsite/sp_trl_eliminarLlaveIdioma", frm, function (data) {
        console.log(data);
        if (data.estado) {

        }
    })
}