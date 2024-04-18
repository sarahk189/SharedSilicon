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
        document.querySelector('.pagination').addEventListener('click', function (event) {
            const chevron = event.target.closest('.chevron');
            if (chevron) {
                event.preventDefault();
                const currentPage = parseInt(this.getAttribute('data-current-page'), 10);
                if (chevron.classList.contains('fa-chevron-left')) {
                    const previousPage = currentPage - 1;
                    if (previousPage >= 1) {
                        const url = generatePaginationUrl(previousPage);
                        updateCoursesByFilter(url);
                    }
                } else if (chevron.classList.contains('fa-chevron-right')) {
                    const nextPage = currentPage + 1;
                    const totalPages = parseInt(this.getAttribute('data-total-pages'), 10);
                    if (nextPage <= totalPages) {
                        const url = generatePaginationUrl(nextPage);
                        updateCoursesByFilter(url);
                    }
                }
            }
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
            .then(res => res.json())
            .then(data => {
                const parser = new DOMParser()
                const dom = parser.parseFromString(data, 'text/html')
                document.querySelector('.courses-show').innerHTML = dom.querySelector('.courses-show').innerHTML

                document.querySelector('.pagination').setAttribute('data-current-page', data.Pagination.CurrentPage);
                document.querySelector('.pagination').setAttribute('data-total-pages', data.Pagination.TotalPages);
            })
            .catch(error => console.error('Error:', error));

    } catch (error) {
        console.error('Error in updateCoursesByFilter:', error);
    }
}


