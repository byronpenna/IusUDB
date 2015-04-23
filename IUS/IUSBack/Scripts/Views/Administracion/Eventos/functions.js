﻿function eventosIniciales() {
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
    /*
    eventos = [{
        title: 'eventoPrueba',
        start: '2015-04-01',
        end: '2015-04-05'
    },
    {
        title: 'Segundo evento',
        start: '2015-04-10 13:00',
        end: '2015-04-15'
    }
    ];
    $.each(eventos, function (i, evento) {
        $('#calendar').fullCalendar('renderEvent', evento, true);
    })*/
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
// acciones script
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
        llenarInputsEdicion(evento,div);
        controlesEdit(true, div);
    }
    function btnPublicar(detalle) {
        frm = { txtHdIdEvento: detalle.find(".txtHdIdEvento").val() }
        actualizarCatalogo("/Administracion/sp_adminfe_publicarEventoWebsite", frm, function (data) {
            console.log("la respuesta del server es: ", data);
        });
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