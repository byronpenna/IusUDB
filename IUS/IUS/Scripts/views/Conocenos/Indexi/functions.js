    function inicial() {
        // click al primero
        var i = $(".txtHdSeleccionadoBuble").val();
        console.log("El valor de i es:", i);
        if (i != -1) {
            $(".menuConocenos #" + i + "").click();
        } else {
            $(".menuConocenos li")[0].click();
        }
    }
// #############
    function divImgCambio(frm) {
        var target = $(".divContenedorInfoL");
        if (frm.idSeleccion == 1) {
            frm.idSeleccion = 2;
        } else if (frm.idSeleccion == 2) {
            frm.idSeleccion = 1;
        }
        actualizarCatalogo(RAIZ + "Conocenos/getInfo", frm, function (data) {
            console.log("la respuesta del servidor para la informacion es: ", data);
            if (data.estado) {
                $(".tituloContenidoConocenos").empty().append(data.Titulo);
                $(".divCuerpoConocenos").empty().append(data.Cuerpo);
                $(".imgPrincipal").attr("src", data.urlImage+frm.idSeleccion+".png");
            }
        }, function () {
            var div = "\
                <div class='row marginNull divImageLoadingL'>\
                    <img class='imageLoadingL' src='"+ RAIZ + "Content/images/generales/ajax-loader.gif'>\
                </div>\
                ";
            //target.empty().append(div);
        });
    }