#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CrimeAPI/CrimeService.csproj", "CrimeAPI/"]
COPY ["CommonItems/CommonItems.csproj", "CommonItems/"]
COPY ["EventBus.Messaging/EventBus.Messaging.csproj", "EventBus.Messaging/"]
RUN dotnet restore "CrimeAPI/CrimeService.csproj"
COPY . .
WORKDIR "/src/CrimeAPI"
RUN dotnet build "CrimeService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrimeService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrimeService.dll"]