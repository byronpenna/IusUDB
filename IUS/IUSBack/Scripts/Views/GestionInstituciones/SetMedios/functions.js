// generics 
    function fillInputsEdit(trMedio,medio,callback) {
        trMedio.find(".txtEnlace").val(medio.enlace);
        trMedio.find(".txtTextoEnlaceEdit").val(medio.nombreEnlace);
        callback();
    }
    function exitEditMode(trMedios,medio) {
        trMedios.find(".tdEnlace").empty().append(medio._enlace);
        trMedios.find(".tdNombreEnlace").empty().append(medio._nombreEnlace);
        controlesEdit( false, trMedios);
    }
    // acciones script
        function btnActualizar(frm, trMedio) {
            actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_editEnlaceInstitucion", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    exitEditMode(trMedio, data.enlace);
                }
            });
        }
        function btnEditar(trMedio) {
            medio = {
                enlace:         trMedio.find(".tdEnlace").text(),
                nombreEnlace:   trMedio.find(".tdNombreEnlace").text(),
            }
            fillInputsEdit(trMedio, medio, function () {
                controlesEdit(true, trMedio);
            });
        }
        function btnAgregar(frm) {
            actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_insertEnlaceInstituciones", frm, function (data) {
                console.log(data);
            });
        }
        function btnEliminar(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_deleteEnlaceInstitucion", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }