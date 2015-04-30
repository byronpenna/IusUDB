$(document).ready(function () {
    // plugins 
        // chosen 
            $(".cbUsuarioCompartir").chosen({
                no_results_text: "Usuario no encontrado",
                width: '80%'
            });
            $(".cbPermisosCompartir").chosen({
                no_results_text: "No se a encontrado permiso",
                width: '80%'
            })
        // acordion    
            $("#accordion").accordion({
                collapsible: true,
                active: false,
                beforeActivate: function (e, ui) {
                    if (e.originalEvent.type != "click") {
                        e.preventDefault();
                    }
                    if (!(e.toElement === undefined)) {
                        if (e.toElement.className == "txtEvento2") {
                            e.preventDefault();
                        }
                    }
                }
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
        // submit
            $(document).on("submit", "#frmAgregarEvento", function (e) {
            e.preventDefault();
            frm = serializeForm($(this));
            var x = confirm("¿Esta seguro que desea agregar este evento?");
            if (x) {
                frmAgregarEvento(frm, $(this));
                
            }
            });
        // click
                $(document).on("click", ".tbCompartir", function () {
                    $("#accordion").accordion("refresh");
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
                $(document).on("click", ".btnActualizar", function () {
                    div = $(this).parents(".detalleEvento");
                    btnActualizar(div);
                });
                $(document).on("click", ".btnEditar", function () {
                    div = $(this).parents(".detalleEvento");
                    btnEditar(div);
                });
                $(document).on("click", ".btnCancelar", function () {
                    div = $(this).parents(".detalleEvento");
                    controlesEdit(false, div);
                });
            // compartir 
                $(document).on("click", ".btnCompartir", function () {
                    detalle = $(this).parents(".detalleEvento");
                    btnCompartir(detalle);
                    
                });
                $(document).on("click", ".btnAgregarUsuarioCompartir", function () {
                    divFrm = $(this).parents(".frmCompartirUsuario");
                    btnAgregarUsuarioCompartir(divFrm);
                });
                $(document).on("click", ".icoQuitarUsuario", function (e) {
                    e.stopPropagation();
                    var x = confirm("¿Esta seguro que desea dejar de compartir?");
                    tr = $(this).parents("tr");
                    if (x) {
                        icoQuitarUsuario(tr);
                    }
                });
                $(document).on("click", ".btnPermisos", function () {
                    btnPermisos();
                });
                // tablas compartir
                $(document).on("click", ".icoEliminarPermisoEvento", function () {
                    var x = confirm("¿Esta seguro que desea quitarle este permiso?");
                    tr = $(this).parents("tr");
                    if (x) {
                        icoEliminarPermisoEvento(tr);
                    }
                })
                $(document).on("click", ".trUsuarioCompartido", function () {
                    tr = $(this);
                    trUsuarioCompartido(tr);
                });
});