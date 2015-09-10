// genericas 
    function getTrEmail(emailPersona){
        var tr = "\
        <tr>\
            <td class='hidden'>\
                <input type='hidden' value='"+emailPersona._idEmail+"' name='txtIdEmailPersona'/>\
            </td>\
            <td>"+emailPersona._email+"</td>\
            <td>"+emailPersona._descripcion+"</td>\
            <td>\
                <button class='btn btnEditarEmail btn-xs' >Editar</button>\
                <button class='btn btnEliminarEmail btn-xs'>Eliminar</button>\
            </td>\
        </tr>\
        ";
        return tr;
    }
    function getTrNumeros(telefono) {
        var tr = "\
        <tr>\
            <td class='hidden'>\
                <input name='txtHdIdTelefono' class='txtHdIdTelefono'  value='@telefono._idTelefonoPersona'/>\
            </td>\
            <td>"+telefono._telefono+"</td>\
            <td>"+telefono._pais._pais+"</td>\
            <td>"+telefono._descripcion+"</td>\
            <td>\
                <button class='btn btn-xs'>Editar</button>\
                <button class='btn btnEliminarTel btn-xs'>Eliminar</button>\
            </td>\
        </tr>\
        ";
        return tr;
    }
// scripts
    // agregar email
        function btnActualizarEmail(frm) {
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_actualizarCorreoPersona", frm, function (data) {
                console.log(data);
            })
        }
        function btnEliminarEmail(frm,tr) { 
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_eliminarCorreoPersona", frm, function (data) {
                console.log("Respuesta servidor", data);
                if (data.estado) {
                    tr.remove();
                }
            });
        }
        function btnGuardarEmail(frm) {
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_guardarCorreoPersona", frm, function (data) {
                console.log("respuesta servidor",data);
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
                    var tr = getTrNumeros(data.telefonoAgregado);
                    $(".tbodyTelefonos").prepend(tr);
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