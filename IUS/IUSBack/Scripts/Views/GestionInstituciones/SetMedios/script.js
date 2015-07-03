﻿$(document).ready(function () {
    // eventos 
        // clicks 
           
                $(document).on("click", ".btnCancelar", function () {
                    var x = confirm("¿Esta seguro de cancelar edicion?");
                    if (x) {
                        trMedio = $(this).parents("tr");
                        controlesEdit(false, trMedio);
                    }
                })
                $(document).on("click", ".btnActualizar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    console.log(frm);
                    btnActualizar(frm, seccion);
                })
                $(document).on("click", ".btnEditar", function () {
                    trMedio = $(this).parents("tr");
                    btnEditar(trMedio);
                })
    
                $(document).on("click", ".btnAgregar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    btnAgregar(frm);
                })
                $(document).on("click", ".btnEliminar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    btnEliminar(frm,seccion);
                });
})