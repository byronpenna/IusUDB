$(document).ready(function () {
    // plugins 
        // tabs 
            $('#horizontalTab').responsiveTabs();
    // eventos 
        // submit
            $(document).on("submit", "#frmInstitucional", function (e) {
                e.preventDefault();
                frm = serializeToJson($(this).serializeArray());
                console.log("Formulario a enviar es: ", frm);
                frmInstitucional(frm);
            })
        // click
            $(document).on("click", ".iconQuitarValor", function () {
                var x = confirm("¿Esta seguro de quitar valor?");
                if (x) {
                    alert("Esta accion no esta disponible por el momento");
                }
                
            });
            $(document).on("click", ".btnAddValor", function () {
                frmSection = $(this).parents(".agregarValorSection");
                frm = serializeSection(frmSection);
                console.log("formulario a agregar", frm);
                btnAddValor(frm);
            })
});