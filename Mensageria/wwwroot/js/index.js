"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

var changeView = {
    load: function (divname, msg) {
        var divCardBorder = document.createElement("div");
        divCardBorder.className = "card col-lg-4";
        divCardBorder.style.fontSize = "0.8em";
        divCardBorder.style.marginBottom = "3%";
    
        var divCardBody = document.createElement("div");
        divCardBody.className = "card-body";
        divCardBorder.appendChild(divCardBody)
        
        var title = document.createElement("div");
        title.className = "card-header bg-transparent";
        title.textContent = "Sender: " + msg.ApplicationName;
        title.style.color = "black";
        title.style.fontWeight = "bold"
        divCardBody.appendChild(title);
    
        var ul = document.createElement("ul");
        ul.className = "list-group list-group-flush";
        divCardBody.appendChild(ul);
    
        var liid = document.createElement("li");
        liid.className = "list-group-item";
        liid.textContent = "Message Id: " + msg.Id;
        ul.appendChild(liid);
    
        var limsg = document.createElement("li");
        limsg.className = "list-group-item";
        limsg.textContent = "Message: " + msg.Message;
        ul.appendChild(limsg);
    
        var lidate = document.createElement("li");
        lidate.className = "list-group-item";
        lidate.textContent = "Timestamp: " + msg.Timestamp;
        ul.appendChild(lidate);
    
        document.getElementById(divname).appendChild(divCardBorder);
    }
  }

connection.on("ReceiveMessage", function (user, message) {
    var msg = JSON.parse(message)

    changeView.load("container", msg);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});