// generic 
    function actualizarTrTabla(tr,persona) {
        tr.find(".tdNombre").empty().append(persona._nombres);
        tr.find(".tdApellido").empty().append(persona._apellidos);
        tr.find(".tdFechaNac").empty().append(persona.getFechaNac);
    }
    function getTrPersona(persona) {
        tr = "\
             <tr class='trPersona'>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdPersona' class='txtHdIdPersona' value='"+persona._idPersona+"'>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtNombrePersona' class='txtNombrePersona form-control'  />\
                    </div>\
                    <div class='normalMode tdNombre'>"+persona._nombres+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtApellidoPersona' class='txtApellidoPersona form-control' />\
                    </div>\
                    <div class='normalMode tdApellido'>"+persona._apellidos+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='dtFechaNacimiento' class='dtFechaNacimiento form-control' />\
                    </div>\
                    <div class='normalMode tdFechaNac'>" + persona.getFechaNac + "</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btn-xs btnEditMode btnActualizar '  >Actualizar</button>\
                        <button class='btn btn-xs btnEditMode btnCancelarEdit'>Cancelar</button>\
                    </div>\
                    <div class='normalMode'>\
                        <button class='btn btn-xs btnEditar' >Editar</button>\
                        <button class='btn btn-xs'>Eliminar</button>\
                    </div>\
                </td>\
            </tr>\
        ";
        return tr;

    }
// actualizar 
    function actualizar(trPersona) {
        console.log("actualizaste");
        //frm = serializeToJson(trPersona.find("input").serializeArray());
        frm = serializeSection(trPersona);
        arrDate = frm.dtFechaNacimiento.split("/");
        frm.dtFechaNacimiento = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2], arrDate[1], arrDate[0]));
        actualizarCatalogo(RAIZ+"GestionPersonas/actualizarPersona", frm, function (data) {
            if (data.estado) {
                persona = data.persona;
                actualizarTrTabla(trPersona, persona);
                controlesEdit(false, trPersona); // deshabilitar la edicion
                updateAllDataTable($('.tablePersonas'));
            }
        });
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
        nombres     = trPersona.find(".tdNombre").text();
        apellidos   = trPersona.find(".tdApellido").text();
        fechaNac = trPersona.find(".tdFechaNac").text();
        trPersona.find(".txtNombrePersona").val(nombres);
        trPersona.find(".txtApellidoPersona").val(apellidos);
        trPersona.find(".dtFechaNacimiento").val(fechaNac);
        controlesEdit(true, trPersona);
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
        frm = serializeSection(tr);
        arrDate = frm.dtFechaNacimiento.split("/");
        frm.dtFechaNacimiento = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2],arrDate[1],arrDate[0]));
        tbody = tr.parents("table").find("tbody");
        actualizarCatalogo(RAIZ+"GestionPersonas/sp_hm_agregarPersona", frm, function (data) {
            console.log("La respuesta del servidor fue:", data);
            if (data.estado) {
                persona = data.persona;
                newTr = getTrPersona(persona);
                clearTr(tr);
                //tbody.prepend(newTr);
                $(".tablePersonas").dataTable().fnAddTr($(newTr)[0]);
                //updateAllDataTable($(".tablePersona"));   
            } else {
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert(data.error.Message);
                }
            }
        });
    }