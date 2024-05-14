document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    let email = document.getElementById('email').value;
    let password = document.getElementById('password').value;

    if (email && password) {
        // Make a POST request to the /api/Login endpoint
        fetch('http://localhost:5272/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json-patch+json',
                'accept': '*/*'
            },
            body: JSON.stringify({
                email: email,
                password: password
            })
        })
        .then(response => response.json())
        .then(data => {
            if (data.token) {
                // If the login was successful, store the token and user details in local storage
                localStorage.setItem('user', JSON.stringify(data));
                // Then redirect to the chat page
                window.location.href = 'chat.html';
            } else {
                alert('Invalid email or password.');
            }
        })
        .catch(error => console.error('Error:', error));
    } else {
        alert('Please enter an email and password.');
    }
});