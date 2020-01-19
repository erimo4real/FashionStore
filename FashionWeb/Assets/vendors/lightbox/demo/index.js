'use strict';
$(document).ready(function () {

    $('.image-popup').magnificPopup({type: 'image'});

    var magnificPopupGalleryConfig = {
        type: 'image',
        gallery: {
            enabled: true
        }
    };

    $('.image-popup-gallery-item').magnificPopup(magnificPopupGalleryConfig);

    $('.chat-app-wrapper .chat-app .chat-body .chat-body-messages .message-items .message-item:not(.outgoing-message).message-item-media ul a').magnificPopup(magnificPopupGalleryConfig);

    $('.chat-app-wrapper .chat-app .chat-body .chat-body-messages .message-items .message-item.outgoing-message.message-item-media ul a').magnificPopup(magnificPopupGalleryConfig);

});