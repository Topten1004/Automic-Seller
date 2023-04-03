/**editModal page script. */
var editModal = (function () {
    "use strict";
    /**
     * editModal script options.
     * */
    var options = {

    };

    /**
     * Init editModal javascript handler.
     */
    var init = function () {
        //initCustomComponents();
        $(document).on('shown.bs.modal', ".kc-modal-edit", validate);
    };
    /**
     * Init components.
     * */
    var initCustomComponents = function () {

    };
    /**
     * validate.
     * */
    var validate = function () {
        /*$.validator.unobtrusive.parse("#edit-modal-form");*/
        var form = $(this).find('form');
        $.validator.unobtrusive.parse(form);
    };

    // public methods.
    return {
        init: init
    };
})();
$(function () {
    editModal.init();
});
