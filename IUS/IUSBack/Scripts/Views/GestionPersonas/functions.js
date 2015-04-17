//
// actualizar 
    function actualizar(trPersona) {
        frm = serializeToJson(trPersona.find("input").serializeArray());
        console.log("formulario es:", frm);
        actualizarCatalogo("/GestionPersonas/actualizarPersona", frm, function (data) {
            if (data.estado) {
                persona = data.persona;
                console.log("la persona es", persona);
                trPersona.find(".tdNombre").empty().append(persona._nombres);
                trPersona.find(".tdApellido").empty().append(persona._apellidos);
                trPersona.find(".tdFechaNac").empty().append(persona.getFechaNac);
                controlesEdit(false, trPersona); // deshabilitar la edicion
            }
        });
        

    }
//edit 
    function editMode(trPersona) {
        nombres     = trPersona.find(".tdNombre").text();
        apellidos   = trPersona.find(".tdApellido").text();
        fechaNac = trPersona.find(".tdFechaNac").text();
        console.log("la fecha de nacimiento es",fechaNac);
        trPersona.find(".txtNombrePersona").val(nombres);
        trPersona.find(".txtApellidoPersona").val(apellidos);
        trPersona.find(".dtFechaNacimiento").val(fechaNac);
        controlesEdit(true, trPersona);
    }

// acciones desde script
    function btnAgregarPersona(tr) {
        frm = serializeSection(tr);
        console.log("el formulario a enviar es:", frm);
        actualizarCatalogo("/GestionPersonas/sp_hm_agregarPersona", frm, function (data) {
            if (data.estado) {

            }
        });
    }