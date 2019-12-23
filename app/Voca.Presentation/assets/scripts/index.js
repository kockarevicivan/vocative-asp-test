$(document).ready(function () {
    $('.fader').on('click', function () {
        closeDialog();
    });

    $('.change-email-link').on('click', function () {
        showEditEmailForm();
    });

    $('.change-email-form .btn').on('click', function () {
        submitEditEmailForm($('.change-email-form .email-field').val());
    });

    $('.change-password-link').on('click', function () {
        showEditPasswordForm();
    });

    $('.change-password-form .btn').on('click', function () {
        submitEditPasswordForm($('.change-password-form .current-password-field').val(), $('.change-password-form .new-password-field').val());
    });

    $('.delete-account-form').on('click', function () {
        showPrompt("Are you sure?", "If you delete your account, all your clients will be removed.", function () {
            deleteAccount();
        });
    });
});

function deleteAccount() {
    $.ajax({
        url: '/Profile/DeleteAccount',
        type: 'GET',
        success: function (result) {
            window.location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
        }
    });
}


function showEditEmailForm() {
    $.ajax({
        url: '/Profile/GetCurrentUserEmail',
        type: 'GET',
        success: function (result) {
            $('.change-email-form .email-field').val(result.currentEmail);

            $('.fader').show();
            $('.change-email-form').show();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
        }
    });
}

function submitEditEmailForm(newEmail) {
    var data = {
        newEmail: newEmail
    };

    $.ajax({
        url: '/Profile/UpdateEmail',
        type: 'POST',
        data: data,
        success: function (result) {
            window.location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
        }
    });
}


function showEditPasswordForm() {
    $('.fader').show();
    $('.change-password-form').show();
}

function submitEditPasswordForm(oldPassword, newPassword) {
    var data = {
        oldPassword: oldPassword,
        newPassword: newPassword
    };

    $.ajax({
        url: '/Profile/UpdatePassword',
        type: 'POST',
        data: data,
        success: function (result) {
            alert('Password changed!');///TODO Show toast message.
            closeDialog();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
            closeDialog();
        }
    });
}


function showPrompt(title, message, callback) {

    $('.prompt > .text-section > h3').html(title);

    if (message) {
        var $paragraph = $('.prompt > .text-section > p');
        $paragraph.html(message);
        $paragraph.show();
    }

    $('.fader').show();
    $('.prompt').show();

    $('.prompt .btn-ok').on('click', callback);
}


function closeDialog() {
    $('.dialog').hide();
    $('.fader').hide();
    closePrompt();
}

function closePrompt() {
    $('.prompt').hide();
    $('.fader').hide();

    var $paragraph = $('.prompt > .text-section > p');
    $paragraph.html('');
    $paragraph.hide();
    $('.prompt > .text-section > h3').text('');

    $('.prompt .btn-ok').off();
}
