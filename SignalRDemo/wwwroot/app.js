let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("demohub")
        .build();

    connection.on("Ping", (message) => {
        console.log(message);
        //const statusDiv = document.getElementById("status");
            //statusDiv.innerHTML = update;
        }
    );

    connection.on("finished", function() {
            connection.stop();
        }
    );

    connection.start()
        .then(value => connection.invoke("Connect", "Hello"))
        .catch(err => console.error(err.toString()));
};

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    if (connection != null) {
        connection.stop().then(value => setupConnection());
    } else {
        setupConnection();
    }
});
