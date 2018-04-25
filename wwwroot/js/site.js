// import Cookies from "js-cookie";

/*!
 * JavaScript Cookie v2.2.0
 * https://github.com/js-cookie/js-cookie
 *
 * Copyright 2006, 2015 Klaus Hartl & Fagner Brack
 * Released under the MIT license
 */
; (function (factory) {
    var registeredInModuleLoader = false;
    if (typeof define === 'function' && define.amd) {
        define(factory);
        registeredInModuleLoader = true;
    }
    if (typeof exports === 'object') {
        module.exports = factory();
        registeredInModuleLoader = true;
    }
    if (!registeredInModuleLoader) {
        var OldCookies = window.Cookies;
        var api = window.Cookies = factory();
        api.noConflict = function () {
            window.Cookies = OldCookies;
            return api;
        };
    }
}(function () {
    function extend() {
        var i = 0;
        var result = {};
        for (; i < arguments.length; i++) {
            var attributes = arguments[i];
            for (var key in attributes) {
                result[key] = attributes[key];
            }
        }
        return result;
    }

    function init(converter) {
        function api(key, value, attributes) {
            var result;
            if (typeof document === 'undefined') {
                return;
            }

            // Write

            if (arguments.length > 1) {
                attributes = extend({
                    path: '/'
                }, api.defaults, attributes);

                if (typeof attributes.expires === 'number') {
                    var expires = new Date();
                    expires.setMilliseconds(expires.getMilliseconds() + attributes.expires * 864e+5);
                    attributes.expires = expires;
                }

                // We're using "expires" because "max-age" is not supported by IE
                attributes.expires = attributes.expires ? attributes.expires.toUTCString() : '';

                try {
                    result = JSON.stringify(value);
                    if (/^[\{\[]/.test(result)) {
                        value = result;
                    }
                } catch (e) { }

                if (!converter.write) {
                    value = encodeURIComponent(String(value))
                        .replace(/%(23|24|26|2B|3A|3C|3E|3D|2F|3F|40|5B|5D|5E|60|7B|7D|7C)/g, decodeURIComponent);
                } else {
                    value = converter.write(value, key);
                }

                key = encodeURIComponent(String(key));
                key = key.replace(/%(23|24|26|2B|5E|60|7C)/g, decodeURIComponent);
                key = key.replace(/[\(\)]/g, escape);

                var stringifiedAttributes = '';

                for (var attributeName in attributes) {
                    if (!attributes[attributeName]) {
                        continue;
                    }
                    stringifiedAttributes += '; ' + attributeName;
                    if (attributes[attributeName] === true) {
                        continue;
                    }
                    stringifiedAttributes += '=' + attributes[attributeName];
                }
                return (document.cookie = key + '=' + value + stringifiedAttributes);
            }

            // Read

            if (!key) {
                result = {};
            }

            // To prevent the for loop in the first place assign an empty array
            // in case there are no cookies at all. Also prevents odd result when
            // calling "get()"
            var cookies = document.cookie ? document.cookie.split('; ') : [];
            var rdecode = /(%[0-9A-Z]{2})+/g;
            var i = 0;

            for (; i < cookies.length; i++) {
                var parts = cookies[i].split('=');
                var cookie = parts.slice(1).join('=');

                if (!this.json && cookie.charAt(0) === '"') {
                    cookie = cookie.slice(1, -1);
                }

                try {
                    var name = parts[0].replace(rdecode, decodeURIComponent);
                    cookie = converter.read ?
                        converter.read(cookie, name) : converter(cookie, name) ||
                        cookie.replace(rdecode, decodeURIComponent);

                    if (this.json) {
                        try {
                            cookie = JSON.parse(cookie);
                        } catch (e) { }
                    }

                    if (key === name) {
                        result = cookie;
                        break;
                    }

                    if (!key) {
                        result[name] = cookie;
                    }
                } catch (e) { }
            }

            return result;
        }

        api.set = api;
        api.get = function (key) {
            return api.call(api, key);
        };
        api.getJSON = function () {
            return api.apply({
                json: true
            }, [].slice.call(arguments));
        };
        api.defaults = {};

        api.remove = function (key, attributes) {
            api(key, '', extend(attributes, {
                expires: -1
            }));
        };

        api.withConverter = init;

        return api;
    }

    return init(function () { });
}));


// function setCookies(){
//     if(cookiesValue == []){
//         Cookies.set('ddsCookie', cookiesValue)
//     }
// }

var cookiesValueDefined = false;

function defineCookiesValues(){
    if(!cookiesValueDefined){
        cookiesValue = [];
        cookiesValueDefined = true;
    }
}

$(document).ready(function () {
    defineCookiesValues();
    var $windowSize = 5000;

    if ($(window).width() < 782) {
        $('#categories-collapse').collapse("hide");
    }

    $('a').mouseup(function() { $(this).blur() })
    window.onresize = function(){
        if ( $(window).width() < 782 && $windowSize > $(window).width()) {      
            $('#categories-collapse').collapse("hide");
        } 
        else {
            $('#categories-collapse').collapse("show");
        }
        $windowSize = $(window).width();
    }

    console.log("javascript loaded");
    $('.categories tr').click(function (event) {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }
    });

    $("input[type='checkbox']").change(function (e) {
        if ($(this).is(":checked")) {
            $(this).parent().addClass("highlight_row");
        } else {
            $(this).parent().removeClass("highlight_row");
        }
    });

    var modal = $('#myModal');

    $('.item-image').on('click', function () {
        price = $(this).parent().children('.caption').children('.row').find('.price').text(); //.parent().prev().find('.price').text();
        productTitle = $(this).parent().children('.caption').children('.product-title').text(); //.$(this).parent().parent().prev().text();
        productDesc = $(this).parent().children('.caption').children('.product-desc').text();
        id = $(this).parent().children('.caption').children('.row').find('.id').text()
        //console.log($(this).parent().parent().prev());
        img = $(this).attr('src'); //parent().parent().parent().prev().attr('src');


        modalContent = modal.find('.modal-content');
        modalContent.find('.modal-image').attr('src', img);
        modalContent.find('.modal-item-price').text(price);
        modalContent.find('.modal-item-title').text(productTitle);
        modalContent.find('.modal-item-desc').text(productDesc);
        modalContent.find('.modal-item-id').text(id);

        modal.css("display", "block");
    });

    $('.modal-cart-button').on('click', () => {
        console.log("here I am what?????");
        var $id = parseInt($.trim($(".modal-item-id").text()));
        // Cookies.set('ddsCookie', cookiesValue);
        // var expiration_date = new Date();
        // var cookie_string = '';
        // expiration_date.setFullYear(expiration_date.getFullYear() + 1);
        cookiesValue.push($id);
        Cookies.set('ddsCookie', cookiesValue);
        console.log("ddsCookie set successfully");

    });

    $('#cart-button').on('click', () => {
        cookieContents = Cookies.get('ddsCookie');
        console.log(typeof cookieContents);
        console.log(cookieContents);
    })

    $('.close').on('click', function () {
        modal.css("display", "none");
    });

    $('.alt-image').on('click', function () {
        console.log("image clicked");
        img = $(this).children('img').attr('src');
        modal.find('.modal-content').find('.modal-image').attr('src', img);
    });


    window.onclick = function (event) {
        if (event.target == document.getElementById('myModal')) {
            document.getElementById('myModal').style.display = "none";
        }
    } 
});


