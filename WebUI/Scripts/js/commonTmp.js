﻿(function (e) { function t() { var e = document.createElement("input"), t = "onpaste"; return e.setAttribute(t, ""), "function" == typeof e[t] ? "paste" : "input" } var n, a = t() + ".mask", r = navigator.userAgent, i = /iphone/i.test(r), o = /android/i.test(r); e.mask = { definitions: { 9: "[0-9]", a: "[A-Za-z]", "*": "[A-Za-z0-9]" }, dataName: "rawMaskFn", placeholder: "_" }, e.fn.extend({ caret: function (e, t) { var n; if (0 !== this.length && !this.is(":hidden")) return "number" == typeof e ? (t = "number" == typeof t ? t : e, this.each(function () { this.setSelectionRange ? this.setSelectionRange(e, t) : this.createTextRange && (n = this.createTextRange(), n.collapse(!0), n.moveEnd("character", t), n.moveStart("character", e), n.select()) })) : (this[0].setSelectionRange ? (e = this[0].selectionStart, t = this[0].selectionEnd) : document.selection && document.selection.createRange && (n = document.selection.createRange(), e = 0 - n.duplicate().moveStart("character", -1e5), t = e + n.text.length), { begin: e, end: t }) }, unmask: function () { return this.trigger("unmask") }, mask: function (t, r) { var c, l, s, u, f, h; return !t && this.length > 0 ? (c = e(this[0]), c.data(e.mask.dataName)()) : (r = e.extend({ placeholder: e.mask.placeholder, completed: null }, r), l = e.mask.definitions, s = [], u = h = t.length, f = null, e.each(t.split(""), function (e, t) { "?" == t ? (h--, u = e) : l[t] ? (s.push(RegExp(l[t])), null === f && (f = s.length - 1)) : s.push(null) }), this.trigger("unmask").each(function () { function c(e) { for (; h > ++e && !s[e]; ); return e } function d(e) { for (; --e >= 0 && !s[e]; ); return e } function m(e, t) { var n, a; if (!(0 > e)) { for (n = e, a = c(t); h > n; n++) if (s[n]) { if (!(h > a && s[n].test(R[a]))) break; R[n] = R[a], R[a] = r.placeholder, a = c(a) } b(), x.caret(Math.max(f, e)) } } function p(e) { var t, n, a, i; for (t = e, n = r.placeholder; h > t; t++) if (s[t]) { if (a = c(t), i = R[t], R[t] = n, !(h > a && s[a].test(i))) break; n = i } } function g(e) { var t, n, a, r = e.which; 8 === r || 46 === r || i && 127 === r ? (t = x.caret(), n = t.begin, a = t.end, 0 === a - n && (n = 46 !== r ? d(n) : a = c(n - 1), a = 46 === r ? c(a) : a), k(n, a), m(n, a - 1), e.preventDefault()) : 27 == r && (x.val(S), x.caret(0, y()), e.preventDefault()) } function v(t) { var n, a, i, l = t.which, u = x.caret(); t.ctrlKey || t.altKey || t.metaKey || 32 > l || l && (0 !== u.end - u.begin && (k(u.begin, u.end), m(u.begin, u.end - 1)), n = c(u.begin - 1), h > n && (a = String.fromCharCode(l), s[n].test(a) && (p(n), R[n] = a, b(), i = c(n), o ? setTimeout(e.proxy(e.fn.caret, x, i), 0) : x.caret(i), r.completed && i >= h && r.completed.call(x))), t.preventDefault()) } function k(e, t) { var n; for (n = e; t > n && h > n; n++) s[n] && (R[n] = r.placeholder) } function b() { x.val(R.join("")) } function y(e) { var t, n, a = x.val(), i = -1; for (t = 0, pos = 0; h > t; t++) if (s[t]) { for (R[t] = r.placeholder; pos++ < a.length; ) if (n = a.charAt(pos - 1), s[t].test(n)) { R[t] = n, i = t; break } if (pos > a.length) break } else R[t] === a.charAt(pos) && t !== u && (pos++, i = t); return e ? b() : u > i + 1 ? (x.val(""), k(0, h)) : (b(), x.val(x.val().substring(0, i + 1))), u ? t : f } var x = e(this), R = e.map(t.split(""), function (e) { return "?" != e ? l[e] ? r.placeholder : e : void 0 }), S = x.val(); x.data(e.mask.dataName, function () { return e.map(R, function (e, t) { return s[t] && e != r.placeholder ? e : null }).join("") }), x.attr("readonly") || x.one("unmask", function () { x.unbind(".mask").removeData(e.mask.dataName) }).bind("focus.mask", function () { clearTimeout(n); var e; S = x.val(), e = y(), n = setTimeout(function () { b(), e == t.length ? x.caret(0, e) : x.caret(e) }, 10) }).bind("blur.mask", function () { y(), x.val() != S && x.change() }).bind("keydown.mask", g).bind("keypress.mask", v).bind(a, function () { setTimeout(function () { var e = y(!0); x.caret(e), r.completed && e == x.val().length && r.completed.call(x) }, 0) }), y() })) } }) })(jQuery);


jQuery(function ($) {
    $("#ShippingPhone").mask("9 (999) 999-99-99");
    $("#Phone").mask("9 (999) 999-99-99");
});

/*Navigator*/
template_fileExt = 'html';
// Template setup
$(document).ready(function () {/*
	// Highlight
	$('#highlights li').hover(function()
	{
		$(this).css('background-position', '0 0').animate({backgroundPosition:'0 -300px'}, 500);
	}, function()
	{
		$(this).css('background-position', '0 -300px').animate({backgroundPosition:'0 0'}, 500);
	});
	*/
    // Nav
    var nav = $('.nav.nav-menu');
    var navElements = nav.children('li');
    navElements.hover(function () {
        $(this).children('a').css({ 'padding-left': '7px', 'padding-right': '17px' }).animate({ paddingLeft: '17px', paddingRight: '7px' }, 100);
    }, function () {
        $(this).children('a').css({ 'padding-left': '17px', 'padding-right': '7px' }).animate({ paddingLeft: '7px', paddingRight: '7px' }, 100);
    });

    // Nav elements with submenu
    var navChildren = navElements.children('ul');
    navChildren.parent().each(function (i) {
        this._height = $(this).height();
        var a = $(this).children('a');
        a.get(0)._opened = false;
        this._aHeight = (parseInt(a.height()) + 17) + 'px';
        $(this).css('height', this._aHeight);
    }).children('a').click(function (e) {
        var a = $(this);
        var li = a.parent();
        var liNode = li.get(0);
        if (!this._opened) {
            e.preventDefault();
            li.animate({ height: liNode._height }).parent().find('li ul').parent().each(function () {
                if (this != liNode) {
                    $(this).animate({ height: this._aHeight }).children('a').get(0)._opened = false;
                }
            });
            this._opened = true;
        }
    });

    // Sub-nav
    navChildren.children('li').children('a').hover(function () {
        $(this).css({ 'padding-left': '0', 'padding-right': '15px', 'border-left-width': '0' }).animate({ paddingLeft: '5px', paddingRight: '10px' }, 100).animate({ borderLeftWidth: '5px', paddingRight: '5px' }, 100);
    }, function () {
        $(this).css({ 'padding-left': '5px', 'padding-right': '5px', 'border-left-width': '5px' }).animate({ paddingLeft: '0', borderLeftWidth: '0', paddingRight: '15px' }, 100);
    });


    $(function () {
        $('li li').click(function () {
            $('li li').removeClass('current');
            $(this).addClass('current');
        });
    });


    // Forms input field auto-tip
    $('#search-form input, .form input[type=text], .form textarea').focus(function () {
        var input = $(this);
        var title = input.attr('title');
        input.removeClass('input-tip');
        if (title && input.val() == title) {
            input.val('');
        }
    }).blur(function () {
        var input = $(this);
        var title = input.attr('title');
        if (title && (input.val() == '' || input.val() == title)) {
            input.val(title);
            input.addClass('input-tip');
        }
    }).blur();

    // Prevent tips to be submitted
    $('#search-form, .form').submit(function () {
        $(this).find('input[type=text]').each(function (i) {
            var input = $(this);
            var title = input.attr('title');
            if (title && input.val() == title) {
                input.val('');
            }
        });
    });

    /*
    * Navigation
    */

    // Detection of current page if none specified
    var current = nav.find('.current');
    if (current.length == 0) {
        // Detects from current url, with hash
        var selection = navElements.find('a');
        var href = document.location.href;
        if (href.substr(-1, 1) == '#') {
            href = href.substr(0, href.length - 1);
        }
        if ($.browser.safari) {
            href = href.replace(' ', '%20'); 		// Webkit leaves spaces unencoded in document.location	
        }
        var found = template_markLinks(selection, href);
        if (!found) {
            // If hash, remove it
            if (document.location.hash && document.location.hash.length > 0) {
                href = document.location.href.substr(0, document.location.href.length - document.location.hash.length);
                found = template_markLinks(selection, href);
            }
        }
        if (!found) {
            // If folder, search with index.html
            if (document.location.pathname.substr(-1, 1) == '/') {
                found = template_markLinks(selection, href + 'index.' + template_fileExt);
            }
        }

        // Reload selection
        if (found) {

            current = nav.find('.current');
        }
    }

    // If current
    if (current.length > 0) {
        current.each(function (i) {
            // Check level
            var element = $(this);
            var parent = element.parent().parent();
            if (parent.get(0).nodeName.toLowerCase() == 'li') {
                element = parent;
            }
            element.children('a').click();
        });
    }


});

function template_markLinks(selection, url) {
    var found = false;
    selection.each(function () {
        if (this.href === url) {
            $(this).parent().addClass('current');
            found = true;
        }
    });
    return found;
}
/*End navigator*/

/*Social */
$(document).ready(function () {
    $(".btn-slide").click(function () {
        if ($("#openCloseIdentifier").is(":hidden")) {
            $("#social").animate({ bottom: "0" }, 450);
            $(this).addClass("active");
            $("#openCloseIdentifier").show();
            $("#social-tab a span").removeClass("fa-plus-circle");
            $("#social-tab a span").addClass("fa-arrow-circle-down");
        } else {
            $("#social").animate({ bottom: "-60px" }, 450);
            $(this).removeClass("active");
            $("#openCloseIdentifier").hide();
            $("#social-tab a span").removeClass("fa-arrow-circle-down");
            $("#social-tab a span").addClass("fa-plus-circle");
        }
    });
});
/*End social*/


function Common() {
    _this = this;

    this.init = function() {
        /*$(".logonEnter").click(function (event) {
         //   alert('!!');
            event.preventDefault();
            _this.showPopup("/Account/AjaxUserEnter", initLoginPopup);
        });*/

        $(".feedback").click(function (event) {
            event.preventDefault();
            _this.showPopup("/Account/FeedBackModal", initFeedBackPopup);
        });

        $(".user-account-edit").click(function () {
            _this.showPopupAccount("/Account/UserAccountEdit", initProfilePopup);
        });

  /*      $(".change-password").click(function () {
            alert('!!');
            _this.showPopup("/Account/ChangePassword", initChangePasswordPopup);
        });*/
    }
    
    
    this.showPopup = function (url, callback) {
        $.ajax({
            type: "GET",
            url: url,
            onbegin: gifLoaderBefore(),
            success: function (data) {
                gifLoaderAfter();
                showModalData(data, callback);
            },

        });
    };

    this.showPopupAccount = function (url, callback) {
        $.ajax({
            type: "GET",
            url: url,
            onbegin: gifLoaderBefore(),
            success: function (data) {
                gifLoaderAfter();
                showModalData(data, callback);
            },

        });
    };

  
    this.showPopupPreview = function (url, callback) {
    $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                showModalData(data, callback);
            } 
        });
    }
    
  /*  function initLoginPopup(modal) {
        $("#LoginButton").click(function () {

            $("#loadingElement").css('display', 'inline');

            $.ajax({
                type: "POST",
                url: "/Account/AjaxUserEnter",
                data: $("#updateLoginForm").serialize(),
                success: function (data) {
                    // showModalData(data);
                    initLoginPopup(modal);
                    updateSuccess(data);
                    loadingElementInterrupt();
                }
            });
        });
    }
    */

    function initProfilePopup(modal) {
        $("#AccountEditButton").click(function () {
            $.ajax({
                type: "POST",
                url: "/Account/UserAccountEdit",
                data: $("#updateAccountEditForm").serialize(),
                success: function (data) {
                    //  showModalData(data);
                    initProfilePopup(modal);
                    updateSuccessProfile(data);
                   
                }
                
            });
        });
    }


    /*function initChangePasswordPopup(modal) {
        $("#ChangePasswordButton").click(function () {
            $.ajax({
                type: "POST",
                url: "/Account/ChangePassword",
                data: $("#updateChangePasswordForm").serialize(),
                success: function (data) {
                    //  showModalData(data);
                    initChangePasswordPopup(modal);
                    updateSuccessProfile(data);
                }
            });
        });
    }*/
    

    function loadingElementInterrupt() {
        $("#loadingElement").css('display', 'none');
    }

    function initFeedBackPopup(modal) {
        $("#FeedBackButton").click(function () {
            $("#loadingElement").css('display', 'inline');
                            $.ajax({
                                type: "POST",
                                url: "/Account/FeedBackModal",
                                data: $("#FeedBackForm").serialize(),
                                success: function (data) {
                                    // showModalData(data);
                                    initFeedBackPopup(modal);
                                    updateSuccessFeedBack(data);
                                    loadingElementInterrupt();
                                }
                            });
                        });
                    }

    function showModalData(data, callback) {
        $(".modal-backdrop").remove();
        var popupWrapper = $("#updateDialog");
        popupWrapper.empty();
        try {
            popupWrapper.html(data);     
        } catch(e) {
            popupWrapper.html(data);  
        } 
        var popup = $(".modal", popupWrapper);
        $(".modal", popupWrapper).modal();
        if (callback != undefined) {
            callback(popup);
        }
    }
    
    function updateSuccess(data) {
        if (data.Success == true) {
            window.location.reload();
        } else {
            if (data.ErrorMessage == "Capcha") {
                window.location.replace("/Account/UserEnter");
            } else {
                $("#update-message").html(data.ErrorMessage);
                $("#update-message").show();    
            }
        }
    }

    function updateSuccessProfile(data) {
        if (data.Success == true) {
            //window.location.replace("/Product/List");
            window.location.reload();
        } else {
            $("#update-message2").html(data.ErrorMessage);
            $("#update-message2").show();
        }
    }


    function updateSuccessFeedBack(data) {

        if (data.Success == true) {
            //window.location.replace("/Product/List");
            alert('Спасибо Ваш запрос отправлен! Мы обязательно свяжемся с Вами в ближайшее время!');
            window.location.reload();
        } else {
             {
                $("#update-message").html(data.ErrorMessage);
                $("#update-message").show();
            }
        }
    }

  
}


var common = null;
$().ready(function () {
    common = new Common();
    common.init();
});

/*
$(document).ready(function() {
    $(function() {
        // слайд-шоу
        $('.carousel').carousel({
                interval: 2000,
                pause: 'hover',
                wrap: true
            }
        );
        $('#the-carouse').carousel();
    });
}); 
*/


  function duration() {
                $("#loading-element-1").addClass('fa-5x');

            }


  $(document).ready(function() {

      $('.quickview').each(function(index, element) {
          var width1 = $(this).width(); //$(this).parent().parent();
          var width2 = $(this).prev('.image').width();
          //alert(width2);
          var tmp = Math.round((width1 - width2) / 2);
          $(this).width(width2);
          $(this).css('margin-left', tmp + "px");
      });

  });

            
       
       /*     
          if (view == 'list') {
              display(view);
          }     
       */


       $(function () {
               var menu = $('.menus'),
               a = menu.find('a');
               a.wrapInner($('<span/>'));
                   a.each(function () {
                       var t = $(this), span = t.find('span');
                   });
               a.hover(function () {
                   var t = $(this).parent().find('a'); 
                   var s = $(this).parent().siblings('li'); 
                        t.toggleClass('shadow');
                        s.toggleClass('blur');
               });
           });


                /*QUICK VIEW*/
                function resetOverlays() {
                    var dialogs = $("div.ui-dialog"); if (dialogs.length == 0)
                    { $(".ui-widget-overlay").remove(); } 
                }
                function minicartDetailPopup() { $('#cart').addClass('active'); $('#cart').load('index.php?route=module/cart #cart > *').hide().fadeIn('slow'); setTimeout(function () { $('#cart .content').fadeOut('slow', function () { $('#cart').removeClass('active'); }); }, 6000); }
                //function ajaxLoading(){$.colorbox({html:'<div id="loadingdialog" style="display:block; min-width:300px;min-height:100px;"><span class="ajaxloading"><style>#cboxLoadingOverlay {display:block !important;} #cboxClose {display:none !important;} </style></span></div>',initialWidth:300,initialHeight:100,width:'auto',height:'auto',overlayClose:false,title:function(){$('#cboxLoadingOverlay').html('<style>#cboxLoadingOverlay {display:block !important;}</style>');$('#cboxLoadingGraphic').html('<style>#cboxLoadingGraphic {display:block !important;}</style>');},onClosed:function(){$('#cboxLoadingOverlay').html('');$('#cboxLoadingGraphic').html('');}});}
                //function ajaxDialog(url,c){$.colorbox({href:url,initialWidth:300,initialHeight:100,height:760,maxWidth:690,width:'100%',opacity:0.25,overlayClose:true,onOpen:function(){$('#cboxLoadingOverlay').html('');$('#cboxLoadingGraphic').html('');},onLoad:function(){$('#colorbox').addClass('quickview-box');$('#cboxClose').hide();},onClosed:function(){$('#cboxLoadingOverlay').html('');$('#cboxLoadingGraphic').html('');},onComplete:function(){$('.colorbox').colorbox({rel:'colorbox',opacity:0.5,width:'auto',height:'auto',current:false,overlayClose:false,title:function(){$('#cboxLoadingOverlay').html('<style>#cboxLoadingOverlay {display:none !important;}</style>');$('#cboxLoadingGraphic').html('<style>#cboxLoadingGraphic {display:none !important;}</style>');},onClosed:function(){$('#cboxLoadingOverlay').html('');$('#cboxLoadingGraphic').html('');}});$('.cloud-zoom, .cloud-zoom-gallery').CloudZoom();$('#cboxClose').show();}});return false;}
                $(document).ready(function () { $('.product-grid .inner').live({ mouseenter: function () { $(this).find('.quickview').css({ "visibility": "visible" }); }, mouseleave: function () { $(this).find('.quickview').css({ "visibility": "hidden" }); } }); $('.product-list > div').live({ mouseenter: function () { $(this).find('.quickview').css({ "visibility": "visible" }); }, mouseleave: function () { $(this).find('.quickview').css({ "visibility": "hidden" }); } }); $('.product-box .inner').live({ mouseenter: function () { $(this).find('.quickview').css({ "visibility": "visible" }); }, mouseleave: function () { $(this).find('.quickview').css({ "visibility": "hidden" }); } }); $('.product-slider ul li .inner').live({ mouseenter: function () { $(this).find('.quickview').css({ "visibility": "visible" }); }, mouseleave: function () { $(this).find('.quickview').css({ "visibility": "hidden" }); } }); });

                /*003 js json*/
                (function ($) {
                    var ls = window.localStorage; var supported; if (typeof ls == 'undefined' || typeof window.JSON == 'undefined') { supported = false; } else { supported = true; }
                    $.totalStorage = function (key, value, options) { return $.totalStorage.impl.init(key, value); }
                    $.totalStorage.setItem = function (key, value) { return $.totalStorage.impl.setItem(key, value); }
                    $.totalStorage.getItem = function (key) { return $.totalStorage.impl.getItem(key); }
                    $.totalStorage.getAll = function () { return $.totalStorage.impl.getAll(); }
                    $.totalStorage.deleteItem = function (key) { return $.totalStorage.impl.deleteItem(key); }
                    $.totalStorage.impl = { init: function (key, value) { if (typeof value != 'undefined') { return this.setItem(key, value); } else { return this.getItem(key); } }, setItem: function (key, value) {
                        if (!supported) { try { $.cookie(key, value); return value; } catch (e) { console.log('Local Storage not supported by this browser. Install the cookie plugin on your site to take advantage of the same functionality. You can get it at https://github.com/carhartl/jquery-cookie'); } }
                        var saver = JSON.stringify(value); ls.setItem(key, saver); return this.parseResult(saver);
                    }, getItem: function (key) {
                        if (!supported) { try { return this.parseResult($.cookie(key)); } catch (e) { return null; } }
                        return this.parseResult(ls.getItem(key));
                    }, deleteItem: function (key) {
                        if (!supported) { try { $.cookie(key, null); return true; } catch (e) { return false; } }
                        ls.removeItem(key); return true;
                    }, getAll: function () {
                        var items = new Array(); if (!supported) { try { var pairs = document.cookie.split(";"); for (var i = 0; i < pairs.length; i++) { var pair = pairs[i].split('='); var key = pair[0]; items.push({ key: key, value: this.parseResult($.cookie(key)) }); } } catch (e) { return null; } } else { for (var i in ls) { if (i.length) { items.push({ key: i, value: this.parseResult(ls.getItem(i)) }); } } }
                        return items;
                    }, parseResult: function (res) {
                        var ret; try {
                            ret = JSON.parse(res); if (ret == 'true') { ret = true; }
                            if (ret == 'false') { ret = false; }
                            if (parseFloat(ret) == ret && typeof ret != "object") { ret = parseFloat(ret); } 
                        } catch (e) { }
                        return ret;
                    } 
                    }
                })(jQuery);




                /*Light box 2.6 min*/
            (function () { var b, d, c; b = jQuery; c = (function () { function b() { this.fadeDuration = 500; this.fitImagesInViewport = true; this.resizeDuration = 700; this.showImageNumberLabel = true; this.wrapAround = false } b.prototype.albumLabel = function (b, c) { return ("\u0418\u0437\u043e\u0431\u0440\u0430\u0436\u0435\u043d\u0438\u0435 " + b + " \u0438\u0437 " + c) }; return b })(); d = (function () { function c(b) { this.options = b; this.album = []; this.currentImageIndex = void 0; this.init() } c.prototype.init = function () { this.enable(); return this.build() }; c.prototype.enable = function () { var c = this; return b('body').on('click', 'a[rel^=lightbox], area[rel^=lightbox], a[data-lightbox], area[data-lightbox]', function (d) { c.start(b(d.currentTarget)); return false }) }; c.prototype.build = function () { var c = this; b("<div id='lightboxOverlay' class='lightboxOverlay'></div><div id='lightbox' class='lightbox'><div class='lb-outerContainer'><div class='lb-container'><img class='lb-image' src='' /><div class='lb-nav'><a class='lb-prev' href='' ><span class='fa fa-3x fa-chevron-left'></span></a><a class='lb-next' href='' ><span class='fa fa-3x fa-chevron-right'></span></a></div><div class='lb-loader'><a class='lb-cancel'></a></div></div></div><div class='lb-dataContainer'><div class='lb-data'><div class='lb-details'><span class='lb-caption'></span><span class='lb-number'></span></div><div class='lb-closeContainer'><a class='lb-close'><span class='fa fa-3x fa-times'></span></a></div></div></div></div>").appendTo(b('body')); this.$lightbox = b('#lightbox'); this.$overlay = b('#lightboxOverlay'); this.$outerContainer = this.$lightbox.find('.lb-outerContainer'); this.$container = this.$lightbox.find('.lb-container'); this.containerTopPadding = parseInt(this.$container.css('padding-top'), 10); this.containerRightPadding = parseInt(this.$container.css('padding-right'), 10); this.containerBottomPadding = parseInt(this.$container.css('padding-bottom'), 10); this.containerLeftPadding = parseInt(this.$container.css('padding-left'), 10); this.$overlay.hide().on('click', function () { c.end(); return false }); this.$lightbox.hide().on('click', function (d) { if (b(d.target).attr('id') === 'lightbox') { c.end() } return false }); this.$outerContainer.on('click', function (d) { if (b(d.target).attr('id') === 'lightbox') { c.end() } return false }); this.$lightbox.find('.lb-prev').on('click', function () { if (c.currentImageIndex === 0) { c.changeImage(c.album.length - 1) } else { c.changeImage(c.currentImageIndex - 1) } return false }); this.$lightbox.find('.lb-next').on('click', function () { if (c.currentImageIndex === c.album.length - 1) { c.changeImage(0) } else { c.changeImage(c.currentImageIndex + 1) } return false }); return this.$lightbox.find('.lb-loader, .lb-close').on('click', function () { c.end(); return false }) }; c.prototype.start = function (c) { var f, e, j, d, g, n, o, k, l, m, p, h, i; b(window).on("resize", this.sizeOverlay); b('select, object, embed').css({ visibility: "hidden" }); this.$overlay.width(b(document).width()).height(b(document).height()).fadeIn(this.options.fadeDuration); this.album = []; g = 0; j = c.attr('data-lightbox'); if (j) { h = b(c.prop("tagName") + '[data-lightbox="' + j + '"]'); for (d = k = 0, m = h.length; k < m; d = ++k) { e = h[d]; this.album.push({ link: b(e).attr('href'), title: b(e).attr('title') }); if (b(e).attr('href') === c.attr('href')) { g = d } } } else { if (c.attr('rel') === 'lightbox') { this.album.push({ link: c.attr('href'), title: c.attr('title') }) } else { i = b(c.prop("tagName") + '[rel="' + c.attr('rel') + '"]'); for (d = l = 0, p = i.length; l < p; d = ++l) { e = i[d]; this.album.push({ link: b(e).attr('href'), title: b(e).attr('title') }); if (b(e).attr('href') === c.attr('href')) { g = d } } } } f = b(window); o = f.scrollTop() + f.height() / 10; n = f.scrollLeft(); this.$lightbox.css({ top: o + 'px', left: n + 'px' }).fadeIn(this.options.fadeDuration); this.changeImage(g) }; c.prototype.changeImage = function (f) { var d, c, e = this; this.disableKeyboardNav(); d = this.$lightbox.find('.lb-image'); this.sizeOverlay(); this.$overlay.fadeIn(this.options.fadeDuration); b('.lb-loader').fadeIn('slow'); this.$lightbox.find('.lb-image, .lb-nav, .lb-prev, .lb-next, .lb-dataContainer, .lb-numbers, .lb-caption').hide(); this.$outerContainer.addClass('animating'); c = new Image(); c.onload = function () { var m, g, h, i, j, k, l; d.attr('src', e.album[f].link); m = b(c); d.width(c.width); d.height(c.height); if (e.options.fitImagesInViewport) { l = b(window).width(); k = b(window).height(); j = l - e.containerLeftPadding - e.containerRightPadding - 20; i = k - e.containerTopPadding - e.containerBottomPadding - 110; if ((c.width > j) || (c.height > i)) { if ((c.width / j) > (c.height / i)) { h = j; g = parseInt(c.height / (c.width / h), 10); d.width(h); d.height(g) } else { g = i; h = parseInt(c.width / (c.height / g), 10); d.width(h); d.height(g) } } } return e.sizeContainer(d.width(), d.height()) }; c.src = this.album[f].link; this.currentImageIndex = f }; c.prototype.sizeOverlay = function () { return b('#lightboxOverlay').width(b(document).width()).height(b(document).height()) }; c.prototype.sizeContainer = function (f, g) { var b, d, e, h, c = this; h = this.$outerContainer.outerWidth(); e = this.$outerContainer.outerHeight(); d = f + this.containerLeftPadding + this.containerRightPadding; b = g + this.containerTopPadding + this.containerBottomPadding; this.$outerContainer.animate({ width: d, height: b }, this.options.resizeDuration, 'swing'); setTimeout(function () { c.$lightbox.find('.lb-dataContainer').width(d); c.$lightbox.find('.lb-prevLink').height(b); c.$lightbox.find('.lb-nextLink').height(b); c.showImage() }, this.options.resizeDuration) }; c.prototype.showImage = function () { this.$lightbox.find('.lb-loader').hide(); this.$lightbox.find('.lb-image').fadeIn('slow'); this.updateNav(); this.updateDetails(); this.preloadNeighboringImages(); this.enableKeyboardNav() }; c.prototype.updateNav = function () { this.$lightbox.find('.lb-nav').show(); if (this.album.length > 1) { if (this.options.wrapAround) { this.$lightbox.find('.lb-prev, .lb-next').show() } else { if (this.currentImageIndex > 0) { this.$lightbox.find('.lb-prev').show() } if (this.currentImageIndex < this.album.length - 1) { this.$lightbox.find('.lb-next').show() } } } }; c.prototype.updateDetails = function () { var b = this; if (typeof this.album[this.currentImageIndex].title !== 'undefined' && this.album[this.currentImageIndex].title !== "") { this.$lightbox.find('.lb-caption').html(this.album[this.currentImageIndex].title).fadeIn('fast') } if (this.album.length > 1 && this.options.showImageNumberLabel) { this.$lightbox.find('.lb-number').text(this.options.albumLabel(this.currentImageIndex + 1, this.album.length)).fadeIn('fast') } else { this.$lightbox.find('.lb-number').hide() } this.$outerContainer.removeClass('animating'); this.$lightbox.find('.lb-dataContainer').fadeIn(this.resizeDuration, function () { return b.sizeOverlay() }) }; c.prototype.preloadNeighboringImages = function () { var c, b; if (this.album.length > this.currentImageIndex + 1) { c = new Image(); c.src = this.album[this.currentImageIndex + 1].link } if (this.currentImageIndex > 0) { b = new Image(); b.src = this.album[this.currentImageIndex - 1].link } }; c.prototype.enableKeyboardNav = function () { b(document).on('keyup.keyboard', b.proxy(this.keyboardAction, this)) }; c.prototype.disableKeyboardNav = function () { b(document).off('.keyboard') }; c.prototype.keyboardAction = function (g) { var d, e, f, c, b; d = 27; e = 37; f = 39; b = g.keyCode; c = String.fromCharCode(b).toLowerCase(); if (b === d || c.match(/x|o|c/)) { this.end() } else if (c === 'p' || b === e) { if (this.currentImageIndex !== 0) { this.changeImage(this.currentImageIndex - 1) } } else if (c === 'n' || b === f) { if (this.currentImageIndex !== this.album.length - 1) { this.changeImage(this.currentImageIndex + 1) } } }; c.prototype.end = function () { this.disableKeyboardNav(); b(window).off("resize", this.sizeOverlay); this.$lightbox.fadeOut(this.options.fadeDuration); this.$overlay.fadeOut(this.options.fadeDuration); return b('select, object, embed').css({ visibility: "visible" }) }; return c })(); b(function () { var e, b; b = new c(); return e = new d(b) }) }).call(this);
                /*end light box*/







/*blur effects*/


    
            $(function () {
                var menu = $('.news-wrap');
                var a = menu.find('div.news-preview');

                a.hover(function () {
                    var t = $(this); 
                    var s = $(this).siblings('div');
                   
                   t.toggleClass('shadow');
                   s.toggleClass('blur');
                });
            });








            /*end blur effects*/
            $(function() {
                if ($.browser.webkit) {
                    $("#enter-button").css("padding-top", "6px");
                    $("#enter-button").css("padding-bottom", "6px");
                }
                if ($.browser.mozilla) {
                    $("#enter-button").css("padding-top", "8px");
                    $("#enter-button").css("padding-bottom", "8px");
                    $("#enter-button").css("margin-top", "0px");
                }

                if (navigator.appName == 'Netscape') {
                    $("#enter-button").css("padding-top", "4px !important");
                    $("#enter-button").css("padding-bottom", "4px !important");
                } else {
                    $("#enter-button").css("padding-top", "6px !important");
                    $("#enter-button").css("padding-bottom", "6px !important");
                }
            });



            //Loading element 

            function gifLoaderBefore() {
                var p = $('#jquery-loader');
                p.addClass('blue-with-image');
                var z = $('#jquery-loader-background');
                z.addClass('loader-bg');
                var lefter = $(window).width() / 2 - 60;
                var upper = $(window).height() / 2 - 60;
                p.css('left', lefter);
                p.css('top', upper);


                /*

                width: 120px; 
    height: 120px; 
    position: fixed; 
    top: 50%; 
    left: 45%;*/


            };

            function gifLoaderAfter() {
                /*delay(1);*/
                var p = $('#jquery-loader');
                p.removeClass('blue-with-image');
                var z = $('#jquery-loader-background');
                z.removeClass('loader-bg');
            };


            $("#jquery-loader-background").click(function (e) {
                gifLoaderAfter();
                /*if ($("#jquery-loader").hasClass('blue-with-image') == true) {
                var p = $('#jquery-loader');
                p.removeClass('blue-with-image');
                var z = $('#jquery-loader-background');
                z.removeClass('loader-bg');*/
            } //else alert('!!');
        )
