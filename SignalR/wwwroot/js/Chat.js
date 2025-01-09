console.log("Aboellil is man");
// Define Connection
var con = new signalR.HubConnectionBuilder().withUrl("/chat").build();

// Start Connection
con.start()
    .then(() => {
        console.log("SignalR connection established.");
        GetAll(); // Call GetAll after the connection is successfully established
    })
    .catch(err => console.error(err.toString()));

// Define Callback function
con.on("GetAllMesseges", (name, messege) => {
    $("#messeges").append("<li>" + name + " : " + messege + "</li>");
});

con.on("NewMessege", function (name, messege) {
    $("#messeges").append("<li>" + name + " : " + messege + "</li>");
});

con.on("groupsend", function (name, groupname) {
    console.log("Received groupsend:", { name, groupname });
    $("#messeges").append("<li>" + name + " ::::: " + groupname + "</li>");
});




// Function to consume service
function SendM() {

    var SinderName = $("#name").val();
    var SinderMessege = $("#messege").val();

    con.invoke("SendMessege", SinderName, SinderMessege).catch(err => console.error(err.toString()));

    $("#messege").val("");
}

function GetAll() {
    con.invoke("GetAll").catch(err => console.error(err.toString()));
}
console.log("Script loaded!");

function JoinGroup() {
    var SinderName = $("#name").val();
    var GroupName = $("#groupname").val();

    console.log("Joining:", GroupName, "with name:", SinderName); // Debug log

    con.invoke("joingroup", GroupName, SinderName);
}


$("#SendBtn").on("click", () => {
    SendM();
});

$("#messege").on("keypress", (e) => {
    if (e.key === "Enter") {
        e.preventDefault();
        SendM();
    }
})


$("#joinbtn").on("click", () => {
    JoinGroup();
});

