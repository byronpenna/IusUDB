$(document).ready(function () {
    // scripts
        $(document).on("submit", "#frmInvitado", function (e) {
            var frm = serializeSection($(this));
            e.preventDefault();
            console.log("Entro aqui");
            if (frm.txtHdAccion == 1)
            {
                console.log("Accion 1");
                // para lost pass
                frmInvitado(frm);
            } else {
                console.log("Accion 2");
                cambiarPass(frm);
            }
                
        })
})