/**cart page script. */
var cart = (function () {
    "use strict";
    /**
     * cart script options.
     * */
    var options = {
        containerSelector: '.cart-page',
        upQuantitySelector: '.up-quantity',
        downQuantitySelector: '.down-quantity',
        cartSelector: '#cart-zone'
    };
    var Msgs = {
        OnRemoveFailure: "Error on removing product.",
        OnSetQuantityFailure: "Error on updating product quantity."
    };
    /**
     * Init cart javascript handler.
     */
    var init = function () {
        initCustomComponents();
        $(options.containerSelector).on('click', options.upQuantitySelector, upQuantity);
        $(options.containerSelector).on('click', options.downQuantitySelector, downQuantity);

    };
    /**
     * Init components.
     * */
    var initCustomComponents = function () {
    };

    var upQuantity = function () {
        var id = $(this).attr("data-id");
        $.ajax({
            type: "POST",
            url: ACTION.CART_UPQUANTITY,
            data: { id: id },
            success: function (data, status) {
                $(options.cartSelector).empty();
                $(options.cartSelector).html(data);
            },
            error: function (xhr, desc, err) {
                toastr.error(Msgs.OnSetQuantityFailure);
            }
        });
    };

    var downQuantity = function () {
        var id = $(this).attr("data-id");
        $.ajax({
            type: "POST",
            url: ACTION.CART_DOWNQUANTITY,
            data: { id: id },
            success: function (data, status) {
                $(options.cartSelector).empty();
                $(options.cartSelector).html(data);
            },
            error: function (xhr, desc, err) {
                toastr.error(Msgs.OnSetQuantityFailure);
            }
        });
    };
    /**
     * On remove item failure.
     * */
    var OnRemoveFailure = function () {
        toastr.error(Msgs.OnRemoveFailure)
    };

    // public methods.
    return {
        init: init,
        OnRemoveFailure: OnRemoveFailure
    };
})();
$(function () {
    cart.init();
});
