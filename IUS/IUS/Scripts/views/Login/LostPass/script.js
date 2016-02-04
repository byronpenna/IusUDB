$(document).ready(function () {
    // scripts
        $(document).on("submit", "#frmInvitado", function (e) {
            var frm = serializeSection($(this));
            e.preventDefault();
            console.log("Entro aqui");
            frmInvitado(frm);
        })
})