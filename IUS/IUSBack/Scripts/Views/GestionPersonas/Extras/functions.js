// jcrop 
    function storeCoords(c) {
        $(".x").val(c.x);
        $(".y").val(c.y);
        $(".imgAlto").val(c.h);
        $(".imgAncho").val(c.w);
    };
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
            val.campos.txtEmail.push("-Campo no debe quedar vacio<br>");
        }
        var testEmail = test(FORMATO_EMAIL, frm.txtEmail);
        //console.log("Test email es:",testEmail);
        if (!testEmail) {
            val.campos.txtEmail.push("-Colocar formato correcto a email<br>");
        }
        if (frm.txtEtiquetaEmail == "") {
            val.campos.txtEtiquetaEmail.push("-Campo no debe quedar vacio<br>");
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
        // foto 
            function getFrmFoto() {
                var frm         = new Object();
                frm             = serializeSection($(".divCorte"));
                frm.idPersona   = $(".txtHdIdPersona").val();
                frm.imgAlto     = frm.imgAlto / $(".imgPersona").width();
                frm.imgAncho    = frm.imgAncho / $(".imgPersona").height();
                frm.x           = frm.x / $(".imgPersona").width();
                frm.y = frm.y / $(".imgPersona").height();
                return frm;
            }
            function inicialFoto() {
                $(".x").val(0);
                $(".y").val(0);
                $(".imgAlto").val(0);
                $(".imgAncho").val(0);
            }

        function getTrEmail(emailPersona,permisos) {
            console.log("Email persona es ", emailPersona);
            var strEditar = "", strEliminar = "";
            if (permisos !== undefined) {
                if(!permisos._editar){
                    strEditar = "disabled";
                }
                if (!permisos._eliminar) {
                    strEliminar = "disabled";
                }
            }
            var clasePrincipal = ""
            if (emailPersona._principal) {
                clasePrincipal = "fa-star";
            } else {
                clasePrincipal = "fa-star-o";
            }
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input type='hidden' value='"+emailPersona._idEmail+"' name='txtIdEmailPersona'/>\
                </td>\
                <td>\
                    <i class='fa " + clasePrincipal + " icoStarPrincipal pointer'></div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input name='txtEmail' class='inputBack form-control txtEmail input-sm' type='email' />\
                        <div class='row marginNull divResultado hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdEmail'>\
                        "+emailPersona._email+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='inputBack txtEtiquetaEmail form-control input-sm' name='txtEtiquetaEmail' />\
                        <div class='row marginNull divResultado hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdEtiqueta'>\
                        "+emailPersona._descripcion+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <div class='btn-group btn-block'>\
                            <button class='btnBack btn col-lg-6 btn-xs btn-default btnActualizarEmail'>Actualizar</button>\
                            <button class='btnBack btn col-lg-6 btn-xs btn-default btnCancelarUpdateEmail'>Cancelar</button>\
                        </div>\
                    </div>\
                    <div class='normalMode tdTelefono'>\
                        <div class='btn-group btn-block'>\
                            <button class='btnBack btnEditarEmail btn col-lg-6 btn-xs btn-default' " + strEditar + ">Editar</button>\
                            <button class='btnBack btnEliminarEmail btn col-lg-6 btn-xs btn-default' " + strEliminar + ">Eliminar</button>\
                        </div>\
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
        function getTrNumeros(telefono,permisos) {
            var strEditar = "", strEliminar = "";
            if (permisos !== undefined) {
                if (!permisos._editar) {
                    strEditar = "disabled";
                }
                if (!permisos._eliminar) {
                    strEliminar = "disabled";
                }
            }
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input name='txtHdIdTelefono' class='txtHdIdTelefono'  value='"+telefono._idTelefonoPersona+"'/>\
                    <input name='txtHdIdPais' class='txtHdIdPais' value='"+telefono._pais._idPais+"' />\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='inputBack txtTelefono form-control input-sm soloNumerosInt' name='txtTelefono' />\
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
                        <select class='selectBack cbPais' name='cbPais'></select>\
                        <div class='row marginNull divResultado hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdPais'>\
                        "+telefono._pais._pais+" \
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input class='inputBack txtEtiquetaTel input-sm form-control' name='txtEtiquetaTel' />\
                        <div class='row marginNull divResultado hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdEtiqueta'>\
                        "+telefono._descripcion+" \
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <div class='btn-group'>\
                            <button class='btn borde btn-sm btn-default btnActualizarTel' title='Aceptar'>\
                                <i class='fa fa-check'></i>\
                            </button>\
                            <button class='btn borde btn-sm btn-default btnCancelarUpdateTel' title='Eliminar'>\
                                <i class='fa fa-times'></i>\
                            </button>\
                        </div>\
                    </div>\
                    <div class='normalMode'>\
                        <div class='btn-group'>\
                            <button class='btn borde btn-sm btn-default btnEditarTel' " + strEditar + " title='Editar'>\
                                <i class='fa fa-pencil'></i>\
                            </button>\
                            <button class='btn borde btn-sm btn-default btnEliminarTel' " + strEliminar + " title='Eliminar'>\
                                <i class='fa fa-trash-o'></i>\
                            </button>\
                        </div>\
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
        function icoStarPrincipal(frm) {
            actualizarCatalogo(RAIZ + "/ExtrasGestionPersonas/sp_rrhh_setEmailPrincipal", frm, function (data) {
                console.log("la data es:", data);
                if (data.estado) {
                    $(".icoStarPrincipal").removeClass("fa-star");
                    $(".icoStarPrincipal").addClass("fa-star-o");
                    $(".txtIdEmailPersona[value='" + data.idCorreo + "']").parents("tr").find(".icoStarPrincipal").addClass("fa-star");
                    $(".txtIdEmailPersona[value='" + data.idCorreo + "']").parents("tr").find(".icoStarPrincipal").removeClass("fa-star-o");
                    //$(".txtIdEmailPersona[value='" + data.idCorreo + "']").parents("tr").find(".icoStarPrincipal").css("background", "red");
                }
            })
        }
    // foto 
        function frmImagenPersona(data, url, imagen,frm,jcrop_api) {
            getImageFromInputFile(imagen, function (imagen) {
                if (imagen.width == imagen.height || (frm.imgAlto > 0 && frm.imgAncho > 0 && frm.imgAncho > 0 ) ) {
                    jcrop_api.destroy();
                    accionAjaxWithImage(url, data, function (data) {
                        console.log("D: D: ",data);
                        if (data.estado) {
                            //jcrop_api.destroy();
                            printMessage($(".divImagePersona .divResultado"), "Imagen asignada exitosamente", true);
                            //$(".imgPersona").attr("src", imagen.src);
                            $(".imgPersona").attr("src", data.imagen+ "?"+ (new Date()).getTime());
                            $(".imgPersona").attr("style", "");
                            $(".btnEstablecer").prop("disabled", true);
                            //jcrop_api.destroy();
                            //$.Jcrop('.imgPersona').destroy();
                        }
                    }, function () {

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
                if (data.estado) {
                    var tr      = getTrEmail(data.emailPersona,data.permisos);
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
                    if (data.estado) {
                        var tr = getTrNumeros(data.telefonoAgregado,data.permisos);
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