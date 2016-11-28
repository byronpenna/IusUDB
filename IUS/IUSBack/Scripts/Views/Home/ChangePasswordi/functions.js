function frmChangePass(frm) {
    console.log("Entro aquí");
    actualizarCatalogo(RAIZ + "/Home/ajax_changePassHome", frm, function (data) {
        console.log("El formulario es: ", data);
        if (data.estado) {
            alert("Se cambio la contraseña correctamente");
            $(".frmChangePass")[0].reset();
        } else {
            alert("Ocurrio un error cambiado contraseña");
        }
    })
}