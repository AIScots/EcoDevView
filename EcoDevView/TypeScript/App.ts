///<reference path="typings/jquery/jquery.d.ts" />
///<reference path="typings/signalr/signalr.d.ts" />

$(function () {
    var mapHub = $.connection.mapHub;
    
    // Register the callbacks
    mapHub.client.updateAnimals = function (animals: IAnimal[]) {
        console.debug('Received animal update: ', animals);
    };

    mapHub.client.updatePlants = function (plants: IPlant[]) {
        console.debug('Received plant update:', plants);
    };

    // Start the whole business
    $.connection.hub.start().then(function () {
        console.log("SignalR started");
    })
    .fail(function (error) {
        console.error("SignalR could not be started", error);
    });
});