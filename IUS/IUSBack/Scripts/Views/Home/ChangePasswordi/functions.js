function frmChangePass(frm) {
    console.log("Entro aquí");
    actualizarCatalogo(RAIZ + "/Home/ajax_changePassHome", frm, function (data) {
        console.log("El formulario es: ", data);
        if (data.estado) {
            //alert("Se cambio la contraseña correctamente");
            printMessage($(".divMensaje"), "Se cambio la contraseña correctamente", true);
            $(".frmChangePass")[0].reset();
        } else {
            printMessage($(".divMensaje"), "Ocurrio un error cambiado contraseña", false);
            //alert("Ocurrio un error cambiado contraseña");
        }
    })
}