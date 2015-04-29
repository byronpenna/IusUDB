// load
    function eventosIniciales() {
    var frm = new Object();
    actualizarCatalogo("/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
        console.log("respuesta eventos principales", data);
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
            console.log("UsuarioEvento a agregar es: ", usuarioEvento);
            tr = "\
                    <tr class='trUsuarioCompartido' >\
                        <td class='hidden'>\
                            <input class='txtHdIdUsuarioEvento' name='txtHdIdUsuarioEvento' value='" + usuarioEvento._idEventoUsuario + "'>\
                        </td>\
                        <td>"+ usuarioEvento._usuario._usuario + "</td>\
                        <td><i class='fa fa-times pointer icoQuitarUsuario'></td>\
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
            //console.log("permisosUsuarioEvento a poner", PermisoUsuarioEvento);
            if (!(PermisoUsuarioEvento === null)) {
                tr += "\
                    <tr>\
                        <tr>\
                            <td class='hidden'>\
                                <input class='txtHdIdPermisoUsuarioEvento' name='txtHdIdPermisoUsuarioEvento' value='" + PermisoUsuarioEvento._idPermisoUsuarioEvento + "'>\
                                <input class='txtHdIdUsuarioEvento' name='txtHdIdUsuarioEvento' value='" + PermisoUsuarioEvento._usuarioEvento._idEventoUsuario + "'>\
                            </td>\
                            <td>" + PermisoUsuarioEvento._permiso._permiso + "</td>\
                            <td><i class='fa fa-times pointer icoEliminarPermisoEvento'></td>\
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
            //div.find(".txtHoraFin").val(evento._horaFin);
            //div.find(".txtHoraInicio").val(evento.)
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
    detalle.find(".btnPublicar").text(eventoWebsite._evento.txtBtnPublicar);
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
        actualizarCatalogo("/Administracion/sp_adminfe_loadCompartirEventos", frm, function (data) {
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
        
        console.log("Formulario a enviar es: ", frm);
        actualizarCatalogo("/Administracion/sp_adminfe_eliminarPermisoUsuarioEvento", frm, function (data) {
            console.log("la respuesta del servidor es: ", data);
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
    function btnPermisos() {
        frm = { idPermisos: $(".cbPermisosCompartir").val(), idUsuarioEvento: $(".trUsuarioCompartido.clickTr").find(".txtHdIdUsuarioEvento").val() }
        tbody = $(".tbPermisos");
        actualizarCatalogo("/Administracion/sp_adminfe_agregarPermisoUsuarioEvento", frm, function (data) {
            console.log("la respuesta del servidor es: ", data);
            if (data.estado) {
                tr = getTrPermisos(data.PermisosUsuariosEventos);
                console.log("Existe para poner encima permiso", tbody.find(".noUsuarioCompartido").length);
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
    function icoQuitarUsuario(tr) {
        frm = serializeSection(tr);
        frm.idEvento = $(".areaCompartir").find(".txtHdIdEvento").val();
        console.log("formulario a enviar es", frm);
        actualizarCatalogo("/Administracion/sp_adminfe_removeUsuarioEvento", frm, function (data) {
            if (data.estado) {
                tr.remove();
                cb = getCbUsuarios(data.usuariosFaltantes);
                $(".cbUsuarioCompartir").empty().append(cb);
                resetChosen($(".cbUsuarioCompartir"));
            }
        });
    }
    function btnAgregarUsuarioCompartir(divFrm) {
        frm = serializeSection(divFrm);
        frm.idEvento = $(".areaCompartir").find(".txtHdIdEvento").val();
        console.log("formulario a enviar es: ", frm);
        actualizarCatalogo("/Administracion/sp_adminfe_compartirEventoUsuario", frm, function (data) {
            console.log("la data del servidor es: ", data);
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
    function btnActualizar(detalle) {
        frm = serializeSection(detalle);
        h3 = detalle.prev();
        frm.txtEvento = h3.find(".txtEvento2").val();
        if (frm.txtHoraInicio != "" && frm.txtHoraFin != "") {
            actualizarCatalogo("/Administracion/sp_adminfe_editarEventos", frm, function (data) {
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
        actualizarCatalogo("/Administracion/sp_adminfe_quitarEventoWebsite", frm, function (data) {
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
        h3 = div.prev();
        div.find(".txtEvento2").val(h3.find(".spanNombreEvento").text());
        llenarInputsEdicion(evento, div);
        controlesEdit(true, div);
        
    }
    function btnPublicar(detalle) {
        frm = { txtHdIdEvento: detalle.find(".txtHdIdEvento").val() }
        actualizarCatalogo("/Administracion/sp_adminfe_publicarEventoWebsite", frm, function (data) {
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
            console.log("la data regresada por el servidor(evento creado) es: ", data);
            if (data.estado) {
                agregarEvento($("#calendar"), data.evento, true, 1);
                div = getEventosAcordion(data.evento);
                $("#accordion").prepend(div);
                $("#accordion").accordion("refresh");
                
                clearTr(frmSection);
                
            } else {
                if (data.error._mostrar && data.error.Message != "") {
                    alert(data.error.Message);
                } else {
                    alert("ocurrio un error");
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
        actualizarCatalogo("/Administracion/sp_adminfe_getPermisosUsuarioEvento", frm, function (data) {
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
