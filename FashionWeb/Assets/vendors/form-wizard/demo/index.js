'use strict';
$(document).ready(function () {

    $('#wizard1').steps({
        headerTag: 'h3',
        bodyTag: 'section',
        autoFocus: true,
        titleTemplate: '#index# #title#'
    });

    $('#wizard2').steps({
        headerTag: 'h3',
        bodyTag: 'section',
        autoFocus: true,
        titleTemplate: '#index# #title#',
        onStepChanging: function (event, currentIndex, newIndex) {
            if(currentIndex < newIndex) {

                var form = document.getElementById('form1');

                // Step 1 form validation
                if(currentIndex === 0) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                        form.classList.add('was-validated');
                    }else{
                        return true;
                    }
                }

                // Step 2 form validation
                if(currentIndex === 1) {
                    var email = $('#email').parsley();
                    if(email.isValid()) {
                        return true;
                    } else { email.validate(); }
                }
            } else { return true; }
        }
    });

});
