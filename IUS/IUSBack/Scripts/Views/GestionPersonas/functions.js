// actualizar 
    function actualizar(trPersona) {
        frm = serializeToJson(trPersona.find("input").serializeArray());
        actualizarCatalogo("", frm, function (data) {
        });
        controlesEdit(false, trPersona); // deshabilitar la edicion

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