// variables 
    var instArr   = new Array();
    var nTabla    = 10;
// funciones 
function iniciales() {
    var idContinente = $(".txtHdIdContinente").val();
    console.log("Id continente es: ", idContinente);
    if(idContinente != -1)
    {
        console.log("Dio click");
        var strFind = "#" + idContinente;
        console.log("String find es: ", strFind);
        $(".menuLateral").find(strFind).click();
    }
}
function buscarContinente(frm) {
    var target = $(".tablaInstitucion").find("tbody");
    var targetFoot = $(".tablaInstitucion").find("tfoot");
    instArr = new Array();
    actualizarCatalogo(RAIZ + "/Instituciones/sp_frontui_getInstitucionesByContinente", frm, function (data) {
        console.log("D: :DP", data)
        if (data.estado) {
            var instituciones = data.instituciones.instituciones;

            var tr = "";
            console.log("instituciones", instituciones);
            var hContinente = "";
            if (data.instituciones.continente !== undefined && data.instituciones.continente != null) {
                hContinente = data.instituciones.continente._continente;
            }
            if (instituciones !== undefined && instituciones != null && instituciones.length > 0) {
                //console.log("entro aqui");
                var ins = new Array();
                $.each(instituciones, function (i, institucion) {
                    ins.push(institucion);
                    if ((i + 1) % nTabla == 0) {
                        instArr.push(ins);
                        ins = new Array();
                    }
                    if (i < nTabla) {
                        tr += getTrInstitucion(institucion);
                    }
                    
                });
                instArr.push(ins);
                console.log("InstArr es", instArr);
                //cons
            } else {
                tr = getTrInstitucionNull();
            }
            //########################
            var n = instArr.length;
            var tFoot = "<tr>\
                <td colspan='4' class='tdPaginador'>\
            "
            console.log("n es", n);
            var claseActive = "";
            
            for (var i = 0; i < n; i++) {
                if (i == 0)
                {
                    claseActive = "activePaginador";
                } else {
                    claseActive = "";
                }
                tFoot += "<div class='paginador "+claseActive+"' id='"+(i+1)+"'>"+(i+1)+"</div>";
            }
            tFoot += "</td>\
                </tr>\
            ";
            targetFoot.empty();
            if (n > 1) {
                targetFoot.append(tFoot);
            } 

            //##############################
            target.empty().append(tr);
            $(".spanContinente").empty().append(hContinente);
        }

    }, function () {
        var trLoading = "\
            <tr>\
                <td colspan='6' class='text-center'>\
                    <img src='" + IMG_GENERALES + "ajax-loader.gif'>\
                </td>\
            </tr>\
            ";
        target.empty().append(trLoading);
    })
}
    function getTrInstitucion(institucion) {
        var tel = ""; var enlaces = "";
        //console.log("los objetos de instituciones son", institucion);
        var url = "#";
        if (institucion._enlaces !== undefined && institucion._enlaces != null) {
            $.each(institucion._enlaces, function (i, enlace) {
                /*enlaces += "\
                    <div class='row marginNull'>\
                        <a href='"+ enlace._enlace + "'>\
                            " + enlace._nombreEnlace + "\
                        </a>\
                    </div>";*/
                url = enlace._enlace;
                return false;
            })
        }
        if (institucion._telefonos !== undefined && institucion._telefonos != null) {
            $.each(institucion._telefonos, function (i, telelefono) {
                tel += "\
                    <div class='row marginNull divTel'>\
                        <a href='tel:" + telelefono._telefono + "'>\
                            " + telelefono._textoTelefono + "\
                        </a>\
                    </div>";
            })
        } else {
            tel = $(".txtHdNoNumberText").val();
            //"No hay numeros asignados"
        }
        var ciudad="",ciudades=""; 
        if (institucion._ciudad != "") {
            var arr = institucion._ciudad.split(",");
            if (arr !== undefined && arr != null && arr.length > 0) {
                $.each(arr, function (i, val) {
                    if (i == 0) {
                        ciudad = val;
                    }
                    if (i == 1) {
                        ciudades += val;
                    }
                    if (i > 1) {
                        ciudades += "," + val;
                    }
                })
            }
        }
        var tr = "\
            <tr>\
                <td>"+ institucion._pais._pais + " </td>\
                <td class='tdNombre tdLink'>\
                   <a href='" + url + "'> " + institucion._nombre + " </a>\
                </td>\
                <td class='tdNombre'>" + ciudad + "</td>\
                <td>\
                    <a href='" + $(".txtHdUrlFicha").val() + "/" + institucion._idInstitucion + "'>\
                        <img src='" + RAIZ + "/Content/images/views/Instituciones/mas.png'/>\
                    </a>\
                </td>\
            </tr>\
        ";
        /*<td>\
                    <a href='" + RAIZ + "/Instituciones/FichaInstitucion/" + institucion._idInstitucion + "' class='btn btn-default'>Ficha</a>\
                </td>\*/
        /*<div class='col-lg-6 mitadLinks'>\
                    <h4>Telefonos </h4>\
        " + tel + "\
                </div>\
                <div class='col-lg-6 mitadLinks'>\
                    <h4>Medios electronicos </h4>\
        "+enlaces+"\
                </div>\*/
        return tr;
    }
    function getTrInstitucionNull() {
        var tr = "\
                <tr >\
                    <td colspan='6' class='text-center tdNull'>" + $(".txtHdNoRegistro").val() + "</td>\
                </tr>\
            ";
        //No hay instituciones para mostrar
        return tr;
    }