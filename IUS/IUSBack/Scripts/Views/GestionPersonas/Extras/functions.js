// genericas 
    
// scripts
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