$(document).ready(function () {
    // plugins 
        var dataTableInstituciones = $(".tbInstituciones").DataTable({
            "bSort": false
        });
    // eventos 
        $(document).on("click", ".btnAddInstitucion", function () {
            data = [
            '1',
            '2',
            '3',
            '4',
            '5'
            ];
            addDataTableRow(dataTableInstituciones, data);
        })
});