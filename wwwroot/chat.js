let user = JSON.parse(localStorage.getItem('user'));
let fromUser = user.fullName;

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5272/chatHub") // Update this with the correct URL of your SignalR hub
    .build();
    
connection.on("ReceiveMessage", function(fromUser, message) {
    console.log("Received message: ", fromUser, message);
    let messages = document.getElementById("messages");
    let messageItem = document.createElement("div");
    messageItem.textContent = fromUser + ": " + message;
    messages.appendChild(messageItem);
    console.log("Appended message: ", messageItem.textContent);
});

connection.start().then(function () {
    connection.invoke("GetOldMessages").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageForm").addEventListener("submit", function (event) {
    event.preventDefault();
    let messageInput = document.getElementById("messageInput");
    let message = messageInput.value;
    connection.invoke("SendMessage", fromUser, message).catch(function (err) {
        return console.error(err.toString());
    });
    messageInput.value = '';
});

connection.onclose(async () => {
    await startSignalRConnection(connection);
});

function displayError(message) {
    let messages = document.getElementById("messages");
    let messageItem = document.createElement("div");
    messageItem.textContent = "Error: " + message;
    messageItem.style.color = "red";
    messages.appendChild(messageItem);
    window.scrollTo(0, document.body.scrollHeight);
}

async function startSignalRConnection(connection) {
    try {
        await connection.start();
        console.log('connected');

        connection.invoke("GetOldMessages")
            .then(function (oldMessages) {
                console.log("Received old messages: ", oldMessages);
                for (let i = 0; i < oldMessages.length; i++) {
                    let message = oldMessages[i];
                    let messages = document.getElementById("messages");
                    let messageItem = document.createElement("div");
                    messageItem.textContent = message.fromUser + ": " + message.message;
                    messageItem.className = message.fromUser === user.fullName ? 'user-message' : 'other-message';
                    messages.appendChild(messageItem);
                }
                window.scrollTo(0, document.body.scrollHeight);
            })
            .catch(function (err) {
                console.error('Error while getting old messages: ' + err);
                displayError('Error while getting old messages: ' + err);
            });
    } catch (err) {
        console.log('Error while establishing connection: ' + err);
        displayError('Error while establishing connection: ' + err);
        setTimeout(() => startSignalRConnection(connection), 5000);
    }
}