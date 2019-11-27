/*$(function () {
    $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('help-block');
    });
    var $form = $('form');
    var $validate = $form.validate();
    var errorClass = "has-error";
    $validate.settings.errorClass = errorClass;
    var previousEPMethod = $validate.settings.errorPlacement;
    $validate.settings.errorPlacement = $.proxy(function (error, inputElement) {
        if (previousEPMethod) {
            previousEPMethod(error, inputElement);
        }
        inputElement.parent().addClass(errorClass);
    }, $form[0]);
    var previousSuccessMethod = $validate.settings.success;
    $validate.settings.success = $.proxy(function (error) {
        //we first need to remove the class, cause the unobtrusive success method removes the node altogether
        error.parent().parent().removeClass(errorClass);
        if (previousSuccessMethod) {
            previousSuccessMethod(error);
        }
    });
});*/
/*
$("#AccountEditButton").click(function () {

    $('#updateAccountEditForm').validate(
    {
        rules: {
            login: {
                minlength: 2,
                required: true
            },
            email: {
                required: true,
                email: true,
                minlength: 16,
            },
            subject: {
                minlength: 2,
                required: true
            },
            message: {
                minlength: 2,
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.control-group').removeClass('success').addClass('error');
        },
        success: function (element) {
            element
    .text('OK!').addClass('valid')
    .closest('.control-group').removeClass('error').addClass('success');
        }
    });
}); // end document.ready
*/

/*

$(".user-account-edit").click(function() {
    /*var form = $("#updateAccountEditForm");
   form.unbind();
    form.data("validator", null);
    validator.unobtrusive.parse(document);
    form.validate(form.data("unobtrusiveValidation").options);
    alert('!');
}*/
/*
 linkObj3 = $(this);

            var dialogDiv = $('#updateDialog2');
            var viewUrl = "UserAccountEdit";  //linkObj3.attr('href'); //"ChangePassword";
            $.get(viewUrl, function (data) {
                dialogDiv.html(data);
                //validation
                var $form = $("#updateAccountEditForm");
                // Unbind existing validation
                $form.unbind();
                $form.data("validator", null);
                // Check document for changes
                $.validator.unobtrusive.parse(document);
                // Re add validation with changes
                //    $form.validate($form.data("unobtrusiveValidation").options);
                //open dialog
                dialogDiv.dialog('open');
            });
            return false;

            */