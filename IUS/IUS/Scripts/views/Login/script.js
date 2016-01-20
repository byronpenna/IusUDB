$(document).ready(function () {
    // eventos 
        // submit 
            $(document).on("submit", "#frmInvitado", function (e) {
                var frm = serializeSection($(this));
                e.preventDefault();
                console.log("Formulario es", frm);
                frmInvitado(frm);
            })
        // click
            
            $(document).on("click", "#btnAdminLogin", function () {
                //console.log("Entro");
                window.location = RAIZ_BACK + "login/index";
            })
            $(document).on("click", ".btnRegistrarse", function () {
                window.location = RAIZ_BACK + "Home/Registro";
            })
            
})