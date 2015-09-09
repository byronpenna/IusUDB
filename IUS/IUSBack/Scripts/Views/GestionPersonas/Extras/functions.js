// genericas 
    
// scripts
    // agregar email
        function btnGuardarEmail(frm) {
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_guardarCorreoPersona", frm, function (data) {
                console.log(data);
                if (data.estado) {

                }
            })
        }
    // telefono 
        // eliminar tel
            function btnEliminarTel(frm,tr) {
                actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_eliminarTel", frm, function (data) {
                    console.log(data);
                    if (data.estado) {
                        tr.remove();
                    }
                })
            }
        // agregar tel
            function valAgregarTel(frm) {
                var val;
            }
            function btnAgregarTel(frm) {
                actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_guardarTelefonoPersona", frm, function (data) {
                    console.log(data);
                    if (data.estado) {

                    }
                })
            }
    // informacion basica
    function btnGuardarInformacionBasica(frm) {
        actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_guardarInformacionPersona", frm, function (data) {
            console.log(data);
            if (data.estado) {
                printMessage($(".divResultadoOperacion"), "Informacion actualizada exitosamente", true);
            } else {
                if (data.error._mostrar) {
                    printMessage($(".divResultadoOperacion"), data.error.Message, false);
                } else {
                    printMessage($(".divResultadoOperacion"), "Ocurrio un error no controlado", false);
                }
            }
        })
    }