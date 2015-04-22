$(document).ready(function () {
    // plugins 
        // datePicker
            $(".dpFecha").datepicker({
                dateFormat: "dd/mm/yy"
            });
        // full calendar
            $("#calendar").fullCalendar({
                editable: true,
                header: {
                    left: 'prev,next today',
                    right: 'month,agendaWeek,agendaDay'
                }
            });
        // tabs
            $('#horizontalTab').responsiveTabs();
    // funciones iniciales 
        eventosIniciales();
    // eventos 
        $(document).on("submit", "#frmAgregarEvento", function (e) {
            e.preventDefault();
            frm = serializeForm($(this));
            var x = confirm("¿Esta seguro que desea agregar este evento?");
            if (x) {
                frmAgregarEvento(frm,$(this));
            }
        });
    

});