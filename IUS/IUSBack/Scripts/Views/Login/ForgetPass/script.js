$(document).ready(function () {
    // eventos 
        // click 
            $(document).on("submit", "#frmInvitado", function (e) {
                e.preventDefault();
                var frm = serializeSection($(this));
                console.log("Formulario a enviar es: ", frm);
                frmInvitado(frm);
            })

})