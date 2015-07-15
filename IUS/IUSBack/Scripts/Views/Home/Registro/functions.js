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