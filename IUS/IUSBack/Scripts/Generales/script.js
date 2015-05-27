$(document).ready(function () {
    $(document).on("keypress", ".soloLetras", function (e) {
        var str = String.fromCharCode(e.which);
        exp = soloLetras();
        var x = test(exp, str);
        if (!x) {
            e.preventDefault();
        }
    });
    $(document).on("keypress", ".soloNumerosDecimal", function (e) {
        var str = String.fromCharCode(e.which);
        if (!($(this).val().length == 0 && str == ".")) {
            console.log($(this).val().indexOf("."));
            if ( !($(this).val().indexOf(".") > 0 && str == ".") ) {
                exp = soloNumeros();
                var x = test(exp, str);
                console.log("valor de x", x);
                if (!x) {
                    e.preventDefault();
                }
            } else {
                e.preventDefault();
            }
        } else {
            e.preventDefault();
        }
    })
    $("#menuLogOut").attr("href", RAIZ + "Login/LogOut");
    console.log("se ejecuto");
    // solo numeros
});
