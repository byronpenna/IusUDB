$(document).ready(function () {
    // eventos 
        // click
            $(document).on("click", "#btnAdminLogin", function () {
                //console.log("Entro");
                window.location = RAIZ_BACK + "login/index";
            })
            $(document).on("click", ".btnRegistrarse", function () {
                window.location = RAIZ_BACK + "Home/Registro";
            })
            
})