// genericas
    // partes
        function getTrInstitucionEducativa(institucion) {
            var tr = "<tr>\
                       <td class='hidden'>\
                           <input name='txtHdIdInstitucionEducativa' class='txtHdIdInstitucionEducativa' value='"+institucion._idInstitucion+"' />\
                           <input name='txtHdIdPaisInstitucion' class='txtHdIdPaisInstitucion' value='"+institucion._pais._idPais+"'/>\
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                               <input class='txtInstitucionEducativa form-control' name='txtInstitucionEducativa' />\
                           </div>\
                           <div class='normalMode tdNombreInstitucion'>\
                               "+institucion._nombre+"\
                           </div>\
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                               <select name='cbPaisInstitucionEducativa' class='form-control cbPaisInstitucionEducativa'>\
                               </select>\
                           </div>\
                           <div class='normalMode tdPais'>\
                               "+institucion._pais._pais+"\
                           </div>\
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                                <button class='btn btnActualizarInstitucionEducativa'>Actualizar</button>\
                                <button class='btn btnCancelarInstitucionEducativa'>Cancelar</button>\
                           </div>\
                           <div class='normalMode tdEmail'>\
                               <button class='btn btnEditarInstitucion'>Editar</button>\
                               <button class='btn btnEliminarInstitucion'>Eliminar</button>\
                           </div>\
                       </td>\
                   </tr>";
            return tr;
        }
        function getCbPaises(pais) {
            var cb = "<option value=" + pais._idPais + " >" + pais._pais + "</option>";
            return cb;
        }
// acciones script
    // instituciones
        // edicion 
            function btnActualizarInstitucionEducativa(frm,tr) {
                actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarInstitucionEducativa", frm, function (data) {
                    console.log("data al editar", data);
                })
            }
        function btnAgregarInstitucion(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
            })
        }
        function btnEliminarInstitucion(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
    // carreras
        function btnEliminarCarrera(frm,tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarCarrera", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
        function btnAgregarCarreraIndividual(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarCarrera", frm, function (data) {
                console.log("Data servidor", data);
            })
        }
        
    // formacion personas
        function btnAgregarCarrera(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarFormacionPersona", frm, function (data) {
                console.log("la data es", data);
            })
        }
        function btnEliminarTitulo(frm,tr){
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarTituloPersona", frm, function (data) {
                console.log("la data es:", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }