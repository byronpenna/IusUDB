// genericas 
    function validarIngreso(frm) {
        var val = new Object();
        val.campos = {
            txtEmail:       new Array(),
            txtPass:        new Array(),
            txtNombre:      new Array(),
            txtApellidos:   new Array(),
            txtFechaNac:    new Array()
        }
        if (frm.txtEmail !== undefined && frm.txtEmail == "") {
            val.campos.txtEmail.push("Este campo no puede quedar vacio");
        }
        if (frm.txtPass !== undefined && frm.txtPass == "") {
            val.campos.txtPass.push("Este campo no puede quedar vacio");
        }
        if (frm.txtNombre !== undefined && frm.txtNombre == "") {
            val.campos.txtNombre.push("Este campo no puede quedar vacio");
        }
        if (frm.txtApellidos !== undefined && frm.txtApellidos == "") {
            val.campos.txtApellidos.push("Este campo no puede quedar vacio");
        }
        if (frm.txtFechaNac !== undefined && frm.txtFechaNac == "") {
            val.campos.txtFechaNac.push("Este campo no puede quedar vacio");
        } else {
            var exp = /^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[/\\/](19|20)\d{2}$/;
            if (!exp.test(frm.txtFechaNac)) {
                val.campos.txtFechaNac.push("Campo deber ser rellenado con formato dd/mm/yyyy")
            }
        }
        
        val.general = new Array();
        val.estado = getEstadoVal(val);
        return val;
    }
// scripts 
    function frmRegistrar(frm) {
        actualizarCatalogo(RAIZ + "/Home/sp_secpu_addUsuario", frm, function (data) {
            console.log(data);
            $(".spanResultado").removeClass("hidden");
            if (data.estado) {
                $(".spanResultado").addClass("spanSuccess");
                $(".spanResultado").removeClass("spanError");
                $(".spanResultado").empty().append("Registrado correctamente revise correo electronico para confirmar su cuenta");
            } else {
                $(".spanResultado").addClass("spanError");
                $(".spanResultado").removeClass("spanSuccess");
                $(".spanResultado").empty().append("Ocurrio un error");
            }
        });
    }