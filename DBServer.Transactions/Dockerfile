#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DBServer.Transactions/DBServer.Transactions.csproj", "DBServer.Transactions/"]
COPY ["DBServer.Application/DBServer.Application.csproj", "DBServer.Application/"]
COPY ["DBServer.DataBase/DBServer.DataBase.csproj", "DBServer.DataBase/"]
COPY ["DBServer.Domain/DBServer.Domain.csproj", "DBServer.Domain/"]
COPY ["DBServer.Infra/DBServer.Infra.csproj", "DBServer.Infra/"]
RUN dotnet restore "DBServer.Transactions/DBServer.Transactions.csproj"
COPY . .
WORKDIR "/src/DBServer.Transactions"
RUN dotnet build "DBServer.Transactions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DBServer.Transactions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DBServer.Transactions.dll"]