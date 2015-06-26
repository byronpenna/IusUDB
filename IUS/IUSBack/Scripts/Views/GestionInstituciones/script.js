$(document).ready(function () {
    // plugins 
        // data table 
            var dataTableInstituciones = $(".tbInstituciones").DataTable({
                "bSort": false
            });
        // chosen
            $(".cbPais").chosen({ no_results_text: "Ese pais no existe", width: '100%' });
    // eventos 
        $(document).on("click", ".btnAddInstitucion", function () {
            frm = serializeSection($(".trFrmInstituciones"));
            btnAddInstitucion(frm);
            //addDataTableRow(dataTableInstituciones, data);
        })
});