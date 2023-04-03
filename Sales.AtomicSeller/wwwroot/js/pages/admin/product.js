/**product page script. */
var product = (function () {
    "use strict";
    /**
     * product script options.
     * */
    var options = {
        containerSelector: '.product-page',
        editSelector: '.edit-product',
        addSelector: '.add-product',
        deleteSelector: '.delete-product',
        searchInputSelector: '.search-product',
        rowSearchSelector: '.row-product',
        modalEditSelector: '#modal-product',
        itemSelectorPrefix: '#product-',
        itemListSelector: '#product-list'
    };
    var Msgs = {
        OnDeleteSuccess: 'Product deleted',
        OnDeleteFailure: 'Error on product delete',
        OnEditSuccess: 'Product updated',
        OnEditFailure: 'Error on product edit'
    };
    /**
     * Init product javascript handler.
     */
    var init = function () {
        initCustomComponents();
        $(document).on('keyup search', options.searchInputSelector, searchItems);
        $(options.containerSelector).on('click', options.editSelector, getItem);
        $(options.containerSelector).on('click', options.addSelector, getItem);
        $(options.containerSelector).on('click', options.deleteSelector, deleteItem);
    };
    /**
     * Init components.
     * */
    var initCustomComponents = function () {
       
    };
    /**
     * Search items.
     * */
    var searchItems = function () {
        var value = $(this).val().toLowerCase();
        $(options.rowSearchSelector).each(function (index) {
            var searchText = $(this).text().toLowerCase();

            if (searchText.indexOf(value) >= 0) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    };
    /**
     * Delete item
     * @param {any} e
     */
    var deleteItem = function (e) {
        e.stopPropagation();
        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");
        bootbox.confirm({
            message: "Delete " + name + "?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: "DELETE",
                        url: ACTION.PRODUCT_DELETE,
                        data: { id: id },
                        success: function (data, status) {
                            toastr.success(Msgs.OnDeleteSuccess);
                            $(options.itemSelectorPrefix + id).deleteElement();
                        },
                        error: function (xhr, desc, err) {
                            toastr.error(Msgs.OnDeleteFailure);
                        }
                    });
                }
            }
        });
    };
    /**
     * Get item.
     * */
    var getItem = function () {
        var id = $(this).attr("data-id");
        var url = ACTION.PRODUCT_EDIT + "?id=" + id;
        $.get(url, function (data, status) {
            $(options.modalEditSelector).find(".modal-title").text(id > 0 ? 'Edit Product' : 'Add Product');
            var modalBody = $(options.modalEditSelector).find(".modal-body");
            modalBody.empty();
            modalBody.html(data);
            initCustomComponents();
            $(options.modalEditSelector).modal('show');
        });
    };
    /**
     * On edit item success.
     * @param {any} response
     */
    var OnEditSuccess = function (response) {
        toastr.success(Msgs.OnEditSuccess)
        $(options.modalEditSelector).modal('hide');

        var url = ACTION.PRODUCT_ROW + "?id=" + response.value.id;
        $.get(url, function (data, status) {
            if ($(options.itemSelectorPrefix + response.value.id).length) {
                $(options.itemSelectorPrefix + response.value.id).empty();
                $(options.itemSelectorPrefix + response.value.id).append($($.parseHTML(data)).html());
            }
            else {
                $(options.itemListSelector).append(data);
            }
        });

    };
    /**
     * On edit item failure.
     * */
    var OnEditFailure = function () {
        toastr.error(Msgs.OnEditFailure)
    };

    // public methods.
    return {
        init: init,
        OnEditSuccess: OnEditSuccess,
        OnEditFailure: OnEditFailure
    };
})();
$(function () {
    product.init();
});
