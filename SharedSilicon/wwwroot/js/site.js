
document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?mode=${theme}`) 
        .then(res => {
            if(res.ok)
                window.location.reload()
            else
                console.log('something')
        })

    })
})

//document.querySelectorAll('.bookmark').forEach(bookmark => {
//    bookmark.addEventListener('click', function (event) {
//        event.preventDefault();

//        var courseId = this.dataset.courseId;

//        if (this.classList.contains('saved')) {
//            // The course is currently saved, so we'll unsave it.
//            fetch(`/api/SavedCourses/${courseId}`, {
//                method: 'DELETE',
//            })
//                .then(response => {
//                    if (response.ok) {
//                        // The course was successfully unsaved.
//                        // You can update the UI here if you want.
//                        this.classList.remove('saved');
//                    } else {
//                        // There was an error unsaving the course.
//                        // You can handle the error here if you want.
//                    }
//                });
//        } else {
//            // The course is not currently saved, so we'll save it.
//            fetch(`/api/SavedCourses`, {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//                body: JSON.stringify({
//                    UserId: /* User ID goes here */,
//                    CourseId: courseId
//                })
//            })
//                .then(response => {
//                    if (response.ok) {
//                        // The course was successfully saved.
//                        // You can update the UI here if you want.
//                        this.classList.add('saved');
//                    } else {
//                        // There was an error saving the course.
//                        // You can handle the error here if you want.
//                    }
//                });
//        }
//    });
//});


//window.addEventListener('resize', checkScreenSize);)
//checkScreenSize();