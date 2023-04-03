/**jqueryExtensions script. */
var jqueryExtensions = (function () {
    "use strict";
    $.fn.deleteElement = function () {
        var element = $(this);
        element.css('background-color', 'pink');
        element.fadeOut("slow", function () {
            element.remove();
        });
    };
})();
$(function () { });
