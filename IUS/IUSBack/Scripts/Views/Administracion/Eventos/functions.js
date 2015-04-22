function eventosIniciales() {
    var frm = new Object();
    actualizarCatalogo("/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
        console.log("Respuesta del server al mandar a traer calendario", data);
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
// acciones script
    function btnPublicar(detalle) {
        frm = { txtHdIdEvento: detalle.find(".txtHdIdEvento").val() }
        actualizarCatalogo("/Administracion/sp_adminfe_crearEvento", frm, function (data) {
        });
    }
    function frmAgregarEvento(frm,frmSection) {
        console.log("Formulario a enviar es: ", frm);
        actualizarCatalogo("/Administracion/sp_adminfe_crearEvento", frm, function (data) {
            console.log("la respuesta del servidor es: ", data);
            if (data.estado) {
                agregarEvento($("#calendar"), data.evento);
                clearTr(frmSection);
            } else {
                alert("ocurrio un error");
            }
        });
    }