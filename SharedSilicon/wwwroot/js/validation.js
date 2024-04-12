const formErrorHandler = (e, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`);

    if (validationResult) {
        e.target.classList.remove('input-validation-error');
        if (spanElement) {
            spanElement.classList.remove('field-validation-error');
            spanElement.classList.add('field-validation-valid');
            spanElement.innerHTML = '';
        }
    } else {
        e.target.classList.add('input-validation-error');
        if (spanElement) {
            spanElement.classList.add('field-validation-error');
            spanElement.classList.remove('field-validation-valid');
            spanElement.innerHTML = e.target.dataset.valRequired;
        }
    }
};

const textValidator = (input, minLength = 2) => {
    let validationResult = input.value.length >= minLength;
    formErrorHandler({ target: input }, validationResult);
}

const emailValidator = (input) => {
    const regEx = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/;
    let validationResult = regEx.test(input.value);
    formErrorHandler({ target: input }, validationResult);
}

const passwordValidator = (input) => {
    let validationResult;
    if (input.dataset.valEqualToOther !== undefined) {
        validationResult = compareValidator(input.value, document.getElementsByName(input.dataset.valEqualToOther.replace('*', 'Form'))[0].value);
    } else {
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$/;
        validationResult = regEx.test(input.value);
    }
    formErrorHandler({ target: input }, validationResult);
}

let forms = document.querySelector('form')
let inputs = forms.querySelectorAll('input[data-val="true"]')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {
                    case 'text':
                        textValidator(e.target)
                        break;
                    case 'email':
                        emailValidator(e.target)
                        break;
                    case 'password':
                        passwordValidator(e.target)
                        break;
                }
            })
        }
    }
})
