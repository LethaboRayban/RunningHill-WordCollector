FROM microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

#Copy solution files and restore
COPY ["WordCollector.slnx", "./"]
COPY ["WordCollector.Api/WordCollector.Api.csproj", "WordCollector.Api/"]
COPY ["WordCollector.Client/WordCollector.Client.csproj", "WordCollector.Client/"]
RUN dotnet restore "WordCollector.slnx"

#Copy the rest of the code and publish
COPY . .
WORKDIR "/src/WordCollector.Api"
RUN dotnet publish "WordCollector.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Runtime image
FROM microsoft.com/dotnet/sdk:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

#Dynamically assigns a PORT
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "WordCollector.Api.dll"]