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
    function btnActualizarLaboralPersona(frm) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_editarLaboralPersonas", frm, function (data) {
            console.log("la respuesta es: ", data);
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