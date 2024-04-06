//const toggleMenu = () => {
//    document.getElementById('menu').classList.toggle('hide');
//    document.getElementById('account-buttons').classList.toggle('hide');

//}

//const checkScreenSize = () => {
//    if (window.innerwidth >= 1200) {
//        document.getElementById('menu').classList.remove('hide');
//        document.getElementById('account-buttons').classList.remove('hide');
//    } else {
//        if (!document.getElementById('menu').classList.contains('hide')) {
//            document.getElementById('menu').classList.add('hide');
//        }
//        if (!document.getElementById('account-buttons').classList.contains('hide')) {
//            document.getElementById('account-buttons').classList.add('hide');
//        }
//    }
//};

//window.addEventListener('resize', checkScreenSize);
//checkScreenSize();




document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('contactForm');
    if (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            var data = {
                fullName: document.getElementById('FullName').value,
                emailAddress: document.getElementById('EmailAddress').value,
                message: document.getElementById('Message').value
            };

            var json = JSON.stringify(data);

            fetch('https://localhost:7048/api/contact/send', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: json
            })
                .then(response => {
                    if (response.ok) {
                        console.log("Contact request was sent successfully.");
                        document.getElementById('successMessage').textContent = "Contact request was sent successfully!";
                        document.getElementById('successMessage').style.display = "block";
                    } else {                       
                        document.getElementById('successMessage').textContent = "An error occurred while sending the contact request.";
                        document.getElementById('successMessage').style.display = "block";
                    }
                })
                .catch(error => {
                    console.error("An error occurred while fetching:", error);
                    alert("An error occurred while fetching: " + error);
                });
        });
    }
});




