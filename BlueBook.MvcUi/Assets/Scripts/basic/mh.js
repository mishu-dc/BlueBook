
var tree;
var dialog;
var selectedNode;

function Edit(node) {
    $('#Id').val(node.id);
    $('#ParentId').val(node.parentId);
    $('#Code').val(node.code);
    $('#Name').val(node.name);
    dialog.open('Edit ' + node.name + " (" + node.type + ")");
}
function Save() {
    var node = {
        Id: $('#Id').val(),
        Code: $('#Code').val(),
        Name: $('#Name').val(),
        ParentId: $('#ParentId').val(),
        text: $('#Code').val() + "-" + $('#Name').val()
    };

    $.ajax({ url: '/MarketHierarchy/SaveAsync', data: { record: node }, method: 'POST' })
        .done(function (newNode) {
            dialog.close();
            if (node.Id === "0") {
                var parentNode = tree.getNodeById(node.ParentId);
                tree.addNode(newNode, parentNode);
            } else {
                tree.updateNode(node.Id, newNode);
            }
            var tNode = tree.getNodeById(node.Id);
            tree.unselect(tNode);
        })
        .fail(function () {
            alert('Failed to save.');
        });
}

function Delete(e) {
    if (selectedNode !== null && selectedNode!==undefined) {
        if (confirm('Are you sure to delete the entry?')) {
            $.ajax({ url: '/MarketHierarchy/DeleteAsync', data: { Id: selectedNode.id }, method: 'POST' })
                .done(function () {
                    var node = tree.getNodeById(selectedNode.id);
                    tree.removeNode(node);
                    selectedNode = null;
                })
                .fail(function () {
                    alert('Failed to delete.');
                });
        }
    }
}

$(document).ready(function () {
    tree = $('#tree').tree({
        uiLibrary: 'bootstrap',
        dataSource: '/MarketHierarchy/ListAsync',
        primaryKey: 'id',
        dataBound: function (e) {
            tree.expandAll();        
        }
    });

    tree.on('select', function (e, node, id) {
        selectedNode = tree.getDataById(id);
        if (node.text().trim().split('-')[1] !== undefined)
            selectedNode.name = node.text().trim().split('-')[1]
        if (node.text().trim().split('-')[0] !== undefined)
            selectedNode.code = node.text().trim().split('-')[0]
    });
    
    dialog = $('#dialog').dialog({
        uiLibrary: 'bootstrap',
        autoOpen: false,
        resizable: false,
        modal: true
    });

    $('#btnAdd').on('click', function () {
        if (selectedNode !== null && selectedNode !== undefined) {
            $('#Id').val('');
            $('#Code').val('');
            $('#Name').val('');
            $('#ParentId').val(selectedNode.id);
            dialog.open('Add chield under ' + selectedNode.name);
            $('#Code').focus();
        }
    });

    $('#btnEdit').on('click', function () {
        if (selectedNode !== null && selectedNode !== undefined) {
            Edit(selectedNode);
        }
    });

    $('#btnDelete').on('click', Delete);

    $('#btnSave').on('click', Save);

    $('#btnCancel').on('click', function () {
        dialog.close();
    });
});