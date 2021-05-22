FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore dockerapi.csproj
COPY . ./
RUN dotnet publish dockerapi.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "dockerapi.dll"]