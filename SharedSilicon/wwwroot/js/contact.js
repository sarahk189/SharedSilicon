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

            
            fetch(`https://localhost:7238/api/contact/send?key=Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                   /* 'ApiKey': 'Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh'*/
                },
                body: json
            })

                .then(response => {
                    if (response.ok) {
                        console.log("Contact request was sent successfully.");
                        document.getElementById('successMessage').textContent = "Message successfully sent!";
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
