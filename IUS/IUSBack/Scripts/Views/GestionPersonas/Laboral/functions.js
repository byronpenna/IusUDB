// genericas 
    // texto 
        function getCbCargos(cargo) {
            var cb = "<option value='"+cargo._idCargoEmpresa+"'>" + cargo._cargo + "</option>";
            return cb;
        }
        function getCbEmpresas(empresa)
        {
            var cb = "<option value='"+empresa._idEmpresa+"'>"+empresa._nombre+"</option>";
            return cb;
        }
    function getObjetoSetEditLaboral(tr) {
        var datosSet = new Object();
        // recolectando datos
            datosSet.fechaInicio = $.trim(tr.find(".tdFechaInicio").text());
            datosSet.fechaFin = $.trim(tr.find(".tdFechaFin").text());
            datosSet.idEmpresa = tr.find(".txtHdIdEmpresa").val();
            datosSet.idCargoEmpresa = tr.find(".txtHdIdCargoEmpresa").val();
            datosSet.observaciones = $.trim(tr.find(".tdObservaciones").text());
        return datosSet;
    }
// acciones 
    function btnAgregarLaboralPersona(frm) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertLaboralPersonas", frm, function (data) {
            console.log(data);
        })
    }
    function btnActualizarLaboralPersona(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_editarLaboralPersonas", frm, function (data) {
            console.log("la respuesta es: ", data);
            if (data.estado) {
                var laboral = data.laboralEditado;
                // texto
                    tr.find(".tdFechaInicio").empty().append(laboral._inicio);
                    tr.find(".tdFechaFin").empty().append(laboral._fin);
                    tr.find(".tdObservaciones").empty().append(laboral._observaciones)
                    tr.find(".tdCargo").empty().append(laboral._cargo._cargo)
                    tr.find(".tdNombreEmpresa").empty().append(laboral._empresa._nombre);
                // hiddens 
                    tr.find(".txtHdIdCargoEmpresa").val(laboral._cargo._idCargoEmpresa);
                    tr.find(".txtHdIdEmpresa").val(laboral._empresa._idEmpresa);
                controlesEdit(false, tr);
            } else {
                alert("Ocurrio un error tratand de actualizar");
            }
        })
    }
    function btnEliminarLaboralPersona(frm, tr) {
        //**
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarLaboralPersonas", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }