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


function defineCookiesValues(){
    cookiesValue1 = Cookies.get("ddsCookie");
    cookiesValue = parseCookieResults(cookiesValue1);
    if(cookiesValue == null){
        cookiesValue = [];
    }
}

function parseCookieResults(l){
    console.log(l);
    if (l){
        return JSON.parse(l);
    } else {
        return [];
    }
}

function without(array, what){
    return array.filter(function(element){ 
        return element !== what;
    });
}

function deleteItem(row, id){
    var i = row.parentNode.parentNode.rowIndex;
    document.getElementById("cartTable").deleteRow(i);
    var temp = Cookies.get("ddsCookie");
    cookiesValue = parseCookieResults(temp);
    var index = cookiesValue.indexOf(id);
    console.log("cookiesValue before removing: " + cookiesValue);
    console.log("this is my index: " + index);
    console.log("this is the id to remove: " + id);
    cookiesValue = without(cookiesValue, id);
    // if (index > -1) {
    //     cookiesValue.splice(index, 1);
    // }
    console.log("cookiesValue after removing: " + cookiesValue);
    Cookies.set("ddsCookie", cookiesValue);
    location.reload();
    setBadge();

}

Array.prototype.unique = function() {
    return this.filter(function (value, index, self) { 
      return self.indexOf(value) === index;
    });
  }

  function countCookies() {
      if (Cookies.get("ddsCookie") == null) {
          return 0;
      }
      var cookie = Cookies.get("ddsCookie");
      var c = parseCookieResults(cookie);
      return c.unique().length;
  }

  function setBadge() {
      var b = document.getElementById("target");
      var c = countCookies();
      if (c == 0) {
          b.style.visibility = 'hidden';
      } else {
          b.style.visibility = 'visible';
          b.innerHTML = parseInt(c);
      }
  }

function clearCart(){
    $("#cartTable > tbody").empty();
    cookiesValue = [];
    Cookies.set("ddsCookie", cookiesValue);
    setBadge();
    location.reload();

}


function incrementValue() {
    var x = document.getElementById("target");
    x.style.visibility = 'visible';
    // if (x.innerHTML == 0) {
    //     x.style.visability = 'visible';
    // }
  x.innerHTML = parseInt(x.innerHTML)+1;
}

$(document).ready(function () {
    defineCookiesValues();
    setBadge();
    // cookiesValue = [];
    // Cookies.set("ddsCookie", cookiesValue);
    var $windowSize = 5000;

    // function triggerPulse() {
    //     var t = document.getElementById("targetEl");
    //     t.classList.add("set-ripple");
        
    //     setTimeout(function() {
    //     t.classList.remove('set-ripple');
    //   }, 600);
        
    //     setTimeout(function() {
    //         t.classList.add("add-new-color");
    //   }, 100);
        
    // };

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
        console.log("clicked");
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

    $('.bounding-box.item-image').on('click', function () {
        price = $(this).attr('p-price');
        productTitle = $(this).attr('p-title');
        productDesc = $(this).attr('p-desc');
        id = $(this).attr('p-id');
        //console.log($(this).parent().parent().prev());
        img = $(this).attr('p-image'); //parent().parent().parent().prev().attr('src');
        img_display = $(this).attr('style')
        urlArray = new Array();
        $(this).parent().children('.urls').each(function () {
            url = $(this).text();
            urlArray.push(url);
        })

        modalContent = modal.find('.modal-content');
        modalContent.find('.modal-image').attr('style', img_display);
        //modalContent.find('.modal-image').attr('src', img);
        modalContent.find('.modal-item-price').text(price);
        modalContent.find('.modal-item-title').text(productTitle);
        modalContent.find('.modal-item-desc').text(productDesc);
        modalContent.find('.modal-item-id').text(id);

        //populate image on modal
        $list = modalContent.find('.list-inline');
        if (urlArray.length > 1) {
            for (var i = 0; i < urlArray.length; i++) {

                alt_img_display = 'background-image: url(' + urlArray[i] + ')';
                console.log(urlArray[i]);
                $list.append('<li class="alt-image"> <img class="bounding-box modal-alt-image" style="background-image: url(' + urlArray[i] + ')"' + ' /></li >');
            }
        }

        modal.css("display", "block");
    });

    $('.modal-cart-button').on('click', () => {
        // triggerPulse;
        
        var $id = parseInt($.trim($(".modal-item-id").text()));
        cookiesValue.push($id);
        Cookies.set('ddsCookie', cookiesValue);
        setBadge();
        console.log("ddsCookie set successfully");
        // console.log(parseCookieResults(Cookies.get("ddsCookie")));
        // console.log(countCookies());
    });

    $('.close').on('click', function () {
        modalContent = modal.find('.modal-content');
        $list = modalContent.find('.list-inline');
        $list.empty();
        modal.css("display", "none");
    });

    $('.list-inline').on('click', '.alt-image', function () {
        console.log("image clicked");
        img_display = $(this).children('img').attr('style');
        modal.find('.modal-content').find('.modal-image').attr('style', img_display);
    });


    window.onclick = function (event) {
        if (event.target == document.getElementById('myModal')) {
            modalContent = modal.find('.modal-content');
            $list = modalContent.find('.list-inline');
            $list.empty();
            document.getElementById('myModal').style.display = "none";
        }
    }

    $(".selectable").selectable({
        selected: function () {
            var string = "";
            $(".selectable img").each(function (index) {
                if ($(this).hasClass("ui-selected")) {
                    string += index + ",";
                    $("#index").val(string);
                }
            });
        }
    });
});

function post(path, params) {
    method = "GET"; 
    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    var securityToken = $('[name=__RequestVerificationToken]').val();
    console.log("securityToken + " + securityToken );
    form.setAttribute("method", method);
    form.setAttribute("action", path);
    form.setAttribute("__RequestVerificationToken", securityToken);
    for(var key in params) {
        if(params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);
            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}

