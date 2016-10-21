$(document).ready(function () { 
    // plugins 
        // slide horizontal 
            inputsTime($(".minutos"), $(".horas"));
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
        // datePicker
            $(".dpFecha").datepicker({
                dateFormat: "dd/mm/yy"
            });
        // full calendar
            var calendar = $("#calendar").fullCalendar({
                editable: true,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                }
            });
    // eventos
            $(document).on("click", ".paginador", function () {
                // next page evento
                var elemento = $(this);
                if (!elemento.hasClass("adelante") && !elemento.hasClass("atras")) {
                    var frm = {
                        n: $(".txtHdNum").val(),
                        pagina: elemento.attr("id")
                    }
                    paginador(frm, elemento);
                } else {
                    var grupo;
                    var grupoPaginador = $(".activeGrupoPaginador");
                    if (elemento.hasClass("adelante")) {
                        grupo = grupoPaginador.next();
                    } else if (elemento.hasClass("atras")) {
                        grupo = grupoPaginador.prev();
                    }
                    $(".activeGrupoPaginador").removeClass("activeGrupoPaginador");
                    if (grupo.hasClass("containerPaginador")) {

                    }
                    grupo.addClass("activeGrupoPaginador");
                    if (grupo.hasClass("containerPaginador")) {
                        grupoPaginador.fadeOut("slow", function () {
                            grupo.fadeIn("slow");
                        })
                    }
                    console.log("grupo", grupo);
                    /*var grupoMostrar = grupo.next();
                    grupo.fadeOut("slow", function () {
                        grupoMostrar.fadeIn("slow");
                    })
                    console.log("grupo", grupo);*/
                }
            });

            $(document).on("click", ".tabEventos", function () {
                $(".divBusqueda").fadeOut("slow");
            })
            $
            $(document).on("click", ".btnTab", function () {
                $('#calendar').fullCalendar('rerenderEvents');
                window.history.pushState({}, "", "/" + $(".txtHdNombreClass").val() + "/" + $(".txtHdFuncion").val() + "/" + $(this).attr("id"));
            })
            $(document).on("click", ".tbCompartir", function () {
                //$("#accordion").accordion("refresh");
                $(".divBusqueda").fadeIn("slow");
            });
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
            $(document).on("change", ".cbFiltroTipoEvento", function () {
                var valor = $(this).val();
                console.log("El valor es", valor);
                if (valor != -1) {
                    $(".txtHdTipoEvento").parents(".nombreEvento").addClass("hidden");
                    $(".txtHdTipoEvento[value='" + valor + "']").parents(".nombreEvento").removeClass("hidden");
                } else {
                    $(".txtHdTipoEvento").parents(".nombreEvento").removeClass("hidden");
                }
                console.log("Quiere eliminar");
            })
            $(document).on("change", ".rbTiempo", function () {
                valTiempo = $(this).val();
                rbTiempo(valTiempo,$(this));
            })
        // click
            //
            $(document).on("click", ".vinculoGay", function (e) {
                window.location.href = $(this).attr("href");
            })
            $(document).on("click", ".tabDesplegableEvento", function (e) {
                e.preventDefault();
                e.stopPropagation();
                e.stopImmediatePropagation();
                e.cancelBubble = true;
                console.log("Click a tab event");
            })
            //#######
            $(document).on("click", ".btnEliminarEvento", function () {
                var seccion = $(this).parents(".divEventoi");
                var frm = { idEvento: seccion.find(".txtHdIdEvento").val() }
                console.log("Formulario a enviar aqui", frm);
                var x = confirm("¿Esta seguro que desea eliminar evento?");
                if (x) {
                    btnEliminarEvento(frm, seccion);
                }
                calendar.fullCalendar('removeEvents', frm.idEvento);
            })
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
            
            // publicar o no website
                $(document).on("click", ".btnAccionQuitarPublicacion", function () {
                    detalle = $(this).parents(".divEventoi");
                    var x = confirm("¿Esta totalmente seguro de hacer esto?");
                    if (x) {
                        btnAccionQuitarPublicacion(detalle);
                    }
                })
                $(document).on("click", ".btnCancelaQuitarPublicacion", function () {
                    detalle = $(this).parents(".divEventoi");
                    btnCancelaQuitarPublicacion(detalle)
                })
                $(document).on("click", ".btnPublicar", function () {
                    detalle = $(this).parents(".divEventoi");
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
                    div = $(this).parents(".divEventoi");
                    btnActualizar(div);
                });
                $(document).on("click", ".btnEditar", function () {
                    div = $(this).parents(".divEventoi");
                    btnEditar(div);
                });
                $(document).on("click", ".btnCancelar", function () {
                    div = $(this).parents(".divEventoi");
                    controlesEdit(false, div);
                });
            // compartir 
                $(document).on("click", ".btnCompartir", function () {
                    detalle = $(this).parents(".divEventoi");
                    
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
    // funciones iniciales 
        iniciales();
        eventosIniciales();
});