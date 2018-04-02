
$(document).ready(function () {
    var grid = $('#grid').grid({
        dataSource: '/Distributor/List',
        columns: [
            { field: 'Code', width: 100, sortable: true },
            { field: 'Name', width: 200, sortable: true },
            { field: 'Address', title: 'Address', sortable: true, width: 200}
        ],
        pager: { limit: 5 }
    });
});