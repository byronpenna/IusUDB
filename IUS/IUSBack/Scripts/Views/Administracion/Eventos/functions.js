function eventosIniciales() {
    var frm = new Object();
    actualizarCatalogo("/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
        if (data.estado) {
            eventos = data.eventos;
            $.each(eventos, function (i, evento) {
                agregarEvento($("#calendar"), evento, true);
            });
        } else {
            console.log("ocurrio un error inesperado");
        }
    });
}
// genericos
    function agregarEvento(calendar,evento,sticker) {
        eventoAgregar = {
            title: evento._evento,
            start: evento.getFechaInicioUSA,
            end: evento.getFechaFinUSA 
        };
        console.log("por lo menos intento agregarlo");
        calendar.fullCalendar('renderEvent', eventoAgregar, true);
    }
    function llenarInputsEdicion(evento,div) {
        console.log("El evento es:", evento);
        // tanto val como text ya que sino a la hora de llenar el formulario muere todo
        div.find(".txtAreaDescripcion").text(evento._descripcion);
        div.find(".txtFechaInicio").val(evento._fechaInicio);
        div.find(".txtFechaFin").val(evento._fechaFin);
        div.find(".txtHoraFin").val(evento._horaInicio);
    }
    function updateDespuesDePublicacion(eventoWebsite,detalle) {
        detalle.find(".btnPublicar").text(eventoWebsite._evento.txtBtnPublicar);
        if (eventoWebsite._estado == true) {
            intEstado = 1;
        } else {
            intEstado = 0;
        }
        detalle.find(".txtHdEstadoEstado").val(intEstado);
    }
    function editModeEncabezadoEvento() {
        txtEdit = h3.find(".nombreEvento").text();
        h3.find(".editMode").removeClass("hidden");
        h3.find(".normalMode").addClass("hidden");
        h3.find(".txtEvento2").val(txtEdit);
    }
// acciones script
    function btnActualizar(detalle) {
        frm = serializeSection(detalle);
        h3 = detalle.prev();
        frm.txtEvento = h3.find(".txtEvento2").val();
        console.log("Formulario a enviar es:", frm);
        if (frm.txtHoraInicio != "" && frm.txtHoraFin != "") {
            actualizarCatalogo("/Administracion/sp_adminfe_editarEventos", frm, function (data) {

            });
        } else {
            alert("Por favor ponga una hora de inicio y de fin ")
        }
    }
    function btnAccionQuitarPublicacion(detalle) {
        var frm = {
            txtHdIdEvento: detalle.find(".txtHdIdEvento").val(),
            txtAreaMotivoQuitar: detalle.find(".txtAreaMotivoQuitar").val()
        }
        console.log("Formulario a enviar para quitar publicacion:", frm);
        actualizarCatalogo("/Administracion/sp_adminfe_quitarEventoWebsite", frm, function (data) {
            console.log("la data devuelta por el server es: ", data);
            if (data.estado) {
                updateDespuesDePublicacion(data.eventoWebsite, detalle);
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function btnEditar(div) {
        fechaIni = div.find(".spanFechaInicio").text();
        separadorIni = fechaIni.indexOf(" ");
        fechaFin = div.find(".spanFechaFin").text();
        separadorFin = fechaFin.indexOf(" ");
        evento = {
            _descripcion: div.find(".pDescripcionEvento").text(),
            _fechaInicio: fechaIni.substring(0, separadorIni),
            _fechaFin: fechaFin.substring(0, separadorFin),
            _horaInicio: fechaIni.substring( separadorIni,fechaIni.length),
            _horaFin:  fechaFin.substring(separadorFin,fechaFin.length)
        }
        llenarInputsEdicion(evento, div);
        h3 = div.prev();
        editModeEncabezadoEvento(h3);
        controlesEdit(true, div);
        
    }
    function btnPublicar(detalle) {
        frm = { txtHdIdEvento: detalle.find(".txtHdIdEvento").val() }
        actualizarCatalogo("/Administracion/sp_adminfe_publicarEventoWebsite", frm, function (data) {
            console.log("la respuesta del server es: ", data);
            updateDespuesDePublicacion(data.eventoPublicado, detalle);
        });
    }
    // no es boton propiamente dicho pero aun asi se le pone btn 
    function btnQuitarPublicacion(div) {
        controlesEdit(true, div, ".quitarPublicacionMode")
    }//-------------------------------
    function btnCancelaQuitarPublicacion(div) {
        txtArea = div.find(".txtAreaMotivoQuitar");
        txtArea.text("");
        txtArea.val("");
        controlesEdit(false, div, ".quitarPublicacionMode", ".normalMode");
    }
    function frmAgregarEvento(frm,frmSection) {
        actualizarCatalogo("/Administracion/sp_adminfe_crearEvento", frm, function (data) {
            if (data.estado) {
                agregarEvento($("#calendar"), data.evento);
                clearTr(frmSection);
            } else {
                alert("ocurrio un error");
            }
        });
    }