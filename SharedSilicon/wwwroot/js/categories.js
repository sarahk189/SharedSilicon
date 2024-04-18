document.addEventListener('DOMContentLoaded', function () {
    
    select()
    searchQuery()
    updateCoursesByFilter()
    paginationClick(); 

   
})



function select() {
    try {
        let select = document.querySelector('.select')
       
        let dropbtn = select.querySelector('.dropbtn')
       
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
          
            updateCoursesByFilter()


        });
    }
    catch (error) {
        console.error('Error in searchQuery:', error);
    }

}

function paginationClick() {
    try {
        const paginationElement = document.querySelector('.pagination');
        let currentPage = parseInt(paginationElement.getAttribute('data-current-page'));
        const totalPages = parseInt(paginationElement.getAttribute('data-total-pages'));

        paginationElement.addEventListener('click', function (event) {
            event.preventDefault();
            if (event.target.closest('.number')) {
                currentPage = parseInt(event.target.textContent);
            } else if (event.target.closest('#previous-button')) {
                currentPage = Math.max(currentPage - 1, 1);
            } else if (event.target.closest('#next-button')) {
                currentPage = Math.min(currentPage + 1, totalPages);
            }
            const url = `/courses/index?pageNumber=${currentPage}`;
            updateCoursesByFilter(url);
        });
    } catch (error) {
        console.error('Error in paginationClick:', error);
    }
}


function updateCoursesByFilter(url) {
    try { 
    if (!url) {

        const category = document.querySelector('.select .dropbtn').getAttribute('data-value') || 'all'
        const searchQuery = document.querySelector('.dropdown-search #searchQuery').value


        url = `/courses/index?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`
    }

        fetch(url)
            .then(res => res.text())
            .then(data => {
                const parser = new DOMParser()
                const dom = parser.parseFromString(data, 'text/html')
                document.querySelector('.courses-show').innerHTML = dom.querySelector('.courses-show').innerHTML

                const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
                document.querySelector('.pagination').innerHTML = pagination
            })
            .catch(error => console.error('Error:', error));

    } catch (error) {
        console.error('Error in updateCoursesByFilter:', error);
    }
}


