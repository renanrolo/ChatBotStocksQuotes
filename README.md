Hello, this is a little project to show how to use communication with SignalR and RabbitMQ.


How to run
1. Front-end
go to folder ```./Front-End/stocks-chat/```
Open command prompt
run ```npm install``` to install/download dependencies
aftter that you execute a ```npm start``` to run it
you will see the project running on http://localhost:3000/

2. Back-End API
go to folder ```./BackEnd/ChatBotStocksQuotes.Api/```
Open command prompt
run ```dotnet run```
It will be possible to access swagger on ```https://localhost:44394/swagger/index.html```

3. Back-End Bot
go to folder ```./BackEnd/ChatBotStocksQuotes.Bot/```
Open command prompt
run ```dotnet run```
There is no interface, just the promp to see some logs.

RabbitMq
It's possible to access RabbitMq's dashboard on ```http://localhost:15672``` with login ```guest``` and password ```guest```
