﻿// generics 
    
// acciones script
    function btnEditar(trMedio) {
        medio = {
            enlace:         trMedio.find(".tdEnlace").text(),
            nombreEnlace: trMedio.find(".tdNombreEnlace").text(),
        }
    }
    function btnAgregar(frm) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_insertEnlaceInstituciones", frm, function (data) {
            console.log(data);
        });
    }
    function btnEliminar(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_deleteEnlaceInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }