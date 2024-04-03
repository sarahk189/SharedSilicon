const toggleMenu = () => {
    document.getElementById('menu').classList.toggle('hide');
    document.getElementById('account-buttons').classList.toggle('hide');

}

const checkScreenSize = () => {
    if (window.innerwidth >= 1200) {
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');
    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.add('hide');
        }
        if (!document.getElementById('account-buttons').classList.contains('hide'){
            document.getElementById('account-buttons').classList.add('hide');
        }
    }

};

document.querySelectorAll('.bookmark').forEach(bookmark => {
    bookmark.addEventListener('click', function (event) {
        event.preventDefault();

        var courseId = this.dataset.courseId;

        if (this.classList.contains('saved')) {
            // The course is currently saved, so we'll unsave it.
            fetch(`/api/SavedCourses/${courseId}`, {
                method: 'DELETE',
            })
                .then(response => {
                    if (response.ok) {
                        // The course was successfully unsaved.
                        // You can update the UI here if you want.
                        this.classList.remove('saved');
                    } else {
                        // There was an error unsaving the course.
                        // You can handle the error here if you want.
                    }
                });
        } else {
            // The course is not currently saved, so we'll save it.
            fetch(`/api/SavedCourses`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    UserId: /* User ID goes here */,
                    CourseId: courseId
                })
            })
                .then(response => {
                    if (response.ok) {
                        // The course was successfully saved.
                        // You can update the UI here if you want.
                        this.classList.add('saved');
                    } else {
                        // There was an error saving the course.
                        // You can handle the error here if you want.
                    }
                });
        }
    });
});


window.addEventListener('resize', checkScreenSize);)
checkScreenSize();