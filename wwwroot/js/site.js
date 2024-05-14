window.initializeSignalR = async () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.on("ReceiveMessage", (user, msg, timestamp) => {
        const messagesList = document.getElementById("messagesList");
        messagesList.innerHTML += `<li>${user}: ${msg} (${timestamp})</li>`;
    });

    connection.on("ConnectedUsers", users => {
        const usersList = document.getElementById("usersList");
        usersList.innerHTML = "";
        users.forEach(user => {
            usersList.innerHTML += `<li>${user}</li>`;
        });
    });

    await connection.start();
    console.log("SignalR connected.");

    window.sendMessage = async () => {
        const messageInput = document.getElementById("messageInput");
        const message = messageInput.value.trim();
        if (message !== "") {
            await connection.invoke("SendMessage", message);
            messageInput.value = "";
        }
    };
};
