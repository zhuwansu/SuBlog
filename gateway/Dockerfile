#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["gateway/Sublog.csproj", "gateway/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DbEntities/DbEntities.csproj", "DbEntities/"]
COPY ["DbEnums/DbEnums.csproj", "DbEnums/"]
COPY ["DapperDb/DapperDb.csproj", "DapperDb/"]
COPY ["DBProvider/DBProvider.csproj", "DBProvider/"]
COPY ["../ApiModels/ApiModels.csproj", "../ApiModels/"]
RUN dotnet restore "gateway/Sublog.csproj"
COPY . .
WORKDIR "/src/gateway"
RUN dotnet build "Sublog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sublog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sublog.dll"]