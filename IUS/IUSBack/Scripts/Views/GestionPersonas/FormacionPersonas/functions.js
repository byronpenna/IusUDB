// acciones script
    function btnAgregarInstitucion(frm) {
        actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
            console.log("Data servidor", data);
        })
    }
    function btnAgregarCarrera(frm) {
        actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarFormacionPersona", frm, function (data) {
            console.log("la data es", data);

        })
    }
    function btnAgregarCarreraIndividual(frm) {
        actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarCarrera", frm, function (data) {
            console.log("Data servidor", data);
        })
    }