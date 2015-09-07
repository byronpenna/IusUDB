// generic 
    function actualizarTrTabla(tr,persona) {
        tr.find(".tdNombre").empty().append(persona._nombres);
        tr.find(".tdApellido").empty().append(persona._apellidos);
        tr.find(".tdFechaNac").empty().append(persona.getFechaNac);
    }
    function getTrPersona(persona,permisos) {
        tr = "\
             <tr class='trPersona'>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdPersona' class='txtHdIdPersona' value='"+persona._idPersona+"'>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtNombrePersona' class='txtNombrePersona form-control txtEdit soloLetras'  />\
                        <div class='row marginNull divResultado hidden'>\
                        </div>\
                    </div>\
                    <div class='normalMode tdNombre'>"+persona._nombres+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtApellidoPersona' class='txtApellidoPersona form-control txtEdit soloLetras' />\
                        <div class='row marginNull divResultado hidden'>\
                        </div>\
                    </div>\
                    <div class='normalMode tdApellido'>"+persona._apellidos+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='dtFechaNacimiento' class='dtFechaNacimiento form-control txtEdit' />\
                        <div class='row marginNull divResultado hidden'>\
                        </div>\
                    </div>\
                    <div class='normalMode tdFechaNac'>" + persona.getFechaNac + "</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='form-control'>\
                            <option value='1'>Masculino</option>\
                            <option value='2'>Femenino</option>\
                        </select>\
                        <div class='row marginNull divResultado hidden'>\
                        </div>\
                    </div>\
                    <div class='normalMode tdSexo'>"+persona._sexo._sexo+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btn-xs btnEditMode btnActualizar '  "+permisos.stringEditar+">Actualizar</button>\
                        <button class='btn btn-xs btnEditMode btnCancelarEdit'>Cancelar</button>\
                    </div>\
                    <div class='normalMode'>\
                        <button class='btn btn-xs btnEditar' " + permisos.stringEditar + ">Editar</button>\
                        <button class='btn btn-xs btnEliminar' " + permisos.stringEliminar + ">Eliminar</button>\
                        <a class='btn btn-xs ' href='"+RAIZ+"GestionPersonas/Extras'>Info adicional</a>\
                    </div>\
                </td>\
            </tr>\
        ";
        return tr;

    }
// actualizar 
    function actualizar(trPersona) {
        //console.log("actualizaste");
        //frm = serializeToJson(trPersona.find("input").serializeArray());
        var frm = serializeSection(trPersona);
        var val = validacionIngreso(frm);
        trPersona.find(".divResultado").empty();
        trPersona.find(".divResultado").addClass("hidden");
        console.log("Formulario a enviar es", frm);
        if (val.estado) {
            arrDate = frm.dtFechaNacimiento.split("/");
            frm.dtFechaNacimiento = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2], arrDate[1], arrDate[0]));
            actualizarCatalogo(RAIZ + "GestionPersonas/actualizarPersona", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    persona = data.persona;
                    actualizarTrTabla(trPersona, persona);
                    controlesEdit(false, trPersona); // deshabilitar la edicion
                    updateAllDataTable($('.tablePersonas'));
                }
                else {
                    if (data.error._mostrar) {
                        printMessage($(".divResultadoGeneral .divResultado"), data.error.Message, false)
                    }
                }
            });
        } else {
            //trPersona.remove();
            var errores;
            $.each(val.campos, function (i, val) {
                errores = "";
                var divResultado = trPersona.find("." + i).parents(".editMode").find(".divResultado");
                
                if (val.length > 0) {
                    divResultado.removeClass("hidden");
                    $.each(val, function (i, val) {
                        errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                    })
                    divResultado.empty().append(errores);
                }
            })
        }
        
    }
    function btnActualizarTodo(tabla) {
        accionActualizarGeneral(tabla, "GestionPersonas/actualizarTodo", function (data, frm) {
            if (data.estado) {
                $.each(data.personas, function (i,val) {
                    tr = getEdit(tabla, ".txtHdIdPersona", val._idPersona);
                    tr = tr.parents("tr");
                    actualizarTrTabla(tr, val);
                    controlesEdit(false, tr);
                    updateAllDataTable($('.tablePersonas'));
                });
            } else {
                alert("Ocurrio un error al intentar actualizar todo");
            }
        });
    }
//edit 
    function editMode(trPersona) {
        var nombres     = trPersona.find(".tdNombre").text();
        var apellidos   = trPersona.find(".tdApellido").text();
        var fechaNac    = trPersona.find(".tdFechaNac").text();
        var idSexo      = trPersona.find(".txtHdIdSexo").val();
        trPersona.find(".txtNombrePersona").val(nombres);
        trPersona.find(".txtApellidoPersona").val(apellidos);
        trPersona.find(".dtFechaNacimiento").val(fechaNac);
        trPersona.find(".cbSexo").val(idSexo);
        controlesEdit(true, trPersona);
    }
// validaciones
    function validacionIngreso(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            txtNombrePersona: new Array(),
            txtApellidoPersona: new Array(),
            dtFechaNacimiento: new Array()
        }
        if (frm.txtNombrePersona == "") {
            val.campos.txtNombrePersona.push("Campo no debe quedar vacio");
        }
        if (frm.txtApellidoPersona == "") {
            val.campos.txtApellidoPersona.push("Campo no debe quedar vacio");
        }
        if (frm.dtFechaNacimiento == "") {
            val.campos.dtFechaNacimiento.push("Campo no debe quedar vacio");
        } else {
            var exp = /^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[/\\/](19|20)\d{2}$/;
            if (!exp.test(frm.dtFechaNacimiento)) {
                console.log("No val");
                val.campos.dtFechaNacimiento.push("Campo deber ser rellenado con formato dd/mm/yyyy")
            } else {
                console.log("Val");
            }
        }
        if (val.campos.txtNombrePersona.length == 0 && val.campos.txtApellidoPersona == 0 && val.campos.dtFechaNacimiento == 0) {
            estado = true;
        }
        val.estado = estado;
        return val;
    }
// acciones desde script

    function btnEliminar(tr) {
        frm = serializeSection(tr);
        console.log("formulario a enviar", frm);
        
        //oTable.row('.selected').remove().draw(false);
        actualizarCatalogo(RAIZ + "GestionPersonas/sp_hm_eliminarPersona", frm, function (data) {
            console.log("respuesta del servidor", data);
            if (data.estado) {
                table = $(".tablePersonas");
                removeDataTable(table, tr);
            } else {
                console.log("error es:", data.error);
            }
        });
    }
    function btnAgregarPersona(tr) {
        $(".divResultadoGeneral .divResultado").hide();
        var frm = serializeSection(tr);
        var val = validacionIngreso(frm);
        tr.find(".divResultado").empty();
        tr.find(".divResultado").addClass("hidden")
        console.log(frm);
        if (val.estado) {
            arrDate = frm.dtFechaNacimiento.split("/");
            frm.dtFechaNacimiento = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2], arrDate[1], arrDate[0]));
            tbody = tr.parents("table").find("tbody");
            actualizarCatalogo(RAIZ + "GestionPersonas/sp_hm_agregarPersona", frm, function (data) {
                console.log("Respuesta servidor",data);
                if (data.estado) {
                    persona = data.persona;
                    newTr = getTrPersona(persona, data.permisos);
                    clearTr(tr);
                    //tbody.prepend(newTr);
                    $(".tablePersonas").dataTable().fnAddTr($(newTr)[0]);
                    //updateAllDataTable($(".tablePersona"));   
                } else {
                    var mjs = "";
                    if (data.error._mostrar) {
                        mjs = data.error.Message;
                        //printMessage($(".divResultadoGeneral .divResultado"), data.error.Message, false)
                    } else {
                        mjs = "Ocurrio un error no controlado";
                    }
                    printMessage($(".divResultadoGeneral .divResultado"), mjs, false);
                    
                }
            });
            
        } else {
            var errores;
            $.each(val.campos, function (i,val) {
                errores = "";
                var divResultado = $(".tablePersonas thead").find("." + i).parents("th").find(".divResultado")
                if (val.length > 0) {
                    console.log("entro");
                    divResultado.removeClass("hidden");
                    $.each(val, function (i, val) {
                        errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                    })
                    divResultado.empty().append(errores);
                }
            })
        }
        /*
        
        */
    }