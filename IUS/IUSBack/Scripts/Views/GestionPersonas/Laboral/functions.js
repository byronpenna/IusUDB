function btnAgregarLaboralPersona(frm) {
    actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertLaboralPersonas", frm, function (data) {
        console.log(data);
    })
}