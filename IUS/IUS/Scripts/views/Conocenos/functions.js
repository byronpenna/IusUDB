
// genericos 
    // div informacion 
        function getDivHistoria(data) {
            
            var div = "\
                <h2 class='tituloInfoL'>" + data.Titulo + "</h2>\
                <div class='contenedorInfoL'>\
                    "+data.Cuerpo+"\
                </div>\
            ";
            return div;
        }
        function getDivIdentidad(data) {

        }
        function getDivOrganicacion(data) {

        }
// scripts 
    function divImgCambio(frm) {
        var target = $(".divContenedorInfoL");
        actualizarCatalogo(RAIZ + "Conocenos/getInfo", frm, function (data) {
            console.log("la respuesta del servidor es", data);
            if (data.estado) {
                var div = "";
                console.log(frm.idSeleccion);
                switch (frm.idSeleccion) {
                    case 1: {
                        
                        div = getDivHistoria(data);
                        break;
                    }
                    case 2: {
                        
                        div = getDivHistoria(data);
                        break;
                    }
                    case 3: {
                        
                        div = getDivHistoria(data);
                        break;
                    }
                    default: {
                        console.log("es defaul");
                    }
                }
                //console.log(div);
                target.empty().append(div);
            }
        }, function () {
            var div = "\
            <div class='row marginNull divImageLoadingL'>\
                <img class='imageLoadingL' src='"+ RAIZ + "Content/images/generales/ajax-loader.gif'>\
            </div>\
            ";
            target.empty().append(div);
        });
    }