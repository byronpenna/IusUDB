function eventosIniciales() {
    /*actualizarCatalogo("Administracion/actualizarUsuario", frm, function (data) {

    });*/
    eventos = [{
        title: 'eventoPrueba',
        start: '2015-04-01',
        end: '2015-04-05'
    },
    {
        title: 'Segundo evento',
        start: '2015-04-10',
        end: '2015-04-15'
    }
    ];
    $.each(eventos, function (i, evento) {
        $('#calendar').fullCalendar('renderEvent', evento, true);
    })    
}
// acciones script
function frmAgregarEvento(frm) {
    console.log("Formulario a enviar es: ", frm);


}