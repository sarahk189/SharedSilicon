document.addEventListener('DOMContentLoaded', function () {

    select()
    searchQuery()
})



function select() {
    try {
        let select = document.querySelector('.select');
        let dropbtn = select.querySelector('.dropbtn');
        let selectOptions = select.querySelector('.select-options')

        dropbtn.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                dropbtn.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                dropbtn.setAttribute('data-value', category)
                updateCoursesByFilter()
            })
        })
    }
    catch (error) {
        console.error(error);
    }
}

function searchQuery() {

    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {

            updateCoursesByFilter()


        })
    }
    catch (error) {
        console.error(error);
    }

}



function updateCoursesByFilter() {

    const category = document.querySelector('.select .dropbtn').getAttribute('data-value') || 'all'
    const searchQuery = document.querySelector('.dropdown-search #searchQuery').value

    const url = `/courses/index?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`


    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.courses-show').innerHTML = dom.querySelector('.courses-show').innerHTML;
        });

}

