// genericas 
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
    function btnEliminarLaboralPersona(frm,tr) {
        //**
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarLaboralPersonas", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }