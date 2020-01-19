'use strict';
$(document).ready(function () {

    $('.btn-notify-primary').click(function () {
        $.notify("This is a primary alert", {type: 'primary'});
    });

    $('.btn-notify-primary-with-title').click(function () {
        $.notify("This is a primary alert", {title: "Primary", type: 'primary'});
    });

    $('.btn-notify-info').click(function () {
        $.notify("This alert needs your attention, but it's not that important.", {type: 'info'});
    });

    $('.btn-notify-info-with-title').click(function () {
        $.notify("This alert needs your attention, but it's not that important.", {title: "Info", type: 'info'});
    });

    $('.btn-notify-success').click(function () {
        $.notify("You successfully read this important alert message.", {type: 'success'});
    });

    $('.btn-notify-success-with-title').click(function () {
        $.notify("You successfully read this important alert message.", {title: "Success", type: 'success'});
    });

    $('.btn-notify-danger').click(function () {
        $.notify("Change a few things up and try submitting again.", {type: 'danger'});
    });

    $('.notify-danger-with-title').click(function () {
        $.notify("Change a few things up and try submitting again.", {title: "Danger", type: 'danger'});
    });

    $('.btn-notify-danger-with-title').click(function () {
        $.notify("Change a few things up and try submitting again.", {title: "Error", type: 'danger'});
    });

    $('.btn-notify-warning').click(function () {
        $.notify("Better check yourself, you're not looking too good.", {type: 'warning'});
    });

    $('.btn-notify-warning-with-title').click(function () {
        $.notify("Better check yourself, you're not looking too good.", {title: "Warning", type: 'warning'});
    });

    $('.btn-notify-dark').click(function () {
        $.notify("This is a warning.", {type: 'dark'});
    });

    $('.btn-notify-dark-with-title').click(function () {
        $.notify("This is a warning.", {title: "Dark", type: 'dark'});
    });

});