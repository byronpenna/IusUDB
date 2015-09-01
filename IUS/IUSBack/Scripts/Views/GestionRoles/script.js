$(document).ready(function () {
    // plugins
        // tabs
            //$('#horizontalTab').responsiveTabs();
        // chosen 
            // tab 1
                $(".cbUsuarios").chosen();
                $(".cbRoles").chosen({ no_results_text: "Rol no encontrado", width: '100%' });
            // tab 2
                $(".cbRolTab2").chosen({ no_results_text: "Rol no encontrado", width: '100%' });
                $(".cbAsignarPermisos").chosen({ no_results_text: "Permiso no encontrado", width: '100%' });
                $(".cbSubMenu").chosen({ no_results_text: "Submenu no encontrado", width: '100%' });
    // eventos
        // keyup
            $(document).on("keydown", ".txtRolEdit", function (e) {
                var charcode = e.which;
                switch (charcode) {
                    case 13: {
                        $(this).parents("tr").find(".btnActualizar").click();
                    }

                }

            })
                
            $(document).on("keydown", ".txtRolAgregar", function (e) {
                var charcode = e.which;
                switch (charcode) {
                    case 13: {
                        $(".btnAgregarRol").click();
                    }
                }
            })
        // click 
            $(document).on("click", "#btnAddRoles", function () {
                var x = confirm("¿Esta seguro que desea agregar los siguientes roles?");
                if (x) {
                    var frm = new Object();
                    frm.rolesAgregar    = $(".cbRoles").val();
                    frm.idUsuario       = $(".cbUsuarios").val();
                    if (frm.rolesAgregar != null) {
                        agregarRoles(frm);
                    }else {
                        //alert("Seleccione un rol a agregar");
                        printMessage($("#tab-1 .divResultado"), "Seleccione un rol a agregar", false);
                    }
                }
            });
            // tab2
                $(document).on("click", ".btnAsignarSubmenu", function () {
                    var frm = new Object();
                    frm.idRol       = $(".cbRolTab2").val();
                    frm.idSubMenu = $(".cbSubMenu").val();
                    console.log(frm);
                    var val = validarAsignarSubMenu(frm);
                    if (val.estado) {
                        btnAsignarSubmenu(frm);
                    }else{
                        var div = "";
                        $.each(val.general, function (i, val) {
                            div += "<div class='row marginNull'>";
                                div += getSpanMessageError(val);
                            div += "</div class='row marginNull'>";
                        })
                        printMessageDiv($(".divAgregarSubMenu .divResultado"), div);
                    }
                    
                });
                $(document).on("click", ".btnAsignarPermiso", function () {
                    var frm = new Object();
                    var x = confirm("¿Esta seguro que desea asignar los siguientes permisos?");
                    
                    if (x) {
                        frm.idRol       = $(".cbRolTab2").val();
                        frm.idPermisos  = $(".cbAsignarPermisos").val();
                        frm.idSubMenu   = $(".trSubMenu.activeTr").find(".txtIdSubMenu").val();
                        console.log("formulario a enviar", frm);
                        var val = validarAsignarPermiso(frm);
                        console.log(val);
                        if (val.estado) {
                            btnAsignarPermiso(frm);
                        } else {
                            var div = "";
                            $.each(val.general, function (i, val) {
                                div += "<div class='row marginNull'>";
                                    div += getSpanMessageError(val);
                                div += "</div class='row marginNull'>";
                            })
                            printMessageDiv($(".divAgregarPermiso .divResultado"), div);
                            /*$(".divResultado").removeClass("hidden");
                            $(".divResultado").empty().append(div);*/
                        }
                        
                    }
                });
                $(document).on("click", ".icoQuitarPermiso", function () {
                    var x = confirm("¿Esta seguro que desea quitar este permiso?");

                    if (x) {
                        trPermiso = $(this).parents("tr");
                        clickIcoQuitarPermiso(trPermiso);
                    }
                })
                $(document).on("click", ".trSubMenu", function () {
                    trSubMenu = $(this);
                    clickTrSubMenu(trSubMenu);
                })
                $(document).on("click", ".iconQuitarRol", function () {
                    var x = confirm("¿Esta seguro que desea quitarle ese rol?");
                    trRol = $(this).parents(".trEstadoRol");
                    if (x) {
                        frm = new Object();
                        frm.idUsuario   = $(".cbUsuarios").val();
                        frm.idRol = trRol.find(".txtIdRol").val();
                        desasociarRol(frm, trRol);
                    }
                })
                $(document).on("click", ".icoQuitarSubMenu", function (e) {
                    e.stopPropagation();
                    var x = confirm("¿Esta seguro que desea quitar ese menu a rol?");
                    trSubmenu = $(this).parents("tr");
                    if (x) {
                         quitarSubMenuArol(trSubmenu);
                    }
                });
            // tab3
                
                $(document).on("click", ".btnAgregarRol", function () {
                    tr = $(this).parents("tr");
                    btnAgregarRol(tr);
                });
                $(document).on("click", ".btnEliminar", function () {
                    tr = $(this).parents("tr");
                    var x = confirm("¿Esta seguro que desea eliminar este rol?");
                    if (x) {
                        btnEliminar(tr);
                    }
                })
                $(document).on("click", ".btnDeshabilitar", function () {
                    var x = confirm("¿Esta seguro que desea cambiarle el estado al rol?");
                    tr = $(this).parents("tr");
                    if (x) {
                        btnDeshabilitar(tr);
                    }
                });
                // edicion 
                    
                    $(document).on("click", ".btnActualizar", function () {
                        tr = $(this).parents("tr");
                        btnActualizar(tr);
                    });
                    $(document).on("click", ".btnEditar", function () {
                        var x = confirm("¿Esta seguro que desea editar este rol?");
                        tr = $(this).parents("tr");
                        if (x) {
                            var resultados = tr.find(".divResultado");
                            resultados.empty();
                            resultados.addClass("hidden");
                            btnEditar(tr);
                            controlesEdit(true, tr);
                        }
                    });
                    $(document).on("click", ".btnCancelarEdit", function () {
                        tr = $(this).parents("tr");
                        cancelarEdicion(tr,true);
                    });
        // change
            // tab1
                $(document).on("change", ".cbUsuarios", function () {
                    // llenar tablita    
                    idUsuario = $(this).val();
                    llenarTablaRolesUsuario(idUsuario);
                });
            // tab2
                $(document).on("change", ".cbRolTab2", function () {
                    var frm = new Object();
                    frm.idRol = $(this).val();
                    changeRolTab2(frm);
                });
        
})
