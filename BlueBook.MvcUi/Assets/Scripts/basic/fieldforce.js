
var grid;
var ffDialog;
var mhDialog;
var tree;
var selectedData;

function ClearValidation() {
    $("form[name = 'form-ff']").validate().resetForm();
    $(".error").removeClass('error');
}

function Edit(e) {
    var record = e.data.record;

    ClearValidation();

    $('#Id').val(record.Id);
    $('#Code').val(record.Code);
    $('#Name').val(record.Name);
    $('#Phone').val(record.Phone);
    $('#Email').val(record.Email);
    $("#AddressLine1").val(record.AddressLine1);
    $("#AddressLine2").val(record.AddressLine2);
    $('#City').val(record.City);
    $("#State").val(record.State);
    $("#ZipCode").val(record.ZipCode);

    $.each(record.DistributorIds, function (i, l) {
        $("input[type=checkbox][value="  + l + "]").prop("checked", true);    
    });

    var tNode = tree.getNodeById(record.MarketHierarchyId);
    if (tNode !==undefined){
        tree.select(tNode);
        $('input[name=hdMhId]').val(record.MarketHierarchyId);
        $(".mhTree").text(tNode[0].textContent);
    }
    
    ffDialog.open('Edit Field Force');
}

function Save() {
    if (!$("form[name = 'form-ff']").valid()) return;

    var ids = [];
    $('#Distributors :checked').each(function () {
        ids.push($(this).val());
    });

    var record = {
        Id: $('#Id').val(),
        Code: $('#Code').val(),
        Name: $('#Name').val(),
        Phone: $('#Phone').val(),
        Email: $('#Email').val(),
        AddressLine1: $('#AddressLine1').val(),
        AddressLine2: $('#AddressLine2').val(),
        State: $('#State').val(),
        City: $('#City').val(),
        ZipCode: $('#ZipCode').val(),
        MarketHierarchyId: $("input[name=hdMhId]").val(),
        DistributorIds: ids
    };

    $.ajax({ url: '/Fieldforce/SaveAsync', data: { record: record }, method: 'POST' })
        .done(function () {
            ffDialog.close();
            grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val(), distributorId: $('#optDristributor').val() });
        })
        .fail(function () {
            alert('Failed to save.');
        });
}

function Delete(e) {
    if (confirm('Are you sure to delete the entry?')) {
        $.ajax({ url: '/Fieldforce/DeleteAsync', data: { Id: e.data.id }, method: 'POST' })
            .done(function () {
                grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val(), distributorId: $('#optDristributor').val() });
            })
            .fail(function () {
                alert('Failed to delete.');
            });
    }
}

$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'Id',
        dataSource: '/FieldForce/ListAsync',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'Id', width: 56 },
            { field: 'Code', width: 100, sortable: true },
            { field: 'Name', width: 200, sortable: true },
            { field: 'Distributors', width: 200, sortable: false },
            { field: 'MarketHierarchy', title:'Market Hierarcy', width: 200, sortable: false },
            { title: '', field: 'Edit', width: 34, type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Edit', events: { 'click': Edit } },
            { title: '', field: 'Delete', width: 34, type: 'icon', icon: 'glyphicon-remove', tooltip: 'Delete', events: { 'click': Delete } }
        ],
        pager: { limit: 10, sizes: [5, 10, 20, 50] }
    });

    ffDialog = $('#ffDialog').dialog({
        uiLibrary: 'bootstrap',
        draggable: false,
        autoOpen: false,
        resizable: false,
        modal: true,
        width: 500,
        height: 800,
        scrollable: true,
        closeOnEscape: false
    });

    mhDialog = $('#mhDialog').dialog({
        uiLibrary: 'bootstrap',
        autoOpen: false,
        resizable: false,
        draggable: true,
        modal: true,
        width: 400,
        height: 400,
        scrollable: true,
        closeOnEscape: true,
        closeButtonInHeader: false
    });

    $.ajax({ url: '/Search/GetDistributors', method: 'POST' })
        .done(function (data) {
            var options = $("#Distributors");
            $.each(data, function (key, value) {
                options.append($('<li></li>').append($('<input>').attr({ type: 'checkbox', name: 'distributors', value: value.Id, id: value.Id }))
                    .append($('<label>').attr({ for: value.Name }).text(this.Name)));
            });

            options = $("#optDristributor");
            options.append(new Option("Select a distributor", "-1"));
            $.each(data, function (key, value) {
                options.append(new Option(value.Name, value.Id));
            });
        })
        .fail(function () {
            console.log("failed to load distributors");
        });

    tree = $('#tree').tree({
        uiLibrary: 'bootstrap',
        dataSource: '/MarketHierarchy/ListAsync',
        primaryKey: 'id',
        dataBound: function (e) {
            tree.expandAll();
        }
    });

    tree.on('select', function (e, node, id) {
        selectedData = tree.getDataById(id);
    });

    $(".mhTree").on('click', function () {
        mhDialog.open('Market Hierarchy');
    });

    $("#btnClose").on('click', function () {
        mhDialog.close();
    });

    $("#btnSelect").on('click', function () {
        mhDialog.close();
        if (selectedData !== undefined) {
            $('input[name=hdMhId]').val(selectedData.id);
            $(".mhTree").text(selectedData.text);
        }
    });

    $('#btnAdd').on('click', function () {
        $('#Id').val('');
        $('#Name').val('');
        $('#Code').val('');
        $('#Phone').val('');
        $('#Email').val('');
        $("#AddressLine1").val('');
        $("#AddressLine2").val('');
        $("#State").val('');
        $("#ZipCode").val('');
        ClearValidation();
        ffDialog.open('Add Field Force');
        $('#Code').focus();
    });

    $('#btnSave').on('click', Save);

    $('#btnCancel').on('click', function () {
        ffDialog.close();
    });

    $('#btnSearch').on('click', function () {
        grid.reload({ code: $('#txtCode').val(), name: $('#txtName').val(), distributorId: $('#optDristributor').val() });
    });

    $('#btnClear').on('click', function () {
        $('#txtCode').val('');
        $('#txtName').val('');
        $("#optDristributor").val(-1);

        grid.reload({ code: '', name: '', distributorId: $('#optDristributor').val()  });
    });
});