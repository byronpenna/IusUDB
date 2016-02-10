$(document).ready(function () {
    // eventos 
        //click 
            $(document).on("click", "#btnUsuarioLogin", function () {
                //cambiarActivePestania("usuario");
                //$("#divLoginInvitado").removeClass("hidden");
                //$("#divLoginAdmin").addClass("hidden");
                window.location = RAIZFRONT + "Login/index";
            })
            $(document).on("click", ".aOlvidoContra", function (e) {
                e.preventDefault();
                var x = prompt("Ingrese correo electronico");
                if (test(FORMATO_EMAIL, x)) {

                } else {
                    printMessage($(".spanMensajes"),"Por favor coloque correctamente un email",false)
                }
            })
            //$(document).on("click", "#btnAdminLogin", function () {
            //    cambiarActivePestania("admin");
            //    $("#divLoginInvitado").addClass("hidden");
            //    $("#divLoginAdmin").removeClass("hidden");
            //})
})