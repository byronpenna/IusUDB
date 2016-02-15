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
                var x = prompt("Ingrese nombre de usuario");
                /*if (test(FORMATO_EMAIL, x)) {*/
                var frm = {
                    usuario: x
                }
                console.log("El valor de x es", x);
                if (x !== null) {
                    aOlvidoContra(frm);
                }
                /*} else {
                    printMessage($(".spanMensajes"),"Por favor coloque correctamente un email",false)
                }*/
            })
            //$(document).on("click", "#btnAdminLogin", function () {
            //    cambiarActivePestania("admin");
            //    $("#divLoginInvitado").addClass("hidden");
            //    $("#divLoginAdmin").removeClass("hidden");
            //})
})