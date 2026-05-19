function validateFormRequiredFields(formId) {
    const form = document.getElementById(formId);
    if (!form) return;

    form.addEventListener('submit', function (e) {
        let isFormValid = true;

        const requiredInputs = form.querySelectorAll('[required]');

        requiredInputs.forEach(input => {
            const errSpan = input.nextElementSibling;
            if (input.value.trim() === "") {
                isFormValid = false;
                input.classList.add('is-invalid');

                if (errSpan && errSpan.tagName === 'SPAN') {
                    errSpan.textContent = "Este campo no puede estar vacío.";
                }
            } else {
                input.classList.remove('is-invalid');
                if (errSpan && errSpan.tagName === 'SPAN') {
                    errSpan.textContent = "";
                }
            }
        });

        if (!isFormValid) {
            e.preventDefault();
        }
    });
}