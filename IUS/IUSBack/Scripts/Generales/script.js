$(document).ready(function () {
    $.extend($.expr[':'], {
        'containsi': function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || '').toLowerCase()
            .indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });
    // iniciales
        setInterval(function () {
            txtTiempo = $(".txtHeaderHoraActual").text();
            $(".txtHeaderHoraActual").empty().append(clockHora(txtTiempo));
        }, 1000);
    // eventos
        // click
            $(document).on("click", ".btnTab", function () {
                $(".btnTab").removeClass("tabActive");
                $(this).addClass("tabActive");
                target = $(this).attr("target");
                $(".tab").addClass("hidden");
                $(target).removeClass("hidden");
            })
        // keypress
            $(document).on("keypress", ".soloLetras", function (e) {
                var str = String.fromCharCode(e.which);
                console.log("E which",e.which)
                //console.log("str es D: ", str);
                exp = soloLetras();
                var x;
                if (e.which != 8 && e.which != 0) { // suprimir y delete
                    x = test(exp, str);
                } else {
                    x = true;
                }
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
                        var x;
                        if (e.which != 8 && e.which != 0) { // suprimir y delete
                            x = test(exp, str);
                        } else {
                            x = true;
                        }
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
    // solo numeros
});
