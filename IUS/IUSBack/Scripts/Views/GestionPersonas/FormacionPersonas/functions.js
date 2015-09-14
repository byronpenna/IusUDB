// acciones script
    function btnAgregarInstitucion(frm) {
        actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
            console.log("Data servidor", data);
        })
    }