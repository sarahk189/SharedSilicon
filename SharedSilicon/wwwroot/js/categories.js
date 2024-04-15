document.addEventListener('DOMContentLoaded', function () {
    console.log('DOMContentLoaded event fired');
    select()
    searchQuery()
    updateCoursesByFilter()
})



function select() {
    try {
        let select = document.querySelector('.select')
        console.log('Select:', select);
        let dropbtn = select.querySelector('.dropbtn')
        console.log('Dropbtn:', dropbtn);
        let selectOptions = select.querySelector('.select-options')
        console.log('Select options:', selectOptions);

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
            });
        });
    }
    catch (error) {
        console.error('Error in select:',error);
    }
}

function searchQuery() {

    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            console.log('Search query keyup event fired');
            updateCoursesByFilter()


        });
    }
    catch (error) {
        console.error('Error in searchQuery:', error);
    }

}



function updateCoursesByFilter() {
    try {
        console.log('Update courses by filter function called');
        const category = document.querySelector('.select .dropbtn').getAttribute('data-value') || 'all'
        const searchQuery = document.querySelector('.dropdown-search #searchQuery').value
        console.log('Category:', category);
        console.log('Search query:', searchQuery);

        const url = `/courses/index?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`
        console.log('URL:', url);

        fetch(url)
            .then(res => res.text())
            .then(data => {
                const parser = new DOMParser()
                const dom = parser.parseFromString(data, 'text/html')
                document.querySelector('.courses-show').innerHTML = dom.querySelector('.courses-show').innerHTML

                const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
                document.querySelector('pagination').innerHTML = pagination
            })
            .catch(error => console.error('Error:', error));

    } catch (error) {
        console.error('Error in updateCoursesByFilter:', error);
    }
}


