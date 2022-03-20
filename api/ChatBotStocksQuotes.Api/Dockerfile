FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChatBotStocksQuotes.Api/ChatBotStocksQuotes.Api.csproj", "ChatBotStocksQuotes.Api/"]
RUN dotnet restore "ChatBotStocksQuotes.Api/ChatBotStocksQuotes.Api.csproj"
COPY . .
WORKDIR "/src/ChatBotStocksQuotes.Api"
RUN dotnet build "ChatBotStocksQuotes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatBotStocksQuotes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBotStocksQuotes.Api.dll"]
