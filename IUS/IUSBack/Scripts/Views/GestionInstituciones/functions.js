// genericas 
    function getTrInstituciones(institucion) {
        tr = "\
            <tr>\
                <td>\
                    <input class='txtHdIdInstitucion' name='txtHdIdInstitucion' value='"+institucion.idInstitucion+"'>\
                </td>\
                <td>"+institucion._nombre+"</td>\
                <td>"+institucion._direccion+"</td>\
                <td>"+institucion._pais._pais+"</td>\
                <td>\
                    <button class='btn'>Editar</button>\
                    <button class='btn btnDeleteInstitucion'>Eliminar</button>\
                </td>\
            </tr>\
        ";
        return tr;
    }
    function comboPaisAddOpcions(Paises,combo,selected) {
        combo.empty();
        $.each(Paises, function (i, pais) {
            opcion = { text: pais._pais ,value:pais._idPais}
            comboAddOpcion(combo,opcion,selected);
        });
    }
    function exitEditMode(trInstitucion,institucion) {
        trInstitucion.find(".tdNombre").empty().append(institucion._nombre);
        trInstitucion.find(".tdDireccion").empty().append(institucion._direccion);
        trInstitucion.find(".txtHdIdPais").val(institucion._pais._idPais);
        trInstitucion.find(".tdPais").empty().append(institucion._pais._pais)
        controlesEdit(false, trInstitucion);
    }
    function fillInputsEdit(trInstitucion, institucion, callback) {
        trInstitucion.find(".txtNombreInstitucionEdit").val(institucion.nombre);
        trInstitucion.find(".txtAreaDireccionEdit").val(institucion.direccion);
        // llenar cosa de paises
        frm = {};
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_getPaises", frm, function (data) {
            if (data.estado) {
                combo = trInstitucion.find(".cbPaisEdit");
                comboPaisAddOpcions(data.paises, combo, institucion.idPais);
                combo.chosen({ no_results_text: "Ese pais no existe", width: '100%' });
            }
            callback();
        });
    }
// acciones script
    function btnActualizarInstitucion(frm, trInstitucion) {
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_editInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                exitEditMode(trInstitucion, data.institucion);
            }
        });
    }
    function btnEditar(trInstitucion) {
        institucion = {
            nombre: trInstitucion.find(".tdNombre").text(),
            direccion: trInstitucion.find(".tdDireccion").text(),
            idPais: trInstitucion.find(".txtHdIdPais").val(),
            idInstitucion: trInstitucion.find(".txtHdIdInstitucion").val(),
            
        }
        console.log(institucion);
        fillInputsEdit(trInstitucion, institucion, function () {
            controlesEdit(true, trInstitucion);
        });
        
    }
    function btnDeleteInstitucion(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_deleteInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                table = $(".tbInstituciones");
                removeDataTable(table, tr);
            }
        });
    }
    function btnAddInstitucion(frm, seccion) {
        
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_insertInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr = getTrInstituciones(data.institucion);
                $(".tbInstituciones").dataTable().fnAddTr($(tr)[0]);
                clearTr(seccion);
            }
        })
    }