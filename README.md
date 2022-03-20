Hello, this is a little project to show how to use communication with SignalR and RabbitMQ.

WIP: MAJOR CHANGE
Altering solution to use docker-compose
1. Front-end:
    - WIP: TO DO
2. Back-End API:
    - OK
3. Back-End Bot:
    - OK
4. RabbitMq:
    - OK
5. MsSql:
    - OK

For now Back-End API is working fine, on docker.compose.yml you can see a little change on the connection string, but thats how it's done.

Back-End API will run on port 5000 with docker-compose, Front-end doesn't know that lol

# Old documentation above

How to run

Before everything, go to folder ```.\docker_compose```, opem command promt and run ```docker-compose up``` to start SQL and RabbitMq servers

##### Front-end

* go to folder ```./Front-End/stocks-chat/```
* open command prompt
* run ```npm install``` to install/download dependencies
* aftter that you execute a ```npm start``` to run it
* you will see the project running on http://localhost:3000/

##### Back-End API

* go to folder ```./BackEnd/ChatBotStocksQuotes.Api/```
* open command prompt
* run ```dotnet run```
* It will be possible to access swagger on ```https://localhost:44394/swagger/index.html```

##### Back-End Bot

* go to folder ```./BackEnd/ChatBotStocksQuotes.Bot/```
* open command prompt
* run ```dotnet run```
* there is no interface, just the promp to see some logs.

## Observations:

RabbitMq

It's possible to access RabbitMq's dashboard on ```http://localhost:15672``` with login ```guest``` and password ```guest```

The database is created automatically by migrations
