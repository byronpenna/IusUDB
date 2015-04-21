$(document).ready(function () {
    // plugins 
        // full calendar
            $("#calendar").fullCalendar({
                editable: true,
            });
        // tabs
            $('#horizontalTab').responsiveTabs();
    // funciones iniciales 
        eventosIniciales();
    
    

});