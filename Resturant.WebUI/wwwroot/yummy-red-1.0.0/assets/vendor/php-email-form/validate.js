(function () {
    "use strict";

    let forms = document.querySelectorAll('.php-email-form');

    forms.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            let action = form.getAttribute('action');

            if (!action) {
                displayError(form, 'Action tapılmadı!');
                return;
            }

            form.querySelector('.loading').classList.add('d-block');
            form.querySelector('.error-message').classList.remove('d-block');
            form.querySelector('.sent-message').classList.remove('d-block');

            let formData = new FormData(form);

            fetch(action, {
                method: 'POST',
                body: formData
            })
                .then(res => res.text())
                .then(data => {
                    form.querySelector('.loading').classList.remove('d-block');

                    if (data.trim() === 'OK') {
                        form.querySelector('.sent-message').classList.add('d-block');

                        form.reset(); // ✅ FORM BOŞALIR

                    } else {
                        throw new Error(data);
                    }
                })
                .catch(err => {
                    displayError(form, err);
                });

        });
    });

    function displayError(form, error) {
        form.querySelector('.loading').classList.remove('d-block');
        form.querySelector('.error-message').innerHTML = error;
        form.querySelector('.error-message').classList.add('d-block');
    }

})();