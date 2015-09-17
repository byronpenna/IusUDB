// acciones script
    // instituciones
        function btnAgregarInstitucion(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
            })
        }
        function btnEliminarInstitucion(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
    // carreras
        function btnEliminarCarrera(frm,tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarCarrera", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
        function btnAgregarCarreraIndividual(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarCarrera", frm, function (data) {
                console.log("Data servidor", data);
            })
        }
        
    // formacion personas
        function btnAgregarCarrera(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarFormacionPersona", frm, function (data) {
                console.log("la data es", data);
            })
        }
        function btnEliminarTitulo(frm,tr){
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarTituloPersona", frm, function (data) {
                console.log("la data es:", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }