﻿// acciones 
    function btnAgregarLaboralPersona(frm) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertLaboralPersonas", frm, function (data) {
            console.log(data);
        })
    }
    function btnEliminarLaboralPersona(frm,tr) {
        //**
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarLaboralPersonas", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }