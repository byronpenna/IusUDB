$(document).ready(function () {
    // plugins 
        // acordion    
            $("#accordion").accordion({
                collapsible: true,
                active:false
            });
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
            $("#tabCompartir").responsiveTabs();
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
        // publicar o no website
            $(document).on("click", ".btnAccionQuitarPublicacion", function () {
                detalle = $(this).parents(".detalleEvento");
                var x = confirm("¿Esta totalmente seguro de hacer esto?");
                if (x) {
                    btnAccionQuitarPublicacion(detalle);
                }
            })
            $(document).on("click", ".btnCancelaQuitarPublicacion", function () {
                detalle = $(this).parents(".detalleEvento");
                btnCancelaQuitarPublicacion(detalle)
            })
            $(document).on("click", ".btnPublicar", function () {
                detalle = $(this).parents(".detalleEvento");
                var estado = parseInt(detalle.find(".txtHdEstadoEstado").val());
                if (estado == 1) {
                    mjs = "quitar publicacion de";
                } else {
                    mjs = "publicar en";
                } 
                var x = confirm("¿Esta seguro que desea "+mjs+" website?");
                if (x == true && estado == 0) {
                    console.log("quiso publicar");
                    btnPublicar(detalle);
                } else if (x == true && estado == 1) {
                    console.log("quiso quitar publicacion");
                    btnQuitarPublicacion(detalle);
                }
            });
        // edicion 
            $(document).on("click", ".btnEditar", function () {
                div = $(this).parents(".detalleEvento");
                btnEditar(div);
            });
            $(document).on("click", ".btnCancelar", function () {
                div = $(this).parents(".detalleEvento");
                controlesEdit(false, div);
            });
        

});