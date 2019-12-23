$(document).ready(function () {
    // Copy
    $('.copy-btn').on('click', function () {
        copyKeyToClipboard($(this).parent().data('client-key'));
    });

    // New client
    $('#new-client-btn').on('click', function () {
        showNewClientForm();
    });

    // Reset API key
    $('.reset-key-btn').on('click', function () {

        var clientId = $(this).parent().parent().data('client-id');

        showPrompt('Are you sure?', 'If you reset this API key, you must change it in your application.', function () {
            resetClientAPIKey.bind(this)(clientId);
        }.bind(this));
    });

    // Delete client
    $('.delete-client-btn').on('click', function () {

        var clientId = $(this).parent().parent().data('client-id');

        showPrompt('Are you sure?', 'If you delete this client, our service would become unavailable to you through it\'s api key. Also, you don\'t get your money back.', function () {
            deleteClient(clientId);
        });
    });

    // Edit client name
    $('.edit-client-btn').on('click', function () {
        var clientId = $(this).parent().parent().data('client-id');
        showEditClientNameForm(clientId);
    });

    $('.change-client-name-form .btn').on('click', function () {
        submitEditClientNameForm($('.change-client-name-form .client-id-field').val(), $('.change-client-name-form .client-name-field').val());
    });
});

function showEditClientNameForm(clientId) {
    var data = {
        clientId: clientId
    };

    $.ajax({
        url: '/Profile/GetClientName',
        type: 'GET',
        data: data,
        success: function (result) {
            $('.change-client-name-form .client-name-field').val(result.clientName);
            $('.change-client-name-form .client-id-field').val(clientId);

            $('.fader').show();
            $('.change-client-name-form').show();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
        }
    });
}

function submitEditClientNameForm(clientId, newClientName) {
    var data = {
        clientId: clientId,
        newName: newClientName
    };

    $.ajax({
        url: '/Profile/UpdateClientName',
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

function deleteClient(clientId) {
    var data = {
        clientId: clientId
    };

    $.ajax({
        url: '/Profile/DeleteClient',
        type: 'POST',
        data: data,
        success: function (result) {
            closePrompt();
            window.location.reload()
        }.bind(this),
        error: function (xhr, ajaxOptions, thrownError) {
            closePrompt();
            alert('failure');///TODO Show toast message.
        }
    });
}

function resetClientAPIKey(clientId) {

    var data = {
        clientId: clientId
    };

    $.ajax({
        url: '/Profile/ResetApiKey',
        type: 'POST',
        data: data,
        success: function (result) {
            closePrompt();
            $(this).siblings('h4').find('span').html(result.newKey);
        }.bind(this),
        error: function (xhr, ajaxOptions, thrownError) {
            alert('failure');///TODO Show toast message.
            closePrompt();
        }
    });
}

function copyKeyToClipboard(key) {
    if (window.clipboardData && window.clipboardData.setData) {
        // IE specific code path to prevent textarea being shown while dialog is visible.
        return clipboardData.setData("Text", key);

    } else if (document.queryCommandSupported && document.queryCommandSupported("copy")) {
        var textarea = document.createElement("textarea");
        textarea.textContent = key;
        textarea.style.position = "fixed";// Prevent scrolling to bottom of page in MS Edge.
        document.body.appendChild(textarea);
        textarea.select();
        try {
            return document.execCommand("copy");
        } catch (ex) {
            console.warn("Copy to clipboard failed.", ex);///TODO Swap with showToast.
            alert('Your browser doesn\'t support clipboard manipulation.');
            return false;
        } finally {
            document.body.removeChild(textarea);
            alert('Copied to clipboard!');///TODO Swap with showToast.
        }
    }
}

function showNewClientForm() {
    $('.fader').show();
    $('.new-client-form').show();
}
