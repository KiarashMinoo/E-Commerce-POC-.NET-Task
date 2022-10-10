#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV http_proxy="http://192.168.1.10:1080"
ENV https_proxy="http://192.168.1.10:1080"
ENV no_proxy="*192.168.1.200:8080"

RUN apt-get update
RUN apt-get -y install curl

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV http_proxy="http://192.168.1.10:1080"
ENV https_proxy="http://192.168.1.10:1080"
ENV no_proxy="*192.168.1.200:8080"
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

ENV http_proxy="http://192.168.1.10:1080"
ENV https_proxy="http://192.168.1.10:1080"
ENV no_proxy="*192.168.1.200:8080"
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll", "--urls", "http://0.0.0.0:80"]