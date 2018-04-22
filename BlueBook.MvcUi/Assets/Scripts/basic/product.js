
var grid;
var dialog;

function ClearValidation() {
    $("form[name = 'form-product']").validate().resetForm();
    $(".error").removeClass('error');
}

function Edit(e) {
    $('#Id').val(e.data.id);
    $('#Code').val(e.data.record.Code);
    $('#Name').val(e.data.record.Name);
    $('#Price').val(e.data.record.Price);
    $("#Brand").val(e.data.record.BrandId);
    ClearValidation();
    dialog.open('Edit Product');
}
function Save() {
    if (!$("form[name = 'form-product']").valid()) return;

    var record = {
        Id: $('#Id').val(),
        Code: $('#Code').val(),
        Name: $('#Name').val(),
        BrandId: $("#Brand").val(),
        Price: $('#Price').val(),
    };

    $.ajax({ url: '/Product/SaveAsync', data: { record: record }, method: 'POST' })
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
        $.ajax({ url: '/Product/DeleteAsync', data: { Id: e.data.id }, method: 'POST' })
            .done(function () {
                grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val(), brandId: $('#optBrand').val() });
            })
            .fail(function () {
                alert('Failed to delete.');
            });
    }
}

$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'Id',
        dataSource: '/Product/ListAsync',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'Id', width: 56 },
            { field: 'Code', width: 100, sortable: true },
            { field: 'Name', width: 200, sortable: true },
            { field: 'BrandName', width: 200, sortable: false },
            { field: 'Price', width: 200, sortable: false },
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

    $.ajax({ url: '/Search/GetBrands', method: 'POST' })
        .done(function (data) {
            var options = $("#Brand");
            $.each(data, function (key, value) {
                options.append(new Option(value.Name, value.Id));
            });

            var options = $("#optBrand");
            options.append(new Option("Select a brand", "-1"));
            $.each(data, function (key, value) {
                options.append(new Option( value.Name, value.Id));
            });
        })
        .fail(function () {
            console.log("failed to load brands");
        });
    
    $('#btnAdd').on('click', function () {
        $('#Id').val('');
        $('#Code').val('');
        $('#Name').val('');
        $('#Price').val('');
        ClearValidation();
        dialog.open('Add Product');
        $('#Code').focus();
    });

    $('#btnSave').on('click', Save);

    $('#btnCancel').on('click', function () {
        dialog.close();
    });

    $('#btnSearch').on('click', function () {
        grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val(), brandId: $('#optBrand').val() });
    });

    $('#btnClear').on('click', function () {
        $('#txtCode').val('');
        $('#txtName').val('');
        $("#optBrand").val(-1);

        grid.reload({ code: '', name: '', brandId:-1 });
    });
});