function Common() {
    _this = this;

    this.init = function () {
        $(".user-account-edit").click(function () {
            alert('ewew');
            _this.showPopup("/Account/UserAccountEdit", initLoginPopup);
        });
    }

    this.showPopup = function (url, callback) {
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                showModalData(data, callback);
            }
        });
    }


    function initLoginPopup(modal) {
        $("#AccountEditButton").click(function () {

          /*  alert('Int');
            if ($('#updateAccountEditForm').valid()) {
                $(this).find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length == 0) {
                        $(this).removeClass('error');
                        alert('Not Error');
                    }
                });
            }
            else {
                alert('Not valid');
                $('#updateAccountEditForm').find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('error');
                        alert('Error');
                    }
                });
            }


            $('#updateAccountEditForm').each(function () {
                $(this).find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('error');
                        alert('Error');
                    }
                });
            });

            */


            $.ajax({
                type: "POST",
                url: "/Account/UserAccountEdit",
                data: $("#updateAccountEditForm").serialize(),
                success: function (data) {


                    //  showModalData(data);
                    initLoginPopup(modal);
                    updateSuccess(data);
                }
            });
        });
    }

    function showModalData(data, callback) {
        $(".modal-backdrop").remove();
        var popupWrapper = $("#updateDialog2");
        popupWrapper.empty();
        popupWrapper.html(data);
        var popup = $(".modal", popupWrapper);
        $(".modal", popupWrapper).modal();
        if (callback != undefined) {
            callback(popup);
        }
    }

    function updateSuccess(data) {
        if (data.Success == true) {
            //window.location.replace("/Product/List");
            window.location.reload();
        } else {
            
                $("#update-message2").html(data.ErrorMessage);
                $("#update-message2").show();
            

            /*
            var $form = $("#updateLoginForm");
            $form.unbind();
            $form.data("validator", null);
            $.validator.unobtrusive.parse(document);
            $form.validate($form.data("unobtrusiveValidation").options);
                        
            */


            }



        }
    
    /*
    function updateSuccess(data) {
    if (data.Success == true) {
    //$('#updateDialog').dialog('close');
    //twitter type notification
    // $('#commonMessage').html("Авторизация прошла успешно!");
    // $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);
    // window.location.replace("Product/List");
    alert('!!!');
    }
    else {
    if (data.ErrorMessage == "Capcha") {
    window.location.replace("Account/UserEnter");
    }
    $("#update-message").html(data.ErrorMessage);
    $("#update-message").show();
    }
    */
}

var common = null;
$().ready(function () {
    common = new Common();
    common.init();
});


