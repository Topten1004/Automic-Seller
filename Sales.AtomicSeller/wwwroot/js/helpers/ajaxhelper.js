(function (define) {
    define(['jquery'], function ($) {
        return (function () {
            var ajaxhelper = {
                Get: get_request,
                Post: post_request,
                Delete: delete_request,
                Put: put_request
            };
            return ajaxhelper;

            function send_request(url, type, dataType, data, successCallback, async = true) {
                var ajaxCall = $.ajax({
                    url: url,
                    type: type,
                    dataType: dataType,
                    data: data,
                    async: async
                });
                ajaxCall.done(successCallback);
                ajaxCall.fail(function (jqXHR, textStatus, errorThrown) {
                    toastr.error(jqXHR.responseJSON.value.message, textStatus, { timeOut: 30000 });
                });
            }

            function get_request(url, data, callback, dataType = 'json', async = true) {
                send_request(url, 'GET', dataType, data, callback, async);
            }

            function post_request(url, data, callback, dataType = 'json', async = true) {
                send_request(url, 'POST', dataType, data, callback, async);
            }

            function delete_request(url, data, callback, dataType = 'json', async = true) {
                send_request(url, 'DELETE', dataType, data, callback, async);
            }

            function put_request(url, data, callback, dataType = 'json', async = true) {
                send_request(url, 'PUT', dataType, data, callback, async);
            }

        })();
    });
}(typeof define === 'function' && define.amd ? define : function (deps, factory) {
    if (typeof module !== 'undefined' && module.exports) { 
        module.exports = factory(require('jquery'));
    } else {
        window.ajaxhelper = factory(window.jQuery);
    }
}));
