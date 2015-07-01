$(document).ready(function () {
    // eventos
        // click 
            $(document).on("click", ".btnActualizar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnActualizar(frm,seccion);
            })
            $(document).on("click", ".btnEditarTel", function () {
                trTel = $(this).parents("tr");
                //table = $(".tbInstituciones").DataTable();
                btnEditarTel(trTel);
            })
            $(document).on("click", ".btnAgregarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnAgregarTel(frm);
            });
            $(document).on("click", ".btnEliminarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnEliminarTel(frm, seccion);
            });
})