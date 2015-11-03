// genericas 
    function getTrInstitucion(institucion) {
        var tel = ""; var enlaces = "";
        if (institucion._enlaces !== undefined && institucion._enlaces != null) {
            $.each(institucion._enlaces, function (i, enlace) {
                enlaces += "\
                <div class='row marginNull'>\
                    " + enlace._enlace + "\
                </div>";
            })
        }
        if (institucion._telefonos !== undefined && institucion._telefonos != null) {
            $.each(institucion._telefonos, function (i, telelefono) {
                tel += "\
                <div class='row marginNull'>\
                    " + telelefono._telefono + "\
                </div>";
            })
        }
        
        var tr = "\
        <tr>\
            <td>"+institucion._pais._pais+" </td>\
            <td>" + institucion._nombre + "</td>\
            <td>\
                <div class='col-lg-6'>\
                    <h4>Telefonos: </h4>\
                    " + enlaces + "\
                </div>\
                <div class='col-lg-6'>\
                    <h4>Medios electronicos: </h4>\
                    "+enlaces+"\
                </div>\
            </td>\
        </tr>\
        ";
        return tr;
    }
// scripts 
    function buscarContinente(frm) {
        var target = $(".tbodyInstitucion");
        actualizarCatalogo(RAIZ + "/Instituciones/sp_frontui_getInstitucionesByContinente", frm, function (data) {
            console.log("D: :DP", data)
            if (data.estado) {
                var instituciones = data.instituciones.instituciones;
                
                var tr = "";
                console.log("instituciones", instituciones);
                if(instituciones !== undefined && instituciones != null && instituciones.length > 0){
                    console.log("entro aqui");
                    $.each(instituciones, function (i,institucion) {
                        tr += getTrInstitucion(institucion);
                    });
                }
                
                //target.empty().append(tr);
            }
            
        }, function () {
            var trLoading = "\
            <tr>\
                <td colspan='3' class='text-center'>\
                    <img src='" + IMG_GENERALES + "ajax-loader.gif'>\
                </td>\
            </tr>\
            ";
            target.empty().append(trLoading);
        })
    }