﻿
              function display(view) {
                  if (view == 'list') {

                      $('.old-price').css('padding', '0');
                      $('.new-price').css('padding', '0');

                      $('#list').parent().addClass('active');
                      $('#grid').parent().removeClass('active');
                      $('.product-grid').toggleClass('product-grid product-list');
                      $('.product-list > div').each(function (index, element) {
                          //alert($(this).height());
                          $(this).height('auto');
                          /* var special = $(element).find('.onsale').html();
                          if (special != null) {
                          html = '<span class="onsale">' + special + '</span>';
                          html += '<div class="row">';
                          } else {
                          html = '<div class="row">';
                          }*/
                          html = '<div class="row" style="margin: 0;">';
                          var image = $(element).find('.image').html();

                          var imagesale = $(element).find('.sale').html();
                          var isSale;
                          if (imagesale != null) {
                              isSale = 'sale';
                          } else {
                              isSale = '';
                          }

                          if (image != null) {
                              html += '<div class="image '+isSale+' col-sm-3" style="margin-top: 5px; padding: 0">' + image + '</div>';

                          } else {
                              html += '<div class="image  ' + isSale + ' col-sm-3" style="margin-top: 5px; padding: 0">&nbsp;</div>';
                          }

                          var width1 = $(".product-list .row div.image").width();
                          var width2 = $(".product-list .row div.image a img").width();
                          //alert(width1);
                          //alert(width2);
                          var pos = Math.round((width1 - width2) / 2);

                          var onsale = $(this).find('.sale .onsale');
                          //alert(onsale.html());
                          /*if (onsale2 != null) {
                          $(this).find('.onsale').css('left', pos + "px");
                          onsale2.css('visibility', "hidden");
                          }*/

                          //html.css('visibility', "hidden");

                          /*  //alert(pos);
                          //  var onsale = $(element).find('div.onsale').html();

                          //var onsale2 = $(this).find('.onsale').html.css('visibility', "hidden");
                          
                          // alert(onsale2.html());

                          if (onsale2 != null) {
                          //$(this).find('.onsale').css('left', pos + "px");
                          //  onsale2.css('visibility', "hidden");
                          }
                          */
                          html += '<div class="name-desc col-sm-4" style="padding: 0;">';

                          var header3 = $(element).find('.product-name').html();
                          if (header != null) {
                              html += '<h3 class="product-name" style="margin: 0; text-align: center;">' + header3 + '</h3>';
                          }

                          //html += '<h3>' + $(element).find('.product-name').html() + '</h3>';
                          /*var rating = $(element).find('.rating').html();
                          if (rating != null) {
                          html += '<div class="rating">' + rating + '</div>';
                          }*/
                          html += '<div class="description" style="padding-left: 5px;">' + $(element).find('.description').html() + '</div>';

                          var quickview = $(element).find('.quickview').html();
                          if (quickview != null) {
                              html += '<div class="quickview" style="padding: 0; display: none">' + quickview + '</div>';
                          }
                          html += '</div>';
                          var price = $(element).find('.price').html();
                          if (price != null) {
                              html += '<div class="col-sm-2 col-xs-6" style="min-height: 120px; position: relative;"><div class="price" style="padding: 0; margin: 0; text-align: center; position: absolute; top:50%; margin-top: -15px;"">' + price + '</div></div>';
                          } else {
                              html += '<div class="col-sm-2 col-xs-6" style="min-height: 120px; position: relative;"><div class="price" style="position: absolute; top:50%; margin-top: -15px;">&nbsp;</div></div>';
                          }
                          html += '<div class="col-sm-3 col-xs-6" style="min-height: 120px; position: relative; horiz-align: center"><div class="actions" style="padding: 0; position: absolute; top:50%; margin-top: -15px;">';
                          /*html += '  <div class="cart">' + $(element).find('.cart').html() + '</div>';
                          html += ' <div class="links">';
                          var wishlist = $(element).find('.wishlist').html();
                          if (wishlist != null) {
                          html += '  <span class="wishlist">' + wishlist + '</span>';
                          }
                          var compare = $(element).find('.compare').html();
                          if (compare != null) {
                          html += '  <span class="compare">' + compare + '</span>';
                          }*/

                          var productIdValues, loginValues, returnUrl = "";
                          var productId = $(element).find('.cart-form input[type=hidden]#ProductID').html(function () { productIdValues = this.value; }); //first(function () { str += this.id + '=' + this.value + '\n'; });
                          var login = $(element).find('.cart-form input[type=hidden]#login').html(function () { loginValues = this.value; });
                          var url = $(element).find('.cart-form input[type=hidden]#returnUrl').html(function () { returnUrl = this.value; });
                          html += '<form action="/Cart/AddToCart" method="post" style="" class="cart-form"><input data-val="true" data-val-number="Значением поля ProductID должно быть число." data-val-required="Требуется поле ProductID." id="ProductID" name="ProductID" type="hidden" value="' + productIdValues + '" />' +
                            '<input id="returnUrl" name="returnUrl" type="hidden" value="' + returnUrl + '" /><input id="login" name="login" type="hidden" value="' + loginValues + '" />                ' +
                            '<button type="submit" class="btn btn-custom btn-md" style=""  value="В корзину"><span class="glyphicon glyphicon-shopping-cart"><b class="glyph-bug">&nbsp;В&nbsp;корзину</b></span></button></form>';
                          html += '</div>';
                          html += '</div>';
                          html += '</div></div>';
                          $(element).html(html);
                      });


                      $('.product-list > .item .onsale').each(function (index, element) {
                          var width1 = $(this).parent().parent();
                          var width2 = $(this).prev(); 
                          var pos = Math.round((width1.width() - width2.width())/ 2)+2;
                          $(element).css('left', pos + "px");
                      });

                      $.totalStorage('display', 'list');

                      
                      
                  } else {

                      $('.old-price').css('padding-right', '10px');
                      $('.new-price').css('padding-left', '10px');

                      $('#grid').parent().addClass('active');
                      $('#list').parent().removeClass('active');
                      $('.product-list').toggleClass('product-list product-grid');
                      $('.product-grid > div').each(function (index, element) {
                          $(this).height('');
                          html = '';
                          var image = $(element).find('.image').html();


                          html += '<div class="grid-box">';
                          var header3 = $(element).find('.product-name').html();
                          if (header != null) {
                              html += '<div class="item-header container"><h3 class="product-name">' + header3 + '</h3></div><div class="inner">';
                          }




                          /*     var special = $(element).find('.onsale').html();
                          if (special != null) {
                          html += '<span class="onsale">' + special + '</span>';
                          }*/
                          var imagesale = $(element).find('.sale').html();
                          var isSale;
                          if (imagesale != null) {
                              isSale = 'sale';
                          } else {
                              isSale = '';
                          }


                          if (image != null) {
                              html += '<div class="image ' + isSale + '">' + image + '</div>';
                          }
                          var special = $(element).find('.quickview').html();
                          if (special != null) {
                              html += '<div class="quickview" style="margin-top: -37px;">' + special + '</div>';
                          }
                          /*  html += '<div class="name">' + $(element).find('.name').html() + '</div>';
                          html += '<div class="description">' + $(element).find('.description').html() + '</div>';*/
                          var price = $(element).find('.price').html();
                          if (price != null) {
                              html += '<div class="item-legend"><p class="price">' + price + '</p>';
                          }

                          html += '<div class="description" style="display: none;">' + $(element).find('.description').html() + '</div>';

                          /* html += '<div class="cart">' + $(element).find('.cart').html() + '</div>';
                          var rating = $(element).find('.rating').html();
                          if (rating != null) {
                          html += '<div class="rating">' + rating + '</div>';
                          }
                          html += ' <div class="links">';
                          var wishlist = $(element).find('.wishlist').html();
                          if (wishlist != null) {
                          html += '  <span class="wishlist">' + wishlist + '</span>';
                          }
                          var compare = $(element).find('.compare').html();
                          if (compare != null) {
                          html += '  <span class="compare">' + compare + '</span>';
                          }
                          html += '</div>';*/
                          var productIdValues, loginValues, returnUrl = "";
                          var productId = $(element).find('.cart-form input[type=hidden]#ProductID').html(function () { productIdValues = this.value; }); //first(function () { str += this.id + '=' + this.value + '\n'; });
                          var login = $(element).find('.cart-form input[type=hidden]#login').html(function () { loginValues = this.value; });
                          var url = $(element).find('.cart-form input[type=hidden]#returnUrl').html(function () { returnUrl = this.value; });
                          /* var str = "";
                          $('.cart-form input[type=hidden]').each(
                          function () {
                          str += this.id + '=' + this.value + '\n';
                          }
                          );
                          alert(str);*/

                          // var value2 = $('.cart-form input[type=hidden]#ProductID').each(function () { str += this.id + '=' + this.value + '\n'; });
                          //   alert(str);
                          //alert(value2.value);
                          //var value2 = $('.cart-form input[type=hidden]').value;
                          // var value2 = $('form#login').val;
                          //alert(value2);
                          html += '<form action="/Cart/AddToCart" method="post" class="cart-form"><input data-val="true" data-val-number="Значением поля ProductID должно быть число." data-val-required="Требуется поле ProductID." id="ProductID" name="ProductID" type="hidden" value="' + productIdValues + '" />' +
                            '<input id="returnUrl" name="returnUrl" type="hidden" value="' + returnUrl + '" /><input id="login" name="login" type="hidden" value="' + loginValues + '" />                ' +
                            '<button type="submit" class="btn btn-custom btn-md"  value="В корзину"><span class="glyphicon glyphicon-shopping-cart"><b class="glyph-bug">&nbsp;В&nbsp;корзину</b></span></button></form>';
                          /*html +='<form action="/Cart/AddToCart" method="post"><input data-val="true" data-val-number="Значением поля ProductID должно быть число." data-val-required="Требуется поле ProductID." id="ProductID" name="ProductID" type="hidden" value="13" />' +
                          '<input id="returnUrl" name="returnUrl" type="hidden" value="/" /><input id="login" name="login" type="hidden" value="viktor" />                ' +
                          '<button type="submit" class="btn btn-default btn-md"  value="В корзину"><span class="glyphicon glyphicon-shopping-cart"> В корзину</span></button></form>';*/
                          html += '</div>';
                          html += '</div>';
                          html += '</div>';
                          $(element).html(html);

                          quickviewResizer();
                          resizer();
                      });

                      /*

                      $('.quickview').each(function (index, element) {
                          var width1 = $(this).width(); //$(this).parent().parent();
                          var width2 = $(this).prev('.image').width();
                          var tmp = Math.round((width1 - width2) / 2);
                          $(this).width(width2);
                          $(this).css('margin-left', tmp + "px");
                      });*/


                      /*$('.product-grid > .item .onsale').each(function (index, element) {
                          var width1 = $(this).parent().parent();
                          var width2 = $(this).prev();
                          var pos = Math.round((width1.width() - width2.width()) / 2) + 2;
                          $(element).css('left', pos + "px");
                      });*/
                      //$(".onsale").css('left','');
                      //$(".onsale").css('left', '0px');

                      $.totalStorage('display', 'grid');
                      
                  }
              }
              view = $.totalStorage('display');








              $(window).load(function () {
                  resizer();
                  quickviewResizer();

                  setInterval(function() {
                      quickviewResizer();
                  }, 5000);
                  setInterval(quickviewResizer(), 1000);
                  //setTimeout(object.func, 300);
                  //setTimeout(object.func, 1300);
              });
              $(window).bind('resize', function () {
                  resizer();
              });
              function resizer() {
                  if ($("#productList").width() < 450) {
                      $('#productList .item-header > h3').css("font-size", "0.8em");
                  } else {
                      $('#productList .item-header > h3').css("font-size", "0.9em");
                  }
              }
              $(window).resize(function () {
                  resizer();
                  quickviewResizer();

              });
              function quickviewResizer() {
                  $('.quickview').each(function (index, element) {

                      var width1 = $(this).parent().parent().parent().width(); //$(this).parent().parent();
                      var width2 = $(this).prev('.image').find('img').width() + 2;
                      var tmp = Math.round((width1 - width2) / 2);
                      $(this).width(width2);
                      $(this).css('margin-left', tmp + 0 + "px");
                      $(this).css('margin-top', "-35px");
                      var width3 = $(this).prev('.image').find('.onsale');
                      if (width3 != null) {
                          width3.css("left", tmp + 3 + "px");
                      }
                  });
              }

              $("a.item-class").click(function () {
                  if ($("#submenu-button").css('display') != "none") {
                      document.getElementById('crumbs').scrollIntoView(true);
                  }
              });

              