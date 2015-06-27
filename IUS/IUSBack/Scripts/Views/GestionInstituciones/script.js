$(document).ready(function () {
    // plugins 
        // data table 
            var dataTableInstituciones = $(".tbInstituciones").DataTable({
                "bSort": false
            });
        // chosen
            $(".cbPais").chosen({ no_results_text: "Ese pais no existe", width: '100%' });
    // eventos 
        // click    
            $(document).on("click", ".btnDeleteInstitucion", function () {
                //var x = confirm("¿?")
                seccion = $(this).parents("tr");
                frm = { idInstitucion: seccion.find(".txtHdIdInstitucion").val() }
                console.log(frm);
                btnDeleteInstitucion(frm,seccion);
            });
            $(document).on("click", ".btnAddInstitucion", function () {
                frm = serializeSection($(".trFrmInstituciones"));
                btnAddInstitucion(frm);
            });
});