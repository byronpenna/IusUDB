// load
function eventosIniciales() {
    var frm = new Object();
    actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
        console.log("respuesta sp_adminfe_getEventosPrincipales: ", data);
        if (data.estado) {
            eventos = data.eventos;
            if (!(eventos === null)) {
                $.each(eventos, function (i, evento) {
                    agregarEvento($("#calendar"), evento, true, evento._propietario);
                });
            }
        } else {
        }
    });
}
// genericos
    function getAccordion(div, withRefresh) {
        if (withRefresh !== undefined && withRefresh == true) {
            div.accordion("destroy");
        }
        div.accordion({
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
    }
    function getEventosPrincipales() {
        var frm = new Object();
        actualizarCatalogo(RAIZ + "/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
            if (data.estado) {
                if (data.eventos !== undefined && data.eventos !== null && data.eventos.length > 0) {
                    var divEventos = "";
                    $.each(data.eventos, function (i, evento) {
                        divEventos += getDivEvento(evento);
                    })
                    $("#accordion").empty().append(divEventos);
                    getAccordion($("#accordion"), true);
                    // Poniendolo en modo busqueda
                    $(".txtHdBuscando").val("0");
                    $(".btnBuscarRangoFecha").empty().append("Buscar");
                }
            }
        })
    }
    // busqueda 
        function getDivEvento(evento) {
            var div = "<h3 class='tabDesplegableEvento nombreEvento "+evento.txtClaseColor+" '>\
                            <span class='spanNombreEvento'>"+evento._evento+"</span>\
                        </h3>\
                        <div class='tabDesplegableEvento detalleEvento'>\
                            <input type='hidden' name='txtHdIdEvento' class='txtHdIdEvento' value='"+evento._idEvento+"' />\
                            <input type='hidden' name='txtHdEstadoEvento' class='txtHdEstadoEstado' value='"+evento._publicado+"' />\
                            <div class='row'>\
                                <div class='normalMode'>\
                                    <p class='text-justify pDescripcionEvento'>"+evento._descripcion+"</p>\
                                </div>\
                                <div class='editMode hidden' style='margin-bottom:5%;'>\
                                    <label>Nombre de evento</label>\
                                    <input type='text' name='txtEvento2' class='txtEvento2' />\
                                </div>\
                                <div class='editMode hidden'>\
                                    <div class='editMode hidden'>\
                                        <label>Descripcion evento</label>\
                                    </div>\
                                    <textarea class='txtAreaDescripcion form-control' name='txtAreaDescripcion'></textarea>\
                                </div>\
                                <div class='quitarPublicacionMode hidden'>\
                                    <label class='text-center'>Motivos para quitar del website</label>\
                                    <textarea class='txtAreaMotivoQuitar form-control' name='txtAreaMotivoQuitar'></textarea>\
                                </div>\
                                <hr />\
                                <div class='row text-center'>\
                                    <div class='col-lg-6'>\
                                        <div class='normalMode'>\
                                            <label>Inicio: </label>\
                                            <span class='spanFechaInicio'>"+evento.getFechaInicio+"</span>\
                                        </div>\
                                        <div class='editMode hidden'>\
                                            <label>Inicio: </label>\
                                            <input class='dpFecha txtFechaInicio form-control' name='txtFechaInicio' />\
                                            <input type='time' class='txtHoraInicio form-control' name='txtHoraInicio' />\
                                        </div>\
                                    </div>\
                                    <div class='col-lg-6'>\
                                        <div class='normalMode'>\
                                            <label>Fin: </label>\
                                            <span class='spanFechaFin'>"+evento.getFechaFin+"</span>\
                                        </div>\
                                        <div class='editMode hidden'>\
                                            <label>Fin: </label>\
                                            <input class='dpFecha txtFechaFin form-control' name='txtFechaFin' />\
                                            <input type='time' class='txtHoraFin form-control ' name='txtHoraFin' />\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>";
            if (evento._propietario != 3)
            {
                div += "\
                    <div class='row text-center sectionBotonesEvento'>\
                        <div class='normalMode'>";
                            if (evento._propietario == 1 || evento._propietario == 2)
                            {
                                div += "<button class='btn btnEditar'>Editar</button>\
                                <button class='btn btnPublicar'>"+evento.txtBtnPublicar+"</button>";
                            }
                            if (evento._propietario == 1)
                            {
                                div += "<button class='btn btnCompartir'>Compartir</button>";
                            }
                    div += "\
                        </div>\
                        <div class='editMode hidden'>\
                            <button class='btn btnActualizar'>Actualizar</button>\
                            <button class='btn btnCancelar'>Cancelar</button>\
                        </div>\
                        <div class='quitarPublicacionMode hidden'>\
                            <button class='btn btnAccionQuitarPublicacion'>Quitar publicacion</button>\
                            <button class='btn btnCancelaQuitarPublicacion'>Cancelar</button>\
                        </div>\
                    </div>";
            }
            div += "\
            </div>";
            return div;

        }
        function prevBtnBuscarRangoFecha(frm) {
            var val = valBusquedaRangoFechas(frm);
            console.log("Val es", val);
            $(".divBusquedaRangoFecha").find(".divResultado").addClass("visibilitiHidden");
            $(".divBusquedaRangoFecha").find(".divResultado").removeClass("hidden");
            if (val.estado) {
                btnBuscarRangoFecha(frm);
            } else {
                var errores;
                $.each(val.campos, function (i, val) {
                    errores = "";
                    var divResultado = $(".divBusquedaRangoFecha").find("." + i).parents(".divControl").find(".divResultado")
                    if (val.length > 0) {
                        divResultado.removeClass("visibilitiHidden");
                        $.each(val, function (i, val) {
                            errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                        })
                        divResultado.empty().append(errores);
                    }
                })
                if (val.general.length > 0) {
                    var div = "";
                    $.each(val.general, function (i, val) {
                        div += "<div class='row marginNull'>";
                        div += getSpanMessageError(val);
                        div += "</div>";
                    })
                    printMessageDiv($(".divResultadoGeneralBusqueda"), div);
                }
            }
        }
        function btnBuscarRangoFecha(frm) {
            actualizarCatalogo(RAIZ + "/Administracion/sp_adminfe_buscarAllEventosPersonalesByDate", frm, function (data) {
                console.log("Respuesta del servidor es:", data);
                if (data.estado) {
                    if (data.eventos !== undefined && data.eventos !== null && data.eventos.length > 0)
                    {
                        //console.log("-_-");
                        var divEventos = "";
                        $.each(data.eventos, function (i,evento) {
                            divEventos += getDivEvento(evento);
                        })
                        $("#accordion").empty().append(divEventos);
                        getAccordion($("#accordion"), true);
                        // Poniendolo en modo busqueda
                        $(".txtHdBuscando").val("1");
                        $(".btnBuscarRangoFecha").empty().append("x");
                    }
                }
            })
        }

        function buscarEvento(txt) {
            var eventoTitulo = $("#accordion .nombreEvento");
            var eventoCuerpo = $("#accordion .detalleEvento");
            eventoCuerpo.css("display","none");
            if (txt == "") {
                eventoTitulo.removeClass("hidden");
            } else {
                console.log("Llego hasta aqui");
                eventoTitulo.addClass("hidden");
                var escogidos = eventoTitulo.find(".spanNombreEvento:containsi(" + txt + ")");
                escogidos = escogidos.parents(".nombreEvento");
                escogidos.removeClass("hidden");
            }
        }
    // validaciones
        function valIngresoEvento(frm) {
            var estado = false;
            var val = new Object();
            val.campos = {
                tiempoFin:          new Array(),
                tiempoInicio:       new Array(),
                txtAreaDescripcion: new Array(),
                txtEvento:          new Array(),
                txtFechaFin:        new Array(),
                txtFechaInicio:     new Array(),
                txtHoraFin:         new Array(),
                txtHoraInicio:      new Array()
            }
            val.general = new Array();
            var dateInicio  = getDateFromString(frm.txtFechaInicio, "dd/mm/yyyy", frm.txtHoraInicio, "hh:mm:ss");
            var dateFin     = getDateFromString(frm.txtFechaFin, "dd/mm/yyyy", frm.txtHoraFin, "hh:mm:ss");
            
            if ((dateFin - dateInicio) < 0) {
                val.general.push("La fecha de fin debe ser mayor que la de inicio");
            }
            val.estado = objArrIsEmpty(val.campos);
            var estado = false;
            if (val.general.length == 0) {
                 estado = true;
            }
            if (estado && val.estado) {
                val.estado = true;
            } else {
                val.estado = false;
            }
            //console.log(dateInicio.getHours());
            return val;
        }
        function valBusquedaRangoFechas(frm) {
            var val = new Object(); var estado1 = false; var estado2 = false;
            val.campos = {
                txtDeFechaBusqueda: new Array(),
                txtHastaFecha:      new Array()
            }
            val.general = new Array();
            if (frm.txtHastaFecha === undefined || frm.txtHastaFecha == "") {
                val.campos.txtHastaFecha.push("Este campo no puede quedar vacio");
            } else {
                estado1 = true;
                if (!FORMATO_FECHA.test(frm.txtHastaFecha)) {
                    val.campos.txtHastaFecha.push("Campo debe ser rellenado con formato dd/mm/yyyy");
                }
            }
            if (frm.txtDeFechaBusqueda === undefined || frm.txtDeFechaBusqueda == "") {
                val.campos.txtDeFechaBusqueda.push("Este campo no puede quedar vacio");
            } else {
                estado2 = true;
                if (!FORMATO_FECHA.test(frm.txtDeFechaBusqueda)) {
                    val.campos.txtDeFechaBusqueda.push("Campo debe ser rellenado con formato dd/mm/yyyy");
                }
            }
            if (estado1 && estado2) {
                var dateInicio = getDateFromString(frm.txtDeFechaBusqueda, "dd/mm/yyyy", frm.txtHoraInicio, "hh:mm:ss");
                var dateFin = getDateFromString(frm.txtHastaFecha, "dd/mm/yyyy", frm.txtHoraFin, "hh:mm:ss");
                if ((dateFin - dateInicio) < 0) {
                    val.general.push("La fecha de fin debe ser mayor que la de inicio")
                }
                
            }
            val.estado = getEstadoVal(val);
            return val;
        }
    // ui 
        function getTiempo(div,inputTxt,valTiempo) {
            horas = div.find(".horas");
            minutos = div.find(".minutos");
            segundos = div.find(".seg");
            tiempo = div.find(".tiempo");
            txt = "@h:@m:@s @t";
            txt = txt.replace("@h", valMinh(horas.slider("value")));
            txt = txt.replace("@m", valMinh(minutos.slider("value")));
            txt = txt.replace("@s", "00");
            if (valTiempo == 0) {
                tiempo = "a.m.";
            } else {
                tiempo = "p.m.";
            }
            txt = txt.replace("@t", tiempo);
            return txt;
        }
        function rbTiempo(valTiempo,divRbTiempo) {
            div = divRbTiempo.parents(".divHora");
            inputTxt = divRbTiempo.parents(".divHora").find(".txtHora");
            txt = getTiempo(div, inputTxt, valTiempo);
            //$(".txtHoraInicio").val(txt);
            inputTxt.val(txt);
            horaConvert(txt);
        }
        function refreshTime(inputSlide) {
            
            div = $(inputSlide.target).parents(".divHora");
            //if (!div.hasClass("timeEdit")) {
                inputTxt = $(inputSlide.target).parents(".divHora").find(".txtHora");

                valTiempo = div.find("input[class='rbTiempo']:checked").val();
                txt = getTiempo(div, inputTxt, valTiempo);

                inputTxt.val(txt);
                console.log("vas a poner ", txt);
                horaConvert(txt);
            //} else {
              /*  console.log("yeaah");
            }*/
            
        }
        // usuarios
        function getCbUsuarios(usuarios) {
            cb = "";
            if (!(usuarios === null)) {
                $.each(usuarios, function (i, usuario) {
                    cb += "<option value='" + usuario._idUsuario + "'>" + usuario._usuario + "</option>";
                });
            }else{
                cb = "<option value='-1' selected disabled>No hay usuarios para compartir</option>"
            }
            return cb;
        }
        function getTrOneUsuarios(usuarioEvento) {
            tr = "\
                    <tr class='trUsuarioCompartido' >\
                        <td class='hidden'>\
                            <input class='txtHdIdUsuarioEvento' name='txtHdIdUsuarioEvento' value='" + usuarioEvento._idEventoUsuario + "'>\
                        </td>\
                        <td>"+ usuarioEvento._usuario._usuario + "</td>\
                        <td>\
                            <button class='btn btn-default icoQuitarUsuario btn-xs'>\
                                <i class='fa fa-trash'></i>\
                            </button>\
                        </td>\
                    </tr>";
            return tr;
        }
        function getTrUsuarios(usuariosEventos) {
            tr = "";
            if (!(usuariosEventos === null)) {
                $.each(usuariosEventos, function (i, usuarioEvento) {
                    tr += getTrOneUsuarios(usuarioEvento);
                });
            } else {
                tr = "<tr><td colspan='2' class='text-center noUsuarioCompartido'>No a compartido con ningun usuario</td></tr>"
            }
            return tr;
        }
        // permisos 
        function getCbPermisos(permisos) {
            cb = "";
            if (!(permisos === null)) {
                $.each(permisos, function (i, permiso) {
                    cb += "<option value='" + permiso._idPermiso + "'>" + permiso._permiso + "</option>";
                });
            } else {
                cb = "<option value='-1'>No hay permisos para agregar</option>"
            }
            return cb;
        }
        function getTrOnePermisos(PermisoUsuarioEvento) {
            tr = "";
            if (!(PermisoUsuarioEvento === null)) {
                tr += "\
                    <tr>\
                        <tr>\
                            <td class='hidden'>\
                                <input class='txtHdIdPermisoUsuarioEvento' name='txtHdIdPermisoUsuarioEvento' value='" + PermisoUsuarioEvento._idPermisoUsuarioEvento + "'>\
                                <input class='txtHdIdUsuarioEvento' name='txtHdIdUsuarioEvento' value='" + PermisoUsuarioEvento._usuarioEvento._idEventoUsuario + "'>\
                            </td>\
                            <td>" + PermisoUsuarioEvento._permiso._permiso + "</td>\
                            <td>\
                                <button class='btn btn-default icoEliminarPermisoEvento'>\
                                    <i class='fa fa-trash pointer'/>\
                                </button>\
                            </td>\
                        </tr>\
                    </tr>\
                ";
            }
            return tr;
        }
        function getTrPermisos(PermisosUsuariosEventos) {
            tr = "";
            if (!(PermisosUsuariosEventos === null)) {
                $.each(PermisosUsuariosEventos, function (i, PermisoUsuarioEvento) {
                    tr += getTrOnePermisos(PermisoUsuarioEvento);
                })
            } else {
                tr = "<tr class='noPermisoUsuarioEvento'><td class='text-center' colspan='2'>El usuario no posee permisos</td></tr>"
            }
            return tr;
        }
    
        // llenar 
        function llenarInputsEdicion(evento, div) {
            // tanto val como text ya que sino a la hora de llenar el formulario muere todo
            div.find(".txtAreaDescripcion").text(evento._descripcion);
            div.find(".txtFechaInicio").val(evento._fechaInicio);
            div.find(".txtFechaFin").val(evento._fechaFin);
            div.find(".txtHoraFin").val(evento._horaFin);
            div.find(".txtHoraInicio").val(evento._horaInicio);
        }
        function llenarInputsVista(evento,div){
            h3 = div.prev();
            div.find(".pDescripcionEvento").empty().append(evento._descripcion);
            div.find(".spanFechaInicio").empty().append(evento.getFechaInicio);
            div.find(".spanFechaFin").empty().append(evento.getFechaFin);
            h3.find(".spanNombreEvento").empty().append(evento._evento);
        }
        function getEventosAcordion(evento) {
            if(evento._publicado){
                intPublicado = 1
            }else{
                intPublicado = 0;
            }
            div = "\
                        <h3 class='nombreEvento " + evento.txtClaseColor + " '>\
                            <span class='spanNombreEvento'>"+ evento._evento + "</span>\
                        </h3>\
                        <div class='detalleEvento'>\
                            <input type='hidden' name='txtHdIdEvento' class='txtHdIdEvento' value='"+ evento._idEvento + "' />\
                            <input type='hidden' name='txtHdEstadoEvento' class='txtHdEstadoEstado' value='"+ intPublicado + "' />\
                            <div class='row'>\
                                <div class='normalMode'>\
                                    <p class='text-justify pDescripcionEvento'>"+ evento._descripcion + "</p>\
                                </div>\
                                <div class='editMode hidden' style='margin-bottom:5%;'>\
                                    <label>Nombre de evento</label>\
                                    <input type='text' name='txtEvento2' class='txtEvento2' />\
                                </div>\
                                <div class='editMode hidden' >\
                                    <div class='editMode hidden'>\
                                        <label>Descripcion evento</label>\
                                    </div>\
                                    <textarea class='txtAreaDescripcion form-control' name='txtAreaDescripcion'></textarea>\
                                </div>\
                                    <div class='quitarPublicacionMode hidden'>\
                                        <label class='text-center'>Motivos para quitar del website</label>\
                                        <textarea class='txtAreaMotivoQuitar form-control' name='txtAreaMotivoQuitar'></textarea>\
                                    </div>\
                                <hr />\
                                <div class='row text-center'>\
                                    <div class='col-lg-6'>\
                                        <div class='normalMode'>\
                                            <label>Inicio: </label>\
                                            <span class='spanFechaInicio'>"+ evento.getFechaInicio + "</span>\
                                        </div>\
                                        <div class='editMode hidden'>\
                                            <label>Inicio: </label>\
                                            <input class='dpFecha txtFechaInicio form-control' name='txtFechaInicio' />\
                                            <input type='time' class='txtHoraInicio form-control' name='txtHoraInicio' />\
                                        </div>\
                                    </div>\
                                    <div class='col-lg-6'>\
                                        <div class='normalMode'>\
                                            <label>Fin: </label>\
                                            <span class='spanFechaFin'>"+ evento.getFechaFin + "</span>\
                                        </div>\
                                        <div class='editMode hidden'>\
                                            <label>Fin: </label>\
                                            <input class='dpFecha txtFechaFin form-control' name='txtFechaFin' />\
                                            <input type='time' class='txtHoraFin form-control ' name='txtHoraFin' />\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                            <div class='row text-center sectionBotonesEvento'>\
                                <div class='normalMode'>\
                                    <button class='btn btnEditar'>Editar</button>\
                                        <button class='btn btnCompartir'>Compartir</button>\
                                    <button class='btn btnPublicar'>"+ evento.txtBtnPublicar + "</button>\
                                </div>\
                                <div class='editMode hidden'>\
                                    <button class='btn btnActualizar' >Actualizar</button>\
                                    <button class='btn btnCancelar' >Cancelar</button>\
                                </div>\
                                <div class='quitarPublicacionMode hidden'>\
                                    <button class='btn btnAccionQuitarPublicacion'>Quitar publicacion</button>\
                                    <button class='btn btnCancelaQuitarPublicacion'>Cancelar</button>\
                                </div>\
                            </div>\
                        </div>\
            ";
            return div;
        }
        function agregarEvento(calendar, evento, sticker, propio) {
            if (propio === null || propio == 1) {
                backColor = "#3A87AD";
            } else if (propio == 2) {
                backColor = "#f1c40f";
            } else if (propio == 3) {
                // eventos publicos
                backColor = "#2ecc71";
            }
            eventoAgregar = {
                title: evento._evento,
                start: evento.getFechaInicioUSA,
                end: evento.getFechaFinUSA,
                color: backColor
            };
            calendar.fullCalendar('renderEvent', eventoAgregar, true);
        }
        function updateDespuesDePublicacion(eventoWebsite, detalle) {
            console.log("Evento website es:", eventoWebsite);
            var icoPublicado = "";
            if (eventoWebsite._evento._publicado)
            {
                icoPublicado = "<i class='fa fa-ban' title='" + eventoWebsite._evento.txtBtnPublicar + "'></i>";
            } else {
                icoPublicado = "<i class='fa fa-globe' title='" + eventoWebsite._evento.txtBtnPublicar + "'></i>";
            }
            detalle.find(".btnPublicar").empty().append(icoPublicado);
            if (eventoWebsite._estado == true) {
                intEstado = 1;
            } else {
                intEstado = 0;
            }
            detalle.find(".txtHdEstadoEstado").val(intEstado);
        }
        function cargarCompartir(div,tab) {
            tr = "\
            <tr>\
                <td colspan='2' class='text-center'>Seleccione usuario</td>\
            </tr>\
        ";
            tab.find(".tbPermisos").empty().append(tr);
            seccion = tab.parents(".areaCompartir");
            idEvento = div.find(".txtHdIdEvento").val();

            seccion.find(".txtHdIdEvento").val(idEvento);
            frm = { idEvento: idEvento }
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_loadCompartirEventos", frm, function (data) {
                if (data.estado) {
                    cbUsuarios = getCbUsuarios(data.usuariosNoCompartidos);
                    $(".cbUsuarioCompartir").empty().append(cbUsuarios);
                    resetChosen($(".cbUsuarioCompartir"));
                    tr = getTrUsuarios(data.usuariosCompartidos);
                    tab.find(".tbUsuarios").empty().append(tr);
                }
            });
        }
        // acciones script
        function icoEliminarPermisoEvento(tr) {
            frm = serializeSection(tr);
        
            
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_eliminarPermisoUsuarioEvento", frm, function (data) {
                
                if (data.estado) {
                    tr.remove();
                    cb = getCbPermisos(data.permisosFaltantes);
                    $(".cbPermisosCompartir").empty().append(cb);
                    resetChosen($(".cbPermisosCompartir"));
                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        // agregar permisos usuario
            function validarIngresoPermiso(frm) {
                var estado = false;
                var val = new Object();
                val.campos = {
                    cbPermisosCompartir:    new Array(),
                    txtValidacionUsuario:   new Array()
                };
                val.general = new Array();
                if (frm.idPermisos === undefined || frm.idPermisos === null || frm.idPermisos == -1) {
                    val.general.push("Debe seleccionar permiso");
                }
                if (frm.idUsuarioEvento === undefined || frm.idUsuarioEvento === null || frm.idUsuarioEvento == -1) {
                    //val.general.push("Debe seleccionar un usuario");
                    val.general.push("Debe seleccionar un usuario de la tabla");
                }
                val.estado = objArrIsEmpty(val.campos);
                var estado = false;
                if (val.general.length == 0) {
                    estado = true;
                }
                if (estado && val.estado) {
                    val.estado = true;
                } else {
                    val.estado = false;
                }
                return val;
            }
            function btnPermisos(frm) {
            
            tbody = $(".tbPermisos");
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_agregarPermisoUsuarioEvento", frm, function (data) {
                
                if (data.estado) {
                    tr = getTrPermisos(data.PermisosUsuariosEventos);
                    
                    if (tbody.find(".noPermisoUsuarioEvento").length == 0) {
                        tbody.prepend(tr);
                    } else {
                        tbody.empty().append(tr);
                    }
                    cb = getCbPermisos(data.permisosFaltantes);
                    $(".cbPermisosCompartir").empty().append(cb);
                    resetChosen($(".cbPermisosCompartir"));
                    if (!data.estadoIndividual) {
                        alert("Algunos registros no fueron guardados correctamente");
                    }
                }
            })
        }
        // agregar usuarios a compartir
            function validarIngresoUsuario(frm) {
                var estado = false;
                var val = new Object();
                val.campos = {
                    cbPermisosCompartir: new Array(),
                    txtValidacionUsuario: new Array()
                };
                val.general = new Array();
                if (frm.cbUsuarioCompartir === undefined || frm.cbUsuarioCompartir === null || frm.cbUsuarioCompartir == -1) {
                    val.general.push("Seleccione un usuario");
                }
                if (frm.idEvento === undefined || frm.idEvento === null || frm.idEvento == -1) {
                    val.general.push("Seleccione un evento");
                }
                val.estado = objArrIsEmpty(val.campos);
                var estado = false;
                if (val.general.length == 0) {
                    estado = true;
                }
                if (estado && val.estado) {
                    val.estado = true;
                } else {
                    val.estado = false;
                }
                return val;
            }
            function btnAgregarUsuarioCompartir(frm) {
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_compartirEventoUsuario", frm, function (data) {
                if (data.estado) {
                    if (!(data.usuarioEventoAgregado === null)) {
                        cb = getCbUsuarios(data.usuariosFaltantes);
                        $(".cbUsuarioCompartir").empty().append(cb);
                        resetChosen($(".cbUsuarioCompartir"));
                        tr = getTrOneUsuarios(data.usuarioEventoAgregado);
                        tbody = $(".tbUsuarios");
                        if (tbody.find(".noUsuarioCompartido").length == 0) {
                            tbody.prepend(tr);
                        } else {
                            tbody.empty().append(tr);
                        }
                    
                    } else {
                        alert("Error");
                    }
                } else {
                    alert("Ocurrio un error");
                }
            });
        }

        function icoQuitarUsuario(tr) {
            frm = serializeSection(tr);
            frm.idEvento = $(".areaCompartir").find(".txtHdIdEvento").val();

            actualizarCatalogo(RAIZ + "/Administracion/sp_adminfe_removeUsuarioEvento", frm, function (data) {
                if (data.estado) {
                    tr.remove();
                    cb = getCbUsuarios(data.usuariosFaltantes);
                    $(".cbUsuarioCompartir").empty().append(cb);
                    resetChosen($(".cbUsuarioCompartir"));
                }
            });
        }
        function btnActualizar(detalle) {
            frm = serializeSection(detalle);
            console.log("formulario a actualizar es: ", frm);
            frm.txtFechaFin    = fechaStandar(frm.txtFechaFin);
            frm.txtFechaInicio = fechaStandar(frm.txtFechaInicio);
            h3 = detalle.prev();
            frm.txtEvento = h3.find(".txtEvento2").val();
            if (frm.txtHoraInicio != "" && frm.txtHoraFin != "") {
                actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_editarEventos", frm, function (data) {
                    if (data.estado) {
                        llenarInputsVista(data.eventoEditado, detalle);
                        controlesEdit(false, div);
                    } else {
                        alert("Ocurrio un error");
                    }
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
            
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_quitarEventoWebsite", frm, function (data) {
                
                if (data.estado) {
                    updateDespuesDePublicacion(data.eventoWebsite, detalle);
                    controlesEdit(false, detalle, ".quitarPublicacionMode")
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
            h3 = div.prev();
            div.find(".txtEvento2").val(h3.find(".spanNombreEvento").text());
            llenarInputsEdicion(evento, div);
            controlesEdit(true, div);
        
        }
        function btnPublicar(detalle) {
            frm = { txtHdIdEvento: detalle.find(".txtHdIdEvento").val() }
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_publicarEventoWebsite", frm, function (data) {
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
        function resetRbTiempo(){
            $(".rbAm").val("0");
            $(".rbPm").val("1");
            $(".rbTiempo").prop("checked", false);
            $(".rbPm").prop("checked", true);
        }
        function limpiarFormulario() {
            $(".txtEvento").val(""); $(".txtAreaDescripcion").val("");
            $(".dpFecha").val("");
        }
        function frmAgregarEvento(frm, frmSection) {
            frm.txtHoraInicio = horaConvert(frm.txtHoraInicio);
            frm.txtHoraFin = horaConvert(frm.txtHoraFin);
            //console.log("formulario a enviar",frm);
            frm.txtFechaFin     = fechaStandar(frm.txtFechaFin);
            frm.txtFechaInicio = fechaStandar(frm.txtFechaInicio);
            console.log("formulario a enviar es:", frm);
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_crearEvento", frm, function (data) {
                console.log("respuesta sp_adminfe_crearEvento", data);
                if (data.estado) {
                    agregarEvento($("#calendar"), data.evento, true, 1);
                    div = getEventosAcordion(data.evento);
                    $("#accordion").prepend(div);
                    irA($("#calendar"));
                    //clearTr(frmSection); se comenta porq mata las horas
                    limpiarFormulario();
                    resetRbTiempo();
                    printMessage($(".divMensajeForm"), "Evento agregado correctamente", true);
                } else {
                    if (data.error._mostrar && data.error.Message != "") {
                        printMessage($(".divMensajeForm"), data.error.Message, false);
                        //alert(data.error.Message);
                    } else {
                        printMessage($(".divMensajeForm"), data.error.Message, false);
                        //alert("ocurrio un error");
                    }
                
                }
            });
        
        }
        function btnCompartir(detalle) {
            h3  = detalle.prev();
            tab = $("#tabUsuario");
            $(".hEventoUsuario").empty().append(h3.find(".spanNombreEvento").text());
            tab.css("background", 'rgba(229,229, 229, 0.5)');
            tab.animate({
                backgroundColor: "white"
            }, 500);
            cargarCompartir(detalle,tab);
        }
        function trUsuarioCompartido(tr) {
            tbody = tr.parents("tbody");
            tbody.find(".clickTr").removeClass("clickTr");
            tr.addClass("clickTr");
            frm = {
                idUsuarioEvento: tr.find(".txtHdIdUsuarioEvento").val(),
            }
            // cargar los permisos de el usuario clickeado
            actualizarCatalogo(RAIZ+"/Administracion/sp_adminfe_getPermisosUsuarioEvento", frm, function (data) {
                if (data.estado) {
                    cb = getCbPermisos(data.permisosFaltantes);
                    $(".cbPermisosCompartir").empty().append(cb);
                    resetChosen($(".cbPermisosCompartir"));
                    tr = getTrPermisos(data.permisosActuales);
                    $(".tbPermisos").empty().append(tr);
                } else {
                    alert("Ocurrio un error");
                }
            });
        }
