$(document).ready(function () {
    // scripts
        $(document).on("submit", "#frmInvitado", function (e) {
            var frm = serializeSection($(this));
            e.preventDefault();
            console.log("Entro aqui");
            if (frm.txtHdAccion == 1)
            {
                // para lost pass
                frmInvitado(frm);
            } else {

            }
                
        })
})