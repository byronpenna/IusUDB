// genericas 
        function getTrEducativo(formacion) {
            var tr = "\
                <tr>\
                    <td>"+ formacion._carrera + "</td>\
                    <td>"+ formacion._institucion + "</td>\
                    <td>" + formacion._nivelTitulo._nombreNivel + "</td>\
                    <td>"+ formacion._observaciones + "</td>\
                </tr>\
            ";
            return tr;
        }
        function getTrLaboral(laboral) {
            var tr = "\
            <tr>\
                <td>" + laboral._empresa._nombre + "</td>\
                <td>" + laboral._cargo._cargo + "</td>\
                <td> ^^' nose como rayos llenar aqui xD </td>\
            </tr>\
            ";
            return tr;
        }
    // tr personas
        function getTrPersonas(persona) {
            var tr = "\
                <tr>\
                    <td class='hidden'>\
                        <input type='text' name='txtHdIdPersona' class='txtHdIdPersona' value='" + persona._idPersona + "'>\
                    </td>\
                    <td>" + persona.nombreCompleto + "</td>\
                    <td>\
                        <button class='btn btnVerFicha'>Ver ficha</button>\
                    </td>\
                </tr>";
            return tr;
        }
        function getTrPersonasNull(loadIcon) {
            var strContenido = "";
            if (loadIcon === undefined || !loadIcon) {
                strContenido = "No se encontraron personas";
            } else {
                strContenido = "<img src='" + IMG_GENERALES + "ajax-loader.gif" + "'";
            }
            var tr = "\
                <tr>\
                    <td colspan='2' class='text-center'>"+strContenido+"</td>\
                </tr>\
            ";
            return tr;
        }
// acciones script 
    function btnVerFicha(frm) {
        // targets
        var targetLaboral = $(".tbodyEmpresa"); var targetEducacion = $(".tbodyEducacion");
        actualizarCatalogo(RAIZ + "/RecursosHumanos/sp_rrhh_detallePesona", frm, function (data) {
            console.log("respuesta de servidor", data);
            var trEducativo = ""; var trLaboral = "";
            if (data.estado) {
                if (data.formaciones !== undefined && data.formaciones != null && data.formaciones.length > 0) {
                    $.each(data.formaciones, function (i,formacion) {
                        trEducativo += getTrEducativo(formacion)
                    })
                }
                if (data.laborales !== undefined && data.laborales != null && data.laborales.length > 0) {
                    $.each(data.laborales, function (i, laboral) {
                        trLaboral += getTrLaboral(laboral);
                    })
                }
                if (data.persona !== undefined && data.laborales != null && data.laborales.length > 0) {
                    $(".hNombreCompleto").empty().append(data.persona.nombreCompleto);
                    $(".spanSexo").empty().append(data._sexo._sexo);
                    $(".spanEdad").empty().append(data.getEdad);
                    $(".spanNombrePais").empty().append();
                    
                    
                }
                console.log(trLaboral);
                targetLaboral.empty().append(trLaboral);
                targetEducacion.empty().append(trEducativo);
            }
        }, function () {
            $(".spanEncabezadoFicha").empty().append("..");

        })
    }
    function btnBusquedaPerfil(frm) {
        var target = $(".tbodyPersonas");
        actualizarCatalogo(RAIZ + "/RecursosHumanos/sp_rrhh_buscarPersonas", frm, function (data) {
            console.log("La data devuelta es", data);
            if (data.estado) {
                var tr = getTrPersonasNull();
                if (data.personas !== undefined && data.personas != null && data.personas.length > 0) {
                    console.log("entro aqui");
                    tr = "";
                    $.each(data.personas, function (i,persona) {
                        tr += getTrPersonas(persona);
                    })
                } else {
                    console.log("D: no entro");
                }
                target.empty().append(tr);
            } else {
                alert("Ocurrio un error");
            }
        }, function () {
            var tr = getTrPersonasNull(true);
            target.empty().append(tr);
        })
    }