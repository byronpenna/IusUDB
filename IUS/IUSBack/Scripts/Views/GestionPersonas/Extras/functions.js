﻿// genericas 
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
    function getCbPaises(pais) {
        var cb = "<option value=" + pais._idPais + " >" + pais._pais + "</option>";
        return cb;
    }
    function regresarNormalidadTrTel(tel,tr) {
        tr.find(".tdTelefono").empty().append(tel._telefono);
        tr.find(".tdPais").empty().append(tel._pais._pais);
        tr.find(".tdEtiqueta").empty().append(tel._descripcion);
        tr.find(".txtHdIdPais").val(tel._pais._idPais);
        controlesEdit(false, tr);
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
            function btnActualizarTel(frm,tr) {
                actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_editarTelefonoPersona", frm, function (data) {
                    console.log("data servidor", data);
                    if (data.estado) {
                        regresarNormalidadTrTel(data.telefonoActualizado, tr);
                    }
                });
            }
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