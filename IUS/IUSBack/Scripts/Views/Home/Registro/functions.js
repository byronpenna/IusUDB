function frmRegistrar(frm) {
    actualizarCatalogo(RAIZ + "/Home/sp_secpu_addUsuario", frm, function (data) {
        console.log(data);
    });
}