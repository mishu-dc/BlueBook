
var grid;
var dialog;

function Edit(e) {
    $('#Id').val(e.data.id);
    $('#Code').val(e.data.record.Code);
    $('#Name').val(e.data.record.Name);
    dialog.open('Edit Brand');
}
function Save() {
    var record = {
        Id: $('#Id').val(),
        Code: $('#Code').val(),
        Name: $('#Name').val(),
    };

    $.ajax({ url: '/Brand/Save', data: { record: record }, method: 'POST' })
        .done(function () {
            dialog.close();
            grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val() });
        })
        .fail(function () {
            alert('Failed to save.');
        });
}

function Delete(e) {
    if (confirm('Are you sure to delete the entry?')) {
        $.ajax({ url: '/Brand/Delete', data: { Id: e.data.id }, method: 'POST' })
            .done(function () {
                grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val() });
            })
            .fail(function () {
                alert('Failed to delete.');
            });
    }
}

$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'Id',
        dataSource: '/Brand/List',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'Id', width: 56 },
            { field: 'Code', width: 100, sortable: true },
            { field: 'Name', width: 200, sortable: true },
            { title: '', field: 'Edit', width: 34, type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Edit', events: { 'click': Edit } },
            { title: '', field: 'Delete', width: 34, type: 'icon', icon: 'glyphicon-remove', tooltip: 'Delete', events: { 'click': Delete } }
        ],
        pager: { limit: 10, sizes: [5, 10, 20, 50] }
    });

    dialog = $('#dialog').dialog({
        uiLibrary: 'bootstrap',
        autoOpen: false,
        resizable: false,
        modal: true
    });

    $('#btnAdd').on('click', function () {
        $('#Id').val('');
        $('#Code').val('');
        $('#Name').val('');
        dialog.open('Add Brand');
        $('#Code').focus();
    });

    $('#btnSave').on('click', Save);

    $('#btnCancel').on('click', function () {
        dialog.close();
    });

    $('#btnSearch').on('click', function () {
        grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val() });
    });

    $('#btnClear').on('click', function () {
        $('#txtCode').val('');
        $('#txtName').val('');
        grid.reload({ code: '', name: '' });
    });
});