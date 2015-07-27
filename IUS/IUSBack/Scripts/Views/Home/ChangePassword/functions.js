    function frmChangePass(frm) {
        actualizarCatalogo(RAIZ + "/Home/sp_usu_changePass", frm, function (data) {
            console.log(data);
            if (data.estado) {
                window.location = RAIZ + "/Home/";
            }
        })
    }