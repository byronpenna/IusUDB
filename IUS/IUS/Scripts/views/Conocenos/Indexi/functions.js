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
    function getHTMLTablaDocumentos(){
        var tabla = "\
        <table class='table tbDocumentos'>\
            <thead>\
                <tr>\
                    <th class='thHead'>Nombre</td>\
                    <th class='thHead'>Acciones</td>\
                </tr>\
            </thead>\
            <tbody>\
                @tbody\
            </tbody>\
        </table>";
        return tabla;
    }
    function getTrDocumento(documentoOficial) {
        var tr = "\
        <tr>\
            <td>"+ documentoOficial._traduccion + "</td>\
            <td class='tdAccion'>\
                <div class='btn-group'>\
                    <a href='" + RAIZ + "Conocenos/documento/" + documentoOficial._idVersion + "' target='_blank' class='btn btnAccionDoc btnIUS'>Descargar</a>\
                    <a href='" + RAIZ + "Conocenos/documento/" + documentoOficial._idVersion + "/2' target='_blank' class='btn btnAccionDoc btnIUS'>Ver</a>\
                </div>\
            </td>\
        </tr>\
        ";
        return tr;
    }
    function divDocumentos() {
        console.log("Div documentos");
        var tabla = getHTMLTablaDocumentos();
        
        $(".imgPrincipal").attr("src", "/Content/images/generales/conocenos/12.png");
        actualizarCatalogo(RAIZ + "Conocenos/getDocumentosByIdioma", frm, function (data) {
            console.log("Data es: ", data);
            var tr = "";
            if (data.estado) {
                if (data.documentosOficiales !== undefined && data.documentosOficiales != null && data.documentosOficiales.length > 0) {
                    $.each(data.documentosOficiales, function (i, documentoOficial) {
                        tr += getTrDocumento(documentoOficial);
                    })
                    console.log("tr es: ", tr);
                    tabla = tabla.replace("@tbody", tr);
                    console.log("aqui");
                    console.log("tabla es:", tabla);
                    $(".divCuerpoConocenos").empty().append(tabla);
                }
            }
        })
        
    }
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