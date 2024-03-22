
const formErrorHandler = (e, validationResult) => {
    let element = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

    if (validationResult) {
        e.target.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        e.target.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = e.target.dataset.valRequired

    }
}

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

const compareValidator = (element, compareWithValue) => {
    if (element === compareWithValue)
        return true

    return false

}

const textValidator = (element, minLength = 2) => {
    if (element.value.length >= minLength) {
        console.log(element)
        formErrorHandler(element, true);
    } else {
        formErrorHandler(element, false);
    }
}



const emailValidator = (element) => {
    const regEx = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/
    formErrorHandler(element, regEx.test(element.value))
}


const passwordValidator = (element) => {

    if (element.dataset.valEqualToOther !== undefined) {
        formErrorHandler(element, compareValidator(element.value, document.getElementsByName(element.dataset.valEqualToOther.replace('*', 'Form'))[0].value))
    } else {
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$/
        formErrorHandler(element, regEx.test(element.value))

    }
}


const checkboxValidator = (element) => {

    if (element.checked)
        formErrorHandler(element, true)

    formErrorHandler(element, false)
}

//Johans kod, ska se om det lirar

let inputs = forms[0].querySelectorAll('input')
let forms = document.querySelectorAll('form')

forms.forEach(form => {
    let inputs = form.querySelectorAll('input');

    inputs.forEach(input => {
        if (input.dataset.val === 'true') {

            if (input.type === 'checkbox') {
                input.addEventListener('change', (e) => {
                    checkBoxValidator(e.target)
                })
            } else {

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
})



//våran kod

//let forms = document.querySelector('form')
//let inputs = forms[0].querySelectorAll('input')


//inputs.forEach(input => {
//    if (input.dataset.val === 'true') {

//        if (input.type === 'checkbox') {
//            input.addEventListener('change', (e) => {
//                checkboxValidator(e.target)
//            })
//        }
//        else {
//            input.addEventListener('keyup', (e) => {
//                switch (e.target.type) {
//                    case 'text':
//                        textValidator(e.target)
//                        break;

//                    case 'email':
//                        passwordValidator(e.target)
//                        break;

//                    case 'password':
//                        passwordValidator(e.target)
//                        break;
//                }
//            })
//        }
//    }
//})



