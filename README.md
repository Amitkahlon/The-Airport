
# The Airport Project

### Description
A multi-threaded path-finding simulator.

**Airport**

You create an airport. Each airport is built of stations and has a different number of stations. For example, an airport can have 3 "runway" stations).

**Stations**

Stations have connections to other stations, so planes in a station can only travel from one station to another only if they are connected(connection can be one way).
stations can take care of only one plane at a time, and for a plane to release the station it has to wait for that station's "Waiting Time".

**Planes** 

Planes have a custom route defining the stations they need to visit, by order. They cannot skip station.
These routes can also have multiple station options, for example, a plane with *a "Landing"* route can visit one of the 3 landing strips the airport can offer, when a plane finishes its route it vanishes from the program.

Planes will search for the least busy station. Bad routes and long station "Waiting time" can easily create a bottleneck.

Actions like plane enter/left the airport, enter/left the station etc.. are saved in the db with data like timestamp, planeId, stationId etc..

## Tech Stack

- UI: WPF C#.
- Backend: ASP.NET, REST, SignalR, C#.
- Database: SQLite, using Entity framework.
- Tests: Some integration tests, C#.


## Project Architecture

The server does all the calculations, Ui instances can connect to the server to receive real-time updates.

The UI displays existing airports, stations, waiting times, planes, plane positions, and logs that are saved in the DB.

You can also see specific details for each plane, station, and airport.

The solution contains these separate projects: 
- Client
- Common
- DAL
- Server
- Test
- Logic
- Plane Maker


## Make Sure

Make sure you run the program from both the client(Wpf) and Server(asp.net core).

Check the `Startup.cs`, if you have your database ready with airports already use PlaneMaker.Load.

If you don't have airports and want to load an airport example, use `PlaneMaker.CreadAndLoad` But **MAKE SURE** next time you run the program on `PlaneMaker.Load` Mode!

## Demo

[Insert gif or link to demo](https://user-images.githubusercontent.com/50583120/187707555-e6ea796d-fc6d-4637-93af-cadf678fdc4b.mp4)


## Authors

- [@Amit Kahlon](https://www.github.com/amitkahlon)

