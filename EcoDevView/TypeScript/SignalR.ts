///<reference path="typings/jquery/jquery.d.ts" />
interface SignalR {
    mapHub: IMapHubProxy;
}

interface IMapHubProxy {
    client: IMapClient;
    server: IMapServer;
}

interface IMapClient {
    updateAnimals(animals: IAnimal[]);
    updatePlants(plants: IPlant[]);
}

interface IMapServer {
    renameAnimal(id: number, newName: string): JQueryPromise<void>;
    setAnimalHealth(id: number, newHealth: number) : JQueryPromise<void>;
}