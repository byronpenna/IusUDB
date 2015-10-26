// validacion 
    function validarInsertExtra(frm) {
        var val = new Object();
        val.campos = {
            cbPais:                     new Array(),
            txtNumeroIdentificacion:    new Array(),
            cbEstadoCivil:              new Array()
        }
        if (frm.cbPais == "") {
            val.campos.cbPais.push("Campo no debe quedar vacio");
        } else {
            if (frm.cbPais == -1) {
                val.campos.cbPais.push("Por favor seleccione un pais");
            }
        }
        if (frm.txtNumeroIdentificacion == "") {
            val.campos.txtNumeroIdentificacion.push("Campo no debe quedar vacio");
        }
        if (frm.cbEstadoCivil == "") {
            val.campos.cbEstadoCivil.push("Campo no debe quedar vacio");
        } else {
            if (frm.cbEstadoCivil == -1) {
                val.campos.cbEstadoCivil.push("Por favor seleccione un estado civil");
            }
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function validarInsertEmail(frm) {
        var val = new Object();
        val.campos = {
            txtEmail:           new Array(),
            txtEtiquetaEmail:   new Array()
        }
        if (frm.txtEmail == "") {
            val.campos.txtEmail.push("Campo no debe quedar vacio");
        }
        if (frm.txtEtiquetaEmail == "") {
            val.campos.txtEtiquetaEmail.push("Campo no debe quedar vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function validarInsertTelefono(frm) {
        var val = new Object();
        val.campos = {
            txtTelefono: new Array(),
            cbPais: new Array(),
            txtEtiquetaTel: new Array()
        }
        if (frm.txtTelefono == "") {
            val.campos.txtTelefono.push("Campo no debe quedar vacio");
        }
        if (frm.txtEtiquetaTel == "") {
            val.campos.txtEtiquetaTel.push("Campo no debe quedar vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
// genericas 
        function getTrEmail(emailPersona){
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input type='hidden' value='"+emailPersona._idEmail+"' name='txtIdEmailPersona'/>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input name='txtEmail' class=' form-control txtEmail' type='email' />\
                    </div>\
                    <div class='normalMode tdEmail'>\
                        "+emailPersona._email+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='txtEtiquetaEmail form-control' name='txtEtiquetaEmail' />\
                    </div>\
                    <div class='normalMode tdEtiqueta'>\
                        "+emailPersona._descripcion+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btn-xs btnActualizarEmail'>Actualizar</button>\
                        <button class='btn btn-xs btnCancelarUpdateEmail'>Cancelar</button>\
                    </div>\
                    <div class='normalMode tdTelefono'>\
                        <button class='btn btnEditarEmail btn-xs'>Editar</button>\
                        <button class='btn btnEliminarEmail btn-xs'>Eliminar</button>\
                    </div>\
                </td>\
            </tr>\
            ";
            return tr;
        }
        function regresarNormalidadTrEmail(emailPersona,tr) {
            tr.find(".tdEtiqueta").empty().append(emailPersona._descripcion)
            tr.find(".tdEmail").empty().append(emailPersona._email);
            controlesEdit(false, tr);
        }
        function clearFrmAddEmail() {
            $(".txtEmail").val("");
            $(".txtEtiquetaEmail").val("");
        }
        
    // numeros
        function getTrNumeros(telefono) {
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input name='txtHdIdTelefono' class='txtHdIdTelefono'  value='"+telefono._idTelefonoPersona+"'/>\
                    <input name='txtHdIdPais' class='txtHdIdPais' value='"+telefono._pais._idPais+"' />\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='txtTelefono form-control' name='txtTelefono' />\
                        <div class='row marginNull divResultado hidden'>\
                            _ \
                        </div>\
                    </div>\
                    <div class='normalMode tdTelefono'>\
                        "+telefono._telefono+" \
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='cbPais' name='cbPais'></select>\
                    </div>\
                    <div class='normalMode tdPais'>\
                        "+telefono._pais._pais+" \
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='txtEtiquetaTel form-control' name='txtEtiquetaTel' />\
                    </div>\
                    <div class='normalMode tdEtiqueta'>\
                        "+telefono._descripcion+" \
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btn-xs btnActualizarTel'>Actualizar</button>\
                        <button class='btn btn-xs btnCancelarUpdateTel'>Cancelar</button>\
                    </div>\
                    <div class='normalMode'>\
                        <button class='btn btn-xs btnEditarTel'>Editar</button>\
                        <button class='btn btnEliminarTel btn-xs'>Eliminar</button>\
                    </div>\
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
        function clearFrmAddTel() {
            $(".txtTelefono").val("");
            $(".txtEtiquetaTel").val("");
        }
// scripts
    // foto 
        function frmImagenPersona(data, url, imagen) {
            getImageFromInputFile(imagen, function (imagen) {
                if (imagen.width == imagen.height) {
                    accionAjaxWithImage(url, data, function (data) {
                        console.log("D: D: ",data);
                        if (data.estado) {
                            printMessage($(".divImagePersona .divResultado"), "Imagen asignada exitosamente", true);
                            $(".imgPersona").attr("src", imagen.src);
                        }
                    })
                } else {
                    //alert("La imagen debe ser cuadrada");
                    printMessage($(".divImagePersona .divResultado"), "La imagen debe ser cuadrada", false);
                }
            })
        }
    // agregar email
        function btnActualizarEmail(frm,tr) {
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_actualizarCorreoPersona", frm, function (data) {
                console.log("actualizacion mail",data);
                if (data.estado) {
                    regresarNormalidadTrEmail(data.emailActualizado, tr);
                }
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
                    var tr      = getTrEmail(data.emailPersona);
                    var tbody   = $(".tbodyEmail");
                    if (tbody.find(".trNoReg").length == 0) {
                        tbody.prepend(tr);
                    } else {
                        tbody.empty().append(tr);
                    }
                    clearFrmAddEmail();
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
                        var tbody = $(".tbodyTelefonos");
                        if (tbody.find(".trNoReg").length == 0) {
                            tbody.prepend(tr);
                        } else {
                            tbody.empty().append(tr);
                        }
                        
                        clearFrmAddTel();
                    }
                })
            }
        // informacion basica
            function btnGuardarInformacionBasica(frm) {
                actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_guardarInformacionPersona", frm, function (data) {
                    
                    if (data.estado) {
                        printMessage($(".divResultadoOperacion"), "Informacion actualizada exitosamente", true);
                        $(".rowControles").find(".divResultado").removeClass("visibilitiHidden");
                        $(".rowControles").find(".divResultado").addClass("hidden");
                    } else {
                        if (data.error._mostrar) {
                            printMessage($(".divResultadoOperacion"), data.error.Message, false);
                        } else {
                            printMessage($(".divResultadoOperacion"), "Ocurrio un error no controlado", false);
                        }
                    }
                })
            }