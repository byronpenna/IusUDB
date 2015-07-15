$(document).ready(function () {
    // eventos 
        //click 
            $(document).on("click", "#btnUsuarioLogin", function () {
                $("#divLoginInvitado").removeClass("hidden");
                $("#divLoginAdmin").addClass("hidden");
            })
            $(document).on("click", "#btnAdminLogin", function () {
                $("#divLoginInvitado").addClass("hidden");
                $("#divLoginAdmin").removeClass("hidden");
            })
})