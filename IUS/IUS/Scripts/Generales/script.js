$(document).ready(function () {
    $.extend($.expr[':'], {
        'containsi': function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || '').toLowerCase()
            .indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });
    // automaticos
        setIdiomaPreferido();
    // Constantes
    // Eventos 
        // keypress
            $(document).on("keypress", ".soloLetras", function (e) {
                var str = String.fromCharCode(e.which);
                exp = soloLetras();
                var x = test(exp, str);
                if (!x) {
                    e.preventDefault();
                }
            })
        // click 
            $(document).on("click", ".aCerrarSession", function () {
                var frm = {};
                actualizarCatalogo(RAIZ + "/Login/logOut", frm, function (data) {
                    if (data.estado) {
                        location.reload();
                    }
                })
            })
            $(document).on("click", ".desplegableMobile", function () {
                ulMenu = $(this).parents(".mobileButton").parent().find(".ulMenu");
                if (ulMenu.is(':visible')) {
                    console.log("visible")
                    ulMenu.hide();
                } else {
                    console.log("no visible")
                    ulMenu.show();
                }
            });
        // change
            
            $(document).on("change", ".cbIdioma", function () {
                idIdioma = $(this).val();
                console.log("entro");
                if (idIdioma != -1 && idIdioma > 0) {
                    cbIdioma(idIdioma);
                }
            })
});