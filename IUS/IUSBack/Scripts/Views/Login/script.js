$(document).ready(function () {
    // eventos 
        //click 
            $(document).on("click", "#btnUsuarioLogin", function () {
                //cambiarActivePestania("usuario");
                //$("#divLoginInvitado").removeClass("hidden");
                //$("#divLoginAdmin").addClass("hidden");
                window.location = RAIZFRONT + "Login/index";
            })
            //$(document).on("click", "#btnAdminLogin", function () {
            //    cambiarActivePestania("admin");
            //    $("#divLoginInvitado").addClass("hidden");
            //    $("#divLoginAdmin").removeClass("hidden");
            //})
})