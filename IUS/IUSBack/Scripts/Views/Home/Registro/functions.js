﻿// genericas 
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
    function btnReenviar(frm) {
        actualizarCatalogo(RAIZ + "/Home/sp_secpu_reenviarCorreo", frm, function (data) {
            console.log("La respuesta es", data);
            var divImprimir = $(".rowMensajeReenviar");
            if (data.estado) {
                printMessage(divImprimir , "El correo se envio correctamente", true);
            } else {
                if (data.error !== undefined && data.error != null && data.error._mostrar) {
                    printMessage(divImprimir, data.error.Message, false);
                } else {
                    printMessage(divImprimir,"Error no controlado", false);
                }

            }
        })
    }
// scripts 
    function frmRegistrar(frm) {
        
        console.log("vv'");
        actualizarCatalogo(RAIZ + "/Home/sp_secpu_addUsuario", frm, function (data) {
            console.log("Respuesta al querer agregar",data);
            //$(".spanResultado").removeClass("hidden");
            if (data.estado) {
                /*$(".spanResultado").addClass("spanSuccess");
                $(".spanResultado").removeClass("spanError");
                $(".spanResultado").empty().append("Registrado correctamente revise correo electronico para confirmar su cuenta");*/
                clearTr($(".frmRegistrar"));
                printMessage($(".spanResultado"), "Registrado correctamente revise correo electronico para confirmar su cuenta", true);
                //
            } else {
                var mjs = "";
                $(".spanResultado").addClass("spanError");
                $(".spanResultado").removeClass("spanSuccess");
                if (data.error._mostrar) {
                    mjs = data.error.Message;
                } else {
                    mjs = "Ocurrio un error agregando";
                }
                //$(".spanResultado").empty().append(mjs);
                printMessage($(".spanResultado"), mjs, false);
            }
        }, function () {
            var img = "<img src='" + IMG_GENERALES + "ajax-loader.gif'>";
            $(".spanResultado").empty().append("Espere por favor </br>"+img);
        });
    }