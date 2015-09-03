﻿$(document).ready(function () {
    // plugins 
        // slide horizontal 
            $(".horas").slider({
                orientation: "horizontal",
                range: "min",
                max: 12,
                min:1,
                value: 1,
                slide: refreshTime,
                change: refreshTime
            });
            $(".minutos").slider({
                orientation: "horizontal",
                range: "min",
                max: 59,
                min: 0,
                value: 0,
                slide: refreshTime,
                change: refreshTime
            });
            /*
            $(".seg").slider({
                orientation: "horizontal",
                range: "min",
                max: 59,
                min: 0,
                value: 0,
                slide: refreshTime,
                change: refreshTime
            });
            $(".tiempo").slider({
                orientation: "horizontal",
                range: "min",
                max: 1,
                min: 0,
                value: 0,
                slide: refreshTime,
                change: refreshTime
            })*/
            //$(".horas").slider("value", 1);
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
            getAccordion($("#accordion"));
            /*$("#accordion").accordion({
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
            });*/
        // datePicker
            $(".dpFecha").datepicker({
                dateFormat: "dd/mm/yy"
            });
            
        // full calendar
            $("#calendar").fullCalendar({
                editable: true,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                }
            });
    // funciones iniciales 
        eventosIniciales();
    // eventos 
        // keypress    
            $(document).on("keyup", ".txtBuscarEventoNombre", function (e) {
                var charCode = e.which;
                if (charCode == 27) { // tecla esc cancela todo
                    $(this).val("");
                }
                var txtBusqueda = $(this).val();
                console.log("Texto a buscar es",txtBusqueda);
                buscarEvento(txtBusqueda);
            })
        // submit
            $(document).on("submit", "#frmAgregarEvento", function (e) {
                e.preventDefault();
                frm = serializeForm($(this));
                var val = valIngresoEvento(frm);
                console.log(val);
                if (val.estado) {
                    $("#frmAgregarEvento .divResultadoMessage").addClass("hidden");
                    var x = confirm("¿Esta seguro que desea agregar este evento?");
                    if (x) {
                        frmAgregarEvento(frm, $(this));
                    }
                } else {
                    var errores;
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        var divResultado = $("#frmAgregarEvento").find("." + i).parents("td").find(".divResultado")
                        if (val.length > 0) {
                            console.log("entro");
                            divResultado.removeClass("hidden");
                            $.each(val, function (i, val) {
                                errores += getSpanMessageError(val);
                            })
                            divResultado.empty().append(errores);
                        }
                    })
                    var div = "";
                    $.each(val.general, function (i, val) {
                        div += getSpanMessageError(val);

                    })
                    //console.log("div malo", div);
                    $(".divResultadoGlobal").removeClass("hidden");
                    $(".divResultadoGlobal").empty().append(div);
                }
                
            });
        // change
            $(document).on("change", ".rbTiempo", function () {
                valTiempo = $(this).val();
                rbTiempo(valTiempo,$(this));
            })
        // click
            //*****************************
            $(document).on("click", ".btnBuscarRangoFecha", function () {
                var frm = serializeSection($(this).parents(".divBusquedaRangoFecha"));
                var op = $(".txtHdBuscando").val();
                if (op == 0) {
                    // no esta buscando
                    prevBtnBuscarRangoFecha(frm);
                } else {
                    // esta buscando 
                    getEventosPrincipales();
                }
                
            })
            //--------------------
            $(document).on("click", ".rbNombre", function () {
                var id = $(this).val();
                $(".controlBusqueda").addClass("hidden");
                $(".controlesBusqueda").find("#" + id).removeClass("hidden");
            })
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
                    var divFrm      = $(this).parents(".frmCompartirUsuario");
                    var frm         = serializeSection(divFrm);
                    frm.idEvento = $(".areaCompartir").find(".txtHdIdEvento").val();
                    //console.log(frm);
                    var val = validarIngresoUsuario(frm);
                    console.log("val es", val);
                    if (val.estado) {
                        btnAgregarUsuarioCompartir(frm);
                    } else {
                        var div = "";
                        $.each(val.general, function (i, val) {
                            div += "<div class='row marginNull'>";
                            div += getSpanMessageError(val);
                            div += "</div>";
                        })
                        console.log(div);
                        printMessageDiv($(".divResultadoAgregarUsuario"), div);
                    }
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
                    var frm = { idPermisos: $(".cbPermisosCompartir").val(), idUsuarioEvento: $(".trUsuarioCompartido.clickTr").find(".txtHdIdUsuarioEvento").val() }
                    console.log("Formulario a enviar", frm);
                    var val = validarIngresoPermiso(frm);
                    if (val.estado) {
                        btnPermisos(frm);
                    } else {
                        var errores;
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = $(".seccionCompartir").find("." + i).parents("div").find(".divResultado")
                            if (val.length > 0) {
                                console.log("entro");
                                divResultado.removeClass("hidden");
                                $.each(val, function (i, val) {
                                    errores += getSpanMessageError(val);
                                })
                                divResultado.empty().append(errores);
                            }
                        })
                        var div = "";
                        $.each(val.general, function (i, val) {
                            div += "<div class='row marginNull'>";
                                div += getSpanMessageError(val);
                            div += "</div>";
                        })
                        printMessageDiv($(".divMensajesCompartirGenerales"), div);
                    }
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