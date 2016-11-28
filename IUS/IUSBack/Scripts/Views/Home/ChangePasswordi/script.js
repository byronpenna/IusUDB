$(document).on("submit", ".frmChangePass", function (e) {
    e.preventDefault();
    var frm = serializeToJson($(this).serializeArray());
    console.log("El formulario es:", frm);
    if (frm.txtPass == frm.txtConfirmarPass) {
        //console.log("El formulario es:", frm);
        frmChangePass(frm);
    } else {
        alert("El campo contraseña y confirmar contraseña no coinciden");
    }
})