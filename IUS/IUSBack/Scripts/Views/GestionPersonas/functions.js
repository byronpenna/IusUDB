﻿// generic 
    function actualizarTrTabla(tr,persona) {
        tr.find(".tdNombre").empty().append(persona._nombres);
        tr.find(".tdApellido").empty().append(persona._apellidos);
        tr.find(".tdFechaNac").empty().append(persona.getFechaNac);
        tr.find(".tdSexo").empty().append(persona._sexo._sexo);
        tr.find(".txtHdIdSexo").val(persona._sexo._idSexo);
        //console.log("Sexp a poner", persona._sexo.idSexo);
    }
    function getTrPersona(persona,permisos) {
        tr = "\
             <tr class='trPersona'>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdPersona' class='txtHdIdPersona' value='"+persona._idPersona+"'>\
                    <input class='txtHdIdSexo' value='"+persona._sexo._idSexo+"' />\
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
                        <select class='form-control cbSexo' name='cbSexo'>\
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
                        <div class='btn-group'>\
                            <button class='btn btn-xs btn-default btnEditMode btnActualizar '  " + permisos.stringEditar + ">Actualizar</button>\
                            <button class='btn btn-xs btn-default btnEditMode btnCancelarEdit'>Cancelar</button>\
                        </div>\
                    </div>\
                    <div class='normalMode'>\
                        <div class='btn-group'>\
                            <button class='btn btn-xs btn-default btnEditar' " + permisos.stringEditar + ">Editar</button>\
                            <button class='btn btn-xs btn-default btnEliminar' " + permisos.stringEliminar + ">Eliminar</button>\
                        </div>\
                        <a class='btn btn-xs ' href='"+RAIZ+"GestionPersonas/Extras/"+persona._idPersona+"'>Info adicional</a>\
                        <a class='btn btn-xs' href='"+RAIZ+"GestionPersonas/Detalle/"+persona._idPersona+"'>\
                            Ver ficha\
                        </a>\
                    </div>\
                </td>\
            </tr>\
        ";
        return tr;

    }
// actualizar 
    function actualizar(trPersona) {
        var frm = serializeSection(trPersona);
        var val = validacionIngreso(frm);
        trPersona.find(".divResultado").empty();
        trPersona.find(".divResultado").addClass("hidden");
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
    function getObjPersonaEdit() {
        var persona = {
            nombres:    trPersona.find(".tdNombre").text(),
            apellidos:  trPersona.find(".tdApellido").text(),
            fechaNac:   trPersona.find(".tdFechaNac").text(),
            idSexo:     trPersona.find(".txtHdIdSexo").val()
        }
        return persona;
    }
    function editMode(trPersona) {
        var persona = getObjPersonaEdit(trPersona);
        trPersona.find(".txtNombrePersona").val(persona.nombres);
        trPersona.find(".txtApellidoPersona").val(persona.apellidos);
        trPersona.find(".dtFechaNacimiento").val(persona.fechaNac);
        trPersona.find(".dtFechaNacimiento").datepicker({
            dateFormat: "dd/mm/yy"
        });
        trPersona.find(".cbSexo").val(persona.idSexo);
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
        //oTable.row('.selected').remove().draw(false);
        actualizarCatalogo(RAIZ + "GestionPersonas/sp_hm_eliminarPersona", frm, function (data) {
            if (data.estado) {
                table = $(".tablePersonas");
                removeDataTable(table, tr);
            } else {
                console.log("error es:", data.error);
            }
        });
    }
    function limpiarTrAgregar(tr) {
        tr.find(".txtNombrePersona").val("");
        tr.find(".txtApellidoPersona").val("");
        tr.find(".dtFechaNacimiento").val("");
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
                    //clearTr(tr);//##############################
                    limpiarTrAgregar(tr);
                    //tbody.prepend(newTr);
                    $(".tablePersonas").dataTable().fnAddTr($(newTr)[0]);
                    printMessage($(".divResultadoGeneral .divResultado"), "Persona agregada correctamente", true);
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
    }