// genericos 
    function inicial() {
        // click al primero
        var i = $(".txtHdSeleccionadoBuble").val();
        if (i == -1) {
            i = 1;
        }
        i--;
        console.log("El valor de i es:", i);
        $(".divImgCambio")[i].click();
    }
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
        function getDivSalesianos(data) {
            var obj = new Array();
            $.each(data, function (i, val) {
                obj[i] = val;
            })
            //" + obj["Texto-inicial"] + "
            var div = "\
            <div class='row marginNull divContenedorInfoL'>\
                <h2 class='tituloInfoL'>Salesianos</h2>\
                <div class='row marginNull'>\
                    \
                </div>\
                <div class='row marginNull divContenedorInfo'>\
                    <div class='col-lg-6 text-center'>\
                        <h6>" + obj['Titulo'] + "</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-titulo-alternativo']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-fecha-fundacion']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-fundador']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-salesianos']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-paises']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-inspectorias']+"</h6>\
                        <span>0</span>\
                    </div>\
                    <div class='col-lg-6 text-center'>\
                        <h6>"+obj['etiqueta-website']+"</h6>\
                        <span>0</span>\
                    </div>\
                </div>\
            </div>";
            return div;
        }
        function getDivIUS(data) {
            var obj = new Array();
            $.each(data, function (i, val) {
                obj[i] = val;
            })
            var div = "\
            <div class='row marginNull divContenedorInfoL'>\
                <h2 class='tituloInfoL'>IUS</h2>\
                <div class='row marginNull' style='margin-bottom:5%'>\
                    "+obj["Texto-inicial"]+"\
                </div>\
                <div class='row marginNull'>\
                    <!-- Continentes presentes -->\
                    <div class='col-lg-6 tituloLlaves'>\
                        " + obj['etiqueta-continentes-presentes'] + "\
                    </div>\
                    <div class='col-lg-6'>\
                        ____\
                    </div>\
                    <!-- Numero de paises presentes -->\
                    <div class='col-lg-6 tituloLlaves'>\
                        " + obj['etiqueta-paises-presentes'] + "\
                    </div>\
                    <div class='col-lg-6'>\
                        ____\
                    </div>\
                    <!-- Total instituciones -->\
                    <div class='col-lg-6 tituloLlaves'>\
                        " + obj['etiqueta-numero-instituciones'] + "\
                    </div>\
                    <div class='col-lg-6'>\
                        ____\
                    </div>\
                    <!-- Numero de estudiantes -->\
                    <div class='col-lg-6 tituloLlaves'>\
                        " + obj['etiqueta-numero-estudiantes'] + "\
                    </div>\
                    <div class='col-lg-6'>\
                        ____\
                    </div>\
                    <!-- Numero de salesianos -->\
                    <div class='col-lg-6 tituloLlaves'>\
                        " + obj['etiqueta-numero-salesianos'] + "\
                    </div>\
                    <div class='col-lg-6'>\
                        ____\
                    </div>\
                </div>\
            </div>";
            return div;
        }
        function getDivOrganicacion(data) {

        }
// scripts 
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
                    case 4:{
                        div = getDivIUS(data);
                        break;
                    }
                    case 5:{
                        console.log("El id de la pagina es: ", frm.idSeleccion);
                        div = getDivSalesianos(data);
                    }
                    default: {
                        console.log("es default");
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