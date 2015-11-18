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
        // moveover
            $(document).on("mouseover", ".cuadritoArbol", function () {
                var chevron = $(this).find(".navChevron");
                var divChevron = $(this).find(".divChevron");
                chevron.removeClass("fa-chevron-right");
                chevron.addClass("fa-chevron-down");
                //divChevron.css("border", "1px solid black");

            })
            $(document).on("mouseleave", ".cuadritoArbol", function () {
                var chevron = $(this).find(".navChevron");
                chevron.removeClass("fa-chevron-down");
                chevron.addClass("fa-chevron-right" );
            })
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
            $(document).on("keypress", ".soloNumerosInt", function (e) {
                var str = String.fromCharCode(e.which);
                if (!($(this).val().length == 0 && str == ".")) {
                    console.log("busqueda de punto", $(this).val().indexOf("."));
                    if (!($(this).val().indexOf(".") > 0 && str == ".")) {
                        exp = soloNumerosInt();
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
            $(document).on("keypress", ".soloNumerosDecimal", function (e) {
                var str = String.fromCharCode(e.which);
                console.log("e which", e.which);
                console.log("str es ", str);
                if (!($(this).val().length == 0 && str == ".")) {
                    console.log("busqueda de punto",$(this).val().indexOf("."));
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
