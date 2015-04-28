// load
    function eventosIniciales() {
    var frm = new Object();
    actualizarCatalogo("/Administracion/sp_adminfe_getEventosPrincipales", frm, function (data) {
        if (data.estado) {
            eventos = data.eventos;
            $.each(eventos, function (i, evento) {
                agregarEvento($("#calendar"), evento, true);
            });
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
        function getTrUsuarios(usuariosEventos) {
            tr = "";
            if (!(usuariosEventos === null)) {
                $.each(usuariosEventos, function (i, usuarioEvento) {
                    tr += "\
                    <tr class='trUsuarioCompartido' >\
                        <td class='hidden'><input class='txtHdIdUsuarioEvento' value='" + usuarioEvento._idEventoUsuario + "'></td>\
                        <td>"+ usuarioEvento._usuario._usuario + "</td>\
                        <td><i class='fa fa-times pointer icoQuitarUsuario'></td>\
                    </tr>";
                });
            } else {
                tr = "<tr><td colspan='2' class='text-center'>No a compartido con ningun usuario</td></tr>"
            }
            return tr;
        }
    // permisos 
        function getCbPermisos(permisos) {
            cb = "";
            if (!(permisos === null)) {
                $.each(permisos, function (i, permiso) {
                    cb += "<option value='" + permiso.idPermiso + "'>" + permiso._permiso + "</option>";
                });
            } else {
                cb = "<option value='-1'>No hay permisos para agregar</option>"
            }
            return cb;
        }
        function getTrPermisos(PermisosUsuariosEventos) {
            tr = "";
            if (!(PermisosUsuariosEventos === null)) {
                $.each(PermisosUsuariosEventos, function (i, PermisoUsuarioEvento) {
                    tr += "\
                        <tr>\
                            <tr>\
                                <td class='hidden'><input class='txtHdIdUsuarioEvento' value='" + PermisoUsuarioEvento._idPermisoUsuarioEvento + "'></td>\
                                <td>" + PermisoUsuarioEvento._permiso._permiso + "</td>\
                                <td><i class='fa fa-times pointer icoPermisoEvento'></td>\
                            </tr>\
                        </tr>\
                    ";
                })
            } else {
                tr = "<tr><td class='text-center' colspan='2'>El usuario no posee permisos</td></tr>"
            }
            return tr;
        }
    function agregarEvento(calendar,evento,sticker) {
        eventoAgregar = {
            title: evento._evento,
            start: evento.getFechaInicioUSA,
            end: evento.getFechaFinUSA 
        };
        calendar.fullCalendar('renderEvent', eventoAgregar, true);
    }
    function llenarInputsEdicion(evento,div) {
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
    function updateDespuesDePublicacion(eventoWebsite,detalle) {
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
    function frmCompartirUsuario(divFrm) {
        frm = serializeSection(divFrm);
        frm.idEvento = $(".areaCompartir").find(".txtHdIdEvento").val();
        console.log("formulario a enviar es: ", frm);
        /*actualizarCatalogo("/Administracion/sp_adminfe_getPermisosUsuarioEvento", frm, function (data) {

        });*/
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
            if (data.estado) {
                agregarEvento($("#calendar"), data.evento);
                clearTr(frmSection);
            } else {
                alert("ocurrio un error");
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
