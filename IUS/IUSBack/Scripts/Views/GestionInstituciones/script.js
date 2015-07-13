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
            
            $(document).on("click", ".btnCancelar", function () {
                trInstitucion = $(this).parents("tr");
                console.log("entro");
                controlesEdit(false, trInstitucion);
            })
            $(document).on("click", ".btnActualizarInstitucion", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnActualizarInstitucion(frm,seccion);
            })
            $(document).on("click", ".btnEditar", function () {
                trInstitucion = $(this).parents("tr");
                //table = $(".tbInstituciones").DataTable();
                btnEditar(trInstitucion);
                
            })
            $(document).on("click", ".btnDeleteInstitucion", function () {
                //var x = confirm("¿?")
                seccion = $(this).parents("tr");
                frm = { idInstitucion: seccion.find(".txtHdIdInstitucion").val() }
                console.log(frm);
                btnDeleteInstitucion(frm,seccion);
            });
            $(document).on("click", ".btnAddInstitucion", function () {
                seccion = $(".trFrmInstituciones");
                frm = serializeSection(seccion);
                btnAddInstitucion(frm,seccion);
            });
});